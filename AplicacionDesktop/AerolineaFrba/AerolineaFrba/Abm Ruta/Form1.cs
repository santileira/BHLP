﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AerolineaFrba.Abm_Ruta
{
    public partial class Principal : Form
    {
        Form formularioSiguiente;

        public Principal()
        {
            InitializeComponent();
        }
        /*
        private void button1_Click(object sender, EventArgs e)
        {
            formularioSiguiente = new Alta();
            this.cambiarVisibilidades(formularioSiguiente);
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            formularioSiguiente = new Baja();
            this.cambiarVisibilidades(formularioSiguiente);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            formularioSiguiente = new Modificacion();
            this.cambiarVisibilidades(formularioSiguiente);
        }
        */
        private void cambiarVisibilidades(Form formularioSiguiente)
        {
            formularioSiguiente.Visible = true;
            this.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Baja baja = new Baja();
            Listado listado = new Listado();

            baja.listado = listado;
            listado.anterior = baja;

            this.cambiarVisibilidades(baja);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            Alta alta = new Alta();
            Listado listado = new Listado();

            alta.listado = listado;
            listado.alta = alta;

            this.cambiarVisibilidades(alta);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            formularioSiguiente = new Menu();
            cambiarVisibilidades(formularioSiguiente);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Modificacion modificacion = new Modificacion();
            Listado listado = new Listado();
            Principal principal = new Principal();

            modificacion.listado = listado;
            listado.siguiente = modificacion;
            listado.anterior = principal;

            this.cambiarVisibilidades(listado);
        }

        private void Principal_Load(object sender, EventArgs e)
        {

        }
    }
}
