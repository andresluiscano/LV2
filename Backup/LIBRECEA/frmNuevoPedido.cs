using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace LIBRECEA
{
    public partial class frmNuevoPedido : Form
    {
        public frmNuevoPedido()
        {
            InitializeComponent();
        }

        private void frmPedidos_Load(object sender, EventArgs e)
        {
            this.txtNombre.Select();
            this.txtFecha.Enabled = false;
            this.txtFecha.Text = Convert.ToString(DateTime.Today);
        }

        
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (algunCampoVacio())
            {
                MySqlConnection conexion = SQL_Methods.IniciarConnection();
                MySqlCommand comando = new MySqlCommand("insertarPedido", conexion);

                decimal debe = Convert.ToDecimal(this.txtTotal.Text) - Convert.ToDecimal(this.txtSeña.Text);

                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("nom_ped", MySqlDbType.VarChar, 30).Value = txtNombre.Text;
                comando.Parameters.Add("total_ped", MySqlDbType.Decimal, 10).Value = Convert.ToDouble(Convert.ToDecimal(this.txtTotal.Text));
                comando.Parameters.Add("senia_ped", MySqlDbType.Decimal, 10).Value = Convert.ToDouble(Convert.ToDecimal(this.txtSeña.Text));
                comando.Parameters.Add("debe_ped", MySqlDbType.Decimal, 10).Value = debe;
                comando.Parameters.Add("estado_ped", MySqlDbType.Enum, 1).Value = false;
                comando.Parameters.Add("fec_ped", MySqlDbType.Date, 10).Value = DateTime.Today;
                comando.Parameters.Add("pagado_ped", MySqlDbType.Enum, 1).Value = false;
                comando.Parameters.Add("desc_ped", MySqlDbType.VarChar, 200).Value = txtDesc.Text;
                comando.Parameters.Add("mail_ped", MySqlDbType.VarChar, 200).Value = txtMail.Text;

                MySqlDataReader dr = comando.ExecuteReader();
                MessageBox.Show("Pedido Ingresado. " + this.txtNombre.Text.ToUpper() + " debe $ " + debe + ".", "Validación");
                txtNombre.Text = "";
                txtDesc.Text = "";
                txtSeña.Text = "";
                txtTotal.Text = "";
                txtMail.Text = "";
                this.txtNombre.Select();

                conexion.Close();
            }
            else
            {
                MessageBox.Show("Faltan colocar datos!", "¡CUIDADO!");
            }
        }

        private bool algunCampoVacio()
        {
            bool condicion = false;
            if (this.txtNombre.Text.Trim() != "" && this.txtTotal.Text.Trim() != "" && this.txtSeña.Text.Trim() != "" && this.txtDesc.Text.Trim() != "")
            {
                condicion = true;
            }

            return condicion;
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtTotal_TextChanged(object sender, EventArgs e)
        {

        }


        private void txtTotal_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                MessageBox.Show("Solo se permiten numeros", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void txtSeña_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                MessageBox.Show("Solo se permiten numeros", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }
    }
}
