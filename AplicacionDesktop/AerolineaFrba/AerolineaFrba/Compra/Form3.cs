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
    public partial class Form3 : Form
    {
        public Form anterior;
        public Form formularioSiguiente;

        public int cantidadButacasDisponibles;
        public double cantidadKilosDisponibles;

        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            this.inicio();
        }

        private void inicio()
        {
            chkEncomiendas.Checked = false;
            chkPasajes.Checked = false;

            txtKilos.Enabled = false;
            txtButacas.Enabled = false;

            txtButacas.Text = "";
            txtKilos.Text = "";
        }


        private void button4_Click(object sender, EventArgs e)
        {
            this.inicio();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Boolean huboErrores = false;
            Boolean huboErrores2 = false;

            if (chkPasajes.Checked)
            {
                huboErrores = Validacion.esVacio(txtButacas, "Butacas", true);
                huboErrores = !Validacion.numeroCorrecto(txtButacas, "Butacas", false);
                
                if(Convert.ToInt32(txtButacas.Text) > this.cantidadButacasDisponibles)
                {
                    MessageBox.Show("La cantidad de butacas solicitadas supera a a cantidad de butacas disponibles", "Error en las butacas", MessageBoxButtons.OK);
                    huboErrores = true;
                }
            }

            if (chkEncomiendas.Checked)
            {
                huboErrores2 = Validacion.esVacio(txtKilos, "Kilos para Encomienda", true);
                huboErrores2 = !Validacion.numeroCorrecto(txtKilos, "Kilos para Encomienda", true);

                if (Convert.ToDouble(txtKilos.Text) > this.cantidadKilosDisponibles)
                {
                    MessageBox.Show("La cantidad de kilos solicitados supera a a cantidad de kilos disponibles", "Error en el pesaje de la encomienda", MessageBoxButtons.OK);
                    huboErrores = true;
                }
            }

            if ((chkPasajes.Checked && !huboErrores) || (chkEncomiendas.Checked && !huboErrores2))
            {
                (this.formularioSiguiente as Compra.Form4).cantidadButacasDisponibles = this.cantidadButacasDisponibles;
                (this.formularioSiguiente as Compra.Form4).cantidadKilosDisponibles = this.cantidadKilosDisponibles;
                this.cambiarVisibilidades(this.formularioSiguiente);
            }
        }

        private void chkPasajes_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPasajes.Checked)
                txtButacas.Enabled = true;
            else
                txtButacas.Enabled = false;
        }

        private void chkEncomiendas_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEncomiendas.Checked)
                txtKilos.Enabled = true;
            else
                txtKilos.Enabled = false;
        }

        private void cambiarVisibilidades(Form formularioSiguiente)
        {
            this.formularioSiguiente.Visible = true;
            this.Visible = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.cambiarVisibilidades(this.anterior);
        }
    }
}
