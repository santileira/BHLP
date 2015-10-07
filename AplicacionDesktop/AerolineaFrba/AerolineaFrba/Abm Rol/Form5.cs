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
                    string condicion = cboFiltro1.Text + " LIKE '%" + txtFiltro1.Text + "%'";
                    this.generarQuery(ref huboCondicion, ref queryselect, condicion);
                }

                if (txtFiltro2.TextLength != 0)
                {
                    string condicion = cboFiltro2.Text + "= '" + txtFiltro2.Text + "'";
                    this.generarQuery(ref huboCondicion, ref queryselect, condicion);
                }

                if (chkEstadoIgnorar.Checked == false)

                this.ejecutarConsulta(queryselect);
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
            //FILTRO 1
      //      if (txtFiltro1.TextLength == 0)
        



            return (txtFiltro1.TextLength == 0 && txtFiltro2.TextLength == 0 && cboFiltro1.SelectedIndex == -1 && cboFiltro2.SelectedIndex == -1 && cboFiltro3.SelectedIndex == -1);
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

            DataTable t = new DataTable("Busqueda");
            SqlDataAdapter a = new SqlDataAdapter(query, Program.conexion);
            //Llenar el Dataset
            DataSet ds = new DataSet();
            a.Fill(ds, "Busqueda");
            //Ligar el datagrid con la fuente de datos
            dg.DataSource = ds;
            dg.DataMember = "Busqueda";

        }

        private void cboFiltro1_SelectedIndexChanged(object sender, EventArgs e)
        {/*
            this.aplicarVisibilidad(cboFiltro1, txtFiltro1, chkFiltro1)
            if (cboFiltro1.Text == "ROL_NOMBRE")
            {
                txtFiltro1.Visible = true;
                chkFiltro1.Visible = false;
            }
            else if (cboFiltro1.Text == "ROL_ESTADO")
            {
                txtFiltro1.Visible = false;
                chkFiltro1.Visible = true;
            }*/
        }

        private void iniciar()
        {
            string queryselect = "SELECT * FROM [ABSTRACCIONX4].[ROLES]";

            DataTable t = new DataTable("Busqueda");
            SqlDataAdapter a = new SqlDataAdapter(queryselect, Program.conexion);
            //Llenar el Dataset
            DataSet ds = new DataSet();
            a.Fill(ds, "Busqueda");
            //Ligar el datagrid con la fuente de datos
            dg.DataSource = ds;
            dg.DataMember = "Busqueda";

            chkEstadoIgnorar.Checked = true;
            optEstadoAlta.Checked = false;
            optEstadoBaja.Checked = false;

            txtFiltro1.Text = "";
            txtFiltro2.Text = "";
            txtFiltro4.Text = "";

            cboFiltro1.SelectedIndex = -1;
            cboFiltro2.SelectedIndex = -1;
            cboFiltro3.SelectedIndex = -1;
            
        }
    }
}
