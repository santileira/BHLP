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
        }

        private void Modificacion_Load(object sender, EventArgs e)
        {
            this.inicio();
        }

        public void seSelecciono(DataGridViewRow registro)
        {
            cargarComboServicio();
            cargarComboFabricante();
          
            txtModeloActual.Text = registro.Cells["Modelo"].Value.ToString();
            txtMatriculaActual.Text = registro.Cells["Matrícula"].Value.ToString();
            // van aca y no al principio porque necesito tener el valor de matricula seteado ya
            Int16 cantidadPasillo = obtenerButacas("Pasillo", txtMatriculaActual.Text);
            Int16 cantidadVentanilla = obtenerButacas("Ventanilla", txtMatriculaActual.Text);

            txtFabricanteActual.Text = registro.Cells["Fabricante"].Value.ToString();
            txtServicioActual.Text = registro.Cells["Tipo de servicio"].Value.ToString();
            txtButacasPActual.Text = cantidadPasillo.ToString();
            txtButacasVActual.Text = cantidadVentanilla.ToString();
            txtKilosActual.Text = registro.Cells["Cantidad de KG"].Value.ToString();
            
            txtModelo.Text = registro.Cells["Modelo"].Value.ToString();
            mkMatricula.Text = registro.Cells["Matrícula"].Value.ToString();
            cboFabricante.Text = registro.Cells["Fabricante"].Value.ToString();
            cboServicio.Text = registro.Cells["Tipo de servicio"].Value.ToString();
            txtButacasP.Text = cantidadPasillo.ToString();
            txtButacasV.Text = cantidadVentanilla.ToString();
            txtKilos.Text = registro.Cells["Cantidad de KG"].Value.ToString();


            Boolean viajeAsignado = !tieneUnViajeAsignado(mkMatricula.Text);

            txtButacasP.Enabled =
            txtButacasPActual.Enabled =
            txtButacasV.Enabled=
            txtButacasVActual.Enabled=
            txtKilos.Enabled =
            txtKilosActual.Enabled =
            txtModelo.Enabled =
            txtModeloActual.Enabled =
            txtServicioActual.Enabled =
            cboServicio.Enabled =
            cboFabricante.Enabled = 
            txtFabricanteActual.Enabled =
            cboFabricante.Enabled = viajeAsignado;

            mkMatricula.Enabled = true;
            txtMatriculaActual.Enabled = true;

            button2.Enabled = true;
            button3.Enabled = true;
            
        }

        private Int16 obtenerButacas(string tipo, string matricula)
        {
            SqlCommand command = new SqlCommand();
            command.Connection = Program.conexion();
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = "SELECT ABSTRACCIONX4.CantidadButacas(@Matricula , @Tipo)";
            command.CommandTimeout = 0;

            command.Parameters.AddWithValue("@Matricula", matricula);
            command.Parameters.AddWithValue("@Tipo", tipo);

            return (Int16)command.ExecuteScalar();
        }

        private Boolean tieneUnViajeAsignado(string matricula)
        {
            SqlCommand command = new SqlCommand();
            command.Connection = Program.conexion();
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = "SELECT ABSTRACCIONX4.TieneViajeAsignado(@Matricula)";
            command.CommandTimeout = 0;

            command.Parameters.AddWithValue("@Matricula", matricula);

            return (Boolean)command.ExecuteScalar();
        }

        private void inicio()
        {
            txtButacasP.Text = "";
            txtButacasV.Text = "";
            txtKilos.Text = "";
            mkMatricula.Text = "";
            txtModelo.Text = "";

            txtButacasP.Enabled = false;
            txtButacasV.Enabled = false;
            txtKilos.Enabled = false;
            mkMatricula.Enabled = false;
            txtModelo.Enabled = false;

            cboFabricante.SelectedIndex = -1;
            cboServicio.SelectedIndex = -1;

            cboServicio.Enabled = false;
            cboFabricante.Enabled = false;

            cboFabricante.Text = "";
            cboServicio.Text = "";

            button2.Enabled = false;
            button3.Enabled = false;

            txtButacasPActual.Text = "";
            txtButacasVActual.Text = "";
            txtKilosActual.Text = "";
            txtMatriculaActual.Text = "";
            txtModeloActual.Text = "";
            txtFabricanteActual.Text = "";
            txtServicioActual.Text = "";
        }

        private void cambiarVisibilidades(Form formularioSiguiente)
        {
            formularioSiguiente.Visible = true;
            this.Visible = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            listado.inicio();
            listado.llamadoDesdeModificacion = true;
            this.cambiarVisibilidades(this.listado);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.inicio();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.datosCorrectos())
            {
                this.modificar();
                MessageBox.Show("Se modificó la aeronave de manera exitosa", "Modificación de aeronave", MessageBoxButtons.OK);
                this.inicio();
            }
        }

        private Object modificar()
        {
            return new SQLManager().generarSP("ModificarAeronave")
                                   .agregarStringSP("@MatriculaActual", txtMatriculaActual)
                                   .agregarStringSP("@Modelo", txtModelo)
                                   .agregarStringSP("@Matricula" , mkMatricula.Text.ToUpper())
                                   .agregarStringSP("@Fabricante", cboFabricante)
                                   .agregarStringSP("@TipoDeServicio", cboServicio)
                                   .agregarIntSP("@CantidadPasillo", txtButacasP)
                                   .agregarIntSP("@CantidadVentanilla", txtButacasV)
                                   .agregarDecimalSP("@CantidadKG", cantidadKilogramos())
                                   .ejecutarSP();
        }

        private Decimal cantidadKilogramos()
        {
            return Decimal.Round(Convert.ToDecimal(txtKilos.Text.Replace(".", ",")), 2);
        }

        private bool datosCorrectos()
        {
            Boolean huboErrores = false;

            huboErrores = this.validarLongitudes() || huboErrores;
            huboErrores = this.validarTipos() || huboErrores;
            huboErrores = this.validarLimitesNumericos() || huboErrores;

            return !huboErrores;
        }

        private Boolean validarLongitudes()
        {
            Boolean algunoVacio = false;
            algunoVacio = Validacion.esVacio(txtModelo, "modelo", true) || algunoVacio;
            algunoVacio = Validacion.esVacio(cboFabricante, "fabricante", true) || algunoVacio;
            algunoVacio = Validacion.esVacio(cboServicio, "tipo de servicio", true) || algunoVacio;
            algunoVacio = Validacion.esVacio(txtButacasP, "cantidad de butacas pasillo", true) || algunoVacio;
            algunoVacio = Validacion.esVacio(txtButacasV, "cantidad de butacas ventanilla", true) || algunoVacio;
            algunoVacio = Validacion.esVacio(txtKilos, "cantidad de kilos", true) || algunoVacio;

            return algunoVacio;
        }

        private Boolean validarTipos()
        {
            Boolean huboError = false;

            huboError = !Validacion.esMatricula(mkMatricula,  true) || huboError;
            huboError = !Validacion.esTextoAlfanumerico(txtModelo,false, "modelo", true) || huboError;
            huboError = !Validacion.esNumero(txtButacasP, "cantidad de butacas pasillo", true) || huboError;
            huboError = !Validacion.esNumero(txtButacasV, "cantidad de butacas ventanilla", true) || huboError;
            huboError = !Validacion.esDecimal(txtKilos, "cantidad de kilos", true) || huboError;

            return huboError;
        }

        private bool validarLimitesNumericos()
        {
            Boolean huboErrores = false;

            huboErrores = !Validacion.estaEntreLimites(txtButacasP, 1, 999, false, "cantidad de butacas pasillo") || huboErrores;
            huboErrores = !Validacion.estaEntreLimites(txtButacasV, 1, 999, false, "cantidad de butacas ventanilla") || huboErrores;
            huboErrores = !Validacion.estaEntreLimites(txtKilos, 0, 999, true, "cantidad de Kg") || huboErrores;

            return huboErrores;
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

        private void txtButacasActual_TextChanged(object sender, EventArgs e)
        {

        }

        
    }
}
