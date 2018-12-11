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
            btnPapeletasIngresadas.Visibility = Visibility.Hidden;
            btnExportarInfo.Visibility = Visibility.Hidden;
            btnCuadroMensual.Visibility = Visibility.Hidden;
            btn_PapeletasPorMes.Visibility = Visibility.Hidden;
            btn_PapeletasComisaria.Visibility = Visibility.Hidden;
            btn_PapeletasOficial.Visibility = Visibility.Hidden;

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
        private void buttonReportes_Click(object sender, RoutedEventArgs e)
        {
            ocultarMostrarBotones(3);
        }
        private void buttonEstadisticas_Click(object sender, RoutedEventArgs e)
        {
            ocultarMostrarBotones(4);
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
                    btnPapeletasIngresadas.Visibility = Visibility.Hidden;
                    btnExportarInfo.Visibility = Visibility.Hidden;
                    btnCuadroMensual.Visibility = Visibility.Hidden;
                    btnReportes.Visibility = Visibility.Visible;
                    image_Reportes.Visibility = Visibility.Visible;
                    btn_PapeletasPorMes.Visibility = Visibility.Hidden;
                    btn_PapeletasComisaria.Visibility = Visibility.Hidden;
                    btn_PapeletasOficial.Visibility = Visibility.Hidden;
                    button.Visibility = Visibility.Visible;
                    image.Visibility = Visibility.Visible;
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
                    btnPapeletasIngresadas.Visibility = Visibility.Hidden;
                    btnExportarInfo.Visibility = Visibility.Hidden;
                    btnCuadroMensual.Visibility = Visibility.Hidden;
                    btnReportes.Visibility = Visibility.Visible;
                    image_Reportes.Visibility = Visibility.Visible;
                    btn_PapeletasPorMes.Visibility = Visibility.Hidden;
                    btn_PapeletasComisaria.Visibility = Visibility.Hidden;
                    btn_PapeletasOficial.Visibility = Visibility.Hidden;
                    button.Visibility = Visibility.Visible;
                    image.Visibility = Visibility.Visible;
                    break;
                case 3:
                    btnMesaPart.Visibility = Visibility.Visible;
                    btnMesaPart1.Visibility = Visibility.Visible;
                    imgMesaPart.Visibility = Visibility.Visible;
                    btnBusqueda.Visibility = Visibility.Visible;
                    imgBusqueda.Visibility = Visibility.Visible;
                    btnEntregaCom.Visibility = Visibility.Hidden;
                    btnEntregaEfec.Visibility = Visibility.Hidden;
                    btnIngresoDev.Visibility = Visibility.Hidden;
                    btnBusquedaPapeleta.Visibility = Visibility.Hidden;
                    btnBusquedaPlaca.Visibility = Visibility.Hidden;
                    btnBusquedaOtros.Visibility = Visibility.Hidden;
                    btnReportes.Visibility = Visibility.Hidden;
                    image_Reportes.Visibility = Visibility.Hidden;
                    btnPapeletasIngresadas.Visibility = Visibility.Visible;
                    btnExportarInfo.Visibility = Visibility.Visible;
                    btnCuadroMensual.Visibility = Visibility.Visible;
                    btn_PapeletasPorMes.Visibility = Visibility.Hidden;
                    btn_PapeletasComisaria.Visibility = Visibility.Hidden;
                    btn_PapeletasOficial.Visibility = Visibility.Hidden;
                    button.Visibility = Visibility.Visible;
                    image.Visibility = Visibility.Visible;
                    break;

                case 4:
                    btnMesaPart.Visibility = Visibility.Visible;
                    btnMesaPart1.Visibility = Visibility.Visible;
                    imgMesaPart.Visibility = Visibility.Visible;
                    btnBusqueda.Visibility = Visibility.Visible;
                    imgBusqueda.Visibility = Visibility.Visible;
                    btnEntregaCom.Visibility = Visibility.Hidden;
                    btnEntregaEfec.Visibility = Visibility.Hidden;
                    btnIngresoDev.Visibility = Visibility.Hidden;
                    btnBusquedaPapeleta.Visibility = Visibility.Hidden;
                    btnBusquedaPlaca.Visibility = Visibility.Hidden;
                    btnBusquedaOtros.Visibility = Visibility.Hidden;
                    btnReportes.Visibility = Visibility.Visible;
                    image_Reportes.Visibility = Visibility.Visible;
                    btnPapeletasIngresadas.Visibility = Visibility.Hidden;
                    btnExportarInfo.Visibility = Visibility.Hidden;
                    btnCuadroMensual.Visibility = Visibility.Hidden;
                    btn_PapeletasPorMes.Visibility = Visibility.Visible;
                    btn_PapeletasComisaria.Visibility = Visibility.Visible;
                    btn_PapeletasOficial.Visibility = Visibility.Visible;
                    button.Visibility = Visibility.Hidden;
                    image.Visibility = Visibility.Hidden;
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

        private void btnPapeletasIngresadas_Click(object sender, RoutedEventArgs e)
        {

            Reporte_PapeletaIngresadas objeto1 = new Reporte_PapeletaIngresadas(this.scrollContenedor);
            this.scrollContenedor.Content = objeto1;
            objeto1.HorizontalAlignment = HorizontalAlignment.Stretch;
            objeto1.VerticalAlignment = VerticalAlignment.Stretch;
        }

        private void btnExportarInfo_Click(object sender, RoutedEventArgs e)
        {
            ExportarInfo objeto2 = new ExportarInfo(this.scrollContenedor);
            this.scrollContenedor.Content = objeto2;
            objeto2.HorizontalAlignment = HorizontalAlignment.Stretch;
            objeto2.VerticalAlignment = VerticalAlignment.Stretch;
        }

        private void btnCuadroMensual_Click(object sender, RoutedEventArgs e)
        {
            CuadroConductores objeto3 = new CuadroConductores(this.scrollContenedor);
            this.scrollContenedor.Content = objeto3;
            objeto3.HorizontalAlignment = HorizontalAlignment.Stretch;
            objeto3.VerticalAlignment = VerticalAlignment.Stretch;
        }

        private void btn_PapeletasPorMes_Click(object sender, RoutedEventArgs e)
        {
            PapeletasPorMes objeto4 = new PapeletasPorMes(this.scrollContenedor);
            this.scrollContenedor.Content = objeto4;
            objeto4.HorizontalAlignment = HorizontalAlignment.Stretch;
            objeto4.VerticalAlignment = VerticalAlignment.Stretch;
        }

        private void btn_PapeletasComisaria_Click(object sender, RoutedEventArgs e)
        {
            PapeletasComisaria objeto5 = new PapeletasComisaria(this.scrollContenedor);
            this.scrollContenedor.Content = objeto5;
            objeto5.HorizontalAlignment = HorizontalAlignment.Stretch;
            objeto5.VerticalAlignment = VerticalAlignment.Stretch;
        }
    }
}
