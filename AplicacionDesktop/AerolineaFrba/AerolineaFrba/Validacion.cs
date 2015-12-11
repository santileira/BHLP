using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AerolineaFrba
{
    // Clase que contiene todas las validaciones generales que se hacen en la aplicación
    static class Validacion
    {
        
        public static Boolean textNombre(TextBox txtBox , string nombreCampo)
        {
            Boolean huboErrores = false;

            if (Validacion.esVacio(txtBox , nombreCampo , true))
            {
                huboErrores = true;
            }
            else
            {//esta hecho asi porque si es vacio no tiene sentido que entre para validar que sea texto ya que no hay nada
                if(!Validacion.esTexto(txtBox))
                {
                    MessageBox.Show("El campo " + nombreCampo + " debe ser una cadena de caracteres", "Error en el nombre", MessageBoxButtons.OK);
                    huboErrores = true;
                }
            }
            
            return !huboErrores;
        }

        // Validación para campos de los filtros
        public static Boolean filtrosContengaEIgualdad(TextBox filtro1, TextBox filtro2)
        {
            Boolean huboErrores = false;

            if (!esSoloTexto(filtro1) && !esVacio(filtro1  , "No importa" , false))
            {
                MessageBox.Show("El filtro que contenga la palabra debe ser una cadena de caracteres", "Error en el nombre", MessageBoxButtons.OK);
                huboErrores = true;
            }

            if (!esSoloTexto(filtro2) && !esVacio(filtro2 , "No importa" , false))
            {
                MessageBox.Show("El filtro por igualdad de palabra debe ser una cadena de caracteres", "Error en el nombre", MessageBoxButtons.OK);
                huboErrores = true;
            }

            return !huboErrores;
        }
        

        //****** VALIDACIONES NUMERICAS *******//

        // Usado para validar números enteros
        public static Boolean esNumero(TextBox txtBox , string nombreCampo = "Opcional" , Boolean mostrarMensaje = false)
        {
            long numero;
            string cadena = txtBox.Text;

            if (cadena == "")
                return true;

            if (long.TryParse(cadena, out numero))
            {
                if (!cadena.Contains(" "))
                {
                    return true;
                }
            }
            if (mostrarMensaje)
            {
                MessageBox.Show("El valor del campo " + nombreCampo + " debe ser un número entero", "Error en los datos de entrada", MessageBoxButtons.OK);
            }
            return false;       
        }

        // Usado para validar números decimales
        public static Boolean esDecimal(TextBox txtBox, string nombreCampo = "Opcional", Boolean mostrarMensaje = false)
        {
            string cadena = txtBox.Text;
            decimal numero;

            if (cadena == "")
                return true;

            if(!comaYPuntoCorrectos(cadena))
            {
                if (mostrarMensaje)
                {
                    MessageBox.Show("El valor del campo " + nombreCampo + " debe ser un número", "Error en los datos de entrada", MessageBoxButtons.OK);
                }
                return false;
            }

            cadena.Replace('.', ',');

            if (Decimal.TryParse(cadena, out numero))
            {
                if (!cadena.Contains(" "))
                {
                    return true;
                }
            }
            if (mostrarMensaje)
            {
                MessageBox.Show("El valor del campo " + nombreCampo + " debe ser un número", "Error en los datos de entrada", MessageBoxButtons.OK);
            }
            return false;
        }

        private static bool comaYPuntoCorrectos(string cadena)
        {
            if (cantidadEnCadena(cadena, '.') > 1)
                return false;

            if (cantidadEnCadena(cadena, ',') > 1)
                return false;
            
            if (cadena.Contains('.') && cadena.Contains(','))
                return false;
            
            return true;
        }


        //********** VALIDACIONES DE TEXTO ******//

        // El texto contiene al menos una letra
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
                if (mostrarMensaje && !esVacio(txtBox))
                {
                    MessageBox.Show("Para el campo " + nombreCampo + " el criterio debe ser alfanumerico", "Error en el nombre", MessageBoxButtons.OK);
                    return false;
                }
                if(!esVacio(txtBox))
                return false;
            }
            return true;
        }

        // Valida que todos los caracteres sean o letras o espacios
        public static Boolean esSoloTexto(TextBox txtBox, string nombreCampo = "Opcional", Boolean mostrarMensaje = false)
        {
            string cadena = txtBox.Text;

            if (cadena == "")
                return true;

            if (cadena.All((car)=>Char.IsLetter(car) || Char.IsWhiteSpace(car)))
            {
                if (Char.IsWhiteSpace(cadena[0]))
                {
                    MessageBox.Show("El campo " + nombreCampo + " debe comenzar con una letra.", "Error en los datos de entrada", MessageBoxButtons.OK);
                    return false;
                }
                /*for (int i = 0; i < cadena.Length - 1; i++)
                {
                    if (Char.IsWhiteSpace(cadena[i]) && cadena[i]==cadena[i+1])
                    {
                        MessageBox.Show("El campo " + nombreCampo + " no puede contener varios espacios consecutivos", "Error en los datos de entrada", MessageBoxButtons.OK);
                        return false;
                    }
                }*/
                return true;
            }
            else
            {
                if (mostrarMensaje)
                {
                    MessageBox.Show("El campo " + nombreCampo + " debe contener solo letras o espacios.", "Error en los datos de entrada", MessageBoxButtons.OK);
                    return false;
                }
                return false;
            }
          
        }

        public static Boolean esTextoAlfanumerico(TextBox txtBox, Boolean primeroLetra,string nombreCampo = "Opcional", Boolean mostrarMensaje = false)
        {
            string cadena = txtBox.Text;
            Boolean huboErrores = false;

            if (cadena == "")
                return true;

            if(primeroLetra)
            {
                if (huboErrores = !Char.IsLetter(cadena[0]))
                {
                    MessageBox.Show("El campo " + nombreCampo + " debe comenzar con una letra.", "Error en los datos de entrada", MessageBoxButtons.OK);
                }
            }
                
            if (!cadena.All((car) => Char.IsLetterOrDigit(car)))
            {
                if (mostrarMensaje)
                {
                    MessageBox.Show("El campo " + nombreCampo + " debe contener solo letras o números.", "Error en los datos de entrada", MessageBoxButtons.OK);
                    
                }
                huboErrores = true;
            }

            if (!cadena.Any((car) => Char.IsLetter(car)))
            {
                if (mostrarMensaje)
                {
                    MessageBox.Show("El campo " + nombreCampo + " debe contener al menos una letra", "Error en los datos de entrada", MessageBoxButtons.OK);

                }
                huboErrores = true;
            }

            return !huboErrores;
        }

        // Utilizado para validar matrícula de aeronave
        public static Boolean esMatricula(MaskedTextBox txtBox, Boolean mostrarMensaje = false)
        {
            string cadena = txtBox.Text;
            if (cantidadEnCadena(cadena, '-') > 1)
            {
                MessageBox.Show("La matrícula debe estar compuesta por 3 letras seguida de 3 dígitos", "Error en los datos de entrada", MessageBoxButtons.OK);
                return false;
            }

            if (!(cadena.Take(3).All((car) => Char.IsLetter(car)) && (cadena.Substring(4).All((car) => Char.IsDigit(car) && !Char.IsWhiteSpace(car))) && cadena.Substring(4).Length==3))
            {
                if (mostrarMensaje)
                {
                    MessageBox.Show("La matrícula debe estar compuesta por 3 letras seguida de 3 dígitos", "Error en los datos de entrada", MessageBoxButtons.OK);
                }
                return false;
            }
            return true;
        }

        //******* VALIDACIONES DE CAMPOS VACIOS ******//

        public static Boolean esVacio(TextBox txtBox, string nombreCampo = "Opcional", bool mostrarMensaje = false)
        {
            Boolean vacio = false;
            if (txtBox.TextLength == 0)
            {
                vacio = true;
                if (mostrarMensaje)
                {
                    MessageBox.Show("El campo " + nombreCampo + " no puede estar vacío", "Error en los datos de entrada", MessageBoxButtons.OK);
                }
            }

            return vacio;
        }

        public static Boolean esVacio(ListBox txtBox, string nombreCampo = "Opcional", bool mostrarMensaje = false)
        {
            Boolean vacio = false;
            if (txtBox.Text == "")
            {
                vacio = true;
                if (mostrarMensaje)
                {
                    MessageBox.Show("El campo " + nombreCampo + " no puede estar vacío", "Error en los datos de entrada", MessageBoxButtons.OK);
                }
            }

            return vacio;
        }

        public static Boolean esVacio(ComboBox cboBox, string nombreCampo = "Opcional", bool mostrarMensaje = false)
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

        public static Boolean listaVacia(List<Object> lista, string nombreCampo = "Opcional" , Boolean mostrarMensaje = false)
        {
            Boolean vacio = false;
            if (lista.Count == 0)
            {
                vacio = true;
                if (mostrarMensaje)
                {
                    MessageBox.Show("Se debe agregar por lo menos un tipo de servicio", "Error en los datos de entrada", MessageBoxButtons.OK);
                }
            }
            return vacio;
        }

        public static Boolean estaSeleccionado(ComboBox combo , Boolean mostrarMensaje = false , String opcional = "opciones")
        {
            
            if (combo.SelectedIndex == -1)
            {
                if (mostrarMensaje)
                {
                    MessageBox.Show("Debe seleccionar un campo en el desplegable de " + opcional, "Error en el campo", MessageBoxButtons.OK);
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
            else
            {
                //MessageBox.Show("El campo " + campo + " debe ser un número", "Error en los datos ingresados", MessageBoxButtons.OK);
                return false;
            }

            return true;
        }

        public static Boolean fechaPosteriorALaDeHoy(DateTimePicker dateTimePicker1)
        {
            if (dateTimePicker1.Value.CompareTo(Program.fechaHoy()) < 0)
            {
                MessageBox.Show("La fecha ingresada debe ser posterior a la fecha de hoy", "Error en los datos de entrada", MessageBoxButtons.OK);
                return false;
            }
            return true;
        }


        //Eventos de validacion de tipos de campos

        public static void controlIngresoNumeroDecimal(object sender, KeyPressEventArgs e)
        {
            string cadena = ((TextBox)sender).Text;
            char caracter = e.KeyChar;

            if (caracter == (char)(Keys.Back))
                return;

            if (caracter.ToString() == ",")
            {
                if (cantidadEnCadena(cadena, ',') != 0)
                {
                    e.Handled = true;
                }
                return;
            }

            if (!Char.IsDigit(caracter))
            {
                e.Handled = true;
            }
        }

        private static int cantidadEnCadena(string cadena, char caracter)
        {
            return cadena.Count((car) => car == caracter);
        }

        public static void controlIngresoNumeroEntero(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Back))
                return;

            if (!Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        //******* VALIDACION DE LIMITES NUMERICOS********//

        public static Boolean estaEntreLimites(TextBox txt, decimal limiteInferior, decimal limiteSuperior,Boolean numeroDecimal,string nombreCampo)
        {
            string cadena = txt.Text;

            if (cadena == "")
                return true;

            if (numeroDecimal)
            {
                if (!esDecimal(txt))
                    return true;
            }
            else
            {
                if (!esNumero(txt))
                    return true;
            }


            decimal numero = Convert.ToDecimal(txt.Text.Replace(".",","));

            if (numero < limiteInferior || numero > limiteSuperior)
            {
                MessageBox.Show("El valor del campo " + nombreCampo + " debe estar entre " + limiteInferior.ToString() + " y " + limiteSuperior.ToString(), "Error en los datos de entrada", MessageBoxButtons.OK);
                return false;
            }

            return true;
        }

    }
}
