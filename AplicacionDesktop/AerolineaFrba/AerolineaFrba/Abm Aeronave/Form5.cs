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

        public Boolean altaActiva = false;

        public Alta alta;
        
        public Listado()
        {
            InitializeComponent();

            //
            // Carga del contenido de combos
            //

            SqlDataReader variable;
            SqlCommand consultaColumnas = new SqlCommand();
            consultaColumnas.CommandType = CommandType.Text;
            consultaColumnas.CommandText = "SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'AERONAVES'";
            consultaColumnas.Connection = Program.conexion();

            variable = consultaColumnas.ExecuteReader();

            while (variable.Read())
            {
                this.cboCamposFiltro1.Items.Add(variable.GetValue(0));
                this.cboCamposFiltro2.Items.Add(variable.GetValue(0));
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
            this.ejecutarConsulta();

            if (dg.Rows.Count == 0)
                MessageBox.Show("No se han encontrado resultados en la consulta", "Informe", MessageBoxButtons.OK);
        }

        private Boolean sePusoFiltro()
        {
            return (txtFiltro1.TextLength != 0 || txtFiltro2.TextLength != 0 || cboFiltro3.SelectedIndex != -1);
        }        

        private void ejecutarConsulta()
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

            conexion.Close();
        }


        private void generarQueryInicial()
        {
            this.query = "SELECT AERO_MOD,AERO_MATRI,AERO_FAB,SERV_COD,AERO_CANT_BUTACAS,AERO_CANT_KGS FROM [ABSTRACCIONX4].[AERONAVES]";
        }

        private void iniciar()
        {
            this.generarQueryInicial();

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


            txtFiltro1.Text = "";
            txtFiltro2.Text = "";
            txtFiltro4.Text = "";

            cboCamposFiltro1.SelectedIndex = -1;
            cboCamposFiltro2.SelectedIndex = -1;
            cboFiltro3.SelectedIndex = -1;

            this.huboCondicion = false;

            button7.Enabled = false;
                                   

        }

        

        private void button1_Click(object sender, EventArgs e)
        {

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
            if(this.altaActiva)
                cambiarVisibilidades(this.alta);
            
        }

        private void cambiarVisibilidades(Form formularioSiguiente)
        {
            formularioSiguiente.Visible = true;
            this.Visible = false;
        }

        private void dg_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            button7.Enabled = true;      
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (this.altaActiva)
            {
                string valor = dg.CurrentCell.Value.ToString();
                this.alta.setFiltroSelector(valor);
                MessageBox.Show("Ha seleccionado el valor " + valor + " para dar de alta en el campo " + this.alta.getCampoSelector(), "Error en el nombre", MessageBoxButtons.OK);
            }
        }

    }
}
