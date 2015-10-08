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
        
        public Listado()
        {
            InitializeComponent();
        }

        private void Listado_Load(object sender, EventArgs e)
        {
            this.iniciar();
        }

        private void generarQueryInicial()
        {
            this.query = "SELECT RUTA_COD, SERV_COD, ";
            this.query += "(SELECT CIU_DESC FROM [ABSTRACCIONX4].[CIUDADES] C WHERE C.CIU_COD = R.CIU_COD_O) ORIGEN, ";
            this.query += "(SELECT CIU_DESC FROM [ABSTRACCIONX4].[CIUDADES] C WHERE C.CIU_COD = R.CIU_COD_D) DESTINO, ";
            this.query += "RUTA_PRECIO_BASE_KG, RUTA_PRECIO_BASE_PASAJE, RUTA_ESTADO ";
            this.query += "FROM [ABSTRACCIONX4].[RUTAS_AEREAS] R";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (this.datosCorrectos())
            {
                bool huboCondicion = false;

                string queryselect = "SELECT * FROM [ABSTRACCIONX4].[ROLES]";
           
                if (this.sePusoFiltro())
                    queryselect = queryselect + " WHERE ";

                if (txtFiltro1.TextLength != 0)
                {
                    string condicion = "ROL_NOMBRE" + " LIKE '%" + txtFiltro1.Text + "%'";
                    this.generarQuery(ref huboCondicion, ref queryselect, condicion);
                }

                if (txtFiltro2.TextLength != 0)
                {
                    string condicion = "ROL_NOMBRE" + "= '" + txtFiltro2.Text + "'";
                    this.generarQuery(ref huboCondicion, ref queryselect, condicion);
                }

                if (chkEstadoIgnorar.Checked == false)
                {
                    string condicion;
                    if (optEstadoAlta.Enabled)
                    {
                       condicion = "ROL_ESTADO" + " = 1";
                    }
                    else
                    {
                        condicion = "ROL_ESTADO" + " = 0";
                    }
                this.generarQuery(ref huboCondicion, ref queryselect, condicion);
                }

                this.ejecutarConsulta(queryselect);

                if (dg.Rows.Count == 1)
                {
                    MessageBox.Show("No se han encontrado resultados en la consulta", "Informe", MessageBoxButtons.OK);
                }
            }
        }

        private Boolean sePusoFiltro()
        {
            return (txtFiltro1.TextLength != 0 || txtFiltro2.TextLength != 0 || cboFiltro3.SelectedIndex != -1);          
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.iniciar();
        }

        private void generarQuery(ref Boolean huboCondicion, ref string queryselect, string condicion)
        {
           if (huboCondicion)
               queryselect += " AND " + condicion;
           else
           {
               queryselect += condicion;
               huboCondicion = true;
           }
        }

        private void ejecutarConsulta(string query)
        {
            SqlCommand command = new SqlCommand();
            SqlConnection conexion = Program.conexion();

            DataTable t = new DataTable("Busqueda");
            SqlDataAdapter a = new SqlDataAdapter(query, conexion);
            //Llenar el Dataset
            DataSet ds = new DataSet();
            a.Fill(ds, "Busqueda");
            //Ligar el datagrid con la fuente de datos
            dg.DataSource = ds;
            dg.DataMember = "Busqueda";

            conexion.Close();
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

            chkEstadoIgnorar.Checked = true;
            optEstadoAlta.Checked = true;
            optEstadoBaja.Checked = false;

            txtFiltro1.Text = "";
            txtFiltro2.Text = "";
            txtFiltro4.Text = "";

            cboCamposFiltro1.SelectedIndex = -1;
            cboCamposFiltro2.SelectedIndex = -1;
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

        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.concatenarCriterio(txtFiltro1, cboCamposFiltro1);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.concatenarCriterio(txtFiltro2, cboCamposFiltro2);
        }

        private void concatenarCriterio(TextBox txt, ComboBox combo)
        {
            if (this.datosCorrectos(txt, combo))
            {
                if (!this.huboCondicion)
                {
                    this.huboCondicion = true;
                    this.query += " WHERE '";
                }
                else
                    this.query += " AND '";

                this.query += combo.Text + "' LIKE %'" + txt.Text + "'%";
            }
        }

        private Boolean datosCorrectos(TextBox txt, ComboBox combo)
        {
            Boolean huboErrores = false;

            if (txt.TextLength == 0)
            {
                MessageBox.Show("El nombre no puede estar en blanco", "Error en el nombre", MessageBoxButtons.OK);
                huboErrores = true;
            }

            if(combo.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar un campo en el desplegable de opciones", "Error en el campo", MessageBoxButtons.OK);
                huboErrores = true;
            }

            if (combo.Text == "RUTA_COD" || combo.Text == "RUTA_PRECIO_BASE_KG" || combo.Text == "RUTA_PRECIO_BASE_PASAJE")
            {
                if (!this.esNumero(txt))
                {
                    MessageBox.Show("Para el campo " + combo.Text + " el criterio debe ser numerico", "Error en el nombre", MessageBoxButtons.OK);
                    huboErrores = true;
                }
            }
            else
            {
                MessageBox.Show("Para el campo " + combo.Text + " el criterio debe ser texto", "Error en el nombre", MessageBoxButtons.OK);
                huboErrores = true;
            }

            if (!this.esTexto(txt))
            {
                MessageBox.Show("El nombre debe ser una cadena de caracteres", "Error en el nombre", MessageBoxButtons.OK);
                huboErrores = true;
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

    }
}
