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
        public Form7(string unMensaje)
        {
            mensaje = unMensaje;
            InitializeComponent();
        }

        private void Form7_Load(object sender, EventArgs e)
        {
            txtMensaje.Text = mensaje;
        }

        private void botonCancelar_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
            this.Close();
        }

    }
}
