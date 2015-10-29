using AerolineaFrba.Abm_Ciudad;
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

namespace AerolineaFrba.Devolucion
{
    public partial class dgEncomiendas : Form
    {
        List<String> pasajes;
        List<String> encomiendas;
        String motivo;
        
        public dgEncomiendas()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.inicio();
        }

        public void inicio()
        {
            this.btBuscar.Enabled = false;
            this.txtCodigo.Text = "";
            this.Cancelar.Visible = false;
            this.Devolver.Visible = false;
            pasajes = new List<String>();
            encomiendas = new List<String>();
        }

        private void llenarPasajes(string codigo)
        {
            string query = "SELECT * FROM [ABSTRACCIONX4].LlenarPasajes(" + codigo + ")";

            SqlConnection conexion = Program.conexion();

            DataTable t = new DataTable("Busqueda");
            SqlDataAdapter a = new SqlDataAdapter(query, conexion);
            //Llenar el Dataset
            DataSet ds = new DataSet();
            a.Fill(ds, "Busqueda");
            //Ligar el datagrid con la fuente de datos
            dgPasaje.DataSource = ds;
            dgPasaje.DataMember = "Busqueda";

            conexion.Close();
        }

        private void llenarEncomiendas(string codigo)
        {
            string query = "SELECT * FROM [ABSTRACCIONX4].LlenarEncomiendas(" + codigo + ")";

            SqlConnection conexion = Program.conexion();

            DataTable t = new DataTable("Busqueda");
            SqlDataAdapter a = new SqlDataAdapter(query, conexion);
            //Llenar el Dataset
            DataSet ds = new DataSet();
            a.Fill(ds, "Busqueda");
            //Ligar el datagrid con la fuente de datos
            dgEncomienda.DataSource = ds;
            dgEncomienda.DataMember = "Busqueda";

            conexion.Close();
        }

        private void btBuscar_Click(object sender, EventArgs e)
        {
            if (this.datosCorrectos())
            {
                this.llenarPasajes(txtCodigo.Text);
                this.llenarEncomiendas(txtCodigo.Text);
                this.Cancelar.Visible = this.Devolver.Visible = true;
                this.txtCodigo.Enabled =  this.btBuscar.Enabled = false;
               
            }
        }
              
        private void txtCodigo_TextChanged(object sender, EventArgs e)
        {
            btBuscar.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (pasajes.Count() != 0 || encomiendas.Count() != 0)
            {
                DialogResult resultado = MessageBox.Show("¿Está seguro que quiere ir para atrás, no se realizará la devolución?", "Advertencia", MessageBoxButtons.YesNo);
                if (apretoSi(resultado))
                {
                    this.cambiarVisibilidades(new Menu());
                }
            }
            else this.cambiarVisibilidades(new Menu());
        }

        private void cambiarVisibilidades(Form proximo)
        {
            proximo.Visible = true;
            this.Visible = false;
        }

        private void dgPasaje_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;
            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
            {
                if (e.ColumnIndex == 0)
                {
                    DialogResult resultado = mostrarMensaje("e pasaje");
                  
                    if (apretoSi(resultado))
                    {
                        pasajes.Add(darValorDadoIndex(e.RowIndex , dgPasaje , "PASAJE_COD"));
                        dgPasaje.Rows.RemoveAt(e.RowIndex);
                    }
                }
            }
        }

        private String darValorDadoIndex(int indice , DataGridView dg , string columna)
        {
            return dg.Rows[indice].Cells[columna].Value.ToString();
        }

        private DialogResult mostrarMensaje(string tipo)
        {
            return MessageBox.Show("¿Está seguro que quiere cancelar/devolver est" + tipo + "?", "Advertencia", MessageBoxButtons.YesNo);
        }


        private Boolean apretoSi(DialogResult resultado)
        {
            return resultado == System.Windows.Forms.DialogResult.Yes;
        }

        private void dgEncomienda_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;
            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
            {
                if (e.ColumnIndex == 0)
                {
                    DialogResult resultado = mostrarMensaje("a encomienda");
                   
                    if (apretoSi(resultado))
                    {
                        encomiendas.Add(darValorDadoIndex(e.RowIndex, dgEncomienda, "ENCOMIENDA_COD"));
                        dgEncomienda.Rows.RemoveAt(e.RowIndex);
                    }
                }
            }
        }

        private Boolean datosCorrectos()
        {
            Boolean huboErrores = false;

            huboErrores = Validacion.esNumero(txtCodigo , "código de compra" , true);

            return huboErrores;
        }

        private void dgEncomiendas_Load(object sender, EventArgs e)
        {

        }

        private void btFinalizar_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show("¿Está seguro que quiere finalizar las devoluciones?", "Advertencia", MessageBoxButtons.YesNo);
            if(apretoSi(resultado))
            {
                    Form2 form = new Form2();
                    form.ShowDialog();
                    motivo = form.Motivo;
                    this.cancelarPasajesYEncomiendas();
                    cambiarVisibilidades(new Menu());
            }    
        }

        private Object cancelarPasajesYEncomiendas()
        {
            SQLManager sqlManager = new SQLManager();
            return sqlManager.generarSP("CancelarPasajesYEncomiendas").
                   agregarStringSP("@Codigo", txtCodigo.Text).
                   agregarListaSP("@Pasajes", pasajes).
                   agregarListaSP("@Encomiendas", encomiendas).
                   agregarFechaSP("@FechaDevolucion" , System.DateTime.Today).
                   agregarStringSP("@Motivo" , motivo).
                   ejecutarSP();
        }
    }
}
