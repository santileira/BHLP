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
        
        //****** AERONAVE ********//
        public static Form ABMAeronaveAlta()
        {
            return new Abm_Aeronave.Alta(false, null, "", Program.fechaHoy());
        }

        public static Form ABMAeronaveBaja()
        {
            return  new Abm_Aeronave.Baja();
        }

        public static Form ABMAeronaveModifiaccion()
        {
            Form modificacion = new Abm_Aeronave.Modificacion();
            Abm_Aeronave.Listado listado = new Abm_Aeronave.Listado();

            listado.loActivoModificar = true;

            (modificacion as Abm_Aeronave.Modificacion).listado = listado;
            listado.anterior = null;
            listado.siguiente = modificacion;
            listado.llamadoDesdeModificacion = false;
            listado.llamadoDesdeModificacionSeleccionar = true;


            return modificacion;
        }


        //****** ROL ********//

        public static Form ABMRolAlta()
        {
            Abm_Rol.Alta alta = new Abm_Rol.Alta();
            Abm_Rol.Listado listado = new Abm_Rol.Listado();

            alta.listado = listado;
            listado.anterior = alta;

            return alta;
        }

        public static Form ABMRolBaja()
        {
            Abm_Rol.Baja baja = new Abm_Rol.Baja();
            Abm_Rol.Listado listado = new Abm_Rol.Listado();

            baja.listado = listado;
            listado.anterior = baja;

            return baja;
        }

        public static Form ABMRolModificacion()
        {
            Abm_Rol.Modificacion modificacion = new Abm_Rol.Modificacion();
            Abm_Rol.Listado listado = new Abm_Rol.Listado();

            modificacion.listado = listado;
            listado.siguiente = modificacion;

            return modificacion;
        }


        //****** RUTA ********//

        public static Form ABMRutaAlta()
        {
            Abm_Ruta.Alta alta = new Abm_Ruta.Alta();
            Abm_Ruta.Listado listado = new Abm_Ruta.Listado();

            alta.listado = listado;
            listado.alta = alta;

            return alta;
        }

        public static Form ABMRutaBaja()
        {
            Abm_Ruta.Baja baja = new Abm_Ruta.Baja();
            Abm_Ruta.Listado listado = new Abm_Ruta.Listado();

            baja.listado = listado;
            listado.anterior = baja;

            return baja;
        }

        public static Form ABMRutaModificacion()
        {
            Abm_Ruta.Modificacion modificacion = new Abm_Ruta.Modificacion();
            Abm_Ruta.Listado listado = new Abm_Ruta.Listado();

            modificacion.listado = listado;
            listado.siguiente = modificacion;

            return modificacion;
        }


        //****** COMPRA / DEVOLUCION ********//

        public static Form Compra()
        {
            Compra.Form1 compra = new Compra.Form1();
            Compra.Form3 ingresarCantidades = new Compra.Form3();
            Compra.Form4 cargaDeDatos = new Compra.Form4();

            Compra.Form2 butacas = new Compra.Form2();
            Compra.Form5 servicioDeEncomiendas = new Compra.Form5();

            Compra.Form6 efectuaCompra = new Compra.Form6();

            compra.formularioSiguiente = ingresarCantidades;
            ingresarCantidades.anterior = compra;

            ingresarCantidades.formularioSiguiente = cargaDeDatos;
            cargaDeDatos.anterior = ingresarCantidades;

            cargaDeDatos.butacas = butacas;
            butacas.anterior = cargaDeDatos;
            cargaDeDatos.servicioDeEncomiendas = servicioDeEncomiendas;
            servicioDeEncomiendas.anterior = cargaDeDatos;

            cargaDeDatos.efectuaCompra = efectuaCompra;
            efectuaCompra.anterior = cargaDeDatos;


            cargaDeDatos.crearColumnas();

            return compra;
        }

        public static Form Devolucion()
        {
            Devolucion.dgEncomiendas devolucion = new Devolucion.dgEncomiendas();
            devolucion.inicio();

            return devolucion;
        }


        //****** MILLAS ********//

        public static Form ConsultaMillas()
        {
            Consulta_Millas.Form1 consultaMillas = new Consulta_Millas.Form1();
            Canje_Millas.Form1 canjeMillas = new Canje_Millas.Form1();

            consultaMillas.canjeMillas = canjeMillas;
            canjeMillas.anterior = consultaMillas;

            return consultaMillas;
        }


        //****** VIAJES ********//

        public static Form GeneracionViaje()
        {
            Generacion_Viaje.Form1 generadorViajes = new Generacion_Viaje.Form1();
            Abm_Aeronave.Listado listadoAeronaves = new Abm_Aeronave.Listado();
            Abm_Ruta.Listado listadoRutas = new Abm_Ruta.Listado();

            generadorViajes.listadoAeronaves = listadoAeronaves;
            generadorViajes.listadoRutas = listadoRutas;

            listadoAeronaves.anterior = generadorViajes;
            listadoAeronaves.loActivoGenerarViajes = true;

            listadoRutas.anterior = generadorViajes;
            listadoRutas.loActivoGenerarViajes = true;

            return generadorViajes;
        }

        public static Form RegistroLlegadaDestino()
        {
            return new Registro_Llegada_Destino.Form1();
        }


        //****** REGISTROS ********//

        public static Form RegistroUsuario()
        {
            return new Registro_de_Usuario.Form1();
        }


        //****** ESTADÍSTICAS ********//

        public static Form ListadoEstadístico()
        {
            return new Listado_Estadistico.Form1();
        }
        

        
    }
}
