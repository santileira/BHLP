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
        const string QUERY_BASE = "SELECT ROL_NOMBRE ,ROL_ESTADO FROM [ABSTRACCIONX4].[ROLES]";
        public Form anterior;
        public Listado listado;
        public Form siguiente;
        string rolSeleccionado;
        List<Object> listaFuncionalidades;
        Boolean estadoRol;
        public Boolean llamadoDeModificacion;
        public Boolean primeraConsulta = true;
        
        
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
            if (this.datosCorrectos())
            {
                bool huboCondicion = false;

                string queryselect = QUERY_BASE;

                if (!this.sePusoFiltro())
                {
                    return;
                }

                queryselect = queryselect + " WHERE ";

                if (txtFiltro1.TextLength != 0)
                {
                    string condicion = "ROL_NOMBRE" + " LIKE '%" + txtFiltro1.Text + "%'";
                    this.generarQuery(ref huboCondicion, ref queryselect, condicion);
                }

                if (txtFiltro2.TextLength != 0)
                {
                    string condicion = "ROL_NOMBRE = '" + txtFiltro2.Text + "'";
                    this.generarQuery(ref huboCondicion, ref queryselect, condicion);
                }

                if (!chkEstadoIgnorar.Checked)
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

                if (dg.Rows.Count == 0)
                {
                    MessageBox.Show("No se han encontrado resultados en la consulta", "Informe", MessageBoxButtons.OK);
                }
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

            actualizarColumnasDeEstado(dg);

            conexion.Close();

            primeraConsulta = false;
        }

  
        private void iniciar()
        {
            string queryselect = QUERY_BASE;

            ejecutarConsulta(queryselect);

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

        private void button6_Click(object sender, EventArgs e)
        {
            if (this.llamadoDeModificacion)
            {

                this.cambiarVisibilidades(this.siguiente);
                (siguiente as Modificacion).seSelecciono(rolSeleccionado, estadoRol, listaFuncionalidades.ToArray());
                
            }
            else this.cambiarVisibilidades(this.anterior);
        }

        private void cambiarVisibilidades(Form formularioSiguiente)
        {
            formularioSiguiente.Visible = true;
            this.Visible = false;
        }

        //boton de seleccionar, solo visible cuando se llama desde modificacion
        private void button4_Click(object sender, EventArgs e)
        {
            if (dg.RowCount==0)
            {
                MessageBox.Show("No se ha seleccionado ningún rol", "Selección invalida", MessageBoxButtons.OK);
                return;
            }

            rolSeleccionado = "";
            listaFuncionalidades = new List<object>();
            estadoRol = false;
            ejecutarSeleccion(ref rolSeleccionado,ref estadoRol, listaFuncionalidades);
            if (siguiente != null)
            {
               
                cambiarVisibilidades(this.siguiente);
                (siguiente as Modificacion).seSelecciono(rolSeleccionado, estadoRol, listaFuncionalidades.ToArray());
            } 
       }

        private void ejecutarSeleccion(ref string rolSeleccionado,ref Boolean estadoRol, List<Object> listaFuncionalidades)
        {
            rolSeleccionado = dg.SelectedRows[0].Cells["ROL_NOMBRE"].Value.ToString();
            estadoRol = (Boolean)(dg.SelectedRows[0].Cells["ROL_ESTADO"].Value);


            string query = "SELECT FUNC_DESC FROM [ABSTRACCIONX4].ROLES r JOIN [ABSTRACCIONX4].FUNCIONES_ROLES fr ON (r.ROL_COD = fr.ROL_COD) JOIN [ABSTRACCIONX4].FUNCIONALIDADES f ON (f.FUNC_COD = fr.FUNC_COD) WHERE r.ROL_NOMBRE = '" + rolSeleccionado + "'";
            SqlCommand command = new SqlCommand(query, Program.conexion());
            command.CommandType = System.Data.CommandType.Text;
            command.CommandTimeout = 0;
            
            SqlDataReader dataReader = command.ExecuteReader();

            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    listaFuncionalidades.Add(dataReader.GetValue(0));
                }
            }

            dataReader.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
            //this.cambiarVisibilidades(this.listado);
        }

        private void actualizarColumnasDeEstado(DataGridView dg)
        {
            if (primeraConsulta)
            {
                DataGridViewColumn columnaHabilitada = new DataGridViewTextBoxColumn();
                columnaHabilitada.Name = "HABILITADO";
                columnaHabilitada.HeaderText = "HABILITADO";
                columnaHabilitada.ReadOnly = true;

                dg.Columns.Insert(dg.Columns["ROL_ESTADO"].Index, columnaHabilitada);

            }

            foreach (DataGridViewRow fila in dg.Rows)
            {
                Boolean valor = (Boolean)(fila.Cells["ROL_ESTADO"].Value);
                if (valor)
                {
                    fila.Cells["HABILITADO"].Value = "SI";
                }
                else
                {
                    fila.Cells["HABILITADO"].Value = "NO";
                }
            }

            dg.Columns["ROL_ESTADO"].Visible = false;

        }

        private void dg_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }


        

    }
}
