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

namespace AerolineaFrba.Compra
{
    public partial class Form2 : Form
    {
        public Form anterior;

        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            this.inicio();
        }

        public void inicio()
        {
            txtApe.Text = "";
            txtDire.Text = "";
            txtDni.Text = "";
            txtNac.Text = "";
            txtNom.Text = "";
            txtTel.Text = "";
            txtMail.Text = "";

            txtApe.Enabled = false;
            txtDire.Enabled = false;
            txtNac.Enabled = false;
            txtNom.Enabled = false;
            txtTel.Enabled = false;
            txtMail.Enabled = false;

            button1.Enabled = false;
                 
        }

        private void cambiarVisibilidades(Form formularioSiguiente)
        {
            formularioSiguiente.Visible = true;
            this.Visible = false;
        }

        public void seSelecciono(DataGridViewRow registro)
        {
            this.ejecutarQuery("select * from [ABSTRACCIONX4].butacasDisponibles('" + registro.Cells["VIAJE_COD"].Value.ToString() + "', '" + registro.Cells["AERO_MATRI"].Value.ToString() + "')", dgButacas);
            this.ejecutarQuery("select * from [ABSTRACCIONX4].kilosDisponibles('" + registro.Cells["VIAJE_COD"].Value.ToString() + "', '" + registro.Cells["AERO_MATRI"].Value.ToString() + "')", dgKilos);

            string kilos = dgKilos.Rows[0].Cells["Kilos"].Value.ToString();

            (((this.anterior as Compra.Form4).anterior as Compra.Form3).anterior as Compra.Form1).completarCantidades(dgButacas.RowCount, kilos);
        }

        private void ejecutarQuery(string query, DataGridView unDg)
        {           
            SqlConnection conexion = Program.conexion();
            DataTable t = new DataTable("Busqueda");
            SqlDataAdapter a = new SqlDataAdapter(query, conexion);
            //Llenar el Dataset
            DataSet ds = new DataSet();
            a.Fill(ds, "Busqueda");
            //Ligar el datagrid con la fuente de datos

            unDg.DataSource = ds;
            unDg.DataMember = "Busqueda";
       
            conexion.Close();
        }
        
        private void button6_Click(object sender, EventArgs e)
        {
            this.cambiarVisibilidades(this.anterior);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtDire.Enabled = true;
            txtNac.Enabled = true;
            txtNom.Enabled = true;
            txtTel.Enabled = true;
            txtMail.Enabled = true;

            this.ejecutarQuery("select * from [ABSTRACCIONX4].buscarCliente('" + txtDni.Text + "', '" + txtApe.Text + "')", dgCliente);

            if (dgCliente.RowCount == 1)
            {
                dgKilos.Rows[0].Cells["Kilos"].Value.ToString();

                txtDire.Text = dgCliente.Rows[0].Cells["CLI_DIRECCION"].Value.ToString();
                txtNac.Text = dgCliente.Rows[0].Cells["CLI_FECHA_NAC"].Value.ToString();
                txtNom.Text = dgCliente.Rows[0].Cells["CLI_NOMBRE"].Value.ToString();
                txtTel.Text = dgCliente.Rows[0].Cells["CLI_TELEFONO"].Value.ToString();
                txtMail.Text = dgCliente.Rows[0].Cells["CLI_MAIL"].Value.ToString();
            }
            else
                MessageBox.Show("No se encuentra cargado el cliente en la BD. Por favor, ingresar los datos para darle de alta", "Cliente no encontrado", MessageBoxButtons.OK);
        
        }


        private void txtDni_TextChanged(object sender, EventArgs e)
        {
            txtApe.Enabled = true;
        }

        private void txtApe_TextChanged(object sender, EventArgs e)
        {
            button1.Enabled = true;
        }
    }
}
