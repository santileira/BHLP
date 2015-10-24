using AerolineaFrba.Abm_Ruta;
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
    public partial class Form1 : Form
    {

        bool seleccionandoOrigen;
        DataGridViewRow aeronaveSeleccionada;
        int viaje_cod;

        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            seleccionandoOrigen = true;
            ListadoCiudades listado = new ListadoCiudades(this);
            listado.vieneDeArribo = true;
            cambiarVisibilidades(listado);
        }

        private void cambiarVisibilidades(Form formularioSiguiente)
        {
            formularioSiguiente.Visible = true;
            this.Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            seleccionandoOrigen = false;
            ListadoCiudades listado = new ListadoCiudades(this);
            listado.vieneDeArribo = true;
            cambiarVisibilidades(listado);
        }

        public void seSelecciono(string ciudad)
        {
            if (seleccionandoOrigen)
            {
                txtCiudadOrigen.Text = ciudad;
            }
            else
            {
                txtCiudadDestino.Text = ciudad;
            }
        }

        public void seSeleccionoMatricula(DataGridViewRow registro)
        {
             txtMatricula.Text = registro.Cells["AERO_MATRI"].Value.ToString();
             aeronaveSeleccionada = registro;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Abm_Aeronave.Listado listado = new Abm_Aeronave.Listado();
            listado.anterior = this;
            listado.seSeteaQuery = true;
            cambiarVisibilidades(listado);

        }

        public String consultaSeteada()
        {
            return "SELECT a.AERO_MATRI,AERO_MOD,AERO_FAB,SERV_DESC,AERO_CANT_BUTACAS,AERO_CANT_KGS from ABSTRACCIONX4.AERONAVES a JOIN ABSTRACCIONX4.SERVICIOS s ON (a.SERV_COD = s.SERV_COD) JOIN ABSTRACCIONX4.VIAJES v ON (a.AERO_MATRI = v.AERO_MATRI) WHERE v.VIAJE_FECHA_LLEGADA IS NULL";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (validarCampos())
            {
                viaje_cod = esDestinoValido();
                if (viaje_cod != -1)
                {
                    Form2 cargaFecha = new Form2(aeronaveSeleccionada, viaje_cod);
                    cargaFecha.anterior = this;
                    cambiarVisibilidades(cargaFecha);
                }
                else
                {
                    MessageBox.Show("El Destino seleccionado no corresponde al destino de la aeronave", "Destino inválido", MessageBoxButtons.OK);
                }
            }
        }

        private int esDestinoValido()
        {

            SqlCommand command = new SqlCommand();
            command.Connection = Program.conexion();
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = "SELECT ABSTRACCIONX4.llegaADestinoCorrecto(@Matricula , @Tipo)";
            command.CommandTimeout = 0;

            command.Parameters.AddWithValue("@Matricula", txtMatricula.Text);
            command.Parameters.AddWithValue("@Tipo", txtCiudadDestino.Text);

            return (int)command.ExecuteScalar();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form formularioSiguiente = new Menu(); 
            this.cambiarVisibilidades(formularioSiguiente);
        }

        private Boolean validarCampos()
        {

            return Validacion.textNombre(txtCiudadOrigen, "Ciudad Origen") &&
                    Validacion.textNombre(txtCiudadDestino, "Ciudad Destino") &&
                     Validacion.textNombre(txtMatricula, "Matricula") &&
                      !Validacion.igualdadCiudades(txtCiudadOrigen, txtCiudadDestino);
        }

    }
}
