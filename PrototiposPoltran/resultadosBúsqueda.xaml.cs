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
using System.Data;
using System.Data.SqlClient;
using iTextSharp.text.pdf;
using System.IO;


namespace PrototiposPoltran
{
    /// <summary>
    /// Lógica de interacción para resultadosBúsqueda.xaml
    /// </summary>
    public partial class resultadosBusqueda : UserControl
    {

        string nroPapeleta;
        string placa;
        int opc;
        int flag;
        string cont;
        ScrollViewer scrollContenedor;
        public resultadosBusqueda(string nroPapeleta)
        {
            InitializeComponent();
            this.nroPapeleta = nroPapeleta;
            //btnImprimir.Visibility = Visibility.Hidden;
            btnAtras.Visibility = Visibility.Hidden;
            lblAtras.Visibility = Visibility.Hidden;
            this.flag = 0;
            //lblImprimir.Visibility = Visibility.Hidden;
            ejecutarBusqueda();
        }

        public resultadosBusqueda(string nroPapeleta, ScrollViewer scrollContenedor)
        {
            InitializeComponent();
            this.nroPapeleta = nroPapeleta;
            this.scrollContenedor = scrollContenedor;
            this.flag = 1;
            ejecutarBusqueda();
        }
        public resultadosBusqueda(ScrollViewer scrollContenedor, int opc, string cont, string nroPapeleta)
        {
            InitializeComponent();
            this.scrollContenedor = scrollContenedor;
            this.opc = opc;
            this.cont = cont;
            this.nroPapeleta = nroPapeleta;
            this.flag = 2;
            ejecutarBusqueda();
        }



        private void ejecutarBusqueda()
        {
            try
            {
                string consultaGral = "SELECT numero_papeleta,falta,fisico,estado.descripcion AS estado,dni_conductor, tipo_infraccion," +
                                        "placa,fecha_imposicion,fecha_envio,numero_oficio,id_relTalonario FROM papeleta " +
                                        "INNER JOIN estado ON(papeleta.estado = estado.id_estado) WHERE numero_papeleta ='" + nroPapeleta + "'";
                string conductor;
                string talonario;
                string comisaria;
                string efectivo;

                Conexion.Open();
                SqlDataAdapter da = Conexion.ejecutarConsulta(consultaGral);
                if (da != null)
                {
                    //Ejecutando cosulta de papeleta
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    lblEstadoRegistro.Text = dt.Rows[0]["estado"].ToString();
                    lblNumPapeleta.Content = dt.Rows[0]["numero_papeleta"].ToString();
                    lblNumPlaca.Content = dt.Rows[0]["placa"].ToString();
                    lblFecha.Content = dt.Rows[0]["fecha_imposicion"].ToString();
                    lblInfrac.Content = dt.Rows[0]["tipo_infraccion"].ToString();
                    lblNumOfic.Content = dt.Rows[0]["numero_oficio"].ToString();
                    lblFecEnv.Content = dt.Rows[0]["fecha_envio"].ToString();
                    conductor = dt.Rows[0]["dni_conductor"].ToString();
                    talonario = dt.Rows[0]["id_relTalonario"].ToString();
                    placa = dt.Rows[0]["placa"].ToString();
                    string consultaCond = "SELECT dni_conductor, brevete, nombre, ape_pat, ape_mat FROM Conductor WHERE dni_conductor= '" + conductor + "'";
                    //Ejecutando consulta de Conductor ****--------- No hay ni Direccion ni Distrito en la BD
                    da = Conexion.ejecutarConsulta(consultaCond);
                    dt = null;
                    dt = new DataTable();
                    if (da != null)
                    {
                        da.Fill(dt);
                        lblNumLicencia.Content = dt.Rows[0]["brevete"].ToString();
                        string licenciaConductor = dt.Rows[0]["brevete"].ToString();
                        string nombreConductor = dt.Rows[0]["ape_pat"].ToString() + " " + dt.Rows[0]["ape_mat"].ToString() + " " + dt.Rows[0]["nombre"].ToString();
                        lblNombreCdt.Content = dt.Rows[0]["ape_pat"].ToString() + " " + dt.Rows[0]["ape_mat"].ToString() + " " + dt.Rows[0]["nombre"].ToString();
                        lblDNICdt.Content = dt.Rows[0]["dni_conductor"].ToString();

                    }
                    //Ejecutando consulta de talonario
                    string consultaTal = "SELECT cip, id_comisaria FROM RelTalonario WHERE id_relTalonario = '" + talonario + "'";
                    dt = null;
                    dt = new DataTable();
                    da = Conexion.ejecutarConsulta(consultaTal);
                    if (da != null)
                    {
                        da.Fill(dt);
                        efectivo = dt.Rows[0]["cip"].ToString();
                        comisaria = dt.Rows[0]["id_comisaria"].ToString();
                        //lblCodEfec.Content = dt.Rows[0]["cip"].ToString();
                        string consultaEfec = "SELECT nombre_efectivo, cip FROM efectivo WHERE cip= '" + efectivo + "'";
                        string consultaCom = "SELECT nombre_comisaria FROM Comisaria WHERE id_comisaria= '" + comisaria + "'";
                        //Ejecutando consulta de efectivo  
                        dt = null;
                        dt = new DataTable();
                        da = Conexion.ejecutarConsulta(consultaEfec);
                        if (da != null)
                        {
                            da.Fill(dt);
                            lblNomEfec.Content = dt.Rows[0]["nombre_efectivo"].ToString();
                            lblCodEfec.Content = dt.Rows[0]["cip"].ToString();
                        }
                        //Ejecutando consulta de comisaria ****-----------No hay Lugar ni distrito en la bd
                        dt = null;
                        dt = new DataTable();
                        da = Conexion.ejecutarConsulta(consultaCom);
                        if (da != null)
                        {
                            da.Fill(dt);
                            lblCom.Content = dt.Rows[0]["nombre_comisaria"].ToString();
                        }

                    }
                    //Ejecutando consulta de Vehiculo ****------------- No hay ni apellidos nombres y direccion del propietario, ni color, la marca o el año del vehiculo
                    string consultaVeh = "SELECT clase FROM vehiculo WHERE id_placa= '" + placa + "'";
                    dt = null;
                    dt = new DataTable();
                    da = Conexion.ejecutarConsulta(consultaVeh);
                    if (da != null)
                    {
                        da.Fill(dt);
                        string ClaseVeh = dt.Rows[0]["clase"].ToString();
                        lblClaseV.Content = dt.Rows[0]["clase"].ToString();
                    }


                }
                else
                    MessageBox.Show("No existe!!!");

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al Consultar a BD: " + ex.Message);
            }
        }

