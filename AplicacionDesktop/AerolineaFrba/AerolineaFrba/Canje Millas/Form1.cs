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

namespace AerolineaFrba.Canje_Millas
{
    public partial class Form1 : Form
    {

        public int dni;
        public string apellido;
        public int millasDisponibles;
        public int millasDispFijas;
        public Form anterior;

        public Form1()
        {
            InitializeComponent();

            this.llenarListadoProductos();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            puntosDisp.Text = millasDisponibles.ToString();
        }

        private void llenarListadoProductos()
        {
            string query = "SELECT PREMIO_COD,PREMIO_DETALLE, PREMIO_PUNTOS,PREMIO_STOCK FROM ABSTRACCIONX4.PREMIOS";

            SqlConnection conexion = Program.conexion();

            DataTable t = new DataTable("Busqueda");
            SqlDataAdapter a = new SqlDataAdapter(query, conexion);
            //Llenar el Dataset
            DataSet ds = new DataSet();
            a.Fill(ds, "Busqueda");
            //Ligar el datagrid con la fuente de datos
            dgListadoProductos.DataSource = ds;
            dgListadoProductos.DataMember = "Busqueda";

            this.dgListadoProductos.Columns["PREMIO_COD"].Visible = false;


            conexion.Close();
        }

        private void dgListadoProductos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!dgListadoProductos.SelectedRows[0].Cells["PREMIO_STOCK"].Style.BackColor.Equals(Color.Gray))
            {
                button1.Enabled = true;
                string premio_detalle = dgListadoProductos.SelectedRows[0].Cells["PREMIO_DETALLE"].Value.ToString();
                txtProdSeleccionado.Text = premio_detalle;

            }
            else
            {
                button1.Enabled = false;
                dgListadoProductos.SelectedRows[0].Selected = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (hayStockDisponible())
            {
                this.agregarALista();
                dgListadoProductos.SelectedRows[0].Selected = false;
            }
            else
            {
                MessageBox.Show("No hay suficientes millas o no hay stock para realizar el canje", "Error", MessageBoxButtons.OK);
            }
        }

        private bool hayStockDisponible()
        {
            string cantStock = dgListadoProductos.SelectedRows[0].Cells["PREMIO_STOCK"].Value.ToString();
            int stockCant;
            stockCant = Convert.ToInt32(cantStock);
            stockCant = int.Parse(cantStock);

            string cantPuntos = dgListadoProductos.SelectedRows[0].Cells["PREMIO_PUNTOS"].Value.ToString();
            int puntos;
            puntos = Convert.ToInt32(cantPuntos);
            puntos = int.Parse(cantPuntos);

            int cantSeleccionada;
            cantSeleccionada = Convert.ToInt32(txtCantSeleccionada.Text);
            cantSeleccionada = int.Parse(txtCantSeleccionada.Text);


            int millasDisp = millasDisponibles - cantSeleccionada * puntos;

            if (millasDisp > 0)
            {

                if (stockCant >= cantSeleccionada)
                {
                    dgListadoProductos.SelectedRows[0].Cells["PREMIO_STOCK"].Style.BackColor = Color.Gray;
                    dgListadoProductos.SelectedRows[0].Cells["PREMIO_DETALLE"].Style.BackColor = Color.Gray;
                    dgListadoProductos.SelectedRows[0].Cells["PREMIO_PUNTOS"].Style.BackColor = Color.Gray;


                    millasDisponibles = millasDisp;
                    puntosDisp.Text = millasDisponibles.ToString();

                    return true;
                }
            }

            return false;

        }

        private void agregarALista()
        {
            listaPremiosSelec.Items.Add(((TextBox)txtProdSeleccionado).Text);
            listCantSelec.Items.Add(((TextBox)txtCantSeleccionada).Text);

        }

        private void button4_Click(object sender, EventArgs e)
        {
            listaPremiosSelec.Items.Clear();
            listCantSelec.Items.Clear();
            txtCantSeleccionada.Clear();
            txtProdSeleccionado.Clear();

            puntosDisp.Text = millasDispFijas.ToString();

            foreach (DataGridViewRow row in dgListadoProductos.Rows)
            {
                row.Cells["PREMIO_STOCK"].Style.BackColor = Color.White;
                row.Cells["PREMIO_DETALLE"].Style.BackColor = Color.White;
                row.Cells["PREMIO_PUNTOS"].Style.BackColor = Color.White;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int puntosDisponibles;
            int.TryParse(puntosDisp.Text, out puntosDisponibles);

            if (puntosDisponibles > 0)
            {

                int cantElementos = listaPremiosSelec.Items.Count;
                for (int i = 0; i < cantElementos; i++)
                {
                    string descripcion = listaPremiosSelec.Items[i].ToString();
                    int cantSelect;
                    int.TryParse(listCantSelec.Items[i].ToString(), out cantSelect);

                    new SQLManager().generarSP("reducirStockDePremio")
                                   .agregarStringSP("@descripcion", descripcion)
                                     .agregarIntSP("@cantidadSolicitada", cantSelect)
                                        .ejecutarSP();

                }

                int totalPuntos = millasDispFijas - millasDisponibles;

                MessageBox.Show("Puntos a Gastar: " + totalPuntos, "Data", MessageBoxButtons.OK);


                new SQLManager().generarSP("DescontarMillas")
                             .agregarIntSP("@cantMillas", totalPuntos)
                               .agregarIntSP("@dni", dni)
                                 .agregarStringSP("@ape", apellido)
                                    .ejecutarSP();

                MessageBox.Show("Se efectuó el Canje", "Canje Exitoso", MessageBoxButtons.OK);

                this.cambiarVisibilidades(new Menu());

            }
            else
            {
                MessageBox.Show("No hay suficientes millas para realizar el canje", "Error", MessageBoxButtons.OK);
                this.cambiarVisibilidades(anterior);
            }


        }

        private int obtenerPuntosDeUnPremio(string description)
        {

            SqlCommand command = new SqlCommand();
            command.Connection = Program.conexion();
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = "SELECT ABSTRACCIONX4.obtenerPuntosDePremio(@descripcion)";
            command.CommandTimeout = 0;

            command.Parameters.AddWithValue("@descripcion", description);

            return (int)command.ExecuteScalar();

        }

        private void cambiarVisibilidades(Form formularioSiguiente)
        {
            formularioSiguiente.Visible = true;
            this.Visible = false;
        }

    }
}
