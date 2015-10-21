using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AerolineaFrba
{
    static class Validacion
    {
        public static Boolean textNombre(TextBox txtBox , string nombreCampo)
        {
            Boolean huboErrores = false;

            if (Validacion.esVacio(txtBox , nombreCampo , true))
            {
                //MessageBox.Show("El nombre no puede estar en blanco", "Error en el nombre", MessageBoxButtons.OK);
                huboErrores = true;
            }
            else
            {//esta hecho asi porque si es vacio no tiene sentido que entre para validar que sea texto ya que no hay nada
                if (!Validacion.esTexto(txtBox))
                {
                    MessageBox.Show("El campo " + nombreCampo + " debe ser una cadena de caracteres", "Error en el nombre", MessageBoxButtons.OK);
                    huboErrores = true;
                }
            }
            
            return !huboErrores;
        }

        public static Boolean filtrosContengaEIgualdad(TextBox filtro1, TextBox filtro2)
        {
            Boolean huboErrores = false;

            if (!esTexto(filtro1) && !esVacio(filtro1  , "No importa" , false))
            {
                MessageBox.Show("El filtro que contenga la palabra debe ser una cadena de caracteres", "Error en el nombre", MessageBoxButtons.OK);
                huboErrores = true;
            }

            if (!esTexto(filtro2) && !esVacio(filtro2 , "No importa" , false))
            {
                MessageBox.Show("El filtro por igualdad de palabra debe ser una cadena de caracteres", "Error en el nombre", MessageBoxButtons.OK);
                huboErrores = true;
            }

            return !huboErrores;
        }
        
        public static Boolean esNumero(TextBox txtBox , string nombreCampo = "Opcional" , Boolean mostrarMensaje = false)
        {
            int numero;
            if (int.TryParse(txtBox.Text, out numero))
            {
                return true;
            }
            else
            {
                if (mostrarMensaje)
                {
                    MessageBox.Show("Para el campo " + nombreCampo + " el criterio debe ser numerico", "Error en el tipo de dato del criterio", MessageBoxButtons.OK);
                }
                return false;
            }
            
        }

        public static Boolean esDecimal(TextBox txtBox)
        {
            decimal unDecimal;
            return decimal.TryParse(txtBox.Text, out unDecimal);
        }

        public static Boolean esTexto(TextBox txtBox , string nombreCampo = "Opcional" , Boolean mostrarMensaje = false)
        {
            String textPattern = "[A-Za-z]";
            System.Text.RegularExpressions.Regex regexTexto = new System.Text.RegularExpressions.Regex(textPattern);
            if (regexTexto.IsMatch(txtBox.Text))
            {
                return true;
            }
            else
            {
                if(mostrarMensaje)
                    MessageBox.Show("Para el campo " + nombreCampo + " el criterio debe ser texto", "Error en el nombre", MessageBoxButtons.OK);
                return false;
            }
            //return regexTexto.IsMatch(txtBox.Text);
        }

        public static Boolean esVacio(TextBox txtBox ,string nombreCampo = "Pelotudo no queres mostrar el mensaje" , bool mostrarMensaje = false)
        {
            Boolean vacio = false;
            if (txtBox.TextLength == 0)
            {
                vacio = true;
                if (mostrarMensaje)
                {
                    MessageBox.Show("El campo " + nombreCampo + " no puede estar vacio", "Error en los datos de entrada", MessageBoxButtons.OK);
                }
            }

            return vacio;
        }

        public static Boolean esVacio(ComboBox cboBox, string nombreCampo = "Pelotudo no queres mostrar el mensaje", bool mostrarMensaje = false)
        {
            Boolean vacio = false;
            if (cboBox.Text.Length == 0)
            {
                vacio = true;
                if (mostrarMensaje)
                {
                    MessageBox.Show("El campo " + nombreCampo + " no puede estar vacio", "Error en los datos de entrada", MessageBoxButtons.OK);
                }
            }

            return vacio;
        }

        public static Boolean estaSeleccionado(ComboBox combo , Boolean mostrarMensaje = false)
        {
            
            if (combo.SelectedIndex == -1)
            {
                if (mostrarMensaje)
                {
                    MessageBox.Show("Debe seleccionar un campo en el desplegable de opciones", "Error en el campo", MessageBoxButtons.OK);
                }
                return false;
            }
            return true;
        } 

        public static Boolean estaCheckeadoCheck(CheckBox check)
        {
            return check.Checked;
        }


        internal static bool estaCheckeadoOpt(RadioButton option)
        {
            return option.Checked;
        }

        public static bool igualdadCiudades(TextBox txtCiudadDestino, TextBox txtCiudadOrigen)
        {
            if (txtCiudadDestino.TextLength * txtCiudadOrigen.TextLength != 0)
            {
                if (txtCiudadOrigen.Text == txtCiudadDestino.Text)
                {
                    MessageBox.Show("La ciudad de origen debe ser distinta a la de destino", "Error en los datos ingresados", MessageBoxButtons.OK);
                    return true;
                }
            }
            return false;
        }

        public static Boolean numeroCorrecto(TextBox txtBox, string campo , bool debeSerDecimal)
        {
            if (!esVacio(txtBox))
            {
                if ((debeSerDecimal && !esDecimal(txtBox)) || (!debeSerDecimal && !esNumero(txtBox)))
                {

                    MessageBox.Show("El campo " + campo + " debe ser un número", "Error en los datos ingresados", MessageBoxButtons.OK);
                    return false;
                }
            }
            return true;
        }
    }
}
