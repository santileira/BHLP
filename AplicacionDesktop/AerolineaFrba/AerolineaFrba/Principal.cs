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

namespace AerolineaFrba
{
    public partial class Principal : Form
    {
        private string rolElegido;
        string usuario;
        Dictionary<string, Action<string>> diccionarioFunc;
        Dictionary<string, Func<Form>> diccionarioFormularios;
        FormLogin login;
        Boolean cerrarAplicacion;

        public Principal(string rolElegido,string usuario,FormLogin login)
        {
            this.login = login;
            this.rolElegido = rolElegido;
            this.usuario = usuario;
            cerrarAplicacion = true;
            InitializeComponent();
        }

        private void Principal_Load(object sender, EventArgs e)
        {
            cargarDiccionarioFuncionalidades();
            cargarDiccionarioFormularios();
            generarMenu();
        }

        private void generarMenu()
        {
            foreach (string funcionalidad in listaFuncionalidades())
            {
                agregarAMenu(funcionalidad);
            }
            agregarBotonLogout();

            statusLabelUsuario.Text += usuario;
            statusLabelRol.Text += rolElegido;

        }

        private void agregarBotonLogout()
        {
            ToolStripButton boton = generarBoton("Salir",70);
            boton.Alignment = ToolStripItemAlignment.Right;
            boton.Margin = new Padding(0, 0, 25, 0);
            boton.Click += new System.EventHandler(salir_Click);
            menu.Items.Add(boton);
        }

        private void cargarDiccionarioFuncionalidades()
        {
            diccionarioFunc = new Dictionary<string, Action<string>>();
            diccionarioFunc.Add("ABM Rol", agregarABM);
            diccionarioFunc.Add("ABM Aeronave", agregarABM);
            diccionarioFunc.Add("ABM Ruta", agregarABM);
            diccionarioFunc.Add("Generación Viaje", agregarAFuncViajes);
            diccionarioFunc.Add("Registro Llegada Destino", agregarAFuncViajes);
            diccionarioFunc.Add("Canje Millas", agregarAFuncMillas);
            diccionarioFunc.Add("Consulta Millas", agregarAFuncMillas);
            diccionarioFunc.Add("Compra", agregarAFuncOperacion);
            diccionarioFunc.Add("Devolución", agregarAFuncOperacion);
            diccionarioFunc.Add("Registro de Usuario", agregarFuncionalidadUnica);
            diccionarioFunc.Add("Listado Estadístico", agregarFuncionalidadUnica);
        }

        private void cargarDiccionarioFormularios()
        {
            diccionarioFormularios = new Dictionary<string, Func<Form>>();
            diccionarioFormularios.Add("ABM Rol Alta", Constructores.ABMRolAlta);
            diccionarioFormularios.Add("ABM Rol Baja", Constructores.ABMRolBaja);
            diccionarioFormularios.Add("ABM Rol Modificación", Constructores.ABMRolModificacion);
            diccionarioFormularios.Add("ABM Aeronave Alta", Constructores.ABMAeronaveAlta);
            diccionarioFormularios.Add("ABM Aeronave Baja", Constructores.ABMAeronaveBaja);
            diccionarioFormularios.Add("ABM Aeronave Modificación", Constructores.ABMAeronaveModifiaccion);
            diccionarioFormularios.Add("ABM Ruta Alta", Constructores.ABMRutaAlta);
            diccionarioFormularios.Add("ABM Ruta Baja", Constructores.ABMRutaBaja);
            diccionarioFormularios.Add("ABM Ruta Modificación", Constructores.ABMRutaModificacion);
            diccionarioFormularios.Add("Generación Viaje", Constructores.GeneracionViaje);
            diccionarioFormularios.Add("Registro Llegada Destino", Constructores.RegistroLlegadaDestino);
            diccionarioFormularios.Add("Canje", Constructores.CanjeMillas);
            diccionarioFormularios.Add("Consulta", Constructores.ConsultaMillas);
            diccionarioFormularios.Add("Compra", Constructores.Compra);
            diccionarioFormularios.Add("Devolución", Constructores.Devolucion);
            diccionarioFormularios.Add("Registro de Usuario", Constructores.RegistroUsuario);
            diccionarioFormularios.Add("Listado Estadístico", Constructores.ListadoEstadístico);
        }

