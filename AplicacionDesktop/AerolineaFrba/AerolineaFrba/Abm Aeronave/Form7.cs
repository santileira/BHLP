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
    public partial class Form7 : Form
    {
        private string mensaje;
        private string matricula;
        private DateTime fechaBaja;
        private DateTime fechaReinicio;
        private Boolean llamadoDesdeBajaLogica;
        public Boolean cargoDatosParaSuplantar;

        public Form7(string unMensaje,string unaMatricula,Boolean desdeBajaLogica,DateTime unaFechaBaja,DateTime unaFechaReinicio )
        {
            mensaje = unMensaje;
            matricula = unaMatricula;
            fechaBaja = unaFechaBaja;
            fechaReinicio = unaFechaReinicio;
            llamadoDesdeBajaLogica = desdeBajaLogica;
            cargoDatosParaSuplantar = false;
            InitializeComponent();
        }

        private void Form7_Load(object sender, EventArgs e)
        {
            txtMensaje.Text = mensaje;
            txtMensaje.ForeColor = Color.White;
        }

        // Realiza la cancelación de pasajes
        private void botonCancelar_Click(object sender, EventArgs e)
        {
            SQLManager manager = new SQLManager().generarSP("CancelarPasajesEncomiendasAeronave")
                                                 .agregarStringSP("@Matricula", matricula)
                                                 .agregarFechaSP("@FechaBaja", fechaBaja);

            if(llamadoDesdeBajaLogica){
                manager = manager.agregarFechaNula("@FechaReinicio");
            }
            else{
                manager = manager.agregarFechaSP("@FechaReinicio",fechaReinicio);
            }

            manager.ejecutarSP();

            this.Close();
        }

        // Suplanta la aeronave, en caso de fallar abre el alta de aeronave
        private void botonSuplantar_Click(object sender, EventArgs e)
        {
            try
            {
                suplantarAeronave();
                MessageBox.Show("Los vuelos de la aeronave se han reemplazado exitosamente", "Suplantar aeronave", MessageBoxButtons.OK);
                this.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Advertencia", MessageBoxButtons.OK);
                darDeAltaNuevaAeronave();
                if (cargoDatosParaSuplantar)
                {
                    suplantarAeronave();
                    this.Close();
                }
            }
        }

        private void suplantarAeronave()
        {
            SQLManager sqlManager = new SQLManager().generarSP("SuplantarAeronave")
                                                    .agregarStringSP("@Matricula", matricula)
                                                    .agregarFechaSP("@FechaBaja", fechaBaja);
            if (llamadoDesdeBajaLogica)
                sqlManager = sqlManager.agregarFechaNula("@FechaReinicio");
            else
                sqlManager = sqlManager.agregarFechaSP("@FechaReinicio", fechaReinicio);

            sqlManager.ejecutarSP();
        }

        private void darDeAltaNuevaAeronave()
        {
            new Alta(true, this,matricula,fechaBaja).ShowDialog();
        }

    }
}
