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

        public void inicio()
        {
            if ((this.butacas as Compra.Form2).cantidadButacas == 0)
            {
                button4.Enabled = false;
                button1.Enabled = false;
            }

            if ((this.servicioDeEncomiendas as Compra.Form5).cantidadKilos == 0)
            {
                button5.Enabled = false;
                button2.Enabled = false;
            }
        }

        public void crearColumnas()
        {
            dgPasajes.ColumnCount = 11;
            dgPasajes.ColumnHeadersVisible = true;

            DataGridViewCellStyle columnHeaderStyle = new DataGridViewCellStyle();
            columnHeaderStyle.BackColor = Color.Beige;
            columnHeaderStyle.Font = new Font("Verdana", 8, FontStyle.Bold);
            dgPasajes.ColumnHeadersDefaultCellStyle = columnHeaderStyle;

            this.agregarCampos(dgPasajes);
            dgPasajes.Columns[8].Name = "BUTACA";
            dgPasajes.Columns[9].Name = "TIPO";
            dgPasajes.Columns[10].Name = "ACTUALIZAR";
            dgPasajes.Columns["CLI_COD"].Visible = false;
            dgPasajes.Columns["ACTUALIZAR"].Visible = false;



            dgEncomiendas.ColumnCount = 10;
            dgEncomiendas.ColumnHeadersVisible = true;

            DataGridViewCellStyle columnHeaderStyle2 = new DataGridViewCellStyle();
            columnHeaderStyle2.BackColor = Color.Beige;
            columnHeaderStyle2.Font = new Font("Verdana", 8, FontStyle.Bold);
            dgEncomiendas.ColumnHeadersDefaultCellStyle = columnHeaderStyle2;

            this.agregarCampos(dgEncomiendas);
            dgEncomiendas.Columns[8].Name = "KILOS";
            dgEncomiendas.Columns[9].Name = "ACTUALIZAR";
            dgEncomiendas.Columns["CLI_COD"].Visible = false;
            dgEncomiendas.Columns["ACTUALIZAR"].Visible = false;

        }

        private void agregarCampos(DataGridView unDg)
        {
            unDg.Columns[0].Name = "CLI_COD";
            unDg.Columns[1].Name = "DNI";
            unDg.Columns[2].Name = "NOMBRE";
            unDg.Columns[3].Name = "APELLIDO";
            unDg.Columns[4].Name = "DIRECCION";
            unDg.Columns[5].Name = "TELEFONO";
            unDg.Columns[6].Name = "MAIL";
            unDg.Columns[7].Name = "FECHA DE NACIMIENTO";
        }

        public void agregarPasaje(DataGridViewRow registroCliente, string numero_butaca, string tipo_butaca, Boolean actualizarTabla)
        {
            dgPasajes.Rows.Add(registroCliente.Cells["CLI_COD"].Value.ToString(), registroCliente.Cells["CLI_DNI"].Value.ToString(),
                registroCliente.Cells["CLI_NOMBRE"].Value.ToString(), registroCliente.Cells["CLI_APELLIDO"].Value.ToString(),
                registroCliente.Cells["CLI_DIRECCION"].Value.ToString(), registroCliente.Cells["CLI_TELEFONO"].Value.ToString(), 
                registroCliente.Cells["CLI_MAIL"].Value.ToString(), registroCliente.Cells["CLI_FECHA_NAC"].Value.ToString(), 
                numero_butaca, tipo_butaca, actualizarTabla);
        }

        public void agregarEncomienda(DataGridViewRow registroEncomienda, string kilos, Boolean actualizarTabla)
        {
            dgEncomiendas.Rows.Add(registroEncomienda.Cells["CLI_COD"].Value.ToString(), registroEncomienda.Cells["CLI_DNI"].Value.ToString(),
                registroEncomienda.Cells["CLI_NOMBRE"].Value.ToString(), registroEncomienda.Cells["CLI_APELLIDO"].Value.ToString(),
                registroEncomienda.Cells["CLI_DIRECCION"].Value.ToString(), registroEncomienda.Cells["CLI_TELEFONO"].Value.ToString(),
                registroEncomienda.Cells["CLI_MAIL"].Value.ToString(), registroEncomienda.Cells["CLI_FECHA_NAC"].Value.ToString(),
                kilos, actualizarTabla);
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

        private void button1_Click(object sender, EventArgs e)
        {
            (this.butacas as Compra.Form2).inicio();
            this.cambiarVisibilidades(this.butacas);
        }

        private void button4_Click(object sender, EventArgs e)
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

        private void button2_Click(object sender, EventArgs e)
        {
            (this.servicioDeEncomiendas as Compra.Form5).inicio();
            this.cambiarVisibilidades(this.servicioDeEncomiendas);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (dgEncomiendas.RowCount == 0)
                MessageBox.Show("No se puede cancelar ninguna encomienda porque aun no se ha confirmado ninguna", "Error en la cancelacion de pasajes", MessageBoxButtons.OK);
            else
            {
                (this.servicioDeEncomiendas as Compra.Form5).liberarEspacio(dgEncomiendas.SelectedRows[0].Cells["KILOS"].Value.ToString());
                dgEncomiendas.Rows.Remove(dgEncomiendas.SelectedRows[0]);
            }
        }

    }
}
