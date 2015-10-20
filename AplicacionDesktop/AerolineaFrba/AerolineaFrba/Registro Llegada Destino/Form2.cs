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

namespace AerolineaFrba.Registro_Llegada_Destino
{
    public partial class Form2 : Form
    {
        int viaje_cod;

        public Form2(DataGridViewRow registro, int viajeCod)
        {
            InitializeComponent();
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "dd/MM/yyyy - HH:mm:ss";

            viaje_cod = viajeCod;

            SqlDataReader reader;
            SqlCommand consultaServicios = new SqlCommand();
            consultaServicios.CommandType = CommandType.Text;
            consultaServicios.CommandText = "SELECT s.SERV_DESC FROM [ABSTRACCIONX4].[AERONAVES] a JOIN [ABSTRACCIONX4].[SERVICIOS] s ON a.SERV_COD = s.SERV_COD WHERE a.AERO_MATRI = '" + registro.Cells["AERO_MATRI"].Value.ToString() + "'";
            consultaServicios.Connection = Program.conexion();

            reader = consultaServicios.ExecuteReader();
            reader.Read();

            txtServicio.Text = reader.GetString(0);
            txtMatricula.Text = registro.Cells["AERO_MATRI"].Value.ToString();
            txtModelo.Text = registro.Cells["AERO_MOD"].Value.ToString();
            txtFabricante.Text = registro.Cells["AERO_FAB"].Value.ToString();
            txtCantButacas.Text = registro.Cells["AERO_CANT_BUTACAS"].Value.ToString();
            txtCantKgs.Text = registro.Cells["AERO_CANT_KGS"].Value.ToString();
            

        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*SqlCommand command = new SqlCommand();
            command.Connection = Program.conexion();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "[GD2C2015].[ABSTRACCIONX4].[agregarFechaLlegada]";
            command.CommandTimeout = 0;

            command.Parameters.AddWithValue("@fecha", dateTimePicker1.Value.ToString());
            command.Parameters.AddWithValue("@viajecod", viaje_cod);

            command.ExecuteScalar();*/

            MessageBox.Show("Se asigno la fecha: " + dateTimePicker1.Value.ToString(), "Fecha Llegada Asignada", MessageBoxButtons.OK);
            Form formularioSiguiente = new Menu();
            this.cambiarVisibilidades(formularioSiguiente);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form formularioSiguiente = new Form1();
            this.cambiarVisibilidades(formularioSiguiente);
        }

        private void cambiarVisibilidades(Form formularioSiguiente)
        {
            formularioSiguiente.Visible = true;
            this.Visible = false;
        }
    }
}
