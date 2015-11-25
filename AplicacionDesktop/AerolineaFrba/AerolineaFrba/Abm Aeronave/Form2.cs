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
        Boolean llamadoDesdeBaja;
        string matriculaAveronaveBaja;
        int limiteButacasPasillo;
        int limiteButacasVentanilla;
        Decimal limiteKG;
        DateTime limiteFecha;
        Form formAnterior;

        public Alta(Boolean llamadoDesdeBaja, Form formAnterior, string matriculaAveronaveBaja, DateTime fechaBaja)
        {
            this.llamadoDesdeBaja = llamadoDesdeBaja;
            this.matriculaAveronaveBaja = matriculaAveronaveBaja;
            this.limiteFecha = fechaBaja;
            this.formAnterior = formAnterior;
            InitializeComponent();
            cargarDatosIniciales();        
        }

        private void cargarDatosIniciales()
        {
            if (llamadoDesdeBaja)
            {

                txtModelo.Enabled = false;

                cargarDatosFijos();

                cboFabricante.SelectedIndex = 0;
                cboCiudades.SelectedIndex = 0;
                cboServicio.SelectedIndex = 0;
                cboFabricante.Enabled = false;
                cboCiudades.Enabled = false;
                cboServicio.Enabled = false;
            }
            else
            {
                cargarComboCiudades();
                cargarComboFabricante();
                cargarComboServicio();
            }
        }

        private void Alta_Load(object sender, EventArgs e)
        {
            this.inicio();
        }

        private void inicio()
        {
            mkMatricula.Text = "";
            txtButacas.Text = "";
            txtVenta.Text = "";
            txtKilos.Text = "";
            dateTimePicker1.Value = Program.fechaHoy();
            if (!llamadoDesdeBaja)
            {
                txtModelo.Text = "";
                cboCiudades.SelectedIndex = -1;
                cboFabricante.SelectedIndex = -1;
                cboServicio.SelectedIndex = -1;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.inicio();
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
                    darDeAltaAeronave();
                    MessageBox.Show("La aeronave fue dada de alta de manera exitosa", "Alta de nueva aeronave", MessageBoxButtons.OK);

                    if (llamadoDesdeBaja)
                    {
                        (formAnterior as Form7).cargoDatosParaSuplantar = true;
                        this.Close();
                    }
                    else
                    {
                        this.Close();
                    }
                }
                catch(Exception error)
                {
                    MessageBox.Show(error.Message, "Error en alta", MessageBoxButtons.OK);
                    return;
                }
            }

        }

        private Object darDeAltaAeronave()
        {
            return new SQLManager().generarSP("AltaAeronave").agregarStringSP("@Modelo", txtModelo)
                                                             .agregarStringSP("@Matricula", mkMatricula.Text.ToUpper())
                                                             .agregarStringSP("@Fabricante", cboFabricante)
                                                             .agregarStringSP("@TipoDeServicio", cboServicio)
                                                             .agregarIntSP("@CantidadPasillo", txtButacas)
                                                             .agregarIntSP("@CantidadVentanilla" , txtVenta)
                                                             .agregarDecimalSP("@CantidadKG", cantidadKilogramos())
                                                             .agregarFechaSP("@FechaAlta", dateTimePicker1)
                                                             .agregarStringSP("@CiudadPrincipal" , cboCiudades.Text)
                                                             .ejecutarSP();
        }

        private Decimal cantidadKilogramos()
        {
            return Decimal.Round(Convert.ToDecimal(txtKilos.Text.Replace(".",",")), 2);
        }

        private bool datosCorrectos()
        {
            Boolean huboErrores = false;

            huboErrores = this.validarLongitudes() || huboErrores;
            huboErrores = this.validarTipos() || huboErrores;
            huboErrores = this.validarLimitesNumericos() || huboErrores;
            huboErrores = this.validarFecha() || huboErrores;
            huboErrores = this.validarLimitesBaja() || huboErrores;

            return !huboErrores;
        }


        private bool validarFecha()
        {
            return !Validacion.fechaPosteriorALaDeHoy(dateTimePicker1);
        }


        private Boolean validarLongitudes()
        {
            Boolean algunoVacio = false;
            algunoVacio = Validacion.esVacio(txtModelo, "modelo", true) || algunoVacio;
            algunoVacio = Validacion.esVacio(cboFabricante, "fabricante",true) || algunoVacio;
            algunoVacio = Validacion.esVacio(cboServicio, "tipo de servicio", true) || algunoVacio;
            algunoVacio = Validacion.esVacio(cboCiudades, "ciudad en la que se encuentra", true) || algunoVacio;
            algunoVacio = Validacion.esVacio(txtButacas, "cantidad de butacas pasillo", true) || algunoVacio;
            algunoVacio = Validacion.esVacio(txtVenta, "cantidad de butacas ventanilla", true) || algunoVacio;
            algunoVacio = Validacion.esVacio(txtKilos, "cantidad de kilos", true) || algunoVacio;

            return algunoVacio;
        }

        private Boolean validarTipos()
        {
            Boolean huboError = false;

            huboError = !Validacion.esMatricula(mkMatricula, true) || huboError;
            huboError = !Validacion.esTextoAlfanumerico(txtModelo, false, "modelo", true) || huboError;
            huboError = !Validacion.esNumero(txtButacas, "cantidad de butacas pasillo", true) || huboError;
            huboError = !Validacion.esNumero(txtVenta, "cantidad de butacas ventanilla", true) || huboError;
            huboError = !Validacion.esDecimal(txtKilos, "cantidad de Kg", true) || huboError;

            return huboError;
        }

        private bool validarLimitesNumericos()
        {
            int limiteInfPasillo, limiteInfVentanilla;
            decimal limiteInfKG;
            if (llamadoDesdeBaja)
            {
                limiteInfPasillo = limiteButacasPasillo;
                limiteInfVentanilla = limiteButacasVentanilla;
                limiteInfKG = limiteKG;
            }
            else
            {
                limiteInfPasillo = limiteInfVentanilla = 1;
                limiteInfKG = 0;
            }

            Boolean huboErrores = false;

            huboErrores = !Validacion.estaEntreLimites(txtButacas, limiteInfPasillo, 999, false, "cantidad de butacas pasillo") || huboErrores;
            huboErrores = !Validacion.estaEntreLimites(txtVenta, limiteInfVentanilla, 999, false, "cantidad de butacas ventanilla") || huboErrores;
            huboErrores = !Validacion.estaEntreLimites(txtKilos, limiteInfKG, 999, true, "cantidad de kilos") || huboErrores;

            return huboErrores;
        }

        private bool validarLimitesBaja()
        {
            if (!llamadoDesdeBaja)
            {
                return false;
            }

            Boolean huboError = false;

            if (dateTimePicker1.Value.CompareTo(limiteFecha) > 0)
            {
                MessageBox.Show("La fecha de alta debe ser anterior a " + limiteFecha.ToString(), "Error en los datos ingresados", MessageBoxButtons.OK);
                huboError = true;
            }

            return huboError;
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

        private void cargarComboCiudades()
        {
            cboCiudades.Items.Clear();

            SqlDataReader reader;
            SqlCommand consultaServicios = new SqlCommand();
            consultaServicios.CommandType = CommandType.Text;
            consultaServicios.CommandText = "SELECT CIU_DESC FROM [ABSTRACCIONX4].[CIUDADES]";
            consultaServicios.Connection = Program.conexion();

            reader = consultaServicios.ExecuteReader();

            while (reader.Read())
                this.cboCiudades.Items.Add(reader.GetValue(0));

            reader.Close();
        }

        private void cargarDatosFijos()
        {
            cboServicio.Items.Clear();

            SqlDataReader reader;
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT * FROM [ABSTRACCIONX4].DatosDeAeronaveASuplantar(@Matricula,@FechaBaja)";
            command.Connection = Program.conexion();

            command.Parameters.AddWithValue("@Matricula", matriculaAveronaveBaja);
            command.Parameters.AddWithValue("@FechaBaja", limiteFecha);

            reader = command.ExecuteReader();

            reader.Read();

            txtModelo.Text = reader.GetValue(0).ToString();
            cboFabricante.Items.Add(reader.GetValue(1).ToString());
            cboServicio.Items.Add(reader.GetValue(2).ToString());
            cboCiudades.Items.Add(reader.GetValue(3).ToString());
            limiteButacasPasillo = Convert.ToInt16(reader.GetValue(4).ToString());
            limiteButacasVentanilla = Convert.ToInt16(reader.GetValue(5));
            limiteKG = Convert.ToDecimal(reader.GetValue(6));

            reader.Close();
        }

        private void mkMatricula_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

    }
}
