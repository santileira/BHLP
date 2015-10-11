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
        

        public Alta()
        {
            InitializeComponent();          
        }

        /*private void cboCampo_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtCampo.Text = "";

            if (cboCampo.SelectedIndex != -1)
            {
                this.listado.campo = cboCampo.Text;
                button1.Enabled = true;
            }
            else
                button1.Enabled = false;
 
        }*/

        private void Alta_Load(object sender, EventArgs e)
        {
            this.iniciar();
        }
        /*
        public void borrarComboSeleccionar()
        {
            cboCampo.SelectedIndex = -1;
        }

        public void setFiltroSelector(string valor)
        {
            txtCampo.Text = valor;
        }

        public string getCampoSelector()
        {
            return cboCampo.Text;
        }*/

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

        private void button1_Click(object sender, EventArgs e)
        {
            /*this.listado.generarQueryInicial();
            this.listado.ejecutarConsulta();
            this.cambiarVisibilidades(this.listado);*/
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
                //HACER EL ALTA CON UNA STORE PROCEDURE, SI ESTA OK:
                MessageBox.Show("Todos los datos son correctos. Se procede a dar de alta a la nueva ruta", "Alta de nueva ruta", MessageBoxButtons.OK);
                       
            }

        }

        private bool datosCorrectos()
        {
            Boolean huboErrores = false;

            huboErrores = this.validarLongitudes() || huboErrores;
            huboErrores = this.validarTipos() || huboErrores;

            return !huboErrores;
        }

        private Boolean validarLongitudes()
        {
            Boolean algunoVacio = !this.seCompleto(txtCodigo, "Código");
            algunoVacio =  !this.seCompleto(txtCiudadDestino, "Ciudad de destino") || algunoVacio;
            algunoVacio =  !this.seCompleto(txtCiudadOrigen, "Ciudad de origen") || algunoVacio;
            algunoVacio =  !this.seCompleto(txtPrecioEncomienda, "Precio de encomienda") || algunoVacio;
            algunoVacio =  !this.seCompleto(txtPrecioPasaje, "Precio de pasaje") || algunoVacio;
            algunoVacio =  !this.seCompleto(cboServicio, "Tipo de servicio") || algunoVacio;

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
            Boolean huboError = !this.numeroCorrecto(txtCodigo, "Código",false);
            huboError = !this.textoCorrecto(txtCiudadDestino, "Ciudad de destino") || huboError;
            huboError = !this.textoCorrecto(txtCiudadOrigen, "Ciudad de origen") || huboError;

            huboError = !this.numeroCorrecto(txtPrecioEncomienda, "Precio de encomienda",true) || huboError;
            huboError = !this.numeroCorrecto(txtPrecioPasaje, "Precio de pasaje",true) || huboError;

            return huboError;
        }

        private Boolean textoCorrecto(TextBox txt, string campo)
        {
            if (txt.TextLength !=0 && !this.esTexto(txt))
            {
                MessageBox.Show("El campo " + campo + " debe ser un texto", "Error en los datos ingresados", MessageBoxButtons.OK);
                return false;
            }
            return true;
        }

        private Boolean numeroCorrecto(TextBox txt, string campo,bool debeSerDecimal)
        {
            if (txt.TextLength != 0)
            {
                if((debeSerDecimal && !esDecimal(txt)) || (!debeSerDecimal && !esNumero(txt))){

                MessageBox.Show("El campo " + campo + " debe ser un número", "Error en los datos ingresados", MessageBoxButtons.OK);
                return false;
                }
            }
            return true;
        }


        private void button6_Click(object sender, EventArgs e)
        {
            formularioSiguiente = new Principal();
            this.cambiarVisibilidades(formularioSiguiente);
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

        private void cargarComboServicio()
        {
            cboServicio.Items.Clear();

            SqlDataReader reader;
            SqlCommand consultaServicios = new SqlCommand();
            consultaServicios.CommandType = CommandType.Text;
            consultaServicios.CommandText = "SELECT SERV_COD FROM [ABSTRACCIONX4].SERVICIOS";
            consultaServicios.Connection = Program.conexion();

            reader = consultaServicios.ExecuteReader();

            while (reader.Read())
                this.cboServicio.Items.Add(reader.GetValue(0));

            reader.Close();
        }

    }
}
