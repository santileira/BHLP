using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AerolineaFrba.Abm_Ruta
{
    public partial class Alta : Form
    {

        public Listado listado;
        Form formularioSiguiente;
        bool seleccionandoOrigen;

        public Alta()
        {
            InitializeComponent();          
        }

        private void Alta_Load(object sender, EventArgs e)
        {
            this.iniciar();
        }

        private void iniciar()
        {
            
            this.cargarComboServicio();

            txtCiudadDestino.Text = "";
            txtCiudadOrigen.Text = "";
            txtCodigo.Text = "";
            txtPrecioEncomienda.Text = "";
            txtPrecioPasaje.Text = "";
            cboServicio.SelectedItem = -1;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.iniciar();
        }

        private void cambiarVisibilidades(Form formularioSiguiente)
        {
            formularioSiguiente.Visible = true;
            this.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.datosCorrectos())
            {
                try
                {
                    darDeAltaRuta();
                    MessageBox.Show("El alta de la ruta se realizó exitosamente.", "Alta de nueva ruta", MessageBoxButtons.OK);
                }
                catch
                {
                    MessageBox.Show("Ya existe una ruta de " + txtCiudadOrigen.Text + " a " + txtCiudadDestino.Text +
                    " con el código " + txtCodigo.Text.ToString() + " y servicio " + cboServicio.Text, 
                    "Advertencia", MessageBoxButtons.OK);
                    return;
                }
                this.cambiarVisibilidades(new Principal());
            }

        }

        private Object darDeAltaRuta()
        {
            SQLManager sqlManager = new SQLManager();
            return sqlManager.generarSP("AltaRuta")
                             .agregarIntSP("@Codigo", txtCodigo)
                             .agregarStringSP("@Servicio", cboServicio)
                             .agregarStringSP("@CiudadOrigen", txtCiudadOrigen)
                             .agregarStringSP("@CiudadDestino", txtCiudadDestino)
                             .agregarDecimalSP("@PrecioPasaje", enDecimal(txtPrecioPasaje.Text))
                             .agregarDecimalSP("@PrecioeEncomienda", enDecimal(txtPrecioEncomienda.Text))
                             .ejecutarSP();
        }

        private Decimal enDecimal(string numero)
        {
            return Decimal.Round(Convert.ToDecimal(numero.Replace(".", ",")), 2);
        }

        private bool datosCorrectos()
        {
            Boolean huboErrores = false;

            huboErrores = this.validarLongitudes() || huboErrores;
            huboErrores = this.validarTipos() || huboErrores;
            huboErrores = this.validarLimitesNumericos() || huboErrores;
            huboErrores = Validacion.igualdadCiudades(txtCiudadDestino , txtCiudadOrigen) || huboErrores;

            return !huboErrores;
        }

        

        private Boolean validarLongitudes()
        {
            Boolean algunoVacio = Validacion.esVacio(txtCodigo, "código" , true);
            algunoVacio = Validacion.esVacio(cboServicio, "tipo de servicio", true) || algunoVacio;
            algunoVacio = Validacion.esVacio(txtCiudadOrigen, "ciudad de origen", true) || algunoVacio;
            algunoVacio =  Validacion.esVacio(txtCiudadDestino, "ciudad de destino" , true) || algunoVacio;
            algunoVacio = Validacion.esVacio(txtPrecioPasaje, "precio de pasaje", true) || algunoVacio;
            algunoVacio =  Validacion.esVacio(txtPrecioEncomienda, "precio de encomienda" , true ) || algunoVacio;
            
            return algunoVacio;
        }

        private bool validarTipos()
        {
            Boolean huboErrores = false;

            huboErrores = Validacion.esNumero(txtCodigo,"código",true) || huboErrores;
            huboErrores = Validacion.esDecimal(txtPrecioPasaje,"precio de pasaje",true) || huboErrores;
            huboErrores = Validacion.esDecimal(txtPrecioEncomienda, "precio de encomienda", true) || huboErrores;

            return !huboErrores;
        }

        private bool validarLimitesNumericos()
        {
            Boolean huboErrores = false;

            huboErrores = Validacion.estaEntreLimites(txtCodigo, 1, 999999999,false, "código") || huboErrores;
            huboErrores = Validacion.estaEntreLimites(txtPrecioPasaje, 0.01m, 999,true, "precio de pasaje") || huboErrores;
            huboErrores = Validacion.estaEntreLimites(txtPrecioEncomienda, 0.01m, 999, true, "precio de encomienda") || huboErrores;

            return !huboErrores;
        }

        private void cargarComboServicio()
        {
            cboServicio.Items.Clear();

            SqlDataReader reader;
            SqlCommand consultaServicios = new SqlCommand();
            consultaServicios.CommandType = CommandType.Text;
            consultaServicios.CommandText = "SELECT SERV_DESC FROM [ABSTRACCIONX4].SERVICIOS";
            consultaServicios.Connection = Program.conexion();

            reader = consultaServicios.ExecuteReader();

            while (reader.Read())
                this.cboServicio.Items.Add(reader.GetValue(0));

            reader.Close();
        }

        private void botonSelOrigen_Click(object sender, EventArgs e)
        {
            seleccionandoOrigen = true;
            ListadoCiudades listado = new ListadoCiudades(this);
            listado.vieneDeAlta = true;
            cambiarVisibilidades(listado);
        }

        private void botonSelDestino_Click(object sender, EventArgs e)
        {
            seleccionandoOrigen = false;
            ListadoCiudades listado = new ListadoCiudades(this);
            listado.vieneDeAlta = true;
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

        

    }
}
