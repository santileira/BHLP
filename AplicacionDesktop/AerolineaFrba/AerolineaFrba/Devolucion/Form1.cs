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

namespace AerolineaFrba.Devolucion
{
    public partial class dgEncomiendas : Form
    {
        public dgEncomiendas()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.inicio();
        }

        public void inicio()
        {
            this.btBuscar.Enabled = false;
            this.txtCodigo.Text = "";
                        
        }

        private void llenarPasajes(string codigo)
        {
            string query = "SELECT * FROM [ABSTRACCIONX4].PASAJES WHERE COMP_COD = [ABSTRACCIONX4].ObtenerCodigo(" + codigo + ")"; 
            
            SqlConnection conexion = Program.conexion();

            DataTable t = new DataTable("Busqueda");
            SqlDataAdapter a = new SqlDataAdapter(query, conexion);
            //Llenar el Dataset
            DataSet ds = new DataSet();
            a.Fill(ds, "Busqueda");
            //Ligar el datagrid con la fuente de datos
            dgPasaje.DataSource = ds;
            dgPasaje.DataMember = "Busqueda";

            conexion.Close();
        }

        private void llenarEncomiendas(string codigo)
        {
            string query = "SELECT * FROM [ABSTRACCIONX4].ENCOMIENDAS WHERE COMP_COD = [ABSTRACCIONX4].ObtenerCodigo(" + codigo +")";

            SqlConnection conexion = Program.conexion();

            DataTable t = new DataTable("Busqueda");
            SqlDataAdapter a = new SqlDataAdapter(query, conexion);
            //Llenar el Dataset
            DataSet ds = new DataSet();
            a.Fill(ds, "Busqueda");
            //Ligar el datagrid con la fuente de datos
            dgEncomienda.DataSource = ds;
            dgEncomienda.DataMember = "Busqueda";

            conexion.Close();
        }

        private void btBuscar_Click(object sender, EventArgs e)
        {
            this.llenarPasajes(txtCodigo.Text);
            this.llenarEncomiendas(txtCodigo.Text);
        }

        private void txtCodigo_TextChanged(object sender, EventArgs e)
        {
            btBuscar.Enabled = true;
        }
    
     
    }
}
