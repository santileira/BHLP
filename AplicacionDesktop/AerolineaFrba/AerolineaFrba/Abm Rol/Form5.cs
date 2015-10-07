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
            this.iniciar();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!this.datosEntradaErroneos())
            {
                bool huboCondicion = false;

                string queryselect = "SELECT * FROM [ABSTRACCIONX4].[ROLES]";
           
                if (this.sePusoFiltro())
                    queryselect = queryselect + " WHERE ";

                if (txtFiltro1.TextLength != 0)
                {
                    string condicion = "ROL_NOMBRE" + " LIKE '%" + txtFiltro1.Text + "%'";
                    this.generarQuery(ref huboCondicion, ref queryselect, condicion);
                }

                if (txtFiltro2.TextLength != 0)
                {
                    string condicion = "ROL_NOMBRE" + "= '" + txtFiltro2.Text + "'";
                    this.generarQuery(ref huboCondicion, ref queryselect, condicion);
                }

                if (chkEstadoIgnorar.Checked == false)
                {
                    string condicion;
                    if (optEstadoAlta.Enabled)
                    {
                       condicion = "ROL_ESTADO" + " = 1";
                    }
                    else
                    {
                        condicion = "ROL_ESTADO" + " = 0";
                    }
                this.generarQuery(ref huboCondicion, ref queryselect, condicion);
                }

                this.ejecutarConsulta(queryselect);

                if (dg.Rows.Count == 1)
                {
                    MessageBox.Show("No se han encontrado resultados en la consulta", "Informe", MessageBoxButtons.OK);
                }
            }
            else
                MessageBox.Show("Los datos de entrada son incorrectos", "Error en la consulta", MessageBoxButtons.OK);

        }

        private Boolean sePusoFiltro()
        {
            return (txtFiltro1.TextLength != 0 || txtFiltro2.TextLength != 0 || cboFiltro3.SelectedIndex != -1);          
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.iniciar();
        }

        private Boolean datosEntradaErroneos()
        {
       
            return (txtFiltro1.TextLength == 0 && txtFiltro2.TextLength == 0 && cboFiltro3.SelectedIndex == -1);
        
        }

        private void generarQuery(ref Boolean huboCondicion, ref string queryselect, string condicion)
        {
           if (huboCondicion)
               queryselect += " AND " + condicion;
           else
           {
               queryselect += condicion;
               huboCondicion = true;
           }
        }

        private void ejecutarConsulta(string query)
        {
            SqlCommand command = new SqlCommand();
            SqlConnection conexion = Program.conexion();

            DataTable t = new DataTable("Busqueda");
            SqlDataAdapter a = new SqlDataAdapter(query, conexion);
            //Llenar el Dataset
            DataSet ds = new DataSet();
            a.Fill(ds, "Busqueda");
            //Ligar el datagrid con la fuente de datos
            dg.DataSource = ds;
            dg.DataMember = "Busqueda";

            conexion.Close();
        }

  

        private void iniciar()
        {
            string queryselect = "SELECT * FROM [ABSTRACCIONX4].[ROLES]";

            SqlConnection conexion = Program.conexion();

            DataTable t = new DataTable("Busqueda");
            SqlDataAdapter a = new SqlDataAdapter(queryselect, conexion);
            //Llenar el Dataset
            DataSet ds = new DataSet();
            a.Fill(ds, "Busqueda");
            //Ligar el datagrid con la fuente de datos
            dg.DataSource = ds;
            dg.DataMember = "Busqueda";

            conexion.Close();

            chkEstadoIgnorar.Checked = true;
            optEstadoAlta.Checked = true;
            optEstadoBaja.Checked = false;

            txtFiltro1.Text = "";
            txtFiltro2.Text = "";
            txtFiltro4.Text = "";

            
            cboFiltro3.SelectedIndex = -1;
            
        }

        private void chkEstadoIgnorar_CheckedChanged(object sender, EventArgs e)
        {
           optEstadoAlta.Enabled = !chkEstadoIgnorar.Checked;
           optEstadoBaja.Enabled = !chkEstadoIgnorar.Checked;
           
            
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
