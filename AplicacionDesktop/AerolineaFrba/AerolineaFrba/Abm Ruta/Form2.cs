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
        List<Object> listaServicios;

        public Alta()
        {
            listaServicios = new List<Object>();
            InitializeComponent();          
        }

        private void Alta_Load(object sender, EventArgs e)
        {
            this.iniciar();
        }

        private void iniciar()
        {
            
            label6.Text = "";
            label6.Visible = false;
            listaServicios.Clear();
            txtCiudadDestino.Text = "";
            txtCiudadOrigen.Text = "";
            txtCodigo.Text = "";
            txtPrecioEncomienda.Text = "";
            txtPrecioPasaje.Text = "";
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

        // Da de alta la ruta si no hay errores
        private void button2_Click(object sender, EventArgs e)
        {
            if (this.datosCorrectos())
            {
                List<string> listaServiciosAAgregar = ejecutarExisteRuta();
                string mensajeServicios = "";

                if (!serviciosADarDeAlta(listaServiciosAAgregar,ref mensajeServicios))
                {
                    try
                    {
                        darDeAltaRuta(listaServiciosAAgregar);
                        MessageBox.Show(mensajeServicios, "Alta de nueva ruta", MessageBoxButtons.OK);
                        MessageBox.Show("El alta de la ruta se realizó exitosamente.", "Alta de nueva ruta", MessageBoxButtons.OK);
                    }
                    catch (Exception excepcion)
                    {
                        MessageBox.Show(excepcion.Message, "Advertencia", MessageBoxButtons.OK);
                        return;
                    }
                    this.Close();
                }
                else
                    MessageBox.Show("Ya existe una ruta con las características elegidas", "Informe", MessageBoxButtons.OK);
            }

        }



        private bool serviciosADarDeAlta(List<string> listaServiciosAAgregar,ref string mensajeServicios)
        {
            if (listaServiciosAAgregar.Count != 0)
            {
                string servicios = "Los servicios a dar de alta serán:\n";
                foreach (string servicio in listaServiciosAAgregar)
                {
                    servicios += servicio + "\n";
                }
                if(listaServiciosAAgregar.Count != listaServicios.Count)
                {
                    servicios += "Para el resto de los servicios ya existe una ruta con los mismo datos";
                }
                mensajeServicios = servicios;
                return false; 
            }
            
            return true;
        }

        private List<string> ejecutarExisteRuta()
        {
            SqlCommand command = new SqlCommand();
            command.Connection = Program.conexion();
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = "SELECT * FROM ABSTRACCIONX4.ExisteRuta(@Origen,@Destino,@PPasaje,@PKg,@Servicios)";
            command.CommandTimeout = 0;

            command.Parameters.AddWithValue("@Origen", txtCiudadOrigen.Text);
            command.Parameters.AddWithValue("@Destino", txtCiudadDestino.Text);
            command.Parameters.AddWithValue("@PPasaje", enDecimal(txtPrecioPasaje.Text));
            command.Parameters.AddWithValue("@PKg", enDecimal(txtPrecioEncomienda.Text));
            SqlParameter param = new SqlParameter("@Servicios", SqlDbType.Structured);
            param.TypeName = "ABSTRACCIONX4.Lista";
            param.Value = crearDataTable(listaServicios);
            command.Parameters.Add(param);

            SqlDataReader reader = command.ExecuteReader();
            List<string> listaServiciosAAgregar = new List<string>();

            while (reader.Read())
            {
                listaServiciosAAgregar.Add(reader.GetString(0));
            }

            reader.Close();

            return listaServiciosAAgregar;
        }

        private DataTable crearDataTable(IEnumerable<Object> lista)
        {
            DataTable table = new DataTable();
            table.Columns.Add("elemento", typeof(string));
            foreach (string elemento in lista)
            {
                table.Rows.Add(elemento.ToString());
            }
            return table;
        }

        private Object darDeAltaRuta(List<string> listaServiciosAAgregar)
        {
            SQLManager sqlManager = new SQLManager();
            return sqlManager.generarSP("AltaRuta")
                             .agregarIntSP("@Codigo", txtCodigo)
                             .agregarListaSP("@Servicios", listaServiciosAAgregar)
                             .agregarStringSP("@CiudadOrigen", txtCiudadOrigen)
                             .agregarStringSP("@CiudadDestino", txtCiudadDestino)
                             .agregarDecimalSP("@PrecioPasaje", enDecimal(txtPrecioPasaje.Text))
                             .agregarDecimalSP("@PrecioeEncomienda", enDecimal(txtPrecioEncomienda.Text))
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
            huboErrores = Validacion.igualdadCiudades(txtCiudadDestino , txtCiudadOrigen) || huboErrores;

            return !huboErrores;
        }

        

        private Boolean validarLongitudes()
        {
            Boolean algunoVacio = Validacion.esVacio(txtCodigo, "código" , true);
            algunoVacio = Validacion.listaVacia(listaServicios, "servicios", true) || algunoVacio;
            algunoVacio = Validacion.esVacio(txtCiudadOrigen, "ciudad de origen", true) || algunoVacio;
            algunoVacio =  Validacion.esVacio(txtCiudadDestino, "ciudad de destino" , true) || algunoVacio;
            algunoVacio = Validacion.esVacio(txtPrecioPasaje, "precio de pasaje", true) || algunoVacio;
            algunoVacio =  Validacion.esVacio(txtPrecioEncomienda, "precio de encomienda" , true ) || algunoVacio;
            
            
            return algunoVacio;
        }

        private bool validarTipos()
        {
            Boolean huboErrores = false;

            huboErrores = !Validacion.esNumero(txtCodigo,"código",true) || huboErrores;
            huboErrores = !Validacion.esDecimal(txtPrecioPasaje,"precio de pasaje",true) || huboErrores;
            huboErrores = !Validacion.esDecimal(txtPrecioEncomienda, "precio de encomienda", true) || huboErrores;

            return huboErrores;
        }

        private bool validarLimitesNumericos()
        {
            Boolean huboErrores = false;

            huboErrores = !Validacion.estaEntreLimites(txtCodigo, 1,99999999,false, "código") || huboErrores;
            huboErrores = !Validacion.estaEntreLimites(txtPrecioPasaje, 0.01m, 999,true, "precio de pasaje") || huboErrores;
            huboErrores = !Validacion.estaEntreLimites(txtPrecioEncomienda, 0.01m, 999, true, "precio de encomienda") || huboErrores;

            return huboErrores;
        }

        //Llevan al listado de ciudades para seleccionar los origenes y destinos
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


        // Muestra los servicios elegidos desde el listado de servicios
        internal void serviciosElegidos(List<object> lista)
        {
            label6.Text = "";
            listaServicios.Clear();
            listaServicios.AddRange(lista);

            foreach (Object e in lista)
            {
                label6.Text += (String)e + " - ";
                label6.Visible = true;
            }

            string texto = label6.Text;
            string textoSinBarra = texto.Remove(texto.Length - 2);
            label6.Text = textoSinBarra;
  
        }

        private void botonSelServicios_Click(object sender, EventArgs e)
        {
            Form formularioServicios = new Servicios(this, null,listaServicios);
            formularioServicios.ShowDialog();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}
