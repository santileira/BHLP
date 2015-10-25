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

            dgCliente.CurrentCell = null;

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
            this.ejecutarQuery("select * from [ABSTRACCIONX4].butacasDisponibles('" + registro.Cells["VIAJE_COD"].Value.ToString() + "', '" + registro.Cells["AERO_MATRI"].Value.ToString() + "')", dgButacas);
            this.ejecutarQuery("select * from [ABSTRACCIONX4].kilosDisponibles('" + registro.Cells["VIAJE_COD"].Value.ToString() + "', '" + registro.Cells["AERO_MATRI"].Value.ToString() + "')", dgKilos);

            string kilos = dgKilos.Rows[0].Cells["Kilos"].Value.ToString();

            (((this.anterior as Compra.Form4).anterior as Compra.Form3).anterior as Compra.Form1).completarCantidades(dgButacas.RowCount, kilos);
        }

        private void ejecutarQuery(string query, DataGridView unDg)
        {           
            SqlConnection conexion = Program.conexion();
            DataTable t = new DataTable("Busqueda");
            SqlDataAdapter a = new SqlDataAdapter(query, conexion);
            //Llenar el Dataset
            DataSet ds = new DataSet();
            a.Fill(ds, "Busqueda");
            //Ligar el datagrid con la fuente de datos

            unDg.DataSource = ds;
            unDg.DataMember = "Busqueda";
       
            conexion.Close();
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

            this.ejecutarQuery("select * from [ABSTRACCIONX4].buscarCliente('" + txtDni.Text + "', '" + txtApe.Text + "')", dgCliente);

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

                dgCliente.Rows[0].Cells["CLI_DIRECCION"].Value = txtDire.Text;
                dgCliente.Rows[0].Cells["CLI_FECHA_NAC"].Value = dp.Value;
                dgCliente.Rows[0].Cells["CLI_NOMBRE"].Value = txtNom.Text;
                dgCliente.Rows[0].Cells["CLI_TELEFONO"].Value = txtTel.Text;
                dgCliente.Rows[0].Cells["CLI_MAIL"].Value = txtMail.Text;
                dgCliente.Rows[0].Cells["CLI_COD"].Value = 0;
                dgCliente.Rows[0].Cells["CLI_DNI"].Value = txtDni.Text;
                dgCliente.Rows[0].Cells["CLI_APELLIDO"].Value = txtApe.Text;      
            
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
            Boolean huboError = false;

            huboError = Validacion.esVacio(txtDni, "DNI", true);
            huboError = Validacion.esVacio(txtTel, "Telefono", true);
            huboError = Validacion.esVacio(txtDire, "Direccion", true);
            huboError = Validacion.esVacio(txtMail, "Mail", true);
            huboError = Validacion.esVacio(txtNom, "Nombre", true);
            huboError = Validacion.esVacio(txtApe, "Apellido", true);
             
            huboError = !Validacion.numeroCorrecto(txtDni, "DNI", false);
            huboError = !Validacion.numeroCorrecto(txtTel, "Telefono", false);

            huboError = !Validacion.esTexto(txtDire, "Direccion", true);
            huboError = !Validacion.esTexto(txtMail, "Mail", true);
            huboError = !Validacion.esTexto(txtNom, "Nombre", true);
            huboError = !Validacion.esTexto(txtApe, "Apellido", true);

            if (dp.Value.Year > DateTime.Now.Year && dp.Value.Month > DateTime.Now.Month && dp.Value.Day > DateTime.Now.Day)
            {
                huboError = true;
                MessageBox.Show("La Fecha de Nacimiento debe ser anterior a la fecha actual", "Error en los datos", MessageBoxButtons.OK);
            }

            if (dgButacas.SelectedRows[0].Cells["BUT_NRO"].Style.BackColor == Color.Gray)
            {
                huboError = true;
                MessageBox.Show("La butaca seleccionada ya se encuentra ocupada", "Error en los datos", MessageBoxButtons.OK);
            }

            if(!huboError)
            {
                MessageBox.Show("Se ha guardado el pasaje", "Pasaje confirmado", MessageBoxButtons.OK);

                dgButacas.SelectedRows[0].Cells["BUT_NRO"].Style.BackColor = Color.Gray;
                dgButacas.SelectedRows[0].Cells["BUT_TIPO"].Style.BackColor = Color.Gray;

                (this.anterior as Compra.Form4).agregarPasaje(dgCliente.Rows[0], dgButacas.SelectedRows[0].Cells["BUT_NRO"].Value.ToString(), dgButacas.SelectedRows[0].Cells["BUT_TIPO"].Value.ToString(), actualizarTabla);

                this.cantidadButacas -= 1;
                this.inicio();
            }
                

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
    }
}
