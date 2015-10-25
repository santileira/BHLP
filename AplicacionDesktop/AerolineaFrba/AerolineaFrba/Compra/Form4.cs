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
                button1.Enabled = false;

            if ((this.servicioDeEncomiendas as Compra.Form5).cantidadKilos == 0)
                button2.Enabled = false;
        }

        public void crearColumnas()
        {
            dgPasajes.ColumnCount = 11;
            dgPasajes.ColumnHeadersVisible = true;

            DataGridViewCellStyle columnHeaderStyle = new DataGridViewCellStyle();
            columnHeaderStyle.BackColor = Color.Beige;
            columnHeaderStyle.Font = new Font("Verdana", 8, FontStyle.Bold);
            dgPasajes.ColumnHeadersDefaultCellStyle = columnHeaderStyle;

            dgPasajes.Columns[0].Name = "CLI_COD";
            dgPasajes.Columns[1].Name = "DNI";
            dgPasajes.Columns[2].Name = "NOMBRE";
            dgPasajes.Columns[3].Name = "APELLIDO";
            dgPasajes.Columns[4].Name = "DIRECCION";
            dgPasajes.Columns[5].Name = "TELEFONO";
            dgPasajes.Columns[6].Name = "MAIL";
            dgPasajes.Columns[7].Name = "FECHA DE NACIMIENTO";
            dgPasajes.Columns[8].Name = "BUTACA";
            dgPasajes.Columns[9].Name = "TIPO";
            dgPasajes.Columns[10].Name = "ACTUALIZAR";

            dgPasajes.Columns["CLI_COD"].Visible = false;
            dgPasajes.Columns["ACTUALIZAR"].Visible = false;

        }

        public void agregarPasaje(DataGridViewRow registroCliente, string numero_butaca, string tipo_butaca, Boolean actualizarTabla)
        {
            dgPasajes.Rows.Add(registroCliente.Cells["CLI_COD"].Value.ToString(), registroCliente.Cells["CLI_DNI"].Value.ToString(),
                registroCliente.Cells["CLI_NOMBRE"].Value.ToString(), registroCliente.Cells["CLI_APELLIDO"].Value.ToString(),
                registroCliente.Cells["CLI_DIRECCION"].Value.ToString(), registroCliente.Cells["CLI_TELEFONO"].Value.ToString(), 
                registroCliente.Cells["CLI_MAIL"].Value.ToString(), registroCliente.Cells["CLI_FECHA_NAC"].Value.ToString(), 
                numero_butaca, tipo_butaca, actualizarTabla);
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
                dgPasajes.Rows.Remove(dgPasajes.SelectedRows[0]);
            }
        }

    }
}
