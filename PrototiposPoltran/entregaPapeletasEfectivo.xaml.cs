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

namespace PrototiposPoltran
{
    /// <summary>
    /// Lógica de interacción para entregaPapeletasEfectivo.xaml
    /// </summary>
    public partial class entregaPapeletasEfectivo : UserControl
    {
        bool flagArch = false;
        BDEfe efe;
        DataTable dataEfe;
        ScrollViewer scrollContenedor;
        String codEfe = null, nomEfe = null;
        String usuario;
        public entregaPapeletasEfectivo(ScrollViewer scroll, String usuario)
        {
            InitializeComponent();
            this.scrollContenedor = scroll;
            this.usuario = usuario;
            efe = new BDEfe();
            dataEfe = efe.GetAll();
            dataGridEfectivo.ItemsSource = dataEfe.DefaultView;

            BDCom com = new BDCom();
            cmbCom.ItemsSource = com.GetAll().DefaultView;
            cmbCom.SelectedValuePath = "id_comisaria";
            cmbCom.DisplayMemberPath = "nombre_comisaria";

            dpFechaEnvio.SelectedDate = DateTime.Today;
        }
        public void CargarTablaTal()
        {
            dataGridTalonarios.ItemsSource = efe.GetTalonarioEfectivo(codEfe).DefaultView;
        }
        public void ClearAll()
        {
            txtRangoAl.Clear();
            txtRangoDel.Clear();
        }
        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int cant = Convert.ToInt32(lblCantidad.Content);
                if(cant > 0)
                {
                    if(codEfe != "" || codEfe != null)
                    {
                        BDTal tal = new BDTal();
                        String codTal = tal.obtenerId(txtRangoDel.Text, txtRangoAl.Text);
                    
                        if(codTal != null)
                        {
                            BDPapeleta pap = new BDPapeleta();
                            int numPapAsig = pap.isAsignado(txtRangoDel.Text, txtRangoAl.Text);
                            if (numPapAsig== 0)
                            {
                            
                                DateTime dt = dpFechaEnvio.SelectedDate.Value;
                                String fecha = dt.Year + "/" + dt.Month + "/" + dt.Day;

                                if (tal.entregarPapeletas(txtRangoDel.Text, txtRangoAl.Text, fecha, codEfe, cmbCom.SelectedValue as String, codTal, usuario, cant))
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
                                MessageBox.Show(numPapAsig +" papeletas dentro del rango ya han sido asignadas");
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
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.ToString());
            }

        }

        private void btnImprimir_Click(object sender, RoutedEventArgs e)
        {
            Reportes rep = new Reportes();
            rep.imprimirPapeletasFaltantes(codEfe,nomEfe, 1);
        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            scrollContenedor.Content = null;
        }

        private void rbAfirmm_Checked(object sender, RoutedEventArgs e)
        {
            flagArch = true;
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

        private void dataGridEfectivo_CurrentCellChanged(object sender, EventArgs e)
        {

        }

        private void dataGridEfectivo_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (dataGridEfectivo.SelectedItem != null)
            {
                DataGridRow row = (DataGridRow)dataGridEfectivo.ItemContainerGenerator.ContainerFromIndex(dataGridEfectivo.SelectedIndex);
                DataGridCell RowColumn = dataGridEfectivo.Columns[0].GetCellContent(row).Parent as DataGridCell;
                codEfe = ((TextBlock)RowColumn.Content).Text;

                RowColumn = dataGridEfectivo.Columns[1].GetCellContent(row).Parent as DataGridCell;
                nomEfe = ((TextBlock)RowColumn.Content).Text;

                CargarTablaTal();


            }
        }

        private void rbNegative_Checked(object sender, RoutedEventArgs e)
        {
            flagArch = false;
        }

        private void txtBuscarEfe_TextChanged(object sender, TextChangedEventArgs e)
        {
            String strbus = txtBuscarEfe.Text;
            if ((txtBuscarEfe.Text) != "")
            {
                DataTable dt= new DataTable();
                dt = efe.obtenerCIPorNom(strbus);
                dataGridEfectivo.ItemsSource = dt.DefaultView;
            }
            else
            {
                dataGridEfectivo.ItemsSource = dataEfe.DefaultView;
            }
        }

        private void dataGridTalonarios_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                if (dataGridTalonarios.SelectedItem != null)
                {
                    string ini = "", fin = "";
                    int cant = 0;
                    DataGridRow row = (DataGridRow)dataGridTalonarios.ItemContainerGenerator.ContainerFromIndex(dataGridTalonarios.SelectedIndex);
                    DataGridCell RowColumn1 = dataGridTalonarios.Columns[1].GetCellContent(row).Parent as DataGridCell;
                    DataGridCell RowColumn2 = dataGridTalonarios.Columns[2].GetCellContent(row).Parent as DataGridCell;
                    DataGridCell RowColumn3 = dataGridTalonarios.Columns[3].GetCellContent(row).Parent as DataGridCell;
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
            catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            
        }
    }
}
