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
        Dictionary<string, Action<string>> diccionarioFunc;
        Dictionary<string, Action<Form>> diccionarioFormularios;


        public Principal(string rolElegido)
        {
            this.rolElegido = rolElegido;
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
            diccionarioFormularios = new Dictionary<string, Action<Form>>();
            diccionarioFormularios.Add("ABM Aeronave Alta", Constructores.ABMAeronaveAlta);
            diccionarioFormularios.Add("ABM Aeronave Baja", Constructores.ABMAeronaveBaja);
            diccionarioFormularios.Add("ABM Aeronave Modificación", Constructores.ABMAeronaveModifiaccion);
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
            Action<Form> metodo;
            if(diccionarioFormularios.TryGetValue(((ToolStripButton)sender).Name,out metodo))
            {
                metodo.Invoke(this);
            }
        }

    }
}
