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
    public partial class Modificacion : Form
    {
        Form formularioSiguiente;

        public Listado listado;

        public Modificacion()
        {
            InitializeComponent();
            dtTime.Format = DateTimePickerFormat.Custom;
            dtTime.CustomFormat = "dd/mm/yyyy hh:mm:ss";
        }

        private void Modificacion_Load(object sender, EventArgs e)
        {
            this.inicio();
        }

        public void seSelecciono(DataGridViewRow registro)
        {
            cargarComboServicio();
            cargarComboFabricante();

            txtModeloActual.Text = registro.Cells["AERO_MOD"].Value.ToString();
            txtMatriculaActual.Text = registro.Cells["AERO_MATRI"].Value.ToString();
            txtFabricanteActual.Text = registro.Cells["AERO_FAB"].Value.ToString();
            txtServicioActual.Text = registro.Cells["SERV_DESC"].Value.ToString();
            txtButacasActual.Text = registro.Cells["AERO_CANT_BUTACAS"].Value.ToString();
            txtKilosActual.Text = registro.Cells["AERO_CANT_KGS"].Value.ToString();
            txtFechaAltaActual.Text = registro.Cells["AERO_FECHA_ALTA"].Value.ToString();
            
            txtModelo.Text = registro.Cells["AERO_MOD"].Value.ToString();
            txtMatricula.Text = registro.Cells["AERO_MATRI"].Value.ToString();
            cboFabricante.Text = registro.Cells["AERO_FAB"].Value.ToString();
            cboServicio.Text = registro.Cells["SERV_DESC"].Value.ToString();
            txtButacas.Text = registro.Cells["AERO_CANT_BUTACAS"].Value.ToString();
            txtKilos.Text = registro.Cells["AERO_CANT_KGS"].Value.ToString();
            dtTime.Text = registro.Cells["AERO_FECHA_ALTA"].Value.ToString(); 

            
            

            if (registro.Cells["AERO_FECHA_RS"].Value.ToString().GetType() == Type.GetType("DateTime"))
                dtTime.Value = Convert.ToDateTime(registro.Cells["AERO_FECHA_RS"].Value.ToString());

            if (tieneUnViajeAsignado(txtMatriculaActual.Text) == 1)
            {
                txtButacas.Visible = false;
                txtButacasActual.Visible = false;
                txtKilos.Visible = false;
                txtKilosActual.Visible = false;
                txtModelo.Visible = true;
                txtModeloActual.Visible = false;
                txtServicioActual.Visible = false;
                cboServicio.Visible = false;
                txtFabricanteActual.Visible = false;
                cboFabricante.Visible = false;
                dtTime.Visible = false;
                txtFechaAltaActual.Visible = false;
                label5.Visible = false;
                label6.Visible = false;
                label4.Visible = false;
                label1.Visible = false;
                label3.Visible = false;
                groupBox2.Visible = false;
            }
            else
            {

            }
            txtButacas.Enabled = true;
            txtKilos.Enabled = true;
            txtMatricula.Enabled = true;
            txtModelo.Enabled = true;

            cboServicio.Enabled = true;
            cboFabricante.Enabled = true;

            button2.Enabled = true;
            button3.Enabled = true;

            chkReinicio.Enabled = registro.Cells["FUERA_SERVICIO"].Value.Equals("SI");
            
        }

        private int tieneUnViajeAsignado(string matricula)
        {
            SqlCommand command = new SqlCommand();
            command.Connection = Program.conexion();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "[GD2C2015].[ABSTRACCIONX4].[TieneViajeAsignado]";
            command.CommandTimeout = 0;

            command.Parameters.AddWithValue("@AeronaveMatricula", matricula);

            return (int)command.ExecuteScalar();
        }

        private void inicio()
        {
            txtButacas.Text = "";
            txtKilos.Text = "";
            txtMatricula.Text = "";
            txtModelo.Text = "";

            txtButacas.Enabled = false;
            txtKilos.Enabled = false;
            txtMatricula.Enabled = false;
            txtModelo.Enabled = false;

            cboFabricante.SelectedIndex = -1;
            cboServicio.SelectedIndex = -1;

            cboServicio.Enabled = false;
            cboFabricante.Enabled = false;

            cboFabricante.Text = "";
            cboServicio.Text = "";

            chkReinicio.Checked = false;
            chkReinicio.Enabled = false;

            dtTime.Enabled = false;
            dtTime.Value = DateTime.Now.Date;

            button2.Enabled = false;
            button3.Enabled = false;

            txtButacasActual.Text = "";
            txtKilosActual.Text = "";
            txtMatriculaActual.Text = "";
            txtModeloActual.Text = "";
            txtFabricanteActual.Text = "";
            txtServicioActual.Text = "";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            formularioSiguiente = new Principal();
            this.cambiarVisibilidades(formularioSiguiente);
        }

        private void cambiarVisibilidades(Form formularioSiguiente)
        {
            formularioSiguiente.Visible = true;
            this.Visible = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.cambiarVisibilidades(this.listado);
        }

        private void chkReinicio_CheckedChanged(object sender, EventArgs e)
        {
            if (chkReinicio.Checked)
                dtTime.Enabled = true;
            else
                dtTime.Enabled = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.inicio();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.datosCorrectos())
            {
                MessageBox.Show("Todos los datos son correctos. Se procede a modificar el registro de aeronave", "Alta de nueva aeronave", MessageBoxButtons.OK);
                this.modificar();       
            }
        }

        private Object modificar()
        {
            SqlCommand command = new SqlCommand();
            command.Connection = Program.conexion();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "[GD2C2015].[ABSTRACCIONX4].[ModificarAeronave]";
            command.CommandTimeout = 0;

            command.Parameters.AddWithValue("@Modelo", txtModelo.Text);
            command.Parameters.AddWithValue("@Matricula", txtMatricula.Text);
            command.Parameters.AddWithValue("@Fabricante", cboFabricante.Text);
            command.Parameters.AddWithValue("@TipoDeServicio", cboServicio.Text);
            command.Parameters.AddWithValue("@CantidadButacas", Convert.ToInt16(txtButacas.Text));
            command.Parameters.AddWithValue("@CantidadKG", cantidadKilogramos());
            command.Parameters.AddWithValue("@FechaReinicio", Convert.ToDateTime(dtTime.Text));

            return command.ExecuteScalar();
        }

        private Decimal cantidadKilogramos()
        {
            return Decimal.Round(Convert.ToDecimal(txtKilos.Text.Replace(".", ",")), 2);
        }

        private bool datosCorrectos()
        {
            return !this.validarTipos() ;
        }

        private Boolean validarLongitudes()
        {
            Boolean algunoVacio = !this.seCompleto(txtModelo, "Modelo");
            algunoVacio = !this.seCompleto(txtMatricula, "Matricula") || algunoVacio;
            algunoVacio = !this.seCompleto(cboServicio, "Tipo de Servicio") || algunoVacio;
            algunoVacio = !this.seCompleto(cboFabricante, "Fabricante") || algunoVacio;
            algunoVacio = !this.seCompleto(txtButacas, "Cantidad de butacas") || algunoVacio;
            algunoVacio = !this.seCompleto(txtKilos, "Cantidad de kilos") || algunoVacio;

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


        private Boolean seCompleto(ComboBox cbo, string campo)
        {
            if (cbo.Text.Equals(""))
            {
                MessageBox.Show("El combo " + campo + " no puede estar vacio", "Error en los datos de entrada", MessageBoxButtons.OK);
                return false;
            }
            else
                return true;
        }

        private Boolean validarTipos()
        {
            Boolean huboError = false;

            huboError = !esTexto(txtModelo, "modelo") || huboError;
            huboError = !esTexto(txtMatricula, "matrícula") || huboError;
            huboError = !esNumero(txtButacas, "cantidad de butacas", true) || huboError;
            huboError = !esNumero(txtKilos, "cantidad de Kg", false) || huboError;

            return huboError;
        }


        private Boolean esTexto(TextBox txt, string campo)
        {
            String textPattern = "[A-Za-z]";
            System.Text.RegularExpressions.Regex regexTexto = new System.Text.RegularExpressions.Regex(textPattern);

            if (txt.TextLength != 0 && !regexTexto.IsMatch(txt.Text))
            {
                MessageBox.Show("El campo " + campo + " debe ser un texto", "Error en los tipos de entrada", MessageBoxButtons.OK);
                return false;
            }
            return true;
        }

        private Boolean esNumero(TextBox txt, string campo, bool debeSerEntero)
        {
            int n;
            decimal d;
            if (txt.TextLength != 0)
            {
                if (debeSerEntero)
                {
                    if (!int.TryParse(txt.Text, out n))
                    {
                        MessageBox.Show("El campo " + campo + " debe ser un número entero", "Error en los tipos de entrada", MessageBoxButtons.OK);
                        return false;
                    }
                }
                else
                {
                    if (!decimal.TryParse(txt.Text, out d))
                    {
                        MessageBox.Show("El campo " + campo + " debe ser un número", "Error en los tipos de entrada", MessageBoxButtons.OK);
                        return false;
                    }
                }

            }
            return true;
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

        private void cargarComboFabricante()
        {
            cboFabricante.Items.Clear();

            SqlDataReader reader;
            SqlCommand consultaServicios = new SqlCommand();
            consultaServicios.CommandType = CommandType.Text;
            consultaServicios.CommandText = "SELECT DISTINCT AERO_FAB FROM [ABSTRACCIONX4].[AERONAVES]";
            consultaServicios.Connection = Program.conexion();

            reader = consultaServicios.ExecuteReader();

            while (reader.Read())
                this.cboFabricante.Items.Add(reader.GetValue(0));

            reader.Close();
        }
    }
}
