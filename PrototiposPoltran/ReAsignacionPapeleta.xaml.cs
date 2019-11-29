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
    /// Lógica de interacción para ReAsignacionPapeleta.xaml
    /// </summary>
    public partial class ReAsignacionPapeleta : UserControl
    {
        String usuario;
        String codComi;
        BDPapeleta pap;
        ScrollViewer sc;
        public ReAsignacionPapeleta()
        {
            InitializeComponent();
        }
        public ReAsignacionPapeleta(ScrollViewer scroll, String usuario)
        {
            InitializeComponent();
            try
            {
                this.sc = scroll;
                this.usuario = usuario;
                CargarTabPap();

                BDCom co = new BDCom();
                cmbCom.ItemsSource = co.GetAll().DefaultView;
                cmbCom.SelectedValuePath = "id_comisaria";
                cmbCom.DisplayMemberPath = "nombre_comisaria";

                dpFechaIngreso.SelectedDate = DateTime.Today;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        public void CargarTabPap()
        {
            pap = new BDPapeleta();
            dataGridPape.ItemsSource = pap.PapeletasaAsignar().DefaultView;
        }

        private void btnGrabar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int cant = Convert.ToInt32(lblCantidad.Content);
                if (cant > 0)
                {
                    if (codComi != "" && txtCodCIP.Text!= "")
                    {
                        DateTime dt = dpFechaIngreso.SelectedDate.Value;
                        String fecha = dt.Year + "/" + dt.Month + "/" + dt.Day;
                        BDPapeleta pap = new BDPapeleta();
                        int numPapAsig = pap.isReAsignado(txtRangoDel.Text, txtRangoAl.Text);
                        if (numPapAsig == cant)
                        {
                            BDTal tal = new BDTal();
                            if (tal.ReasignarTalonario(cant, txtRangoDel.Text, txtRangoAl.Text, fecha, txtCodCIP.Text, codComi, usuario))
                            {
                                DataTable dat = tal.obtenerDatos(txtRangoDel.Text, txtRangoAl.Text);
                                Reportes rep = new Reportes();
                                rep.TalonariosAsignados(dat);
                                
                                CargarTabPap();
                                MessageBox.Show("Se actualizacion los datos exitosamente");
                            }
                            else
                            {
                                MessageBox.Show("no se guardaron los datos...");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Error: no todas las papeletas se encuentran en el estado de reasignar");
                        }
                    }
                    else
                    {
                        MessageBox.Show("No se selecciono comisaria o no se ingreso CIP");
                    }
                }
                else
                {
                    MessageBox.Show("Error: Rango de Papeletas ");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString());
            }
        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            this.sc = null;
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
                if (str != "")
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
            try
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
            catch(Exception ex)
            {
                this.lblCantidad.Content = "No numerico";
            }
        }

        private void dataGridPape_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cmbCom_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            codComi = cmbCom.SelectedValue.ToString();
        }
    }
}
