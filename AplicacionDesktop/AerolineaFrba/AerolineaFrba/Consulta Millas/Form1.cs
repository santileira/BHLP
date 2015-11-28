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

namespace AerolineaFrba.Consulta_Millas
{
    public partial class Form1 : Form
    {

        public Form canjeMillas;
        public Boolean canjeHabilitado;

        public Form1(Boolean canjeHabilitado)
        {
            this.canjeHabilitado = canjeHabilitado;
            InitializeComponent();            
        }

        private void llenarHistorialDeMillas()
        {
            string query = "SELECT Tipo,Origen,Destino,[Fecha de Compra],Precio,CAST(Precio/10 as Int) as 'Cant. de Millas'  FROM [ABSTRACCIONX4].obtenerHistorialMillasPasajes(" + txtDni.Text + ",'" + txtApe.Text + "') UNION SELECT Tipo,Origen,Destino,[Fecha de Compra],Precio,CAST(Precio/10 as Int) as 'Cant. de Millas'  FROM [ABSTRACCIONX4].obtenerHistorialMillasEncomiendas(" + txtDni.Text + ",'" + txtApe.Text + "')";

            SqlConnection conexion = Program.conexion();

            DataTable t = new DataTable("Busqueda");
            SqlDataAdapter a = new SqlDataAdapter(query, conexion);
            //Llenar el Dataset
            DataSet ds = new DataSet();
            a.Fill(ds, "Busqueda");
            //Ligar el datagrid con la fuente de datos
            dgHistorial.DataSource = ds;
            dgHistorial.DataMember = "Busqueda";

            conexion.Close();



            SqlDataReader totalMillas;
            SqlDataReader millasCanjeadas;

            SqlCommand consultaTotal = new SqlCommand();
            SqlCommand consultaCanjeadas = new SqlCommand();

            consultaTotal.CommandType = CommandType.Text;
            consultaCanjeadas.CommandType = CommandType.Text;

            consultaTotal.CommandText = "SELECT SUM(Millas) FROM (SELECT CAST(Precio/10 as Int) as Millas FROM [ABSTRACCIONX4].obtenerHistorialMillasPasajes(" + txtDni.Text + ",'" + txtApe.Text + "') UNION ALL  SELECT CAST(Precio/10 as Int) as Millas FROM [ABSTRACCIONX4].obtenerHistorialMillasEncomiendas(" + txtDni.Text + ",'" + txtApe.Text + "')) as Sarasa";
            consultaCanjeadas.CommandText = "SELECT SUM(P.PREMIO_PUNTOS * C.CANJE_CANTIDAD) FROM ABSTRACCIONX4.CANJES C JOIN ABSTRACCIONX4.PREMIOS P ON C.PREMIO_COD = P.PREMIO_COD WHERE C.CLI_COD = (SELECT CU.CLI_COD FROM ABSTRACCIONX4.CLIENTES CU WHERE CU.CLI_DNI =" + txtDni.Text + " AND CU.CLI_APELLIDO = '" + txtApe.Text + "' ) AND C.CANJE_FECHA BETWEEN [ABSTRACCIONX4].obtenerFechaDeHoy() - 365 AND [ABSTRACCIONX4].obtenerFechaDeHoy()";

            consultaTotal.Connection = Program.conexion();
            consultaCanjeadas.Connection = Program.conexion();

            totalMillas = consultaTotal.ExecuteReader();
            millasCanjeadas = consultaCanjeadas.ExecuteReader();

            totalMillas.Read();
            millasCanjeadas.Read();

            string totMillas = totalMillas.GetValue(0).ToString();
            string millasCanj = millasCanjeadas.GetValue(0).ToString();
            int millasTotales;
            int millas_canjeadas;

            int.TryParse(totMillas, out millasTotales);
            int.TryParse(millasCanj, out millas_canjeadas);

            var total = millasTotales;
            var canjeadas = millas_canjeadas;
            var final = total - canjeadas;


            cantTotalMillas.Text = final.ToString();

        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Boolean camposInvalidos = false;
            if (validarCampos())
            {
                this.llenarHistorialDeMillas();
                this.llenarHistorialDeCanjes();
            }
            else
                camposInvalidos = true;

            if (dgHistorial.RowCount > 0)
            {
                this.button3.Enabled = true;
            }
            else
            {
                this.button3.Enabled = false;
                if(!camposInvalidos)
                MessageBox.Show("El cliente es inválido o bien no posee puntos disponibles a la fecha.", "Canje de Millas", MessageBoxButtons.OK);
            
             }
            
        }

