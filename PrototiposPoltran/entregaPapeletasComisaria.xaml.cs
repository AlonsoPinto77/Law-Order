using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PrototiposPoltran
{
    /// <summary>
    /// Lógica de interacción para entregaPapeletas.xaml
    /// </summary>
    public partial class entregaPapeletasComisaria : UserControl
    {
        bool flagArch;
        BDCom c;
        String usuario;
        DataTable dataCom;
        String codComi, nomComi;
        ScrollViewer scrollContenedor;
        public entregaPapeletasComisaria(ScrollViewer scroll, String usuario)
        {
            InitializeComponent();
            this.usuario = usuario;
            this.scrollContenedor = scroll;

            c = new BDCom();
            dataCom = c.GetAll();
            dataGridComisaria.ItemsSource = dataCom.DefaultView;

            dpFechaEnvio.SelectedDate = DateTime.Today;
        }
        public void CargarTablaTal()
        {
            dataGridRelTalonario.ItemsSource = c.GetTalonarioComixEfe(codComi).DefaultView;
        }
        public void ClearAll()
        {
            txtRangoAl.Clear();
            txtRangoDel.Clear();
        }
        private void rbAfirmm_Checked(object sender, RoutedEventArgs e)
        {
            flagArch = true;
        }
        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            scrollContenedor.Content = null;
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int cant = Convert.ToInt32(lblCantidad.Content);
                if (cant > 0)
                {
                    if (codComi != "" || codComi != null)
                    {
                        BDTal tal = new BDTal();
                        String codTal = tal.obtenerId(txtRangoDel.Text, txtRangoAl.Text);

                        if (codTal != null)
                        {
                            BDPapeleta pap = new BDPapeleta();
                            int numPapAsig = pap.isAsignado(txtRangoDel.Text, txtRangoAl.Text);
                            if (numPapAsig == 0)
                            {
                                DateTime dt = dpFechaEnvio.SelectedDate.Value;
                                String fecha = dt.Year + "/" + dt.Month + "/" + dt.Day;

                                if (tal.entregarPapeletas(txtRangoDel.Text, txtRangoAl.Text, fecha, txtCodCIP.Text, codComi, codTal, usuario, cant))
                                {
                                    CargarTablaTal();
                                    if (flagArch)
                                    {
                                        DataTable dat = tal.obtenerDatos(txtRangoDel.Text, txtRangoAl.Text);
                                        Reportes rep = new Reportes();
                                        rep.TalonariosEntregados(dat);
                                        ClearAll();
                                    }
                                    MessageBox.Show("Se Asigno correctamente las papeletas");
                                }
                                else
                                {
                                    MessageBox.Show("Error: Al asignar las papeletas");
                                }
                            }
                            else
                            {
                                MessageBox.Show(numPapAsig + " papeletas dentro del rango ya han sido asignadas");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Rango de papeletas no pertenece a un talonario");
                        }
                    }
                    else
                    {
                        MessageBox.Show("No se selecciono Efectivo");
                    }
                }
                else
                {
                    MessageBox.Show("Error: Rango de Papeletas ");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString());
            }
        }

        private void btnImprimir_Click(object sender, RoutedEventArgs e)
        {
            Reportes rep = new Reportes();
            rep.imprimirPapeletasFaltantes(codComi,nomComi, 2);
        }

        private void txtCodCIP_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Validacion.IsValidCIP(txtCodCIP.Text) || (txtCodCIP.Text) == "")
            {
                lblErrCIP.Content = "El Codigo CIP no es Valido";
            }
            else
            {
                lblErrCIP.Content = "";
                BDEfe efe = new BDEfe();
                String str = efe.GetNom(txtCodCIP.Text);
                if( str != "")
                {
                    lblnombreEfectivo.Content = str;
                }
                else
                {
                    lblnombreEfectivo.Content = "NO EXISTE";
                }
            }
            
        }

        private void txtRangoDel_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Validacion.IsValidCodPapeleta(txtRangoDel.Text) || (txtRangoDel.Text) == "")
            {
                lblErrRango1.Content = "El Codigo de Papeleta no es Valido";
            }
            else
            {
                lblErrRango1.Content = "";
                if (this.txtRangoDel.Text != "" && this.txtRangoAl.Text != "")
                {
                    this.lblCantidad.Content = (int.Parse(this.txtRangoAl.Text) - int.Parse(this.txtRangoDel.Text) + 1).ToString();
                }
            }
        }

        private void txtRangoAl_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Validacion.IsValidCodPapeleta(txtRangoAl.Text) || (txtRangoAl.Text) == "")
            {
                lblErrRango2.Content = "El Codigo de Papeleta no es Valido";
            }
            else
            {
                lblErrRango2.Content = "";
                if (this.txtRangoDel.Text != "" && this.txtRangoAl.Text != "")
                {
                    this.lblCantidad.Content = (int.Parse(this.txtRangoAl.Text) - int.Parse(this.txtRangoDel.Text) + 1).ToString();
                }
            }
        }

        private void dataGridComisaria_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if(dataGridComisaria.SelectedItem != null)
            {
                DataGridRow row = (DataGridRow)dataGridComisaria.ItemContainerGenerator.ContainerFromIndex(dataGridComisaria.SelectedIndex);
                DataGridCell RowColumn = dataGridComisaria.Columns[0].GetCellContent(row).Parent as DataGridCell;
                codComi =((TextBlock)RowColumn.Content).Text;

                RowColumn = dataGridComisaria.Columns[1].GetCellContent(row).Parent as DataGridCell;
                nomComi = ((TextBlock)RowColumn.Content).Text;

                CargarTablaTal();


            }
        }

        private void rbNegative_Checked(object sender, RoutedEventArgs e)
        {
            flagArch = false;
        }

        private void txtBusCom_TextChanged(object sender, TextChangedEventArgs e)
        {
            
            String strbus = txtBusCom.Text;
            if ((txtBusCom.Text) != "")
            {
                DataTable dt = new DataTable();
                dt = c.obtenerCodorNom(strbus);
                dataGridComisaria.ItemsSource = dt.DefaultView;
            }
            else
            {
                dataGridComisaria.ItemsSource = dataCom.DefaultView;
            }
        }

        private void dataGridRelTalonario_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                if (dataGridRelTalonario.SelectedItem != null)
                {
                    string ini = "", fin = "";
                    int cant = 0;
                    DataGridRow row = (DataGridRow)dataGridRelTalonario.ItemContainerGenerator.ContainerFromIndex(dataGridRelTalonario.SelectedIndex);
                    DataGridCell RowColumn1 = dataGridRelTalonario.Columns[1].GetCellContent(row).Parent as DataGridCell;
                    DataGridCell RowColumn2 = dataGridRelTalonario.Columns[2].GetCellContent(row).Parent as DataGridCell;
                    DataGridCell RowColumn3 = dataGridRelTalonario.Columns[3].GetCellContent(row).Parent as DataGridCell;
                    ini = ((TextBlock)RowColumn1.Content).Text;
                    fin = ((TextBlock)RowColumn2.Content).Text;
                    cant = Convert.ToInt32(((TextBlock)RowColumn3.Content).Text);

                    BDTal tal = new BDTal();
                    string str = "Esta Seguro que desea eliminar los datos?. Se pederan los datos de las papeletas asignadas";
                    MessageBoxResult mes = MessageBox.Show(str, "Mensaje", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (mes == MessageBoxResult.Yes)
                    {
                        if (tal.EliminarRelTalonario(cant, ini, fin))
                            CargarTablaTal();
                        else
                            MessageBox.Show("Error: No se guardo el Talonario");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}
