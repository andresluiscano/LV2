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
    public partial class frmEstado : Form
    {
        public frmEstado()
        {
            InitializeComponent();
                        
        }
        public MySqlConnection conexion = SQL_Methods.IniciarConnection();
        public MySqlDataAdapter adapter;
        public MySqlDataAdapter adapter2;
        DataSet set = new DataSet("PEDIDOS");
        
        private void frmEstado_Load(object sender, EventArgs e)
        {
            List<Pedido> listaPedidos = new List<Pedido>();

            dgvEstado.Refresh();
            
            //Este es el codigo para llenar el Data Grid con el Data set usando un Adapter con un Builder
            adapter = new MySqlDataAdapter("SELECT PED_ID AS ID, PED_NOMBRE AS NOMBRE , PED_MAIL AS MAIL, PED_TOTAL AS TOTAL, PED_SENIA AS SENIA, PED_DEBE AS DEBE, PED_ESTADO AS ESTADO, PED_FEC AS FECHA, PED_PAGADO AS PAGADO, PED_DESC AS DESCRIPCION FROM PEDIDOS WHERE PED_PAGADO = FALSE", conexion);
            MySqlCommandBuilder builder = new MySqlCommandBuilder(adapter);
            //conexion.Open();
            dgvEstado.Refresh();
            dgvEstado.DataSource = "";
            adapter.Fill(set, "PEDIDOS");
            dgvEstado.DataSource = set;
            dgvEstado.DataMember = "PEDIDOS";
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
        DataTable changes = set.Tables["PEDIDOS"].GetChanges();
        
        if (changes == null)
        {
            MessageBox.Show("No ha habido cambios");
        }
        else
        {
            adapter.Update(changes);
            set.Tables["PEDIDOS"].AcceptChanges();

            MessageBox.Show("Actualizado");
            set.Clear();
            dgvEstado.ResetBindings();
            adapter.Fill(set, "PEDIDOS");
            dgvEstado.DataSource = set;
            dgvEstado.DataMember = "PEDIDOS";
        }
               
        
        }

        private void dgvEstado_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
