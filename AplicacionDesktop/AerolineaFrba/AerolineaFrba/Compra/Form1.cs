﻿using System;
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
    public partial class Form1 : Form
    {
        Boolean seleccionandoOrigen;
        public Boolean primeraVez = true;
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
            dateTimePicker1.CustomFormat = "dd/MM/yyyy";

            dateTimePicker1.Value = DateTime.Now;

            txtCiudadDestino.Text = "";
            txtCiudadOrigen.Text = "";

            button1.Enabled = false;
            button2.Enabled = true;
            button3.Enabled = false;

            dg.DataSource = null;
        }
        

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

        private void button1_Click(object sender, EventArgs e)
        {
            if (DateTime.Compare(DateTime.Now, dateTimePicker1.Value) == 1)
            {
                if (dateTimePicker1.Value.Year != DateTime.Now.Year || dateTimePicker1.Value.Month != DateTime.Now.Month || dateTimePicker1.Value.Day != DateTime.Now.Day)
                    MessageBox.Show("La fecha de salida no puede ser anterior a la fecha de hoy");
                else
                {
                    this.ejecutarQuery();
                }
            }
            else
                this.ejecutarQuery();
        }

        private void ejecutarQuery()
        {
            string query = "select * from [ABSTRACCIONX4].buscarViajesDisponibles('" + dateTimePicker1.Value.ToString() + "', '" + txtCiudadOrigen.Text + "', '" + txtCiudadDestino.Text + "')";
            SqlConnection conexion = Program.conexion();
            DataTable t = new DataTable("Busqueda");
            SqlDataAdapter a = new SqlDataAdapter(query, conexion);
            //Llenar el Dataset
            DataSet ds = new DataSet();
            a.Fill(ds, "Busqueda");
            //Ligar el datagrid con la fuente de datos
            dg.DataSource = ds;
            dg.DataMember = "Busqueda";

            conexion.Close();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if (DateTime.Compare(DateTime.Now, dateTimePicker1.Value) == 1)
            {
                if (dateTimePicker1.Value.Year != DateTime.Now.Year || dateTimePicker1.Value.Month != DateTime.Now.Month || dateTimePicker1.Value.Day != DateTime.Now.Day)
                {
                    MessageBox.Show("La fecha de salida no puede ser anterior a la fecha de hoy");
                    dateTimePicker1.Value = DateTime.Now;
                }
            }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            formularioSiguiente = new Menu();
            this.cambiarVisibilidades(formularioSiguiente);
        }

    }
}
