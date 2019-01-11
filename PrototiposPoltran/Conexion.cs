using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace PrototiposPoltran
{
    static class Conexion
    {
        static String conexionString = "Data Source=localhost;Initial Catalog=BD_GestionPapeletas;Integrated Security=True";
        public static SqlConnection sqlConexion;
        public static void Open()
        {
            try {
                sqlConexion = new SqlConnection(conexionString);
                sqlConexion.Open();
            }
            catch(SqlException e)
            {
                MessageBox.Show("No se conecto la base datos "+ e.ToString());
            }            
        }

        public static void Close()
        {
            try
            {
                sqlConexion.Close();
            }
            catch(SqlException e)
            {
                MessageBox.Show("No se cerro la conexion a la base de datos " + e.ToString());
            }
        }

        

    }
}
