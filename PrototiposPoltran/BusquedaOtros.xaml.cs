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
using iTextSharp;
using iTextSharp.text;
using System.Data.SqlClient;
using iTextSharp.text.pdf;
using System.IO;
using System.Data;

namespace PrototiposPoltran
{
    /// <summary>
    /// Lógica de interacción para BusquedaOtros.xaml
    /// </summary>
    public partial class BusquedaOtros : UserControl
    {
        resultadosBusquedaOtros resultadoOtros;
        ScrollViewer scrollContenedor;
        List<string> nombres = new List<string>();
        int opc = 0;
        bool flag = false;
        string tituloRep;
        string encabezadoRep;
        string nombreRep;
        public BusquedaOtros(ScrollViewer scrollContenedor)
        {
            InitializeComponent();
            this.lblSugerencias.Visibility = Visibility.Hidden;
            this.txtBusqueda.Visibility = Visibility.Hidden;
            this.scrollContenedor = scrollContenedor;
        }

        public BusquedaOtros(ScrollViewer scrollContenedor, int opc, string cont)
        {
            InitializeComponent();
            this.scrollContenedor = scrollContenedor;
            txtBusqueda.Text = cont;
            this.opc = opc;
            this.lblSugerencias.Visibility = Visibility.Hidden;
            ejecutarBusqueda();
        }

        private List<string> llenarListaNombres()
        {
            List<string> nombres = new List<string>();
            
            Conexion.Open();
            string consulta = "SELECT DISTINCT nombre_efectivo from efectivo";
            SqlCommand cmd = new SqlCommand(consulta, Conexion.sqlConexion);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows == true)
            {
                while (dr.Read())
                    nombres.Add(dr["nombre_efectivo"].ToString());
            }

            dr.Close();
            Conexion.Close();
            txtBusqueda.TextChanged += new TextChangedEventHandler(txtBusqueda_TextChanged);
            if (nombres != null)
                return nombres;
            else
                return null;
        }

