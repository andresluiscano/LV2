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
    public partial class frmDeuda : Form
    {
        public List<Materiales> listaMaterialesPorCliente = new List<Materiales>();
        public List<Materiales> listaMateriales = new List<Materiales>();
        public List<string> clienteSeleccionado;
        public frmDeuda(List<string> cliente)
        {
            InitializeComponent();
            clienteSeleccionado = cliente;
            lblDatos.Text = cliente[2].ToString().ToUpper() + ", " + cliente[1].ToString().ToUpper();
            
            //List<Materiales> listaMateriales = new List<Materiales>();
            MySqlConnection conexion = SQL_Methods.IniciarConnection();
            
            MySqlCommand consulta = new MySqlCommand("SELECT * FROM material ",conexion);
            MySqlDataReader ejecuta = consulta.ExecuteReader();

            txtFecha.Text = DateTime.Today.ToShortDateString();
            while (ejecuta.Read())
            {
                Materiales pMateriales = new Materiales();
                pMateriales.Id = ejecuta.GetInt32(0);
                pMateriales.nombre = ejecuta.GetString(1);
                pMateriales.precio = ejecuta.GetDouble(2);


                listaMateriales.Add(pMateriales);
                cboMateriales.Items.Add(pMateriales.nombre.ToString());
            }

            
            conexion.Close();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.agregar();
                              
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtPrecio.Text = listaMateriales[cboMateriales.SelectedIndex].precio.ToString();
        }

        private void frmDeuda_Load(object sender, EventArgs e)
        {
            
        }

        private void txtPrecio_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.txtPrecio.Text = "";
            this.txtCantidad.Text = "";
            this.cboMateriales.Text = "";
            this.agregar();
            
        }

        public void agregar()
        {
            if (this.txtCantidad.Text != "" && this.cboMateriales.Text != "")
            {
                MySqlConnection conexion = SQL_Methods.IniciarConnection();
                MySqlCommand comando = new MySqlCommand("insertarDeuda", conexion);

                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("id_deuda", MySqlDbType.Int16, 10).Value = Convert.ToInt32(clienteSeleccionado[0].ToString());
                comando.Parameters.Add("id_material", MySqlDbType.Int16, 5).Value = Convert.ToInt32(listaMateriales[cboMateriales.SelectedIndex].Id.ToString());
                comando.Parameters.Add("monto", MySqlDbType.Decimal, 10).Value = Convert.ToDouble(Convert.ToDecimal(listaMateriales[cboMateriales.SelectedIndex].precio) * Convert.ToInt16(txtCantidad.Text.ToString()));
                comando.Parameters.Add("fec", MySqlDbType.Date, 10).Value = DateTime.Today;

                MySqlDataReader dr = comando.ExecuteReader();

                conexion.Close();

                DialogResult result = MessageBox.Show("Deuda agendada, desea seguir agregando deudas para " + lblDatos.Text, "Salir", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    this.cboMateriales.Text = "";
                    this.txtPrecio.Text = "";
                    this.txtCantidad.Text = "";
                }
                else
                {
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Algun/os campos estan vacios");
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtFecha_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
