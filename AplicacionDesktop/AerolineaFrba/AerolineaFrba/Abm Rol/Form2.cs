using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AerolineaFrba.Abm_Rol
{
    public partial class Alta : Form
    {
        Form formularioSiguiente;
        public Listado listado;

        public Alta()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.iniciar();
        }

        private void iniciar()
        {
            txtNombre.Text = "";
            lstFuncionalidadesActuales.Items.Clear();
            txtNombre.Focus();

            string queryselect = "SELECT FUNC_DESC FROM [ABSTRACCIONX4].[FUNCIONALIDADES]";
            SqlCommand command = new SqlCommand(queryselect, Program.conexion());
            SqlDataAdapter a = new SqlDataAdapter(command);
            DataTable t = new DataTable();
            //Llenar el Dataset
            a.Fill(t);

            lstFuncionalidadesTotales.DisplayMember = "FUNC_DESC";
            lstFuncionalidadesTotales.DataSource = t;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.datosCorrectos())
            {
                MessageBox.Show("El nombre ingresado es correcto. Se procede a dar de alta al nuevo rol", "Alta de roles", MessageBoxButtons.OK);
                //string cadenaComando = "insert into [ABSTRACCIONX4].[ROLES] (ROL_ESTADO, ROL_NOMBRE) values (1, '" + txtNombre.Text + "')";
                //this.ejecutarCommand(cadenaComando);
                List<string> funcionalidades = new List<string>();
                foreach(Object item in lstFuncionalidadesActuales.Items)
                {
                    funcionalidades.Add(item.ToString());
                }


                darDeAltaRol(txtNombre.Text, funcionalidades);
                string nombreRol = txtNombre.Text;



                /*foreach(String funcion in lstFuncionalidadesActuales.Items)
                {
                  darDeAltaFuncionalidad(funcion , nombreRol);
                }*/
            }
        }

        private void darDeAltaFuncionalidad(string funcion , string rol)
        {
            SqlCommand command = new SqlCommand();
            command.Connection = Program.conexion();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "[GD2C2015].[ABSTRACCIONX4].[AltaFuncionalidad]";
            command.CommandTimeout = 0;


            command.Parameters.AddWithValue("@Funcion", funcion);
            command.Parameters.AddWithValue("@Rol", rol);

            command.ExecuteScalar();
            
        }

        private Object darDeAltaRol(string nombre,List<string> funcionalidades)
        {
            SqlCommand command = new SqlCommand();
            command.Connection = Program.conexion();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "[GD2C2015].[ABSTRACCIONX4].[AltaRolV2]";
            command.CommandTimeout = 0;
            

            command.Parameters.AddWithValue("@Nombre", nombre);
            SqlParameter parametroFuncionalidades = new SqlParameter("@Funcionalidades", CreateDataTable(funcionalidades));
            parametroFuncionalidades.SqlDbType = SqlDbType.Structured;
            command.Parameters.Add(parametroFuncionalidades);
           
            return command.ExecuteScalar();
        }

        private static DataTable CreateDataTable(IEnumerable<string> ids)
        {
            DataTable table = new DataTable();
            table.Columns.Add("elemento", typeof(string));
            foreach (string id in ids)
            {
                table.Rows.Add(id);
            }
            return table;
        }


        private Boolean datosCorrectos()
        {
            return !this.validarTextNombre();
        }

        private Boolean esTexto(TextBox txt)
        {
            String textPattern = "[A-Za-z]";
            System.Text.RegularExpressions.Regex regexTexto = new System.Text.RegularExpressions.Regex(textPattern);

            return regexTexto.IsMatch(txt.Text);
        }

        private Boolean esNumero(TextBox txt)
        {
            String numericPattern = "[0-9]";
            System.Text.RegularExpressions.Regex regexNumero = new System.Text.RegularExpressions.Regex(numericPattern);

            return regexNumero.IsMatch(txt.Text);
        }

        private void Alta_Load(object sender, EventArgs e)
        {
            this.iniciar();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.agregarALaLista(lstFuncionalidadesTotales, lstFuncionalidadesActuales);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.eliminarDeLaLista(lstFuncionalidadesActuales);
        }

        private void eliminarDeLaLista(ListBox lista)
        {
            if (lista.SelectedIndex != -1)
                lista.Items.RemoveAt(lista.SelectedIndex);
        }

        private void agregarALaLista(ListBox lista1, ListBox lista2)
        {
            string valor = lista1.Text;
            if (!lista2.Items.Contains(valor))
                lista2.Items.Add(lista1.Text);
        }

        private void ejecutarCommand(string cadenaComando)
        {
            SqlCommand command = new SqlCommand();
            command.CommandType = System.Data.CommandType.Text;
            command.CommandTimeout = 0;
            command.CommandText = cadenaComando;

            try
            {
                command.ExecuteReader().Close();
                MessageBox.Show("Se dio de alta correctamente al rol " + txtNombre.Text, "Alta de roles", MessageBoxButtons.OK);
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                MessageBox.Show("Ocurrio un error al intentar dar de alta. Verifique que el rol ingresado no se encuentre ya cargado", "Error en el Alta del nuevo rol", MessageBoxButtons.OK);
            }
        }

        private Boolean validarTextNombre()
        {
            Boolean huboErrores = false;

            if (txtNombre.TextLength == 0)
            {
                MessageBox.Show("El nombre no puede estar en blanco", "Error en el nombre", MessageBoxButtons.OK);
                huboErrores = true;
            }

            if (!this.esTexto(txtNombre))
            {
                MessageBox.Show("El nombre debe ser una cadena de caracteres", "Error en el nombre", MessageBoxButtons.OK);
                huboErrores = true;
            }

            return huboErrores;
        }

        private void button4_Click(object sender, EventArgs e)
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
