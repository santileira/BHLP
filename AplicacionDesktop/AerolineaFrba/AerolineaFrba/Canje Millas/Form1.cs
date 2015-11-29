using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AerolineaFrba.Canje_Millas
{
    public partial class Form1 : Form
    {

        public int dni;
        public string apellido;
        public int millasDisponibles;
        public int millasDispFijas;
        public Form anterior;
        public bool seSeleccionoPremio;

        public Form1()
        {
            InitializeComponent();

            this.llenarListadoProductos();
            seSeleccionoPremio = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            puntosDisp.Text = millasDisponibles.ToString();
        }

        public void inicio()
        {
            txtProdSeleccionado.Text = "";
            txtCantSeleccionada.Text = "";
            puntosDisp.Text = millasDisponibles.ToString();

            listaPremiosSelec.Items.Clear();
            listCantSelec.Items.Clear();

            millasDisponibles = millasDispFijas;

            foreach (DataGridViewRow row in dgListadoProductos.Rows)
            {
                row.Cells["Stock"].Style.BackColor = Color.White;
                row.Cells["Producto"].Style.BackColor = Color.White;
                row.Cells["Puntos"].Style.BackColor = Color.White;
            }

            seSeleccionoPremio = false;

        }

        //Método usado para llenar en el dg el resultado de la query. En este caso la query arroja todos los premios
        //que poseen stock. 

        private void llenarListadoProductos()
        {
            string query = "SELECT PREMIO_COD,PREMIO_DETALLE as Producto, PREMIO_PUNTOS as Puntos,PREMIO_STOCK as Stock FROM ABSTRACCIONX4.PREMIOS WHERE PREMIO_STOCK > 0";

            SqlConnection conexion = Program.conexion();

            DataTable t = new DataTable("Busqueda");
            SqlDataAdapter a = new SqlDataAdapter(query, conexion);
            //Llenar el Dataset
            DataSet ds = new DataSet();
            a.Fill(ds, "Busqueda");
            //Ligar el datagrid con la fuente de datos
            dgListadoProductos.DataSource = ds;
            dgListadoProductos.DataMember = "Busqueda";

            this.dgListadoProductos.Columns["PREMIO_COD"].Visible = false;


            conexion.Close();
        }

        private void dgListadoProductos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!dgListadoProductos.SelectedRows[0].Cells["Stock"].Style.BackColor.Equals(Color.DarkTurquoise))
            {
                button1.Enabled = true;
                string premio_detalle = dgListadoProductos.SelectedRows[0].Cells["Producto"].Value.ToString();
                txtProdSeleccionado.Text = premio_detalle;
                seSeleccionoPremio = true;

            }
            else
            {
                button1.Enabled = false;
                dgListadoProductos.SelectedRows[0].Selected = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            if(!seSeleccionoPremio)
                MessageBox.Show("Se debe seleccionar un premio", "Error", MessageBoxButtons.OK);
            if (validarCampo() && seSeleccionoPremio)
            {
                if (hayStockDisponible())
                {
                    this.agregarALista();
                    dgListadoProductos.SelectedRows[0].Selected = false;
                    button1.Enabled = false;
                    seSeleccionoPremio = false;
                }

            }

         
        }

        private Boolean validarCampo()
        {
            return !Validacion.esVacio(txtCantSeleccionada, "Cant. Seleccionada",true) &&
                    Validacion.esNumero(txtCantSeleccionada, "Cant. Seleccionada",true);
        }

        //Método que verifica en base a los resultados del DG, si hay stock disponible y si las millas del cliente
        // son suficientes como para realizar el canje.

        private bool hayStockDisponible()
        {
            string cantStock = dgListadoProductos.SelectedRows[0].Cells["Stock"].Value.ToString();
            int stockCant;
            stockCant = Convert.ToInt32(cantStock);
            stockCant = int.Parse(cantStock);

            string cantPuntos = dgListadoProductos.SelectedRows[0].Cells["Puntos"].Value.ToString();
            int puntos;
            puntos = Convert.ToInt32(cantPuntos);
            puntos = int.Parse(cantPuntos);

            int cantSeleccionada;
            cantSeleccionada = Convert.ToInt32(txtCantSeleccionada.Text);
            cantSeleccionada = int.Parse(txtCantSeleccionada.Text);


            int millasDisp = millasDisponibles - cantSeleccionada * puntos;

            if (millasDisp > 0)
            {

                if (stockCant >= cantSeleccionada)
                {
                    dgListadoProductos.SelectedRows[0].Cells["Stock"].Style.BackColor = Color.DarkTurquoise;
                    dgListadoProductos.SelectedRows[0].Cells["Producto"].Style.BackColor = Color.DarkTurquoise;
                    dgListadoProductos.SelectedRows[0].Cells["Puntos"].Style.BackColor = Color.DarkTurquoise;


                    millasDisponibles = millasDisp;
                    puntosDisp.Text = millasDisponibles.ToString();

                    return true;
                }
                else
                {
                    MessageBox.Show("No hay stock para realizar el canje", "Error", MessageBoxButtons.OK);
                }
            }
            else
            {
                MessageBox.Show("No hay suficientes millas para realizar el canje", "Error", MessageBoxButtons.OK);
            }

            return false;

        }

        private void agregarALista()
        {
            listaPremiosSelec.Items.Add(((TextBox)txtProdSeleccionado).Text);
            listCantSelec.Items.Add(((TextBox)txtCantSeleccionada).Text);

        }

        private void button4_Click(object sender, EventArgs e)
        {
            listaPremiosSelec.Items.Clear();
            listCantSelec.Items.Clear();
            txtCantSeleccionada.Clear();
            txtProdSeleccionado.Clear();

            puntosDisp.Text = millasDispFijas.ToString();
            millasDisponibles = millasDispFijas;

            foreach (DataGridViewRow row in dgListadoProductos.Rows)
            {
                row.Cells["Stock"].Style.BackColor = Color.White;
                row.Cells["Producto"].Style.BackColor = Color.White;
                row.Cells["Puntos"].Style.BackColor = Color.White;
            }

            seSeleccionoPremio = false;
        }

        //Método responsable de efectuar el canje una vez que todo sea correcto. Además se encarga de ejecutar
        //los SP necesarios. Por ejemplo, reducir el stock del producto y dar de alta el registro en canje.

        private void button2_Click(object sender, EventArgs e)
        {
            int puntosDisponibles;
            int.TryParse(puntosDisp.Text, out puntosDisponibles);
            int totalPuntos = millasDispFijas - millasDisponibles;

            if (totalPuntos == 0)
            {
                MessageBox.Show("Debe seleccionar al menos un producto", "Error", MessageBoxButtons.OK);
            }
            else
            {
                if (puntosDisponibles > 0)
                {

                    int cantElementos = listaPremiosSelec.Items.Count;
                    for (int i = 0; i < cantElementos; i++)
                    {
                        string descripcion = listaPremiosSelec.Items[i].ToString();
                        int cantSelect;
                        int.TryParse(listCantSelec.Items[i].ToString(), out cantSelect);

                        new SQLManager().generarSP("reducirStockDePremio")
                                       .agregarStringSP("@descripcion", descripcion)
                                         .agregarIntSP("@cantidadSolicitada", cantSelect)
                                            .ejecutarSP();



                        new SQLManager().generarSP("insertarCanje")
                                        .agregarFechaSP("@canje_fecha", Program.fechaHoy())
                                         .agregarIntSP("@canje_cantidad", cantSelect)
                                          .agregarStringSP("@descripcion", descripcion)
                                           .agregarIntSP("@dni", dni)
                                            .agregarStringSP("@ape", apellido)
                                                .ejecutarSP();


                    }


                    MessageBox.Show("Puntos a Gastar: " + totalPuntos, "Canje", MessageBoxButtons.OK);
                    MessageBox.Show("Se efectuó el Canje", "Canje Exitoso", MessageBoxButtons.OK);

                    this.Close();

                }
                else
                {
                    MessageBox.Show("No hay suficientes millas para realizar el canje", "Error", MessageBoxButtons.OK);
                    this.cambiarVisibilidades(anterior);
                }

            }


        }

        private int obtenerPuntosDeUnPremio(string description)
        {

            SqlCommand command = new SqlCommand();
            command.Connection = Program.conexion();
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = "SELECT ABSTRACCIONX4.obtenerPuntosDePremio(@descripcion)";
            command.CommandTimeout = 0;

            command.Parameters.AddWithValue("@descripcion", description);

            return (int)command.ExecuteScalar();

        }

        private void cambiarVisibilidades(Form formularioSiguiente)
        {
            formularioSiguiente.Visible = true;
            this.Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.cambiarVisibilidades(anterior);
        }

    }
}
