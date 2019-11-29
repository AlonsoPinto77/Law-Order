using System;
using System.Collections.Generic;
using System.Data;
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
using iTextSharp;
using iTextSharp.text;
using System.Data.SqlClient;
using iTextSharp.text.pdf;
using System.IO;


namespace PrototiposPoltran
{
    /// <summary>
    /// Lógica de interacción para busquedaPapeletas.xaml
    /// </summary>
    public partial class busquedaPapeletas : UserControl
    {
        resultadosBusquedaPlaca resultadoPl;
        ScrollViewer scrollContenedor;
        bool bsqPlaca;
        bool flag = false;
        string tituloRep;
        public busquedaPapeletas(ScrollViewer scroll)
        {
            InitializeComponent();
            this.bsqPlaca = false;
            this.scrollContenedor = scroll;
        }
        public busquedaPapeletas(ScrollViewer scroll, bool bsqPlaca)
        {
            InitializeComponent();
            this.bsqPlaca = bsqPlaca;
            lblPapeletaPlac.Content = "N° de Placa:";
            this.scrollContenedor = scroll;
        }

        public busquedaPapeletas(ScrollViewer scroll, bool bsqPlaca, string placa)
        {
            InitializeComponent();
            lblPapeletaPlac.Content = "N° de Placa:";
            this.scrollContenedor = scroll;
            this.bsqPlaca = bsqPlaca;
            txtBusqueda.Text = placa;
            ejecutarBusqueda();
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            ejecutarBusqueda();

        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            scrollContenedor.Content = null;

        }

