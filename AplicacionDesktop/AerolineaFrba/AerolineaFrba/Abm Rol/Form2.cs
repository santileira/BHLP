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

namespace AerolineaFrba.Abm_Rol
{
    public partial class Alta : Form
    {
        public Alta()
        {
            InitializeComponent();
        }

        private void Alta_Load(object sender, EventArgs e)
        {
            string queryselect = "SELECT top 10 Ruta_Ciudad_Origen FROM gd_esquema.Maestra";
            SqlCommand command = new SqlCommand(queryselect, Program.conexion);
            
            SqlDataAdapter a = new SqlDataAdapter(command);

            DataTable t = new DataTable();

            //Llenar el Dataset

            a.Fill(t);
 
            lstFuncionalidadesTotales.DisplayMember = "Ruta_Ciudad_Origen";
            lstFuncionalidadesTotales.DataSource = t;

            //dg.DataSource = ds;
            //dg.DataMember = "Busqueda";
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string valor = lstFuncionalidadesTotales.Text;
            if (!lstFuncionalidadesNuevas.Items.Contains(valor))
                lstFuncionalidadesNuevas.Items.Add(lstFuncionalidadesTotales.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(lstFuncionalidadesNuevas.SelectedIndex != -1)
                lstFuncionalidadesNuevas.Items.RemoveAt(lstFuncionalidadesNuevas.SelectedIndex);
        }
    }
}