        private Boolean validarCampos()
        {
            Boolean esValido = true;

            esValido = !Validacion.esVacio(txtDni, "Dni", true) && esValido;
            esValido = !Validacion.esVacio(txtApe, "Apellido", true) && esValido;
            esValido = Validacion.esNumero(txtDni, "Dni", true) && esValido;
            esValido = Validacion.estaEntreLimites(txtDni, 0, 99999999, true, "Dni") && esValido;
            esValido = Validacion.esSoloTexto(txtApe, "Apellido" , true) && esValido;
           

            return esValido;
        }


        private void cambiarVisibilidades(Form formularioSiguiente)
        {
            formularioSiguiente.Visible = true;
            this.Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {


            if (validarCampos())
            {

                if (dgHistorial.RowCount > 0)
                {

                    (canjeMillas as Canje_Millas.Form1).inicio();
                    int dni;
                    dni = Convert.ToInt32(txtDni.Text);
                    dni = int.Parse(txtDni.Text);
                    int cantMillas;
                    cantMillas = Convert.ToInt32(cantTotalMillas.Text);
                    cantMillas = int.Parse(cantTotalMillas.Text);


                    (canjeMillas as Canje_Millas.Form1).dni = dni;
                    (canjeMillas as Canje_Millas.Form1).apellido = txtApe.Text;
                    (canjeMillas as Canje_Millas.Form1).millasDisponibles = cantMillas;
                    (canjeMillas as Canje_Millas.Form1).millasDispFijas = cantMillas;
                    (canjeMillas as Canje_Millas.Form1).inicio();

                    this.cambiarVisibilidades(canjeMillas);

                }
                else
                {
                    MessageBox.Show("El cliente es inválido o bien no posee puntos disponibles a la fecha.", "Canje de Millas", MessageBoxButtons.OK);
                }

            }

        }


        private void Form1_Load(object sender, EventArgs e)
        {
            button3.Visible = canjeHabilitado;
        }

        private void txtDni_TextChanged(object sender, EventArgs e)
        {
            this.button3.Enabled = false;
        }

        private void txtApe_TextChanged(object sender, EventArgs e)
        {
            this.button3.Enabled = false;
        }

        private void llenarHistorialDeCanjes()
        {
            string query = "SELECT C.CANJE_FECHA as Fecha, P.PREMIO_DETALLE as Premio, C.CANJE_CANTIDAD as Cantidad, P.PREMIO_PUNTOS * C.CANJE_CANTIDAD as 'Puntos Consumidos' FROM ABSTRACCIONX4.CANJES C JOIN ABSTRACCIONX4.PREMIOS P ON C.PREMIO_COD = P.PREMIO_COD WHERE C.CLI_COD = (SELECT CU.CLI_COD FROM ABSTRACCIONX4.CLIENTES CU WHERE CU.CLI_DNI =" + txtDni.Text + " AND CU.CLI_APELLIDO = '"+ txtApe.Text  +"' )";

            SqlConnection conexion = Program.conexion();

            DataTable t = new DataTable("Busqueda");
            SqlDataAdapter a = new SqlDataAdapter(query, conexion);
            //Llenar el Dataset
            DataSet ds = new DataSet();
            a.Fill(ds, "Busqueda");
            //Ligar el datagrid con la fuente de datos
            dgCanjes.DataSource = ds;
            dgCanjes.DataMember = "Busqueda";

            conexion.Close();

        }


    }
}
