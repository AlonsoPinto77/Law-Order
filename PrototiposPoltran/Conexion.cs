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
    class Conexion
    {
        String conexionString = "Data Source=localhost;Initial Catalog=SoftPape;Integrated Security=True";
        DataTable tabla;
        SqlConnection sqlConexion;
        SqlDataAdapter sqlDataAdapter;
        SqlCommand cmd;
        SqlDataReader sqlDataReader;

        public Conexion()
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

        public void Close()
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
