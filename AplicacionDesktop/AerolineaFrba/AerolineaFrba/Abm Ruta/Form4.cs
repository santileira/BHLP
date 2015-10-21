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

            idRuta = Convert.ToInt32(registro.Cells["RUTA_ID"].Value);
            
            txtCodigo.Text = registro.Cells["CODIGO_DE_RUTA"].Value.ToString();
            cboServicio.Text = txtTipoDeServicio.Text = registro.Cells["TIPO_SERVICIO"].Value.ToString();
            txtCiudadOrigenNueva.Text = txtCiudadOrigen.Text = registro.Cells["ORIGEN"].Value.ToString();
            txtCiudadDestinoNueva.Text = txtCiudadDestino.Text = registro.Cells["DESTINO"].Value.ToString();
            txtPrecioEncomiendaNueva.Text = txtPrecioEncomienda.Text = registro.Cells["RUTA_PRECIO_BASE_KG"].Value.ToString();
            txtPrecioPasajeNuevo.Text = txtPrecioPasaje.Text = registro.Cells["RUTA_PRECIO_BASE_PASAJE"].Value.ToString();

            txtCodigo.Enabled = true;
            txtTipoDeServicio.Enabled = true;
            txtPrecioEncomienda.Enabled = true;
            txtPrecioPasaje.Enabled = true;
            txtCiudadDestino.Enabled = true;
            txtCiudadOrigen.Enabled = true;
            txtPrecioEncomiendaNueva.Enabled = true;
            txtPrecioPasajeNuevo.Enabled = true;
            txtCiudadOrigenNueva.Enabled = false;
            txtCiudadDestinoNueva.Enabled = false;


            botonSelOrigen.Enabled = true;
            botonSelDestino.Enabled = true;
            botonLimpiar.Enabled = true;
            botonGuardar.Enabled = true;
            
            
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

        private void button6_Click(object sender, EventArgs e)
        {
            formularioSiguiente = new Principal();
            this.iniciar();
            this.cambiarVisibilidades(formularioSiguiente);
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
            }
        }

        private Object modificarRuta()
        {
            SqlCommand command = new SqlCommand();
            command.Connection = Program.conexion();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "[GD2C2015].[ABSTRACCIONX4].[ModificarRuta]";
            command.CommandTimeout = 0;

            command.Parameters.AddWithValue("@IdRuta", idRuta);
            command.Parameters.AddWithValue("@Codigo", Convert.ToInt32(txtCodigo.Text));
            command.Parameters.AddWithValue("@Servicio", cboServicio.Text);
            command.Parameters.AddWithValue("@CiudadOrigen", txtCiudadOrigenNueva.Text);
            command.Parameters.AddWithValue("@CiudadDestino", txtCiudadDestinoNueva.Text);
            command.Parameters.AddWithValue("@PrecioPasaje", enDecimal(txtPrecioPasajeNuevo.Text));
            command.Parameters.AddWithValue("@PrecioeEncomienda", enDecimal(txtPrecioEncomiendaNueva.Text));

            return command.ExecuteScalar();
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
            huboErrores = Validacion.igualdadCiudades(txtCiudadDestino, txtCiudadOrigen) || huboErrores;

            return !huboErrores;
        }

        private Boolean validarLongitudes()
        {

            Boolean algunoVacio = Validacion.esVacio(txtCodigo, "código", true);
            algunoVacio = Validacion.esVacio(txtCiudadDestino, "ciudad de destino", true) || algunoVacio;
            algunoVacio = Validacion.esVacio(txtCiudadOrigen, "ciudad de origen", true) || algunoVacio;
            algunoVacio = Validacion.esVacio(txtPrecioEncomienda, "precio de encomienda", true) || algunoVacio;
            algunoVacio = Validacion.esVacio(txtPrecioPasaje, "precio de pasaje", true) || algunoVacio;
            algunoVacio = Validacion.esVacio(cboServicio, "tipo de servicio", true) || algunoVacio;

            return algunoVacio;
        }

        /*private Boolean seCompleto(TextBox txt, string campo)
        {
            if (txt.TextLength == 0)
            {
                MessageBox.Show("El campo " + campo + " no puede estar vacio", "Error en los datos de entrada", MessageBoxButtons.OK);
                return false;
            }
            return true;
        }*/

       /* private Boolean seCompleto(ComboBox cbo, string campo)
        {
            if (cbo.SelectedIndex == -1)
            {
                MessageBox.Show("El campo " + campo + " no puede estar vacio", "Error en los datos de entrada", MessageBoxButtons.OK);
                return false;
            }
            return true;
        }*/

        private Boolean validarTipos()
        {
            Boolean huboError = !Validacion.numeroCorrecto(txtCodigo, "código", false);

            huboError = !Validacion.numeroCorrecto(txtPrecioEncomienda, "precio de encomienda", true) || huboError;
            huboError = !Validacion.numeroCorrecto(txtPrecioPasaje, "Pprecio de pasaje", true) || huboError;

            return huboError;
        }
        
        /*private Boolean textoCorrecto(TextBox txt, string campo)
        {
            if (txt.TextLength != 0 && !this.esTexto(txt))
            {
                MessageBox.Show("El campo " + campo + " debe ser un texto", "Error en los datos ingresados", MessageBoxButtons.OK);
                return false;
            }
            return true;
        }*/

        /*private Boolean numeroCorrecto(TextBox txt, string campo, bool debeSerDecimal)
        {
            if (txt.TextLength != 0)
            {
                if ((debeSerDecimal && !esDecimal(txt)) || (!debeSerDecimal && !esNumero(txt)))
                {

                    MessageBox.Show("El campo " + campo + " debe ser un número", "Error en los datos ingresados", MessageBoxButtons.OK);
                    return false;
                }
            }
            return true;
        }*/

        /*private Boolean validarIgualdadCiudades()
        {
            if (txtCiudadDestinoNueva.TextLength * txtCiudadOrigenNueva.TextLength != 0)
            {
                if (txtCiudadOrigenNueva.Text == txtCiudadDestinoNueva.Text)
                {
                    MessageBox.Show("La ciudad de origen debe ser distinta a la de destino", "Error en los datos ingresados", MessageBoxButtons.OK);
                    return true;
                }
            }
            return false;
        }*/

        /*private Boolean esTexto(TextBox txt)
        {
            String textPattern = "[A-Za-z]";
            System.Text.RegularExpressions.Regex regexTexto = new System.Text.RegularExpressions.Regex(textPattern);

            return regexTexto.IsMatch(txt.Text);
        }*/

        /*private Boolean esNumero(TextBox txt)
        {
            int numero;
            return int.TryParse(txt.Text, out numero);
        }*/

        /*private Boolean esDecimal(TextBox txt)
        {
            decimal unDecimal;
            return decimal.TryParse(txt.Text, out unDecimal);
        }*/

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
