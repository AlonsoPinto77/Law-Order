﻿using System;
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
    /// Lógica de interacción para Utilidades.xaml
    /// </summary>
    public partial class MantenimientoTablas : UserControl
    {
        public MantenimientoTablas()
        {
            InitializeComponent();
        }
        ScrollViewer scrollContenedor;
        public MantenimientoTablas(ScrollViewer scroll)
        {
            InitializeComponent();
            this.scrollContenedor = scroll;
        }
        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
