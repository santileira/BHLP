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

namespace AerolineaFrba.Abm_Rol
{
    public partial class Baja : Form
    {
        private string query;
        private string ultimaQuery;
        Form formularioSiguiente;
        public Listado listado;

        public Baja()
        {
            InitializeComponent();
        }

        private void Listado_Load(object sender, EventArgs e)
        {
            this.iniciar();
        }

        // Realiza la consulta a la BD
        private void button3_Click_1(object sender, EventArgs e)
        {
            if (this.datosCorrectos())
            {
                bool huboCondicion = false;

                string querySelect = query;
               
                if (this.sePusoFiltro())
                    querySelect = querySelect + " AND ";
                else
                    MessageBox.Show("No se ha agregado ningún filtro. Agregue para poder realizar la búsqueda", "Informe", MessageBoxButtons.OK);

                if (/*txtFiltro1.TextLength != 0*/!Validacion.esVacio(txtFiltro1 , "No importa" , false))
                {
                  
                    string condicion = "ROL_NOMBRE" + " LIKE '%" + txtFiltro1.Text + "%'";
                    this.generarQuery(ref huboCondicion, ref querySelect, condicion);
                    
                }

                if (/*txtFiltro2.TextLength != 0*/!Validacion.esVacio(txtFiltro2 , "No importa" , false))
                {
                    string condicion = "ROL_NOMBRE" + "= '" + txtFiltro2.Text + "'";
                    this.generarQuery(ref huboCondicion, ref querySelect, condicion);
                }

               
                this.ejecutarQuery(querySelect);
                ultimaQuery = querySelect;
                txtFiltro1.Text = "";
                txtFiltro2.Text = "";
            
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
            return Validacion.filtrosContengaEIgualdad(txtFiltro1, txtFiltro2);
        
        }


        private void generarQuery(ref Boolean huboCondicion, ref string laQuery, string condicion)
        {
           if (huboCondicion)
               laQuery += " AND " + condicion;
           else
           {
               laQuery += condicion;
               huboCondicion = true;
           }
          
        }

        private void ejecutarQuery(string query)
        {

            SQLManager.ejecutarQuery(query, dg);

            
        }

        private void iniciar()
        {
            this.generarQueryInicial();
            this.ejecutarQuery(query);
            ultimaQuery = query;
           
           
            txtFiltro1.Text = "";
            txtFiltro2.Text = "";
         
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.cambiarVisibilidades(this.listado);
        }

        // Verifica si se presionó el botón de baja, la realiza de acuerdo a las validaciones correspondientes
        private void dg_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;
            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
            {
                if (e.ColumnIndex == 0)
                {
                    if(dg.Rows[e.RowIndex].Cells["Nombre"].Value.Equals("Administrador"))
                    {
                        MessageBox.Show("No puede darle de baja al rol de Administrador", "Error", MessageBoxButtons.OK);
                        return;
                    }
                    DialogResult resultado = mostrarMensaje("lógica");
                    if (apretoSi(resultado))
                    {
                        darDeBajaRol(darValorDadoIndex(e.RowIndex));
                        ejecutarQuery(query);
                        MessageBox.Show("Se ha dado de baja al rol correctamente", "Baja de roles", MessageBoxButtons.OK);
                    }
                }
            }
        }

        private Object darDeBajaRol(string nombre)
        {
            SQLManager sqlManager = new SQLManager();
            return sqlManager.generarSP("BajaRol")
                             .agregarStringSP("@Nombre", nombre)
                             .ejecutarSP();
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

        private Boolean apretoSi(DialogResult resultado){
            return resultado == System.Windows.Forms.DialogResult.Yes;
        }

        private DialogResult mostrarMensaje(string tipoDeBaja)
        {
            return MessageBox.Show("¿Está seguro que quiere dar de baja " + tipoDeBaja + " este registro?", "Advertencia", MessageBoxButtons.YesNo);
        }

        private string darValorDadoIndex(int index)
        {
            return dg.Rows[index].Cells["Nombre"].Value.ToString();
        }

        private void generarQueryInicial()
        {
            query = "SELECT ROL_NOMBRE AS 'Nombre' FROM [ABSTRACCIONX4].[ROLES] WHERE ROL_ESTADO = '1'";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.iniciar();
        }

        private void cambiarVisibilidades(Form formularioSiguiente)
        {
            formularioSiguiente.Visible = true;
            this.Visible = false;
        }

            
        
    }


     
}
