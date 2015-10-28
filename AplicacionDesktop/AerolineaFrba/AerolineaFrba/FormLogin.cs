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

namespace AerolineaFrba
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        private void FormPrincipalAdministrador_Load(object sender, EventArgs e)
        {
            iniciar();
        }

        private void iniciar()
        {
            vaciarTextos();

            radioAdministrador.Checked = true;
            radioInvitado.Checked = false;

            gbAdministrador.Enabled = true;
            gbInvitado.Enabled = false;

            cargarComboRoles();
        }

        private void radioAdministrador_CheckedChanged(object sender, EventArgs e)
        {
            Boolean tildadoAdministrador = radioAdministrador.Checked;
            
            gbAdministrador.Enabled = tildadoAdministrador;
            gbInvitado.Enabled = !tildadoAdministrador;

            vaciarTextos();
        }

        private void vaciarTextos()
        {
            txtPassword.Text = "";
            txtUsuario.Text = "";
            cboRoles.SelectedIndex = -1;
        }

        private void cargarComboRoles()
        {
            cboRoles.Items.Clear();

            SqlDataReader reader;
            SqlCommand consultaRoles = new SqlCommand();
            consultaRoles.CommandType = CommandType.Text;
            consultaRoles.CommandText = "SELECT ROL_NOMBRE FROM [ABSTRACCIONX4].ROLES_USUARIOS RU JOIN [ABSTRACCIONX4].ROLES R ON (RU.ROL_COD = R.ROL_COD) WHERE USERNAME = 'INVITADO'";
            consultaRoles.Connection = Program.conexion();

            reader = consultaRoles.ExecuteReader();

            while (reader.Read())
                cboRoles.Items.Add(reader.GetValue(0));

            reader.Close();
        }

        private void botonIngresar_Click(object sender, EventArgs e)
        {
            if (radioAdministrador.Checked)
            {
                ingresarComoAdministrador();
            }
            else
            {
                ingresarComoInvitado();
            }
        }

        

        private void cambiarVisibilidades(Form formularioSiguiente)
        {
            formularioSiguiente.Visible = true;
            this.Visible = false;
        }

        private void ingresarComoAdministrador()
        {
            Boolean huboErrores = false;

            if (validarTipos())
                huboErrores = true;

            if (validarLongitudes())
                huboErrores = true;
            
            
            if (!huboErrores)
            {
                cambiarVisibilidades(new Principal("ADMINISTRADOR"));
            } 
            
        }

        

        private void ingresarComoInvitado()
        {
            if (cboRoles.SelectedIndex == -1)
            {
                MessageBox.Show("Debe elegir algún rol para poder ingresar como invitado", "Error", MessageBoxButtons.OK);
                return;
            }

            cambiarVisibilidades(new Principal(cboRoles.SelectedItem.ToString()));
        }

        private bool validarLongitudes()
        {
            Boolean huboErrores = false;
            
            if (txtUsuario.TextLength == 0)
            {
                MessageBox.Show("Debe completar el campo usuario", "Error", MessageBoxButtons.OK);
                huboErrores = true;
            }
            if (txtPassword.TextLength == 0)
            {
                MessageBox.Show("Debe completar el campo contraseña", "Error", MessageBoxButtons.OK);
                huboErrores = true;
            }

            return huboErrores;
        }

        private bool validarTipos()
        {
            if (!Validacion.esTexto(txtUsuario) && txtUsuario.TextLength > 0)
            {
                MessageBox.Show("El nombre de usuario debe ser un texto", "Error", MessageBoxButtons.OK);
                return true;
            }
            return false;
        }

    }
}
