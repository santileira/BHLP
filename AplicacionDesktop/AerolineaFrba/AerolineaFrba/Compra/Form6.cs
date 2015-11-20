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
        private bool tarjetaNueva;
        private bool esEfectivo;

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

            int anioActual = (int)Program.fechaHoy().Year;            
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
            /*SqlDataReader varCuotas;
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
            }*/
        }

        private void Form6_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            bool huboError = this.hacerValidacionesDeTipo();

            if (!huboError)
            {

                if (sePuedeEfectuarLaCompra())
                {

                    string codigoPNR = CreatePNR(10);
                    cargarDatosDeCompra(codigoPNR);

                    MessageBox.Show("Se realizo la compra con éxito", "Compra de pasajes y/o encomiendas", MessageBoxButtons.OK);

                    this.Close();

                }
                else
                {
                    MessageBox.Show("Los datos de la tarjeta son inválidos", "Compra de pasajes y/o encomiendas", MessageBoxButtons.OK);
                }

            }
            
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
            validacion = !Validacion.estaSeleccionado(cboFormaPago,true) || validacion;

            if (!cboFormaPago.SelectedIndex.Equals(-1))
            {

                if (cboFormaPago.SelectedItem.ToString() == "Tarjeta de crédito")
                {
                    validacion = Validacion.esVacio(txtCodSeg, "Cod. Seg.", true) || validacion;
                    validacion = Validacion.esVacio(txtNroTarjeta, "Nro. Tarjeta", true) || validacion;
                    validacion = !Validacion.estaSeleccionado(cboAnios, true) || validacion;
                    validacion = !Validacion.estaSeleccionado(cboMeses, true) || validacion;
                    validacion = !Validacion.estaSeleccionado(cboTipoTarjeta, true) || validacion;
                    validacion = !Validacion.numeroCorrecto(txtNroTarjeta, "Nro. Tarjeta", true) || validacion;
                    validacion = !Validacion.esNumero(txtCodSeg, "Cod. Seg.", true) || validacion;
                }
            }

            return validacion;
        }

        private bool sePuedeEfectuarLaCompra()
        {
            int nroTarjeta;
            int.TryParse(txtNroTarjeta.Text, out nroTarjeta);

            SqlDataReader reader;
            SqlCommand query = new SqlCommand();
            query.CommandType = CommandType.Text;
            query.CommandText = "SELECT * FROM [ABSTRACCIONX4].[TARJETAS] WHERE TARJ_NRO = " + nroTarjeta + "";
            query.Connection = Program.conexion();

            reader = query.ExecuteReader();
            reader.Read();

            if (reader.HasRows)
            {
                if (this.esTarjetaValida())
                {
                    tarjetaNueva = false;
                    esEfectivo = false;
                    return true;
                }
                else
                {
                    return false;
                }

            }

            if (cboFormaPago.Text.Equals("Tarjeta de crédito"))
            {
                tarjetaNueva = true;
            }else
            {
                tarjetaNueva = false;
                esEfectivo = true;
            }

            return true;
        }

        private bool esTarjetaValida()
        {
            
            int nroTarjeta;
            int vtoMes;
            int vtoAnios;
            int codSeg;

            int.TryParse(txtNroTarjeta.Text, out nroTarjeta);
            int.TryParse(cboMeses.Text, out vtoMes);
            int.TryParse(cboAnios.Text, out vtoAnios);
            int.TryParse(txtCodSeg.Text, out codSeg);


            SqlCommand command = new SqlCommand();
            command.Connection = Program.conexion();
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = "SELECT ABSTRACCIONX4.datosValidosDeTarjeta(@tarjNro , @tarjVtoMes, @tarjVtoAnio, @tarjCodSeg, @tarjTipo)";
            command.CommandTimeout = 0;

            command.Parameters.AddWithValue("@tarjNro", nroTarjeta);
            command.Parameters.AddWithValue("@tarjVtoMes", vtoMes);
            command.Parameters.AddWithValue("@tarjVtoAnio", vtoAnios);
            command.Parameters.AddWithValue("@tarjCodSeg", codSeg);
            command.Parameters.AddWithValue("@tarjTipo", cboTipoTarjeta.Text);

            return (bool)command.ExecuteScalar();
        }

        private void cargarDatosDeCompra(string codigoPNR)
        {
            int cliCod;
            int viajeCod;
            int dni;
            int tel;
            DateTime fechaNac;
            decimal precio;
            decimal peso;
            int butNro;
            int encontrado;
            int actualizar;
            int dniTxt;


            int.TryParse(txtDni.Text, out dniTxt);

            DataTable tablaPasajes = new DataTable();
            
            tablaPasajes.Columns.Add("CLI_COD", typeof(int));
            tablaPasajes.Columns.Add("CLI_DNI", typeof(int));
            tablaPasajes.Columns.Add("CLI_NOMBRE", typeof(string));
            tablaPasajes.Columns.Add("CLI_APELLIDO", typeof(string));
            tablaPasajes.Columns.Add("CLI_DIRECCION", typeof(string));
            tablaPasajes.Columns.Add("CLI_TELEFONO", typeof(int));
            tablaPasajes.Columns.Add("CLI_MAIL", typeof(string));
            tablaPasajes.Columns.Add("CLI_FECHA_NAC", typeof(DateTime));
            tablaPasajes.Columns.Add("VIAJE_COD", typeof(int));
            tablaPasajes.Columns.Add("IMPORTE", typeof(decimal));
            tablaPasajes.Columns.Add("BUTACA", typeof(int));
            tablaPasajes.Columns.Add("MATRICULA", typeof(string));
            tablaPasajes.Columns.Add("ENCONTRADO", typeof(bool));
            tablaPasajes.Columns.Add("ACTUALIZAR", typeof(bool));
            tablaPasajes.Columns.Add("ES_COMPRADOR", typeof(int));
                       

            foreach (DataGridViewRow row in pasajes.Rows)
            {
                int.TryParse(row.Cells["CLI_COD"].Value.ToString(), out cliCod);
                int.TryParse(row.Cells["VIAJE_COD"].Value.ToString(), out viajeCod);
                int.TryParse(row.Cells["CLI_DNI"].Value.ToString(), out dni);
                int.TryParse(row.Cells["CLI_TELEFONO"].Value.ToString(), out tel);
                DateTime.TryParse(row.Cells["CLI_FECHA_NAC"].Value.ToString(), out fechaNac);
                decimal.TryParse(row.Cells["IMPORTE"].Value.ToString(), out precio);
                int.TryParse(row.Cells["BUTACA"].Value.ToString(), out butNro);
                int esComprador;

                if ((bool)row.Cells["ENCONTRADO"].Value)
                {
                    encontrado = 1;
                }
                else
                {
                    encontrado = 0;
                }

                if ((bool)row.Cells["ACTUALIZAR"].Value)
                {
                    actualizar = 1;
                }
                else
                {
                    actualizar = 0;
                }

                if (dni == dniTxt)
                {
                    esComprador = 1;
                }
                else
                {
                    esComprador = 0;
                }

                tablaPasajes.Rows.Add(cliCod,dni,
                    row.Cells["CLI_NOMBRE"].Value.ToString(),
                    row.Cells["CLI_APELLIDO"].Value.ToString(),
                    row.Cells["CLI_DIRECCION"].Value.ToString(),
                    tel,
                    row.Cells["CLI_MAIL"].Value.ToString(),
                    fechaNac,viajeCod, precio, butNro, 
                    row.Cells["MATRICULA"].Value.ToString(),
                    encontrado, actualizar, esComprador);

            }

            
            DataTable tablaEncomiendas = new DataTable();

            tablaEncomiendas.Columns.Add("CLI_COD", typeof(int));
            tablaEncomiendas.Columns.Add("CLI_DNI", typeof(int));
            tablaEncomiendas.Columns.Add("CLI_NOMBRE", typeof(string));
            tablaEncomiendas.Columns.Add("CLI_APELLIDO", typeof(string));
            tablaEncomiendas.Columns.Add("CLI_DIRECCION", typeof(string));
            tablaEncomiendas.Columns.Add("CLI_TELEFONO", typeof(int));
            tablaEncomiendas.Columns.Add("CLI_MAIL", typeof(string));
            tablaEncomiendas.Columns.Add("CLI_FECHA_NAC", typeof(DateTime));
            tablaEncomiendas.Columns.Add("VIAJE_COD", typeof(int));
            tablaEncomiendas.Columns.Add("IMPORTE", typeof(int));
            tablaEncomiendas.Columns.Add("KILOS", typeof(int));
            tablaEncomiendas.Columns.Add("MATRICULA", typeof(string));
            tablaEncomiendas.Columns.Add("ENCONTRADO", typeof(bool));
            tablaEncomiendas.Columns.Add("ACTUALIZAR", typeof(bool));
            tablaEncomiendas.Columns.Add("ES_COMPRADOR", typeof(int));

            foreach (DataGridViewRow row in encomiendas.Rows)
            {
                int.TryParse(row.Cells["CLI_COD"].Value.ToString(), out cliCod);
                int.TryParse(row.Cells["VIAJE_COD"].Value.ToString(), out viajeCod);
                int.TryParse(row.Cells["CLI_DNI"].Value.ToString(), out dni);
                int.TryParse(row.Cells["CLI_TELEFONO"].Value.ToString(), out tel);
                DateTime.TryParse(row.Cells["CLI_FECHA_NAC"].Value.ToString(), out fechaNac);
                decimal.TryParse(row.Cells["IMPORTE"].Value.ToString(), out precio);
                decimal.TryParse(row.Cells["KILOS"].Value.ToString(), out peso);
                int esComprador;

                if ((bool)row.Cells["ENCONTRADO"].Value)
                {
                    encontrado = 1;
                }
                else
                {
                    encontrado = 0;
                }

                if ((bool)row.Cells["ACTUALIZAR"].Value)
                {
                    actualizar = 1;
                }
                else
                {
                    actualizar = 0;
                }

                if (dni == dniTxt)
                {
                    esComprador = 1;
                }
                else
                {
                    esComprador = 0;
                }

                tablaEncomiendas.Rows.Add(cliCod,dni,
                    row.Cells["CLI_NOMBRE"].Value.ToString(),
                    row.Cells["CLI_APELLIDO"].Value.ToString(),
                    row.Cells["CLI_DIRECCION"].Value.ToString(),
                    tel,
                    row.Cells["CLI_MAIL"].Value.ToString(),
                    fechaNac,viajeCod, precio, peso,
                    row.Cells["MATRICULA"].Value.ToString(),
                    encontrado, actualizar, esComprador);


            }

            int vencMes;
            int vencAnio;
            int.TryParse(cboMeses.Text, out vencMes);
            int.TryParse(cboAnios.Text, out vencAnio);

            if (tarjetaNueva)
            {

                new SQLManager().generarSP("ingresarDatosDeCompra")
                                 .agregarTableSP("@TablaPasajes", tablaPasajes)
                                 .agregarTableSP("@TablaEncomiendas", tablaEncomiendas)
                                 .agregarIntSP("@dni", txtDni)
                                 .agregarStringSP("@ape", txtApe)
                                 .agregarStringSP("@nombre", txtNom)
                                 .agregarStringSP("@direccion", txtDire)
                                 .agregarStringSP("@mail", txtMail)
                                 .agregarFechaSP("@fechanac", dp)
                                 .agregarIntSP("@telefono", txtTel)
                                 .agregarBooleanoSP("@encontroComprador", encontroCliente)
                                 .agregarBooleanoSP("@actualizarComprador", actualizarTabla)
                                 .agregarStringSP("@codigoPNR", codigoPNR)
                                 .agregarStringSP("@formaDePago", cboFormaPago)
                                 .agregarIntSP("@nroTarjeta", txtNroTarjeta)
                                 .agregarIntSP("@codSeg", txtCodSeg)
                                 .agregarIntSP("@vencMes", vencMes)
                                 .agregarIntSP("@vencAnio", vencAnio)
                                 .agregarStringSP("@tipoTarjeta", cboTipoTarjeta)
                                 .agregarBooleanoSP("@agregarTarjeta", tarjetaNueva)
                                 .ejecutarSP();

            }
            else
            {
                if (esEfectivo)
                {

                    new SQLManager().generarSP("ingresarDatosDeCompra")
                                 .agregarTableSP("@TablaPasajes", tablaPasajes)
                                 .agregarTableSP("@TablaEncomiendas", tablaEncomiendas)
                                 .agregarIntSP("@dni", txtDni)
                                 .agregarStringSP("@ape", txtApe)
                                 .agregarStringSP("@nombre", txtNom)
                                 .agregarStringSP("@direccion", txtDire)
                                 .agregarStringSP("@mail", txtMail)
                                 .agregarFechaSP("@fechanac", dp)
                                 .agregarIntSP("@telefono", txtTel)
                                 .agregarBooleanoSP("@encontroComprador", encontroCliente)
                                 .agregarBooleanoSP("@actualizarComprador", actualizarTabla)
                                 .agregarStringSP("@codigoPNR", codigoPNR)
                                 .agregarStringSP("@formaDePago", cboFormaPago)
                                 .agregarIntSP("@nroTarjeta", 0)
                                 .agregarIntSP("@codSeg", 0)
                                 .agregarIntSP("@vencMes", 0)
                                 .agregarIntSP("@vencAnio", 0)
                                 .agregarStringSP("@tipoTarjeta", "nada")
                                 .agregarBooleanoSP("@agregarTarjeta", tarjetaNueva)
                                 .ejecutarSP();

                }
                else
                {
                    new SQLManager().generarSP("ingresarDatosDeCompra")
                                 .agregarTableSP("@TablaPasajes", tablaPasajes)
                                 .agregarTableSP("@TablaEncomiendas", tablaEncomiendas)
                                 .agregarIntSP("@dni", txtDni)
                                 .agregarStringSP("@ape", txtApe)
                                 .agregarStringSP("@nombre", txtNom)
                                 .agregarStringSP("@direccion", txtDire)
                                 .agregarStringSP("@mail", txtMail)
                                 .agregarFechaSP("@fechanac", dp)
                                 .agregarIntSP("@telefono", txtTel)
                                 .agregarBooleanoSP("@encontroComprador", encontroCliente)
                                 .agregarBooleanoSP("@actualizarComprador", actualizarTabla)
                                 .agregarStringSP("@codigoPNR", codigoPNR)
                                 .agregarStringSP("@formaDePago", cboFormaPago)
                                 .agregarIntSP("@nroTarjeta", txtNroTarjeta)
                                 .agregarIntSP("@codSeg", txtCodSeg)
                                 .agregarIntSP("@vencMes", vencMes)
                                 .agregarIntSP("@vencAnio", vencAnio)
                                 .agregarStringSP("@tipoTarjeta", cboTipoTarjeta)
                                 .agregarBooleanoSP("@agregarTarjeta", tarjetaNueva)
                                 .ejecutarSP();

                }

                
            }
           
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
