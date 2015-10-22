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
    public partial class Principal : Form
    {
        Form formularioSiguiente;

        public Principal()
        {
            InitializeComponent();
        }
        
       
        private void cambiarVisibilidades(Form formularioSiguiente)
        {
            formularioSiguiente.Visible = true;
            this.Visible = false;
        }

        private void Principal_Load(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            formularioSiguiente = new Menu();
            this.cambiarVisibilidades(formularioSiguiente);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Alta alta = new Alta();
           
            this.cambiarVisibilidades(alta);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Baja baja = new Baja();

            this.cambiarVisibilidades(baja);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Modificacion modificacion = new Modificacion();
            Listado listado = new Listado();

            Principal principal = new Principal();

            listado.loActivoModificar = true;

            modificacion.listado = listado;
            listado.anterior = principal;
            listado.siguiente = modificacion;
            listado.llamadoDesdeModificacion = false;
            listado.llamadoDesdeModificacionSeleccionar = true;

            this.cambiarVisibilidades(listado);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Listado listado = new Listado();

            this.cambiarVisibilidades(listado);
        }


    }
}
