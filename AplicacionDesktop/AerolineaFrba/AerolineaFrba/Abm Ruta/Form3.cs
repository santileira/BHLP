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
        Boolean seleccionandoOrigen;
        
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
            this.query = "SELECT RUTA_ID 'Id' ,RUTA_COD 'Código' , SERV_DESC 'Servicio', ";
            this.query += this.buscarCiudad("R.CIU_COD_O") + " 'Origen', ";
            this.query +=this.buscarCiudad("R.CIU_COD_D") + " 'Destino', ";
            this.query += "RUTA_PRECIO_BASE_KG 'Precio Base Por Kilogramo', RUTA_PRECIO_BASE_PASAJE 'Precio Base Por Pasaje' ";
            this.query += "FROM [ABSTRACCIONX4].[RUTAS_AEREAS] R , [ABSTRACCIONX4].SERVICIOS S WHERE R.RUTA_ESTADO = '1' AND R.SERV_COD = S.SERV_COD AND [ABSTRACCIONX4].EstaSiendoUsada(RUTA_ID) = 0";
        }

        private string buscarCiudad(string cod)
        {
            return "(SELECT CIU_DESC FROM [ABSTRACCIONX4].[CIUDADES] C WHERE C.CIU_COD = " + cod + ")";
        }

        private void button3_Click(object sender, EventArgs e)
        {


            if ((!sePusoAgregarFiltro1 ||!Validacion.esVacio(txtFiltros)) && !Validacion.esVacio(txtFiltro1) && Validacion.estaSeleccionado(cboCamposFiltro1))
            {
                MessageBox.Show("No se ha agregado el filtro que contenga a la palabra. Agreguelo para tenerlo en cuenta", "Informe", MessageBoxButtons.OK);
            }
            if ((!sePusoAgregarFiltro2 || !Validacion.esVacio(txtFiltros)) && !Validacion.esVacio(txtFiltro2) && Validacion.estaSeleccionado(cboCamposFiltro2))
            {
                MessageBox.Show("No se ha agregado el filtro por igualdad de palabra. Agreguelo para tenerlo en cuenta", "Informe", MessageBoxButtons.OK);
            }
            if (Validacion.esVacio(txtFiltro1) &&Validacion.estaSeleccionado(cboCamposFiltro1))
                MessageBox.Show("No se ha agregado contenido para el filtro que contenga a la palabra", "Informe", MessageBoxButtons.OK);
            
            if (Validacion.esVacio(txtFiltro2) && Validacion.estaSeleccionado(cboCamposFiltro2))
                MessageBox.Show("No se ha agregado contenido para el filtro por igualdad de palabra", "Informe", MessageBoxButtons.OK);
            this.ejecutarQuery();
            

            if (dg.Rows.Count == 0)
                MessageBox.Show("No se han encontrado resultados en la consulta", "Informe", MessageBoxButtons.OK);
        }

        private Boolean sePusoFiltro()
        {
            return (!Validacion.esVacio(txtFiltro1) || !Validacion.esVacio(txtFiltro2) );          
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.iniciar();
        }

        private void ejecutarQuery()
        {

    
            /*
            SqlConnection conexion = Program.conexion();
            
            DataTable t = new DataTable("Busqueda");
            SqlDataAdapter a = new SqlDataAdapter(this.query, conexion);
            //Llenar el Dataset
            DataSet ds = new DataSet();
            a.Fill(ds, "Busqueda");
            //Ligar el datagrid con la fuente de datos
            dg.DataSource = ds;
            dg.DataMember = "Busqueda";
           
            conexion.Close();*/
            sePusoAgregarFiltro1 = false;
            sePusoAgregarFiltro2 = false;
            SQLManager.ejecutarQuery(query, dg);
        }

        private void iniciar()
        {
            this.generarQueryInicial();
            this.ejecutarQuery();
            
            txtFiltros.Text = "";


            txtDestino.Text = "";
            txtOrigen.Text = "";
         
            txtFiltro1.Text = "";
            txtFiltro2.Text = "";
            txtDestino.Text = "";
            button4.Enabled = false;
            button5.Enabled = false;
            txtFiltro1.Enabled = false;
            txtFiltro2.Enabled = false;
            cboCamposFiltro1.SelectedIndex = -1;
            cboCamposFiltro2.SelectedIndex = -1;
        
            this.huboCondicion = false;

            dg.Columns["Id"].Visible = false;
        }

        
        private void button5_Click(object sender, EventArgs e)
        {
            this.filtro = 1;
            if (txtFiltro1.Enabled && !Validacion.esVacio(txtFiltro1))
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

        private string buscarNombreCampo(string combo)
        {
            if (combo == "ORIGEN")
                return this.buscarCiudad("R.CIU_COD_O");
            else if (combo == "DESTINO")
                return this.buscarCiudad("R.CIU_COD_D");
            else if (combo == "TIPO_SERVICIO")
                return "SERV_DESC";
            else
                return combo;
        }

     


        private Boolean concatenarCriterio(TextBox txt, ComboBox combo, string criterio)
        {
            if (this.datosCorrectos(txt, combo))
            {
                
                this.query += " AND ";

                string campo = this.buscarNombreCampo(combo.Text);

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
                huboErrores = true;
            }

            if(!Validacion.estaSeleccionado(combo , true))
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
                    int idRuta = Convert.ToInt32(darValorDadoIndex(e.RowIndex,"Id"));
                    if (apretoSi(resultado))
                    {
                        darDeBajaRuta(idRuta);
                        ejecutarQuery();
                    }
                }
             
            }
        }


        private DialogResult mostrarMensaje(string tipoDeBaja)
        {
            return MessageBox.Show("¿Está seguro que quiere dar de baja " + tipoDeBaja + " este registro?", "Advertencia", MessageBoxButtons.YesNo);
        }

        private void darDeBajaRuta(int idRuta)
        {
            SQLManager sqlManager = new SQLManager().generarSP("BajaRuta")
                                                    .agregarIntSP("@IdRuta", idRuta);

            try
            {
                sqlManager.ejecutarSP();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error al dar de baja", MessageBoxButtons.OK);
            }
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
            if (Validacion.estaSeleccionado(cboCamposFiltro1))
            {
               txtFiltro1.Enabled = true;
               button5.Enabled = true;
            }
        }

        private void cboCamposFiltro2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Validacion.estaSeleccionado(cboCamposFiltro2))
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

        private void button7_Click(object sender, EventArgs e)
        {
            this.cambiarAListadoCiudades(true);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.cambiarAListadoCiudades(false);
        }


        public void cambiarAListadoCiudades(Boolean boolean )
        {
            ListadoCiudades list = new ListadoCiudades(this);
            this.seleccionandoOrigen = boolean;
            cambiarVisibilidades(list);
            list.vieneDeBaja = true;
            this.Visible = false;
        }

         public void seSelecciono(string ciudad)
        {
            if (seleccionandoOrigen)
            {
                txtOrigen.Text = ciudad;
                string campo = this.buscarNombreCampo("ORIGEN");
                this.query += " AND " + campo + " = '" + ciudad + "'";
            
            }
            else
            {
                txtDestino.Text = ciudad;
                string campo = this.buscarNombreCampo("DESTINO");
                this.query += " AND " + campo + " = '" + ciudad + "'";
            }
            this.ejecutarQuery();
        }


    }
}