        private void btnImprimir_Click(object sender, RoutedEventArgs e)
        {
            //Creando reporte
            try
            {
                if (flag)
                {
                    DataSet ds = resultadoPl.devolverDatos();
                    if (ds != null)
                    {
                        BDSistema sis = new BDSistema();
                        String dir = sis.Directorio();
                        if (dir != null)
                        {
                            //Ubicación del reporte
                            string ubicacionReporte = @dir + "\\reportePapeletasPlaca_" + txtBusqueda.Text + ".pdf";
                            //Creacion de reporte con iTextSharp
                            Document reporte = new Document(PageSize.LETTER);
                            PdfWriter writer = PdfWriter.GetInstance(reporte, new FileStream(ubicacionReporte, FileMode.Create));

                            //Añadiendo titulo 
                            reporte.AddTitle("Reporte de papeletas por placa " + txtBusqueda.Text);
                            reporte.AddAuthor("Sistema de Gestión de papeletas Poltran v1.1");

                            //Abriendo Archivo
                            reporte.Open();

                            Font _standardFont = new Font(Font.FontFamily.HELVETICA, 8, Font.NORMAL, BaseColor.BLACK);

                            //Encabezado de reporte
                            reporte.Add(new iTextSharp.text.Paragraph("Reporte de papeletas por placa"));
                            reporte.Add(Chunk.NEWLINE);

                            PdfPTable tablaDatosPrincipales = new PdfPTable(11);
                            tablaDatosPrincipales.WidthPercentage = 100;

                            //Configurando titulo de las columnas de datos por fila
                            PdfPCell clNroPapeleta = new PdfPCell(new Phrase("Nro. Papeleta", _standardFont));
                            clNroPapeleta.BorderWidth = 0;
                            clNroPapeleta.BorderWidthBottom = 0.75f;

                            PdfPCell clFalta = new PdfPCell(new Phrase("Falta", _standardFont));
                            clFalta.BorderWidth = 0;
                            clFalta.BorderWidthBottom = 0.75f;

                            PdfPCell clFisico = new PdfPCell(new Phrase("Fisico", _standardFont));
                            clFisico.BorderWidth = 0;
                            clFisico.BorderWidthBottom = 0.75f;

                            PdfPCell clEstado = new PdfPCell(new Phrase("Fisico", _standardFont));
                            clEstado.BorderWidth = 0;
                            clEstado.BorderWidthBottom = 0.75f;

                            PdfPCell clDNI = new PdfPCell(new Phrase("DNI Conductor", _standardFont));
                            clDNI.BorderWidth = 0;
                            clDNI.BorderWidthBottom = 0.75f;

                            PdfPCell clInfraccion = new PdfPCell(new Phrase("Tipo de Infracción", _standardFont));
                            clInfraccion.BorderWidth = 0;
                            clInfraccion.BorderWidthBottom = 0.75f;

                            PdfPCell clPlaca = new PdfPCell(new Phrase("Nro. Placa", _standardFont));
                            clPlaca.BorderWidth = 0;
                            clPlaca.BorderWidthBottom = 0.75f;

                            PdfPCell clFecha = new PdfPCell(new Phrase("Fecha de imposición", _standardFont));
                            clFecha.BorderWidth = 0;
                            clFecha.BorderWidthBottom = 0.75f;

                            PdfPCell clFechaEnvio = new PdfPCell(new Phrase("Fecha de envio", _standardFont));
                            clFechaEnvio.BorderWidth = 0;
                            clFechaEnvio.BorderWidthBottom = 0.75f;

                            PdfPCell clOficio = new PdfPCell(new Phrase("N° de Oficio", _standardFont));
                            clOficio.BorderWidth = 0;
                            clOficio.BorderWidthBottom = 0.75f;

                            PdfPCell clIdRelTal = new PdfPCell(new Phrase("ID Rel. Talonario", _standardFont));
                            clIdRelTal.BorderWidth = 0;
                            clIdRelTal.BorderWidthBottom = 0.75f;

                            tablaDatosPrincipales.AddCell(clNroPapeleta);
                            tablaDatosPrincipales.AddCell(clFalta);
                            tablaDatosPrincipales.AddCell(clFisico);
                            tablaDatosPrincipales.AddCell(clEstado);
                            tablaDatosPrincipales.AddCell(clDNI);
                            tablaDatosPrincipales.AddCell(clInfraccion);
                            tablaDatosPrincipales.AddCell(clPlaca);
                            tablaDatosPrincipales.AddCell(clFecha);
                            tablaDatosPrincipales.AddCell(clFechaEnvio);
                            tablaDatosPrincipales.AddCell(clOficio);
                            tablaDatosPrincipales.AddCell(clIdRelTal);

                            PdfPTable tablaDatosConsulta = new PdfPTable(11);
                            tablaDatosConsulta.WidthPercentage = 100;
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                //Recorriendo la lista y llenando el reporte
                                clNroPapeleta = new PdfPCell(new Phrase(dr["numero_papeleta"].ToString(), _standardFont));

                                clFalta = new PdfPCell(new Phrase(dr["falta"].ToString(), _standardFont));

                                clFisico = new PdfPCell(new Phrase(dr["fisico"].ToString(), _standardFont));

                                clEstado = new PdfPCell(new Phrase(dr["estado"].ToString(), _standardFont));

                                clDNI = new PdfPCell(new Phrase(dr["dni_conductor"].ToString(), _standardFont));

                                clInfraccion = new PdfPCell(new Phrase(dr["tipo_infraccion"].ToString(), _standardFont));

                                clPlaca = new PdfPCell(new Phrase(dr["placa"].ToString(), _standardFont));

                                clFecha = new PdfPCell(new Phrase(dr["fecha_imposicion"].ToString(), _standardFont));

                                clFechaEnvio = new PdfPCell(new Phrase(dr["fecha_envio"].ToString(), _standardFont));

                                clOficio = new PdfPCell(new Phrase(dr["numero_oficio"].ToString(), _standardFont));

                                clIdRelTal = new PdfPCell(new Phrase(dr["id_relTalonario"].ToString(), _standardFont));

                                tablaDatosConsulta.AddCell(clNroPapeleta);
                                tablaDatosConsulta.AddCell(clFalta);
                                tablaDatosConsulta.AddCell(clFisico);
                                tablaDatosConsulta.AddCell(clEstado);
                                tablaDatosConsulta.AddCell(clDNI);
                                tablaDatosConsulta.AddCell(clInfraccion);
                                tablaDatosConsulta.AddCell(clPlaca);
                                tablaDatosConsulta.AddCell(clFecha);
                                tablaDatosConsulta.AddCell(clFechaEnvio);
                                tablaDatosConsulta.AddCell(clOficio);
                                tablaDatosConsulta.AddCell(clIdRelTal);
                            }

                            reporte.Add(new iTextSharp.text.Paragraph("Lista de Papeletas para placa " + txtBusqueda.Text));
                            reporte.Add(tablaDatosPrincipales);
                            reporte.Add(tablaDatosConsulta);
                            reporte.Close();
                            writer.Close();

                            //Confirmacion de creacion de reporte y apertura automatica del reporte en pdf
                            MessageBox.Show("Reporte creado con exito");
                            System.Diagnostics.Process.Start(ubicacionReporte);
                        }
                    }
                    else
                        MessageBox.Show("Realice una búsqueda...");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        private void txtBusqueda_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                ejecutarBusqueda();
        }

        private void ejecutarBusqueda()
        {
            if (bsqPlaca == false)
            {
                if (txtBusqueda.Text != ""  || lblErrBus.Content.ToString() == "")
                {
                    resultadosBusqueda resultado = new resultadosBusqueda(txtBusqueda.Text);
                    this.contenedor.Content = resultado;
                    btnImprimir.Visibility = Visibility.Hidden;
                }
                else
                    MessageBox.Show("Introduzca el numero de papeleta para ejecutar la búsqueda");
            }
            else
            {
                if (txtBusqueda.Text != "" || lblErrBus.Content.ToString() == "")
                {
                    flag = true;
                    resultadoPl = new resultadosBusquedaPlaca(txtBusqueda.Text, this.scrollContenedor);
                    this.contenedor.Content = resultadoPl;
                    //this.btnVer.Visibility = Visibility.Visible;
                }

            }
        }
        private void txtBusqueda_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            if (bsqPlaca == false)
            {
                if (Validacion.IsValidCodPapeleta(txtBusqueda.Text) || (txtBusqueda.Text) == "")
                {
                    lblErrBus.Content = "El Codigo de Papeleta no es Valido";
                }
                else
                {
                    lblErrBus.Content = "";
                }
            }
            else
            {
                if (Validacion.IsValidPlaca(txtBusqueda.Text) || (txtBusqueda.Text) == "")
                {
                    lblErrBus.Content = "La placa no es Valida";
                }
                else
                {
                    lblErrBus.Content = "";
                }
            }
        }

    }
}
