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
    class DBAccess
    {
        SqlConnection conn;
        public DBAccess()
        {
            Conexion.Open();
        }
        public DataTable GetPapeletasIngresadasw(string oficio, string year)
        {
            
            DataTable dt = new DataTable();
            try
            {
                conn = Conexion.sqlConexion;
                if (conn.State.ToString() == "Closed")
                {
                    conn.Open();
                }
                string codString = string.Empty;
                codString = " Select * from papeleta where numero_oficio='" + oficio + "' AND estado!='A' AND estado!='E'";
                SqlCommand cmd = new SqlCommand(codString, conn);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
                Conexion.Close();
                return dt;

            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e.Message);
                return dt;
            }
        }
        public DataTable GetExportarInfo(string oficio, string year)
        {
            DataTable dt = new DataTable();
            try
            {
                conn = Conexion.sqlConexion;
                if (conn.State.ToString() == "Closed")
                {
                    conn.Open();
                }
                string codString = string.Empty;
                codString = " Select * from papeleta where numero_oficio='" + oficio + "' ";
                SqlCommand cmd = new SqlCommand(codString, conn);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
                Conexion.Close();
                return dt;

            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e.Message);
                return dt;
            }
        }

        public DataTable GetConductors(string mes, string year)
        {
            DataTable dt = new DataTable();
            try
            {
                conn = Conexion.sqlConexion;
                if (conn.State.ToString() == "Closed")
                {
                    conn.Open();
                }
                string codString = string.Empty;
                codString = " Select * from papeleta where fecha_envio='" + mes + "' ";
                SqlCommand cmd = new SqlCommand(codString, conn);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
                Conexion.Close();
                return dt;

            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e.Message);
                return dt;
            }
        }

    }
}
