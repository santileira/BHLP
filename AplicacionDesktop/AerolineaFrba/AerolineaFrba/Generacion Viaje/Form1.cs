﻿using System;
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

        Boolean sePuedeGuardar = false;

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
            dateTimePicker1.CustomFormat = "dd/mm/yyyy - HH:mm";
            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.CustomFormat = "dd/mm/yyyy - HH:mm";

            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now;

            txtMatricula.Text = "";
            txtRuta.Text = "";

            this.listadoAeronaves.serv_cod = null;
            this.listadoRutas.serv_cod = null;

            dateTimePicker2.Enabled = false;
            button5.Enabled = false;

            sePuedeGuardar = false;
        }

        public void seSeleccionoAeronave(DataGridViewRow registro)
        {
            txtMatricula.Text = registro.Cells["AERO_MATRI"].Value.ToString();
            this.listadoRutas.serv_cod = registro.Cells["SERV_COD"].Value.ToString();
        }

        public void seSeleccionoRuta(DataGridViewRow registro)
        {
            txtRuta.Text = registro.Cells["RUTA_ID"].Value.ToString();
            this.listadoAeronaves.serv_cod = registro.Cells["SERV_COD"].Value.ToString();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (this.fechasErroneas())
                MessageBox.Show("Verifique que las fechas de salida y llegada ingresadas sean correctas", "Error en los datos de entrada", MessageBoxButtons.OK);
            else
            {
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
            button5.Enabled = true;

            if (!fechasErroneas())
                this.listadoAeronaves.fechaLlegada = dateTimePicker2.Value;      
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
            }
        }

    }
}
