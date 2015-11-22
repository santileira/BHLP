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

            SqlCommand consultaTotal = new SqlCommand();

            consultaTotal.CommandType = CommandType.Text;

            consultaTotal.CommandText = "SELECT SUM([Cant. de Millas]) FROM (SELECT * FROM [ABSTRACCIONX4].obtenerHistorialMillasPasajes(" + txtDni.Text + ",'" + txtApe.Text + "') UNION  SELECT * FROM [ABSTRACCIONX4].obtenerHistorialMillasEncomiendas(" + txtDni.Text + ",'" + txtApe.Text + "')) as Sarasa";

            consultaTotal.Connection = Program.conexion();

            totalMillas = consultaTotal.ExecuteReader();

            totalMillas.Read();
            cantTotalMillas.Text = totalMillas.GetValue(0).ToString();

        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (validarCampos())
            {
                this.llenarHistorialDeMillas();
                this.llenarHistorialDeCanjes();
            }

            if (dgHistorial.RowCount > 0)
            {
                this.button3.Enabled = true;
            }
            else
            {
                this.button3.Enabled = false;
            }
            
        }

        private Boolean validarCampos()
        {

            return !Validacion.esVacio(txtDni, "Dni",true)  &&
                        Validacion.numeroCorrecto(txtDni, "Dni", true) &&
                            Validacion.textNombre(txtApe, "Apellido");
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
            string query = "SELECT C.CANJE_FECHA as Fecha, P.PREMIO_DETALLE as Premio, C.CANJE_CANTIDAD as Cantidad, P.PREMIO_PUNTOS * C.CANJE_CANTIDAD as 'Puntos Consumidos' FROM ABSTRACCIONX4.CANJES C JOIN ABSTRACCIONX4.PREMIOS P ON C.PREMIO_COD = P.PREMIO_COD";

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
