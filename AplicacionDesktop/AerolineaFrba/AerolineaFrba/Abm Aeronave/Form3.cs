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
        const string QUERY_BASE = "SELECT AERO_MATRI 'Matrícula',AERO_MOD 'Modelo',AERO_FAB 'Fabricante',SERV_DESC 'Tipo de servicio',AERO_CANT_KGS 'Cantidad de KG',AERO_FECHA_ALTA 'Fecha de alta', AERO_FECHA_BAJA 'Fecha de baja' FROM ABSTRACCIONX4.AERONAVES a JOIN ABSTRACCIONX4.SERVICIOS s ON (a.SERV_COD = s.SERV_COD)";
        string query;
        public Form anterior;
        private Boolean sePusoAgregarFiltro1 = false;
        private Boolean sePusoAgregarFiltro2 = false;
        private Boolean sePusoAgregarFiltro3 = false;
        private int filtro;
        private int indiceAeronaveElegida;
        public Boolean huboCondicion;

        public Baja()
        {
            InitializeComponent();
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
            if (txtFiltro1.TextLength == 0 && cboCamposFiltro1.SelectedIndex != -1)
                MessageBox.Show("No se ha agregado contenido para el filtro que contenga a la palabra", "Informe", MessageBoxButtons.OK);
            if (txtFiltro2.TextLength == 0 && cboCamposFiltro2.SelectedIndex != -1)
                MessageBox.Show("No se ha agregado contenido para el filtro por igualdad de palabra", "Informe", MessageBoxButtons.OK);

            this.ejecutarQuery();
            if (dg.Rows.Count == 0)
                MessageBox.Show("No se han encontrado resultados en la consulta", "Informe", MessageBoxButtons.OK);
        }

        private Boolean sePusoFiltro()
        {
            return (txtFiltro1.TextLength != 0 || txtFiltro2.TextLength != 0);
        }

        // Realiza la búsqueda a la BD para llenar el datargid de acuerdo a los filtros usados
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
            txtFiltros.Text = "";

            this.huboCondicion = false;
        }

        // Métodos que agregan los filtros a la consulta
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


        private Boolean concatenarCriterio(TextBox txt, ComboBox combo, string criterio)
        {
            if (this.datosCorrectos(txt, combo))
            {
                this.query += " AND ";

                this.query += this.nombreColumna(combo.Text) + criterio;
                string mensaje = "'" + txt.Text + "'" + " sobre el campo " + combo.Text;
                if (this.filtro == 1)
                    txtFiltros.Text += "Se ha agregado el filtro por contenido del valor " + mensaje + System.Environment.NewLine;
                else
                    txtFiltros.Text += "Se ha agregado el filtro por igualdad del valor " + mensaje + System.Environment.NewLine;
                return true;
            }
            return false;

        }

        // Validacion de textbox de filtros
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
            return int.TryParse(txt.Text, out n);
        }

        private void cambiarVisibilidades(Form formularioSiguiente)
        {
            formularioSiguiente.Visible = true;
            this.Visible = false;
        }

        // Selección de una fila
        private void button7_Click(object sender, EventArgs e)
        {
            if (dg.RowCount == 0)
            {
                MessageBox.Show("No se ha seleccionado ningún rol", "Selección invalida", MessageBoxButtons.OK);
                return;
            }
        }

        // Se ejecuta al hacer click en el datagrid, verifica si se presionó un boton de baja o FS
        // y llama a los métodos correspondientes
        private void dg_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;
            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
            {
                indiceAeronaveElegida = e.RowIndex;

                Object fechaAlta = dg.Rows[indiceAeronaveElegida].Cells["Fecha de alta"].Value;
                Object fechaBajaDefinitiva = dg.Rows[indiceAeronaveElegida].Cells["Fecha de baja"].Value;

                if (fechaAlta == DBNull.Value)
                {
                    fechaAlta = null;
                }
                if (fechaBajaDefinitiva == DBNull.Value)
                {
                    fechaBajaDefinitiva = null;
                }

                if (e.ColumnIndex == 0)
                {
                    new Form6(this, false, (Nullable<DateTime>)fechaAlta, (Nullable<DateTime>)fechaBajaDefinitiva).ShowDialog();
                    ejecutarQuery();
                }
                else
                    if (e.ColumnIndex == 1)
                    {
                        if (!dg.Rows[indiceAeronaveElegida].Cells["Fecha de baja"].Value.ToString().Equals(""))
                        {
                            MessageBox.Show("La aeronave elegida ya tiene una fecha de baja establecida", "Selección invalida", MessageBoxButtons.OK);
                            return;
                        }
                        new Form6(this, true, (Nullable<DateTime>)fechaAlta, (Nullable<DateTime>)fechaBajaDefinitiva).ShowDialog();
                        ejecutarQuery();
                    }
            }
        }

        public void darDeBajaLogica(DateTime fechaBaja)
        {
            string matricula = dg.Rows[indiceAeronaveElegida].Cells["Matrícula"].Value.ToString();
            SQLManager manager = new SQLManager();

            manager = manager.generarSP("DarDeBajaLogica").agregarStringSP("@Matricula", matricula)
                                                          .agregarFechaSP("@FechaBaja", fechaBaja);
            try
            {
                manager.ejecutarSP();
                MessageBox.Show("La fecha de baja de la aeronave fue cargada exitosamente", "Baja de aeronave", MessageBoxButtons.OK);
            }
            catch (Exception e)
            {
                if (e.Message.Contains("fuera de servicio"))
                {
                    MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK);
                    return;
                }
                new Form7(e.Message, matricula, true, fechaBaja, Program.fechaHoy()).ShowDialog();
            }
        }

        public void dejarFueraDeServicio(DateTime fechaReinicio, DateTime fechaBaja)
        {
            string matricula = dg.Rows[indiceAeronaveElegida].Cells["Matrícula"].Value.ToString();
            SQLManager manager = new SQLManager();

            manager = manager.generarSP("DejarAeronaveFueraDeServicio").agregarStringSP("@Matricula", matricula)
                                                                       .agregarFechaSP("@FechaBaja", fechaBaja)
                                                                       .agregarFechaSP("@FechaReinicio", fechaReinicio);
            try
            {
                manager.ejecutarSP();
                MessageBox.Show("El período de fuera de servicio se ha cargado exitosamente", "Fuera de servicio", MessageBoxButtons.OK);
            }
            catch (Exception e)
            {
                if (e.Message.Contains("fuera de servicio"))
                {
                    MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK);
                    return;
                }
                new Form7(e.Message, matricula,false, fechaBaja, fechaReinicio).ShowDialog();
            }
        }

        private Boolean apretoSi(DialogResult resultado)
        {
            return resultado == System.Windows.Forms.DialogResult.Yes;
        }



        private DialogResult mostrarMensaje(string tipoDeBaja)
        {
            return MessageBox.Show("¿Está seguro que quiere " + tipoDeBaja + " esta aeronave?", "Advertencia", MessageBoxButtons.YesNo);
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
