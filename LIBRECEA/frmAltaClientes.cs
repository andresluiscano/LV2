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
    public partial class frmAltaClientes : Form
    {
        public frmAltaClientes()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MySqlConnection conexion = SQL_Methods.IniciarConnection();
            MySqlCommand consulta = new MySqlCommand("SELECT * FROM clientes WHERE cli_nombre='" + txtNombre.Text + "' AND cli_apellido ='" +txtApellido.Text +"'" , conexion);
            MySqlDataReader ejecuta = consulta.ExecuteReader();

                if (ejecuta.HasRows == true || algunCampoVacio())
                {
                    MessageBox.Show("El cliente ingresado ya existe o le faltaron ingresar datos. Ingrese correctamente los datos.");
                    txtNombre.Text = "";
                    txtApellido.Text = "";
                    this.txtNombre.Select();
                }
                else
                {
                    ejecuta.Close();
                    MySqlCommand comando = new MySqlCommand("insertarCliente", conexion);
                    
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.Add("nombre", MySqlDbType.VarChar, 30).Value = txtNombre.Text;
                    comando.Parameters.Add("apellido", MySqlDbType.VarChar,30).Value = txtApellido.Text;
                    MySqlDataReader dr = comando.ExecuteReader();
                    MessageBox.Show("El cliente se ingresó correctamente.");
                    txtNombre.Text = "";
                    txtApellido.Text = "";
                    this.txtNombre.Select();
                }
            conexion.Close();
            }
            
            
        

        private bool algunCampoVacio()
        {
            bool condicion = false;
            if (this.txtNombre.Text.Trim() == "")
            {
                condicion = true;
            }
            if (this.txtApellido.Text.Trim() == "")
            {
                condicion = true;
            }
            return condicion;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmAltaClientes_Load(object sender, EventArgs e)
        {

        }

    }
}
