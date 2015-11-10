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
    public partial class Modificacion : Form
    {
        Form formularioSiguiente;

        public Listado listado;
        bool seleccionandoOrigen;
        private int idRuta;

        public Modificacion()
        {
            InitializeComponent();
        }

        private void Modificacion_Load(object sender, EventArgs e)
        {
            this.iniciar();
        }

        public void seSelecciono(DataGridViewRow registro)
        {
            cargarComboServicio();

            idRuta = Convert.ToInt32(registro.Cells["Id"].Value);

            txtCodigo.Text = registro.Cells["Código Ruta"].Value.ToString();
            cboServicio.Text = txtTipoDeServicio.Text = registro.Cells["Descripción"].Value.ToString();
            txtCiudadOrigenNueva.Text = txtCiudadOrigen.Text = registro.Cells["Origen"].Value.ToString();
            txtCiudadDestinoNueva.Text = txtCiudadDestino.Text = registro.Cells["Destino"].Value.ToString();
            txtPrecioEncomiendaNueva.Text = txtPrecioEncomienda.Text = registro.Cells["Precio Base Por Kilogramo"].Value.ToString();
            txtPrecioPasajeNuevo.Text = txtPrecioPasaje.Text = registro.Cells["Precio Base Pasaje"].Value.ToString();

            Boolean viajeProgramado = !tieneViajeProgramado(idRuta);

            botonSelOrigen.Enabled =
            botonSelDestino.Enabled =
            txtTipoDeServicio.Enabled =
            txtPrecioEncomienda.Enabled =
            txtPrecioPasaje.Enabled =
            txtCiudadDestino.Enabled =
            txtCiudadOrigen.Enabled =
            txtPrecioEncomiendaNueva.Enabled =
            txtPrecioPasajeNuevo.Enabled =
            txtCiudadOrigenNueva.Enabled =
            cboServicio.Enabled =
            txtCiudadDestinoNueva.Enabled = viajeProgramado;


            txtCodigo.Enabled = true;
            botonLimpiar.Enabled = true;
            botonGuardar.Enabled = true;
            
            
        }

        private Boolean tieneViajeProgramado(int idRuta)
        {
            SqlCommand command = new SqlCommand();
            command.Connection = Program.conexion();
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = "SELECT ABSTRACCIONX4.TieneViajeProgramado(@IdRuta)";
            command.CommandTimeout = 0;

            command.Parameters.AddWithValue("@IdRuta", idRuta);

            return (Boolean)command.ExecuteScalar();
        }



        public void seSelecciono(string ciudad)
        {
            if (seleccionandoOrigen)
            {
                txtCiudadOrigenNueva.Text = ciudad;
            }
            else
            {
                txtCiudadDestinoNueva.Text = ciudad;
            }
        }

        private void iniciar()
        {
          this.cargarComboServicio();

          txtCiudadDestino.Text = "";
          txtCiudadOrigen.Text = "";
          txtCodigo.Text = "";
          txtPrecioEncomienda.Text = "";
          txtPrecioPasaje.Text = "";
          txtTipoDeServicio.Text = "";
          txtCiudadDestinoNueva.Text = "";
          txtCiudadOrigenNueva.Text = "";
          txtPrecioEncomiendaNueva.Text = "";
          txtPrecioPasajeNuevo.Text = "";
          cboServicio.SelectedIndex = -1;
             
          txtCodigo.Enabled = false;
          txtTipoDeServicio.Enabled = false;
          txtPrecioEncomienda.Enabled = false;
          txtPrecioPasaje.Enabled = false;
          txtPrecioEncomiendaNueva.Enabled = false;
          txtPrecioPasajeNuevo.Enabled = false;
          txtCiudadOrigen.Enabled = false;
          txtCiudadOrigenNueva.Enabled = false;
          txtCiudadDestino.Enabled = false;
          txtCiudadDestinoNueva.Enabled = false;

          botonSelOrigen.Enabled = false;
          botonSelDestino.Enabled = false;
          botonLimpiar.Enabled = false;
          botonGuardar.Enabled = false;

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

        private void cambiarVisibilidades(Form formularioSiguiente)
        {
            formularioSiguiente.Visible = true;
            this.Visible = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.iniciar();
            this.listado.llamadoDeModificacion = true;
            this.cambiarVisibilidades(this.listado);
        }

        private void botonLimpiar_Click(object sender, EventArgs e)
        {
            
            this.iniciar();
        }

        private void botonGuardar_Click(object sender, EventArgs e)
        {
            if (this.datosCorrectos())
            {
                MessageBox.Show("Todos los datos son correctos. Se procede a modificar el registro de aeronave", "Alta de nueva aeronave", MessageBoxButtons.OK);
                modificarRuta();
                (listado as Listado).inicio();
                this.cambiarVisibilidades(this.listado);
            }
        }

        private Object modificarRuta()
        {
            SQLManager sqlManager = new SQLManager();
            return sqlManager.generarSP("ModificarRuta").agregarIntSP("@IdRuta", idRuta)
                                                        .agregarIntSP("@Codigo", txtCodigo)
                                                        .agregarStringSP("@Servicio", cboServicio)
                                                        .agregarStringSP("@CiudadOrigen", txtCiudadOrigenNueva)
                                                        .agregarStringSP("@CiudadDestino", txtCiudadDestinoNueva)
                                                        .agregarDecimalSP("@PrecioPasaje", enDecimal(txtPrecioPasajeNuevo.Text))
                                                        .agregarDecimalSP("@PrecioeEncomienda", enDecimal(txtPrecioEncomiendaNueva.Text))
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
            huboErrores = Validacion.igualdadCiudades(txtCiudadOrigenNueva, txtCiudadDestinoNueva) || huboErrores;

            return !huboErrores;
        }

        private Boolean validarLongitudes()
        {

            Boolean algunoVacio = Validacion.esVacio(txtCodigo, "código", true);
            algunoVacio = Validacion.esVacio(cboServicio, "tipo de servicio", true) || algunoVacio;
            algunoVacio = Validacion.esVacio(txtCiudadOrigenNueva, "ciudad de origen", true) || algunoVacio;
            algunoVacio = Validacion.esVacio(txtCiudadDestinoNueva, "ciudad de destino", true) || algunoVacio;
            algunoVacio = Validacion.esVacio(txtPrecioPasajeNuevo, "precio de pasaje", true) || algunoVacio;
            algunoVacio = Validacion.esVacio(txtPrecioEncomiendaNueva, "precio de encomienda", true) || algunoVacio;

            return algunoVacio;
        }

        private Boolean validarTipos()
        {
            Boolean huboErrores = false;

            huboErrores = Validacion.esNumero(txtCodigo, "código", true) || huboErrores;
            huboErrores = Validacion.esDecimal(txtPrecioPasajeNuevo, "precio de pasaje", true) || huboErrores;
            huboErrores = Validacion.esDecimal(txtPrecioEncomiendaNueva, "precio de encomienda", true) || huboErrores;

            return huboErrores;
        }

        private bool validarLimitesNumericos()
        {
            Boolean huboErrores = false;

            huboErrores = Validacion.estaEntreLimites(txtCodigo, 1, 999999999, false, "código") || huboErrores;
            huboErrores = Validacion.estaEntreLimites(txtPrecioPasajeNuevo, 0.01m, 999, true, "precio de pasaje") || huboErrores;
            huboErrores = Validacion.estaEntreLimites(txtPrecioEncomiendaNueva, 0.01m, 999, true, "precio de encomienda") || huboErrores;

            return !huboErrores;
        }

        private void botonSelOrigen_Click(object sender, EventArgs e)
        {
            seleccionandoOrigen = true;
            ListadoCiudades listado = new ListadoCiudades(this);
            listado.vieneDeModificacion = true;
            cambiarVisibilidades(listado);
        }

        private void botonSelDestino_Click(object sender, EventArgs e)
        {
            seleccionandoOrigen = false;
            ListadoCiudades listado = new ListadoCiudades(this);
            listado.vieneDeModificacion = true;
            cambiarVisibilidades(listado);
        }

    }
}
