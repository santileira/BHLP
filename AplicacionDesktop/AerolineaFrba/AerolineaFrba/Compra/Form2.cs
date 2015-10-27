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
    public partial class Form2 : Form
    {
        public Form anterior;
        public Boolean encontroCliente = false;
        public Boolean actualizarTabla = false;
        public int codigoCliente = 0;

        public int cantidadButacas;

        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            this.inicio();
        }

        public void inicio()
        {
            txtApe.Text = "";
            txtDire.Text = "";
            txtDni.Text = "";

            txtNom.Text = "";
            txtTel.Text = "";
            txtMail.Text = "";

            txtDni.Focus();

            txtApe.Enabled = false;
            txtDire.Enabled = false;
            dp.Enabled = false;
            txtNom.Enabled = false;
            txtTel.Enabled = false;
            txtMail.Enabled = false;

            button1.Enabled = false;

            encontroCliente = false;
            actualizarTabla = false;

            dgButacas.CurrentCell = null;

            dp.Value = DateTime.Now;

            if (this.cantidadButacas == 0)
                button2.Enabled = false;
            else
                button2.Enabled = true;
        }

        private void cambiarVisibilidades(Form formularioSiguiente)
        {
            formularioSiguiente.Visible = true;
            this.Visible = false;
        }
        
        public void seSelecciono(DataGridViewRow registro)
        {
            SQLManager.ejecutarQuery("select * from [ABSTRACCIONX4].butacasDisponibles('" + registro.Cells["VIAJE_COD"].Value.ToString() + "', '" + registro.Cells["AERO_MATRI"].Value.ToString() + "')", dgButacas);
            SQLManager.ejecutarQuery("select * from [ABSTRACCIONX4].kilosDisponibles('" + registro.Cells["VIAJE_COD"].Value.ToString() + "', '" + registro.Cells["AERO_MATRI"].Value.ToString() + "')", dgKilos);

            string kilos = dgKilos.Rows[0].Cells["Kilos"].Value.ToString();

            (((this.anterior as Compra.Form4).anterior as Compra.Form3).anterior as Compra.Form1).completarCantidades(dgButacas.RowCount, kilos);
        }
        
        private void button6_Click(object sender, EventArgs e)
        {
            this.cambiarVisibilidades(this.anterior);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtDire.Enabled = true;
            dp.Enabled = true;
            txtNom.Enabled = true;
            txtTel.Enabled = true;
            txtMail.Enabled = true;

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
            }
            else
            {
                MessageBox.Show("No se encuentra cargado el cliente en la BD. Por favor, ingresar los datos para darle de alta", "Cliente no encontrado", MessageBoxButtons.OK);   
            }
        }


        private void txtDni_TextChanged(object sender, EventArgs e)
        {
            txtApe.Enabled = true;
        }

        private void txtApe_TextChanged(object sender, EventArgs e)
        {
            button1.Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.inicio();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Boolean huboError = this.hacerValidacionesDeTipo();

            if (dp.Value.Year > DateTime.Now.Year && dp.Value.Month > DateTime.Now.Month && dp.Value.Day > DateTime.Now.Day)
            {
                huboError = true;
                MessageBox.Show("La Fecha de Nacimiento debe ser anterior a la fecha actual", "Error en los datos", MessageBoxButtons.OK);
            }

            if (dgButacas.CurrentCell == null)
            {
                huboError = true;
                MessageBox.Show("No ha seleccionado ninguna butaca disponible", "Error en los datos", MessageBoxButtons.OK);
            }
            else
            {
                if (dgButacas.SelectedRows[0].Cells["BUT_NRO"].Style.BackColor == Color.Gray)
                {
                    huboError = true;
                    MessageBox.Show("La butaca seleccionada ya se encuentra ocupada", "Error en los datos", MessageBoxButtons.OK);
                }
   
            }

            string viaje_cod = (((this.anterior as Compra.Form4).anterior as Compra.Form3).anterior as Compra.Form1).viaje;

            if (encontroCliente)
            {
                string fechaSalida = (((this.anterior as Compra.Form4).anterior as Compra.Form3).anterior as Compra.Form1).fechaSalida;
                string fechaLlegada = (((this.anterior as Compra.Form4).anterior as Compra.Form3).anterior as Compra.Form1).fechaLlegada;
                string cli_cod = dgCliente.Rows[0].Cells["CLI_COD"].Value.ToString();
                

                SQLManager.ejecutarQuery("select * from [ABSTRACCIONX4].pasajero_disponible('" + cli_cod + "', '" + viaje_cod + "', '" + fechaSalida + "', '" + fechaLlegada + "')", dgCliente);

                if (dgCliente.RowCount == 0)
                {
                    MessageBox.Show("El cliente no puede realizar un viaje porque ya tiene programado otro viaje en ese periodo", "Error en los datos", MessageBoxButtons.OK);
                    huboError = true;
                }
            }

            if(!huboError)
            {
                MessageBox.Show("Se ha guardado el pasaje", "Pasaje confirmado", MessageBoxButtons.OK);

                string matricula = (((this.anterior as Compra.Form4).anterior as Compra.Form3).anterior as Compra.Form1).matricula;

                if (!this.encontroCliente)
                {

                    new SQLManager().generarSP("ingresarDatosDelCliente")
                          .agregarIntSP("@dni", txtDni)
                            .agregarStringSP("@ape", txtApe)
                              .agregarStringSP("@nombre", txtNom)
                                .agregarStringSP("@direccion", txtDire)
                                   .agregarStringSP("@mail", txtMail)
                                     .agregarFechaSP("@fechanac", dp)
                                        .agregarIntSP("@telefono", txtTel)
                                            .ejecutarSP();

                    dgCliente2.ColumnCount = 13;
                    this.agregarCampos(dgCliente2);

                    dgCliente2.Rows.Add("0", txtDni.Text, txtNom.Text, txtApe.Text, txtDire.Text, txtTel.Text,
                        txtMail.Text, dp.Value.ToString());
                }

                dgButacas.SelectedRows[0].Cells["BUT_NRO"].Style.BackColor = Color.Gray;
                dgButacas.SelectedRows[0].Cells["BUT_TIPO"].Style.BackColor = Color.Gray;

                if (this.encontroCliente)
                    (this.anterior as Compra.Form4).agregarPasaje(dgCliente.Rows[0], dgButacas.SelectedRows[0].Cells["BUT_NRO"].Value.ToString(), dgButacas.SelectedRows[0].Cells["BUT_TIPO"].Value.ToString(), this.calcularImporte(), actualizarTabla, viaje_cod, matricula);
                else
                    (this.anterior as Compra.Form4).agregarPasaje(dgCliente2.Rows[0], dgButacas.SelectedRows[0].Cells["BUT_NRO"].Value.ToString(), dgButacas.SelectedRows[0].Cells["BUT_TIPO"].Value.ToString(), this.calcularImporte(), actualizarTabla, viaje_cod, matricula);

                this.cantidadButacas -= 1;

                if (this.actualizarTabla)
                {
                    new SQLManager().generarSP("actualizarDatosDelCliente")
                          .agregarIntSP("@dni",txtDni)
                            .agregarStringSP("@ape",txtApe)
                              .agregarStringSP("@nombre", txtNom)
                                .agregarStringSP("@direccion",txtDire)
                                   .agregarStringSP("@mail",txtMail)
                                     .agregarFechaSP("@fechanac",dp)
                                       .agregarIntSP("@telefono",txtTel)
                                            .ejecutarSP();
                }



                this.inicio();
            }
        }

        private string calcularImporte()
        {
            string origen = (((this.anterior as Compra.Form4).anterior as Compra.Form3).anterior as Compra.Form1).origen;
            string destino = (((this.anterior as Compra.Form4).anterior as Compra.Form3).anterior as Compra.Form1).destino;

            SQLManager.ejecutarQuery("select * from [ABSTRACCIONX4].importePasaje('" + origen + "', '" + destino + "')", dgImporte);

            return dgImporte.Rows[0].Cells["IMPORTE"].Value.ToString();
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
            validacion = Validacion.esVacio(txtTel, "Telefono", true) || validacion;
            validacion = Validacion.esVacio(txtDire, "Direccion", true) || validacion;
            validacion = Validacion.esVacio(txtMail, "Mail", true) || validacion;
            validacion = Validacion.esVacio(txtNom, "Nombre", true) || validacion;
            validacion = Validacion.esVacio(txtApe, "Apellido", true) || validacion;
            validacion = !Validacion.numeroCorrecto(txtDni, "DNI", false) || validacion;
            validacion = !Validacion.numeroCorrecto(txtTel, "Telefono", false) || validacion;
            validacion = !Validacion.esTexto(txtDire, "Direccion", true) || validacion;
            validacion = !Validacion.esTexto(txtMail, "Mail", true) || validacion;
            validacion = !Validacion.esTexto(txtNom, "Nombre", true) || validacion;
            validacion = !Validacion.esTexto(txtApe, "Apellido", true) || validacion;

            return validacion;
        }

        public void liberarButaca(string but)
        {
            foreach (DataGridViewRow row in dgButacas.Rows)
            {
                if (row.Cells["BUT_NRO"].Value.ToString() == but)
                {
                    row.Cells["BUT_NRO"].Style.BackColor = Color.White;
                    row.Cells["BUT_TIPO"].Style.BackColor = Color.White;
                }
            }

            this.cantidadButacas += 1;
        }

        private void dgButacas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

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

        private void txtNac_TextChanged(object sender, EventArgs e)
        {
            this.hayQueActualizarTabla();
        }

        private void txtMail_TextChanged(object sender, EventArgs e)
        {
            this.hayQueActualizarTabla();
        }

        private void hayQueActualizarTabla()
        {
            if (encontroCliente)
                actualizarTabla = true;
        }

        private void dp_ValueChanged(object sender, EventArgs e)
        {
            this.hayQueActualizarTabla();
        }

    }
}