        private void agregarAMenu(string funcionalidad)
        {
            Action<string> operacion;

            if (diccionarioFunc.TryGetValue(funcionalidad, out operacion))
            {
                operacion.Invoke(funcionalidad);
            }
        }

        private void agregarABM(string nombre)
        {
            generarDesplegable(nombre);
            
            agregarADesplegable(nombre,generarBoton("Alta",80,nombre));
            agregarADesplegable(nombre, generarBoton("Baja", 80,nombre));
            agregarADesplegable(nombre, generarBoton("Modificación", 80,nombre));
        }

        private void agregarFuncionalidadUnica(string nombre)
        {
            menu.Items.Add(generarBoton(nombre,80));
        }

        private void agregarAFuncViajes(string nombre)
        {
            crearYAgregarADesplegable("Viajes",nombre,120);
        }

        private void agregarAFuncMillas(string nombre)
        {
            crearYAgregarADesplegable("Millas", nombre.Replace(" Millas",""),60);
        }

        private void agregarAFuncOperacion(string nombre)
        {
            crearYAgregarADesplegable("Operación", nombre,80);
        }

        private void crearYAgregarADesplegable(string desplegable, string funcionalidad,int longitudBoton)
        {
            if (menu.Items.Find(desplegable,false).Length==0)
            {
                generarDesplegable(desplegable);
            }

            agregarADesplegable(desplegable,generarBoton(funcionalidad,longitudBoton));
        }

        private List<string> listaFuncionalidades()
        {
            List<string> funcionalidades = new List<string>();

            string query = "SELECT FUNC_DESC FROM [ABSTRACCIONX4].ROLES r JOIN [ABSTRACCIONX4].FUNCIONES_ROLES fr ON (r.ROL_COD = fr.ROL_COD) JOIN [ABSTRACCIONX4].FUNCIONALIDADES f ON (f.FUNC_COD = fr.FUNC_COD) WHERE r.ROL_NOMBRE = '" + rolElegido + "'";
            SqlDataReader reader;
            SqlCommand consultaRoles = new SqlCommand();
            consultaRoles.CommandType = CommandType.Text;
            consultaRoles.CommandText = query;
            consultaRoles.Connection = Program.conexion();

            reader = consultaRoles.ExecuteReader();

            while (reader.Read())
                funcionalidades.Add(reader.GetValue(0).ToString());

            reader.Close();

            return funcionalidades;
        }

        private void generarDesplegable(string nombre)
        {
            ToolStripMenuItem menuDesplegable = new ToolStripMenuItem(nombre);
            menuDesplegable.Name = nombre;
            menu.Items.Add(menuDesplegable);
        }

        private void agregarADesplegable(string nombre,ToolStripButton boton)
        {
            ((ToolStripMenuItem)menu.Items.Find(nombre, false)[0]).DropDownItems.Add(boton);
        }


        private ToolStripButton generarBoton(string nombre, int longitud)
        {
            ToolStripButton boton = new ToolStripButton(nombre);
            boton.Name = nombre;
            boton.Width = longitud;
            boton.Click += new System.EventHandler(boton_Click);

            return boton;
        }

        private ToolStripButton generarBoton(string nombre, int longitud,string ABM)
        {
            ToolStripButton boton = new ToolStripButton(nombre);
            boton.Name = ABM + " " + nombre;
            boton.Width = longitud;
            boton.Click += new System.EventHandler(boton_Click);

            return boton;
        }

        private void boton_Click(object sender, EventArgs e)
        {
            Func<Form> metodo;
            if(diccionarioFormularios.TryGetValue(((ToolStripButton)sender).Name,out metodo))
            {
                mostrarForm(metodo.Invoke());
            }
        }

        private void salir_Click(object sender, EventArgs e)
        {
            cerrarAplicacion = false;
            this.Close();
            login.iniciar();
            login.Visible = true;
        }

        public void mostrarForm(Form formulario)
        {
            formulario.MdiParent = this;
            formulario.Show();
        }

        private void Principal_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if(cerrarAplicacion)
                login.Close();
        }

    }
}
