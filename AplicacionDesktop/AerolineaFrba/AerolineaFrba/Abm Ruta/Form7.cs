using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AerolineaFrba.Abm_Ruta
{
    public partial class Servicios : Form
    {
        Alta formAlta;
        Modificacion formModificacion;
        List<Object> listaServiciosIniciales;

        public Servicios(Alta formAlta, Modificacion formModificacion,List<Object> listaServiciosIniciales)
        {
            this.formAlta = formAlta;
            this.formModificacion = formModificacion;
            this.listaServiciosIniciales = listaServiciosIniciales;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.iniciar();
        }

        // Carga los listados con los servicios de la ruta
        private void iniciar()
        {
            lstServiciosActuales.Items.Clear();

            string queryselect = "SELECT SERV_DESC FROM [ABSTRACCIONX4].[SERVICIOS]";
            SqlCommand command = new SqlCommand(queryselect, Program.conexion());
            SqlDataAdapter a = new SqlDataAdapter(command);
            DataTable t = new DataTable();
            //Llenar el Dataset
            a.Fill(t);

            lstServiciosTotales.DisplayMember = "SERV_DESC";
            lstServiciosTotales.DataSource = t;

        }

        // Carga los servicios elegidos en el formulario del cual se llamó a éste
        private void button2_Click(object sender, EventArgs e)
        {
            if (formAlta != null)
            {
                formAlta.serviciosElegidos(serviciosActuales());
            }
            else
            {
                formModificacion.serviciosElegidos(serviciosActuales());
            }
            this.Close();
        }

        private List<Object> serviciosActuales()
        {
            List<Object> servicios = new List<Object>();
            foreach (Object func in lstServiciosActuales.Items)
            {
                servicios.Add(func);
            }
            return servicios;
        }

        private void Servicios_Load(object sender, EventArgs e)
        {
            this.iniciar();
            lstServiciosActuales.Items.Clear();
            lstServiciosActuales.Items.AddRange(listaServiciosIniciales.ToArray());
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.agregarALaLista(lstServiciosTotales, lstServiciosActuales);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.eliminarDeLaLista(lstServiciosActuales);
        }

        private void eliminarDeLaLista(ListBox lista)
        {
            if (lista.SelectedIndex != -1)
                lista.Items.RemoveAt(lista.SelectedIndex);
        }

        private void agregarALaLista(ListBox lista1, ListBox lista2)
        {
            string valor = lista1.Text;
            if (!lista2.Items.Contains(valor))
                lista2.Items.Add(lista1.Text);
        }
    }
}
