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
    /// Lógica de interacción para CuadroConductores.xaml
    /// </summary>
    public partial class CuadroConductores : UserControl
    {
        ScrollViewer scrollContenedor;
        public CuadroConductores(ScrollViewer scroll)
        {
            InitializeComponent();
            this.scrollContenedor = scroll;
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
