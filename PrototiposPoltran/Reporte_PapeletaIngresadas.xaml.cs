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
using System.Drawing.Printing;
using System.Data;
using Microsoft.Reporting.WinForms;

namespace PrototiposPoltran
{
    /// <summary>
    /// Lógica de interacción para Reporte_PapeletaIngresadas.xaml
    /// </summary>
    public partial class Reporte_PapeletaIngresadas : UserControl
    {
        ScrollViewer scrollContenedor;

        DBAccess db = new DBAccess();
        public Reporte_PapeletaIngresadas(ScrollViewer scroll)
        {

            this.scrollContenedor = scroll;
            InitializeComponent();
            System.Drawing.Printing.PageSettings pg = new System.Drawing.Printing.PageSettings();
            pg.Margins.Top = 0;
            pg.Margins.Bottom = 0;
            pg.Margins.Left = 0;
            pg.Margins.Right = 0;
            System.Drawing.Printing.PaperSize size = new System.Drawing.Printing.PaperSize();
            size.RawKind = (int)PaperKind.B5;
            pg.PaperSize = size;
            DemorReport.SetPageSettings(pg);
            this.DemorReport.RefreshReport();
            DemorReport.Reset();

        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {


        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            //TextBox ka = (TextBox)textBoxOficio;

            string K = textBoxOficio.Text;

            DemorReport.Reset();
            DataTable dt = db.GetPapeletasIngresadasw(K, K);
            ReportDataSource ds1 = new ReportDataSource("DataSet1", dt);
            DemorReport.LocalReport.DataSources.Add(ds1);
            DemorReport.LocalReport.ReportEmbeddedResource = "PrototiposPoltran.Reportes.PapeletasIngresadas.rdlc";
            DemorReport.RefreshReport();

        }
    }
}