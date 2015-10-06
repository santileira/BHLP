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
    public partial class Modificacion : Form
    {
        public Modificacion()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.eliminarDeLaLista(lstFuncionalidadesActuales);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.eliminarDeLaLista(lstFuncionalidadesAEliminar);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.agregarALaLista(lstFuncionalidadesActuales, lstFuncionalidadesAEliminar);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.agregarALaLista(lstFuncionalidadesTotales, lstFuncionalidadesActuales);
        }

        private void eliminarDeLaLista(ListBox lista)
        {
            if (lista.SelectedIndex != -1)
                lista.Items.RemoveAt(lista.SelectedIndex);
        }

        private void agregarALaLista(ListBox lista1, ListBox lista2)
        {
            string valor = lista1.Text;
            if (!lista2.Items.Contains(valor))
                lista2.Items.Add(lista1.Text);
        }


    }
}
