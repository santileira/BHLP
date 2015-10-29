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
    public partial class Form6 : Form
    {
        public Form anterior;
        public DataGridView pasajes;
        public DataGridView encomiendas;
        private bool encontroCliente;
        private bool actualizarTabla;

        public Form6()
        {
            InitializeComponent();

            //
            // Carga del contenido de combos
            //

            SqlDataReader varTarjeta;
            SqlCommand consultaColumnas = new SqlCommand();
            consultaColumnas.CommandType = CommandType.Text;
            consultaColumnas.CommandText = "SELECT TIPO_DESC FROM [ABSTRACCIONX4].TIPOS";
            consultaColumnas.Connection = Program.conexion();
            varTarjeta = consultaColumnas.ExecuteReader();

            while (varTarjeta.Read())
            {
                this.cboTipoTarjeta.Items.Add(varTarjeta.GetValue(0));
            }

            int anioActual = (int)DateTime.Now.Year;            
            for(int i=0;i<10;i++){
                cboAnios.Items.Add(anioActual+i);
            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboFormaPago.SelectedItem.ToString() == "Tarjeta de crédito")
            {
                groupBox3.Visible = true;
            }
            else
            {
                groupBox3.Visible = false;
            }
                
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.cambiarVisibilidades(anterior);
        }

        private void cambiarVisibilidades(Form formularioSiguiente)
        {
            formularioSiguiente.Visible = true;
            this.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtDire.Enabled = true;
            dp.Enabled = true;
            txtNom.Enabled = true;
            txtTel.Enabled = true;
            txtMail.Enabled = true;

            SqlDataReader reader;
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "select * from [ABSTRACCIONX4].buscarCliente(@dni,@ape)";
            command.Connection = Program.conexion();

            command.Parameters.AddWithValue("@dni", txtDni.Text);
            command.Parameters.AddWithValue("@ape", txtApe.Text);

            reader = command.ExecuteReader();

            reader.Read();

            if (reader.HasRows)
            {
                encontroCliente = true;

                txtNom.Text = reader.GetValue(2).ToString();
                txtDire.Text = reader.GetValue(4).ToString();
                txtTel.Text = reader.GetValue(5).ToString();
                txtMail.Text = reader.GetValue(6).ToString();
                dp.Value = (DateTime)reader.GetValue(7);

            }
            else
            {
                MessageBox.Show("No se encuentra cargado el cliente en la BD. Por favor, ingresar los datos para darle de alta", "Cliente no encontrado", MessageBoxButtons.OK);
            }


        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlDataReader varCuotas;
            SqlCommand consulta = new SqlCommand();
            consulta.CommandType = CommandType.Text;
            consulta.CommandText = "SELECT TIPO_CUO FROM [ABSTRACCIONX4].TIPOS WHERE TIPO_DESC='" + cboTipoTarjeta.SelectedItem.ToString() + "'";
            consulta.Connection = Program.conexion();
            varCuotas = consulta.ExecuteReader();

            varCuotas.Read();

            if ((bool)varCuotas.GetValue(0))
            {
                ckPagaCuotas.Visible = true;
            }
            else
            {
                ckPagaCuotas.Visible = false;
            }
        }

        private void Form6_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (sePuedeEfectuarLaCompra())
            {
                /*
                if (!encontroCliente)
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
                }

                if (actualizarTabla)
                {
                    new SQLManager().generarSP("actualizarDatosDelCliente")
                         .agregarIntSP("@dni", txtDni)
                           .agregarStringSP("@ape", txtApe)
                             .agregarStringSP("@nombre", txtNom)
                               .agregarStringSP("@direccion", txtDire)
                                  .agregarStringSP("@mail", txtMail)
                                    .agregarFechaSP("@fechanac", dp)
                                      .agregarIntSP("@telefono", txtTel)
                                           .ejecutarSP();
                }
                */
                string codigoPNR = CreatePNR(10);
                cargarDatosDeCompra(codigoPNR);
                cargarDatosDePasajes(codigoPNR);
                cargarDatosDeEncomiendas(codigoPNR);

                MessageBox.Show("Se realizo la compra con éxito", "Compra de pasajes y/o encomiendas", MessageBoxButtons.OK);

                Form formularioSiguiente = new Menu();
                this.cambiarVisibilidades(formularioSiguiente);
            }
            else
            {
                MessageBox.Show("No es posible efectuar la compra", "Compra de pasajes y/o encomiendas", MessageBoxButtons.OK);
            }

            
        }

        private bool sePuedeEfectuarLaCompra()
        {
            return true;
        }

        private void cargarDatosDePasajes(string codigoPNR)
        {
            DataTable dt = new DataTable();
            dt = pasajes.DataSource as DataTable;

            /*dt.Columns.Add("CLI_COD", typeof(string));

            dt.Columns.Add("CLI_DNI", typeof(int));*/

            MessageBox.Show(((DataTable)pasajes.DataSource).Columns[0].Caption, "Co", MessageBoxButtons.OK);
                MessageBox.Show(dt.Columns[0].Caption, "Co", MessageBoxButtons.OK);
           
            
   
            
            new SQLManager().generarSP("ingresarDatos")
                             .agregarTableSP("@TablaPasajes", dt)
                                .ejecutarSP();
            /*
            int cliCod;
            int viajeCod;
            decimal pasajePrecio;
            int butNro;

            foreach (DataGridViewRow row in pasajes.Rows)
            {
              
                int.TryParse(row.Cells["CLI_COD"].Value.ToString(), out cliCod);
                int.TryParse(row.Cells["VIAJE_COD"].Value.ToString(), out viajeCod);
                decimal.TryParse(row.Cells["IMPORTE"].Value.ToString(), out pasajePrecio);
                int.TryParse(row.Cells["BUTACA"].Value.ToString(), out butNro);

                new SQLManager().generarSP("ingresarDatosDePasajes")
                          .agregarIntSP("@cliCod", cliCod)
                            .agregarIntSP("@viajeCod", viajeCod)
                              .agregarDecimalSP("@pasajePrecio", pasajePrecio)
                                .agregarFechaSP("@pasajeFechaCompra", DateTime.Now)
                                   .agregarIntSP("@butNro", butNro)
                                     .agregarStringSP("@aeroMatri", row.Cells["MATRICULA"].Value.ToString())
                                           .ejecutarSP();

            }*/
        }

        private void cargarDatosDeEncomiendas(string codigoPNR)
        {/*
            int cliCod;
            int viajeCod;
            decimal encomiendaPrecio;
            decimal encomiendaPesoKG;

            foreach (DataGridViewRow row in encomiendas.Rows)
            {

                int.TryParse(row.Cells["CLI_COD"].Value.ToString(), out cliCod);
                int.TryParse(row.Cells["VIAJE_COD"].Value.ToString(), out viajeCod);
                decimal.TryParse(row.Cells["IMPORTE"].Value.ToString(), out encomiendaPrecio);
                decimal.TryParse(row.Cells["KILOS"].Value.ToString(), out encomiendaPesoKG);

                

                new SQLManager().generarSP("ingresarDatosDeEncomiendas")                    
                          .agregarIntSP("@cliCod", cliCod)
                            .agregarIntSP("@viajeCod", viajeCod)
                              .agregarDecimalSP("@encomiendaPrecio", encomiendaPrecio)
                                .agregarFechaSP("@encomiendaFechaCompra", DateTime.Now)
                                   .agregarDecimalSP("@encomiendaPesoKG", encomiendaPesoKG)
                                     .agregarStringSP("@aeroMatri", row.Cells["MATRICULA"].Value.ToString())
                                           .ejecutarSP();
            
            }*/
        }

        private void cargarDatosDeCompra(string codigoPNR)
        {
            /*
            new SQLManager().generarSP("ingresarDatosDeCompra") */
        }


        public string CreatePNR(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
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
