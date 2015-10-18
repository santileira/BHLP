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
    public partial class Modificacion : Form
    {
        Form formularioSiguiente;

        public Listado listado;

        public Modificacion()
        {
            InitializeComponent();
        }

        private void Modificacion_Load(object sender, EventArgs e)
        {
            this.inicio();
        }

        public void seSelecciono(DataGridViewRow registro)
        {
            cargarComboServicio();
            cargarComboFabricante();

            txtModelo.Text = registro.Cells["AERO_MOD"].Value.ToString();
            txtMatricula.Text = registro.Cells["AERO_MATRI"].Value.ToString();
            cboFabricante.Text = registro.Cells["AERO_FAB"].Value.ToString();
            cboServicio.Text = registro.Cells["SERV_DESC"].Value.ToString();
            txtButacas.Text = registro.Cells["AERO_CANT_BUTACAS"].Value.ToString();
            txtKilos.Text = registro.Cells["AERO_CANT_KGS"].Value.ToString();

            if (registro.Cells["AERO_FECHA_RS"].Value.ToString().GetType() == Type.GetType("DateTime"))
                dtTime.Value = Convert.ToDateTime(registro.Cells["AERO_FECHA_RS"].Value.ToString());
          
            txtButacas.Enabled = true;
            txtKilos.Enabled = true;
            txtMatricula.Enabled = true;
            txtModelo.Enabled = true;

            cboServicio.Enabled = true;
            cboFabricante.Enabled = true;

            button2.Enabled = true;
            button3.Enabled = true;

            chkReinicio.Enabled = registro.Cells["FUERA_SERVICIO"].Value.Equals("SI");
            
        }

        private void inicio()
        {
            txtButacas.Text = "";
            txtKilos.Text = "";
            txtMatricula.Text = "";
            txtModelo.Text = "";

            txtButacas.Enabled = false;
            txtKilos.Enabled = false;
            txtMatricula.Enabled = false;
            txtModelo.Enabled = false;

            cboFabricante.SelectedIndex = -1;
            cboServicio.SelectedIndex = -1;

            cboServicio.Enabled = false;
            cboFabricante.Enabled = false;

            cboFabricante.Text = "";
            cboServicio.Text = "";

            chkReinicio.Checked = false;
            chkReinicio.Enabled = false;

            dtTime.Enabled = false;
            dtTime.Value = DateTime.Now.Date;

            button2.Enabled = false;
            button3.Enabled = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            formularioSiguiente = new Principal();
            this.cambiarVisibilidades(formularioSiguiente);
        }

        private void cambiarVisibilidades(Form formularioSiguiente)
        {
            formularioSiguiente.Visible = true;
            this.Visible = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.cambiarVisibilidades(this.listado);
        }

        private void chkReinicio_CheckedChanged(object sender, EventArgs e)
        {
            if (chkReinicio.Checked)
                dtTime.Enabled = true;
            else
                dtTime.Enabled = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.inicio();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.datosCorrectos())
            {
                MessageBox.Show("Todos los datos son correctos. Se procede a modificar el registro de aeronave", "Alta de nueva aeronave", MessageBoxButtons.OK);
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
            algunoVacio = !this.seCompleto(txtMatricula, "Matricula") || algunoVacio;
            algunoVacio = !this.seCompleto(cboServicio, "Tipo de Servicio") || algunoVacio;
            algunoVacio = !this.seCompleto(cboFabricante, "Fabricante") || algunoVacio;
            algunoVacio = !this.seCompleto(txtButacas, "Cantidad de butacas") || algunoVacio;
            algunoVacio = !this.seCompleto(txtKilos, "Cantidad de kilos") || algunoVacio;

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
            if (cbo.Text.Equals(""))
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

            huboError = !esTexto(txtModelo, "modelo") || huboError;
            huboError = !esTexto(txtMatricula, "matrícula") || huboError;
            huboError = !esNumero(txtButacas, "cantidad de butacas", true) || huboError;
            huboError = !esNumero(txtKilos, "cantidad de Kg", false) || huboError;

            return huboError;
        }


        private Boolean esTexto(TextBox txt, string campo)
        {
            String textPattern = "[A-Za-z]";
            System.Text.RegularExpressions.Regex regexTexto = new System.Text.RegularExpressions.Regex(textPattern);

            if (txt.TextLength != 0 && !regexTexto.IsMatch(txt.Text))
            {
                MessageBox.Show("El campo " + campo + " debe ser un texto", "Error en los tipos de entrada", MessageBoxButtons.OK);
                return true;
            }
            return false;
        }

        private Boolean esNumero(TextBox txt, string campo, bool debeSerEntero)
        {
            int n;
            decimal d;
            if (txt.TextLength != 0)
            {
                if (debeSerEntero)
                {
                    if (!int.TryParse(txt.Text, out n))
                    {
                        MessageBox.Show("El campo " + campo + " debe ser un número entero", "Error en los tipos de entrada", MessageBoxButtons.OK);
                        return false;
                    }
                }
                else
                {
                    if (!decimal.TryParse(txt.Text, out d))
                    {
                        MessageBox.Show("El campo " + campo + " debe ser un número", "Error en los tipos de entrada", MessageBoxButtons.OK);
                        return false;
                    }
                }

            }
            return true;
        }

        private void cargarComboServicio()
        {
            cboServicio.Items.Clear();

            SqlDataReader reader;
            SqlCommand consultaServicios = new SqlCommand();
            consultaServicios.CommandType = CommandType.Text;
            consultaServicios.CommandText = "SELECT SERV_DESC FROM [ABSTRACCIONX4].SERVICIOS";
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
    }
}
