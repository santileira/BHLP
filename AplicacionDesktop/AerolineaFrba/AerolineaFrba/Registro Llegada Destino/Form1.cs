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
        int viaje_cod_diferido;

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
             txtMatricula.Text = registro.Cells["Matricula"].Value.ToString();
             aeronaveSeleccionada = registro;
             int.TryParse(registro.Cells["Cod. Viaje"].Value.ToString(), out viaje_cod_diferido);
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

        //Método que devuelve la query necesaria para pasarle al listado de aeronaves. Para que solo me liste
        // aquellas aeronaves que ya salieron y que estan en vuelo o bien ya llegaron  pero no se les hizo el 
        // registro de llegada.

        public String consultaSeteada()
        {
            return "SELECT a.SERV_COD, a.AERO_MATRI as Matricula,AERO_MOD as Modelo,AERO_FAB as Fabricante,SERV_DESC as Servicio,(SELECT COUNT(BUT_ID) FROM ABSTRACCIONX4.BUTACAS B WHERE B.AERO_MATRI=a.AERO_MATRI) as 'Cant. Butacas',AERO_CANT_KGS as 'Cant. Kgs', v.VIAJE_COD as 'Cod. Viaje', v.VIAJE_FECHA_SALIDA as 'Fecha de Salida', v.VIAJE_FECHA_LLEGADAE as 'Fecha de Llegada Estimada' from ABSTRACCIONX4.AERONAVES a JOIN ABSTRACCIONX4.SERVICIOS s ON (a.SERV_COD = s.SERV_COD) JOIN ABSTRACCIONX4.VIAJES v ON (a.AERO_MATRI = v.AERO_MATRI) JOIN ABSTRACCIONX4.RUTAS_AEREAS r ON (r.RUTA_ID = v.RUTA_ID) WHERE v.VIAJE_FECHA_LLEGADA IS NULL AND v.VIAJE_FECHA_SALIDA < [ABSTRACCIONX4].obtenerFechaDeHoy() AND a.AERO_FECHA_BAJA is NULL AND r.RUTA_ESTADO = 1 AND NOT EXISTS (SELECT 1 FROM ABSTRACCIONX4.FUERA_SERVICIO_AERONAVES f WHERE ABSTRACCIONX4.datetime_is_between(v.VIAJE_FECHA_SALIDA,f.FECHA_FS,f.FECHA_REINICIO)=1 OR ABSTRACCIONX4.datetime_is_between(v.VIAJE_FECHA_LLEGADAE,f.FECHA_FS,f.FECHA_REINICIO)=1)";
        }

        //Método que verifica los destinos indicados.

        private void button4_Click(object sender, EventArgs e)
        {
            if (validarCampos())
            {
                if (esOrigenValido())
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
                        MessageBox.Show("El Destino seleccionado difiere al destino de Ruta", "Aviso de Destino Diferido", MessageBoxButtons.OK);

                        Form2 cargaFecha = new Form2(aeronaveSeleccionada, viaje_cod_diferido);
                        cargaFecha.anterior = this;
                        cambiarVisibilidades(cargaFecha);
                    }
                }
                else
                {
                    MessageBox.Show("El Origen seleccionado no corresponde al origen de la aeronave", "Origen inválido", MessageBoxButtons.OK);
                }
            }
        }


        private bool esOrigenValido()
        {

            SqlCommand command = new SqlCommand();
            command.Connection = Program.conexion();
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = "SELECT ABSTRACCIONX4.esOrigenCorrecto(@viaje_cod , @ciuOrigenTxt)";
            command.CommandTimeout = 0;

            command.Parameters.AddWithValue("@viaje_cod", viaje_cod_diferido);
            command.Parameters.AddWithValue("@ciuOrigenTxt", txtCiudadOrigen.Text);

            return (bool)command.ExecuteScalar();

        }

        private int esDestinoValido()
        {

            SqlCommand command = new SqlCommand();
            command.Connection = Program.conexion();
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = "SELECT ABSTRACCIONX4.llegaADestinoCorrecto(@viaje_cod , @ciuDestinoTxt)";
            command.CommandTimeout = 0;

            command.Parameters.AddWithValue("@viaje_cod", viaje_cod_diferido);
            command.Parameters.AddWithValue("@ciuDestinoTxt", txtCiudadDestino.Text);

            return (int)command.ExecuteScalar();

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
