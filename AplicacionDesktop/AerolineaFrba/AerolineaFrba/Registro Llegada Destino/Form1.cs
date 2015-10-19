using AerolineaFrba.Abm_Ruta;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AerolineaFrba.Registro_Llegada_Destino
{
    public partial class Form1 : Form
    {

        bool seleccionandoOrigen;

        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            seleccionandoOrigen = true;
            ListadoCiudades listado = new ListadoCiudades(this);
            listado.vieneDeArribo = true;
            cambiarVisibilidades(listado);
        }

        private void cambiarVisibilidades(Form formularioSiguiente)
        {
            formularioSiguiente.Visible = true;
            this.Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            seleccionandoOrigen = false;
            ListadoCiudades listado = new ListadoCiudades(this);
            listado.vieneDeArribo = true;
            cambiarVisibilidades(listado);
        }

        public void seSelecciono(string ciudad)
        {
            if (seleccionandoOrigen)
            {
                txtCiudadOrigen.Text = ciudad;
            }
            else
            {
                txtCiudadDestino.Text = ciudad;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Abm_Aeronave.Listado listado = new Abm_Aeronave.Listado();
            listado.anterior = this;
            listado.seSeteaQuery = true;
            cambiarVisibilidades(listado);

        }

        public String consultaSeteada()
        {
            return "SELECT a.AERO_MATRI,AERO_MOD,AERO_FAB,SERV_DESC,AERO_CANT_BUTACAS,AERO_CANT_KGS from ABSTRACCIONX4.AERONAVES a JOIN ABSTRACCIONX4.SERVICIOS s ON (a.SERV_COD = s.SERV_COD) JOIN ABSTRACCIONX4.VIAJES v ON (a.AERO_MATRI = v.AERO_MATRI) WHERE v.VIAJE_FECHA_LLEGADA = NULL";
        }
    }
}
