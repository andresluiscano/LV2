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
    public partial class frmFicha : Form
    {
        public frmFicha()
        {
            InitializeComponent();
            btnAceptar.Enabled =false;
            btnLimpiar.Enabled = false;
            btnBuscar.Enabled = true;
            btnModificar.Enabled = false;
            this.txtNombre.Select();
                        
        }
        public List<Cliente> listaClientes = new List<Cliente>();
        public string Desde, Hasta= "";
        int ID=0;

        private void button1_Click(object sender, EventArgs e)
        {
            this.buscar();
        }
         
        
        private void cmdAceptar_Click(object sender, EventArgs e)
        {

            //List<Cliente> colCliSelec = new List<Cliente>();

            //int x= this.dgvBuscar.CurrentRow.Cells.Count;
            //for (int i = 0; i < x; i++)
            //{
            //    colCliSelec.Add(this.dgvBuscar.CurrentRow.Cells[i].Value.ToString());
            //}
            //colCliSelec = listaClientes[ID];
            frmDeuda deuda = new frmDeuda(listaClientes,ID);
            deuda.Show();
          
                        
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
            dgvDeudas.DataSource = "";
            Desde = "";
            Hasta = "";
            ID=0;
        }

        private void frmBuscarClientes_Load(object sender, EventArgs e)
        {
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            
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
            MySqlCommand consulta = new MySqlCommand(String.Format(
           "SELECT cli_id AS ID, cli_nombre AS NOMBRE, cli_apellido AS APELLIDO FROM clientes  where cli_nombre ='{0}' or cli_apellido='{1}'", txtNombre.Text, txtApellido.Text), conexion);
                      
            MySqlDataReader ejecuta = consulta.ExecuteReader();
            
            while (ejecuta.Read())
            {
                Cliente pCliente = new Cliente();
                pCliente.ID = ejecuta.GetInt32(0);
                pCliente.NOMBRE = ejecuta.GetString(1);
                pCliente.APELLIDO = ejecuta.GetString(2);
                
                listaClientes.Add(pCliente);
            }            

            if (listaClientes.Count == 0)
            {
                    DialogResult result = MessageBox.Show("Lo sentimos, el cliente no se encuentra registrado. ¿Desea Registrarlo?", "ERROR!", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        frmAltaClientes altaCliente = new frmAltaClientes();
                        altaCliente.Show();
                        btnAceptar.Enabled = true;
                        btnModificar.Enabled = true;
                        btnLimpiar.Enabled = true;
                        btnBuscar.Enabled = false;
                        txtApellido.Enabled = false;
                        txtNombre.Enabled = false;
                        listaClientes.Clear();
                    }
                    else
                    {
                        btnBuscar.Enabled = true;
                        btnLimpiar.Enabled = false;
                        txtApellido.Enabled = true;
                        txtNombre.Enabled = true;
                        txtNombre.Text = "";
                        txtApellido.Text = "";
                        dgvBuscar.DataSource = "";
                        dgvDeudas.DataSource = "";
                        listaClientes.Clear();
                        this.txtNombre.Select();
                   }
            }
            else
            {

                btnLimpiar.Enabled = true;
                btnBuscar.Enabled = false;
                txtApellido.Enabled = true;
                txtNombre.Enabled = true;
                dgvBuscar.DataSource = listaClientes;
                btnLimpiar.Enabled = true;
                //listaClientes.Clear();
                ejecuta.Close();
                conexion.Close();
                this.Refresh();
            }
        }

        private void dgvBuscar_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvDeudas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
           frmAltaClientes altaCliente = new frmAltaClientes();
           altaCliente.Show();
        }

        private void dgvBuscar_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
            MySqlConnection conexion2 = SQL_Methods.IniciarConnection();
            MySqlCommand consulta2 = new MySqlCommand(String.Format(
                "SELECT (SELECT mat_nombre MATERIAL FROM material WHERE mat_id = D.deu_id_material) AS MATERIAL, D.deu_monto AS MONTO, D.deu_fec AS FECHA FROM deudas D  join clientes C on D.deu_id_cli = C.cli_id where deu_id_cli = '" + listaClientes[e.RowIndex].ID.ToString() + "' and D.deu_pagado=0 order by deu_fec"), conexion2);
            MySqlDataReader ejecuta2 = consulta2.ExecuteReader();

            List<Deudas> deudas = new List<Deudas>();
             while (ejecuta2.Read())
             {
                    
                 Deudas pDeudas = new Deudas();
                 pDeudas.MATERIAL = ejecuta2.GetString(0);
                 pDeudas.MONTO = ejecuta2.GetDecimal(1);
                 pDeudas.FECHA = ejecuta2.GetDateTime(2);

                 deudas.Add(pDeudas);
              }
             
             dgvDeudas.DataSource = deudas;
             conexion2.Close();
             ID = e.RowIndex;
             txtTotal.Text = "";
             txtNombre.Text = listaClientes[ID].NOMBRE;
             txtApellido.Text = listaClientes[ID].APELLIDO;
             btnAceptar.Enabled = true;
             btnModificar.Enabled = true;
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            MySqlConnection conexion = SQL_Methods.IniciarConnection();
            MySqlCommand consulta = new MySqlCommand(String.Format(
                "Update deudas D set D.deu_pagado = 1 where D.deu_fec >= '" + Desde + "' AND D.deu_fec <= '" + Hasta + "' AND D.deu_pagado = 0;"), conexion);
            MySqlDataReader ejecuta = consulta.ExecuteReader();
            conexion.Close();
            
            DialogResult result = MessageBox.Show("El Señor/a " + txtNombre.Text + ", " +txtApellido.Text + " ha efectuado un Pago", "Salir", MessageBoxButtons.OK);

        }

        private void button2_Click_1(object sender, EventArgs e)
        {

            if (Desde == "" && Hasta == "")
            {
                DialogResult result = MessageBox.Show("Debe seleccionar primero un rango de fechas válidos.");
            }
            else
            {
                MySqlConnection conexion = SQL_Methods.IniciarConnection();
                MySqlCommand consulta = new MySqlCommand(String.Format(
                    "select CASE WHEN (sum(deu_monto) IS NULL) THEN 'O.00' ELSE sum(deu_monto) END AS TOTAL from deudas D where D.deu_fec >= '" + Desde + "' AND D.deu_fec <= '" + Hasta + "' AND D.deu_id_cli ='" + listaClientes[ID].ID.ToString() + "' AND D.deu_pagado=0"), conexion);
                MySqlDataReader ejecuta = consulta.ExecuteReader();
                while (ejecuta.Read())
                {
                    txtTotal.Text = ejecuta.GetString(0);
                }

                conexion.Close();
            }
        }
        private void dtpDesde_ValueChanged(object sender, EventArgs e)
        {
            Desde = this.dtpDesde.Value.Date.Year.ToString() + '-' + this.dtpDesde.Value.Date.Month.ToString() + '-' + this.dtpDesde.Value.Date.Day.ToString();
            Hasta = this.dtpHasta.Value.Date.Year.ToString() + '-' + this.dtpHasta.Value.Date.Month.ToString() + '-' + this.dtpHasta.Value.Date.Day.ToString();
        } 
    }
}

