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

namespace AerolineaFrba.Compra
{
    public partial class Form6 : Form
    {
        public Form anterior;

        public Form6()
        {
            InitializeComponent();

            //
            // Carga del contenido de combos
            //

            SqlDataReader varTarjeta;
            SqlCommand consultaColumnas = new SqlCommand();
            consultaColumnas.CommandType = CommandType.Text;
            consultaColumnas.CommandText = "SELECT TIPO_DESC FROM [ABSTRACCIONX4].TIPOS";
            consultaColumnas.Connection = Program.conexion();
            varTarjeta = consultaColumnas.ExecuteReader();

            while (varTarjeta.Read())
            {
                this.comboBox2.Items.Add(varTarjeta.GetValue(0));
            }

            int anioActual = (int)DateTime.Now.Year;            
            for(int i=0;i<10;i++){
                cboAnios.Items.Add(anioActual+i);
            }

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem.ToString() == "Tarjeta de crédito")
            {
                groupBox3.Visible = true;
            }
            else
            {
                groupBox3.Visible = false;
            }
                
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.cambiarVisibilidades(anterior);
        }

        private void cambiarVisibilidades(Form formularioSiguiente)
        {
            formularioSiguiente.Visible = true;
            this.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtDire.Enabled = true;
            dp.Enabled = true;
            txtNom.Enabled = true;
            txtTel.Enabled = true;
            txtMail.Enabled = true;

            SqlDataReader reader;
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "select * from [ABSTRACCIONX4].buscarCliente(@dni,@ape)";
            command.Connection = Program.conexion();

            command.Parameters.AddWithValue("@dni", txtDni.Text);
            command.Parameters.AddWithValue("@ape", txtApe.Text);

            reader = command.ExecuteReader();

            reader.Read();

            if (reader.HasRows)
            {
                txtNom.Text = reader.GetValue(0).ToString();
                txtDire.Text = reader.GetValue(1).ToString();
                txtTel.Text = reader.GetValue(2).ToString();
                txtMail.Text = reader.GetValue(3).ToString();
                dp.Value = (DateTime)reader.GetValue(4);

            }
            else
            {
                MessageBox.Show("No se encuentra cargado el cliente en la BD. Por favor, ingresar los datos para darle de alta", "Cliente no encontrado", MessageBoxButtons.OK);
            }


        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlDataReader varCuotas;
            SqlCommand consulta = new SqlCommand();
            consulta.CommandType = CommandType.Text;
            consulta.CommandText = "SELECT TIPO_CUO FROM [ABSTRACCIONX4].TIPOS WHERE TIPO_DESC='" + comboBox2.SelectedItem.ToString() + "'";
            consulta.Connection = Program.conexion();
            varCuotas = consulta.ExecuteReader();

            varCuotas.Read();

            if ((bool)varCuotas.GetValue(0))
            {
                checkBox1.Visible = true;
            }
            else
            {
                checkBox1.Visible = false;
            }
        }
    }
}
