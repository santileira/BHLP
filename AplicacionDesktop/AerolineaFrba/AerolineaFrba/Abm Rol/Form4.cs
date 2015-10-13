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

        SqlCommand command = new SqlCommand();
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
            txtNombre.Text = "";
            txtRolSeleccionado.Text = "";
            lstFuncionalidadesActuales.Items.Clear();

            txtNombre.Enabled = false;
            button6.Enabled = false;
            button3.Enabled = false;
            button2.Enabled = false;
            checkHabilitado.Enabled = false;
            lstFuncionalidadesTotales.Enabled = false;
            modif.Visible = false;
            modifFuncionalidades.Visible = false;
            modifHabilitacion.Visible = false;
            modifNombre.Visible = false;

            string queryselect = "SELECT FUNC_DESC FROM [ABSTRACCIONX4].[FUNCIONALIDADES]";
            command = new SqlCommand(queryselect, Program.conexion());
            SqlDataAdapter a = new SqlDataAdapter(command);
            DataTable t = new DataTable();
            //Llenar el Dataset
            a.Fill(t);

            lstFuncionalidadesTotales.DisplayMember = "FUNC_DESC";
            lstFuncionalidadesTotales.DataSource = t;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private Boolean datosCorrectos()
        {
            Boolean huboErroresEnText = this.validarTextNombre();
            Boolean huboErroresEnList = false;

            if (lstFuncionalidadesActuales.Items.Count > 0 && huboErroresEnText)
            {
                MessageBox.Show("El nombre no puede estar en blanco", "Error en el nombre", MessageBoxButtons.OK);
                huboErroresEnList = true;
            }

            return !(huboErroresEnText || huboErroresEnList);
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

            command.CommandType = System.Data.CommandType.Text;
            command.CommandTimeout = 0;
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
                fueModificado("Funcionalidad");
            }
        }

        private void agregarALaLista(ListBox lista1, ListBox lista2)
        {
            string valor = lista1.Text;
            if (!lista2.Items.Contains(valor))
            {
                lista2.Items.Add(lista1.Text);
                fueModificado("Funcionalidad");
            }
        }

        private void ejecutarCommand(string cadenaComando)
        {
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

        private void button5_Click(object sender, EventArgs e)
        {
            this.cambiarVisibilidades(this.listado);
        }

        public void seSelecciono(string nombreRol,bool habilitado,Object[] funcionalidadesRol)
        {
            txtRolSeleccionado.Text = nombreRol;
            txtNombre.Text = nombreRol;
          
            lstFuncionalidadesActuales.Items.Clear();
            lstFuncionalidadesActuales.Items.AddRange(funcionalidadesRol);

            txtNombre.Enabled = true;
            button6.Enabled = true;
            button3.Enabled = true;
            button2.Enabled = true;
            lstFuncionalidadesTotales.Enabled = true;

            checkHabilitado.Enabled = !habilitado;
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
            if (txtNombre.Text != txtRolSeleccionado.Text)
            {
                fueModificado("Nombre");
            }
        }

        public void fueModificado(string elemento)
        {
            switch (elemento)
            {
                case "Nombre":
                    modifNombre.Visible = true;
                    break;
                case "Funcionalidad":
                    modifFuncionalidades.Visible = true;
                    break;
                case "Habilitacion":
                    modifHabilitacion.Visible = true;
                    break;
            }
            modif.Visible = true;
        }

        private void checkHabilitado_CheckedChanged(object sender, EventArgs e)
        {
            fueModificado("Habilitacion");
        }

        public void ocultarNorificacionModificaciones()
        {
            modif.Visible = false;
            modifFuncionalidades.Visible = false;
            modifHabilitacion.Visible = false;
            modifNombre.Visible = false;
        }
        
    }
}
