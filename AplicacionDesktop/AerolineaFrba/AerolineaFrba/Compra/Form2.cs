﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AerolineaFrba.Compra
{
    public partial class Form2 : Form
    {
        public Form anterior;

        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void cambiarVisibilidades(Form formularioSiguiente)
        {
            this.anterior.Visible = true;
            this.Visible = false;
        }

        public void seSelecciono(DataGridViewRow registro)
        {
            this.ejecutarQuery("select * from [ABSTRACCIONX4].butacasDisponibles('" + registro.Cells["VIAJE_COD"].Value.ToString() + "', '" + registro.Cells["AERO_MATRI"].Value.ToString() + "')");
        }

        private void ejecutarQuery(string query)
        {           
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
        
        private void button6_Click(object sender, EventArgs e)
        {
            this.cambiarVisibilidades(this.anterior);
        }
    }
}
