using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LIBRECEA
{
    public partial class frmModificarClientes : Form
    {
        public frmFicha buscarClientes = new frmFicha();
        public frmModificarClientes(List<string> cliente, frmFicha formBuscar)
        {
            InitializeComponent();
            txtNombre.Text = cliente[1].ToString();
            txtApellido.Text = cliente[2].ToString();
        }

        private void frmModificarClientes_Load(object sender, EventArgs e)
        {

        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {

        }
    }
}
