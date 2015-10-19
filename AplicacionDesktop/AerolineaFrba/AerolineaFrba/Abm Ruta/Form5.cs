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

namespace AerolineaFrba.Abm_Ruta
{
    public partial class Listado : Form
    {
        string query;
        Boolean huboCondicion;
        int filtro;
        private bool sePusoAgregarFiltro1 = false;
        private bool sePusoAgregarFiltro2 = false;
        public Form anterior;
        public Form siguiente;
        public Alta alta;
        Boolean primeraVez = true;
        public Boolean llamadoDeModificacion = false;
        private DataGridViewRow ultimoRegistroSeleccionado;
        public Boolean primeraConsulta = true;
        public Boolean loActivoGenerarViajes = false;

        public Listado()
        {
            InitializeComponent();
        }

        private void Listado_Load(object sender, EventArgs e)
        {
            this.iniciar();

            foreach (DataGridViewColumn c in dg.Columns)
            {
                c.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void generarQueryInicial()
        {
            this.query = "SELECT RUTA_COD CODIGO_DE_RUTA,  (SELECT S.SERV_DESC FROM [ABSTRACCIONX4].[SERVICIOS] S WHERE S.SERV_COD = R.SERV_COD)  TIPO_SERVICIO, ";
            this.query += this.buscarCiudad("R.CIU_COD_O") + " ORIGEN, ";
            this.query +=this.buscarCiudad("R.CIU_COD_D") + " DESTINO, ";
            this.query += "RUTA_PRECIO_BASE_KG, RUTA_PRECIO_BASE_PASAJE, RUTA_ESTADO ";
            this.query += "FROM [ABSTRACCIONX4].[RUTAS_AEREAS] R";
        }

        private string buscarCiudad(string cod)
        {
            return "(SELECT CIU_DESC FROM [ABSTRACCIONX4].[CIUDADES] C WHERE C.CIU_COD = " + cod + ")";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (this.primeraVez && chkEstadoIgnorar.Checked == false)
            {
                if (!this.huboCondicion)
                {
                    this.huboCondicion = true;
                    this.query += " WHERE ";
                }
                else
                    this.query += " AND ";

                if (optEstadoAlta.Checked)
                    this.query += "RUTA_ESTADO = 1"; 
                else
                    this.query += "RUTA_ESTADO = 0";

                this.primeraVez = false;
            }

            if ((!sePusoAgregarFiltro1 || txtFiltros.TextLength != 0) && txtFiltro1.TextLength != 0 && cboCamposFiltro1.SelectedIndex != -1)
            {
                MessageBox.Show("No se ha agregado el filtro que contenga a la palabra. Agreguelo para tenerlo en cuenta", "Informe", MessageBoxButtons.OK);
            }
            if ((!sePusoAgregarFiltro2 || txtFiltros.TextLength != 0) && txtFiltro2.TextLength != 0 && cboCamposFiltro2.SelectedIndex != -1)
            {
                MessageBox.Show("No se ha agregado el filtro por igualdad de palabra. Agreguelo para tenerlo en cuenta", "Informe", MessageBoxButtons.OK);
            }
            if (txtFiltro1.TextLength == 0 && cboCamposFiltro1.SelectedIndex != -1)
                MessageBox.Show("No se ha agregado contenido para el filtro que contenga a la palabra", "Informe", MessageBoxButtons.OK);
            if (txtFiltro2.TextLength == 0 && cboCamposFiltro2.SelectedIndex != -1)
                MessageBox.Show("No se ha agregado contenido para el filtro por igualdad de palabra", "Informe", MessageBoxButtons.OK);

            if (dg.Rows.Count == 0)
                MessageBox.Show("No se han encontrado resultados en la consulta", "Informe", MessageBoxButtons.OK);

            this.ejecutarQuery();
        }

        private Boolean sePusoFiltro()
        {
            return (txtFiltro1.TextLength != 0 || txtFiltro2.TextLength != 0 || cboFiltro3.SelectedIndex != -1);          
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.iniciar();
        }

        private void ejecutarQuery()
        {
     //       sePusoAgregarFiltro1 = false;
      //      sePusoAgregarFiltro2 = false;
            
            SqlConnection conexion = Program.conexion();

            DataTable t = new DataTable("Busqueda");
            SqlDataAdapter a = new SqlDataAdapter(this.query, conexion);
            //Llenar el Dataset
            DataSet ds = new DataSet();
            a.Fill(ds, "Busqueda");
            //Ligar el datagrid con la fuente de datos
            dg.DataSource = ds;
            dg.DataMember = "Busqueda";
            
            actualizarColumnasDeEstado(dg);

            conexion.Close();

            primeraConsulta = false;
        }

        private void actualizarColumnasDeEstado(DataGridView dg)
        {
            if (primeraConsulta)
            {
                DataGridViewColumn columnaHabilitada = new DataGridViewTextBoxColumn();
                columnaHabilitada.Name = "HABILITADA";
                columnaHabilitada.HeaderText = "HABILITADA";
                columnaHabilitada.ReadOnly = true;

                dg.Columns.Insert(dg.Columns["RUTA_ESTADO"].Index, columnaHabilitada);

            }

            foreach (DataGridViewRow fila in dg.Rows)
            {
                Boolean valor = (Boolean)(fila.Cells["RUTA_ESTADO"].Value);
                if (valor)
                {
                    fila.Cells["HABILITADA"].Value = "SI";
                }
                else
                {
                    fila.Cells["HABILITADA"].Value = "NO";
                }
            }

            dg.Columns["RUTA_ESTADO"].Visible = false;
    
        }

        private void iniciar()
        {
            this.generarQueryInicial();
            this.ejecutarQuery();
            
            txtFiltros.Text = "";
            chkEstadoIgnorar.Checked = true;
            optEstadoAlta.Checked = true;
            optEstadoBaja.Checked = false;

            txtFiltro1.Enabled = false;
            txtFiltro2.Enabled = false;
            button5.Enabled = false;
            button4.Enabled = false;
            txtFiltro1.Text = "";
            txtFiltro2.Text = "";
            txtFiltro4.Text = "";

            cboCamposFiltro1.SelectedIndex = -1;
            cboCamposFiltro2.SelectedIndex = -1;
            cboCamposFiltro1.Text = "Seleccione un campo";
            cboCamposFiltro2.Text = "Seleccione un campo";
            cboFiltro3.SelectedIndex = -1;

            this.huboCondicion = false;
            
        }

        private void chkEstadoIgnorar_CheckedChanged(object sender, EventArgs e)
        {
           optEstadoAlta.Enabled = !chkEstadoIgnorar.Checked;
           optEstadoBaja.Enabled = !chkEstadoIgnorar.Checked;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.filtro = 1;
            if (txtFiltro1.Enabled && txtFiltro1.Text.Length != 0)
            {
                if (this.concatenarCriterio(txtFiltro1, cboCamposFiltro1, " LIKE '%" + txtFiltro1.Text + "%'"))
                {
                    cboCamposFiltro1.SelectedIndex = -1;
                    txtFiltro1.Text = "";
                    
                  
                     this.sePusoAgregarFiltro1 = true;
                }
                }
            else
            {
                MessageBox.Show("Debe llenar el campo desplegable y el texto para poder agregar filtro", "Advertencia", MessageBoxButtons.OK);
            }
        }

        private string buscarNombreCampo(ComboBox combo)
        {
            if (combo.Text == "ORIGEN")
                return this.buscarCiudad("R.CIU_COD_O");
            else if (combo.Text == "DESTINO")
                return this.buscarCiudad("R.CIU_COD_D");
            else if (combo.Text == "TIPO_SERVICIO")
                return "SERV_COD";
            else if (combo.Text == "CODIGO_DE_RUTA")
                return "RUTA_COD";
            else
                return combo.Text;
        }

        private Boolean concatenarCriterio(TextBox txt, ComboBox combo, string criterio)
        {
            if (this.datosCorrectos(txt, combo))
            {
                if (!this.huboCondicion)
                {
                    this.huboCondicion = true;
                    this.query += " WHERE ";
                }
                else
                    this.query += " AND ";

                string campo = this.buscarNombreCampo(combo);

                this.query += campo + criterio;

                string mensaje = "'" + txt.Text + "'" + " sobre el campo " + combo.Text;
                if (this.filtro == 1)
                    txtFiltros.Text += "Se ha agregado el filtro por contenido del valor " + mensaje + System.Environment.NewLine;
                    
                else
                    txtFiltros.Text += "Se ha agregado el filtro por igualdad del valor " + mensaje + System.Environment.NewLine;
                return true;
            }
            return false;
        }

        private Boolean datosCorrectos(TextBox txt, ComboBox combo)
        {
            Boolean huboErrores = false;

            if (txt.TextLength == 0)
            {
                MessageBox.Show("El criterio no puede estar en blanco", "Error en el criterio", MessageBoxButtons.OK);
                huboErrores = true;
            }

            if(combo.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar un campo en el desplegable de opciones", "Error en el campo", MessageBoxButtons.OK);
                huboErrores = true;
            }

            if (combo.Text.Equals("RUTA_COD") || combo.Text.Equals("RUTA_PRECIO_BASE_KG") || combo.Text.Equals("RUTA_PRECIO_BASE_PASAJE"))
            {
                if (!this.esNumero(txt))
                {
                    MessageBox.Show("Para el campo " + combo.Text + " el criterio debe ser numerico", "Error en el tipo de dato del criterio", MessageBoxButtons.OK);
                    huboErrores = true;
                }
            }
            else if(combo.Text.Equals("TIPO_SERVICIO") || combo.Text.Equals("ORIGEN") || combo.Text.Equals("DESTINO"))
            {
                if (!this.esTexto(txt))
                {
                    MessageBox.Show("Para el campo " + combo.Text + " el criterio debe ser texto", "Error en el nombre", MessageBoxButtons.OK);
                    huboErrores = true;
                }
            }

            return !huboErrores;
        }

        private Boolean esTexto(TextBox txt)
        {
            String textPattern = "[A-Za-z]";
            System.Text.RegularExpressions.Regex regexTexto = new System.Text.RegularExpressions.Regex(textPattern);

            return regexTexto.IsMatch(txt.Text);
        }

        private Boolean esNumero(TextBox txt)
        {
            String numericPattern = "[0-9]";
            System.Text.RegularExpressions.Regex regexNumero = new System.Text.RegularExpressions.Regex(numericPattern);

            return regexNumero.IsMatch(txt.Text);
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            this.filtro = 2;
            if (txtFiltro2.Enabled && txtFiltro2.Text.Length != 0)
            {
                if (this.concatenarCriterio(txtFiltro2, cboCamposFiltro2, " = '" + txtFiltro2.Text + "'"))
                {
                    txtFiltro2.Text = "";
                    cboCamposFiltro2.SelectedIndex = -1;
                  
                    this.sePusoAgregarFiltro2 = true;
                } 
           }
            else
            {
                MessageBox.Show("Debe llenar el campo desplegable y el texto para poder agregar filtro", "Advertencia", MessageBoxButtons.OK);
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void cboCamposFiltro1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboCamposFiltro1.SelectedIndex != -1)
            {
                txtFiltro1.Enabled = true;
                button5.Enabled = true;
            }
            else
            {
                txtFiltro1.Enabled = false;
                button5.Enabled = false;
            }
        }

        private void cboCamposFiltro2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboCamposFiltro2.SelectedIndex != -1)
            {
                txtFiltro2.Enabled = true;
                button4.Enabled = true;
            }
            else
            {
                txtFiltro2.Enabled = false;
                button4.Enabled = false;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (this.llamadoDeModificacion)
            {
                
                this.cambiarVisibilidades(this.siguiente);
                (siguiente as Modificacion).seSelecciono(ultimoRegistroSeleccionado);
            }
            else this.cambiarVisibilidades(this.anterior);
        }

        private void cambiarVisibilidades(Form formularioSiguiente)
        {
            formularioSiguiente.Visible = true;
            this.Visible = false;
        }

        private void botonSeleccionar_Click(object sender, EventArgs e)
        {
            if (dg.RowCount == 0)
            {
                MessageBox.Show("No se ha seleccionado ninguna ruta", "Selección invalida", MessageBoxButtons.OK);
                return;
            }

            if (this.loActivoGenerarViajes)
            {
                (anterior as Generacion_Viaje.Form1).seSeleccionoRuta(dg.SelectedRows[0]);
            }
            else
            {
                if (siguiente == null) cambiarVisibilidades(this.anterior);
                else
                {
                    cambiarVisibilidades(this.siguiente);
                    ultimoRegistroSeleccionado = dg.SelectedRows[0];
                    (siguiente as Modificacion).seSelecciono(dg.SelectedRows[0]);
                }
            }
         }

    }
}
