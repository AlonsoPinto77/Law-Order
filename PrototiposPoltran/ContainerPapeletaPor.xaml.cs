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
    /// Lógica de interacción para PapeletaPorOficialContainer.xaml
    /// </summary>
    public partial class ContainerPapeletaPor : UserControl
    {
        public ContainerPapeletaPor(int tip)
        {
            InitializeComponent();

            System.Windows.Forms.Integration.WindowsFormsHost host = new 
                System.Windows.Forms.Integration.WindowsFormsHost();
            
            if(tip == 1)
            {
                PapeletasPorOficial axWmp = new PapeletasPorOficial();

                host.Child = axWmp;
                this.grid1.Children.Add(host);
            }
            if(tip == 2)
            {
                PapeletasPorComisaria axWmp = new PapeletasPorComisaria();
                host.Child = axWmp;
                this.grid1.Children.Add(host);
            }
            
        }
    }
}
