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

        }

        private void llenarPasajes(string codigo)
        {
            string query = "SELECT * FROM [ABSTRACCIONX4].PASAJES WHERE COMP_COD = [ABSTRACCIONX4].ObtenerCodigo(" + codigo + ")";

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
            string query = "SELECT * FROM [ABSTRACCIONX4].ENCOMIENDAS WHERE COMP_COD = [ABSTRACCIONX4].ObtenerCodigo(" + codigo + ")";

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
            }
        }
              
        private void txtCodigo_TextChanged(object sender, EventArgs e)
        {
            btBuscar.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.cambiarVisibilidades(new Menu());
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
                    //int idRuta = Convert.ToInt32(darValorDadoIndex(e.RowIndex,"RUTA_ID"));
                    if (apretoSi(resultado))
                    {
                        //darDeBajaRuta(idRuta);
                        /*darDeBajaPasajes(idRuta);
                        darDeBajaEncomienda(idRuta);*/
                        //ejecutarQuery();
                    }
                }
            }
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
                    //int idRuta = Convert.ToInt32(darValorDadoIndex(e.RowIndex,"RUTA_ID"));
                    if (apretoSi(resultado))
                    {
                        //darDeBajaRuta(idRuta);
                        /*darDeBajaPasajes(idRuta);
                        darDeBajaEncomienda(idRuta);*/
                        //ejecutarQuery();
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
    }
}
