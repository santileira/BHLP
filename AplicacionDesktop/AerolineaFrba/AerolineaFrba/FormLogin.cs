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

            MessageBox.Show("El campo  no puede estar vacio", "Error en los datos de entrada", MessageBoxButtons.OK);

            reader = consultaRoles.ExecuteReader();

            while (reader.Read())
                cboRoles.Items.Add(reader.GetValue(0));

            reader.Close();
        }

    }
}
