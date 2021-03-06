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
    public partial class Form4 : Form
    {
        public Form servicioDeEncomiendas;
        public Form butacas;
        public Form efectuaCompra;

        public Form anterior;

        public int cantidadButacas;
        public double cantidadKilos;

        public int butacasSelec;
        public double kilosSelec;

        public Form4()
        {
            this.VisibleChanged += new EventHandler(this.Form_VisibleChanged);
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
        }

        private void Form_VisibleChanged(object sender, EventArgs e)
        {
            habilitarBotonesCompra();
            labelPasajesRestantes.Text = "Pasajes restantes: " + butacasRestantes().ToString();
            labelKGRestantes.Text = "KG Restantes: " + kilosRestantes().ToString();
        }

        public void habilitarBotonesCompra()
        {
            button1.Enabled = butacasRestantes() > 0;
            button2.Enabled = kilosRestantes() > 0;
        }

        public Boolean seRegistroPasajeDelCliente(String dni, String apellido)
        {
            foreach (DataGridViewRow row in dgPasajes.Rows)
            {
                if (row.Cells["DNI"].Value.ToString() == dni && row.Cells["Apellido"].Value.ToString() == apellido)
                {
                    return true;
                }
            }

            return false;
        }

        public Boolean seRegistroEncomiendaDelCliente(String dni, String apellido)
        {
            foreach (DataGridViewRow row in dgEncomiendas.Rows)
            {
                if (row.Cells["DNI"].Value.ToString() == dni && row.Cells["Apellido"].Value.ToString() == apellido)
                {
                    return true;
                }
            }

            return false;
        }

        public void activarCompraPasajes()
        {
            button4.Enabled = true;
            button1.Enabled = true;
            labelPasajesRestantes.Visible = true;
        }

        public void activarCompraEncomienda()
        {
            button2.Enabled = true;
            button5.Enabled = true;
            labelKGRestantes.Visible = true;
        }


        public void desactivarCompraPasajes()
        {
            button4.Enabled = false;
            button1.Enabled = false;
            labelPasajesRestantes.Visible = false;
        }

        public void desactivarCompraEncomienda()
        {
            button2.Enabled = false;
            button5.Enabled = false;
            labelKGRestantes.Visible = false;
        }

        /*
         * Metodo que inicializa las cabeceras de los data grid para pasajes y encomiendas
         */
        public void crearColumnas()
        {
            dgPasajes.ColumnCount = 15;
            dgPasajes.ColumnHeadersVisible = true;

            this.agregarCampos(dgPasajes);
            dgPasajes.Columns[8].Name = "Butaca";
            dgPasajes.Columns[9].Name = "Tipo";
            dgPasajes.Columns[10].Name = "Importe";
            dgPasajes.Columns[11].Name = "Actualizar";
            dgPasajes.Columns[12].Name = "Código de viaje";
            dgPasajes.Columns[13].Name = "Matrícula";
            dgPasajes.Columns[14].Name = "Encontrado";
            
            dgPasajes.Columns["Código"].Visible = false;
            dgPasajes.Columns["Actualizar"].Visible = false;
            dgPasajes.Columns["Código de viaje"].Visible = false;
            dgPasajes.Columns["Matrícula"].Visible = false;
            dgPasajes.Columns["Encontrado"].Visible = false;

            dgEncomiendas.ColumnCount = 14;
            dgEncomiendas.ColumnHeadersVisible = true;

            this.agregarCampos(dgEncomiendas);
            dgEncomiendas.Columns[8].Name = "Kilos";
            dgEncomiendas.Columns[9].Name = "Importe";
            dgEncomiendas.Columns[10].Name = "Actualizar";
            dgEncomiendas.Columns[11].Name = "Código de viaje";
            dgEncomiendas.Columns[12].Name = "Matrícula";
            dgEncomiendas.Columns[13].Name = "Encontrado";

            dgEncomiendas.Columns["Código"].Visible = false;
            dgEncomiendas.Columns["Actualizar"].Visible = false;
            dgEncomiendas.Columns["Código de viaje"].Visible = false;
            dgEncomiendas.Columns["Matrícula"].Visible = false;
            dgEncomiendas.Columns["Encontrado"].Visible = false;
        }

        private void agregarCampos(DataGridView unDg)
        {
            unDg.Columns[0].Name = "Código";
            unDg.Columns[1].Name = "DNI";
            unDg.Columns[2].Name = "Nombre";
            unDg.Columns[3].Name = "Apellido";
            unDg.Columns[4].Name = "Dirección";
            unDg.Columns[5].Name = "Teléfono";
            unDg.Columns[6].Name = "Mail";
            unDg.Columns[7].Name = "Fecha de nacimiento";
        }

        /*
         * Metodo que settea en el data grid de pasajes los datos recibidos en el registroCliente recibido por el form 2
         */
        public void agregarPasaje(DataGridViewRow registroCliente, string numero_butaca, string tipo_butaca, string importe, Boolean actualizarTabla,Boolean encontroCliente, string viaje_cod, string matricula)
        {
            dgPasajes.Rows.Add(registroCliente.Cells["CLI_COD"].Value.ToString(), registroCliente.Cells["CLI_DNI"].Value.ToString(),
                registroCliente.Cells["CLI_NOMBRE"].Value.ToString(), registroCliente.Cells["CLI_APELLIDO"].Value.ToString(),
                registroCliente.Cells["CLI_DIRECCION"].Value.ToString(), registroCliente.Cells["CLI_TELEFONO"].Value.ToString(), 
                registroCliente.Cells["CLI_MAIL"].Value.ToString(), registroCliente.Cells["CLI_FECHA_NAC"].Value.ToString(), 
                numero_butaca, tipo_butaca, importe, actualizarTabla, viaje_cod, matricula,encontroCliente);
        }

        public void agregarPasaje(string cliCod,string dni, string nom, string ape, string dire, string tel, string mail, string fechaNac, string numero_butaca, string tipo_butaca, string importe, Boolean actualizarTabla, Boolean encontroCliente, string viaje_cod, string matricula)
        {
            dgPasajes.Rows.Add(cliCod, dni, nom, ape, dire, tel, mail, fechaNac,
                numero_butaca, tipo_butaca, importe, actualizarTabla, viaje_cod, matricula, encontroCliente);
        }

        /*
         * Metodo que settea en el data grid de encomiendas los datos recibidos en el registroCliente recibido por el form 5
         */
        public void agregarEncomienda(DataGridViewRow registroEncomienda, string kilos, string importe, Boolean actualizarTabla,Boolean encontroCliente, string viaje_cod, string matricula)
        {
            dgEncomiendas.Rows.Add(registroEncomienda.Cells["CLI_COD"].Value.ToString(), registroEncomienda.Cells["CLI_DNI"].Value.ToString(),
                registroEncomienda.Cells["CLI_NOMBRE"].Value.ToString(), registroEncomienda.Cells["CLI_APELLIDO"].Value.ToString(),
                registroEncomienda.Cells["CLI_DIRECCION"].Value.ToString(), registroEncomienda.Cells["CLI_TELEFONO"].Value.ToString(),
                registroEncomienda.Cells["CLI_MAIL"].Value.ToString(), registroEncomienda.Cells["CLI_FECHA_NAC"].Value.ToString(),
                kilos, importe, actualizarTabla, viaje_cod, matricula,encontroCliente);
        }

        public void agregarEncomienda(string cliCod, string dni, string nom, string ape, string dire, string tel, string mail, string fechaNac, string kilos, string importe, Boolean actualizarTabla, Boolean encontroCliente, string viaje_cod, string matricula)
        {
            dgEncomiendas.Rows.Add(cliCod, dni, nom, ape, dire, tel, mail, fechaNac,
                kilos, importe, actualizarTabla, viaje_cod, matricula, encontroCliente);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show("Si vuelve al formulario anterior perderá los datos de pasajes/encomiendas ya cargados. ¿Desea regresar de todas formas?", "Advertencia", MessageBoxButtons.YesNo);
            if (apretoSi(resultado))
            {
                dgEncomiendas.Rows.Clear();
                dgPasajes.Rows.Clear();
                this.cambiarVisibilidades(this.anterior);

                (this.butacas as Compra.Form2).borrarButacasSeleccionadas();
            }
        }

        private Boolean apretoSi(DialogResult resultado)
        {
            return resultado == System.Windows.Forms.DialogResult.Yes;
        } 

        private void cambiarVisibilidades(Form formularioSiguiente)
        {
            formularioSiguiente.Visible = true;
            this.Visible = false;
        }


        // Efectuar Compra

        private void button3_Click(object sender, EventArgs e)
        {
            double kilosPedidos = 0;
            double kilos;
            foreach (DataGridViewRow row in dgEncomiendas.Rows)
            {
                double.TryParse(row.Cells["Kilos"].Value.ToString(), out kilos);
                kilosPedidos = kilosPedidos + kilos;
            }

            
            if (butacasSelec == dgPasajes.RowCount && kilosSelec == kilosPedidos)
            {

                (this.efectuaCompra as Compra.Form6).pasajes = dgPasajes;
                (this.efectuaCompra as Compra.Form6).encomiendas = dgEncomiendas;

                this.cambiarVisibilidades(efectuaCompra);
            }
            else
            {
                MessageBox.Show("No indico la totalidad de pasajes y/o kilos", "Error de Compra", MessageBoxButtons.OK);
            }

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            (this.butacas as Compra.Form2).inicio();
            this.cambiarVisibilidades(this.butacas);
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            (this.servicioDeEncomiendas as Compra.Form5).inicio();
            this.cambiarVisibilidades(this.servicioDeEncomiendas);
        }

        /*
         * Metodo que cancela un pasaje seleccionado de entre los pasajes listados hasta el momento
         */
        private void button4_Click_1(object sender, EventArgs e)
        {
            if (dgPasajes.RowCount == 0)
                MessageBox.Show("No se puede cancelar ningun pasaje porque aun no se ha confirmado ninguno", "Error en la cancelacion de pasajes", MessageBoxButtons.OK);
            else
            {
                (this.butacas as Compra.Form2).liberarButaca(dgPasajes.SelectedRows[0].Cells["Butaca"].Value.ToString());
                (this.butacas as Compra.Form2).inicio();
                dgPasajes.Rows.Remove(dgPasajes.SelectedRows[0]);
                labelPasajesRestantes.Text = "Pasajes restantes: " + butacasRestantes().ToString();
                habilitarBotonesCompra();
            }
        }

        /*
         * Metodo que cancela una encomienda seleccionada de entre las encomiendas listadas hasta el momento
         */
        private void button5_Click_1(object sender, EventArgs e)
        {
            if (dgEncomiendas.RowCount == 0)
                MessageBox.Show("No se puede cancelar ninguna encomienda porque aun no se ha confirmado ninguna", "Error en la cancelacion de pasajes", MessageBoxButtons.OK);
            else
            {
                (this.servicioDeEncomiendas as Compra.Form5).liberarEspacio(dgEncomiendas.SelectedRows[0].Cells["Kilos"].Value.ToString());
                dgEncomiendas.Rows.Remove(dgEncomiendas.SelectedRows[0]);
                labelKGRestantes.Text = "KG Restantes: " + kilosRestantes().ToString();
                habilitarBotonesCompra();
            }
        }

        private void dgPasajes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgEncomiendas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        public int butacasRestantes()
        {
            return butacasSelec - dgPasajes.RowCount;
        }

        public decimal kilosRestantes()
        {
            decimal kilosUsados = 0;
            foreach(DataGridViewRow fila in dgEncomiendas.Rows)
            {
                kilosUsados += Convert.ToDecimal(fila.Cells["Kilos"].Value);
            }
            return Convert.ToDecimal(kilosSelec) - kilosUsados;
        }

    }
}
