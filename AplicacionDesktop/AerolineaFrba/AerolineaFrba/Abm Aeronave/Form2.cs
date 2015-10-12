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

namespace AerolineaFrba.Abm_Aeronave
{
    public partial class Alta : Form
    {

        public Listado listado;
        Form formularioSiguiente;
        TextBox txtSeleccionar;
        string campoSeleccionar;
        

        public Alta()
        {
            InitializeComponent();

            //
            // Carga del contenido de combos
            //

            SqlDataReader variable;
            SqlCommand consultaColumnas = new SqlCommand();
            consultaColumnas.CommandType = CommandType.Text;
            consultaColumnas.CommandText = "SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'AERONAVES'";
            consultaColumnas.Connection = Program.conexion();

            variable = consultaColumnas.ExecuteReader();

            while (variable.Read())
                this.cboCampo.Items.Add(variable.GetValue(0));
            
        }

        private void cboCampo_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtCampo.Text = "";

            if (cboCampo.SelectedIndex != -1)
            {
                this.listado.campo = cboCampo.Text;
                button1.Enabled = true;
            }
            else
                button1.Enabled = false;
 
        }

        private void Alta_Load(object sender, EventArgs e)
        {
            this.inicio();
        }

        public void borrarComboSeleccionar()
        {
            cboCampo.SelectedIndex = -1;
        }

        public void setFiltroSelector(string valor)
        {
            txtSeleccionar.Text = valor;
        }

        public string getCampoSelector()
        {
            return campoSeleccionar;
        }

        private void inicio()
        {
            txtCampo.Text = "";
            txtFabricante.Text = "";
            txtMatricula.Text = "";
            txtModelo.Text = "";
            txtModelo.Focus();
            txtServicio.Text = "";
            txtButacas.Text = "";
            txtKilos.Text = "";
            txtBajaFS.Text = "";
            txtBajaVU.Text = "";
            txtBD.Text = "";
            txtFS.Text = "";
            txtRS.Text = "";
                        
            button1.Enabled = false;


        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.inicio();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtSeleccionar = txtCampo;
            campoSeleccionar = cboCampo.Text;
            this.listado.generarQueryInicial();
            this.listado.ejecutarConsulta();
            this.cambiarVisibilidades(this.listado);
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
                MessageBox.Show("Todos los datos son correctos. Se procede a dar de alta a la nueva aeronave", "Alta de nueva aeronave", MessageBoxButtons.OK);
                //HACER EL ALTA CON UNA STORE PROCEDURE            
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
            Boolean algunoVacio = !this.seCompleto(txtModelo, "Modelo");
            algunoVacio =  !this.seCompleto(txtMatricula, "Matricula") || algunoVacio;
            algunoVacio =  !this.seCompleto(txtFabricante, "Fabricante") || algunoVacio;
            algunoVacio =  !this.seCompleto(txtServicio, "Tipo de servicio") || algunoVacio;
            algunoVacio =  !this.seCompleto(txtButacas, "Cantidad de butacas") || algunoVacio;
            algunoVacio =  !this.seCompleto(txtKilos, "Cantidad de kilos") || algunoVacio;

            return algunoVacio;
        }

        private Boolean seCompleto(TextBox txt, string campo)
        {
            if (txt.TextLength == 0)
            {
                MessageBox.Show("El campo " + campo + " no puede estar vacio", "Error en los datos de entrada", MessageBoxButtons.OK);
                return false;
            }
            else
                return true;
        }

        private Boolean validarTipos()
        {
            Boolean huboError = false;

            if(txtModelo.TextLength != 0 && !this.esTexto(txtModelo))
            {
                MessageBox.Show("El campo modelo debe ser de tipo texto", "Error en los tipos de entrada", MessageBoxButtons.OK);
                huboError = true;
            }

            if(txtMatricula.TextLength != 0 && !this.esTexto(txtMatricula))
            {
                MessageBox.Show("El campo matricula debe ser de tipo texto", "Error en los tipos de entrada", MessageBoxButtons.OK);
                huboError = true;
            }

            if (txtFabricante.TextLength != 0 && !this.esTexto(txtFabricante))
            {
                MessageBox.Show("El campo fabricante debe ser de tipo texto", "Error en los tipos de entrada", MessageBoxButtons.OK);
                huboError = true;
            }

            if (txtServicio.TextLength != 0 && !this.esTexto(txtServicio))
            {
                MessageBox.Show("El campo tipo de servicio debe ser de tipo texto", "Error en los tipos de entrada", MessageBoxButtons.OK);
                huboError = true;
            }

            if (txtButacas.TextLength != 0 && !this.esNumero(txtButacas))
            {
                MessageBox.Show("El campo butacas debe ser de tipo numerico", "Error en los tipos de entrada", MessageBoxButtons.OK);
                huboError = true;
            }

            if (txtKilos.TextLength != 0 && !this.esNumero(txtKilos))
            {
                MessageBox.Show("El campo kilos debe ser de tipo numerico", "Error en los tipos de entrada", MessageBoxButtons.OK);
                huboError = true;
            }

            if (txtKilos.TextLength != 0 && !this.esNumero(txtKilos))
            {
                MessageBox.Show("El campo kilos debe ser de tipo numerico", "Error en los tipos de entrada", MessageBoxButtons.OK);
                huboError = true;
            }

            huboError = fechasErroneas(txtAlta, "Fecha de alta") || huboError;
            huboError = fechasErroneas(txtBajaFS, "Baja por fuera de servicio") || huboError;
            huboError = fechasErroneas(txtBajaVU, "Baja por vida util") || huboError;
            huboError = fechasErroneas(txtBD, "Baja definitiva") || huboError;
            huboError = fechasErroneas(txtFS, "Fuera de servicio") || huboError;
            huboError = fechasErroneas(txtRS, "Reinicio de servicio") || huboError;

            return huboError;

        }

        private Boolean fechasErroneas(TextBox txt, string campo)
        {
            
            DateTime fecha;
            if (txt.TextLength != 0 && (!DateTime.TryParse(txt.Text, out fecha) || fecha < DateTime.Now.Date))
            {
                MessageBox.Show("El campo " + campo + " debe ser de tipo Date, y una fecha posterior a la fecha actual", "Error en los tipos de entrada", MessageBoxButtons.OK);
                return true;
            }

            return false;
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
            String numericPattern = "[0-9]";
            System.Text.RegularExpressions.Regex regexNumero = new System.Text.RegularExpressions.Regex(numericPattern);

            return regexNumero.IsMatch(txt.Text);
        }

        private void txtModelo_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtCampo_TextChanged(object sender, EventArgs e)
        {

        }

        

        private void button7_Click(object sender, EventArgs e)
        {
            txtSeleccionar = txtFabricante;
            campoSeleccionar = "AERO_FAB";
            this.listado.campo = campoSeleccionar;
            this.listado.generarQueryInicial();
            this.listado.ejecutarConsulta();
            this.cambiarVisibilidades(this.listado);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            txtSeleccionar = txtServicio;
            campoSeleccionar = "SERV_COD";
            this.listado.campo = campoSeleccionar;
            this.listado.generarQueryInicial();
            this.listado.ejecutarConsulta();
            this.cambiarVisibilidades(this.listado);
        }

        private void txtFabricante_TextChanged(object sender, EventArgs e)
        {

        }

    }
}
