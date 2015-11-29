using Microsoft.SqlServer.Server;
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

namespace AerolineaFrba.Abm_Rol
{
    public partial class Modificacion : Form
    {



        public Modificacion()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.iniciar();
        }

        private void iniciar()
        {
            txtRolSeleccionado.Text = "";
            lstFuncionalidadesActuales.Items.Clear();
            txtNombre.Enabled = false;
            txtNombre.Text = "";
            button6.Enabled = false;
            button3.Enabled = false;
            button2.Enabled = false;
            checkHabilitado.Enabled = false;
            checkHabilitado.Checked = false;
            lstFuncionalidadesTotales.Enabled = false;

            button1.Enabled = false;

            string queryselect = "SELECT FUNC_DESC FROM [ABSTRACCIONX4].[FUNCIONALIDADES]";
            SqlCommand command = new SqlCommand(queryselect, Program.conexion());
            command.CommandType = System.Data.CommandType.Text;
            command.CommandTimeout = 0;
            SqlDataAdapter a = new SqlDataAdapter(command);
            DataTable t = new DataTable();
            //Llenar el Dataset
            a.Fill(t);

            lstFuncionalidadesTotales.DisplayMember = "FUNC_DESC";
            lstFuncionalidadesTotales.DataSource = t;

        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (datosCorrectos())
            {
                if ( String.Compare(txtRolSeleccionado.Text , "Administrador" , false) == 0 && txtRolSeleccionado.Text != txtNombre.Text)
                {
                    MessageBox.Show("No se puede modificar el nombre del rol Administrador" , "Informe" , MessageBoxButtons.OK);
                    return;
                }
                if (this.ExisteNombreRol())
                {
                    if (txtNombre.Text != txtRolSeleccionado.Text)
                    {
                        MessageBox.Show("El nombre ingresado ya existe. No es posible dar de alta el rol", "Informe", MessageBoxButtons.OK);
                        return;
                    }     
                }
                this.realizarModificacion();
                
            }
        }

        private void realizarModificacion()
        {
            DialogResult resultado = MessageBox.Show("Se procede a modificar el rol seleccionado", "Informe", MessageBoxButtons.YesNo);
            if (apretoSi(resultado))
            {
                modificarRol();
                MessageBox.Show("Se ha modificado al rol correctamente. Los cambios tendrán efecto al reiniciar la sesión.", "Modificación de roles", MessageBoxButtons.OK);
                this.Close();

            }
        }
        private bool ExisteNombreRol()
        {
            SqlCommand command = new SqlCommand();
            command.Connection = Program.conexion();
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = "SELECT ABSTRACCIONX4.ExisteNombreRol(@Nombre)";
            command.CommandTimeout = 0;

            command.Parameters.AddWithValue("@Nombre", txtNombre.Text);

            return (Boolean)command.ExecuteScalar();
        }

        private void modificarRol()
        {
            new SQLManager().generarSP("ModificarRol")
                            .agregarStringSP("@NombreOriginal", txtRolSeleccionado)
                            .agregarStringSP("@NombreNuevo", txtNombre)
                            .agregarListaSP("@Funcionalidades", funcionalidadesActuales())
                            .agregarBooleanoSP("@Estado",checkHabilitado.Enabled?checkHabilitado.Checked:true)
                            .ejecutarSP();
        }

        private List<Object> funcionalidadesActuales()
        {
            List<Object> funcionalidades = new List<Object>();
            foreach (Object func in lstFuncionalidadesActuales.Items)
            {
                funcionalidades.Add(func);
            }
            return funcionalidades;
        }

        private bool apretoSi(DialogResult resultado)
        {
            return resultado == System.Windows.Forms.DialogResult.Yes;
        }

        private Boolean datosCorrectos()
        {
            Boolean huboError = Validacion.esVacio(txtNombre, "nombre de rol", true);
            huboError = !Validacion.esSoloTexto(txtNombre, "nombre de rol", true) || huboError;

            return !huboError;
        }

        private void Alta_Load(object sender, EventArgs e)
        {
            this.iniciar();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.agregarALaLista(lstFuncionalidadesTotales, lstFuncionalidadesActuales);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.eliminarDeLaLista(lstFuncionalidadesActuales);
        }

        private void eliminarDeLaLista(ListBox lista)
        {
            if (lista.SelectedIndex != -1)
            {
                lista.Items.RemoveAt(lista.SelectedIndex);

            }
        }

        private void agregarALaLista(ListBox lista1, ListBox lista2)
        {
            string valor = lista1.Text;
            if (!lista2.Items.Contains(valor))
            {
                lista2.Items.Add(lista1.Text);

            }
        }

        private void cambiarVisibilidades(Form formularioSiguiente)
        {
            
            formularioSiguiente.Visible = true;
            this.Visible = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Listado listado = new Listado();
            listado.anterior = this;
            listado.llamadoDeModificacion = true;
            listado.ShowDialog();
            
        }

        public void seSelecciono(string nombreRol, bool habilitado, Object[] funcionalidadesRol)
        {

            txtRolSeleccionado.Text = nombreRol;
            txtRolSeleccionado.Enabled = true;
            txtNombre.Enabled = true;
            txtNombre.Text = nombreRol;

            lstFuncionalidadesActuales.Items.Clear();
            lstFuncionalidadesActuales.Items.AddRange(funcionalidadesRol);


            button6.Enabled = true;
            button3.Enabled = true;
            button2.Enabled = true;
            lstFuncionalidadesTotales.Enabled = true;

            checkHabilitado.Enabled = !habilitado;
            checkHabilitado.Checked = false;
            button1.Enabled = true;
        }
    }
}

        

      
    

