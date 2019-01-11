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
    /// Lógica de interacción para PapeletasComisaria.xaml
    /// </summary>
    public partial class PapeletasComisaria : UserControl
    {
        ScrollViewer scrollContenedor;
        public PapeletasComisaria(ScrollViewer scroll)
        {
            InitializeComponent();
            this.scrollContenedor = scroll;
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            //resultadosBúsqueda resultado = new resultadosBúsqueda();
            //this.contenedor.Content = resultado;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            scrollContenedor.Content = null;

        }
    }
}
