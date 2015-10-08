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
        private const string QUERY_BASE = "SELECT ROL_NOMBRE,ROL_ESTADO FROM [ABSTRACCIONX4].[ROLES]";
        private string ultimaQuery;
           
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
            if (this.datosCorrectos())
            {
                bool huboCondicion = false;

                string queryselect = QUERY_BASE;
           
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
                    if (optEstadoAlta.Checked)
                    {
                       condicion = "ROL_ESTADO = 1";
                    }
                    else
                    {
                        condicion = "ROL_ESTADO = 0";
                    }
                    this.generarQuery(ref huboCondicion, ref queryselect, condicion);
                }

                this.ejecutarConsulta(queryselect);
            }

        }

        private Boolean sePusoFiltro()
        {
            return (txtFiltro1.TextLength != 0 || txtFiltro2.TextLength != 0 || cboFiltro3.SelectedIndex != -1 || !chkEstadoIgnorar.Checked);          
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.iniciar();
        }

        private Boolean datosCorrectos()
        {
            Boolean huboErrores = false;

            if (!this.esTexto(txtFiltro1))
            {
                MessageBox.Show("El filtro que contenga la palabra debe ser una cadena de caracteres", "Error en el nombre", MessageBoxButtons.OK);
                huboErrores = true;
            }

            if (!this.esTexto(txtFiltro2))
            {
                MessageBox.Show("El filtro por igualdad de palabra debe ser una cadena de caracteres", "Error en el nombre", MessageBoxButtons.OK);
                huboErrores = true;
            }

            return !huboErrores;
        
        }

        private Boolean esTexto(TextBox txt)
        {
            if (txt.Text.Length == 0)
            {
                return true;
            }

            String textPattern = "[A-Za-z]";
            System.Text.RegularExpressions.Regex regexTexto = new System.Text.RegularExpressions.Regex(textPattern);

            return regexTexto.IsMatch(txt.Text);
        }

        private void generarQuery(ref Boolean huboCondicion, ref string laQuery, string condicion)
        {
           if (huboCondicion)
               laQuery += " AND " + condicion;
           else
           {
               laQuery += condicion;
               huboCondicion = true;
           }
           Console.Write(laQuery);
        }

        private void ejecutarConsulta(string query)
        {
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
            ejecutarConsulta(QUERY_BASE);
            ultimaQuery = QUERY_BASE;
       
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
                        string cadenaComando = "UPDATE [ABSTRACCIONX4].[ROLES] SET ROL_ESTADO = 0 WHERE ROL_NOMBRE = '" + darValorDadoIndex(e.RowIndex);
                        ejecutarCommand(cadenaComando);
                        ejecutarConsulta(ultimaQuery);
                    }
                }
                else
                    if (e.ColumnIndex == 1)
                    {
                        DialogResult resultado = mostrarMensaje("física");
                        if (apretoSi(resultado))
                        {
                            string cadenaComando = "DELETE FROM [ABSTRACCIONX4].[ROLES] WHERE ROL_NOMBRE = '" + darValorDadoIndex(e.RowIndex);
                            ejecutarCommand(cadenaComando);
                            ejecutarConsulta(ultimaQuery);
                        }
                    }
            }
        }

       private void ejecutarCommand(string cadenaComando)
       {
            SqlCommand command = new SqlCommand();
            command.Connection = Program.conexion();
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = cadenaComando;
            command.CommandTimeout = 0;
            command.ExecuteReader().Close();
        }

        private Boolean apretoSi(DialogResult resultado){
            return resultado == System.Windows.Forms.DialogResult.Yes;
        }

        private DialogResult mostrarMensaje(string tipoDeBaja)
        {
            return MessageBox.Show("¿Está seguro que quiere dar de baja " + tipoDeBaja + " este registro?", "Advertencia", MessageBoxButtons.YesNo);
        }

        private string darValorDadoIndex(int index)
        {
            return dg.Rows[index].Cells["ROL_NOMBRE"].Value.ToString() + "'";
        }
            

    }



     
}
