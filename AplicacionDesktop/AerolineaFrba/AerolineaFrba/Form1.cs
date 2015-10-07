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

        private void Menu_Load(object sender, EventArgs e)
        {

        }

    }
}
