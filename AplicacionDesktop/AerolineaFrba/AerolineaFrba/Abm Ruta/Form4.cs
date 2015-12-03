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

        public Listado listado;
        bool seleccionandoOrigen;
        private int idRuta;
        List<Object> listaServicios;

        public Modificacion()
        {
            listaServicios = new List<Object>();
            InitializeComponent();
        }

        private void Modificacion_Load(object sender, EventArgs e)
        {
            this.iniciar();
        }

        // Carga los datos seleccionados en el listado
        public void seSelecciono(DataGridViewRow registro,List<Object> tiposDeServicio)
        {

            idRuta = Convert.ToInt32(registro.Cells["Id"].Value);

            txtCodigo.Text = registro.Cells["Código Ruta"].Value.ToString();
            txtCiudadOrigenNueva.Text = txtCiudadOrigen.Text = registro.Cells["Origen"].Value.ToString();
            txtCiudadDestinoNueva.Text = txtCiudadDestino.Text = registro.Cells["Destino"].Value.ToString();
            txtPrecioEncomiendaNueva.Text = txtPrecioEncomienda.Text = registro.Cells["Precio Base Por Kilogramo"].Value.ToString();
            txtPrecioPasajeNuevo.Text = txtPrecioPasaje.Text = registro.Cells["Precio Base Pasaje"].Value.ToString();

            this.listaServicios.Clear();
            this.listaServicios.AddRange(tiposDeServicio);

            Boolean viajeProgramado = !tieneViajeProgramado(idRuta);
            

            botonSelOrigen.Enabled =
            botonSelDestino.Enabled =
            botonSelServicios.Enabled =
            txtPrecioEncomienda.Enabled =
            txtPrecioPasaje.Enabled =
            txtCiudadDestino.Enabled =
            txtCiudadOrigen.Enabled =
            txtCiudadOrigenNueva.Enabled =
            txtCiudadDestinoNueva.Enabled = viajeProgramado;
            
            Boolean viajeVendidos = !tieneViajeVendidos(idRuta);

            txtPrecioEncomiendaNueva.Enabled =
            txtPrecioPasajeNuevo.Enabled = viajeVendidos;


            txtCodigo.Enabled = true;
            botonLimpiar.Enabled = true;
            botonGuardar.Enabled = true;


            foreach (Object e in tiposDeServicio)
            {
                label9.Text += (String)e + " - ";
                label10.Text += (String)e + " - ";
            }

            label9.Text = label9.Text.Remove(label9.Text.Length - 2);
            label10.Text = label10.Text.Remove(label10.Text.Length - 2);
            
        }

        private Boolean tieneViajeVendidos(int idRuta)
        {
            SqlCommand command = new SqlCommand();
            command.Connection = Program.conexion();
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = "SELECT ABSTRACCIONX4.TieneViajeVendidos(@IdRuta)";
            command.CommandTimeout = 0;

            command.Parameters.AddWithValue("@IdRuta", idRuta);

            return (Boolean)command.ExecuteScalar();
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
          txtCiudadDestino.Text = "";
          txtCiudadOrigen.Text = "";
          txtCodigo.Text = "";
          txtPrecioEncomienda.Text = "";
          txtPrecioPasaje.Text = "";
          txtCiudadDestinoNueva.Text = "";
          txtCiudadOrigenNueva.Text = "";
          txtPrecioEncomiendaNueva.Text = "";
          txtPrecioPasajeNuevo.Text = "";
          listaServicios.Clear();
          label10.Text = "";
          label9.Text = "";
             
          txtCodigo.Enabled = false;
          botonSelServicios.Enabled = false;
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

        private void cambiarVisibilidades(Form formularioSiguiente)
        {
            formularioSiguiente.Visible = true;
            this.Visible = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.iniciar();
            Listado listado = new Listado();
            listado.llamadoDeModificacion = true;
            listado.siguiente = this;
            listado.ShowDialog();
        }

        private void botonLimpiar_Click(object sender, EventArgs e)
        {
            this.iniciar();
        }

        private void botonGuardar_Click(object sender, EventArgs e)
        {
            if (this.datosCorrectos())
            {
                if (!existeRuta())
                {
                    try
                    {
                        modificarRuta();
                        MessageBox.Show("Se ha realizado la modificación correctamente", "Modificación de ruta", MessageBoxButtons.OK);
                        this.Close();
                    }
                    catch (Exception error)
                    {
                        MessageBox.Show(error.Message, "Modificación de ruta", MessageBoxButtons.OK);
                    }
                }
                else
                    MessageBox.Show("Ya existe una ruta con las características elegidas", "Informe", MessageBoxButtons.OK);
            }
        }

        private bool existeRuta()
        {
            SqlCommand command = new SqlCommand();
            command.Connection = Program.conexion();
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = "SELECT ABSTRACCIONX4.ExisteRuta(@Origen,@Destino,@PPasaje,@PKg,@Servicios)";
            command.CommandTimeout = 0;

            command.Parameters.AddWithValue("@Origen", txtCiudadOrigenNueva.Text);
            command.Parameters.AddWithValue("@Destino", txtCiudadDestinoNueva.Text);
            command.Parameters.AddWithValue("@PPasaje", txtPrecioPasajeNuevo.Text);
            command.Parameters.AddWithValue("@PKg", txtPrecioEncomiendaNueva.Text);
            //command.Parameters.AddWithValue("@Servicios", crearDataTable(listaServicios));
            SqlParameter param = new SqlParameter("@Servicios", SqlDbType.Structured);
            param.TypeName = "ABSTRACCIONX4.Lista";
            param.Value = crearDataTable(listaServicios);
            command.Parameters.Add(param);

            return (Boolean)command.ExecuteScalar();
        }

        private object crearDataTable(IEnumerable<Object> lista)
        {
             DataTable table = new DataTable();
            table.Columns.Add("elemento", typeof(string));
            foreach (string elemento in lista)
            {
                table.Rows.Add(elemento.ToString());
            }
            return table;
        }
        

        private Object modificarRuta()
        {
            SQLManager sqlManager = new SQLManager();
            return sqlManager.generarSP("ModificarRuta").agregarIntSP("@IdRuta", idRuta)
                                                        .agregarIntSP("@Codigo", txtCodigo)
                                                        .agregarListaSP("@Servicios", listaServicios)
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
            algunoVacio = Validacion.esVacio(txtCiudadOrigenNueva, "ciudad de origen", true) || algunoVacio;
            algunoVacio = Validacion.esVacio(txtCiudadDestinoNueva, "ciudad de destino", true) || algunoVacio;
            algunoVacio = Validacion.esVacio(txtPrecioPasajeNuevo, "precio de pasaje", true) || algunoVacio;
            algunoVacio = Validacion.esVacio(txtPrecioEncomiendaNueva, "precio de encomienda", true) || algunoVacio;

            return algunoVacio;
        }

        private Boolean validarTipos()
        {
            Boolean huboErrores = false;

            huboErrores = !Validacion.esNumero(txtCodigo, "código", true) || huboErrores;
            huboErrores = !Validacion.esDecimal(txtPrecioPasajeNuevo, "precio de pasaje", true) || huboErrores;
            huboErrores = !Validacion.esDecimal(txtPrecioEncomiendaNueva, "precio de encomienda", true) || huboErrores;

            return huboErrores;
        }

        private bool validarLimitesNumericos()
        {
            Boolean huboErrores = false;

            huboErrores = !Validacion.estaEntreLimites(txtCodigo, 1, 99999999, false, "código") || huboErrores;
            huboErrores = !Validacion.estaEntreLimites(txtPrecioPasajeNuevo, 0.01m, 999, true, "precio de pasaje") || huboErrores;
            huboErrores = !Validacion.estaEntreLimites(txtPrecioEncomiendaNueva, 0.01m, 999, true, "precio de encomienda") || huboErrores;

            return huboErrores;
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


        public void serviciosElegidos(List<object> lista)
        {
            listaServicios.Clear();
            listaServicios.AddRange(lista);

            label10.Text = "";
            foreach (Object e in lista)
            {
                label10.Text += (String)e + " - ";
                label10.Visible = true;
            }
            
        }

        private void botonSelServicios_Click(object sender, EventArgs e)
        {
            Form formularioServicios = new Servicios(null, this,listaServicios);
            formularioServicios.ShowDialog();
        }

        
    }
}
