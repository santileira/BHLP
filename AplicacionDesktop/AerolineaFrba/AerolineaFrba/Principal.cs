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


        public Principal(string rolElegido)
        {
            this.rolElegido = rolElegido;
            InitializeComponent();
        }

        private void Principal_Load(object sender, EventArgs e)
        {
            cargarDiccionarioFuncionalidades();
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
            diccionarioFunc.Add("Generar Viaje", agregarFuncionalidadUnica);
        }

        private void agregarAMenu(string funcionalidad)
        {
            Action<string> operacion;

            diccionarioFunc.TryGetValue(funcionalidad, out operacion);

            operacion.Invoke(funcionalidad);
        }

        private void agregarABM(string nombre)
        {
            ToolStripMenuItem menuABM = new ToolStripMenuItem(nombre);
            
            ToolStripButton botonAlta = new ToolStripButton("Alta");
            ToolStripButton botonBaja = new ToolStripButton("Baja");
            ToolStripButton botonModificacion = new ToolStripButton("Modificacion");

            botonAlta.Width = botonBaja.Width = botonModificacion.Width = 80;

            menuABM.DropDownItems.Add(botonAlta);
            menuABM.DropDownItems.Add(botonBaja);
            menuABM.DropDownItems.Add(botonModificacion);

            menu.Items.Add(menuABM);
        }

        private void agregarFuncionalidadUnica(string nombre)
        {
            menu.Items.Add(new ToolStripButton(nombre));
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

    }
}
