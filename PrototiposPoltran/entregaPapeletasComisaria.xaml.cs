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
        String codComi;
        ScrollViewer scrollContenedor;
        public entregaPapeletasComisaria(ScrollViewer scroll, String usuario)
        {
            InitializeComponent();
            this.usuario = usuario;
            this.scrollContenedor = scroll;

            c = new BDCom();
            dataGridComisaria.ItemsSource = c.GetAll().DefaultView;
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
                    if (codComi != "")
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
                                        //Reporte
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
                codComi=((TextBlock)RowColumn.Content).Text;

                CargarTablaTal();


            }
        }

        private void rbNegative_Checked(object sender, RoutedEventArgs e)
        {
            flagArch = false;
        }
    }
}
