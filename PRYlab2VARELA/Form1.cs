using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PRYlab2VARELA
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            clsEmpleados Empleado = new clsEmpleados();
            Empleado.ConectarBase();
        }

        private void empleadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListadoEmpleados fEmpleados = new frmListadoEmpleados();
            fEmpleados.ShowDialog();
        }
    }
}
