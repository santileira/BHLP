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
            Compra.Form3 ingresarCantidades = new Compra.Form3();
            Compra.Form4 cargaDeDatos = new Compra.Form4();

            Compra.Form2 butacas = new Compra.Form2();
            Compra.Form5 servicioDeEncomiendas = new Compra.Form5();

            Compra.Form6 efectuaCompra = new Compra.Form6();

            compra.formularioSiguiente = ingresarCantidades;
            ingresarCantidades.anterior = compra;

            ingresarCantidades.formularioSiguiente = cargaDeDatos;
            cargaDeDatos.anterior = ingresarCantidades;

            cargaDeDatos.butacas = butacas;
            butacas.anterior = cargaDeDatos;
            cargaDeDatos.servicioDeEncomiendas = servicioDeEncomiendas;
            servicioDeEncomiendas.anterior = cargaDeDatos;

            cargaDeDatos.efectuaCompra = efectuaCompra;
            efectuaCompra.anterior = cargaDeDatos;
            

            cargaDeDatos.crearColumnas();
            

            
            this.cambiarVisibilidades(compra);
        }

    }
}
