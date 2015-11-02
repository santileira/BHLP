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

                darDeAltaRol();
                
            }
        }


        private Object darDeAltaRol()
        {
            SQLManager sqlManager = new SQLManager();
            return sqlManager.generarSP("AltaRol").
                   agregarStringSP("@Nombre", txtNombre.Text).
                   agregarListaSP("@Funcionalidades", funcionalidadesActuales()).
                   ejecutarSP();
        }

        private List<Object> funcionalidadesActuales()
        {
            List<Object> funcionalidades = new List<Object>();
            foreach (Object func in lstFuncionalidadesActuales.Items)
            {
                funcionalidades.Add(func);
            }
            return funcionalidades;
        }


        private Boolean datosCorrectos()
        {
            Boolean huboError = Validacion.esVacio(txtNombre, "nombre de rol", true);
            huboError = !Validacion.esSoloTexto(txtNombre, "nombre de rol", true) || huboError;

            return !huboError;
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

        /*private Boolean validarTextNombre()
        {
            Boolean huboErrores = false;

            if (Validacion.esVacio(txtNombre , "nombre de Rol" , true))
            {
                MessageBox.Show("El nombre no puede estar en blanco", "Error en el nombre", MessageBoxButtons.OK);
                huboErrores = true;
            }

            if (!Validacion.esTexto(txtNombre))
            {
                MessageBox.Show("El nombre debe ser una cadena de caracteres", "Error en el nombre", MessageBoxButtons.OK);
                huboErrores = true;
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
    }
}