        private void btnAtras_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                switch (flag)
                {
                    case 0:
                        break;
                    case 1:
                        if (placa != "")
                        {
                            busquedaPapeletas volver = new busquedaPapeletas(this.scrollContenedor, true, placa);
                            this.scrollContenedor.Content = volver;
                        }
                        break;
                    case 2:
                        if (placa != "")
                        {
                            BusquedaOtros volver = new BusquedaOtros(this.scrollContenedor, opc, cont);
                            this.scrollContenedor.Content = volver;
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }


        }

        private void btnImprimir_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BDSistema sis = new BDSistema();
                String dir = sis.Directorio();
                if (dir != null)
                {
                    string ubicacionReporte = @dir + "\\" + nroPapeleta + ".pdf";
                    //Creacion de reporte con iTextSharp
                    Document reporte = new Document(PageSize.LETTER);
                    PdfWriter writer = PdfWriter.GetInstance(reporte, new FileStream(ubicacionReporte, FileMode.Create));

                    //Añadiendo titulo 
                    reporte.AddTitle("Reporte de papeletas por papeleta " + nroPapeleta);
                    reporte.AddAuthor("Sistema de Gestión de papeletas Poltran v1.1");

                    //Abriendo Archivo
                    reporte.Open();

                    Font _standardFont = new Font(Font.FontFamily.HELVETICA, 8, Font.NORMAL, BaseColor.BLACK);

                    //Encabezado de reporte
                    reporte.Add(new iTextSharp.text.Paragraph("Reporte de Papeleta N°" + nroPapeleta));
                    reporte.Add(Chunk.NEWLINE);

                    PdfPTable tablaDatosPrincipales = new PdfPTable(3);
                    tablaDatosPrincipales.WidthPercentage = 100;

                    //Configurando titulo de las columnas de datos básicos
                    PdfPCell clNroPapeleta = new PdfPCell(new Phrase("Nro. Papeleta", _standardFont));
                    clNroPapeleta.BorderWidth = 0;
                    clNroPapeleta.BorderWidthBottom = 0.75f;

                    PdfPCell clPlaca = new PdfPCell(new Phrase("Nro. Placa", _standardFont));
                    clPlaca.BorderWidth = 0;
                    clPlaca.BorderWidthBottom = 0.75f;

                    PdfPCell clFecha = new PdfPCell(new Phrase("Fecha de emisión", _standardFont));
                    clFecha.BorderWidth = 0;
                    clFecha.BorderWidthBottom = 0.75f;

                    tablaDatosPrincipales.AddCell(clNroPapeleta);
                    tablaDatosPrincipales.AddCell(clPlaca);
                    tablaDatosPrincipales.AddCell(clFecha);

                    clNroPapeleta = new PdfPCell(new Phrase(nroPapeleta, _standardFont));
                    clNroPapeleta.BorderWidth = 0;

                    clPlaca = new PdfPCell(new Phrase(lblNumPlaca.Content.ToString(), _standardFont));
                    clPlaca.BorderWidth = 0;

                    clFecha = new PdfPCell(new Phrase(lblFecha.Content.ToString()));
                    clFecha.BorderWidth = 0;

                    tablaDatosPrincipales.AddCell(clNroPapeleta);
                    tablaDatosPrincipales.AddCell(clPlaca);
                    tablaDatosPrincipales.AddCell(clFecha);

                    //Configurando las columnas de datos de conductor

                    PdfPTable tablaConductor = new PdfPTable(3);
                    tablaConductor.WidthPercentage = 100;


                    PdfPCell clLicencia = new PdfPCell(new Phrase("Nro. Brevete", _standardFont));
                    clLicencia.BorderWidth = 0;
                    clLicencia.BorderWidthBottom = 0.75f;

                    PdfPCell clDNI = new PdfPCell(new Phrase("DNI Conductor", _standardFont));
                    clDNI.BorderWidth = 0;
                    clDNI.BorderWidthBottom = 0.75f;

                    PdfPCell clNombre = new PdfPCell(new Phrase("Apellidos y Nombres", _standardFont));
                    clNombre.BorderWidth = 0;
                    clNombre.BorderWidthBottom = 0.75f;

                    tablaConductor.AddCell(clLicencia);
                    tablaConductor.AddCell(clDNI);
                    tablaConductor.AddCell(clNombre);

                    clLicencia = new PdfPCell(new Phrase(lblNumLicencia.Content.ToString(), _standardFont));
                    clLicencia.BorderWidth = 0;

                    clDNI = new PdfPCell(new Phrase(lblDNICdt.Content.ToString(), _standardFont));
                    clPlaca.BorderWidth = 0;

                    clNombre = new PdfPCell(new Phrase(lblNombreCdt.Content.ToString()));
                    clFecha.BorderWidth = 0;

                    tablaConductor.AddCell(clLicencia);
                    tablaConductor.AddCell(clDNI);
                    tablaConductor.AddCell(clNombre);

                    //Configurando las columnas de datos de vehiculo
                    PdfPTable tablaVehiculo = new PdfPTable(1);
                    tablaVehiculo.WidthPercentage = 100;


                    PdfPCell clClase = new PdfPCell(new Phrase("Clase de Vehiculo", _standardFont));
                    clClase.BorderWidth = 0;
                    clClase.BorderWidthBottom = 0.75f;

                    tablaVehiculo.AddCell(clClase);

                    clClase = new PdfPCell(new Phrase(lblClaseV.Content.ToString(), _standardFont));
                    clClase.BorderWidth = 0;

                    tablaVehiculo.AddCell(clClase);

                    //Configurando las columnas de datos de comisaria
                    PdfPTable tablaComisaria = new PdfPTable(3);
                    tablaComisaria.WidthPercentage = 100;


                    PdfPCell clComisaria = new PdfPCell(new Phrase("Comisaría", _standardFont));
                    clComisaria.BorderWidth = 0;
                    clComisaria.BorderWidthBottom = 0.75f;

                    PdfPCell clEfectivo = new PdfPCell(new Phrase("Nombre de Efectivo que intervino", _standardFont));
                    clEfectivo.BorderWidth = 0;
                    clEfectivo.BorderWidthBottom = 0.75f;

                    PdfPCell clCIP = new PdfPCell(new Phrase("CIP de Efectivo", _standardFont));
                    clCIP.BorderWidth = 0;
                    clCIP.BorderWidthBottom = 0.75f;

                    tablaComisaria.AddCell(clComisaria);
                    tablaComisaria.AddCell(clEfectivo);
                    tablaComisaria.AddCell(clCIP);

                    clComisaria = new PdfPCell(new Phrase(lblCom.Content.ToString(), _standardFont));
                    clComisaria.BorderWidth = 0;
                    clComisaria.BorderWidthBottom = 0.75f;

                    clEfectivo = new PdfPCell(new Phrase(lblNomEfec.Content.ToString(), _standardFont));
                    clEfectivo.BorderWidth = 0;
                    clEfectivo.BorderWidthBottom = 0.75f;

                    clCIP = new PdfPCell(new Phrase(lblCodEfec.Content.ToString()));
                    clCIP.BorderWidth = 0;
                    clCIP.BorderWidthBottom = 0.75f;

                    tablaComisaria.AddCell(clComisaria);
                    tablaComisaria.AddCell(clEfectivo);
                    tablaComisaria.AddCell(clCIP);

                    ////Configurando las columnas de datos de infracción
                    PdfPTable tablaInfraccion = new PdfPTable(3);
                    tablaComisaria.WidthPercentage = 100;


                    PdfPCell clOficio = new PdfPCell(new Phrase("N° de Oficio", _standardFont));
                    clOficio.BorderWidth = 0;
                    clOficio.BorderWidthBottom = 0.75f;

                    PdfPCell clInfraccion = new PdfPCell(new Phrase("Tipo de Infracción", _standardFont));
                    clInfraccion.BorderWidth = 0;
                    clInfraccion.BorderWidthBottom = 0.75f;

                    PdfPCell clFechaEnvio = new PdfPCell(new Phrase("Fecha de envio", _standardFont));
                    clFechaEnvio.BorderWidth = 0;
                    clFechaEnvio.BorderWidthBottom = 0.75f;

                    tablaInfraccion.AddCell(clOficio);
                    tablaInfraccion.AddCell(clInfraccion);
                    tablaInfraccion.AddCell(clFechaEnvio);

                    clOficio = new PdfPCell(new Phrase(lblNumOfic.Content.ToString(), _standardFont));
                    clOficio.BorderWidth = 0;

                    clInfraccion = new PdfPCell(new Phrase(lblInfrac.Content.ToString(), _standardFont));
                    clInfraccion.BorderWidth = 0;

                    clFechaEnvio = new PdfPCell(new Phrase(lblFecEnv.Content.ToString()));
                    clFechaEnvio.BorderWidth = 0;

                    tablaInfraccion.AddCell(clOficio);
                    tablaInfraccion.AddCell(clInfraccion);
                    tablaInfraccion.AddCell(clFechaEnvio);


                    reporte.Add(new iTextSharp.text.Paragraph("Datos de Papeleta"));
                    reporte.Add(tablaDatosPrincipales);
                    reporte.Add(new iTextSharp.text.Paragraph("Datos Generales del conductor"));
                    reporte.Add(tablaConductor);
                    reporte.Add(new iTextSharp.text.Paragraph("Caracteristicas del Vehiculo"));
                    reporte.Add(tablaVehiculo);
                    reporte.Add(new iTextSharp.text.Paragraph("Datos de Comisaría que interviene"));
                    reporte.Add(tablaComisaria);
                    reporte.Add(new iTextSharp.text.Paragraph("Datos de Infracción"));
                    reporte.Add(tablaInfraccion);
                    reporte.Close();
                    writer.Close();

                    //Confirmacion de creacion de reporte y apertura automatica del reporte en pdf
                    MessageBox.Show("Reporte creado con exito");
                    System.Diagnostics.Process.Start(ubicacionReporte);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro al crear reporte: " + ex.Message);
            }

        }
    }
}
