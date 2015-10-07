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
    public partial class Baja : Form
    {
       /* public Baja()
        {
            InitializeComponent();
        }

        private void Baja_Load(object sender, EventArgs e)
        { }*/

           
        public Baja()
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

            DataTable t = new DataTable("Busqueda");
            SqlDataAdapter a = new SqlDataAdapter(query, Program.conexion);
            //Llenar el Dataset
            DataSet ds = new DataSet();
            a.Fill(ds, "Busqueda");
            //Ligar el datagrid con la fuente de datos
            dg.DataSource = ds;
            dg.DataMember = "Busqueda";

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

        private void dg_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;
            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
            {
                if (e.ColumnIndex == 0)
                {
                    DialogResult resultado = mostrarMensaje("lógica");
                    if (apretoSi(resultado))
                    {
                        string cadenaComando = "UPDATE [ABSTRACCIONX4].[ROLES] SET ROL_ESTADO = 0";
                        ejecutarCommand(cadenaComando);
                    }
                }
                else
                    if (e.ColumnIndex == 1)
                    {
                        DialogResult resultado = mostrarMensaje("física");
                        if (apretoSi(resultado))
                        {
                            string cadenaComando = "DELETE FROM [ABSTRACCIONX4].[ROLES]";
                            ejecutarCommand(cadenaComando);
                        }
                    }
            }
        }

       private void ejecutarCommand(string cadenaComando)
       {
            SqlCommand command = new SqlCommand();
            command.Connection = Program.conexion;
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = cadenaComando;
            command.CommandTimeout = 0;
            command.ExecuteReader();
        }

        private Boolean apretoSi(DialogResult resultado){
            return resultado == System.Windows.Forms.DialogResult.Yes;
        }

        private DialogResult mostrarMensaje(string tipoDeBaja)
        {
            return MessageBox.Show("¿Está seguro que quiere dar de baja " + tipoDeBaja + " este registro?", "Advertencia", MessageBoxButtons.YesNo);
        }


    }

     
}
