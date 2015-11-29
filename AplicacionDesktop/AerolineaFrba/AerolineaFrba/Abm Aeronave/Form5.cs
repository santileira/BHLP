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

namespace AerolineaFrba.Abm_Aeronave
{

    public partial class Listado : Form
    {
        string query;
        Boolean huboCondicion;
        public Form anterior;
        public Form siguiente;
        public bool primeraConsulta = true;
        public bool seSeteaQuery = false;
        public bool llamadoDesdeModificacion = false;
        public bool filtro1;
        public bool sePusoAgregarFiltro2 = false;
        public bool sePusoAgregarFiltro1 = false;

        public bool loActivoGenerarViajes = false;
        public string queryViajes;

        public bool loActivoModificar = false;
        public string serv_cod = null;

        public Boolean llamadoDesdeModificacionSeleccionar;

        
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

        //Boton Limpiar
        private void button2_Click(object sender, EventArgs e)
        {
            this.inicio();
        }


        //Boton Buscar
        private void button3_Click(object sender, EventArgs e)
        {
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
            
            this.ejecutarConsulta();
            if (dg.Rows.Count == 0)
                MessageBox.Show("No se han encontrado resultados en la consulta", "Informe", MessageBoxButtons.OK);
        }

        private Boolean sePusoFiltro()
        {
            return (txtFiltro1.TextLength != 0 || txtFiltro2.TextLength != 0);
        }

        // Carga el datagrid con los datos de la consulta
        public void ejecutarConsulta()
        {
            SqlConnection conexion = Program.conexion();

            DataTable t = new DataTable("Busqueda");
            SqlDataAdapter a = new SqlDataAdapter(this.query, conexion);
            //Llenar el Dataset
            DataSet ds = new DataSet();
            a.Fill(ds, "Busqueda");
            //Ligar el datagrid con la fuente de datos
            dg.DataSource = ds;
            dg.DataMember = "Busqueda";

            if (!seSeteaQuery)
            {
                //actualizarColumnasDeEstado(dg); 
            }
            conexion.Close();

            primeraConsulta = false;

            
            dg.Columns["SERV_COD"].Visible = false;
            
        }

        public void inicio()
        {

            if (seSeteaQuery)
            {
                query = (anterior as Registro_Llegada_Destino.Form1).consultaSeteada();

            }
            else
            {
                this.extenderQuery();
            }

            ejecutarConsulta();

            txtFiltro1.Text = "";
            txtFiltro2.Text = "";

            listaFiltros.Items.Clear();
            
            cboCamposFiltro1.SelectedIndex = -1;
            cboCamposFiltro2.SelectedIndex = -1;

            this.huboCondicion = false;

            if (seSeteaQuery)
            {
                this.huboCondicion = true;
            }


        }

        // Para el caso de que se llame desde generar viaje
        public void extenderQuery()
        {
            query = "select a.SERV_COD, AERO_MATRI 'Matrícula',AERO_MOD 'Modelo',AERO_FAB 'Fabricante',SERV_DESC 'Tipo de servicio',AERO_CANT_KGS 'Cantidad de KG',AERO_FECHA_ALTA 'Fecha de alta',AERO_FECHA_BAJA 'Fecha de baja',(SELECT CIU_DESC FROM [ABSTRACCIONX4].CIUDADES WHERE CIU_COD = a.CIU_COD_ORIGEN) 'Ciudad de origen' from ABSTRACCIONX4.AERONAVES a JOIN ABSTRACCIONX4.SERVICIOS s ON (a.SERV_COD = s.SERV_COD)";

            if (this.loActivoGenerarViajes)
            {
                this.query += this.queryViajes;

                if (this.serv_cod != null)
                    this.query += " AND a.SERV_COD = " + this.serv_cod;
            }
        }

