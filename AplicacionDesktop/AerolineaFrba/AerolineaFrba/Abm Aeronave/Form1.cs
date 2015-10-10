using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AerolineaFrba.Abm_Aeronave
{
    public partial class Principal : Form
    {
        Form formularioSiguiente;

        public Principal()
        {
            InitializeComponent();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            /*formularioSiguiente = new Alta();
            this.cambiarVisibilidades(formularioSiguiente);*/
        }
        
        private void button2_Click_1(object sender, EventArgs e)
        {
            /*formularioSiguiente = new Baja();
            this.cambiarVisibilidades(formularioSiguiente);*/
        }

        private void button3_Click(object sender, EventArgs e)
        {
            /*formularioSiguiente = new Modificacion();
            this.cambiarVisibilidades(formularioSiguiente);*/
        }
        
        private void button4_Click(object sender, EventArgs e)
        {
            formularioSiguiente = new Listado();
            this.cambiarVisibilidades(formularioSiguiente);
        }

        private void cambiarVisibilidades(Form formularioSiguiente)
        {
            formularioSiguiente.Visible = true;
            this.Visible = false;
        }

        private void Principal_Load(object sender, EventArgs e)
        {

        }

        private void Principal_Load_1(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            formularioSiguiente = new Menu();
            this.cambiarVisibilidades(formularioSiguiente);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Alta alta = new Alta();
            Listado listado = new Listado();

            alta.listado = listado;
            listado.altaActiva = true;
            listado.alta = alta;
         
            this.cambiarVisibilidades(alta);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            /*
            Baja baja = new Baja();
            Listado listado = new Listado();

            baja.listado = listado;
            listado.anterior = baja;

            this.cambiarVisibilidades(baja);
             * */
        }


    }
}
