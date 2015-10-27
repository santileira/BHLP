using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
            cboRoles.Text = "";
        }

        private void cargarComboRoles()
        {
            cboRoles.Items.Clear();

            SqlDataReader reader;
            SqlCommand consultaServicios = new SqlCommand();
            consultaServicios.CommandType = CommandType.Text;
            consultaServicios.CommandText = "SELECT SERV_DESC FROM [ABSTRACCIONX4].SERVICIOS";
            consultaServicios.Connection = Program.conexion();

            reader = consultaServicios.ExecuteReader();

            while (reader.Read())
                this.cboServicio.Items.Add(reader.GetValue(0));

            reader.Close();
        }

    }
}
