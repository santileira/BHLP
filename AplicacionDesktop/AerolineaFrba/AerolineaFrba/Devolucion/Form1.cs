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
            this.btFinalizar.Visible = false;
        }

        private void llenarDg(DataGridView dg , string funcion)
        {
            string query = "SELECT * FROM [ABSTRACCIONX4]." + funcion;

            SQLManager.ejecutarQuery(query, dg);
        }

        

        private void btBuscar_Click(object sender, EventArgs e)
        {
            if (this.datosCorrectos())
            {
                this.llenarDg(dgPasaje , "LlenarPasajes('" + txtCodigo.Text + "')");
                this.llenarDg(dgEncomienda , "LlenarEncomiendas('" + txtCodigo.Text + "')");
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
                        pasajes.Add(darValorDadoIndex(e.RowIndex , dgPasaje , "Código"));
                        dgPasaje.Rows.RemoveAt(e.RowIndex);
                        this.btFinalizar.Visible = true;
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
                        encomiendas.Add(darValorDadoIndex(e.RowIndex, dgEncomienda, "Código"));
                        dgEncomienda.Rows.RemoveAt(e.RowIndex);
                        this.btFinalizar.Visible = true;
                    }
                }
            }
        }

        private Boolean datosCorrectos()
        {
            Boolean huboErrores = false;

            huboErrores = Validacion.esTexto(txtCodigo , "código de compra" , true);

            return huboErrores;
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

        private void dineroOtarjeta()
        {
            
        }

        private Object cancelarPasajesYEncomiendas()
        {
            SQLManager sqlManager = new SQLManager();
            return sqlManager.generarSP("CancelarPasajesYEncomiendas").
                   agregarStringSP("@Codigo", txtCodigo.Text).
                   agregarListaSP("@Pasajes", pasajes).
                   agregarListaSP("@Encomiendas", encomiendas).
                   agregarFechaSP("@FechaDevolucion" , System.DateTime.Now).
                   agregarStringSP("@Motivo" , motivo).
                   ejecutarSP();
        }
    }
}
