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
    public partial class Form7 : Form
    {
        private string mensaje;
        private string matricula;
        private DateTime fechaBaja;
        private DateTime fechaReinicio;
        private Boolean llamadoDesdeBajaLogica;

        public Form7(string unMensaje,string unaMatricula,Boolean desdeBajaLogica,DateTime unaFechaBaja,DateTime unaFechaReinicio )
        {
            mensaje = unMensaje;
            matricula = unaMatricula;
            fechaBaja = unaFechaBaja;
            fechaReinicio = unaFechaReinicio;
            llamadoDesdeBajaLogica = desdeBajaLogica;
            InitializeComponent();
        }

        private void Form7_Load(object sender, EventArgs e)
        {
            txtMensaje.Text = mensaje;
        }

        private void botonCancelar_Click(object sender, EventArgs e)
        {
            SQLManager manager = new SQLManager().generarSP("CancelarPasajesEncomiendasAeronave")
                                                 .agregarStringSP("@Matricula", matricula)
                                                 .agregarFechaSP("@FechaBaja", fechaBaja);

            if(llamadoDesdeBajaLogica){
                manager = manager.agregarFechaNula("@FechaReinicio");
            }
            else{
                manager = manager.agregarFechaSP("@FechaReinicio",fechaReinicio);
            }

            manager.ejecutarSP();

            this.Close();
        }

        private void botonSuplantar_Click(object sender, EventArgs e)
        {
            SQLManager sqlManager = new SQLManager().generarSP("SuplantarAeronave")
                                                    .agregarStringSP("@Matricula", matricula)
                                                    .agregarFechaSP("@FechaBaja", fechaBaja);
            if (llamadoDesdeBajaLogica)
                sqlManager.agregarFechaNula("@FechaReinicio").ejecutarSP();
            else
                sqlManager.agregarFechaSP("@FechaReinicio", fechaReinicio).ejecutarSP();

            this.Close();
        }

    }
}