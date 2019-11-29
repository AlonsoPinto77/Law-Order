using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Collections;
using System.Windows.Controls;

namespace PrototiposPoltran
{
    public partial class PapeletasPorComisaria : Form
    {
        SqlCommand cmd;
        SqlDataReader dr;
        public PapeletasPorComisaria()
        {
            this.TopLevel = false;
            InitializeComponent();
        }

        ArrayList Comisaria = new ArrayList();
        ArrayList Papeletas = new ArrayList();
        private void Prueba_Load(object sender, EventArgs e)
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {
            int ano = Convert.ToInt32(textBoxAno.Text);
            int mes = Convert.ToInt32(textBoxMes.Text);
            GraficoComisaria(ano, mes);
        }
        private void GraficoComisaria(int a, int m)
        {
            Conexion.Open();
            cmd = new SqlCommand("EstadisticasPrueba", Conexion.sqlConexion);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ano", SqlDbType.Int).Value = a;
            cmd.Parameters.Add("@mes", SqlDbType.Int).Value = m;
            
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Comisaria.Add(dr.GetString(0));
                Papeletas.Add(dr.GetInt32(1));
            }
            chart1.Series[0].Points.DataBindXY(Comisaria, Papeletas);
            Comisaria.Clear();
            Papeletas.Clear();
            dr.Close();
            Conexion.Close();
        }

        private void textBoxAno_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
                MessageBox.Show("SOLO SE PUEDEN INGRESAR NUMEROS");
            }
        }

        private void textBoxMes_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
                MessageBox.Show("SOLO SE PUEDEN INGRESAR NUMEROS");
            }
        }

        private void textBoxAno_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxAno.Text))
            {
                e.Cancel = true;
            }
            if (e.Cancel)
                MessageBox.Show("LLENE EL CAMPO");
        }

        private void textBoxMes_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxMes.Text))
            {
                e.Cancel = true;
            }
            if (e.Cancel)
                MessageBox.Show("LLENE EL CAMPO");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach (var series in chart1.Series)
            {
                series.Points.Clear();
            }
        }
    }
}
