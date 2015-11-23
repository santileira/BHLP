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
            if (!chkPasajes.Checked && !chkEncomiendas.Checked)
            {
                MessageBox.Show("Debe seleccionar al menos un pasaje o encomienda", "Error Compra", MessageBoxButtons.OK);
            }
            else
            {

                Boolean huboErrores = false;
                Boolean huboErrores2 = false;

                int but = 0;
                double kg = 0;

                if (chkPasajes.Checked)
                {
                    huboErrores = Validacion.esVacio(txtButacas, "Pasajes", true) || !Validacion.numeroCorrecto(txtButacas, "Pasajes", false);

                    if (int.TryParse(txtButacas.Text, out but))
                    {
                        if (but > this.cantidadButacasDisponibles)
                        {
                            MessageBox.Show("La cantidad de butacas solicitadas supera a a cantidad de butacas disponibles", "Error en las butacas", MessageBoxButtons.OK);
                            huboErrores = true;
                        }
                    }
                }

                if (chkEncomiendas.Checked)
                {
                    huboErrores2 = Validacion.esVacio(txtKilos, "Kilos para Encomienda", true) && !Validacion.numeroCorrecto(txtKilos, "Kilos para Encomienda", true);

                    if (Validacion.numeroCorrecto(txtKilos, "Kilos para Encomienda", true))
                    {
                        double.TryParse(txtKilos.Text, out kg);

                        if (kg > this.cantidadKilosDisponibles)
                        {
                            MessageBox.Show("La cantidad de kilos solicitados supera a a cantidad de kilos disponibles", "Error en el pesaje de la encomienda", MessageBoxButtons.OK);
                            huboErrores2 = true;
                        }

                    }
                    else
                        huboErrores2 = true;
                }

                if (chkPasajes.Checked && !huboErrores)
                {
                    ((this.formularioSiguiente as Compra.Form4).butacas as Compra.Form2).cantidadButacas = but;
                    (this.formularioSiguiente as Compra.Form4).activarCompraPasajes();
                }
                else
                    (this.formularioSiguiente as Compra.Form4).desactivarCompraPasajes();

                if (chkEncomiendas.Checked && !huboErrores2)
                {
                    ((this.formularioSiguiente as Compra.Form4).servicioDeEncomiendas as Compra.Form5).cantidadKilos = kg;
                    (this.formularioSiguiente as Compra.Form4).activarCompraEncomienda();
                }
                else
                    (this.formularioSiguiente as Compra.Form4).desactivarCompraEncomienda();

                if (!huboErrores && !huboErrores2)
                {
                    this.cambiarVisibilidades(this.formularioSiguiente);
                }

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
            formularioSiguiente.Visible = true;
            this.Visible = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.cambiarVisibilidades(this.anterior);
        }
    }
}
