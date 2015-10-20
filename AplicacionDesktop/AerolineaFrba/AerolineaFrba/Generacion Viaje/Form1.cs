using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
            dateTimePicker1.CustomFormat = "dd/MM/yyyy HH:mm:ss";
            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.CustomFormat = "dd/MM/yyyy HH:mm:ss";

            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now;

            txtMatricula.Text = "";
            txtRuta.Text = "";

            this.listadoAeronaves.serv_cod = null;
            this.listadoAeronaves.extenderQuery();
            this.listadoAeronaves.ejecutarConsulta();

            this.listadoRutas.serv_cod = null;
            this.listadoRutas.generarQueryInicial();
            this.listadoRutas.ejecutarQuery();
        }

        public void seSeleccionoAeronave(DataGridViewRow registro)
        {
            txtMatricula.Text = registro.Cells["AERO_MATRI"].Value.ToString();
            this.listadoRutas.serv_cod = registro.Cells["SERV_COD"].Value.ToString();
            this.listadoRutas.inicio();
        }

        public void seSeleccionoRuta(DataGridViewRow registro)
        {
            txtRuta.Text = registro.Cells["RUTA_ID"].Value.ToString();
            this.listadoAeronaves.serv_cod = registro.Cells["SERV_COD"].Value.ToString();
            this.listadoAeronaves.inicio();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.cambiarVisibilidades(this.listadoAeronaves);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.cambiarVisibilidades(this.listadoRutas);
        }

        private void cambiarVisibilidades(Form formularioSiguiente)
        {
            formularioSiguiente.Visible = true;
            this.Visible = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            formularioSiguiente = new Menu();
            this.cambiarVisibilidades(formularioSiguiente);
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            this.listadoAeronaves.fechaSalida = dateTimePicker1.Value;
            dateTimePicker2.Enabled = true;
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            this.listadoAeronaves.fechaLlegada = dateTimePicker2.Value;
            button5.Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.inicio();
        }

    }
}
