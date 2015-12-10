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
            this.VisibleChanged += new EventHandler(this.Form_VisibleChanged);
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            this.inicio();
        }

        private void Form_VisibleChanged(object sender, EventArgs e)
        {
            groupBox1.Enabled = cantidadButacasDisponibles > 0;
            groupBox2.Enabled = cantidadKilosDisponibles > 0;
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

        /*
         * Metodo que verifica que las cantidades de pasajes y/o encomiendas ingresados sean menores o iguales
         * a la capacidad maxima disponible de la aeronave. Si se cumple, se habilita el formulario para
         * la seleccion de pasajes y encomiendas para los pasajeros
         */
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
                    huboErrores = Validacion.esVacio(txtButacas, "cantidad de pasajes", true) || huboErrores;
                    huboErrores = !Validacion.esNumero(txtButacas, "cantidad de pasajes", true) || huboErrores;
                    huboErrores = !Validacion.estaEntreLimites(txtButacas,1,cantidadButacasDisponibles,false,"cantidad de pasajes") || huboErrores;
                    if (!Validacion.esVacio(txtButacas, "cantidad de pasajes", false) &&
                        Validacion.esNumero(txtButacas, "cantidad de pasajes", false) &&
                        Convert.ToInt16(txtButacas.Text) > cantidadButacasDisponibles)
                    {
                        MessageBox.Show("La cantidad de butacas disponibles restantes es " + cantidadButacasDisponibles.ToString(), "Error Compra", MessageBoxButtons.OK);
                    }
                }

                if (chkEncomiendas.Checked)
                {
                    huboErrores2 = Validacion.esVacio(txtKilos, "cantidad de kilos", true) || huboErrores2;
                    huboErrores2 = !Validacion.esDecimal(txtKilos, "cantidad de kilos", true) || huboErrores2;
                    huboErrores2 = !Validacion.estaEntreLimites(txtKilos, 0.01m, Convert.ToDecimal(cantidadKilosDisponibles), true, "cantidad de kilos") || huboErrores2;
                    if (!Validacion.esVacio(txtKilos, "cantidad de kilos", false) &&
                        Validacion.esDecimal(txtKilos, "cantidad de kilos", false) &&
                        Convert.ToDecimal(txtKilos.Text.Replace(".",",")) > Convert.ToDecimal(cantidadKilosDisponibles))
                    {
                        MessageBox.Show("La cantidad de kilos disponibles restantes es " + cantidadKilosDisponibles.ToString(), "Error Compra", MessageBoxButtons.OK);
                    }
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
                    int cantBut;
                    double cantKilo;
                    int.TryParse(txtButacas.Text, out cantBut);
                    double.TryParse(txtKilos.Text.Replace(".",","), out cantKilo);

                    (formularioSiguiente as Compra.Form4).butacasSelec = cantBut;
                    (formularioSiguiente as Compra.Form4).kilosSelec = Math.Round(cantKilo,2);
                    this.cambiarVisibilidades(this.formularioSiguiente);
                    
                }



            }
        }

        private void chkPasajes_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPasajes.Checked)
            {
                txtButacas.Enabled = true;
            }
            else
            {
                txtButacas.Enabled = false;
                txtButacas.Text = "";
            }   
        }

        private void chkEncomiendas_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEncomiendas.Checked)
            {
                txtKilos.Enabled = true;
            }
            else
            {
                txtKilos.Enabled = false;
                txtKilos.Text = "";
            }
        }

        private void cambiarVisibilidades(Form formularioSiguiente)
        {
            formularioSiguiente.Visible = true;
            this.Visible = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.inicio();
            this.cambiarVisibilidades(this.anterior);
        }
    }
}
