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

namespace AerolineaFrba.Compra
{
    public partial class Form5 : Form
    {
        public Form anterior;
        public Boolean encontroCliente = false;
        public Boolean actualizarTabla = false;
        public int codigoCliente = 0;

        public double cantidadKilos;

        public Form5()
        {
            InitializeComponent();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            this.inicio();
        }

        public void liberarEspacio(string kilos)
        {
            double kg;
            double.TryParse(kilos, out kg);

            this.cantidadKilos += kg;
            this.inicio();
        }

        public void inicio()
        {
            txtApe.Text = "";
            txtDire.Text = "";
            txtDni.Text = "";
            txtKilos.Text = "";
            txtNom.Text = "";
            txtTel.Text = "";
            txtMail.Text = "";
            labelRestantes.Text = "KG Restantes: " + (anterior as Form4).kilosRestantes().ToString();

            txtDni.Enabled = true;
            txtDni.Focus();
            

            txtApe.Enabled = false;
            txtDire.Enabled = false;
            dp.Enabled = false;
            txtNom.Enabled = false;
            txtTel.Enabled = false;
            txtMail.Enabled = false;
            txtKilos.Enabled = false;

            button1.Enabled = false;

            encontroCliente = false;
            actualizarTabla = false;

            dp.Value = Program.fechaHoy();

            if (this.cantidadKilos == 0)
                button2.Enabled = false;
            else
                button2.Enabled = true;
        }