        // Agregan los filtros a la consulta
        private void button4_Click_1(object sender, EventArgs e)
        {
            this.filtro1 = false;

            if (this.concatenarCriterio(txtFiltro2, cboCamposFiltro2, " = '" + txtFiltro2.Text + "'"))
            {
                cboCamposFiltro2.SelectedIndex = -1;
                txtFiltro2.Text = "";
                this.sePusoAgregarFiltro2 = true;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.filtro1 = true;
            if (this.concatenarCriterio(txtFiltro1, cboCamposFiltro1, " LIKE '%" + txtFiltro1.Text + "%'"))
            {
                cboCamposFiltro1.SelectedIndex = -1;
                txtFiltro1.Text = "";
                this.sePusoAgregarFiltro1 = true;
            }
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

                this.query += nombreColumna(combo.Text) + criterio;
                string mensaje = "'" + txt.Text + "'" + " sobre el campo " + combo.Text;
                
                if (this.filtro1)
                {
                    listaFiltros.Items.Add("Se ha agregado el filtro por contenido del valor ");
                }
                else
                {
                        listaFiltros.Items.Add("Se ha agregado el filtro por igualdad del valor ");
                }
                listaFiltros.Items.Add(mensaje);
                return true;    
            }
            return false;
        }

        private string nombreColumna(string nombre)
        {
            switch (nombre)
            {
                case "Matrícula":
                    return "AERO_MATRI";
                case "Servicio":
                    return "s.SERV_DESC";
                case "Modelo":
                    return "AERO_MOD";
                case "Fabricante":
                    return "AERO_FAB";
            }
            return "";
        }

        // Validaciones de textbox de filtros
        private Boolean datosCorrectos(TextBox txt, ComboBox combo)
        {
            Boolean huboErrores = false;

            if (txt.TextLength == 0)
            {
                MessageBox.Show("El criterio no puede estar en blanco", "Error en el criterio", MessageBoxButtons.OK);
                huboErrores = true;
            }

            if (combo.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar un campo en el desplegable de opciones", "Error en el campo", MessageBoxButtons.OK);
                huboErrores = true;
            }

            else if (combo.Text.Equals("Modelo") || combo.Text.Equals("Matrícula") || combo.Text.Equals("Fabricante") || combo.Text.Equals("Servicio"))
            {
                if (!Validacion.esTexto(txt))
                {
                    MessageBox.Show("Para el campo " + combo.Text + " el criterio debe ser texto", "Error en el nombre", MessageBoxButtons.OK);
                    huboErrores = true;
                }
            }

            return !huboErrores;
        }


        private void button6_Click(object sender, EventArgs e)
        {

            if (llamadoDesdeModificacion)
                cambiarVisibilidades(this.siguiente);
            else
                cambiarVisibilidades(this.anterior);

        }

        private void cambiarVisibilidades(Form formularioSiguiente)
        {
            formularioSiguiente.Visible = true;
            this.Visible = false;
        }

        private void concatenarCriterio(DateTimePicker dataTime, ComboBox combo, string criterio)
        {
            if (!this.huboCondicion)
            {
                this.huboCondicion = true;
                this.query += " WHERE ";
            }
            else
                this.query += " AND ";

            this.query += combo.Text + criterio;

            string mensaje = "'" + dataTime.Text + "'" + " sobre el campo " + combo.Text;

            
            listaFiltros.Items.Add("Se ha agregado el filtro por contenido del valor ");
            listaFiltros.Items.Add(mensaje);

        }

        // Selección de una fila
        private void button7_Click_1(object sender, EventArgs e)
        {
            if (dg.RowCount == 0)
            {
                MessageBox.Show("No se ha seleccionado ninguna aeronave", "Selección invalida", MessageBoxButtons.OK);
                return;
            }


            List<Object> listaFuncionalidades = new List<object>(7);

            if (llamadoDesdeModificacionSeleccionar)
            {
                cambiarVisibilidades(this.siguiente);
                (siguiente as Modificacion).seSelecciono(dg.SelectedRows[0]);
                return;
            }
            if (seSeteaQuery)
            {
                (anterior as Registro_Llegada_Destino.Form1).seSeleccionoMatricula(dg.SelectedRows[0]);
            }

            if (this.loActivoGenerarViajes)
            {
                (anterior as Generacion_Viaje.Form1).seSeleccionoAeronave(dg.SelectedRows[0]);
            }

            this.cambiarVisibilidades(this.anterior);

        }

        private void dg_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dg_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

    }
}
    

