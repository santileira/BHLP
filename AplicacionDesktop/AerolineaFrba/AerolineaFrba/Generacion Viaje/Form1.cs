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

namespace AerolineaFrba.Generacion_Viaje
{
    public partial class Form1 : Form
    {
        public Abm_Aeronave.Listado listadoAeronaves;
        public Abm_Ruta.Listado listadoRutas;

        Boolean sePuedeGuardar = false;
        Boolean primeraVez = true;

        Form formularioSiguiente;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.inicio();
        }

        private void inicio()
        {
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "dd/MM/yyyy - HH:mm";
            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.CustomFormat = "dd/MM/yyyy - HH:mm";

            dateTimePicker1.Value = Program.fechaHoy();
            dateTimePicker2.Value = Program.fechaHoy();

            txtMatricula.Text = "";
            txtRuta.Text = "";
            txtMatricula.Enabled = false;

            this.listadoAeronaves.serv_cod = null;
            this.listadoRutas.serv_cod = null;

            sePuedeGuardar = false;
        }

        /*
         * Metodo que se invoca desde el listado de aeronave cuando se selecciona una aeronave.
         * Su objetivo es completar el txtMatricula con la matricula seleccionada
         */
        public void seSeleccionoAeronave(DataGridViewRow registro)
        {
            txtMatricula.Text = registro.Cells["Matrícula"].Value.ToString();
            this.listadoRutas.serv_cod = registro.Cells["Tipo de servicio"].Value.ToString();
        }

