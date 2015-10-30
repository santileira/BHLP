using System;
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
        }

        private void registrarUsuario()
        {
            SQLManager manager = new SQLManager().generarSP("RegistrarUsuario")
                                                 .agregarStringSP("@Usuario", txtUsuario)
                                                 .agregarStringSP("@Contrasenia", encriptarSegunSHA256(txtPassword.Text));

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
            return Validacion.textNombre(txtUsuario, "usuario") && validarLongitudContrasenias() && validarContraseniasIguales();
        }

        private bool validarLongitudContrasenias()
        {
            if (txtUsuario.TextLength == 0 || txtPassword.TextLength == 0 || txtPassword2.TextLength == 0)
            {
                MessageBox.Show("Debe ccompletar todos los campos", "Error en los datos de entrada", MessageBoxButtons.OK);
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

        private string encriptarSegunSHA256(string cadena)
        {
            SHA256CryptoServiceProvider provider = new SHA256CryptoServiceProvider();

            byte[] bytesEntrada = Encoding.UTF8.GetBytes(cadena);
            byte[] bytesEncriptados = provider.ComputeHash(bytesEntrada);

            StringBuilder cadenaEncriptada = new StringBuilder();

            for(int i = 0; i < bytesEncriptados.Length; i++)
            {
                cadenaEncriptada.Append(bytesEncriptados[i].ToString("x2").ToLower());
            }


            return cadenaEncriptada.ToString();
        }  

    }
}
