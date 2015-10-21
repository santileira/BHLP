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
        public static Boolean textNombre(string contenido)
        {
            Boolean huboErrores = false;

            if (Validacion.esVacio(contenido))
            {
                MessageBox.Show("El nombre no puede estar en blanco", "Error en el nombre", MessageBoxButtons.OK);
                huboErrores = true;
            }
            else
            {//esta hecho asi porque si es vacio no tiene sentido que entre para validar que sea texto ya que no hay nada
                if (!Validacion.esTexto(contenido))
                {
                    MessageBox.Show("El nombre debe ser una cadena de caracteres", "Error en el nombre", MessageBoxButtons.OK);
                    huboErrores = true;
                }
            }
            
            return !huboErrores;
        }

        public static Boolean filtrosContengaEIgualdad(string contenidoFiltro1, string igualdadFiltro2)
        {
            Boolean huboErrores = false;

            if (!esTexto(contenidoFiltro1) && !esVacio(contenidoFiltro1))
            {
                MessageBox.Show("El filtro que contenga la palabra debe ser una cadena de caracteres", "Error en el nombre", MessageBoxButtons.OK);
                huboErrores = true;
            }

            if (!esTexto(igualdadFiltro2) && !esVacio(igualdadFiltro2))
            {
                MessageBox.Show("El filtro por igualdad de palabra debe ser una cadena de caracteres", "Error en el nombre", MessageBoxButtons.OK);
                huboErrores = true;
            }

            return !huboErrores;
        }
        
        public static Boolean esNumero(string numero)
        {
            String numericPattern = "[0-9]";
            System.Text.RegularExpressions.Regex regexNumero = new System.Text.RegularExpressions.Regex(numericPattern);

            return regexNumero.IsMatch(numero);
        }

        public static Boolean esTexto(string texto)
        {
            String textPattern = "[A-Za-z]";
            System.Text.RegularExpressions.Regex regexTexto = new System.Text.RegularExpressions.Regex(textPattern);

            return regexTexto.IsMatch(texto);
        }

        public static Boolean esVacio(string texto)
        {
            return texto.Length == 0;
        }

        public static Boolean estaSeleccionado(ComboBox combo)
        {
            return combo.SelectedIndex != -1;
        }

        public static Boolean estaCheckeadoCheck(CheckBox check)
        {
            return check.Checked;
        }


        internal static bool estaCheckeadoOpt(RadioButton option)
        {
            return option.Checked;
        }
    }
}
