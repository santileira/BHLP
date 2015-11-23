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

namespace AerolineaFrba.Generacion_Viaje
{
    public partial class Form1 : Form
    {
        public Abm_Aeronave.Listado listadoAeronaves;
        public Abm_Ruta.Listado listadoRutas;

        Boolean sePuedeGuardar = false;
        Boolean primeraVez = true;

        Form formularioSiguiente;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.inicio();
        }

        private void inicio()
        {
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "dd/MM/yyyy - HH:mm";
            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.CustomFormat = "dd/MM/yyyy - HH:mm";

            dateTimePicker1.Value = Program.fechaHoy();
            dateTimePicker2.Value = Program.fechaHoy();
            
            txtMatricula.Text = "";
            txtRuta.Text = "";

            this.listadoAeronaves.serv_cod = null;
            this.listadoRutas.serv_cod = null;

            sePuedeGuardar = false;
        }

        public void seSeleccionoAeronave(DataGridViewRow registro)
        {
            txtMatricula.Text = registro.Cells["AERO_MATRI"].Value.ToString();
            this.listadoRutas.serv_cod = registro.Cells["SERV_COD"].Value.ToString();
        }

        public void seSeleccionoRuta(DataGridViewRow registro)
        {
            txtRuta.Text = registro.Cells["Id"].Value.ToString();
            this.listadoAeronaves.serv_cod = registro.Cells["serv_cod"].Value.ToString();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (this.fechasErroneas())
                MessageBox.Show("Verifique que las fechas de salida y llegada ingresadas sean correctas", "Error en los datos de entrada", MessageBoxButtons.OK);
            else if (DateTime.Compare(Program.fechaHoy(), dateTimePicker1.Value) == 1)
                MessageBox.Show("La fecha de salida no puede ser anterior a la fecha de hoy");
            else if (DateTime.Compare(Program.fechaHoy(), dateTimePicker2.Value) == 1)
                MessageBox.Show("La fecha de llegada estimada no puede ser anterior a la fecha de hoy");
            else
            {
                this.listadoAeronaves.queryViajes = " WHERE ABSTRACCIONX4.aeronave_disponible(AERO_MATRI, '"
                + dateTimePicker1.Value + "', '" + dateTimePicker2.Value + "') = 1 ";

                this.listadoAeronaves.extenderQuery();
                this.listadoAeronaves.ejecutarConsulta();
                this.cambiarVisibilidades(this.listadoAeronaves);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.listadoRutas.generarQueryInicial();
            this.listadoRutas.ejecutarQuery();
            this.cambiarVisibilidades(this.listadoRutas);
        }

        private void cambiarVisibilidades(Form formularioSiguiente)
        {
            formularioSiguiente.Visible = true;
            this.Visible = false;
        }


        private Boolean fechasErroneas()
        {
            Boolean huboError = false;

            if (DateTime.Compare(dateTimePicker1.Value, dateTimePicker2.Value) == 1)
            {
                huboError = true;
                MessageBox.Show("La fecha de llegada no puede ser anterior a la fecha de salida");
            }

            TimeSpan diferencia = dateTimePicker2.Value - dateTimePicker1.Value;

            if (diferencia.Days >= 1)
            {
                huboError = true;
                MessageBox.Show("Las aeronaves tardan como mucho 24 hs en llegar a destino");
            }

            if (DateTime.Compare(Program.fechaHoy(), dateTimePicker1.Value) == 1)
            {
                huboError = true;
                MessageBox.Show("La fecha de salida no puede ser anterior a la fecha de hoy"); 
            }

            if (DateTime.Compare(Program.fechaHoy(), dateTimePicker2.Value) == 1)
            {
                huboError = true;
                MessageBox.Show("La fecha de llegada no puede ser anterior a la fecha de hoy");
            }

            return huboError;
        }


        private void button3_Click(object sender, EventArgs e)
        {
            this.inicio();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Boolean huboError = false;
             
            if (txtMatricula.Text == "")
            {
                huboError = true;
                MessageBox.Show("Debe seleccionar una aeronave", "Error en los datos de entrada", MessageBoxButtons.OK);
            }
            if (txtRuta.Text == "")
            {
                huboError = true;
                MessageBox.Show("Debe seleccionar una ruta aerea", "Error en los datos de entrada", MessageBoxButtons.OK);
            }
            if (this.fechasErroneas())
            {
                huboError = true;
                MessageBox.Show("Verifique que las fechas de salida y llegada ingresadas sean correctas", "Error en los datos de entrada", MessageBoxButtons.OK);
            }
            
            if(!huboError)
            {
                this.insertarNuevoViaje();
                MessageBox.Show("Se inserto correctamente el nuevo viaje, el cual ya se encuentra disponible para la venta de pasajes", "Nuevo viaje", MessageBoxButtons.OK);
            }
        }

        private Object insertarNuevoViaje()
        {
            SQLManager manejador = new SQLManager();
            manejador.generarSP("GenerarNuevoViaje");
            manejador.agregarStringSP("@salida", dateTimePicker1.Value.ToString());
            manejador.agregarStringSP("@llegadaEstimada", dateTimePicker2.Value.ToString());
            manejador.agregarIntSP("@ruta", txtRuta);
            manejador.agregarStringSP("@matricula", txtMatricula.Text);
            return manejador.ejecutarSP();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void txtRuta_TextChanged(object sender, EventArgs e)
        {

        }

    }
}
