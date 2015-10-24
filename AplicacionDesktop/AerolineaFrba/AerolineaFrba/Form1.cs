using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AerolineaFrba
{
    public partial class Menu : Form
    {
        Form formularioSiguiente;

        public Menu()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            formularioSiguiente = new Abm_Rol.Principal();
            this.cambiarVisibilidades(formularioSiguiente);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            formularioSiguiente = new Abm_Aeronave.Principal();
            this.cambiarVisibilidades(formularioSiguiente);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            formularioSiguiente = new Abm_Ciudad.Principal();
            this.cambiarVisibilidades(formularioSiguiente);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            formularioSiguiente = new Abm_Ruta.Principal();
            this.cambiarVisibilidades(formularioSiguiente);
        }

        private void cambiarVisibilidades(Form formularioSiguiente)
        {
            formularioSiguiente.Visible = true;
            this.Visible = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            formularioSiguiente = new Registro_Llegada_Destino.Form1();
            this.cambiarVisibilidades(formularioSiguiente);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Generacion_Viaje.Form1 generadorViajes = new Generacion_Viaje.Form1();
            Abm_Aeronave.Listado listadoAeronaves = new Abm_Aeronave.Listado();
            Abm_Ruta.Listado listadoRutas = new Abm_Ruta.Listado();

            generadorViajes.listadoAeronaves = listadoAeronaves;
            generadorViajes.listadoRutas = listadoRutas;

            listadoAeronaves.anterior = generadorViajes;
            listadoAeronaves.loActivoGenerarViajes = true;

            listadoRutas.anterior = generadorViajes;
            listadoRutas.loActivoGenerarViajes = true;

            this.cambiarVisibilidades(generadorViajes);

        }

        private void button7_Click(object sender, EventArgs e)
        {
            Compra.Form1 compra = new Compra.Form1();
            Compra.Form2 compra2 = new Compra.Form2();
            compra.formularioSiguiente = compra2;
            compra2.anterior = compra;
            this.cambiarVisibilidades(compra);
        }

    }
}
