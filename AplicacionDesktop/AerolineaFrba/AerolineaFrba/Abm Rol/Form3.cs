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
        private string query;
        private string ultimaQuery;
        Form formularioSiguiente;
        public Listado listado;

        public Baja()
        {
            InitializeComponent();
        }

        private void Listado_Load(object sender, EventArgs e)
        {
            this.iniciar();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            if (this.datosCorrectos())
            {
                bool huboCondicion = false;

                string querySelect = query;
               
                if (this.sePusoFiltro())
                    querySelect = querySelect + " AND ";
                else
                    MessageBox.Show("No se ha agregado ningún filtro. Agregue para poder realizar la búsqueda", "Informe", MessageBoxButtons.OK);

                if (/*txtFiltro1.TextLength != 0*/!Validacion.esVacio(txtFiltro1 , "No importa" , false))
                {
                  
                    string condicion = "ROL_NOMBRE" + " LIKE '%" + txtFiltro1.Text + "%'";
                    this.generarQuery(ref huboCondicion, ref querySelect, condicion);
                    
                }

                if (/*txtFiltro2.TextLength != 0*/!Validacion.esVacio(txtFiltro2 , "No importa" , false))
                {
                    string condicion = "ROL_NOMBRE" + "= '" + txtFiltro2.Text + "'";
                    this.generarQuery(ref huboCondicion, ref querySelect, condicion);
                }

               
                this.ejecutarQuery(querySelect);
                ultimaQuery = querySelect;
                txtFiltro1.Text = "";
                txtFiltro2.Text = "";
                txtFiltro4.Text = "";

                cboFiltro3.SelectedIndex = -1;
            }
            
        }

        private Boolean sePusoFiltro()
        {
            return (!Validacion.esVacio(txtFiltro1) || !Validacion.esVacio(txtFiltro2) || !Validacion.estaSeleccionado(cboFiltro3));          
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.iniciar();
        }

        private Boolean datosCorrectos()
        {
            //Boolean huboErrores = false;
            return Validacion.filtrosContengaEIgualdad(txtFiltro1, txtFiltro2);
            /*if (!this.esTexto(txtFiltro1))
            {
                MessageBox.Show("El filtro que contenga la palabra debe ser una cadena de caracteres", "Error en el nombre", MessageBoxButtons.OK);
                huboErrores = true;
            }

            if (!this.esTexto(txtFiltro2))
            {
                MessageBox.Show("El filtro por igualdad de palabra debe ser una cadena de caracteres", "Error en el nombre", MessageBoxButtons.OK);
                huboErrores = true;
            }

            return !huboErrores;*/
        
        }

        /*private Boolean esTexto(TextBox txt)
        {
            if (txt.Text.Length == 0)
            {
                return true;
            }

            String textPattern = "[A-Za-z]";
            System.Text.RegularExpressions.Regex regexTexto = new System.Text.RegularExpressions.Regex(textPattern);

            return regexTexto.IsMatch(txt.Text);
        }*/

        private void generarQuery(ref Boolean huboCondicion, ref string laQuery, string condicion)
        {
           if (huboCondicion)
               laQuery += " AND " + condicion;
           else
           {
               laQuery += condicion;
               huboCondicion = true;
           }
          
        }

        private void ejecutarQuery(string query)
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
            this.generarQueryInicial();
            this.ejecutarQuery(query);
            ultimaQuery = query;
           
           
            txtFiltro1.Text = "";
            txtFiltro2.Text = "";
            txtFiltro4.Text = "";
            
            cboFiltro3.SelectedIndex = -1;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.cambiarVisibilidades(this.listado);
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
                        darDeBajaRol(darValorDadoIndex(e.RowIndex));
                        ejecutarQuery(query);
                    }
                }
                // BAJA FISICA
               /* else
                    if (e.ColumnIndex == 1)
                    {
                        DialogResult resultado = mostrarMensaje("física");
                        if (apretoSi(resultado))
                        {
                            string cadenaComando = "DELETE FROM [ABSTRACCIONX4].[ROLES] WHERE ROL_NOMBRE = '" + darValorDadoIndex(e.RowIndex);
                            ejecutarCommand(cadenaComando);
                            ejecutarQuery(ultimaQuery);
                        }
                    }*/
            }
        }

        private Object darDeBajaRol(string nombre)
        {
            SqlCommand command = new SqlCommand();
            command.Connection = Program.conexion();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "[GD2C2015].[ABSTRACCIONX4].[BajaRol]";
            command.CommandTimeout = 0;

            command.Parameters.AddWithValue("@Nombre", nombre);
           
            return command.ExecuteScalar();
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
            return dg.Rows[index].Cells["ROL_NOMBRE"].Value.ToString();
        }

        private void generarQueryInicial()
        {
            query = "SELECT ROL_NOMBRE FROM [ABSTRACCIONX4].[ROLES] WHERE ROL_ESTADO = '1'";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.iniciar();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            formularioSiguiente = new Principal();
            cambiarVisibilidades(formularioSiguiente);
        }

        private void cambiarVisibilidades(Form formularioSiguiente)
        {
            formularioSiguiente.Visible = true;
            this.Visible = false;
        }

            
        
    }


     
}
