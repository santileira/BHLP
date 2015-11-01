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
    public partial class ListadoCiudades : Form
    {
        const string QUERY_BASE = "SELECT CIU_DESC 'Descripcion' FROM [ABSTRACCIONX4].[CIUDADES]";
        public Form anterior;
        public bool vieneDeAlta = false;
        public bool vieneDeArribo = false;
        public bool vieneDeModificacion = false;
        public bool vieneDeCompras = false;
        public bool vieneDeBaja = false;
        //public Listado listado;

        public ListadoCiudades(Form formAnterior)
        {
            anterior = formAnterior;
            InitializeComponent();
        }

        private void ListadoCiudades_Load(object sender, EventArgs e)
        {
            this.iniciar();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (this.datosCorrectos())
            {
                bool huboCondicion = false;

                string queryselect = QUERY_BASE;

                if (!this.sePusoFiltro())
                {
                    return;
                }

                queryselect = queryselect + " WHERE ";

                if (!Validacion.esVacio(txtFiltro1))
                {
                    string condicion = "CIU_DESC" + " LIKE '%" + txtFiltro1.Text + "%'";
                    this.generarQuery(ref huboCondicion, ref queryselect, condicion);
                }

                if (!Validacion.esVacio(txtFiltro2))
                {
                    string condicion = "CIU_DESC LIKE '_" + txtFiltro2.Text + "'";
                    this.generarQuery(ref huboCondicion, ref queryselect, condicion);
                }

                this.ejecutarConsulta(queryselect);

                if (dg.Rows.Count == 0)
                {
                    MessageBox.Show("No se han encontrado resultados en la consulta", "Informe", MessageBoxButtons.OK);
                }
            }
        }

        private Boolean sePusoFiltro()
        {
            return (!Validacion.esVacio(txtFiltro1) || !Validacion.esVacio(txtFiltro2));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.iniciar();
        }

        private Boolean datosCorrectos()
        {
            Boolean huboErrores = false;

            if (!Validacion.esTexto(txtFiltro1))
            {
                MessageBox.Show("El filtro que contenga la palabra debe ser una cadena de caracteres", "Error en el nombre", MessageBoxButtons.OK);
                huboErrores = true;
            }

            if (!Validacion.esTexto(txtFiltro2))
            {
                MessageBox.Show("El filtro por igualdad de palabra debe ser una cadena de caracteres", "Error en el nombre", MessageBoxButtons.OK);
                huboErrores = true;
            }

            return !huboErrores;

        }

        /*
        private Boolean esTexto(TextBox txt)
        {
            if (txt.Text.Length == 0)
            {
                return true;
            }

            String textPattern = "[A-Za-z]";
            System.Text.RegularExpressions.Regex regexTexto = new System.Text.RegularExpressions.Regex(textPattern);

            return regexTexto.IsMatch(txt.Text);
        }*/

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
            SQLManager.ejecutarQuery(query, dg);
            /*SqlCommand command = new SqlCommand();
            SqlConnection conexion = Program.conexion();

            DataTable t = new DataTable("Busqueda");
            SqlDataAdapter a = new SqlDataAdapter(query, conexion);
            //Llenar el Dataset
            DataSet ds = new DataSet();
            a.Fill(ds, "Busqueda");
            //Ligar el datagrid con la fuente de datos
            dg.DataSource = ds;
            dg.DataMember = "Busqueda";

            conexion.Close();*/
        }

  
        private void iniciar()
        {
            string queryselect = QUERY_BASE;

            ejecutarConsulta(queryselect);

            txtFiltro1.Text = "";
            txtFiltro2.Text = "";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            cambiarVisibilidades(this.anterior);
        }

        private void cambiarVisibilidades(Form formularioSiguiente)
        {
            anterior.Visible = true;
            this.Visible = false;
        }

        //boton de seleccionar, solo visible cuando se llama desde modificacion
        private void button4_Click(object sender, EventArgs e)
        {
            if (dg.RowCount==0)
            {
                MessageBox.Show("No se ha seleccionado ningún rol", "Selección invalida", MessageBoxButtons.OK);
                return;
            }

            cargarSeleccion();
            cambiarVisibilidades(this.anterior);
        }

        private void cargarSeleccion()
        {
            if (vieneDeAlta)
            {
                (anterior as Alta).seSelecciono(this.ciudadSeleccionada());
            }
            
            if(vieneDeModificacion)
            {
                (anterior as Modificacion).seSelecciono(this.ciudadSeleccionada());
            }

            if (vieneDeArribo)
            {
                (anterior as Registro_Llegada_Destino.Form1).seSelecciono(this.ciudadSeleccionada());
            }

            if (vieneDeCompras)
            {
                (anterior as Compra.Form1).seSelecciono(this.ciudadSeleccionada());
            }

            if (vieneDeBaja)
            {
                (anterior as Baja).seSelecciono(this.ciudadSeleccionada());
            }


        }


        private string ciudadSeleccionada()
        {
            return dg.SelectedRows[0].Cells["Descripcion"].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
            //this.cambiarVisibilidades(this.listado);
        }
        

    }
}
