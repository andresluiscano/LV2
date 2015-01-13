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
    public partial class frmBuscarClientes : Form
    {
        public frmBuscarClientes()
        {
            InitializeComponent();
            btnAceptar.Enabled =false;
            btnLimpiar.Enabled = false;
            btnBuscar.Enabled = true;
            btnModificar.Enabled = false;
            this.txtNombre.Select();
            
        }
        public List<Cliente> listaClientes = new List<Cliente>();


        private void button1_Click(object sender, EventArgs e)
        {
            this.buscar();
        
        }
         
        
        private void cmdAceptar_Click(object sender, EventArgs e)
        {
            //string celda;
            //celda = this.dgvBuscar.CurrentCell.Value.ToString();
            //celda = this.dgvBuscar.CurrentRow.Cells[0].Value.ToString();
            //this.textBox1.Text = celda;

            var colCliSelec = new List<string>();

            int x= this.dgvBuscar.CurrentRow.Cells.Count;
            for (int i = 0; i < x; i++)
            {
                colCliSelec.Add(this.dgvBuscar.CurrentRow.Cells[i].Value.ToString());
            }

            frmDeuda deuda = new frmDeuda(colCliSelec);
            btnAceptar.Enabled=false;
            deuda.Show();
            
            //this.Close();
           
            
                        
        }

        private void button2_Click(object sender, EventArgs e)
        {
            btnAceptar.Enabled = false;
            btnModificar.Enabled = false;
            btnLimpiar.Enabled = false;
            btnBuscar.Enabled = true;
            txtApellido.Enabled = true;
            txtNombre.Enabled = true;
            txtNombre.Text = "";
            txtApellido.Text = "";
            dgvBuscar.DataSource = "";
            listaClientes.Clear();
            this.txtNombre.Select();
        }

        private void frmBuscarClientes_Load(object sender, EventArgs e)
        {
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            var colCliSelec = new List<string>();

            int x = this.dgvBuscar.CurrentRow.Cells.Count;
            for (int i = 0; i < x; i++)
            {
                colCliSelec.Add(this.dgvBuscar.CurrentRow.Cells[i].Value.ToString());
            }

            frmModificarClientes modificar = new frmModificarClientes(colCliSelec, this);
            btnAceptar.Enabled = false;
            modificar.Show();


        }

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                this.buscar();
            }
        }

        private void buscar()
        {
            MySqlConnection conexion = SQL_Methods.IniciarConnection();
            MySqlConnection conexion2 = SQL_Methods.IniciarConnection();
            dgvBuscar.Refresh();

            MySqlCommand consulta = new MySqlCommand(String.Format(
           "SELECT cli_id AS ID, cli_nombre AS NOMBRE, cli_apellido AS APELLIDO FROM clientes  where cli_nombre ='{0}' or cli_apellido='{1}'", txtNombre.Text, txtApellido.Text), conexion);
            //MySqlCommand consulta = new MySqlCommand("SELECT cli_id,cli_nombre,cli_apellido FROM clientes WHERE cli_apellido='{}'" , conexion);
            
            
            MySqlDataReader ejecuta = consulta.ExecuteReader();


            while (ejecuta.Read())
            {
                Cliente pCliente = new Cliente();
                pCliente.ID = ejecuta.GetInt32(0);
                pCliente.NOMBRE = ejecuta.GetString(1);
                pCliente.APELLIDO = ejecuta.GetString(2);


                listaClientes.Add(pCliente);

            }
            dgvBuscar.DataSource = listaClientes;

                           
                MySqlCommand consulta2 = new MySqlCommand(String.Format("SELECT (SELECT mat_nombre MATERIAL FROM material WHERE mat_id = D.deu_id_material) AS MATERIAL, D.deu_monto AS MONTO, D.deu_fec AS FECHA FROM deudas D WHERE deu_id_cli = (SELECT cli_id ID FROM clientes WHERE cli_nombre ='"+txtNombre.Text+ "' OR cli_apellido = '"+txtApellido.Text+"')"), conexion2);
                MySqlDataReader ejecuta2 = consulta2.ExecuteReader();

                List<Deudas> deudas = new List<Deudas>();
                while (ejecuta2.Read())
                {
                    
                    Deudas pDeudas = new Deudas();
                    pDeudas.id_material = ejecuta2.GetString(0);
                    pDeudas.monto = ejecuta2.GetDouble(1);
                    pDeudas.fec = ejecuta2.GetDateTime(2);

                    deudas.Add(pDeudas);
                 }
                dgvDeudas.DataSource = deudas;
           
            ejecuta.Close();
            conexion.Close();
            conexion2.Close();
            btnAceptar.Enabled = true;
            btnLimpiar.Enabled = true;
            btnBuscar.Enabled = false;
            txtApellido.Enabled = false;
            txtNombre.Enabled = false;

            if (listaClientes.Count == 0)
            {
                DialogResult result = MessageBox.Show("Lo sentimos, el cliente no se encuentra registrado. ¿Desea Registrarlo?", "ERROR!", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    frmAltaClientes altaCliente = new frmAltaClientes();
                    altaCliente.Show();
                    conexion.Close();
                }
               /* else if (result == DialogResult.No)
                {
                    this.Close();

                }*/
            }

        }

        private void dgvBuscar_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvDeudas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        } 
    }
}

