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
            
            txtCodigo.Text = registro.Cells["CODIGO_DE_RUTA"].Value.ToString();
            txtTipoDeServicio.Text = registro.Cells["TIPO_SERVICIO"].Value.ToString();
            txtCiudadOrigen.Text = registro.Cells["ORIGEN"].Value.ToString();
            txtCiudadDestino.Text = registro.Cells["DESTINO"].Value.ToString();
            txtPrecioEncomienda.Text = registro.Cells["RUTA_PRECIO_BASE_KG"].Value.ToString();
            txtPrecioPasaje.Text = registro.Cells["RUTA_PRECIO_BASE_PASAJE"].Value.ToString();

            txtCodigo.Enabled = true;
            txtTipoDeServicio.Enabled = true;
            txtPrecioEncomienda.Enabled = true;
            txtPrecioPasaje.Enabled = true;
            txtCiudadDestino.Enabled = true;
            txtCiudadOrigen.Enabled = true;



            botonSelOrigen.Enabled = true;
            botonSelDestino.Enabled = true;
            botonLimpiar.Enabled = true;
            botonGuardar.Enabled = true;
            
            
        }

        public void seSelecciono(string ciudad)
        {
            if (seleccionandoOrigen)
            {
                txtCiudadOrigen1.Text = ciudad;
            }
            else
            {
                txtCiudadDestino1.Text = ciudad;
            }
        }

        private void iniciar()
        {
          this.cargarComboServicio();

          txtCiudadDestino1.Text = "";
          txtCiudadOrigen1.Text = "";
          txtCodigo.Text = "";
          txtPrecioEncomienda.Text = "";
          txtPrecioPasaje.Text = "";
          txtTipoDeServicio.Text = ""; ;

          txtCodigo.Enabled = false;
          txtTipoDeServicio.Enabled = false;
          txtPrecioEncomienda.Enabled = false;
          txtPrecioPasaje.Enabled = false;

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
                //HACER EL ALTA CON UNA STORE PROCEDURE            
            }
        }



        private bool datosCorrectos()
        {
            Boolean huboErrores = false;

            //huboErrores = this.validarLongitudes() || huboErrores;
            huboErrores = this.validarTipos() || huboErrores;
            huboErrores = this.validarIgualdadCiudades() || huboErrores;

            return !huboErrores;
        }

        private Boolean validarLongitudes()
        {
            Boolean algunoVacio = !this.seCompleto(txtCodigo, "Código");
            algunoVacio = !this.seCompleto(txtCiudadDestino1, "Ciudad de destino") || algunoVacio;
            algunoVacio = !this.seCompleto(txtCiudadOrigen1, "Ciudad de origen") || algunoVacio;
            algunoVacio = !this.seCompleto(txtPrecioEncomienda1, "Precio de encomienda") || algunoVacio;
            algunoVacio = !this.seCompleto(txtPrecioPasaje1, "Precio de pasaje") || algunoVacio;
            algunoVacio = !this.seCompleto(cboServicio, "Tipo de servicio") || algunoVacio;

            return algunoVacio;
        }

        private Boolean seCompleto(TextBox txt, string campo)
        {
            if (txt.TextLength == 0)
            {
                MessageBox.Show("El campo " + campo + " no puede estar vacio", "Error en los datos de entrada", MessageBoxButtons.OK);
                return false;
            }
            return true;
        }

        private Boolean seCompleto(ComboBox cbo, string campo)
        {
            if (cbo.SelectedIndex == -1)
            {
                MessageBox.Show("El campo " + campo + " no puede estar vacio", "Error en los datos de entrada", MessageBoxButtons.OK);
                return false;
            }
            return true;
        }

        private Boolean validarTipos()
        {
            Boolean huboError = !this.numeroCorrecto(txtCodigo, "Código", false);

            huboError = !this.numeroCorrecto(txtPrecioEncomienda1, "Precio de encomienda", true) || huboError;
            huboError = !this.numeroCorrecto(txtPrecioPasaje1, "Precio de pasaje", true) || huboError;

            return huboError;
        }

        private Boolean textoCorrecto(TextBox txt, string campo)
        {
            if (txt.TextLength != 0 && !this.esTexto(txt))
            {
                MessageBox.Show("El campo " + campo + " debe ser un texto", "Error en los datos ingresados", MessageBoxButtons.OK);
                return false;
            }
            return true;
        }

        private Boolean numeroCorrecto(TextBox txt, string campo, bool debeSerDecimal)
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
        }

        private Boolean validarIgualdadCiudades()
        {
            if (txtCiudadDestino1.TextLength * txtCiudadOrigen1.TextLength != 0)
            {
                if (txtCiudadOrigen1.Text == txtCiudadDestino1.Text)
                {
                    MessageBox.Show("La ciudad de origen debe ser distinta a la de destino", "Error en los datos ingresados", MessageBoxButtons.OK);
                    return true;
                }
            }
            return false;
        }

        private Boolean esTexto(TextBox txt)
        {
            String textPattern = "[A-Za-z]";
            System.Text.RegularExpressions.Regex regexTexto = new System.Text.RegularExpressions.Regex(textPattern);

            return regexTexto.IsMatch(txt.Text);
        }

        private Boolean esNumero(TextBox txt)
        {
            int numero;
            return int.TryParse(txt.Text, out numero);
        }

        private Boolean esDecimal(TextBox txt)
        {
            decimal unDecimal;
            return decimal.TryParse(txt.Text, out unDecimal);
        }

        private void botonSelOrigen_Click(object sender, EventArgs e)
        {
            seleccionandoOrigen = true;
            ListadoCiudades listado = new ListadoCiudades(this);
            listado.vieneDeAlta = false;
            cambiarVisibilidades(listado);
        }

        private void botonSelDestino_Click(object sender, EventArgs e)
        {
            seleccionandoOrigen = false;
            ListadoCiudades listado = new ListadoCiudades(this);
            listado.vieneDeAlta = false;
            cambiarVisibilidades(listado);
        }

        private void cboServicio_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

    }
}
