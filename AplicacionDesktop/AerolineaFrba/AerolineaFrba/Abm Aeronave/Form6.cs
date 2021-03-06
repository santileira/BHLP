﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AerolineaFrba.Abm_Aeronave
{
    public partial class Form6 : Form
    {
        Baja baja;
        Boolean llamadoDesdeBajaLogica;
        Nullable<DateTime> fechaAlta;
        Nullable<DateTime> fechaBajaDefinitiva;
        public Form6(Baja unabBaja, Boolean llamadoDesdeBajaLogica, Nullable<DateTime> fechaAlta, Nullable<DateTime> fechaBajaDefinitiva)
        {
            baja = unabBaja;
            this.llamadoDesdeBajaLogica = llamadoDesdeBajaLogica;
            this.fechaAlta = fechaAlta;
            this.fechaBajaDefinitiva = fechaBajaDefinitiva;
            InitializeComponent();
        }

        // Muestra la fecha de reinicio si se llamó por fuera de servicio
        private void Form6_Load(object sender, EventArgs e)
        {
            fechaBaja.Value = Program.fechaHoy();
            fechaReinicio.Value = Program.fechaHoy();
            if (llamadoDesdeBajaLogica)
            {
                fechaReinicio.Visible = false;
                label1.Visible = false;

            }

        }

        private void botonCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Realiza las validaciones correspondientes e intenta dar de baja en la BD
        private void botonAceptar_Click(object sender, EventArgs e)
        {
            if (!validarFecha())
            {
                if (!llamadoDesdeBajaLogica)
                {
                    DialogResult resultado = MessageBox.Show("¿Está seguro que quiere dejar fuera de servicio esta aeronave?", "Advertencia", MessageBoxButtons.YesNo);
                    if (apretoSi(resultado))
                    {
                        baja.dejarFueraDeServicio(fechaReinicio.Value, fechaBaja.Value);
                        this.Close();
                    }
                }
                else
                {
                    DialogResult resultado = MessageBox.Show("¿Está seguro que quiere dar de baja lógica esta aeronave?", "Advertencia", MessageBoxButtons.YesNo);
                    if (apretoSi(resultado))
                    {
                        baja.darDeBajaLogica(fechaBaja.Value);
                        this.Close();
                    }
                }
            }
        }

        private Boolean apretoSi(DialogResult resultado)
        {
            return resultado == System.Windows.Forms.DialogResult.Yes;
        }

        private bool validarFecha()
        {
            Boolean huboError = false;
            if (fechaReinicio.Value.CompareTo(Program.fechaHoy()) <= 0 && !llamadoDesdeBajaLogica)
            {
                MessageBox.Show("La fecha reinicio debe ser posterior a la fecha de hoy", "Error en los datos de entrada", MessageBoxButtons.OK);
                huboError = true;
            }

            if (fechaBaja.Value.CompareTo(Program.fechaHoy()) <= 0)
            {
                MessageBox.Show("La fecha de baja debe ser posterior a la fecha de hoy", "Error en los datos de entrada", MessageBoxButtons.OK);
                huboError = true;
            }

            if (fechaBaja.Value.CompareTo(fechaReinicio.Value) >= 0 && !llamadoDesdeBajaLogica)
            {
                MessageBox.Show("La fecha de baja debe ser anterior a la fecha de reinicio", "Error en los datos de entrada", MessageBoxButtons.OK);
                return true;
            }

            if (fechaAlta != null)
            {
                if (fechaBaja.Value.CompareTo(fechaAlta) <= 0)
                {
                    MessageBox.Show("La fecha de baja debe ser posterior a la fecha de alta de la aeronave", "Error en los datos de entrada", MessageBoxButtons.OK);
                    return true;
                }
            }

            if(fechaBajaDefinitiva != null && !llamadoDesdeBajaLogica)
            {
                if (fechaReinicio.Value.CompareTo(fechaBajaDefinitiva) >= 0)
                {
                    MessageBox.Show("La fecha de reinicio debe ser anterior a la fecha de baja definitiva", "Error en los datos de entrada", MessageBoxButtons.OK);
                    return true;
                }
            }

            return huboError;
        }



       
    }
}
