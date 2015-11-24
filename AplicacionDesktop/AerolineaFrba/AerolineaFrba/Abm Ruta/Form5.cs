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
        Boolean huboCondicion ;
        int filtro;
        private bool sePusoAgregarFiltro1 = false;
        private bool sePusoAgregarFiltro2 = false;
        public Form anterior;
        public Form siguiente;
        public Alta alta;
        Boolean primeraVez = true;
        public Boolean llamadoDeModificacion;
        private DataGridViewRow ultimoRegistroSeleccionado;
        public Boolean primeraConsulta = true;

        public Boolean loActivoGenerarViajes = false;
        public string serv_cod = null;

        public Listado()
        {
            InitializeComponent();
        }

        private void Listado_Load(object sender, EventArgs e)
        {
            this.inicio();

            foreach (DataGridViewColumn c in dg.Columns)
            {
                c.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        public void generarQueryInicial()
        {
            this.query = "SELECT";
            if (this.llamadoDeModificacion)
                this.query += " DISTINCT ";

            this.query += " R.RUTA_ID 'Id' ,  RUTA_COD 'Código Ruta', ";
            this.query += this.buscarCiudad("R.CIU_COD_O") + " 'Origen', ";
            this.query += this.buscarCiudad("R.CIU_COD_D") + " 'Destino', ";
            this.query += "RUTA_PRECIO_BASE_KG 'Precio Base Por Kilogramo' , RUTA_PRECIO_BASE_PASAJE 'Precio Base Pasaje' , RUTA_ESTADO 'Estado' ";
            
            if (!this.llamadoDeModificacion)
                this.query += ", S.SERV_DESC 'Servicio' , S.SERV_COD 'Codigo Serv' ";

            this.query += "FROM [ABSTRACCIONX4].[RUTAS_AEREAS] R, [ABSTRACCIONX4].[SERVICIOS] S, [ABSTRACCIONX4].[SERVICIOS_RUTAS] SR ";
            this.query += "WHERE R.RUTA_ID = SR.RUTA_ID AND SR.SERV_COD = S.SERV_COD";
            if (this.loActivoGenerarViajes && this.serv_cod != null)
                query += " AND (select sr.serv_cod from [ABSTRACCIONX4].[SERVICIOS_RUTAS] sr where sr.ruta_id = R.ruta_id) = " + this.serv_cod;

            this.query += " ORDER BY R.RUTA_COD";
        }

        private string buscarCiudad(string cod)
        {
            return "(SELECT CIU_DESC FROM [ABSTRACCIONX4].[CIUDADES] C WHERE C.CIU_COD = " + cod + ")";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (this.primeraVez && chkEstadoIgnorar.Checked == false)
            {
                    this.query += " AND ";

                if (optEstadoAlta.Checked)
                    this.query += "RUTA_ESTADO = 1"; 
                else
                    this.query += "RUTA_ESTADO = 0";

                this.primeraVez = false;
            }

            if ((!sePusoAgregarFiltro1 || listaFiltros.Text == "") && txtFiltro1.TextLength != 0 && cboCamposFiltro1.SelectedIndex != -1)
            {
                MessageBox.Show("No se ha agregado el filtro que contenga a la palabra. Agreguelo para tenerlo en cuenta", "Informe", MessageBoxButtons.OK);
            }
            if ((!sePusoAgregarFiltro2 || listaFiltros.Text == "") && txtFiltro2.TextLength != 0 && cboCamposFiltro2.SelectedIndex != -1)
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
            return (txtFiltro1.TextLength != 0 || txtFiltro2.TextLength != 0);          
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.inicio();
        }

        public void ejecutarQuery()
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

            dg.Columns["Id"].Visible = false;
            if(!this.llamadoDeModificacion)
            dg.Columns["Codigo Serv"].Visible = false;
          
        }

        private void actualizarColumnasDeEstado(DataGridView dg)
        {
            if (primeraConsulta)
            {
                DataGridViewColumn columnaHabilitada = new DataGridViewTextBoxColumn();
                columnaHabilitada.Name = "Habilitada";
                columnaHabilitada.HeaderText = "Habilitada";
                columnaHabilitada.ReadOnly = true;

                dg.Columns.Insert(dg.Columns["Estado"].Index, columnaHabilitada);

            }

            foreach (DataGridViewRow fila in dg.Rows)
            {
                Boolean valor = (Boolean)(fila.Cells["Estado"].Value);
                if (valor)
                {
                    fila.Cells["Habilitada"].Value = "SI";
                }
                else
                {
                    fila.Cells["Habilitada"].Value = "NO";
                }
            }

            dg.Columns["Estado"].Visible = false;
    
        }

        public void inicio()
        {
            this.generarQueryInicial();
            this.ejecutarQuery();


            listaFiltros.Items.Clear();
            chkEstadoIgnorar.Checked = true;
            optEstadoAlta.Checked = true;
            optEstadoBaja.Checked = false;

            txtFiltro1.Enabled = false;
            txtFiltro2.Enabled = false;
            button5.Enabled = false;
            button4.Enabled = false;
            txtFiltro1.Text = "";
            txtFiltro2.Text = "";
     

            cboCamposFiltro1.SelectedIndex = -1;
            cboCamposFiltro2.SelectedIndex = -1;
            cboCamposFiltro1.Text = "Seleccione un campo";
            cboCamposFiltro2.Text = "Seleccione un campo";


            this.huboCondicion = false;

            dg.Columns["Id"].Visible = false;
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
                return "S.SERV_DESC";
            else if (combo.Text == "CODIGO_DE_RUTA")
                return "RUTA_COD";
            else
                return combo.Text;
        }

        private Boolean concatenarCriterio(TextBox txt, ComboBox combo, string criterio)
        {
            if (this.datosCorrectos(txt, combo))
            {
                
                    this.query += " AND ";

                string campo = this.buscarNombreCampo(combo);

                this.query += campo + criterio;

                string mensaje = "'" + txt.Text + "'" + " sobre el campo " + combo.Text;
                if (this.filtro == 1)
                    listaFiltros.Items.Add("Se ha agregado el filtro por contenido del valor ");
                    
                else
                    listaFiltros.Items.Add("Se ha agregado el filtro por igualdad del valor ");
                listaFiltros.Items.Add(mensaje);
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
                this.Close();
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
                cambiarVisibilidades(this.anterior);
            }
            else
            {
                if (siguiente == null) cambiarVisibilidades(this.anterior);
                else
                {
                    this.Close();
                    ultimoRegistroSeleccionado = dg.SelectedRows[0];
                    (siguiente as Modificacion).seSelecciono(dg.SelectedRows[0],obtenerServiciosDe(dg.SelectedRows[0].Cells["Id"].Value.ToString()));
                }
            }
         }

        private List<object> obtenerServiciosDe(string idRuta)
        {
            List<Object> listaServicios = new List<Object>();

            SqlDataReader reader;
            SqlCommand consultaServicios = new SqlCommand();
            consultaServicios.CommandType = CommandType.Text;
            consultaServicios.CommandText = "SELECT tipoServicio FROM [ABSTRACCIONX4].ServiciosDeRuta(@IdRuta)";
            consultaServicios.Connection = Program.conexion();
            consultaServicios.Parameters.AddWithValue("@IdRuta", idRuta);

            reader = consultaServicios.ExecuteReader();

            while (reader.Read())
                listaServicios.Add(reader.GetValue(0));

            reader.Close();

            return listaServicios;
        }

        private void txtFiltros_TextChanged(object sender, EventArgs e)
        {

        }

    }
}
