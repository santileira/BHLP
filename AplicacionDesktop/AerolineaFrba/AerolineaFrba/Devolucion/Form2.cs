using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AerolineaFrba.Devolucion
{
    public partial class Form2 : Form
    {
        public string Motivo;
        public Form2()
        {
            
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Validacion.esTexto(textBox1, "motivo", true))
            {
                Motivo = textBox1.Text;
                this.Close();
            } 
       }
    }
}
