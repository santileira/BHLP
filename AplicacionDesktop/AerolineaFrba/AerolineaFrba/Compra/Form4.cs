using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AerolineaFrba.Compra
{
    public partial class Form4 : Form
    {
        public Form servicioDeEncomiendas;
        public Form butacas;

        public Form anterior;

        public int cantidadButacasDisponibles;
        public double cantidadKilosDisponibles;

        public Form4()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.cambiarVisibilidades(this.anterior);
        }

        private void cambiarVisibilidades(Form formularioSiguiente)
        {
            formularioSiguiente.Visible = true;
            this.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            (this.butacas as Compra.Form2).inicio();
            this.cambiarVisibilidades(this.butacas);
        }
    }
}
