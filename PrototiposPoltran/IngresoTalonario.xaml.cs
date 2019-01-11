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
    /// Lógica de interacción para IngresoTalonario.xaml
    /// </summary>
    public partial class IngresoTalonario : UserControl
    {
        String codUsuario = "";
        ScrollViewer scrollContenedor;
         
        public IngresoTalonario(ScrollViewer scroll,String usuario)
        {
            InitializeComponent();
            this.scrollContenedor = scroll;
            codUsuario = usuario;
        }

        private void btnGrabar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int cant = Convert.ToInt32(lblCantidad.Content);

                if (cant > 0)
                {
                    int lgNroOfi = ((lblErrNroOfi.Content) as String).Length;
                    int lgTitOfi = ((lblErrTitOfi.Content) as String).Length;
                    BDPapeleta pap = new BDPapeleta();
                    int numPapAsig = pap.isAsignado(txtRangoDel.Text, txtRangoAl.Text);
                    if (numPapAsig == 0)
                    {
                        if (lgNroOfi == 0 && lgTitOfi == 0)
                        {
                            BDOficio ofi = new BDOficio();
                            if (ofi.IngresarOficio(txtNroOficio.Text, "M", txtTituloOficio.Text, txtDescripcion.Text))
                            {
                                DateTime dt = dpFechaIngreso.SelectedDate.Value;
                                String fecha = dt.Year + "/" + dt.Month + "/" + dt.Day;
                                BDTal tal = new BDTal();
                                if (tal.IngresarTal(txtRangoDel.Text, txtRangoAl.Text, fecha, codUsuario, txtNroOficio.Text, Convert.ToInt32(lblCantidad.Content)))
                                    MessageBox.Show("Se Grabo el Talonario y Oficio exitosamente");
                                else
                                    MessageBox.Show("Error: No se Guardo el Talonario");
                            }
                            else
                            {
                                MessageBox.Show("Error: No se grabo el oficio");
                            }

                        }
                    }
                    else
                    {
                        MessageBox.Show(numPapAsig + " papeletas dentro del rango ya han sido ingresadas en otro talonario");
                    }
                }
                else
                {
                    MessageBox.Show("Error: La cantidad negativa");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: "+ex.ToString());
            }
        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            this.scrollContenedor.Content = null;
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

        private void txtTituloOficio_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtTituloOficio.Text == "")
            {
                lblErrTitOfi.Content = "No se Ingreso Titulo";
            }
            else
            {
                lblErrTitOfi.Content = "";
            }
        }

        private void txtNroOficio_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtNroOficio.Text == "")
            {
                lblErrNroOfi.Content = "No se Ingreso Nro Oficio";
            }
            else
            {
                lblErrNroOfi.Content = "";
            }
        }
    }
}
