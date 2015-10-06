using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AerolineaFrba.Abm_Rol
{
    public partial class Baja : Form
    {
        public Baja()
        {
            InitializeComponent();
        }

        private void Baja_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (lstFuncionalidadesAEliminar.SelectedIndex != -1)
                lstFuncionalidadesAEliminar.Items.RemoveAt(lstFuncionalidadesAEliminar.SelectedIndex);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string valor = lstFuncionalidadesTotales.Text;
            if (!lstFuncionalidadesAEliminar.Items.Contains(valor))
                lstFuncionalidadesAEliminar.Items.Add(lstFuncionalidadesTotales.Text);
        }
    }
}
