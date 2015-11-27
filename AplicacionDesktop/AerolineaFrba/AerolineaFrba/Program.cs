using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace AerolineaFrba
{
    static class Program
    {
        public static string rolActual;
       

        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Abre la conexion con sql
             //public static SqlConnection laConexion; new SqlConnection("Data Source=localhost\\SQLSERVER2012;Initial Catalog=GD2C2015;Persist Security Info=True;User ID=gd;Password=gd2015");


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //CREO LA TABLA CON LA FECHA DEL XML
            SQLManager manager = new SQLManager();
            manager.generarSP("crearTablaFecha");
            manager.agregarFechaSP("@fecha", Program.fechaHoy());
            manager.ejecutarSP();

            Application.Run(new FormLogin());
        }

        public static SqlConnection conexion()
        {
            string configuracion = ConfigurationManager.AppSettings["configuracionSQL"].ToString();
            SqlConnection laConexion = new SqlConnection(configuracion);
            laConexion.Open();
            return laConexion;
        }

        public static DateTime fechaHoy()
        {
            return DateTime.ParseExact(ConfigurationManager.AppSettings["FechaSistema"], "yyyy-dd-MM HH:mm:ss",
                                       System.Globalization.CultureInfo.InvariantCulture);
        }


    }
}
