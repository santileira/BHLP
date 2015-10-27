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
    public partial class Baja : Form
    {
        string query;
        Boolean huboCondicion;
        int filtro;
        private Boolean sePusoAgregarFiltro1 = false;
        private Boolean sePusoAgregarFiltro2 = false;
        Form formularioSiguiente;
        public Listado listado;
        
        public Baja()
        {
            InitializeComponent();
        }

        private void Listado_Load(object sender, EventArgs e)
        {
            this.iniciar();
        }

        private void generarQueryInicial()
        {
            this.query = "SELECT RUTA_ID,RUTA_COD , (SELECT S.SERV_DESC FROM [ABSTRACCIONX4].[SERVICIOS] S WHERE S.SERV_COD = R.SERV_COD) TIPO_SERVICIO, ";
            this.query += this.buscarCiudad("R.CIU_COD_O") + " ORIGEN, ";
            this.query +=this.buscarCiudad("R.CIU_COD_D") + " DESTINO, ";
            this.query += "RUTA_PRECIO_BASE_KG, RUTA_PRECIO_BASE_PASAJE ";
            this.query += "FROM [ABSTRACCIONX4].[RUTAS_AEREAS] R WHERE R.RUTA_ESTADO = '1' AND [ABSTRACCIONX4].EstaSiendoUsada(RUTA_ID) = 0";
        }

        private string buscarCiudad(string cod)
        {
            return "(SELECT CIU_DESC FROM [ABSTRACCIONX4].[CIUDADES] C WHERE C.CIU_COD = " + cod + ")";
        }

        private void button3_Click(object sender, EventArgs e)
        {


            if ((!sePusoAgregarFiltro1 || /*txtFiltros.TextLength != 0*/ !Validacion.esVacio(txtFiltros)) && !Validacion.esVacio(txtFiltro1) && /*cboCamposFiltro1.SelectedIndex != -1*/Validacion.estaSeleccionado(cboCamposFiltro1))
            {
                MessageBox.Show("No se ha agregado el filtro que contenga a la palabra. Agreguelo para tenerlo en cuenta", "Informe", MessageBoxButtons.OK);
            }
            if ((!sePusoAgregarFiltro2 || /*txtFiltros.TextLength != 0*/ !Validacion.esVacio(txtFiltros)) && !Validacion.esVacio(txtFiltro2) && /*cboCamposFiltro2.SelectedIndex != -1*/Validacion.estaSeleccionado(cboCamposFiltro2))
            {
                MessageBox.Show("No se ha agregado el filtro por igualdad de palabra. Agreguelo para tenerlo en cuenta", "Informe", MessageBoxButtons.OK);
            }
            if (Validacion.esVacio(txtFiltro1) && /*cboCamposFiltro1.SelectedIndex != -1*/Validacion.estaSeleccionado(cboCamposFiltro1))
                MessageBox.Show("No se ha agregado contenido para el filtro que contenga a la palabra", "Informe", MessageBoxButtons.OK);
            
            if (Validacion.esVacio(txtFiltro2) && /*cboCamposFiltro2.SelectedIndex != -1*/Validacion.estaSeleccionado(cboCamposFiltro2))
                MessageBox.Show("No se ha agregado contenido para el filtro por igualdad de palabra", "Informe", MessageBoxButtons.OK);
            this.ejecutarQuery();
            

            if (dg.Rows.Count == 0)
                MessageBox.Show("No se han encontrado resultados en la consulta", "Informe", MessageBoxButtons.OK);
        }

        private Boolean sePusoFiltro()
        {
            return (!Validacion.esVacio(txtFiltro1) || !Validacion.esVacio(txtFiltro2) || Validacion.estaSeleccionado(cboFiltro3)/*cboFiltro3.SelectedIndex != -1*/);          
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.iniciar();
        }

        private void ejecutarQuery()
        {
            sePusoAgregarFiltro1 = false;
            sePusoAgregarFiltro2 = false;
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

        private void iniciar()
        {
            this.generarQueryInicial();
            this.ejecutarQuery();
            
            txtFiltros.Text = "";
         
        

            txtFiltro1.Text = "";
            txtFiltro2.Text = "";
            txtFiltro4.Text = "";
            button4.Enabled = false;
            button5.Enabled = false;
            txtFiltro1.Enabled = false;
            txtFiltro2.Enabled = false;
            cboCamposFiltro1.SelectedIndex = -1;
            cboCamposFiltro2.SelectedIndex = -1;
            cboFiltro3.SelectedIndex = -1;

            this.huboCondicion = false;

            dg.Columns["RUTA_ID"].Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //this.cambiarVisibilidades(this.listado);
            throw new NotFiniteNumberException();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.filtro = 1;
            if (txtFiltro1.Enabled && !Validacion.esVacio(txtFiltro1)/*txtFiltro1.Text.Length != 0*/)
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

        private string buscarNombreCampo(ComboBox combo)
        {
            if (combo.Text == "ORIGEN")
                return this.buscarCiudad("R.CIU_COD_O");
            else if (combo.Text == "DESTINO")
                return this.buscarCiudad("R.CIU_COD_D");
            else if (combo.Text == "TIPO_SERVICIO")
                return "SERV_COD";
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

            
            if (Validacion.esVacio(txt , "criterio" , true))
            {
                //MessageBox.Show("El criterio no puede estar en blanco", "Error en el criterio", MessageBoxButtons.OK);
                huboErrores = true;
            }

            if(Validacion.estaSeleccionado(combo , true))
            {
                //MessageBox.Show("Debe seleccionar un campo en el desplegable de opciones", "Error en el campo", MessageBoxButtons.OK);
                huboErrores = true;
            }

            if (combo.Text.Equals("RUTA_COD") || combo.Text.Equals("RUTA_PRECIO_BASE_KG") || combo.Text.Equals("RUTA_PRECIO_BASE_PASAJE"))
            {
                if (!Validacion.esNumero(txt , combo.Text , true))
                {
                    /*MessageBox.Show("Para el campo " + combo.Text + " el criterio debe ser numerico", "Error en el tipo de dato del criterio", MessageBoxButtons.OK);*/
                    huboErrores = true;
                }
            }
            else if(combo.Text.Equals("TIPO_SERVICIO") || combo.Text.Equals("ORIGEN") || combo.Text.Equals("DESTINO"))
            {
                if (!Validacion.esTexto(txt , combo.Text , true))
                {
                    /*MessageBox.Show("Para el campo " + combo.Text + " el criterio debe ser texto", "Error en el nombre", MessageBoxButtons.OK);*/
                    huboErrores = true;
                }
            }

            return !huboErrores;
        }

        /*private Boolean esTexto(TextBox txt)
        {
            String textPattern = "[A-Za-z]";
            System.Text.RegularExpressions.Regex regexTexto = new System.Text.RegularExpressions.Regex(textPattern);

            return regexTexto.IsMatch(txt.Text);
        }*/

        /*private Boolean esNumero(TextBox txt)
        {
            String numericPattern = "[0-9]";
            System.Text.RegularExpressions.Regex regexNumero = new System.Text.RegularExpressions.Regex(numericPattern);

            return regexNumero.IsMatch(txt.Text);
        }*/

        private void button4_Click_1(object sender, EventArgs e)
        {
            this.filtro = 2;
            if (txtFiltro2.Enabled && /*txtFiltro2.Text.Length != 0*/!Validacion.esVacio(txtFiltro2))
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
                    MessageBox.Show("Debe llenar el texto para poder agregar filtro", "Advertencia", MessageBoxButtons.OK);
                }
        }


        private void dg_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;
            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
            {
                if (e.ColumnIndex == 0)
                {
                    DialogResult resultado = mostrarMensaje("lógica");
                    int idRuta = Convert.ToInt32(darValorDadoIndex(e.RowIndex,"RUTA_ID"));
                    if (apretoSi(resultado))
                    {
                        darDeBajaRuta(idRuta);
                        /*darDeBajaPasajes(idRuta);
                        darDeBajaEncomienda(idRuta);*/
                        ejecutarQuery();
                    }
                }
                //BAJA FISICA
                /*else
                    if (e.ColumnIndex == 1)
                    {
                        DialogResult resultado = mostrarMensaje("física");
                        if (apretoSi(resultado))
                        {
                            string cadenaComando = "DELETE FROM [ABSTRACCIONX4].[RUTAS_AEREAS] WHERE RUTA_COD = '" + darValorDadoIndex(e.RowIndex , "RUTA_COD");
                            ejecutarCommand(cadenaComando);
                            ejecutarQuery();
                        }
                    }*/
            }
        }

       /* private Object darDeBajaEncomienda(int idRuta)
        {
            SQLManager sqlManager = new SQLManager();
            MessageBox.Show(idRuta.ToString(), "Ruta id", MessageBoxButtons.OK);
            return sqlManager.generarSP("BorrarEncomiendas").agregarIntSP("@IdRuta", idRuta).ejecutarSP();
        }

        private Object darDeBajaPasajes(int idRuta)
        {
            SQLManager sqlManager = new SQLManager();
            MessageBox.Show(idRuta.ToString(), "Ruta id", MessageBoxButtons.OK);
            return sqlManager.generarSP("BorrarPasajes").agregarIntSP("@IdRuta", idRuta).ejecutarSP();
        }*/

        private DialogResult mostrarMensaje(string tipoDeBaja)
        {
            return MessageBox.Show("¿Está seguro que quiere dar de baja " + tipoDeBaja + " este registro?", "Advertencia", MessageBoxButtons.YesNo);
        }

        private Object darDeBajaRuta(int idRuta)
        {
            SQLManager sqlManager = new SQLManager();
            return sqlManager.generarSP("BajaRuta").agregarIntSP("@IdRuta", idRuta).ejecutarSP();
           
            /*SqlCommand command = new SqlCommand();
            command.Connection = Program.conexion();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "[GD2C2015].[ABSTRACCIONX4].[BajaRuta]";
            command.CommandTimeout = 0;

            command.Parameters.AddWithValue("@IdRuta", idRuta);

            return command.ExecuteScalar();*/
        }

        private void ejecutarCommand(string cadenaComando)
        {
            SqlCommand command = new SqlCommand();
            command.Connection = Program.conexion();
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = cadenaComando;
            command.CommandTimeout = 0;
            command.ExecuteReader().Close();
        }

        private Boolean apretoSi(DialogResult resultado)
        {
            return resultado == System.Windows.Forms.DialogResult.Yes;
        }

        private string darValorDadoIndex(int index , string fila)
        {
            return  dg.Rows[index].Cells[fila].Value.ToString();
        }

        private void cboCamposFiltro1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (/*cboCamposFiltro1.SelectedIndex != -1*/Validacion.estaSeleccionado(cboCamposFiltro1))
            {
               txtFiltro1.Enabled = true;
               button5.Enabled = true;
            }
        }

        private void cboCamposFiltro2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (/*cboCamposFiltro2.SelectedIndex != -1*/Validacion.estaSeleccionado(cboCamposFiltro2))
            {
                txtFiltro2.Enabled = true;
                button4.Enabled = true;
            }
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

    }
}
