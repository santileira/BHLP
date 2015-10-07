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
    public partial class Listado : Form
    {
        
        public Listado()
        {
            InitializeComponent();
        }

        private void Listado_Load(object sender, EventArgs e)
        {
         //   string queryselect = "SELECT * FROM [ABSTRACCIONX4].[ROLES]";
            string queryselect = "SELECT top 10 Ruta_Ciudad_Origen FROM gd_esquema.Maestra";
            DataTable t = new DataTable("Busqueda");
            SqlDataAdapter a = new SqlDataAdapter(queryselect, Program.conexion);
            //Llenar el Dataset
            DataSet ds = new DataSet();
            a.Fill(ds, "Busqueda");
            //Ligar el datagrid con la fuente de datos
            dg.DataSource = ds;
            dg.DataMember = "Busqueda";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //string queryselect = "SELECT * FROM [ABSTRACCIONX4].[ROLES]";
            string queryselect = "SELECT top 10 Ruta_Ciudad_Origen FROM gd_esquema.Maestra";

            if(this.sePusoFiltro())
               queryselect = queryselect + " WHERE ";
            /*
             * SE CONCATENAN LOS CRITERIOS 
                        if (txtFiltro1.TextLength != 0)
                            queryselect = queryselect + dg.NOMBRE_COLUMNA + " LIKE " + txtFiltro1.Text;
            
                        if(txtFiltro2.TextLength != 0)
                            queryselect = queryselect + "";
             * 
             *        
            */

        }

        private Boolean sePusoFiltro()
        {
            if (txtFiltro1.TextLength != 0 || txtFiltro2.TextLength != 0 || cboFiltro3.SelectedIndex != -1)
                return true;
            
            return false;
            
        }


    }
}
