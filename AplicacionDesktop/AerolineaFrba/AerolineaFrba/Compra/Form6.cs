﻿using System;
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
        private decimal totalAAbonar = 0;
        private int clienteCodigo = 0;

        public Form6()
        {
            InitializeComponent();
            if (Program.rolActual.Equals("Cliente"))
            {
                cboFormaPago.Items.Remove("Efectivo");
            }
            //
            // Carga del contenido de combos
            //
            txtDni.Enabled = true;
            button2.Enabled = false;

            SqlDataReader varTarjeta;
            SqlCommand consultaColumnas = new SqlCommand();
            consultaColumnas.CommandType = CommandType.Text;
            consultaColumnas.CommandText = "SELECT TIPOTARJ_DESC FROM [ABSTRACCIONX4].TIPOS_TARJETAS";
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
                txtNroTarjeta.Text = "";
                txtCodSeg.Text = "";
                cboAnios.Text = "";
                cboMeses.Text = "";
                cboTipoTarjeta.Text = "";
                cboCuotas.Text = "";
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

        //Método que se ejecuta al disparar el evento del boton BUSCAR EN LA BASE DE DATOS, en donde se ejecuta
        //un SP que verifica si dicho cliente ya existe. Si ya existe autocompleta los campos, en el caso de que
        //no exista verifica que el dni ingresado no este ya registrado en la BD. Si todo esta bien, permite el
        //alta del nuevo cliente.

        private void button1_Click(object sender, EventArgs e)
        {
            if (!validarDNIYApellido())
            {

                button2.Enabled = true;
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

                    clienteCodigo = (int)reader.GetValue(0);
                    txtNom.Text = reader.GetValue(2).ToString();
                    txtDire.Text = reader.GetValue(4).ToString();
                    txtTel.Text = reader.GetValue(5).ToString();
                    txtMail.Text = reader.GetValue(6).ToString();
                    dp.Value = (DateTime)reader.GetValue(7);
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

                        MessageBox.Show("Dni inválido. Ya existe un Cliente con ese DNI", "Error cliente", MessageBoxButtons.OK);
                    }
                    else
                    {
                        MessageBox.Show("No se encuentra cargado el cliente en la BD. Por favor, ingresar los datos para darle de alta", "Cliente no encontrado", MessageBoxButtons.OK);
                    }
                }
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

        private Boolean validarDNIYApellido()
        {
            Boolean validacion = Validacion.esVacio(txtDni, "DNI", true);
            validacion = Validacion.esVacio(txtApe, "Apellido", true) || validacion;
            validacion = !Validacion.esNumero(txtDni, "DNI", true) || validacion;
            validacion = !Validacion.esSoloTexto(txtApe, "Apellido", true) || validacion;
            return validacion;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
           cboCuotas.Items.Clear();
           SqlDataReader varCuotas;
           SqlCommand consulta = new SqlCommand();
           consulta.CommandType = CommandType.Text;
           consulta.CommandText = "SELECT TC.CUO_NUM FROM [ABSTRACCIONX4].TIPOS_CUOTAS TC JOIN [ABSTRACCIONX4].TIPOS_TARJETAS TT ON TC.TIPOTARJ_COD = TT.TIPOTARJ_COD WHERE TIPOTARJ_DESC='" + cboTipoTarjeta.SelectedItem.ToString() + "'";
           consulta.Connection = Program.conexion();
           varCuotas = consulta.ExecuteReader();


           while (varCuotas.Read())
            {
                this.cboCuotas.Items.Add(varCuotas.GetValue(0));
            }
           

        }

        private void Form6_Load(object sender, EventArgs e)
        {

        }


        //Método que se ejecuta al dispararse el evento del botón efectuar Compra.

        private void button2_Click(object sender, EventArgs e)
        {
            bool huboError = this.hacerValidacionesDeTipo();

            if (!this.encontroCliente)
            {
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
            }

            if (!huboError)
            {

                if (sePuedeEfectuarLaCompra())
                {

                    string codigoPNR = CreatePNR(10);
                    cargarDatosDeCompra(codigoPNR);
                    

                    this.Close();

                }
                else
                {
                    MessageBox.Show("Los datos ingresados de la tarjeta no coinciden con los datos registrados", "Compra de pasajes y/o encomiendas", MessageBoxButtons.OK);
                }

            }
            
        }

        private Boolean hacerValidacionesDeTipo()
        {
            Boolean validacion = Validacion.esVacio(txtDni, "DNI", true);
            validacion = Validacion.esVacio(txtApe, "Apellido", true) || validacion;
            validacion = Validacion.esVacio(txtNom, "Nombre", true) || validacion;
            validacion = Validacion.esVacio(txtDire, "Dirección", true) || validacion;
            validacion = Validacion.esVacio(txtTel, "Teléfono", true) || validacion;
            validacion = Validacion.esVacio(txtMail, "Mail", true) || validacion;
         
            validacion = !Validacion.esNumero(txtDni, "DNI", true) || validacion;
            validacion = !Validacion.esSoloTexto(txtApe, "Apellido", true) || validacion;
            validacion = !Validacion.esSoloTexto(txtNom, "Nombre", true) || validacion;
            validacion = !Validacion.esTexto(txtDire, "Dirección", true) || validacion;
            validacion = !Validacion.esNumero(txtTel, "Teléfono", true) || validacion;
            if (dp.Value.CompareTo(Program.fechaHoy()) > 0)
            {
                validacion = true;
                MessageBox.Show("La Fecha de Nacimiento debe ser anterior a la fecha actual", "Error en los datos", MessageBoxButtons.OK);
            }
            validacion = !Validacion.esTexto(txtMail, "Mail", true) || validacion;
           

            validacion = !Validacion.estaEntreLimites(txtDni, 1, 999999999, false, "DNI") || validacion;
            validacion = !Validacion.estaEntreLimites(txtTel, 1, 999999999, false, "Teléfono") || validacion;
          
            validacion = !Validacion.estaSeleccionado(cboFormaPago,true , "forma de pago") || validacion;

            if (!cboFormaPago.SelectedIndex.Equals(-1))
            {

                if (cboFormaPago.SelectedItem.ToString() == "Tarjeta de crédito")
                {
                    
                    validacion = Validacion.esVacio(txtNroTarjeta, "Nro. Tarjeta", true) || validacion;
                    validacion = Validacion.esVacio(txtCodSeg, "Cod. Seg.", true) || validacion;
                    validacion = !Validacion.estaSeleccionado(cboAnios, true , "año de fecha de vencimiento") || validacion;
                    validacion = !Validacion.estaSeleccionado(cboMeses, true , "mes de fecha de vencimiento") || validacion;
                    validacion = !Validacion.estaSeleccionado(cboTipoTarjeta, true , "tipo de tarjeta") || validacion;
                    validacion = !Validacion.estaSeleccionado(cboCuotas, true , "cuotas") || validacion;
                    validacion = !Validacion.esNumero(txtNroTarjeta, "Nro. Tarjeta", true) || validacion;
                    validacion = !Validacion.esNumero(txtCodSeg, "Cod. Seg.", true) || validacion;
                    validacion = !Validacion.estaEntreLimites(txtNroTarjeta, 0, 9999999999999999, false, "número de tarjeta") || validacion;
                    validacion = !Validacion.estaEntreLimites(txtCodSeg, 0, 9999, false, "código de seguridad") || validacion;

                    if (Validacion.estaSeleccionado(cboAnios, false, "año de fecha de vencimiento") && Validacion.estaSeleccionado(cboMeses, false, "mes de fecha de vencimiento"))
                    {
                        int anioVto = Convert.ToInt16(cboAnios.Text);
                        int mesVto = Convert.ToInt16(cboMeses.Text);
                        if (anioVto<Program.fechaHoy().Year ||
                            (anioVto == Program.fechaHoy().Year && mesVto < Program.fechaHoy().Month))
                        {
                            MessageBox.Show("La fecha de vencimiento de la tarjeta no puede ser anterior a la fecha de hoy.", "Error en los datos de entrada", MessageBoxButtons.OK);
                            validacion = true;
                        }
                    }
                }
            }

            return validacion;
        }
               
        // Método que verifica la validez en el pago. Esto solo tiene sentido en nuestro caso si se paga con TC.
        // En ese caso se verifica que si la TC ya existe en nuestra BD se ingrese los datos correctos.

        private bool sePuedeEfectuarLaCompra()
        {
            Int64 nroTarjeta;
            Int64.TryParse(txtNroTarjeta.Text, out nroTarjeta);

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
            
            Int64 nroTarjeta;
            int vtoMes;
            int vtoAnios;
            int codSeg;

            Int64.TryParse(txtNroTarjeta.Text, out nroTarjeta);
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


        private Decimal enDecimal(string numero)
        {
            return Decimal.Round(Convert.ToDecimal(numero.Replace(".", ",")), 2);
        }
        
        
        // Método responsable de hacer toda la preparación de los datos para enviarlo como parámetro al SP/FUNCTION
        // que se encargan de dar de alta todos los registros asociados a una COMPRA. Por ejemplo, altas en compras,
        // en pasajes, en encomiendas, en tarjetas (si se pago de esa forma y la tarjeta no existe) etc.



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

            tablaPasajes.Columns.Add("Código", typeof(int));
            tablaPasajes.Columns.Add("DNI", typeof(int));
            tablaPasajes.Columns.Add("Nombre", typeof(string));
            tablaPasajes.Columns.Add("Apellido", typeof(string));
            tablaPasajes.Columns.Add("Dirección", typeof(string));
            tablaPasajes.Columns.Add("Teléfono", typeof(int));
            tablaPasajes.Columns.Add("Mail", typeof(string));
            tablaPasajes.Columns.Add("Fecha de nacimiento", typeof(DateTime));
            tablaPasajes.Columns.Add("Código de viaje", typeof(int));
            tablaPasajes.Columns.Add("Importe", typeof(decimal));
            tablaPasajes.Columns.Add("Butaca", typeof(int));
            tablaPasajes.Columns.Add("Matrícula", typeof(string));
            tablaPasajes.Columns.Add("Encontrado", typeof(bool));
            tablaPasajes.Columns.Add("Actualizar", typeof(bool));
            tablaPasajes.Columns.Add("ES_COMPRADOR", typeof(int));
                       

            foreach (DataGridViewRow row in pasajes.Rows)
            {
                int.TryParse(row.Cells["Código"].Value.ToString(), out cliCod);
                int.TryParse(row.Cells["Código de viaje"].Value.ToString(), out viajeCod);
                int.TryParse(row.Cells["DNI"].Value.ToString(), out dni);
                int.TryParse(row.Cells["Teléfono"].Value.ToString(), out tel);
                DateTime.TryParse(row.Cells["Fecha de nacimiento"].Value.ToString(), out fechaNac);
                precio = enDecimal(row.Cells["Importe"].Value.ToString());
                int.TryParse(row.Cells["Butaca"].Value.ToString(), out butNro);
                int esComprador;

                totalAAbonar = totalAAbonar + precio;

                if ((bool)row.Cells["Encontrado"].Value)
                {
                    encontrado = 1;
                }
                else
                {
                    encontrado = 0;
                }

                if ((bool)row.Cells["Actualizar"].Value)
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
                    row.Cells["Nombre"].Value.ToString(),
                    row.Cells["Apellido"].Value.ToString(),
                    row.Cells["Dirección"].Value.ToString(),
                    tel,
                    row.Cells["Mail"].Value.ToString(),
                    fechaNac,viajeCod, precio, butNro, 
                    row.Cells["Matrícula"].Value.ToString(),
                    encontrado, actualizar, esComprador);

            }

            
            DataTable tablaEncomiendas = new DataTable();

            tablaEncomiendas.Columns.Add("Código", typeof(int));
            tablaEncomiendas.Columns.Add("DNI", typeof(int));
            tablaEncomiendas.Columns.Add("Nombre", typeof(string));
            tablaEncomiendas.Columns.Add("Apellido", typeof(string));
            tablaEncomiendas.Columns.Add("Dirección", typeof(string));
            tablaEncomiendas.Columns.Add("Teléfono", typeof(int));
            tablaEncomiendas.Columns.Add("Mail", typeof(string));
            tablaEncomiendas.Columns.Add("Fecha de nacimiento", typeof(DateTime));
            tablaEncomiendas.Columns.Add("Código de viaje", typeof(int));
            tablaEncomiendas.Columns.Add("Importe", typeof(decimal));
            tablaEncomiendas.Columns.Add("Kilos", typeof(decimal));
            tablaEncomiendas.Columns.Add("Matrícula", typeof(string));
            tablaEncomiendas.Columns.Add("Encontrado", typeof(bool));
            tablaEncomiendas.Columns.Add("Actualizar", typeof(bool));
            tablaEncomiendas.Columns.Add("ES_COMPRADOR", typeof(int));

            foreach (DataGridViewRow row in encomiendas.Rows)
            {
                int.TryParse(row.Cells["Código"].Value.ToString(), out cliCod);
                int.TryParse(row.Cells["Código de viaje"].Value.ToString(), out viajeCod);
                int.TryParse(row.Cells["DNI"].Value.ToString(), out dni);
                int.TryParse(row.Cells["Teléfono"].Value.ToString(), out tel);
                DateTime.TryParse(row.Cells["Fecha de nacimiento"].Value.ToString(), out fechaNac);
                precio = enDecimal(row.Cells["Importe"].Value.ToString());
                peso = enDecimal(row.Cells["Kilos"].Value.ToString());
                int esComprador;

                totalAAbonar = totalAAbonar + precio;

                if ((bool)row.Cells["Encontrado"].Value)
                {
                    encontrado = 1;
                }
                else
                {
                    encontrado = 0;
                }

                if ((bool)row.Cells["Actualizar"].Value)
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
                    row.Cells["Nombre"].Value.ToString(),
                    row.Cells["Apellido"].Value.ToString(),
                    row.Cells["Dirección"].Value.ToString(),
                    tel,
                    row.Cells["Mail"].Value.ToString(),
                    fechaNac,viajeCod, precio, peso,
                    row.Cells["Matrícula"].Value.ToString(),
                    encontrado, actualizar, esComprador);

            }

            int vencMes;
            int vencAnio;
            int cuotas;
            int.TryParse(cboMeses.Text, out vencMes);
            int.TryParse(cboAnios.Text, out vencAnio);
            int.TryParse(cboCuotas.Text, out cuotas);
            
            SQLManager manager = new SQLManager();

            if (tarjetaNueva)
            {

                manager = manager.generarSP("ingresarDatosDeCompra")
                                 .agregarTableSP("@TablaPasajes", tablaPasajes)
                                 .agregarTableSP("@TablaEncomiendas", tablaEncomiendas)
                                 .agregarIntSP("@clienteCodigo", clienteCodigo)
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
                                 .agregarIntSP("@cuotas", cuotas)
                                 .agregarStringSP("@formaDePago", cboFormaPago)
                                 .agregarInt64SP("@nroTarjeta", txtNroTarjeta)
                                 .agregarIntSP("@codSeg", txtCodSeg)
                                 .agregarIntSP("@vencMes", vencMes)
                                 .agregarIntSP("@vencAnio", vencAnio)
                                 .agregarStringSP("@tipoTarjeta", cboTipoTarjeta)
                                 .agregarBooleanoSP("@agregarTarjeta", tarjetaNueva);

            }
            else
            {
                if (esEfectivo)
                {

                    manager = manager.generarSP("ingresarDatosDeCompra")
                                 .agregarTableSP("@TablaPasajes", tablaPasajes)
                                 .agregarTableSP("@TablaEncomiendas", tablaEncomiendas)
                                 .agregarIntSP("@clienteCodigo", clienteCodigo)
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
                                 .agregarIntSP("@cuotas", 0)
                                 .agregarStringSP("@formaDePago", cboFormaPago)
                                 .agregarInt64SP("@nroTarjeta", 0)
                                 .agregarIntSP("@codSeg", 0)
                                 .agregarIntSP("@vencMes", 0)
                                 .agregarIntSP("@vencAnio", 0)
                                 .agregarStringSP("@tipoTarjeta", "nada")
                                 .agregarBooleanoSP("@agregarTarjeta", tarjetaNueva);

                }
                else
                {
                    manager = manager.generarSP("ingresarDatosDeCompra")
                                 .agregarTableSP("@TablaPasajes", tablaPasajes)
                                 .agregarTableSP("@TablaEncomiendas", tablaEncomiendas)
                                 .agregarIntSP("@clienteCodigo", clienteCodigo)
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
                                 .agregarIntSP("@cuotas", cuotas)
                                 .agregarStringSP("@formaDePago", cboFormaPago)
                                 .agregarInt64SP("@nroTarjeta", txtNroTarjeta)
                                 .agregarIntSP("@codSeg", txtCodSeg)
                                 .agregarIntSP("@vencMes", vencMes)
                                 .agregarIntSP("@vencAnio", vencAnio)
                                 .agregarStringSP("@tipoTarjeta", cboTipoTarjeta)
                                 .agregarBooleanoSP("@agregarTarjeta", tarjetaNueva);

                }
                               
            }


            try
            {
                manager.ejecutarSP();
                MessageBox.Show("Se realizo la compra con éxito. Su codigo de PNR: " + codigoPNR + ". Monto a abonar: " + totalAAbonar.ToString() + ".", "Compra de pasajes y/o encomiendas", MessageBoxButtons.OK);
            }
            catch
            {
                MessageBox.Show("Fallo en la compra", "Fallo Compra", MessageBoxButtons.OK);
                this.Close();
                return;
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
