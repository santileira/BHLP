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
        public Form anterior;

        public Form2(DataGridViewRow registro, int viajeCod)
        {
            InitializeComponent();
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "dd/MM/yyyy - HH:mm:ss";
            /*dateTimePicker1.MinDate = fechaSalida;
            dateTimePicker1.MaxDate = fechaSalida.AddDays(1);*/
            dateTimePicker1.Value = Program.fechaHoy();

            viaje_cod = viajeCod;

            SqlDataReader reader;
            SqlCommand consultaServicios = new SqlCommand();
            consultaServicios.CommandType = CommandType.Text;
            consultaServicios.CommandText = "SELECT s.SERV_DESC FROM [ABSTRACCIONX4].[AERONAVES] a JOIN [ABSTRACCIONX4].[SERVICIOS] s ON a.SERV_COD = s.SERV_COD WHERE a.AERO_MATRI = '" + registro.Cells["Matricula"].Value.ToString() + "'";
            consultaServicios.Connection = Program.conexion();

            reader = consultaServicios.ExecuteReader();
            reader.Read();

            txtServicio.Text = reader.GetString(0);
            txtMatricula.Text = registro.Cells["Matricula"].Value.ToString();
            txtModelo.Text = registro.Cells["Modelo"].Value.ToString();
            txtFabricante.Text = registro.Cells["Fabricante"].Value.ToString();
            txtCantButacas.Text = registro.Cells["Cant. Butacas"].Value.ToString();
            txtCantKgs.Text = registro.Cells["Cant. Kgs"].Value.ToString();
            

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
            

            DateTime fechaSalida = (DateTime)obtenerFechaSalida(viaje_cod);
            DateTime fechaMaxima = fechaSalida.AddDays(1);
            /*TimeSpan ts = dateTimePicker1.Value - fechaSalida;

            int differenceInDays = ts.Days;
            int differenceInHours = ts.Hours;
            int differenceInMinutes = ts.Minutes;*/

            if ((dateTimePicker1.Value > fechaSalida) && (dateTimePicker1.Value<fechaMaxima))
            {

                if (debeHaberLlegadoAntes(fechaSalida, dateTimePicker1.Value))
                {

                    new SQLManager().generarSP("agregarFechaLlegada")
                                      .agregarStringSP("@fecha", dateTimePicker1.Value.ToString())
                                        .agregarIntSP("@viajecod", viaje_cod)
                                          .ejecutarSP();

                    new SQLManager().generarSP("agregarCantButacasAViajes")
                                       .agregarIntSP("@viaje_cod", viaje_cod)
                                        .ejecutarSP();

                    MessageBox.Show("Se asigno la fecha: " + dateTimePicker1.Value.ToString(), "Fecha Llegada Asignada", MessageBoxButtons.OK);

                    this.Close();
                }
                else
                {
                    MessageBox.Show("Dicha aeronave debe haber llegado antes, ya que hay un vuelo con fecha de salida anterior a la fecha indicada. ", "Fecha Incorrecta", MessageBoxButtons.OK);
                }

            }else{
                MessageBox.Show("La fecha de llegada debe ser dentro del rango de 24hs. posterior a la fecha de salida ", "Fecha Incorrecta", MessageBoxButtons.OK);
            }

        }

        private bool debeHaberLlegadoAntes(DateTime fechaSalida, DateTime fechaLlegada)
        {

            SqlCommand command = new SqlCommand();
            command.Connection = Program.conexion();
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = "SELECT ABSTRACCIONX4.debeHaberLlegadoAntes(@fechaSalida,@fechaLlegada,@aero_matri)";
            command.CommandTimeout = 0;
            
            command.Parameters.AddWithValue("@fechaSalida", fechaSalida);
            command.Parameters.AddWithValue("@fechaLlegada", fechaLlegada);
            command.Parameters.AddWithValue("@aero_matri", txtMatricula.Text);



            return (bool)command.ExecuteScalar();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.cambiarVisibilidades(anterior);
        }

        private void cambiarVisibilidades(Form formularioSiguiente)
        {
            formularioSiguiente.Visible = true;
            this.Visible = false;
        }

        private DateTime obtenerFechaSalida(int viajeCod)
        {

            SqlCommand command = new SqlCommand();
            command.Connection = Program.conexion();
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = "SELECT ABSTRACCIONX4.obtenerFechaSalidaDeUnViaje(@ViajeCod)";
            command.CommandTimeout = 0;

            command.Parameters.AddWithValue("@ViajeCod", viajeCod);

            return (DateTime)command.ExecuteScalar();

        }

    }
}
