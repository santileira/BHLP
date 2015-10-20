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

        public bool loActivoGenerarViajes = false;
        public bool loActivoModificar = false;
        public string serv_cod = null;
        public DateTime fechaSalida;
        public DateTime fechaLlegada;

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
            this.ejecutarConsulta();

            if (dg.Rows.Count == 0)
                MessageBox.Show("No se han encontrado resultados en la consulta", "Informe", MessageBoxButtons.OK);
        }

        private Boolean sePusoFiltro()
        {
            return (txtFiltro1.TextLength != 0 || txtFiltro2.TextLength != 0 || cboCamposFiltro3.SelectedIndex != -1);
        }        

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

            //modificar check que se pone automáticamente en los campos de tipo bit
            if (!seSeteaQuery)
            {
                actualizarColumnasDeEstado(dg);
            }
            conexion.Close();

            primeraConsulta = false;

            if (loActivoModificar)
            {
                dg.Columns["SERV_COD"].Visible = false;
            }
        }

        private void actualizarColumnasDeEstado(DataGridView dg)
        {
            if (primeraConsulta)
            {
                DataGridViewColumn columnaHabilitada = new DataGridViewTextBoxColumn();
                columnaHabilitada.Name = "HABILITADA";
                columnaHabilitada.HeaderText = "HABILITADA";
                columnaHabilitada.ReadOnly = true;

                DataGridViewColumn columnaFueraServicio = new DataGridViewTextBoxColumn();
                columnaFueraServicio.Name = "FUERA_SERVICIO";
                columnaFueraServicio.HeaderText = "FUERA_SERVICIO";
                columnaFueraServicio.ReadOnly = true;

                dg.Columns.Insert(dg.Columns["AERO_BAJA_VU"].Index, columnaHabilitada);
                dg.Columns.Insert(dg.Columns["AERO_BAJA_FS"].Index, columnaFueraServicio);
            }

            foreach (DataGridViewRow fila in dg.Rows)
            {
                Boolean valor = (Boolean)(fila.Cells["AERO_BAJA_VU"].Value);
                if (valor)
                {
                    fila.Cells["HABILITADA"].Value = "NO";
                }
                else
                {
                    fila.Cells["HABILITADA"].Value = "SI";
                }
                valor = (Boolean)(fila.Cells["AERO_BAJA_FS"].Value);
                if (valor)
                {
                    fila.Cells["FUERA_SERVICIO"].Value = "SI";
                }
                else
                {
                    fila.Cells["FUERA_SERVICIO"].Value = "NO";
                }
            }

            dg.Columns["AERO_BAJA_FS"].Visible = false;
            dg.Columns["AERO_BAJA_VU"].Visible = false;
        }

        public void inicio()
        {

            //
            // Carga del contenido de combos
            //

            SqlDataReader varcampo;
            SqlDataReader varfecha;

            SqlCommand consultaColumnas = new SqlCommand();
            SqlCommand consultaColumnasFechas = new SqlCommand();

            consultaColumnas.CommandType = CommandType.Text;
            consultaColumnasFechas.CommandType = CommandType.Text;

            if (seSeteaQuery)
            {
                consultaColumnas.CommandText = "SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'AERONAVES' AND COLUMN_NAME NOT LIKE 'AERO_FECHA%' AND COLUMN_NAME NOT LIKE 'AERO_BAJA%'";
            }
            else
            {
                consultaColumnas.CommandText = "SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'AERONAVES' AND COLUMN_NAME NOT LIKE 'AERO_FECHA%'";
            }

            consultaColumnasFechas.CommandText = "SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'AERONAVES' AND COLUMN_NAME LIKE 'AERO_FECHA%'";

            consultaColumnas.Connection = Program.conexion();

            varcampo = consultaColumnas.ExecuteReader();

            while (varcampo.Read())
            {
                this.cboCamposFiltro1.Items.Add(varcampo.GetValue(0));
                this.cboCamposFiltro2.Items.Add(varcampo.GetValue(0));
            }

            consultaColumnasFechas.Connection = Program.conexion();
            varfecha = consultaColumnasFechas.ExecuteReader();

            while (varfecha.Read())
            {
                this.cboCamposFiltro3.Items.Add(varfecha.GetValue(0));
            }

            /////////////////////////Fin carga contenido de combos//////////////////////////


            if (seSeteaQuery)
            {
                query = (anterior as Registro_Llegada_Destino.Form1).consultaSeteada();

                    label5.Visible = false;
                    cboCamposFiltro3.Visible = false;
                    dateTimePicker1.Visible = false;
                    button1.Visible = false;
                
            }
            else
            {
                this.extenderQuery();
            }

            ejecutarConsulta();

            txtFiltro1.Text = "";
            txtFiltro2.Text = "";

            cboCamposFiltro1.SelectedIndex = -1;
            cboCamposFiltro2.SelectedIndex = -1;
            cboCamposFiltro3.SelectedIndex = -1;

            this.huboCondicion = false;

            
        }

        public void extenderQuery()
        {
            query = "select a.SERV_COD, AERO_MATRI,AERO_MOD,AERO_FAB,SERV_DESC,AERO_CANT_BUTACAS,AERO_CANT_KGS,AERO_FECHA_ALTA,AERO_BAJA_FS,AERO_FECHA_FS,AERO_BAJA_VU,AERO_FECHA_BAJA,AERO_FECHA_RS from ABSTRACCIONX4.AERONAVES a JOIN ABSTRACCIONX4.SERVICIOS s ON (a.SERV_COD = s.SERV_COD)";

            if (this.loActivoGenerarViajes && this.serv_cod != null)
                this.query += " WHERE a.SERV_COD = " + this.serv_cod + " AND ABSTRACCIONX4.aeronave_disponible(AERO_MATRI, '"
                + this.fechaSalida + "', '" + this.fechaLlegada + "') = 1";
        }


        private void button4_Click_1(object sender, EventArgs e)
        {
            this.concatenarCriterio(txtFiltro2, cboCamposFiltro2, " = '" + txtFiltro2.Text + "'");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.concatenarCriterio(txtFiltro1, cboCamposFiltro1, " LIKE '%" + txtFiltro1.Text + "%'");
        }

       
        private void concatenarCriterio(TextBox txt, ComboBox combo, string criterio)
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
                
                this.query += combo.Text + criterio;
                MessageBox.Show("Se ha agregado el filtro sobre el campo " + combo.Text, "Filtro agregado", MessageBoxButtons.OK);
            }
        }

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

            if (combo.Text.Equals("AERO_CANT_BUTACAS") || combo.Text.Equals("AERO_CANT_KGS")) 
            {
                if (!this.esNumero(txt))
                {
                    MessageBox.Show("Para el campo " + combo.Text + " el criterio debe ser numerico", "Error en el tipo de dato del criterio", MessageBoxButtons.OK);
                    huboErrores = true;
                }
            }
            else if (combo.Text.Equals("AERO_MOD") || combo.Text.Equals("AERO_MATRI") || combo.Text.Equals("AERO_FAB") || combo.Text.Equals("SERV_COD"))
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

        private void button6_Click(object sender, EventArgs e)
        {
<<<<<<< HEAD
            if (llamadoDesdeModificacion)
                cambiarVisibilidades(this.siguiente);
            else
                cambiarVisibilidades(this.anterior);
=======
            cambiarVisibilidades(this.anterior, false);              
>>>>>>> d9697b819620f58acea8ebbb9e009a9a6e2a26ef
        }

        private void cambiarVisibilidades(Form formularioSiguiente)
        {
            formularioSiguiente.Visible = true;
            this.Visible = false;
        }


        private void button1_Click_1(object sender, EventArgs e)
        {
            this.concatenarCriterio(dateTimePicker1, cboCamposFiltro3, " = '" + dateTimePicker1.Value + "'");
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
            MessageBox.Show("query: "+this.query ,"error", MessageBoxButtons.OK);
            MessageBox.Show("Se ha agregado el filtro sobre el campo " + combo.Text, "Filtro agregado", MessageBoxButtons.OK);
            
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            if (dg.RowCount == 0)
            {
                MessageBox.Show("No se ha seleccionado ningún rol", "Selección invalida", MessageBoxButtons.OK);
                return;
            }

<<<<<<< HEAD
            List<Object> listaFuncionalidades = new List<object>(7);

            if (llamadoDesdeModificacion)
            {
               
                cambiarVisibilidades(this.siguiente);
                (siguiente as Modificacion).seSelecciono(dg.SelectedRows[0]);
=======
            if (seSeteaQuery)
            {
                (anterior as Registro_Llegada_Destino.Form1).seSeleccionoMatricula(dg.SelectedRows[0]);
>>>>>>> d9697b819620f58acea8ebbb9e009a9a6e2a26ef
            }

            if (this.loActivoGenerarViajes)
            {
                (anterior as Generacion_Viaje.Form1).seSeleccionoAeronave(dg.SelectedRows[0]);
            }
            
            if(this.loActivoModificar)
            {
                List<Object> listaFuncionalidades = new List<object>(7);
                (anterior as Modificacion).seSelecciono(dg.SelectedRows[0]);
            }
            
            //this.Close();
            
        }

    }
}
