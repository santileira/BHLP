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
    public partial class Modificacion : Form
    {

        Form formularioSiguiente;
        public Listado listado;


        public Modificacion()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.iniciar();
        }

        private void iniciar()
        {
            txtRolSeleccionado.Text = "";
            lstFuncionalidadesActuales.Items.Clear();
            txtNombre.Enabled = false;
            txtNombre.Text = "";
            button6.Enabled = false;
            button3.Enabled = false;
            button2.Enabled = false;
            checkHabilitado.Enabled = false;
            lstFuncionalidadesTotales.Enabled = false;

            button1.Enabled = false;

            string queryselect = "SELECT FUNC_DESC FROM [ABSTRACCIONX4].[FUNCIONALIDADES]";
            SqlCommand command = new SqlCommand(queryselect, Program.conexion());
            command.CommandType = System.Data.CommandType.Text;
            command.CommandTimeout = 0;
            SqlDataAdapter a = new SqlDataAdapter(command);
            DataTable t = new DataTable();
            //Llenar el Dataset
            a.Fill(t);

            lstFuncionalidadesTotales.DisplayMember = "FUNC_DESC";
            lstFuncionalidadesTotales.DataSource = t;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (datosCorrectos())
            {
                DialogResult resultado = MessageBox.Show("Se procede a modificar el rol seleccionado", "Informe", MessageBoxButtons.YesNo);
                if (apretoSi(resultado))
                {
                    string nombreNuevo = txtNombre.Text;
                    string nombreOriginal = txtRolSeleccionado.Text;
                    modificarRol(nombreNuevo, nombreOriginal);

                    darDeBajaFuncionalidades(nombreNuevo);

                    foreach (String funcion in lstFuncionalidadesActuales.Items)
                    {
                        darDeAltaFuncionalidad(funcion, nombreNuevo);
                    }

                    (listado as Listado).iniciar();
                    this.cambiarVisibilidades(this.listado);

                }

            }
        }

        private Object darDeBajaFuncionalidades(string nombreNuevo)
        {
            SqlCommand command = new SqlCommand();
            command.Connection = Program.conexion();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "[GD2C2015].[ABSTRACCIONX4].[BajaFuncionalidades]";
            command.CommandTimeout = 0;

            command.Parameters.AddWithValue("@NombreRol", nombreNuevo);

            return command.ExecuteScalar();

        }

        private Object darDeAltaFuncionalidad(string funcion, string rol)
        {
            SqlCommand command = new SqlCommand();
            command.Connection = Program.conexion();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "[GD2C2015].[ABSTRACCIONX4].[AltaFuncionalidad]";
            command.CommandTimeout = 0;


            command.Parameters.AddWithValue("@Funcion", funcion);
            command.Parameters.AddWithValue("@Rol", rol);

            return command.ExecuteScalar();

        }


        private Object modificarRol(string nombreNuevo, string nombreOriginal)
        {
            SqlCommand command = new SqlCommand();
            command.Connection = Program.conexion();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "[GD2C2015].[ABSTRACCIONX4].[ModificarRol]";
            command.CommandTimeout = 0;

            command.Parameters.AddWithValue("@NombreNuevo", nombreNuevo);
            command.Parameters.AddWithValue("@NombreOriginal", nombreOriginal);

            return command.ExecuteScalar();
        }

        private bool apretoSi(DialogResult resultado)
        {
            return resultado == System.Windows.Forms.DialogResult.Yes;
        }

        private Boolean datosCorrectos()
        {
            /*Boolean huboErroresEnText = this.validarTextNombre();
      
            return !(huboErroresEnText);*/
            return Validacion.textNombre(txtNombre.Text);
        }

        /*private Boolean esTexto(TextBox txt)
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
            
        }*/

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
            {
                lista.Items.RemoveAt(lista.SelectedIndex);

            }
        }

        private void agregarALaLista(ListBox lista1, ListBox lista2)
        {
            string valor = lista1.Text;
            if (!lista2.Items.Contains(valor))
            {
                lista2.Items.Add(lista1.Text);

            }
        }

        private void ejecutarCommand(string cadenaComando)
        {
            SqlCommand command = new SqlCommand();
            command.Connection = Program.conexion();
            command.CommandType = System.Data.CommandType.StoredProcedure;
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

        /*private Boolean validarTextNombre()
        {
            Boolean huboErrores = false;
            
            if (txtNombre.TextLength == 0)
            {
                MessageBox.Show("El nombre no puede estar en blanco", "Error en el nombre", MessageBoxButtons.OK);
                huboErrores = true;
             
            }
            else
            {           
                if (txtNombre.TextLength > 60)
                {
                    MessageBox.Show("El nombre debe tener a lo sumo 60 caracteres", "Error en el nombre", MessageBoxButtons.OK);
                    huboErrores = true;
                }

                if (!this.esTexto(txtNombre))
                {
                    MessageBox.Show("El nombre debe ser una cadena de caracteres", "Error en el nombre", MessageBoxButtons.OK);
                    huboErrores = true;
                }
            }
            
            return huboErrores;
        }*/

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

        private void button5_Click(object sender, EventArgs e)
        {
            this.listado.llamadoDeModificacion = true;
            this.cambiarVisibilidades(this.listado);
        }

        public void seSelecciono(string nombreRol, bool habilitado, Object[] funcionalidadesRol)
        {

            txtRolSeleccionado.Text = nombreRol;
            txtRolSeleccionado.Enabled = true;
            txtNombre.Enabled = true;
            txtNombre.Text = nombreRol;

            lstFuncionalidadesActuales.Items.Clear();
            lstFuncionalidadesActuales.Items.AddRange(funcionalidadesRol);


            button6.Enabled = true;
            button3.Enabled = true;
            button2.Enabled = true;
            lstFuncionalidadesTotales.Enabled = true;

            checkHabilitado.Enabled = !habilitado;
            button1.Enabled = true;
        }
    }
}

        

      
    