        /*
         * Metodo que se invoca desde el listado de rutas cuando se selecciona una ruta aerea.
         * Su objetivo es completar el txtRuta con la ruta seleccionada
         */
        public void seSeleccionoRuta(DataGridViewRow registro)
        {
            button5.Enabled = true;
            txtMatricula.Text = "";
            txtRuta.Text = registro.Cells["Id"].Value.ToString();
            this.listadoAeronaves.serv_cod = registro.Cells["Codigo Serv"].Value.ToString();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (this.fechasErroneas())
                MessageBox.Show("Verifique que las fechas de salida y llegada ingresadas sean correctas", "Error en los datos de entrada", MessageBoxButtons.OK);
            else if (DateTime.Compare(Program.fechaHoy(), dateTimePicker1.Value) == 1)
                MessageBox.Show("La fecha de salida no puede ser anterior a la fecha de hoy");
            else if (DateTime.Compare(Program.fechaHoy(), dateTimePicker2.Value) == 1)
                MessageBox.Show("La fecha de llegada estimada no puede ser anterior a la fecha de hoy");
            else
            {
                
                this.listadoAeronaves.queryViajes = " WHERE ABSTRACCIONX4.aeronave_disponible(AERO_MATRI, '"
                + dateTimePicker1.Value + "', '" + dateTimePicker2.Value + "') = 1 ";

                this.listadoAeronaves.queryViajes += " and [ABSTRACCIONX4].aeronave_en_servicio(AERO_MATRI, '"
                + dateTimePicker1.Value + "', '" + dateTimePicker2.Value + "') = 1 ";
                
                if(txtRuta.Text != "")
                    this.listadoAeronaves.queryViajes += " and [ABSTRACCIONX4].sigue_la_ruta(AERO_MATRI, '" + txtRuta.Text + "', '"
                    + dateTimePicker1.Value + "', '" + dateTimePicker2.Value + "') = 1 ";

                this.listadoAeronaves.queryViajes += " and [ABSTRACCIONX4].datetime_is_between(AERO_FECHA_ALTA, '"
                + dateTimePicker1.Value + "', [ABSTRACCIONX4].FechaReinicioOMaxima(NULL)) = 0 ";

                this.listadoAeronaves.queryViajes += " and (select count(*) from [ABSTRACCIONX4].VIAJES v " +
                    "where v.AERO_MATRI=a.AERO_MATRI and [ABSTRACCIONX4].datetime_is_between(VIAJE_FECHA_SALIDA, '" + dateTimePicker1.Value + "',[ABSTRACCIONX4].FechaReinicioOMaxima(NULL))=1) = 0 ";

                this.listadoAeronaves.extenderQuery();
                this.listadoAeronaves.ejecutarConsulta();
                this.cambiarVisibilidades(this.listadoAeronaves);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.listadoRutas.serv_cod = null;
            this.listadoRutas.generarQueryInicial();
            this.listadoRutas.ejecutarQuery();
            this.cambiarVisibilidades(this.listadoRutas);
        }

        private void cambiarVisibilidades(Form formularioSiguiente)
        {
            formularioSiguiente.Visible = true;
            this.Visible = false;
        }


        /*
         * Metodo que retorna true si hubo algun valor erroneo o no esperado relacionado a
         * las fechas de salida y llegada de la aeronave. Retorna false si las fechas ingresadas son correctas
         */
        private Boolean fechasErroneas()
        {
            Boolean huboError = false;

            if (DateTime.Compare(dateTimePicker1.Value, dateTimePicker2.Value) == 1)
            {
                huboError = true;
                MessageBox.Show("La fecha de llegada no puede ser anterior a la fecha de salida");
            }

            TimeSpan diferencia = dateTimePicker2.Value - dateTimePicker1.Value;

            if (diferencia.Days >= 1)
            {
                huboError = true;
                MessageBox.Show("Las aeronaves tardan como mucho 24 hs en llegar a destino");
            }
            else if (DateTime.Compare(dateTimePicker1.Value, dateTimePicker2.Value) == 0)
            {
                huboError = true;
                MessageBox.Show("Las fechas de salida y llegada no pueden ser iguales");
            }

            if (DateTime.Compare(Program.fechaHoy(), dateTimePicker1.Value) == 1)
            {
                huboError = true;
                MessageBox.Show("La fecha de salida no puede ser anterior a la fecha de hoy"); 
            }

            if (DateTime.Compare(Program.fechaHoy(), dateTimePicker2.Value) == 1)
            {
                huboError = true;
                MessageBox.Show("La fecha de llegada no puede ser anterior a la fecha de hoy");
            }

            return huboError;
        }


        private void button3_Click(object sender, EventArgs e)
        {
            this.inicio();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Boolean huboError = false;
             
            if (txtMatricula.Text == "")
            {
                huboError = true;
                MessageBox.Show("Debe seleccionar una aeronave", "Error en los datos de entrada", MessageBoxButtons.OK);
            }
            if (txtRuta.Text == "")
            {
                huboError = true;
                MessageBox.Show("Debe seleccionar una ruta aerea", "Error en los datos de entrada", MessageBoxButtons.OK);
            }
            if (this.fechasErroneas())
            {
                huboError = true;
                MessageBox.Show("Verifique que las fechas de salida y llegada ingresadas sean correctas", "Error en los datos de entrada", MessageBoxButtons.OK);
            }
            
            if(!huboError)
            {
                if (this.insertarNuevoViaje() != null)
                {
                    MessageBox.Show("Se inserto correctamente el nuevo viaje, el cual ya se encuentra disponible para la venta de pasajes", "Nuevo viaje", MessageBoxButtons.OK);
                    this.Close();
                }
            }
        }

        /*
         * Metodo que se encarga de invocar al SQLManager para armar la cadena que ejecuta
         * la procedure GenerarNuevoViaje, el cual inserta el nuevo registro en la tabla de viajes,
         * siempre y cuando se cumplan las restricciones del formulario, y siempre que la insercion
         * no haya sido fallida. En caso de no poder insertarse el registro (ya sea por fallo de constrains,
         * o algun otro fallo relacionado a la integridad de los datos), se lanzara una excepcion que se
         * cacheada en la aplicacion
         */
        private Object insertarNuevoViaje()
        {
            SQLManager manejador = new SQLManager();
            manejador.generarSP("GenerarNuevoViaje");
            manejador.agregarStringSP("@salida", dateTimePicker1.Value.ToString());
            manejador.agregarStringSP("@llegadaEstimada", dateTimePicker2.Value.ToString());
            manejador.agregarIntSP("@ruta", txtRuta);
            manejador.agregarStringSP("@matricula", txtMatricula.Text);

            try
            {
                manejador.ejecutarSP();
                return 1;
            }
            catch (System.Exception e)
            {
                MessageBox.Show(e.Message, "Erro al generar el nuevo viaje", MessageBoxButtons.OK);
                return null;
            }   
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }


    }
}
