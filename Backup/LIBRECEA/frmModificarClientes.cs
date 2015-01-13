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
        public frmBuscarClientes buscarClientes = new frmBuscarClientes();
        public frmModificarClientes(List<string> cliente, frmBuscarClientes formBuscar)
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
