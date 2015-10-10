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

namespace AerolineaFrba.Abm_Aeronave
{
    public partial class Alta : Form
    {
        public Alta alta;

        public Listado listado;

        Form formularioSiguiente;
        

        public Alta()
        {
            InitializeComponent();

            //
            // Carga del contenido de combos
            //

            SqlDataReader variable;
            SqlCommand consultaColumnas = new SqlCommand();
            consultaColumnas.CommandType = CommandType.Text;
            consultaColumnas.CommandText = "SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'AERONAVES'";
            consultaColumnas.Connection = Program.conexion();

            variable = consultaColumnas.ExecuteReader();

            while (variable.Read())
                this.cboCampo.Items.Add(variable.GetValue(0));
            
        }

        private void cboCampo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboCampo.SelectedIndex != -1)
                button1.Enabled = true;
            else
                button1.Enabled = false;
        }

        private void Alta_Load(object sender, EventArgs e)
        {
            this.inicio();
        }

        public void setFiltroSelector(string valor)
        {
            txtCampo.Text = valor;
        }

        public string getCampoSelector()
        {
            return cboCampo.Text;
        }

        private void inicio()
        {
            txtCampo.Text = "";
            txtFabricante.Text = "";
            txtMatricula.Text = "";
            txtModelo.Text = "";
            txtModelo.Focus();
            txtServicio.Text = "";
            txtButacas.Text = "";
            txtKilos.Text = "";
            txtBajaFS.Text = "";
            txtBajaVU.Text = "";
            txtBD.Text = "";
            txtFS.Text = "";
            txtRS.Text = "";
                        
            button1.Enabled = false;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.inicio();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.cambiarVisibilidades(this.listado);
        }

        private void cambiarVisibilidades(Form formularioSiguiente)
        {
            formularioSiguiente.Visible = true;
            this.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
        }

        private void button6_Click(object sender, EventArgs e)
        {
            formularioSiguiente = new Principal();
            this.cambiarVisibilidades(formularioSiguiente);
        }

    }
}
