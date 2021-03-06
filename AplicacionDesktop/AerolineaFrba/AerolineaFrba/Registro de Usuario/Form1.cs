﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AerolineaFrba.Registro_de_Usuario
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void botonRegistrar_Click(object sender, EventArgs e)
        {
            if (datosCorrectos())
            {
                registrarUsuario();
            }
            else
            {
                
            }
        }

        // Realiza el llamado al sp para registrar al usuario efectivamente, si es que no hubo errores
        // encripta la contraseña antes de enviarla
        private void registrarUsuario()
        {
            SQLManager manager = new SQLManager().generarSP("RegistrarUsuario")
                                                 .agregarStringSP("@Usuario", txtUsuario)
                                                 .agregarStringSP("@Contrasenia", Encriptador.encriptarSegunSHA256(txtPassword.Text));

            try
            {
                manager.ejecutarSP();
                MessageBox.Show("El usuario " + txtUsuario.Text + " ha sido registrado de forma correcta", "Registro exitoso", MessageBoxButtons.OK);
                this.Close();
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message, "Error en el registro", MessageBoxButtons.OK);
            }
        }

        private Boolean datosCorrectos()
        {
            Boolean huboError = false;
            
            huboError = !Validacion.esTextoAlfanumerico(txtUsuario,true,"usuario",true)|| huboError;
            huboError = !validarLongitudContrasenias() || huboError;
            huboError = !validarContraseniasIguales() || huboError;

            return !huboError;
        }
        private bool validarLongitudContrasenias()
        {
            if (txtUsuario.TextLength < 4  || txtPassword.TextLength < 4  || txtPassword2.TextLength < 4)
            {
                MessageBox.Show("El usuario y/o la contraseña debe tener al menos 4 caracteres", "Error en los datos de entrada", MessageBoxButtons.OK);
                return false;
            }
            return true;
        }

        private Boolean validarContraseniasIguales()
        {
            if (!txtPassword.Text.Equals(txtPassword2.Text))
            {
                MessageBox.Show("La contraseña no coincide", "Error en los datos de entrada", MessageBoxButtons.OK);
                return false;
            }
            return true;
        }

        private void txtUsuario_TextChanged(object sender, EventArgs e)
        {

        }

    }
}
