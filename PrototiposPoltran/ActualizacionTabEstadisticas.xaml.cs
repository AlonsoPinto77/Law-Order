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
    /// Lógica de interacción para ActualizacionTabEstadisticas.xaml
    /// </summary>
    public partial class ActualizacionTabEstadisticas : Window
    {
        private ScrollViewer scrollContenedor;

        public ActualizacionTabEstadisticas()
        {
            InitializeComponent();
        }

        public ActualizacionTabEstadisticas(ScrollViewer scrollContenedor)
        {
            this.scrollContenedor = scrollContenedor;
        }
    }
}
