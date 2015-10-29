using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AerolineaFrba
{
    class Constructores
    {
        public static void mostrar(Form formulario, Form padre)
        {
            formulario.MdiParent = padre;
            //formulario.Visible = true;
            formulario.Show();
        }

        public static void ABMAeronaveAlta(Form padre)
        {
            Form alta = new Abm_Aeronave.Alta(false, null, "", DateTime.Today);
            mostrar(alta, padre);
        }

        public static void ABMAeronaveBaja(Form padre)
        {
            Form baja = new Abm_Aeronave.Baja();
            mostrar(baja, padre);
        }

        public static void ABMAeronaveModifiaccion(Form padre)
        {
            Form modificacion = new Abm_Aeronave.Modificacion();
            Abm_Aeronave.Listado listado = new Abm_Aeronave.Listado();

            listado.loActivoModificar = true;

            (modificacion as Abm_Aeronave.Modificacion).listado = listado;
            listado.anterior = null;
            listado.siguiente = modificacion;
            listado.llamadoDesdeModificacion = false;
            listado.llamadoDesdeModificacionSeleccionar = true;

            
            mostrar(modificacion, padre);
        }
        
    }
}
