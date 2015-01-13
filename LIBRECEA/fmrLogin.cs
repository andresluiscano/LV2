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
    public partial class fmrLogin : Form
    {
        public fmrLogin()
        {
            InitializeComponent();
            this.txtUser.Select();
           
        }

        private void Login_Load(object sender, EventArgs e)
        {
           
        }


        private void txtPass_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                this.entrar();
            }
        }

        public void entrar()
        {
            MySqlConnection conexionAlternativa = SQL_Methods.IniciarConnection();
            Form frmMenu = new frmMenu();
            //miConexion.Open();
            //SqlCommand consulta= new SqlCommand("SELECT * FROM Usuario WHERE nombre='"+txtUser.Text+"' and password='"+txtPass.Text+"'", miConexion);
            MySqlCommand consulta = new MySqlCommand("SELECT * FROM usuarios WHERE usr_usuario='" + txtUser.Text + "' and usr_password='" + txtPass.Text + "'", conexionAlternativa);
            MySqlDataReader ejecuta = consulta.ExecuteReader();

            if (ejecuta.Read() == true)
            {
                MessageBox.Show("Bienvenido " + txtUser.Text.ToUpper() + "!", "Usuario Correcto");
                conexionAlternativa.Close();
                frmMenu.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Por favor, ingrese correctamente los datos", "Usuario Incorrecto");
                txtUser.Text = "";
                txtPass.Text = "";
                txtUser.Focus();
                this.txtUser.Select();

            }
            //miConexion.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            entrar();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void txtPass_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                this.entrar();
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtUser_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPass_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }


    }
}
