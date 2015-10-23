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

        public Form7(string unMensaje,string unaMatricula,Boolean desdeBajaLogica,DateTime unaFechaBaja,DateTime unaFechaReinicio )
        {
            mensaje = unMensaje;
            matricula = unaMatricula;
            fechaBaja = unaFechaBaja;
            fechaReinicio = unaFechaReinicio;
            llamadoDesdeBajaLogica = desdeBajaLogica;
            InitializeComponent();
        }

        private void Form7_Load(object sender, EventArgs e)
        {
            txtMensaje.Text = mensaje;
        }

        private void botonCancelar_Click(object sender, EventArgs e)
        {
            ; // para comprar con null no encontre otra cosa
            if (llamadoDesdeBajaLogica)
            {
                new SQLManager().generarSP("CancelarAeronaveBaja")
                            .agregarStringSP("@Matricula", matricula)
                            .agregarFechaSP("@FechaBaja", fechaBaja)
                            .ejecutarSP();
            }
            else
            {
                new SQLManager().generarSP("CancelarAeronaveFueraServicio")
                            .agregarStringSP("@Matricula", matricula)
                            .agregarFechaSP("@FechaBaja", fechaBaja)
                            .agregarFechaSP("@FechaReinicio", fechaReinicio)
                            .ejecutarSP();
            }
            
            this.Close();
        }

        private void botonSuplantar_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Entre a suplantar", "OK", MessageBoxButtons.OK);
            SQLManager sqlManager = new SQLManager().generarSP("SuplantarAeronave").agregarStringSP("@Matricula", matricula)
            .agregarFechaSP("@FechaBaja", fechaBaja);
            if (llamadoDesdeBajaLogica)
                sqlManager.agregarStringSP("@FechaReinicio",(string) null).ejecutarSP();
            else
                sqlManager.agregarFechaSP("@FechaReinicio", fechaReinicio).ejecutarSP();
        }

    }
}
