using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AerolineaFrba.Listado_Estadistico
{
    public partial class Form1 : Form
    {
        string mensaje;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.inicio();
        }

        private void inicio()
        {
            cboSemestre.SelectedIndex = 0;
            cboEstadistica.SelectedIndex = 0;
            txtAnio.Text = "";
            dg.Visible = false;

            dg.CurrentCell = null;

        } 
        private string semestreSeleccionado()
        {
            if (cboSemestre.SelectedIndex == 0)
                return "1";
            else
                return "2";
        }

        /*
         * Metodo que, dado el elemento/index seleccionado del combo de estadisticas, devuelve la cadena
         * que representa el nombre de la function a ejecutar para obtener el TOP 5 correspondiente
         */
        private string estadisticaSeleccionada()
        {
            if (cboEstadistica.SelectedIndex == 0)
            {
                mensaje = "Para el período seleccionado no existen 5 destinos para los cuales se hayan comprado pasajes. Se mostraran los destinos para los cuales se registro compra de pasajes, ordenados por cantidad de pasajes vendidos";
                return "[ABSTRACCIONX4].destinosConMasPasajesVendidos";
            }
            if (cboEstadistica.SelectedIndex == 1)
            {
                mensaje = "Para el período seleccionado no existen 5 aeronaves que hayan realizado viajes. Se mostraran las aeronaves con viajes registrados, ordenados por cantidad de butacas vacias";
                return "[ABSTRACCIONX4].destinosConAeronaveMasVacia";
            }
            if (cboEstadistica.SelectedIndex == 2)
            {
                mensaje = "Para el período seleccionado no existen 5 clientes que hayan comprado pasajes. Se mostraran los clientes para los cuales se registraron millas por compra de pasajes no cancelados, ordenados por cantidad de millas";
                return "[ABSTRACCIONX4].clientesConMasMillas";
            }
            if (cboEstadistica.SelectedIndex == 3)
            {
                mensaje = "Para el período seleccionado no existen 5 clientes que hayan cancelado pasajes. Se mostraran los clientes que hayan comprado pasajes, y luego que los hayan cancelado, ordenados por cantidad de pasajes cancelados";
                return "[ABSTRACCIONX4].destinosConMasPasajesCancelados";
            }
            else
                mensaje = "Para el período seleccionado no existen 5 aeronaves que hayan estado fuera de servicio. Se mostraran las aeronaves para los cuales se registraron periodos fuera de servicios, ordenados por tiempo fuera de servicio";
            
            return "[ABSTRACCIONX4].aeronavesConMayorFueraDeServicio";
        }

        /*
         * Metodo que ejecuta la procedure setteada en el atributo estadisticaSeleccionada, con los
         * parametros año y semestre ingresados por pantalla
         */
        private void button1_Click_1(object sender, EventArgs e)
        {
            Boolean huboError = false;
            huboError = !Validacion.estaSeleccionado(cboSemestre,  true , "Semestre") || huboError;
            huboError = Validacion.esVacio(txtAnio, "Año", true) || huboError;
            huboError = !Validacion.esNumero(txtAnio, "Año", true) || huboError;
            huboError = !Validacion.estaEntreLimites(txtAnio,1990,2050,false, "Año") || huboError;
            huboError = !Validacion.estaSeleccionado(cboEstadistica, true, "Estadística") || huboError;
            

            if (!huboError)
            {
                SQLManager.ejecutarQuery("select * from " + this.estadisticaSeleccionada() + "(" + this.semestreSeleccionado() + ", '" + txtAnio.Text + "')", dg);
                dg.CurrentCell = null;

                if (dg.RowCount == 0)
                {
                    MessageBox.Show("No se encontraron registros para armar el TOP 5", "Estadistica vacia", MessageBoxButtons.OK);
                    dg.Visible = false;
                }
                else
                {
                    dg.Visible = true;

                    if (dg.RowCount < 5)
                        MessageBox.Show(mensaje, "Estadistica incompleta", MessageBoxButtons.OK);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.inicio();
        }



        private void cambiarVisibilidades(Form formularioSiguiente)
        {
            formularioSiguiente.Visible = true;
            this.Visible = false;
        }
    }
}
