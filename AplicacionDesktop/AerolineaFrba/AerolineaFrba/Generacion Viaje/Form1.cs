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

        string serv_cod = null;

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
            dateTimePicker1.Format = DateTimePickerFormat.Time;
            dateTimePicker2.Format = DateTimePickerFormat.Time;

            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now;
            
        }

        public void seSeleccionoAeronave(DataGridViewRow registro)
        {
            txtMatricula.Text = registro.Cells["AERO_MATRI"].Value.ToString();

            this.serv_cod = registro.Cells["SERV_COD"].Value.ToString();
        }

        public void seSeleccionoRuta(DataGridViewRow registro)
        {
            txtRuta.Text = registro.Cells["RUTA_ID"].Value.ToString();

            this.serv_cod = registro.Cells["SERV_COD"].Value.ToString();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (this.serv_cod != null)
                this.listadoAeronaves.serv_cod = this.serv_cod;

            this.cambiarVisibilidades(this.listadoAeronaves);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.serv_cod != null)
                this.listadoRutas.serv_cod = this.serv_cod;

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

    }
}
