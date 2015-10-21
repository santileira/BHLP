using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AerolineaFrba
{
    class SQLManager
    {
        public SqlCommand command;

        public SQLManager generarSP(string nombre)
        {
            command = new SqlCommand();
            command.Connection = Program.conexion();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "[GD2C2015].[ABSTRACCIONX4].[" + nombre + "]";
            command.CommandTimeout = 0;
            return this;
        }

        public SQLManager agregarIntSP(string nombreVariable, TextBox txt)
        {
            command.Parameters.AddWithValue(nombreVariable ,Convert.ToInt32(txt.Text));
            return this;
        }

        public SQLManager agregarIntSP(string nombreVariable, Int32 numero)
        {
            command.Parameters.AddWithValue(nombreVariable, numero);
            return this;
        }

        public SQLManager agregarStringSP(string nombreVariable , TextBox txt)
        {
            command.Parameters.AddWithValue(nombreVariable, Convert.ToInt32(txt.Text));
            return this;
        }

        public SQLManager agregarStringSP(string nombreVariable, ComboBox cmb)
        {
            command.Parameters.AddWithValue(nombreVariable, Convert.ToInt32(cmb.Text));
            return this;
        }

        public SQLManager agregarStringSP(string nombreVariable, string nombre)
        {
            command.Parameters.AddWithValue(nombreVariable, nombre);
            return this;
        }



        public Object ejecutarSP()
        {
            return command.ExecuteScalar();
        }

        public SQLManager agregarDecimalSP(string nombreVariable, decimal numero)
        {
            command.Parameters.AddWithValue(nombreVariable, numero);
            return this;
        }
    }
}
