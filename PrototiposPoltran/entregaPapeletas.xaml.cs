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
    public partial class entregaPapeletas : UserControl
    {
        ScrollViewer scrollContenedor;
        public entregaPapeletas(ScrollViewer scroll)
        {
            InitializeComponent();
            this.scrollContenedor = scroll;
        }

        private void radioButton_Checked(object sender, RoutedEventArgs e)
        {
            
        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void dataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int i = 0;

        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            scrollContenedor.Content = null;
        }
    }
}
