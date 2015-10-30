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

        public Form1()
        {
            InitializeComponent();
        }

        private void llenarHistorialDeMillas()
        {
            string query = "SELECT Tipo,Origen,Destino,[Fecha de Compra],Precio,[Cant. de Millas]  FROM [ABSTRACCIONX4].obtenerHistorialMillasPasajes(" + txtDni.Text + ",'" + txtApe.Text + "') UNION SELECT Tipo,Origen,Destino,[Fecha de Compra],Precio,[Cant. de Millas]  FROM [ABSTRACCIONX4].obtenerHistorialMillasEncomiendas(" + txtDni.Text + ",'" + txtApe.Text + "') ORDER BY [Fecha de Compra] DESC";

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
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.llenarHistorialDeMillas();

            int cantidad = 0;
            int rowsCount = dgHistorial.RowCount;
            int i = 0;
            foreach (DataGridViewRow row in dgHistorial.Rows)
            {

                if (i < rowsCount - 1)
                {
                    cantidad = cantidad + (int)row.Cells[5].Value;
                }

                i = i + 1;
            }
            
            cantTotalMillas.Text = cantidad.ToString();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.cambiarVisibilidades(new Menu());
        }

        private void cambiarVisibilidades(Form formularioSiguiente)
        {
            formularioSiguiente.Visible = true;
            this.Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
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

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
