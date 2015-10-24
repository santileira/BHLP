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
          
            txtModeloActual.Text = registro.Cells["AERO_MOD"].Value.ToString();
            txtMatriculaActual.Text = registro.Cells["AERO_MATRI"].Value.ToString();
            // van aca y no al principio porque necesito tener el valor de matricula seteado ya
            Int16 cantidadPasillo = obtenerButacas("Pasillo", txtMatriculaActual.Text);
            Int16 cantidadVentanilla = obtenerButacas("Ventanilla", txtMatriculaActual.Text);

            txtFabricanteActual.Text = registro.Cells["AERO_FAB"].Value.ToString();
            txtServicioActual.Text = registro.Cells["SERV_DESC"].Value.ToString();
            txtButacasActual.Text = cantidadPasillo.ToString();
            txtVenta.Text = cantidadVentanilla.ToString();
            txtKilosActual.Text = registro.Cells["AERO_CANT_KGS"].Value.ToString();
            
            txtModelo.Text = registro.Cells["AERO_MOD"].Value.ToString();
            txtMatricula.Text = registro.Cells["AERO_MATRI"].Value.ToString();
            cboFabricante.Text = registro.Cells["AERO_FAB"].Value.ToString();
            cboServicio.Text = registro.Cells["SERV_DESC"].Value.ToString();
            txtButacas.Text = cantidadPasillo.ToString();
            txtVenta1.Text = cantidadVentanilla.ToString();
            txtKilos.Text = registro.Cells["AERO_CANT_KGS"].Value.ToString();


            Boolean viajeAsignado = !tieneUnViajeAsignado(txtMatricula.Text);

            txtButacas.Enabled =
            txtButacasActual.Enabled =
            txtVenta1.Enabled=
            txtVenta.Enabled=
            txtKilos.Enabled =
            txtKilosActual.Enabled =
            txtModelo.Enabled =
            txtModeloActual.Enabled =
            txtServicioActual.Enabled =
            cboServicio.Enabled =
            cboFabricante.Enabled = 
            txtFabricanteActual.Enabled =
            cboFabricante.Enabled = viajeAsignado;

            txtMatricula.Enabled = true;
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
            command.CommandText = "SELECT ABSTRACCIONX4.TieneViajeComprado(@Matricula)";
            command.CommandTimeout = 0;

            command.Parameters.AddWithValue("@Matricula", matricula);

            return (Boolean)command.ExecuteScalar();
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
                MessageBox.Show("Todos los datos son correctos. Se procede a modificar el registro de aeronave", "Alta de nueva aeronave", MessageBoxButtons.OK);
                this.modificar();
                /*try
                {
                    this.modificar();
                }
                catch
                {
                    MessageBox.Show("Ya existe una aeronave con la matrícula " + txtMatricula.Text, "Advertencia", MessageBoxButtons.OK);
                    return;
                }*/
                (listado as Listado).inicio();
                this.cambiarVisibilidades(this.listado);
            }
        }

        private Object modificar()
        {
            return new SQLManager().generarSP("ModificarAeronave").agregarStringSP("@MatriculaActual", txtMatriculaActual).
            agregarStringSP("@Modelo", txtModelo).agregarStringSP("@Matricula" , txtMatricula).
            agregarStringSP("@Fabricante", cboFabricante).agregarStringSP("@TipoDeServicio", cboServicio).
            agregarIntSP("@CantidadPasillo", txtButacas).agregarIntSP("@CantidadVentanilla", txtVenta1).
            agregarDecimalSP("@CantidadKG", cantidadKilogramos()).ejecutarSP();
            /*SqlCommand command = new SqlCommand();
            command.Connection = Program.conexion();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "[GD2C2015].[ABSTRACCIONX4].[ModificarAeronave]";
            command.CommandTimeout = 0;

            command.Parameters.AddWithValue("@MatriculaActual", txtMatriculaActual.Text);
            command.Parameters.AddWithValue("@Modelo", txtModelo.Text);
            command.Parameters.AddWithValue("@Matricula", txtMatricula.Text);
            command.Parameters.AddWithValue("@Fabricante", cboFabricante.Text);
            command.Parameters.AddWithValue("@TipoDeServicio", cboServicio.Text);
            command.Parameters.AddWithValue("@CantidadButacas", Convert.ToInt16(txtButacas.Text));
            command.Parameters.AddWithValue("@CantidadKG", cantidadKilogramos());

            return command.ExecuteScalar();*/
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

            return !huboErrores;
        }

        private Boolean validarLongitudes()
        {
            Boolean algunoVacio = Validacion.esVacio(txtModelo, "Modelo");
            algunoVacio = Validacion.esVacio(txtMatricula, "Matricula") || algunoVacio;
            algunoVacio = Validacion.esVacio(cboServicio, "Tipo de Servicio") || algunoVacio;
            algunoVacio = Validacion.esVacio(cboFabricante, "Fabricante") || algunoVacio;
            algunoVacio = Validacion.esVacio(txtButacas, "Cantidad de butacas pasillo") || algunoVacio;
            algunoVacio = Validacion.esVacio(txtVenta1, "Cantidad de butacas ventanilla") || algunoVacio;
            algunoVacio = Validacion.esVacio(txtKilos, "Cantidad de kilos") || algunoVacio;

            return algunoVacio;
        }
        /*
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
        */

       /* private Boolean seCompleto(ComboBox cbo, string campo)
        {
            if (cbo.Text.Equals(""))
            {
                MessageBox.Show("El combo " + campo + " no puede estar vacio", "Error en los datos de entrada", MessageBoxButtons.OK);
                return false;
            }
            else
                return true;
        }*/

        private Boolean validarTipos()
        {
            Boolean huboError = false;

            huboError = !Validacion.esTexto(txtModelo, "modelo" , true) || huboError;
            huboError = !Validacion.esTexto(txtMatricula, "matrícula" , true) || huboError;
            huboError = !Validacion.numeroCorrecto(txtButacas, "cantidad de butacas pasillo", false) || huboError;
            huboError = !Validacion.numeroCorrecto(txtVenta1, "cantidad de butacas ventanilla", false) || huboError;
            huboError = !Validacion.numeroCorrecto(txtKilos, "cantidad de Kg", true) || huboError;

            return huboError;
        }


       /* private Boolean esTexto(TextBox txt, string campo)
        {
            String textPattern = "[A-Za-z]";
            System.Text.RegularExpressions.Regex regexTexto = new System.Text.RegularExpressions.Regex(textPattern);

            if (txt.TextLength != 0 && !regexTexto.IsMatch(txt.Text))
            {
                MessageBox.Show("El campo " + campo + " debe ser un texto", "Error en los tipos de entrada", MessageBoxButtons.OK);
                return false;
            }
            return true;
        }*/

       /* private Boolean esNumero(TextBox txt, string campo, bool debeSerEntero)
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
