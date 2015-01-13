using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
//using System.Data.SqlClient;
using MySql.Data.MySqlClient;

using System.Windows.Forms;

namespace LIBRECEA
{
    class SQL_Methods // Clase Abstracta. Nunca se instanciará
    {
        #region Variables de Clase
        public static bool DBConnectStatus = false;
        #endregion

        static public MySqlConnection IniciarConnection()
        {
            MySqlConnection myConnection;

            //string ConectionQuery = "user id=" + "gd" + ";" +
            //                        "password=" + "gd2013" + ";" +
            //                        "server=" + "localhost" + "\\" + "SQLSERVER2008" + ";" +
            //                        "Trusted_Connection=no;" +
            //                        "database=" + "TP-ROCK" + ";" +
            //                        "connection timeout=5";
            string ConectionQuery = "Server=192.168.0.110;User id=andres; Database=librecea; Password=andres";

            myConnection = new MySqlConnection(ConectionQuery);
            try
            {
                myConnection.Open();
                DBConnectStatus = true;
                return myConnection;
            }
            catch (Exception e)
            {
                DBConnectStatus = false;
                MessageBox.Show(ConectionQuery);
                MessageBox.Show(e.ToString());
                return null;
            }
        }

    /*    static public DataTable EjecutarProcedure(SqlConnection myConnection, string myQuery)
        {
            MySqlDataAdapter miDataAdapter;
            DataSet miDataSet;
            DataTable unaDataTable = new DataTable();

            miDataAdapter = new MySqlDataAdapter(myQuery, myConnection);
            miDataSet = new DataSet();
            miDataAdapter.Fill(miDataSet, "Tabla");
            unaDataTable = miDataSet.Tables["Tabla"];
            miDataSet.Dispose();
            miDataAdapter.Dispose();

            return unaDataTable;
        }*/
    }
}
