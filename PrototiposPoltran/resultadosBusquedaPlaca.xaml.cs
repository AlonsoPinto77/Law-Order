using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;

namespace PrototiposPoltran
{
    /// <summary>
    /// Lógica de interacción para resultadosBusquedaPlaca.xaml
    /// </summary>
    public partial class resultadosBusquedaPlaca : UserControl
    {
        string nroPlaca;
        ScrollViewer scrollContenedor;
        DataSet tabConsulta;
        public resultadosBusquedaPlaca(string nroPlaca, ScrollViewer scrollContenedor)
        {
            InitializeComponent();
            this.nroPlaca = nroPlaca;
            this.scrollContenedor = scrollContenedor;
            buscarPapeletas();
        }

        private void buscarPapeletas()
        {
            try
            {
                string consultaPlaca = "SELECT numero_papeleta,falta,fisico,estado.descripcion AS estado,dni_conductor, tipo_infraccion," +
                                        "placa,fecha_imposicion,fecha_envio,numero_oficio,id_relTalonario FROM papeleta " +
                                        "INNER JOIN estado ON(papeleta.estado = estado.id_estado) WHERE placa ='" + nroPlaca + "'";
                Conexion.Open();
                SqlDataAdapter da = Conexion.ejecutarConsulta(consultaPlaca);
                if (da != null)
                {
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    tabConsulta = ds;
                    dtGrdListaInfrac.ItemsSource = ds.Tables[0].DefaultView;
                    dtGrdListaInfrac.IsReadOnly = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

        }

        //devolverDatos() ejecuta una devolución para el contenedor superior, en este caso busquedaPapeletas, y usa 
        //el dataset para generar el reporte
        public DataSet devolverDatos()
        {
            if (tabConsulta != null)
                return tabConsulta;
            else
                return null;
        }

        private void dtGrdListaInfrac_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ejecutarBusquedaPorRegistro(sender);
        }

        private void dtGrdListaInfrac_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                ejecutarBusquedaPorRegistro(sender);
        }
        private void ejecutarBusquedaPorRegistro(object sender)
        {
            try
            {
                if (sender != null)
                {
                    DataGrid grid = sender as DataGrid;
                    if (grid != null && grid.SelectedItems != null && grid.SelectedItems.Count == 1)
                    {
                        DataGridRow dgr = grid.ItemContainerGenerator.ContainerFromItem(grid.SelectedItem) as DataGridRow;
                        DataRowView dr = (DataRowView)dgr.Item;
                        String nroPapeleta = dr[0].ToString();
                        resultadosBusqueda busqueda = new resultadosBusqueda(nroPapeleta, scrollContenedor);
                        this.scrollContenedor.Content = busqueda;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al ejecutar busqueda por registro " + ex.Message.ToString());
            }
        }

    }
}
