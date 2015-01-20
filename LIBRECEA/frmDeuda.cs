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
        public List<Cliente>clienteSeleccionado;
        public int x=0;
        public frmDeuda(List<Cliente> cliente, int N)
        {
            InitializeComponent();
            x = N;
            clienteSeleccionado = cliente;
            lblDatos.Text = cliente[x].NOMBRE.ToString().ToUpper() + ", "+ cliente[x].APELLIDO.ToString().ToUpper();
            
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
                MySqlConnection conexion3 = SQL_Methods.IniciarConnection();

                string id_deuda, id_material, fec= "";
                decimal monto = 0;
                id_deuda = (clienteSeleccionado[0].ID.ToString());
                id_material = listaMateriales[cboMateriales.SelectedIndex].Id.ToString();
                fec = DateTime.Today.Year.ToString() + '-' + DateTime.Today.Month.ToString() + '-' + DateTime.Today.Day.ToString();
                monto = (Convert.ToDecimal(listaMateriales[cboMateriales.SelectedIndex].precio)*Convert.ToInt16(txtCantidad.Text.ToString()));

                MySqlCommand consulta2 = new MySqlCommand(String.Format("CALL insertarDeuda(" + id_deuda + "," + id_material + "," + monto.ToString().Replace(',', '.') + ",'" + fec + "')"), conexion3);

                MySqlDataReader ejecuta2 = consulta2.ExecuteReader();

                conexion3.Close();
               
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
