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
    
    public partial class Baja : Form
    {
        const string QUERY_BASE = "SELECT AERO_MATRI,AERO_MOD,AERO_FAB,SERV_DESC,AERO_CANT_BUTACAS,AERO_CANT_KGS,AERO_FECHA_ALTA FROM ABSTRACCIONX4.AERONAVES a JOIN ABSTRACCIONX4.SERVICIOS s ON (a.SERV_COD = s.SERV_COD) WHERE AERO_BAJA_VU = 0 AND AERO_BAJA_FS = 0";
        string query;
        Boolean huboCondicion;
        public Form anterior;
        private Form formularioSiguiente;
        private Boolean sePusoAgregarFiltro1 = false;
        private Boolean sePusoAgregarFiltro2 = false;
        private Boolean sePusoAgregarFiltro3 = false;
        private int filtro;
        
        
        public Baja()
        {
            InitializeComponent();

            //
            // Carga del contenido de combos
            //

            SqlDataReader varcampo;
            SqlDataReader varfecha;

            SqlCommand consultaColumnas = new SqlCommand();
            SqlCommand consultaColumnasFechas = new SqlCommand();

            consultaColumnas.CommandType = CommandType.Text;
            consultaColumnasFechas.CommandType = CommandType.Text;

            consultaColumnas.CommandText = "SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'AERONAVES' AND COLUMN_NAME NOT LIKE 'AERO_FECHA%'";
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

        }

        private void Listado_Load(object sender, EventArgs e)
        {
            this.iniciar();
            
        }

        //Boton Limpiar
        private void button2_Click(object sender, EventArgs e)
        {
            this.iniciar();
        }
     

        //Boton Buscar
        private void button3_Click(object sender, EventArgs e)
        {
            this.ejecutarQuery();
            if ((!sePusoAgregarFiltro1 || txtFiltros.TextLength != 0) && txtFiltro1.TextLength != 0 && cboCamposFiltro1.SelectedIndex != -1)
            {
                MessageBox.Show("No se ha agregado el filtro que contenga a la palabra. Agreguelo para tenerlo en cuenta", "Informe", MessageBoxButtons.OK);
            }
            if ((!sePusoAgregarFiltro2 || txtFiltros.TextLength != 0) && txtFiltro2.TextLength != 0 && cboCamposFiltro2.SelectedIndex != -1)
            {
                MessageBox.Show("No se ha agregado el filtro por igualdad de palabra. Agreguelo para tenerlo en cuenta", "Informe", MessageBoxButtons.OK);
            }
            if ((!sePusoAgregarFiltro3 || txtFiltros.TextLength != 0) && dateTimePicker1.Text.Length != 0 && cboCamposFiltro3.SelectedIndex != -1)
            {
                MessageBox.Show("No se ha agregado el filtro por fecha. Agreguelo para tenerlo en cuenta", "Informe", MessageBoxButtons.OK);
            }
            if (txtFiltro1.TextLength == 0 && cboCamposFiltro1.SelectedIndex != -1)
                MessageBox.Show("No se ha agregado contenido para el filtro que contenga a la palabra", "Informe", MessageBoxButtons.OK);
            if (txtFiltro2.TextLength == 0 && cboCamposFiltro2.SelectedIndex != -1)
                MessageBox.Show("No se ha agregado contenido para el filtro por igualdad de palabra", "Informe", MessageBoxButtons.OK);
            if (dateTimePicker1.Text.Length == 0 && cboCamposFiltro3.SelectedIndex != -1)
            {
                MessageBox.Show("No se ha agregado contenido para el filtro por fecha", "Informe", MessageBoxButtons.OK);
            }
            
            this.ejecutarQuery();
            if (dg.Rows.Count == 0)
                MessageBox.Show("No se han encontrado resultados en la consulta", "Informe", MessageBoxButtons.OK);
        }

        private Boolean sePusoFiltro()
        {
            return (txtFiltro1.TextLength != 0 || txtFiltro2.TextLength != 0 || cboCamposFiltro3.SelectedIndex != -1);
        }        

        public void ejecutarQuery()
        {
            sePusoAgregarFiltro1 = false;
            sePusoAgregarFiltro2 = false;
            sePusoAgregarFiltro3 = false;
            
            SqlConnection conexion = Program.conexion();

            DataTable t = new DataTable("Busqueda");
            SqlDataAdapter a = new SqlDataAdapter(this.query, conexion);
            //Llenar el Dataset
            DataSet ds = new DataSet();
            a.Fill(ds, "Busqueda");
            //Ligar el datagrid con la fuente de datos
            dg.DataSource = ds;
            dg.DataMember = "Busqueda";


            conexion.Close();


        }


        public void generarQueryInicial()
        {
            this.query = QUERY_BASE;
        }

        private void iniciar()
        {
            generarQueryInicial();

            ejecutarQuery();

            txtFiltro1.Text = "";
            txtFiltro2.Text = "";
            txtFiltro1.Enabled = false;
            txtFiltro2.Enabled = false;
            cboCamposFiltro1.SelectedIndex = -1;
            cboCamposFiltro2.SelectedIndex = -1;
            cboCamposFiltro3.SelectedIndex = -1;
            dateTimePicker1.Enabled = false;
            txtFiltros.Text = "";

            this.huboCondicion = false;
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            this.filtro = 2;
            if (txtFiltro2.Enabled && txtFiltro2.Text.Length != 0)
            {
                if (this.concatenarCriterio(txtFiltro2, cboCamposFiltro2, " = '" + txtFiltro2.Text + "'"))
                {
                    cboCamposFiltro2.SelectedIndex = -1;
                    txtFiltro2.Text = "";
                    this.sePusoAgregarFiltro2 = true;
                }
            }
            else
            {
                MessageBox.Show("Debe llenar el campo desplegable y el texto para poder agregar filtro", "Advertencia", MessageBoxButtons.OK);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.filtro = 1;
            if (txtFiltro1.Enabled && txtFiltro1.Text.Length != 0)
            {
                if (this.concatenarCriterio(txtFiltro1, cboCamposFiltro1, " LIKE '%" + txtFiltro1.Text + "%'"))
                {
                    txtFiltro1.Text = "";
                    this.sePusoAgregarFiltro1 = true;
                    cboCamposFiltro1.SelectedIndex = -1;
                }
            }
            else
            {
                MessageBox.Show("Debe llenar el campo desplegable y el texto para poder agregar filtro", "Advertencia", MessageBoxButtons.OK);
            }

        }

       
        private Boolean concatenarCriterio(TextBox txt, ComboBox combo, string criterio)
        {
            if (this.datosCorrectos(txt, combo))
            {
                /*if (!this.huboCondicion)
                {
                    this.huboCondicion = true;
                    this.query += " WHERE ";
                }
                else*/
                    this.query += " AND ";
                
                this.query += combo.Text + criterio;
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
            int n;
            return int.TryParse(txt.Text,out n);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            formularioSiguiente = new Principal();
            this.cambiarVisibilidades(formularioSiguiente);
        }

        private void cambiarVisibilidades(Form formularioSiguiente)
        {
            formularioSiguiente.Visible = true;
            this.Visible = false;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (dg.RowCount == 0)
            {
                MessageBox.Show("No se ha seleccionado ningún rol", "Selección invalida", MessageBoxButtons.OK);
                return;
            }
           
            //ejecutarSeleccion();

            //(anterior as Modificacion).seSelecciono();

            //this.Close();
            //cambiarVisibilidades(this.anterior, true);
        }

        private void ejecutarSeleccion()
        {
            //aca se debe volcar todo en los parametros
        }

        private void dg_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;
            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
            {
                if (e.ColumnIndex == 0)
                {
                    DialogResult resultado = mostrarMensaje("dejar fuera de servicio");
                    if (apretoSi(resultado))
                    {

                        this.dejarFueraDeServicio();
                    }
                }
                else
                    if (e.ColumnIndex == 1)
                    {
                        DialogResult resultado = mostrarMensaje("dar de baja lógica");
                        if (apretoSi(resultado))
                        {
                        
                            //ejecutarQuery(ultimaQuery);
                        }
                    }
            }
        }

        private void dejarFueraDeServicio()
        {
            throw new NotImplementedException();
        }
        
        private void button1_Click_1(object sender, EventArgs e)
        {
           
            if (cboCamposFiltro3.SelectedIndex != -1 && dateTimePicker1.Text.Length != 0)
            {
                if (this.concatenarCriterio(dateTimePicker1, cboCamposFiltro3, " = '" + dateTimePicker1.Value + "'"))
                {
                    txtFiltro1.Text = "";
                    this.sePusoAgregarFiltro3 = true;
                    cboCamposFiltro1.SelectedIndex = -1;
                }
            }
            else
            {
                MessageBox.Show("Debe llenar el campo desplegable y la fecha para poder agregar filtro", "Advertencia", MessageBoxButtons.OK);
            }
        }

        private Boolean concatenarCriterio(DateTimePicker dataTime, ComboBox combo, string criterio)
        {
            /*if (!this.huboCondicion)
            {
                    this.huboCondicion = true;
                    this.query += " WHERE ";
            }
            else*/
                    this.query += " AND ";

            this.query += combo.Text + criterio;
            string mensaje = "'" + dateTimePicker1.Value + "'" + " sobre el campo " + combo.Text;
            //MessageBox.Show("query: "+this.query ,"error", MessageBoxButtons.OK);
            txtFiltros.Text += "Se ha agregado el filtro por fecha " + mensaje + System.Environment.NewLine;
            
            return true;
        }

        private Boolean apretoSi(DialogResult resultado)
        {
            return resultado == System.Windows.Forms.DialogResult.Yes;
        }

        private DialogResult mostrarMensaje(string tipoDeBaja)
        {
            return MessageBox.Show("¿Está seguro que quiere " + tipoDeBaja + " esta aeronave?", "Advertencia", MessageBoxButtons.YesNo);
        }

        private void cboCamposFiltro3_SelectedIndexChanged(object sender, EventArgs e)
        {
            dateTimePicker1.Enabled = true;
        }

        private void cboCamposFiltro2_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFiltro2.Enabled = true;
        }

        private void cboCamposFiltro1_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFiltro1.Enabled = true;
        }



    }
}
