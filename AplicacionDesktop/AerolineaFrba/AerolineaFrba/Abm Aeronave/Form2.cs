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

namespace AerolineaFrba.Abm_Aeronave
{
    public partial class Alta : Form
    {

        public Listado listado;
        Form formularioSiguiente;
        

        public Alta()
        {
            InitializeComponent();                      
        }

        

        private void Alta_Load(object sender, EventArgs e)
        {
            this.inicio();
        }

        private void inicio()
        {
            cargarComboServicio();
            cargarComboFabricante();
            txtMatricula.Text = "";
            txtModelo.Text = "";
            txtModelo.Focus();
            txtButacas.Text = "";
            txtKilos.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.inicio();
        }

        
        private void cambiarVisibilidades(Form formularioSiguiente)
        {
            formularioSiguiente.Visible = true;
            this.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.datosCorrectos())
            {
                MessageBox.Show("Todos los datos son correctos. Se procede a dar de alta a la nueva aeronave", "Alta de nueva aeronave", MessageBoxButtons.OK);
                //HACER EL ALTA CON UNA STORE PROCEDURE            
            }

        }

        private bool datosCorrectos()
        {
            Boolean huboErrores = false;

            huboErrores = this.validarLongitudes() || huboErrores;
            huboErrores = this.validarTipos() || huboErrores;

            return !huboErrores;
        }

        private Boolean validarLongitudes()
        {
            Boolean algunoVacio = !this.seCompleto(txtModelo, "Modelo");
            algunoVacio =  !this.seCompleto(txtMatricula, "Matricula") || algunoVacio;
            algunoVacio =  !this.seCompleto(txtButacas, "Cantidad de butacas") || algunoVacio;
            algunoVacio = !this.seCompleto(txtKilos, "Cantidad de kilos") || algunoVacio;
            algunoVacio = !this.seCompleto(cboServicio, "Tipo de Servicio") || algunoVacio;
            algunoVacio = !this.seCompleto(cboFabricante, "Fabricante") || algunoVacio;

            return algunoVacio;
        }

        private Boolean seCompleto(TextBox txt, string campo)
        {
            if (txt.TextLength == 0)
            {
                MessageBox.Show("El campo " + campo + " no puede estar vacio", "Error en los datos de entrada", MessageBoxButtons.OK);
                return false;
            }
            else
                return true;
        }


        private Boolean seCompleto(ComboBox cbo, string campo)
        {
            if (cbo.SelectedText.Length == 0)
            {
                MessageBox.Show("El combo " + campo + " no puede estar vacio", "Error en los datos de entrada", MessageBoxButtons.OK);
                return false;
            }
            else
                return true;
        }

        private Boolean validarTipos()
        {
            Boolean huboError = false;

            if(txtModelo.TextLength != 0 && !this.esTexto(txtModelo))
            {
                MessageBox.Show("El campo modelo debe ser de tipo texto", "Error en los tipos de entrada", MessageBoxButtons.OK);
                huboError = true;
            }

            if(txtMatricula.TextLength != 0 && !this.esTexto(txtMatricula))
            {
                MessageBox.Show("El campo matricula debe ser de tipo texto", "Error en los tipos de entrada", MessageBoxButtons.OK);
                huboError = true;
            }
            
            if (txtButacas.TextLength != 0 && !this.esNumero(txtButacas))
            {
                MessageBox.Show("El campo butacas debe ser de tipo numerico", "Error en los tipos de entrada", MessageBoxButtons.OK);
                huboError = true;
            }

            if (txtKilos.TextLength != 0 && !this.esNumero(txtKilos))
            {
                MessageBox.Show("El campo kilos debe ser de tipo numerico", "Error en los tipos de entrada", MessageBoxButtons.OK);
                huboError = true;
            }

            if (txtKilos.TextLength != 0 && !this.esNumero(txtKilos))
            {
                MessageBox.Show("El campo kilos debe ser de tipo numerico", "Error en los tipos de entrada", MessageBoxButtons.OK);
                huboError = true;
            }


            return huboError;

        }        


        private void button6_Click(object sender, EventArgs e)
        {
            formularioSiguiente = new Principal();
            this.cambiarVisibilidades(formularioSiguiente);
        }

        private Boolean esTexto(TextBox txt)
        {
            String textPattern = "[A-Za-z]";
            System.Text.RegularExpressions.Regex regexTexto = new System.Text.RegularExpressions.Regex(textPattern);

            return regexTexto.IsMatch(txt.Text);
        }

        private Boolean esNumero(TextBox txt)
        {
            String numericPattern = "[0-9]";
            System.Text.RegularExpressions.Regex regexNumero = new System.Text.RegularExpressions.Regex(numericPattern);

            return regexNumero.IsMatch(txt.Text);
        }

        private void txtModelo_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtCampo_TextChanged(object sender, EventArgs e)
        {

        }
                

        private void txtFabricante_TextChanged(object sender, EventArgs e)
        {

        }

        private void cargarComboServicio()
        {
            cboServicio.Items.Clear();

            SqlDataReader reader;
            SqlCommand consultaServicios = new SqlCommand();
            consultaServicios.CommandType = CommandType.Text;
            consultaServicios.CommandText = "SELECT SERV_COD FROM [ABSTRACCIONX4].SERVICIOS";
            consultaServicios.Connection = Program.conexion();

            reader = consultaServicios.ExecuteReader();

            while (reader.Read())
                this.cboServicio.Items.Add(reader.GetValue(0));

            reader.Close();
        }

        private void cargarComboFabricante()
        {
            cboFabricante.Items.Clear();

            SqlDataReader reader;
            SqlCommand consultaServicios = new SqlCommand();
            consultaServicios.CommandType = CommandType.Text;
            consultaServicios.CommandText = "SELECT DISTINCT AERO_FAB FROM [ABSTRACCIONX4].[AERONAVES]";
            consultaServicios.Connection = Program.conexion();

            reader = consultaServicios.ExecuteReader();

            while (reader.Read())
                this.cboFabricante.Items.Add(reader.GetValue(0));

            reader.Close();
        }

        private void cboServicio_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

    }
}
