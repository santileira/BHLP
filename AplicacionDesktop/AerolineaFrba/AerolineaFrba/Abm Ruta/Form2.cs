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
                MessageBox.Show("Todos los datos son correctos. Se procede a dar de alta a la nueva ruta", "Alta de nueva ruta", MessageBoxButtons.OK);
                darDeAltaRuta();
            }

        }

        private Object darDeAltaRuta()
        {
            SQLManager sqlManager = new SQLManager();
            return sqlManager.generarSP("AltaRuta").agregarIntSP("@Codigo", txtCodigo).agregarStringSP("@Servicio", cboServicio).
            agregarStringSP("@CiudadOrigen", txtCiudadOrigen).agregarStringSP("@CiudadDestino", txtCiudadDestino).
            agregarDecimalSP("@PrecioPasaje", enDecimal(txtPrecioPasaje.Text)).agregarDecimalSP("@PrecioeEncomienda", enDecimal(txtPrecioEncomienda.Text)).ejecutarSP();
            
            /*SqlCommand command = new SqlCommand();
            command.Connection = Program.conexion();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "[GD2C2015].[ABSTRACCIONX4].[AltaRuta]";
            command.CommandTimeout = 0;


            command.Parameters.AddWithValue("@Codigo", Convert.ToInt32(txtCodigo.Text));
            command.Parameters.AddWithValue("@Servicio", cboServicio.Text);
            command.Parameters.AddWithValue("@CiudadOrigen", txtCiudadOrigen.Text);
            command.Parameters.AddWithValue("@CiudadDestino", txtCiudadDestino.Text);
            command.Parameters.AddWithValue("@PrecioPasaje", enDecimal(txtPrecioPasaje.Text));
            command.Parameters.AddWithValue("@PrecioeEncomienda", enDecimal(txtPrecioEncomienda.Text));

            return command.ExecuteScalar();*/
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
            huboErrores = Validacion.igualdadCiudades(txtCiudadDestino , txtCiudadOrigen) || huboErrores;

            return !huboErrores;
        }

        private Boolean validarLongitudes()
        {
            Boolean algunoVacio = Validacion.esVacio(txtCodigo, "código" , true);
            algunoVacio =  Validacion.esVacio(txtCiudadDestino, "ciudad de destino" , true) || algunoVacio;
            algunoVacio =  Validacion.esVacio(txtCiudadOrigen, "ciudad de origen" , true) || algunoVacio;
            algunoVacio =  Validacion.esVacio(txtPrecioEncomienda, "precio de encomienda" , true ) || algunoVacio;
            algunoVacio =  Validacion.esVacio(txtPrecioPasaje, "precio de pasaje" , true) || algunoVacio;
            algunoVacio =  Validacion.esVacio(cboServicio, "tipo de servicio" , true) || algunoVacio;

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

        /*private Boolean seCompleto(ComboBox cbo, string campo)
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
            Boolean huboError = !Validacion.numeroCorrecto(txtCodigo, "código",false);

            huboError = !Validacion.numeroCorrecto(txtPrecioEncomienda, "precio de encomienda",true) || huboError;
            huboError = !Validacion.numeroCorrecto(txtPrecioPasaje, "Pprecio de pasaje",true) || huboError;

            return huboError;
        }

        /*private Boolean textoCorrecto(TextBox txt, string campo)
        {
            if (txt.TextLength !=0 && !this.esTexto(txt))
            {
                MessageBox.Show("El campo " + campo + " debe ser un texto", "Error en los datos ingresados", MessageBoxButtons.OK);
                return false;
            }
            return true;
        }*/

        /*private Boolean numeroCorrecto(TextBox txt, string campo = "Opcional" ,bool debeSerDecimal = false)
        {
            if (!Validacion.esVacio(txt))
            {
                if((debeSerDecimal && !esDecimal(txt)) || (!debeSerDecimal && !esNumero(txt))){

                MessageBox.Show("El campo " + campo + " debe ser un número", "Error en los datos ingresados", MessageBoxButtons.OK);
                return false;
                }
            }
            return true;
        }*/

       /*private Boolean validarIgualdadCiudades()
        {
            if (txtCiudadDestino.TextLength * txtCiudadOrigen.TextLength != 0)
            {
                if (txtCiudadOrigen.Text == txtCiudadDestino.Text)
                {
                    MessageBox.Show("La ciudad de origen debe ser distinta a la de destino", "Error en los datos ingresados", MessageBoxButtons.OK);
                    return true;
                }
            }
            return false;
        }*/

        private void button6_Click(object sender, EventArgs e)
        {
            formularioSiguiente = new Principal();
            this.cambiarVisibilidades(formularioSiguiente);
        }

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
        }

        private Boolean esDecimal(TextBox txt)
        {
            decimal unDecimal;
            return decimal.TryParse(txt.Text, out unDecimal); 
        }*/

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