        private void txtBusqueda_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (opc == 5 || opc == 4)
            {
                string cadenaEscrita = txtBusqueda.Text;
                List<string> autoList = new List<string>();
                autoList.Clear();

                foreach (string item in nombres)
                {
                    if (!string.IsNullOrEmpty(txtBusqueda.Text))
                        if (item.StartsWith(cadenaEscrita))
                            autoList.Add(item);
                }
                if (autoList.Count > 0)
                {
                    lblSugerencias.ItemsSource = autoList;
                    lblSugerencias.Visibility = Visibility.Visible;
                }
                else if (txtBusqueda.Text.Equals(""))
                {
                    lblSugerencias.Visibility = Visibility.Hidden;
                    lblSugerencias.ItemsSource = null;
                }
                else
                {
                    lblSugerencias.Visibility = Visibility.Hidden;
                    lblSugerencias.ItemsSource = null;
                }
            }


        }

        private void lblSugerencias_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lblSugerencias.ItemsSource != null)
            {
                lblSugerencias.Visibility = Visibility.Collapsed;
                txtBusqueda.TextChanged -= new TextChangedEventHandler(txtBusqueda_TextChanged);
                if (lblSugerencias.SelectedIndex != -1)
                {
                    txtBusqueda.Text = lblSugerencias.SelectedItem.ToString();
                }
                txtBusqueda.TextChanged += new TextChangedEventHandler(txtBusqueda_TextChanged);
            }
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
                    switch (opc)
                    {
                        case 1:
                            break;
                        case 2://------------->Titulo en caso sea busqueda por conductor
                            tituloRep = "Reporte de papeletas por conductor";
                            encabezadoRep = "Reporte de papeletas del conductor con DNI N° ";
                            nombreRep = "reportePapeletasDNICdt_";
                            break;
                        case 3:
                            tituloRep = "Reporte de papeletas por efectivo";
                            encabezadoRep = "Reporte de papeletas de efectivo con CIP N° ";
                            nombreRep = "reportePapeletasEfectivoCIP_";
                            break;
                        case 4:
                            tituloRep = "Reporte de papeletas por efectivo";
                            encabezadoRep = "Reporte de papeletas de efectivo ";
                            nombreRep = "reportePapeletasEfectivo_";
                            break;
                    }
                    DataSet ds = resultadoOtros.devolverDatos();
                    if (ds != null)
                    {
                        BDSistema sis = new BDSistema();
                        String dir = sis.Directorio();
                        if (dir != null)
                        {
                            string ubicacionReporte = @dir + "\\" + nombreRep + txtBusqueda.Text + ".pdf";
                            //Creacion de reporte con iTextSharp
                            Document reporte = new Document(PageSize.A4);
                            PdfWriter writer = PdfWriter.GetInstance(reporte, new FileStream(ubicacionReporte, FileMode.Create));

                            //Añadiendo titulo 
                            reporte.AddTitle(tituloRep);
                            reporte.AddAuthor("Sistema de Gestión de papeletas Poltran v1.1");

                            //Abriendo Archivo
                            reporte.Open();

                            Font _standardFont = new Font(Font.FontFamily.HELVETICA, 8, Font.NORMAL, BaseColor.BLACK);

                            //Encabezado de reporte
                            reporte.Add(new iTextSharp.text.Paragraph(encabezadoRep + " " + txtBusqueda.Text));
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

                            PdfPCell clEstado = new PdfPCell(new Phrase("Estado", _standardFont));
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

                            PdfPCell clIdRelTal = new PdfPCell(new Phrase("ID Talonario", _standardFont));
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

                            //reporte.Add(new iTextSharp.text.Paragraph(tituloRep + txtBusqueda.Text));
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
                        MessageBox.Show("Realice una búsqueda");
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
            try
            {
                int temp;
                if ((opc == 5 || opc == 4 || opc == 3) && txtBusqueda.Text != "") // se verifica que la opcion de ejectivo o cip esta marcada y que el texto de busqueda no sea vacio
                    if (int.TryParse(txtBusqueda.Text, out temp))
                    {
                        opc = 3; //-----------------> opc es 3 porque se eligió usar el codigo cip
                    }
                    else
                    {
                        opc = 4; //-----------------> opc es 4 porque se eligió usar el nombre del efectivo     
                    }
                if (opc >= 1 && opc <= 4)
                    if (txtBusqueda.Text != "")
                    {
                        resultadoOtros = new resultadosBusquedaOtros(this.scrollContenedor, opc, txtBusqueda.Text);
                        flag = true;
                        this.contenedor.Content = resultadoOtros;
                    }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al ejecutar busqueda: " + ex.Message);
            }

        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            lblSugerencias.Visibility = Visibility.Hidden;
            ejecutarBusqueda();

        }

        private void rbtnDelegacion_Click(object sender, RoutedEventArgs e)
        {
            opc = 1; //se eligió busqueda por delegación
            lblBusqueda.Content = "Delegación:";
            lblBusqueda.Visibility = Visibility.Visible;
            txtBusqueda.Visibility = Visibility.Visible;
            txtBusqueda.Text = "";
        }

        private void rbtnConductor_Click(object sender, RoutedEventArgs e)
        {
            opc = 2; //se eligió busqueda por conductor
            lblBusqueda.Content = "DNI de Conductor:";
            lblBusqueda.Visibility = Visibility.Visible;
            txtBusqueda.Visibility = Visibility.Visible;
            txtBusqueda.Text = "";
        }

        private void rbtnEfectivo_Click(object sender, RoutedEventArgs e)
        {
            opc = 5; //se eligió búsqueda por efectivo o CIP
            lblBusqueda.Content = "Nombre o CIP de Efectivo:";
            nombres = llenarListaNombres();
            lblBusqueda.Visibility = Visibility.Visible;
            txtBusqueda.Visibility = Visibility.Visible;
            txtBusqueda.Text = "";
        }


    }
}
