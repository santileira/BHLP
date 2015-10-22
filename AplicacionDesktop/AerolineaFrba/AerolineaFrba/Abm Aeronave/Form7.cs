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
        public Form7(string unMensaje,string unaMatricula,DateTime unaFechaBaja,DateTime unaFechaReinicio)
        {
            mensaje = unMensaje;
            matricula = unaMatricula;
            fechaBaja = unaFechaBaja;
            fechaReinicio = unaFechaReinicio;
            InitializeComponent();
        }

        private void Form7_Load(object sender, EventArgs e)
        {
            txtMensaje.Text = mensaje;
        }

        private void botonCancelar_Click(object sender, EventArgs e)
        {
            new SQLManager().generarSP("CancelarAeronaveFueraServicio")
                            .agregarStringSP("@Matricula",matricula)
                            .agregarFechaSP("@FechaBaja",fechaBaja)
                            .agregarFechaSP("@FechaReinicio",fechaReinicio)
                            .ejecutarSP();
            this.Close();
        }

    }
}
