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
using System.Windows.Shapes;

namespace PrototiposPoltran
{
    /// <summary>
    /// Lógica de interacción para VentanaPrincipal.xaml
    /// </summary>
    public partial class VentanaPrincipal : Window
    {
        public VentanaPrincipal()
        {
            InitializeComponent();
            btnEntregaCom.Visibility = Visibility.Hidden;
            btnEntregaEfec.Visibility = Visibility.Hidden;
            btnIngresoDev.Visibility = Visibility.Hidden;
            btnBusquedaPapeleta.Visibility = Visibility.Hidden;
            btnBusquedaPlaca.Visibility = Visibility.Hidden;
            btnBusquedaOtros.Visibility = Visibility.Hidden;

        }

        private void btnMesaPart1_Click(object sender, RoutedEventArgs e)
        {
            /*btnMesaPart.Visibility = Visibility.Hidden;
            btnMesaPart1.Visibility = Visibility.Hidden;
            imgMesaPart.Visibility = Visibility.Hidden;
            btnEntregaCom.Visibility = Visibility.Visible;
            btnEntregaEfec.Visibility = Visibility.Visible;
            btnIngresoDev.Visibility = Visibility.Visible;*/
            ocultarMostrarBotones(1);

        }

        private void btnEntregaEfec_Click(object sender, RoutedEventArgs e)
        {
            entregaPapeletas entrega = new entregaPapeletas(this.scrollContenedor);
            //this.contentContenedor.Content = entrega;
            this.scrollContenedor.Content = entrega;
            entrega.HorizontalAlignment = HorizontalAlignment.Stretch;
            entrega.VerticalAlignment = VerticalAlignment.Stretch;
        }

        private void btnBusqueda_Click(object sender, RoutedEventArgs e)
        {
            /*btnMesaPart.Visibility = Visibility.Hidden;
            btnMesaPart1.Visibility = Visibility.Hidden;
            imgMesaPart.Visibility = Visibility.Hidden;
            btnEntregaCom.Visibility = Visibility.Hidden;
            btnEntregaEfec.Visibility = Visibility.Hidden;
            btnIngresoDev.Visibility = Visibility.Hidden;*/
            ocultarMostrarBotones(2);
                

        }
        private void ocultarMostrarBotones(int id)
        {
            switch (id)
            {
                case 1:
                    btnMesaPart.Visibility = Visibility.Hidden;
                    btnMesaPart1.Visibility = Visibility.Hidden;
                    imgMesaPart.Visibility = Visibility.Hidden;
                    btnBusquedaPapeleta.Visibility = Visibility.Hidden;
                    btnBusquedaPlaca.Visibility = Visibility.Hidden;
                    btnBusquedaOtros.Visibility = Visibility.Hidden;
                    btnEntregaCom.Visibility = Visibility.Visible;
                    btnEntregaEfec.Visibility = Visibility.Visible;
                    btnIngresoDev.Visibility = Visibility.Visible;
                    btnBusquedaPapeleta.Visibility = Visibility.Hidden;
                    btnBusqueda.Visibility = Visibility.Visible;
                    imgBusqueda.Visibility = Visibility.Visible;
                    break;
                case 2:  
                    btnBusqueda.Visibility = Visibility.Hidden;       
                    imgBusqueda.Visibility = Visibility.Hidden;
                    btnEntregaCom.Visibility = Visibility.Hidden;
                    btnEntregaEfec.Visibility = Visibility.Hidden;
                    btnIngresoDev.Visibility = Visibility.Hidden;
                    btnBusquedaPapeleta.Visibility = Visibility.Visible;
                    btnBusquedaPlaca.Visibility = Visibility.Visible;
                    btnBusquedaOtros.Visibility = Visibility.Visible;
                    btnMesaPart.Visibility = Visibility.Visible;
                    btnMesaPart1.Visibility = Visibility.Visible;
                    imgMesaPart.Visibility = Visibility.Visible;
                    break;
            }

            
        }

        private void btnBusquedaPapeleta_Click(object sender, RoutedEventArgs e)
        {
            busquedaPapeletas busquedapapel = new busquedaPapeletas(this.scrollContenedor);
            this.scrollContenedor.Content = busquedapapel;
            busquedapapel.HorizontalAlignment = HorizontalAlignment.Stretch;
            busquedapapel.VerticalAlignment = VerticalAlignment.Stretch;
        }

        
    }
}
