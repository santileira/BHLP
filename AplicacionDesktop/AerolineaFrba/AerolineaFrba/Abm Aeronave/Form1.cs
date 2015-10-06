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
            formularioSiguiente = new Alta();
            this.cambiarVisibilidades(formularioSiguiente);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            formularioSiguiente = new Baja();
            this.cambiarVisibilidades(formularioSiguiente);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            formularioSiguiente = new Modificacion();
            this.cambiarVisibilidades(formularioSiguiente);
        }

        private void cambiarVisibilidades(Form formularioSiguiente)
        {
            formularioSiguiente.Visible = true;
            this.Visible = false;
        }
       
    }
}
