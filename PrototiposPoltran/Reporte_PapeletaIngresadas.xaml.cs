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
    /// Lógica de interacción para Reporte_PapeletaIngresadas.xaml
    /// </summary>
    public partial class Reporte_PapeletaIngresadas : UserControl
    {
        ScrollViewer scrollContenedor;
        public Reporte_PapeletaIngresadas(ScrollViewer scroll)
        {
        
            this.scrollContenedor = scroll;
            InitializeComponent();
        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
