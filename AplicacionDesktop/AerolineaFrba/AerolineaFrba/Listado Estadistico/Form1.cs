using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AerolineaFrba.Listado_Estadistico
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.inicio();
        }

        private void inicio()
        {
            cboSemestre.SelectedIndex = 0;
            cboEstadistica.SelectedIndex = 0;
            dg.Visible = false;
            
            DataGridViewCellStyle columnHeaderStyle = new DataGridViewCellStyle();
            columnHeaderStyle.BackColor = Color.Beige;
            columnHeaderStyle.Font = new Font("Calibri", 12, FontStyle.Bold);
            dg.ColumnHeadersDefaultCellStyle = columnHeaderStyle;

        }

        private string semestreSeleccionado()
        {
            if (cboSemestre.SelectedIndex == 0)
                return "1";
            else
                return "2";
        }

        private string estadisticaSeleccionada()
        {
            if (cboEstadistica.SelectedIndex == 0)
                return "[ABSTRACCIONX4].destinosConMasPasajesVendidos";
            if (cboEstadistica.SelectedIndex == 1)
                return "[ABSTRACCIONX4].destinosConAeronaveMasVacia";
            if (cboEstadistica.SelectedIndex == 2)
                return "[ABSTRACCIONX4].clientesConMasMillas";
            if (cboEstadistica.SelectedIndex == 3)
                return "[ABSTRACCIONX4].destinosConMasPasajesCancelados";
            else
                return "[ABSTRACCIONX4].aeronavesConMayorFueraDeServicio";
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Boolean huboError = !Validacion.numeroCorrecto(txtAnio, "Año", false);

            if (!huboError)
            {
                SQLManager.ejecutarQuery("select * from " + this.estadisticaSeleccionada() + "(" + this.semestreSeleccionado() + ", '" + txtAnio.Text + "')", dg);
                dg.Visible = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.inicio();
        }
    }
}
