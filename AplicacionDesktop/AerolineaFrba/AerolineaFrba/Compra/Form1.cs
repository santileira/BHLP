﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AerolineaFrba.Compra
{
    public partial class Form1 : Form
    {
        Boolean seleccionandoOrigen;
        public Boolean primeraVez = true;
        public Form formularioSiguiente;

        public string viaje;
        public string matricula;
        public string origen;
        public string destino;
        public string fechaSalida;
        public string fechaLlegada;

        public Boolean seleccionoViaje;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.inicio();
        }

        public void completarCantidades(int butacas, string kilos)
        {
            lblButacas.Text = butacas.ToString();
            lblKilos.Text = kilos;
        }

        private void inicio()
        {
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "dd/MM/yyyy";

            dateTimePicker1.Value = Program.fechaHoy();

            txtCiudadDestino.Text = "";
            txtCiudadOrigen.Text = "";
            lblButacas.Text = "";
            lblKilos.Text = "";

            button1.Enabled = false;
            button2.Enabled = true;
            button3.Enabled = false;

            dg.DataSource = null;

            seleccionoViaje = false;
        }


        /*
         * Metodo que despliega un formulario para seleccionar una ciudad de entre un listado de ciudades disponibles
         */
        private void button2_Click(object sender, EventArgs e)
        {
            seleccionandoOrigen = true;
            Abm_Ruta.ListadoCiudades listado = new Abm_Ruta.ListadoCiudades(this);
            listado.vieneDeCompras = true;
            this.cambiarVisibilidades(listado);
        }

        private void cambiarVisibilidades(Form formularioSiguiente)
        {
            formularioSiguiente.Visible = true;
            this.Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            seleccionandoOrigen = false;
            Abm_Ruta.ListadoCiudades listado = new Abm_Ruta.ListadoCiudades(this);
            listado.vieneDeCompras = true;
            cambiarVisibilidades(listado);
        }

        public void seSelecciono(string ciudad)
        {
            if (seleccionandoOrigen)
            {
                txtCiudadOrigen.Text = ciudad;
                button3.Enabled = true;
            }
            else
            {
                txtCiudadDestino.Text = ciudad;
            }

            if (txtCiudadOrigen.Text != "" && txtCiudadDestino.Text != "")
                button1.Enabled = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.inicio();
        }

        /*
         * Metodo que verifica los datos ingresados (fecha de salida, origen y destino), controlando que la fecha
         * ingresada sea una fecha valida, y que se halla ingresado un origen y un destino para el pasaje o 
         * encomienda. Luego, se invoca a la procedure buscarViajesDisponibles que listara todos los viajes
         * cuyas aeronaves partan durante la fecha ingresada, en el origen ingresado y arriben al destino ingresado.
         */
        private void button1_Click(object sender, EventArgs e)
        {
            
            if (DateTime.Compare(Program.fechaHoy(), dateTimePicker1.Value) == 1)
            {
                if (dateTimePicker1.Value.Year != Program.fechaHoy().Year || dateTimePicker1.Value.Month != Program.fechaHoy().Month || dateTimePicker1.Value.Day != Program.fechaHoy().Day)
                    MessageBox.Show("La fecha de salida no puede ser anterior a la fecha de hoy");
                else
                {
                    lblButacas.Text = "";
                    lblKilos.Text = "";
                    SQLManager.ejecutarQuery("select * from [ABSTRACCIONX4].buscarViajesDisponibles('" + dateTimePicker1.Value.ToString() + "', '" + txtCiudadOrigen.Text + "', '" + txtCiudadDestino.Text + "')", dg);
                    //this.ejecutarQuery();

                    dg.Columns["Código de viaje"].Visible = false;
                    dg.Columns["Matrícula"].Visible = false;

                    dg.CurrentCell = null;

                    //En caso de que no encuentre viajes disponibles
                    if (dg.RowCount == 0)
                    {
                        MessageBox.Show("No se encontraron viajes disponibles", "Ningun viaje disponible", MessageBoxButtons.OK);
                        dg.Visible = false;
                    }
                    else
                        dg.Visible = true;
                        dg.CurrentCell = dg.SelectedRows[0].Cells[0];
                }
            }
            else
            {

                lblButacas.Text = "";
                lblKilos.Text = "";
                
                SQLManager.ejecutarQuery("select VIAJE_COD 'Código de viaje', AERO_MATRI 'Matrícula',Fecha_Salida 'Fecha de salida', Fecha_Llegada 'Fecha de llegada', Origen 'Origen', Destino 'Destino', Tipo_Servicio 'Tipo de servicio' from [ABSTRACCIONX4].buscarViajesDisponibles('" + dateTimePicker1.Value.ToString() + "', '" + txtCiudadOrigen.Text + "', '" + txtCiudadDestino.Text + "')", dg);
                //this.ejecutarQuery();

                dg.Columns["Código de viaje"].Visible = false;
                dg.Columns["Matrícula"].Visible = false;

                dg.CurrentCell = null;

                //En caso de que no encuentre viajes disponibles
                if (dg.RowCount == 0)
                {
                    MessageBox.Show("No se encontraron viajes disponibles", "Ningun viaje disponible", MessageBoxButtons.OK);
                    dg.Visible = false;
                }
                else
                    dg.Visible = true;
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        /*
         * Metodo que settea en variables cada uno de los datos correspondientes al registro seleccionado
         * del data grid que refleja los viajes disponibles
         */
        private void dg_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            this.viaje = dg.SelectedRows[0].Cells["Código de viaje"].Value.ToString();
            this.matricula = dg.SelectedRows[0].Cells["Matrícula"].Value.ToString();
            this.origen = txtCiudadOrigen.Text;
            this.destino = txtCiudadDestino.Text;
            this.fechaSalida = dg.SelectedRows[0].Cells["Fecha de salida"].Value.ToString();
            this.fechaLlegada = dg.SelectedRows[0].Cells["Fecha de llegada"].Value.ToString();

            (((formularioSiguiente as Compra.Form3).formularioSiguiente as Compra.Form4).butacas as Compra.Form2).seSelecciono(dg.SelectedRows[0]);
            seleccionoViaje = true;
        }

        /*
         * Metodo que verifica si la seleccion de viaje y la introduccion de datos es correcta. Si es asi, 
         * settea los valores correspondientes en el siguiente formulario (form 3) con las cantidades de 
         * butacas y encomienda disponible en la aeronave del viaje seleccionado
         */
        private void button5_Click(object sender, EventArgs e)
        {

            if (!seleccionoViaje)
                MessageBox.Show("No ha seleccionado ningun viaje. Verifique haber presionado sobre alguno de los datos que figuran en la grilla", "Error en la seleccion de viajes", MessageBoxButtons.OK);
            else
            {
                if (dg.CurrentCell == null)
                    MessageBox.Show("Debe seleccionar algun viaje disponible", "Error en la seleccion de viajes", MessageBoxButtons.OK);
                else
                {
                    if (Convert.ToInt32(lblButacas.Text) == 0 && Convert.ToDouble(lblKilos.Text) == 0)
                    {
                        MessageBox.Show("Este viaje no tiene más capacidad.", "Error en la seleccion de viajes", MessageBoxButtons.OK);
                        return;
                    }

                    (formularioSiguiente as Compra.Form3).cantidadButacasDisponibles = Convert.ToInt32(lblButacas.Text);
                    (formularioSiguiente as Compra.Form3).cantidadKilosDisponibles = Convert.ToDouble(lblKilos.Text);

                    this.cambiarVisibilidades(formularioSiguiente);
                }
            }


        }

        private void dg_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

    }
}
