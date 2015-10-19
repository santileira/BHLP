using System;
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

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        public void seSeleccionoAeronave(DataGridViewRow registro)
        {
            txtMatricula.Text = registro.Cells["AERO_MATRI"].Value.ToString();
        }

        public void seSeleccionoRuta(DataGridViewRow registro)
        {
            txtRuta.Text = registro.Cells["RUTA_ID"].Value.ToString();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.cambiarVisibilidades(this.listadoAeronaves);
        }

        private void button1_Click(object sender, EventArgs e)
        {
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

    }
}
