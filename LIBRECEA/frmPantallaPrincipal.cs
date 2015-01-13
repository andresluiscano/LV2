using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;

namespace LIBRECEA
{
    public partial class frmPantallaPrincipal : Form
    {
        public frmPantallaPrincipal()
        {
            InitializeComponent();
        }
        //SETEO EL PATH, PUBLICO
        //string path = "C:\\Documents and Settings\\Administrador\\Mis documentos\\Descargas";
        string path = "C:\\Documents and Settings\\Administrador\\Escritorio\\LIBRECEA 2.0";

        private void sToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
           
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void nuevoToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            frmAltaClientes altaCliente = new frmAltaClientes();
            altaCliente.Show();
        }

        private void buscarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmBuscarClientes buscarCliente = new frmBuscarClientes();
            buscarCliente.Show();
        }

        private void frmPantallaPrincipal_Load(object sender, EventArgs e)
        {
            buscarSecciones();
        }

        private void nuevoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmNuevoPedido nuevoPedido = new frmNuevoPedido();
            nuevoPedido.Show();
        }

        private void estadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmEstado estado = new frmEstado();
            estado.Show();
        }



        private void button2_Click(object sender, EventArgs e)
        {
         
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*
            axAcroPDF1.Visible = false;
            if (listBox1.SelectedItem != null)
            {
                string ruta = path + "\\" + comboBox1.Text + "\\" + comboBox2.Text+ "\\" +listBox1.SelectedItem.ToString() + ".pdf";
                
              //  axAcroPDF1.LoadFile(ruta);
                try
                {
                    //axAcroPDF1.LoadFile(listBox1.SelectedItem.ToString());
                    axAcroPDF1.LoadFile(ruta);
                                        
                }
                catch (ArgumentException ex)
                {
                    MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                //axAcroPDF1.Visible = true;    

                char[] delimiterChars = { '_' };
                string text = listBox1.SelectedItem.ToString();
                string[] words = text.Split(delimiterChars);

                if (words.Count() == 6)
                {
                    textBox4.Text = words[0];
                    textBox5.Text = words[1];
                    textBox6.Text = words[2];
                    textBox1.Text = words[3];
                    textBox2.Text = words[4];
                    if (textBox1.Text == "SF")
                    {
                        textBox3.Text = (precioHoja(words[3]) * (GetNoOfPagesPDF(ruta))).ToString();
                        //textBox3.Text = words[5];
                    }
                    else 
                    {
                        textBox3.Text = (precioHoja(words[3]) * (GetNoOfPagesPDF(ruta))/2).ToString();
                    }
                }

                
                
                
                
               

            }
            axAcroPDF1.Visible = true;
            */
        }

        private void btnAdelante_Click(object sender, EventArgs e)
        {

                

        }

        


        /*
        //ESTO ES PARA PONER QUE LA IMAGEN SE AGRANDE AUTOMATICAMENTE
        protected override void OnPaint(PaintEventArgs e)
        {
            // Get image compiled as an embedded resource.
            Assembly asm = Assembly.GetExecutingAssembly();
            //Bitmap bmp = new Bitmap(@"C:\imagen.gif");
            Bitmap backgroundImage = new Bitmap(LIBRECEA.Properties.Resources.f_pantalla_principal);

            e.Graphics.DrawImage(backgroundImage, this.ClientRectangle,
                new Rectangle(0, 0, backgroundImage.Width, backgroundImage.Height),
                GraphicsUnit.Pixel);
        }*/

        private void buscarSecciones()
        {
            MySqlConnection conexion = SQL_Methods.IniciarConnection();
            MySqlCommand consulta = new MySqlCommand(String.Format("SELECT * FROM seccion"), conexion);
            MySqlDataReader ejecuta = consulta.ExecuteReader();


            while (ejecuta.Read())
            {
                Seccion pSeccion = new Seccion();
                pSeccion.id = ejecuta.GetInt16(0);
                pSeccion.tipo = ejecuta.GetString(1);
                
               comboBox1.Items.Add(pSeccion.tipo.ToUpper());
            }
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            buscarProfesores(this.comboBox1.SelectedIndex);
        }

        private void buscarProfesores(int seccion)
        {
            comboBox2.Items.Clear();
            comboBox2.Text = "";
            comboBox1.Text = "";
            listBox1.Items.Clear();
            listBox1.ClearSelected();
            //axAcroPDF1.Visible = false;
            
            seccion++;

            MySqlConnection conexion = SQL_Methods.IniciarConnection();
            MySqlCommand consulta = new MySqlCommand(String.Format("SELECT * FROM profesores where prof_id_sec=" + seccion + " ORDER BY prof_ape"), conexion);
            MySqlDataReader ejecuta = consulta.ExecuteReader();


            while (ejecuta.Read())
            {
                Profesores pProfesores = new Profesores();
                pProfesores.id = ejecuta.GetInt16(0);
                pProfesores.nombre = ejecuta.GetString(1);
                pProfesores.apellido = ejecuta.GetString(2);
                pProfesores.sec = ejecuta.GetInt16(3);

                comboBox2.Items.Add(pProfesores.nombre.ToUpper() + " " + pProfesores.apellido.ToUpper());
            }

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            string path2 = path + "\\" + comboBox1.Text +"\\" + comboBox2.Text;
            
            string[] archivos = Directory.GetFiles(path2);
            //string[] archivos = Directory.GetFiles(path);
            for (int i = 0; i < archivos.Length; i++)
            {

                string nueva = archivos[i].Replace(path2, "");
                nueva = nueva.TrimStart(new Char[] { '\\', '*', '.' });

                
                listBox1.Items.Add(nueva.Replace(".pdf", ""));
                //listBox1.Items.Add(archivos[i]);
            }
            
        }

        public static double GetNoOfPagesPDF(string FileName)
        {

            int result = 0;
            FileStream fs = new FileStream(FileName, FileMode.Open, FileAccess.Read);
            StreamReader r = new StreamReader(fs);
            string pdfText = r.ReadToEnd();

            System.Text.RegularExpressions.Regex regx = new Regex(@"/Type\s*/Page[^s]");
            System.Text.RegularExpressions.MatchCollection matches = regx.Matches(pdfText);
            result = matches.Count;
            return result;
        }

        public static double precioHoja(string hoja)
        {
            MySqlConnection conexion = SQL_Methods.IniciarConnection();

            MySqlCommand consulta = new MySqlCommand("SELECT * FROM material WHERE mat_nombre ='"+hoja+"'", conexion);
            MySqlDataReader ejecuta = consulta.ExecuteReader();

            while (ejecuta.Read())
            {
                Materiales pMateriales = new Materiales();
                pMateriales.Id = ejecuta.GetInt32(0);
                pMateriales.nombre = ejecuta.GetString(1);
                pMateriales.precio = ejecuta.GetDouble(2);

            }
            return ejecuta.GetDouble(2);
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void elegirPathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialogoRuta = new FolderBrowserDialog();

            if (dialogoRuta.ShowDialog() == DialogResult.OK)
            {
                txtPath.Text = dialogoRuta.SelectedPath;
            }

            MySqlConnection conexion2 = SQL_Methods.IniciarConnection();
            //MySqlCommand consulta = new MySqlCommand("SELECT * FROM path WHERE path_nom='" + dialogoRuta.SelectedPath + "'", conexion2);
            //MySqlDataReader ejecuta = consulta.ExecuteReader();

            //ejecuta.Close();
            MySqlCommand comando = new MySqlCommand("insertarPath", conexion2);

            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Add("path", MySqlDbType.VarChar, 100).Value = dialogoRuta.SelectedPath;
            MySqlDataReader dr = comando.ExecuteReader();
            MessageBox.Show("Path Aceptado.");
            conexion2.Close();
            
            }
         

        /*OpenFileDialog open = new OpenFileDialog();
            open.Title = "Open";
            open.Filter = "All Files|*.*";
            try
            {
                if (open.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    axAcroPDF1.LoadFile(open.FileName);
                }
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            axAcroPDF1.Visible = true;
            textBox6.Text = open.FileName;*/
        
    }
}
