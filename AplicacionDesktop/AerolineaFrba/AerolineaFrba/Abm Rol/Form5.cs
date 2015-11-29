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
        public const string QUERY_BASE = "SELECT ROL_NOMBRE 'Nombre' ,ROL_ESTADO 'Estado' FROM [ABSTRACCIONX4].[ROLES]";
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

        // Realiza la consulta a la BD en base a los filtros
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

                if (/*txtFiltro1.TextLength != 0*/!Validacion.esVacio(txtFiltro1 , "No importa" , false))
                {
                    string condicion = "ROL_NOMBRE" + " LIKE '%" + txtFiltro1.Text + "%'";
                    this.generarQuery(ref huboCondicion, ref queryselect, condicion);
                }

                if (/*txtFiltro2.TextLength != 0*/!Validacion.esVacio(txtFiltro2 , "No importa" , false))
                {
                    string condicion = "ROL_NOMBRE = '" + txtFiltro2.Text + "'";
                    this.generarQuery(ref huboCondicion, ref queryselect, condicion);
                }

                if (/*!chkEstadoIgnorar.Checked*/!Validacion.estaCheckeadoCheck(chkEstadoIgnorar))
                {
                    string condicion;
                    if (/*optEstadoAlta.Checked*/Validacion.estaCheckeadoOpt(optEstadoAlta))
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
            return (!Validacion.esVacio(txtFiltro1) || !Validacion.esVacio(txtFiltro2) || !Validacion.estaCheckeadoCheck(chkEstadoIgnorar));          
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.iniciar();
        }

        private Boolean datosCorrectos()
        {
            Boolean huboErrores = false;

            if (!Validacion.esTexto(txtFiltro1))
            {
                MessageBox.Show("El filtro que contenga la palabra debe ser una cadena de caracteres", "Error en el nombre", MessageBoxButtons.OK);
                huboErrores = true;
            }

            if (!Validacion.esTexto(txtFiltro2))
            {
                MessageBox.Show("El filtro por igualdad de palabra debe ser una cadena de caracteres", "Error en el nombre", MessageBoxButtons.OK);
                huboErrores = true;
            }

            return !huboErrores;

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

        public void ejecutarConsulta(string query)
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

            actualizarColumnasDeEstado(dg);

            conexion.Close();

            primeraConsulta = false;
        }

  
        public void iniciar()
        {
            string queryselect = QUERY_BASE;

            ejecutarConsulta(queryselect);

            chkEstadoIgnorar.Checked = true;
            optEstadoAlta.Checked = true;
            optEstadoBaja.Checked = false;

            txtFiltro1.Text = "";
            txtFiltro2.Text = ""; 
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

               
                (anterior as Modificacion).seSelecciono(rolSeleccionado, estadoRol, listaFuncionalidades.ToArray());
                cambiarVisibilidades(this.anterior);
                
            this.Close();
       }

        private void ejecutarSeleccion(ref string rolSeleccionado,ref Boolean estadoRol, List<Object> listaFuncionalidades)
        {
            rolSeleccionado = dg.SelectedRows[0].Cells["Nombre"].Value.ToString();
            estadoRol = (Boolean)(dg.SelectedRows[0].Cells["Estado"].Value);


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
        }

        // Muestra la columna de habilitado con si o no
        private void actualizarColumnasDeEstado(DataGridView dg)
        {
            if (primeraConsulta)
            {
                DataGridViewColumn columnaHabilitada = new DataGridViewTextBoxColumn();
                columnaHabilitada.Name = "Habilitado";
                columnaHabilitada.HeaderText = "Habilitado";
                columnaHabilitada.ReadOnly = true;

                dg.Columns.Insert(dg.Columns["Estado"].Index, columnaHabilitada);

            }

            foreach (DataGridViewRow fila in dg.Rows)
            {
                Boolean valor = (Boolean)(fila.Cells["Estado"].Value);
                if (valor)
                {
                    fila.Cells["Habilitado"].Value = "SI";
                }
                else
                {
                    fila.Cells["Habilitado"].Value = "NO";
                }
            }

            dg.Columns["Estado"].Visible = false;

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.cambiarVisibilidades(anterior);
        }
    }
}
