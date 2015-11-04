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
    public partial class Form4 : Form
    {
        public Form servicioDeEncomiendas;
        public Form butacas;
        public Form efectuaCompra;

        public Form anterior;

        public int cantidadButacas;
        public double cantidadKilos;

        public Form4()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
        }

        public Boolean seRegistroPasajeDelCliente(String dni, String apellido)
        {
            foreach (DataGridViewRow row in dgPasajes.Rows)
            {
                if (row.Cells["CLI_DNI"].Value.ToString() == dni && row.Cells["CLI_APELLIDO"].Value.ToString() == apellido)
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
                if (row.Cells["CLI_DNI"].Value.ToString() == dni && row.Cells["CLI_APELLIDO"].Value.ToString() == apellido)
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
        }

        public void activarCompraEncomienda()
        {
            button2.Enabled = true;
            button5.Enabled = true;
        }


        public void desactivarCompraPasajes()
        {
            button4.Enabled = false;
            button1.Enabled = false;
        }

        public void desactivarCompraEncomienda()
        {
            button2.Enabled = false;
            button5.Enabled = false;
        }

        public void crearColumnas()
        {
            dgPasajes.ColumnCount = 15;
            dgPasajes.ColumnHeadersVisible = true;

            DataGridViewCellStyle columnHeaderStyle = new DataGridViewCellStyle();
            columnHeaderStyle.BackColor = Color.Beige;
            columnHeaderStyle.Font = new Font("Verdana", 8, FontStyle.Bold);
            dgPasajes.ColumnHeadersDefaultCellStyle = columnHeaderStyle;

            this.agregarCampos(dgPasajes);
            dgPasajes.Columns[8].Name = "BUTACA";
            dgPasajes.Columns[9].Name = "TIPO";
            dgPasajes.Columns[10].Name = "IMPORTE";
            dgPasajes.Columns[11].Name = "ACTUALIZAR";
            dgPasajes.Columns[12].Name = "VIAJE_COD";
            dgPasajes.Columns[13].Name = "MATRICULA";
            dgPasajes.Columns[14].Name = "ENCONTRADO";
            
            dgPasajes.Columns["CLI_COD"].Visible = false;
            dgPasajes.Columns["ACTUALIZAR"].Visible = false;
            dgPasajes.Columns["VIAJE_COD"].Visible = false;
            dgPasajes.Columns["MATRICULA"].Visible = false;
            dgPasajes.Columns["ENCONTRADO"].Visible = false;

            dgEncomiendas.ColumnCount = 14;
            dgEncomiendas.ColumnHeadersVisible = true;

            DataGridViewCellStyle columnHeaderStyle2 = new DataGridViewCellStyle();
            columnHeaderStyle2.BackColor = Color.Beige;
            columnHeaderStyle2.Font = new Font("Verdana", 8, FontStyle.Bold);
            dgEncomiendas.ColumnHeadersDefaultCellStyle = columnHeaderStyle2;

            this.agregarCampos(dgEncomiendas);
            dgEncomiendas.Columns[8].Name = "KILOS";
            dgEncomiendas.Columns[9].Name = "IMPORTE";
            dgEncomiendas.Columns[10].Name = "ACTUALIZAR";
            dgEncomiendas.Columns[11].Name = "VIAJE_COD";
            dgEncomiendas.Columns[12].Name = "MATRICULA";
            dgEncomiendas.Columns[13].Name = "ENCONTRADO";

            dgEncomiendas.Columns["CLI_COD"].Visible = false;
            dgEncomiendas.Columns["ACTUALIZAR"].Visible = false;
            dgEncomiendas.Columns["VIAJE_COD"].Visible = false;
            dgEncomiendas.Columns["MATRICULA"].Visible = false;
            dgEncomiendas.Columns["ENCONTRADO"].Visible = false;
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
            this.cambiarVisibilidades(this.anterior);
        }

        private void cambiarVisibilidades(Form formularioSiguiente)
        {
            formularioSiguiente.Visible = true;
            this.Visible = false;
        }


        // Efectuar Compra

        private void button3_Click(object sender, EventArgs e)
        {
            (this.efectuaCompra as Compra.Form6).pasajes = dgPasajes;
            (this.efectuaCompra as Compra.Form6).encomiendas = dgEncomiendas;

            this.cambiarVisibilidades(efectuaCompra);
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

        private void button4_Click_1(object sender, EventArgs e)
        {
            if (dgPasajes.RowCount == 0)
                MessageBox.Show("No se puede cancelar ningun pasaje porque aun no se ha confirmado ninguno", "Error en la cancelacion de pasajes", MessageBoxButtons.OK);
            else
            {
                (this.butacas as Compra.Form2).liberarButaca(dgPasajes.SelectedRows[0].Cells["BUTACA"].Value.ToString());
                (this.butacas as Compra.Form2).inicio();
                dgPasajes.Rows.Remove(dgPasajes.SelectedRows[0]);
            }
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            if (dgEncomiendas.RowCount == 0)
                MessageBox.Show("No se puede cancelar ninguna encomienda porque aun no se ha confirmado ninguna", "Error en la cancelacion de pasajes", MessageBoxButtons.OK);
            else
            {
                (this.servicioDeEncomiendas as Compra.Form5).liberarEspacio(dgEncomiendas.SelectedRows[0].Cells["KILOS"].Value.ToString());
                dgEncomiendas.Rows.Remove(dgEncomiendas.SelectedRows[0]);
            }
        }

        private void dgPasajes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgEncomiendas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

    }
}