        /*
         * Metodo para buscar al cliente en la base de datos. Si existe, autocompletara los text box restantes.
         */
        private void button1_Click(object sender, EventArgs e)
        {
            Boolean validacion = false;
            validacion = Validacion.esVacio(txtDni, "DNI", true) || validacion;
            validacion = Validacion.esVacio(txtApe, "Apellido", true) || validacion;

            validacion = !Validacion.esNumero(txtDni, "DNI", true) || validacion;
            validacion = !Validacion.esSoloTexto(txtApe, "Apellido", true) || validacion;

            validacion = !Validacion.estaEntreLimites(txtDni, 1, 999999999, false, "DNI") || validacion;

            if (validacion)
            {
                return;
            }
            button2.Enabled = true;
            txtDire.Enabled = true;
            dp.Enabled = true;
            txtNom.Enabled = true;
            txtTel.Enabled = true;
            txtMail.Enabled = true;
            txtKilos.Enabled = true;


            SQLManager.ejecutarQuery("select * from [ABSTRACCIONX4].buscarCliente('" + txtDni.Text + "', '" + txtApe.Text + "')", dgCliente);

            if (dgCliente.RowCount == 1)
            {
                encontroCliente = true;

                txtDire.Text = dgCliente.Rows[0].Cells["CLI_DIRECCION"].Value.ToString();
                dp.Value = (DateTime)dgCliente.Rows[0].Cells["CLI_FECHA_NAC"].Value;
                txtNom.Text = dgCliente.Rows[0].Cells["CLI_NOMBRE"].Value.ToString();
                txtTel.Text = dgCliente.Rows[0].Cells["CLI_TELEFONO"].Value.ToString();
                txtMail.Text = dgCliente.Rows[0].Cells["CLI_MAIL"].Value.ToString();

                codigoCliente = (int)dgCliente.Rows[0].Cells["CLI_COD"].Value;

                txtDni.Enabled = false;
            }
            else
            {
                SqlDataReader varCli = this.tieneDocumento(txtDni.Text);
                
                varCli.Read();

                if (varCli.HasRows)
                {
                    button2.Enabled = false;
                    txtDire.Enabled = false;
                    dp.Enabled = false;
                    txtNom.Enabled = false;
                    txtTel.Enabled = false;
                    txtMail.Enabled = false;
                    txtKilos.Enabled = false;
                    MessageBox.Show("Dni inválido. Ya existe un Cliente con ese DNI", "Error cliente", MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show("No se encuentra cargado el cliente en la BD. Por favor, ingresar los datos para darle de alta", "Cliente no encontrado", MessageBoxButtons.OK);
                }
            }
        }

        private void hayQueActualizarTabla()
        {
            if (encontroCliente)
                actualizarTabla = true;
        }

        private void txtDni_TextChanged(object sender, EventArgs e)
        {
            txtApe.Enabled = true;
        }

        private void txtApe_TextChanged(object sender, EventArgs e)
        {
            button1.Enabled = true;
        }

        private void txtNom_TextChanged(object sender, EventArgs e)
        {
            this.hayQueActualizarTabla();
        }

        private void txtDire_TextChanged(object sender, EventArgs e)
        {
            this.hayQueActualizarTabla();
        }

        private void txtTel_TextChanged(object sender, EventArgs e)
        {
            this.hayQueActualizarTabla();
        }

        private void dp_ValueChanged(object sender, EventArgs e)
        {
            this.hayQueActualizarTabla();
        }

        private void txtMail_TextChanged(object sender, EventArgs e)
        {
            this.hayQueActualizarTabla();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.inicio();
        }

        /*
         * Metodo agrega al listado de encomiendas del form 4, la encomienda para el cliente ingresado
         */
        private void button2_Click(object sender, EventArgs e)
        {
            Boolean huboError = this.hacerValidacionesDeTipo();

            if (txtDni.Text.Length > 0 && txtApe.Text.Length > 0)
            {
                SqlDataReader varCli = this.tieneDocumento(txtDni.Text);

                varCli.Read();
                if (varCli.HasRows)
                {
                    MessageBox.Show("Dni inválido. Ya existe un Cliente con ese DNI", "Error cliente", MessageBoxButtons.OK);
                    huboError = true;
                }
            }
            
            if (dp.Value.CompareTo(Program.fechaHoy()) > 0)
            {
                huboError = true;
                MessageBox.Show("La Fecha de Nacimiento debe ser anterior a la fecha actual", "Error en los datos", MessageBoxButtons.OK);
            }

            double kg = 0;
            if (Validacion.esDecimal(txtKilos, "Kilos de encomienda", false))
            {
                double.TryParse(txtKilos.Text.Replace(".",","), out kg);

                if (Convert.ToDecimal(kg) > (anterior as Form4).kilosRestantes())
                {
                    huboError = true;
                    //MessageBox.Show("Se ha ingresado un numero de kilos mayor al disponible en la aeronave", "Error en los datos", MessageBoxButtons.OK);
                }
            }
            else
                huboError = true;

            if ((this.anterior as Compra.Form4).seRegistroEncomiendaDelCliente(txtDni.Text, txtApe.Text))
            {
                MessageBox.Show("Ya se selecciono una encomienda para el cliente en este vuelo", "Error en los datos", MessageBoxButtons.OK);
                huboError = true;
            }

            if (!huboError)
            {
                MessageBox.Show("Se ha guardado la encomienda", "Encomienda confirmada", MessageBoxButtons.OK);

                if (!this.encontroCliente)
                {

                    dgCliente2.ColumnCount = 13;
                    this.agregarCampos(dgCliente2);

                    dgCliente2.Rows.Add("0", txtDni.Text, txtNom.Text, txtApe.Text, txtDire.Text, txtTel.Text, 
                        txtMail.Text, dp.Value.ToString());
                }

                string viaje_cod = (((this.anterior as Compra.Form4).anterior as Compra.Form3).anterior as Compra.Form1).viaje;
                string matricula = (((this.anterior as Compra.Form4).anterior as Compra.Form3).anterior as Compra.Form1).matricula;
                string textoKilos = Decimal.Round(Convert.ToDecimal(txtKilos.Text.Replace(".", ",")), 2).ToString();

                if(this.encontroCliente)
                    if (this.actualizarTabla)
                    {
                        (this.anterior as Compra.Form4).agregarEncomienda(dgCliente.Rows[0].Cells["CLI_COD"].Value.ToString(), txtDni.Text, txtNom.Text, txtApe.Text, txtDire.Text, txtTel.Text, txtMail.Text, dp.Text, textoKilos, this.calcularImporte(), actualizarTabla, encontroCliente, viaje_cod, matricula);
                    }else{
                        (this.anterior as Compra.Form4).agregarEncomienda(dgCliente.Rows[0], textoKilos, this.calcularImporte(), actualizarTabla, encontroCliente, viaje_cod, matricula);
                    }
                else
                    (this.anterior as Compra.Form4).agregarEncomienda(dgCliente2.Rows[0], textoKilos, this.calcularImporte(), actualizarTabla, encontroCliente, viaje_cod, matricula);

                this.cantidadKilos -= kg;

                this.inicio();
                if((anterior as Form4).kilosRestantes() == 0)
                    this.cambiarVisibilidades(this.anterior);
            }
        }

        private SqlDataReader tieneDocumento(string dni)
        {
            SqlDataReader varCli;
            SqlCommand consulta = new SqlCommand();
            consulta.CommandType = CommandType.Text;
            consulta.CommandText = "select 1 from [ABSTRACCIONX4].CLIENTES WHERE CLI_DNI =" + dni + " AND CLI_APELLIDO !='" + txtApe.Text + "'";
            consulta.Connection = Program.conexion();
            varCli = consulta.ExecuteReader();
            return varCli;
        }

        private string calcularImporte()
        {
            string origen = (((this.anterior as Compra.Form4).anterior as Compra.Form3).anterior as Compra.Form1).origen;
            string destino = (((this.anterior as Compra.Form4).anterior as Compra.Form3).anterior as Compra.Form1).destino;

            SQLManager.ejecutarQuery("select * from [ABSTRACCIONX4].importeEncomienda(" + txtKilos.Text.Replace(",",".") + ", '" + origen + "', '" + destino + "')", dgImporte);

            return dgImporte.Rows[0].Cells["Importe"].Value.ToString();
        }

        private void agregarCampos(DataGridView unDg)
        {
            unDg.Columns[0].Name = "CLI_COD";
            unDg.Columns[1].Name = "CLI_DNI";
            unDg.Columns[2].Name = "CLI_NOMBRE";
            unDg.Columns[3].Name = "CLI_APELLIDO";
            unDg.Columns[4].Name = "CLI_DIRECCION";
            unDg.Columns[5].Name = "CLI_TELEFONO";
            unDg.Columns[6].Name = "CLI_MAIL";
            unDg.Columns[7].Name = "CLI_FECHA_NAC";
        }

        private Boolean hacerValidacionesDeTipo()
        {
            Boolean validacion = Validacion.esVacio(txtDni, "DNI", true);
            validacion = Validacion.esVacio(txtApe, "Apellido", true) || validacion;
            validacion = Validacion.esVacio(txtNom, "Nombre", true) || validacion;
            validacion = Validacion.esVacio(txtDire, "Dirección", true) || validacion;
            validacion = Validacion.esVacio(txtTel, "Teléfono", true) || validacion;
            validacion = Validacion.esVacio(txtMail, "Mail", true) || validacion;
            validacion = Validacion.esVacio(txtKilos, "Kilos de encomienda", true) || validacion;

            validacion = !Validacion.esNumero(txtDni, "DNI", true) || validacion;
            validacion = !Validacion.esSoloTexto(txtApe, "Apellido", true) || validacion;
            validacion = !Validacion.esSoloTexto(txtNom, "Nombre", true) || validacion;
            validacion = !Validacion.esTexto(txtDire, "Dirección", true) || validacion;
            validacion = !Validacion.esNumero(txtTel, "Teléfono", true) || validacion;
            validacion = !Validacion.esTexto(txtMail, "Mail", true) || validacion;
            validacion = !Validacion.esDecimal(txtKilos, "Kilos de encomienda", true) || validacion;

            validacion = !Validacion.estaEntreLimites(txtDni, 1, 999999999, false, "DNI") || validacion;
            validacion = !Validacion.estaEntreLimites(txtTel, 1, 999999999,false,"Teléfono") || validacion;
            validacion = !Validacion.estaEntreLimites(txtKilos,0.01m, (anterior as Form4).kilosRestantes(),true,"Kilos de encomienda") || validacion;

            return validacion;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.cambiarVisibilidades(this.anterior);
        }

        private void cambiarVisibilidades(Form formularioSiguiente)
        {
            formularioSiguiente.Visible = true;
            this.Visible = false;
        }

       
    }
}
