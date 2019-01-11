using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

namespace PrototiposPoltran
{
    /// <summary>
    /// Lógica de interacción para resultadosBusquedaOtros.xaml
    /// </summary>
    public partial class resultadosBusquedaOtros : UserControl
    {
        int opc = 0;
        string cont;
        ScrollViewer scrollContenedor;
        DataSet tabConsulta;
        public resultadosBusquedaOtros(ScrollViewer scrollContenedor, int opc, string cont)
        {
            InitializeComponent();
            this.scrollContenedor = scrollContenedor;
            this.opc = opc;
            this.cont = cont;
            buscarPapeletas();
        }

        private void buscarPapeletas()
        {
            //Para todos los case (casos) cont significa el contenido, no se sabe exactamente cual será...
            //... puede ser una delegación, un dni de conductor, un nombre o un cip del efectivo... por eso el nombre "cont"
            try
            {
                Conexion.Open();
                SqlDataAdapter da;
                if (opc >= 1 && opc <= 4)
                {

                    switch (opc)
                    {
                        case 1:
                            //Aun se encuentraen fase de desarrollo
                            MessageBox.Show("Opción en construccion...");
                            break;
                        /*string consultaPlaca = "SELECT * FROM papeleta WHERE placa ='";
                        Conexion con = new Conexion();
                        SqlDataAdapter da = Conexion.ejecutarConsulta(consultaPlaca);
                        if (da != null)
                        {
                            DataSet ds = new DataSet();
                            da.Fill(ds);
                            tabConsulta = ds;
                            dtGrdListaInfrac.ItemsSource = ds.Tables[0].DefaultView;
                            dtGrdListaInfrac.IsReadOnly = true;
                        }*/
                        case 2:
                            //Ejecuta la consulta de todas la papeletas que pueda tener un conductor por su dni
                            string consultaConductor = "SELECT * FROM papeleta WHERE dni_conductor ='" + cont + "'";
                            da = Conexion.ejecutarConsulta(consultaConductor);
                            DataSet ds = new DataSet();
                            if (da != null)
                            {
                                da.Fill(ds);
                                tabConsulta = ds;
                                dtGrdListaInfrac.ItemsSource = ds.Tables[0].DefaultView;
                                dtGrdListaInfrac.IsReadOnly = true;
                            }
                            break;
                        case 3:
                            string consultaCIP = "SELECT id_relTalonario FROM RelTalonario WHERE cip ='" + cont + "'";
                            Conexion.Open();
                            DataSet ds1 = new DataSet();
                            DataSet temp0 = new DataSet();
                            //Ya se tiene el CIP del efectivo, se procede a ejecutar la consulta de cuantos y cuales id_relTalonario tiene un efectivo
                            da = Conexion.ejecutarConsulta(consultaCIP);
                            if (da != null)
                            {
                                //Con el cip obtenido se procede a buscar todas las papeletas que tengan el o los id_relTalonario
                                da.Fill(ds1);
                                foreach (DataRow dr in ds1.Tables[0].Rows) //-------------> Recorre todos los posibles id_relTalonario que pueda tener un efectivo
                                {
                                    //DataRow dr obtiene cada id_relTalonario que pueda tener un efectivo
                                    string consultaPap = "SELECT numero_papeleta,falta,fisico,estado.descripcion AS estado,dni_conductor, tipo_infraccion,"+
                                        "placa,fecha_imposicion,fecha_envio,numero_oficio,id_relTalonario FROM papeleta "+
                                        "INNER JOIN estado ON(papeleta.estado = estado.id_estado) WHERE id_relTalonario ='" + dr["id_relTalonario"].ToString() + "'";
                                    da = Conexion.ejecutarConsulta(consultaPap); //---------------> al ejecutar la consulta lo hace por cada id_relTalonario que tenga dr
                                    if (da != null)
                                        da.Fill(temp0);//----------------> Aqui lo que hace es llenar el DataSet temp0 (temporal obviamente) que va adicionando en cada iteracion las papeletas que encuentre
                                }
                                tabConsulta = temp0;
                                dtGrdListaInfrac.ItemsSource = temp0.Tables[0].DefaultView;
                                dtGrdListaInfrac.IsReadOnly = true;
                            }
                            break;
                        case 4:
                            string consultaNombreEf = "SELECT cip FROM efectivo WHERE nombre_efectivo = '" + cont + "'";
                            string consultaIdRelTalCip = "SELECT id_relTalonario FROM RelTalonario WHERE cip= '";
                            string cip;
                            DataSet ds2 = new DataSet();
                            DataSet temp = new DataSet();
                            DataTable dt2 = new DataTable();
                            //Primero se obtiene el codigo cip del efectivo
                            da = Conexion.ejecutarConsulta(consultaNombreEf);
                            if (da != null)
                            {
                                da.Fill(dt2);
                                cip = dt2.Rows[0]["cip"].ToString();
                                //Con el cip obtenido se procede a buscar todas las papeletas que tengan el o los id_relTalonario
                                da = Conexion.ejecutarConsulta(consultaIdRelTalCip + cip + "'");
                                if (da != null)
                                {
                                    da.Fill(ds2);
                                    foreach (DataRow dr in ds2.Tables[0].Rows) //-------------> Recorre todos los posibles id_relTalonario que pueda tener un efectivo
                                    {
                                        //DataRow dr obtiene cada id_relTalonario que pueda tener un efectivo
                                        string consultaPap = "SELECT numero_papeleta,falta,fisico,estado.descripcion AS estado,dni_conductor, tipo_infraccion," +
                                        "placa,fecha_imposicion,fecha_envio,numero_oficio,id_relTalonario FROM papeleta " +
                                        "INNER JOIN estado ON(papeleta.estado = estado.id_estado) WHERE id_relTalonario ='" + dr["id_relTalonario"].ToString() + "'";
                                        da = Conexion.ejecutarConsulta(consultaPap); //---------------> al ejecutar la consulta lo hace por cada id_relTalonario que tenga dr
                                        if (da != null)
                                            da.Fill(temp); //----------------> Aqui lo que hace es llenar el DataSet temp (temporal obviamente) que va adicionando en cada iteracion las papeletas que encuentre
                                    }
                                    tabConsulta = temp;
                                    dtGrdListaInfrac.ItemsSource = temp.Tables[0].DefaultView;
                                    dtGrdListaInfrac.IsReadOnly = true;
                                }
                            }
                            break;
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

        }

        //devolverDatos() ejecuta una devolución para el contenedor superior, en este caso busquedaOtros, y usa 
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
                        resultadosBusqueda busqueda = new resultadosBusqueda(this.scrollContenedor, opc, cont, nroPapeleta);
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
