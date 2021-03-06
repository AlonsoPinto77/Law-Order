﻿using System;
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
        //static String conexionString = "Data Source=PAPELETAS04;Initial Catalog=BD_GestionPapeletas;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        //static String conexionString = "Data Source=localhost;Initial Catalog=BD_GestionPapeletas;Integrated Security=True";
        static String conexionString = "Data Source=DESKTOP-PCEUCOL\\SQLEXPRESS;Initial Catalog=BD_GestionPapeletas;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
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
        public static SqlDataAdapter ejecutarConsulta(string consulta)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(consulta, sqlConexion);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                Close();
                return da;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al ejecutar consulta: " + ex.Message);
                return null;
            }

        }


    }
}
