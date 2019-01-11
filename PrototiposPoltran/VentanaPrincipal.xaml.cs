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
        String codUsuario = "";
        public VentanaPrincipal(string cod)
        {
            InitializeComponent();
            if(cod != "")
            {
                codUsuario = cod;
            }
            else
            {
                MainWindow m = new MainWindow();
                m.Show();
                this.Close();
            }
            
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
                    btnAsigPape.Visibility = Visibility.Visible;
                    btnIngTalMun.Visibility = Visibility.Visible;

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
                    btnEstadisticas.Visibility = Visibility.Visible;
                    image.Visibility = Visibility.Visible;
                    btn_MantenimientoTablas.Visibility = Visibility.Hidden;
                    btn_ActIndices.Visibility = Visibility.Hidden;
                    btn_Backup.Visibility = Visibility.Hidden;
                    btn_ActTablasEstadisticas.Visibility = Visibility.Hidden;
                    btnUtilidades.Visibility = Visibility.Visible;
                    imgBD.Visibility = Visibility.Visible;
                    imgconf.Visibility = Visibility.Visible;
                    break;
                case 2:
                    btnBusqueda.Visibility = Visibility.Hidden;
                    imgBusqueda.Visibility = Visibility.Hidden;
                    btnEntregaCom.Visibility = Visibility.Hidden;
                    btnEntregaEfec.Visibility = Visibility.Hidden;
                    btnIngresoDev.Visibility = Visibility.Hidden;
                    btnAsigPape.Visibility = Visibility.Hidden;
                    btnIngTalMun.Visibility = Visibility.Hidden;

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
                    btnEstadisticas.Visibility = Visibility.Visible;
                    image.Visibility = Visibility.Visible;
                    btn_MantenimientoTablas.Visibility = Visibility.Hidden;
                    btn_ActIndices.Visibility = Visibility.Hidden;
                    btn_Backup.Visibility = Visibility.Hidden;
                    btn_ActTablasEstadisticas.Visibility = Visibility.Hidden;
                    btnUtilidades.Visibility = Visibility.Visible;
                    imgBD.Visibility = Visibility.Visible;
                    imgconf.Visibility = Visibility.Visible;
                    break;
                case 3:
                    btnMesaPart.Visibility = Visibility.Visible;
                    btnMesaPart1.Visibility = Visibility.Visible;
                    imgMesaPart.Visibility = Visibility.Visible;
                    
                    btnEntregaCom.Visibility = Visibility.Hidden;
                    btnEntregaEfec.Visibility = Visibility.Hidden;
                    btnIngresoDev.Visibility = Visibility.Hidden;
                    btnAsigPape.Visibility = Visibility.Hidden;
                    btnIngTalMun.Visibility = Visibility.Hidden;

                    btnBusqueda.Visibility = Visibility.Visible;
                    imgBusqueda.Visibility = Visibility.Visible;

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

                    btnEstadisticas.Visibility = Visibility.Visible;
                    image.Visibility = Visibility.Visible;

                    btn_MantenimientoTablas.Visibility = Visibility.Hidden;
                    btn_ActIndices.Visibility = Visibility.Hidden;
                    btn_Backup.Visibility = Visibility.Hidden;
                    btn_ActTablasEstadisticas.Visibility = Visibility.Hidden;

                    btnUtilidades.Visibility = Visibility.Visible;
                    imgBD.Visibility = Visibility.Visible;
                    imgconf.Visibility = Visibility.Visible;
                    break;
                case 4:
                    btnMesaPart.Visibility = Visibility.Visible;
                    btnMesaPart1.Visibility = Visibility.Visible;
                    imgMesaPart.Visibility = Visibility.Visible;

                    
                    btnEntregaCom.Visibility = Visibility.Hidden;
                    btnEntregaEfec.Visibility = Visibility.Hidden;
                    btnIngresoDev.Visibility = Visibility.Hidden;
                    btnAsigPape.Visibility = Visibility.Hidden;
                    btnIngTalMun.Visibility = Visibility.Hidden;
                    btnBusqueda.Visibility = Visibility.Visible;
                    imgBusqueda.Visibility = Visibility.Visible;

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

                    btnEstadisticas.Visibility = Visibility.Hidden;
                    image.Visibility = Visibility.Hidden;

                    btn_MantenimientoTablas.Visibility = Visibility.Hidden;
                    btn_ActIndices.Visibility = Visibility.Hidden;
                    btn_Backup.Visibility = Visibility.Hidden;
                    btn_ActTablasEstadisticas.Visibility = Visibility.Hidden;

                    btnUtilidades.Visibility = Visibility.Visible;
                    imgBD.Visibility = Visibility.Visible;
                    imgconf.Visibility = Visibility.Visible;
                    break;
                case 5:
                    btnMesaPart.Visibility = Visibility.Visible;
                    btnMesaPart1.Visibility = Visibility.Visible;
                    imgMesaPart.Visibility = Visibility.Visible;

                    
                    btnEntregaCom.Visibility = Visibility.Hidden;
                    btnEntregaEfec.Visibility = Visibility.Hidden;
                    btnIngresoDev.Visibility = Visibility.Hidden;
                    btnAsigPape.Visibility = Visibility.Hidden;
                    btnIngTalMun.Visibility = Visibility.Hidden;

                    btnBusqueda.Visibility = Visibility.Visible;
                    imgBusqueda.Visibility = Visibility.Visible;

                    btnBusquedaPapeleta.Visibility = Visibility.Hidden;
                    btnBusquedaPlaca.Visibility = Visibility.Hidden;
                    btnBusquedaOtros.Visibility = Visibility.Hidden;

                    btnReportes.Visibility = Visibility.Visible;
                    image_Reportes.Visibility = Visibility.Visible;

                    btnPapeletasIngresadas.Visibility = Visibility.Hidden;
                    btnExportarInfo.Visibility = Visibility.Hidden;
                    btnCuadroMensual.Visibility = Visibility.Hidden;
                    btn_PapeletasPorMes.Visibility = Visibility.Hidden;
                    btn_PapeletasComisaria.Visibility = Visibility.Hidden;
                    btn_PapeletasOficial.Visibility = Visibility.Hidden;

                    btnEstadisticas.Visibility = Visibility.Visible;
                    image.Visibility = Visibility.Visible;

                    btn_MantenimientoTablas.Visibility = Visibility.Visible;
                    btn_ActIndices.Visibility = Visibility.Visible;
                    btn_Backup.Visibility = Visibility.Visible;
                    btn_ActTablasEstadisticas.Visibility = Visibility.Visible;

                    btnUtilidades.Visibility = Visibility.Hidden;
                    imgBD.Visibility = Visibility.Hidden;
                    imgconf.Visibility = Visibility.Hidden;
                    break;
            }

        }
        //---------------------------------BOTONES EN GENERAL------------------------------------
        private void btnMesaPart1_Click(object sender, RoutedEventArgs e)
        {
            ocultarMostrarBotones(1);
        }
        private void btnBusqueda_Click(object sender, RoutedEventArgs e)
        {
            ocultarMostrarBotones(2);
        }
        private void btnReportes_Click(object sender, RoutedEventArgs e)
        {
            ocultarMostrarBotones(3);
        }
        private void btnEstadisticas_Click(object sender, RoutedEventArgs e)
        {
            ocultarMostrarBotones(4);
        }
        private void btnUtilidades_Click(object sender, RoutedEventArgs e)
        {
            ocultarMostrarBotones(5);
        }

        //------------------------------MESA DE PARTES--------------------------------
        private void btnEntregaEfec_Click(object sender, RoutedEventArgs e)
        {
            entregaPapeletasEfectivo entrega = new entregaPapeletasEfectivo(this.scrollContenedor, codUsuario);
            //this.contentContenedor.Content = entrega;
            this.scrollContenedor.Content = entrega;
            entrega.HorizontalAlignment = HorizontalAlignment.Stretch;
            entrega.VerticalAlignment = VerticalAlignment.Stretch;
        }
        private void btnEntregaCom_Click(object sender, RoutedEventArgs e)
        {
            entregaPapeletasComisaria entrega = new entregaPapeletasComisaria(this.scrollContenedor, codUsuario);
            //this.contentContenedor.Content = entrega;
            this.scrollContenedor.Content = entrega;
            entrega.HorizontalAlignment = HorizontalAlignment.Stretch;
            entrega.VerticalAlignment = VerticalAlignment.Stretch;
        }

        private void btnIngresoDev_Click(object sender, RoutedEventArgs e)
        {
            GestionPapeletas gesPap = new GestionPapeletas(this.scrollContenedor);
            this.scrollContenedor.Content = gesPap;
            gesPap.HorizontalAlignment = HorizontalAlignment.Stretch;
            gesPap.VerticalAlignment = VerticalAlignment.Stretch;
        }
        private void btnIngTalMun_Click(object sender, RoutedEventArgs e)
        {
            IngresoTalonario ingTalonario = new IngresoTalonario(this.scrollContenedor,codUsuario);
            this.scrollContenedor.Content = ingTalonario;
            ingTalonario.HorizontalAlignment = HorizontalAlignment.Stretch;
            ingTalonario.VerticalAlignment = VerticalAlignment.Stretch;
        }

        private void btnAsigPape_Click(object sender, RoutedEventArgs e)
        {

        }
        //------------------------------BUSQUEDA--------------------------------------
        private void btnBusquedaPapeleta_Click(object sender, RoutedEventArgs e)
        {
            busquedaPapeletas busquedapapel = new busquedaPapeletas(this.scrollContenedor);
            this.scrollContenedor.Content = busquedapapel;
            busquedapapel.HorizontalAlignment = HorizontalAlignment.Stretch;
            busquedapapel.VerticalAlignment = VerticalAlignment.Stretch;
        }

        private void btnBusquedaPlaca_Click(object sender, RoutedEventArgs e)
        {
            busquedaPapeletas busquedaplaca = new busquedaPapeletas(this.scrollContenedor, true);
            this.scrollContenedor.Content = busquedaplaca;
            busquedaplaca.HorizontalAlignment = HorizontalAlignment.Stretch;
            busquedaplaca.VerticalAlignment = VerticalAlignment.Stretch;
        }

        private void btnBusquedaOtros_Click(object sender, RoutedEventArgs e)
        {
            BusquedaOtros busquedaOtros = new BusquedaOtros(this.scrollContenedor);
            this.scrollContenedor.Content = busquedaOtros;
            busquedaOtros.HorizontalAlignment = HorizontalAlignment.Stretch;
            busquedaOtros.VerticalAlignment = VerticalAlignment.Stretch;
        }
        //-----------------------------REPORTES---------------------------------------
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
        //---------------------------ESTADISTICAS--------------------------------------
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

        private void btn_PapeletasOficial_Click(object sender, RoutedEventArgs e)
        {

        }
        //-------------------------------UTILIDADES------------------------------------
        private void btn_MantenimientoTablas_Click(object sender, RoutedEventArgs e)
        {
            MantenimientoTablas mantTab = new MantenimientoTablas(this.scrollContenedor);
            this.scrollContenedor.Content = mantTab;
            mantTab.HorizontalAlignment = HorizontalAlignment.Stretch;
            mantTab.VerticalAlignment = VerticalAlignment.Stretch;
        }

        private void btnBackup_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_ActIndices_Click(object sender, RoutedEventArgs e)
        {
            ActualizacionIndices tab = new ActualizacionIndices(this.scrollContenedor);
            this.scrollContenedor.Content = tab;
            tab.HorizontalAlignment = HorizontalAlignment.Stretch;
            tab.VerticalAlignment = VerticalAlignment.Stretch;
        }

        private void btn_ActTablasEstadisticas_Click(object sender, RoutedEventArgs e)
        {
            ActualizacionTabEstadisticas tab = new ActualizacionTabEstadisticas(this.scrollContenedor);
            this.scrollContenedor.Content = tab;
            tab.HorizontalAlignment = HorizontalAlignment.Stretch;
            tab.VerticalAlignment = VerticalAlignment.Stretch;
        }
        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        
    }
}
