using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Data;
using System.Windows.Forms;
namespace PrototiposPoltran
{
    public class Reportes
    {
        String[] mes = { "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio",
                "Julio", "Agosto", "Setiembre", "Octubre", "Noviembre", "Diciembre" };

        public void TalonariosEntregados(DataTable dt)
        {            
            BDSistema sis = new BDSistema();
            DataTable data = sis.sistemaDatos();
            String dir = data.Rows[0][1].ToString();
            if (dir != null && dt != null)
            {
                DataRow row1 = dt.Rows[0];
                String cod = row1["cip"].ToString();
                String nomefe = row1["nombre_efectivo"].ToString();
                String codc = row1["id_comisaria"].ToString();
                String nomcom = row1["nombre_comisaria"].ToString();
                String fec = row1["fecha_entrega"].ToString();
                String coa = DateTime.Now.ToString("MM/dd/yyyyHH:mm").Replace("/", "");
                coa = coa.Replace(":", "");
                String nrocnt = data.Rows[0][2].ToString();
                for (int i = 0; i < 4 - nrocnt.Length; i++){
                    nrocnt = "0" + nrocnt;
                }

                int ini = Convert.ToInt32(row1["inicio"].ToString());
                int fin = Convert.ToInt32(row1["fin"].ToString());
                int cant = Convert.ToInt32(row1["cantidad"].ToString());
                //Ubicación del reporte
                string ubicacionReporte = @dir + "\\PapEntregado_" + cod + "_" + nrocnt + "_" + coa + ".pdf";
                //Creacion de reporte con iTextSharp
                Document reporte = new Document(PageSize.LETTER);
                PdfWriter writer = PdfWriter.GetInstance(reporte, new FileStream(ubicacionReporte, FileMode.Create));

                //Abriendo Archivo
                reporte.Open();

                Font _standardFont = new Font(Font.FontFamily.TIMES_ROMAN, 11, Font.NORMAL, BaseColor.BLACK);
                Font _boldstandardFont = new Font(Font.FontFamily.TIMES_ROMAN, 11, Font.BOLD, BaseColor.BLACK);

                //Añadiendo titulo 
                reporte.AddTitle("Reporte de Papeletas Entregadas" + cod + "\n");
                reporte.AddAuthor("Sistema de Gestión de papeletas Poltran v1.1");

                //Encabezado de reporte
                String strTitle = "ACTA DE RECEPCION DE PAPELETAS";
                Font _TitleFont = new Font(Font.FontFamily.TIMES_ROMAN, 12, Font.BOLD, BaseColor.BLACK);
                reporte.Add(new iTextSharp.text.Paragraph("\n"));

                Paragraph title;
                title = new Paragraph(strTitle, _TitleFont);
                title.Alignment = Element.ALIGN_CENTER;
                reporte.Add(title);

                reporte.Add(new iTextSharp.text.Paragraph("\n"));
                Paragraph text;
                text = new Paragraph();
                text.Add(new Chunk("Yo ", _standardFont));
                text.Add(new Chunk(nomefe, _boldstandardFont));
                text.Add(new Chunk(" identificado con el CIP Nro. ", _standardFont));
                text.Add(new Chunk(cod, _boldstandardFont));
                text.Add(new Chunk(" trabajando en la Comisaria PNP ", _standardFont));
                text.Add(new Chunk(nomcom, _boldstandardFont));
                text.Add(new Chunk(" recepciono la cantidad de ", _standardFont));
                text.Add(new Chunk(cant + " ", _boldstandardFont));
                text.Add(new Chunk("cuya numeracion correlativa es del ", _standardFont));
                text.Add(new Chunk(ini + " ", _boldstandardFont));
                text.Add(new Chunk("hasta ", _standardFont));
                text.Add(new Chunk(fin + " ", _boldstandardFont));
                text.Add(new Chunk("para mayor constancia firmo la presente Acta de Recepcion en señal de conformidad ", _standardFont));
                text.Alignment = Element.ALIGN_JUSTIFIED_ALL;
                reporte.Add(text);
                reporte.Add(new iTextSharp.text.Paragraph("\n"));
                Paragraph p1 = new Paragraph();
                String[] seg1 = fec.Split('-');
                String fechacadena = "Arequipa, " + seg1[2] + " de " + mes[(Convert.ToInt32(seg1[1]))-1] + " del " + seg1[0];
                p1.Add(new Chunk(fechacadena, _boldstandardFont));
                reporte.Add(p1);

                reporte.Add(new iTextSharp.text.Paragraph("\n"));
                reporte.Add(new iTextSharp.text.Paragraph("\n"));
                int nrocol = 5;
                PdfPTable tablaDatosPrincipales = new PdfPTable(nrocol);
                tablaDatosPrincipales.WidthPercentage = 80;

                PdfPCell clPapeleta = new PdfPCell(new Phrase("Numero de Papeleta", _standardFont));
                clPapeleta.BorderWidth = 0;
                clPapeleta.BorderWidthBottom = 0.75f;                          
                tablaDatosPrincipales.AddCell(clPapeleta);

                PdfPTable tablaDatosConsulta = new PdfPTable(nrocol);
                tablaDatosConsulta.WidthPercentage = 80;
                
                int cont = ini, relleno=0;
                String text1 = "";
                int temp = cant % 5;
                if (temp != 0)
                {
                    if(temp > 5)
                    {
                        relleno = 10 - temp;
                    }
                    else
                    {
                        relleno = 5 - temp;
                    }
                }
                int total = cant+relleno;
                for(int j=0;j < total; j++)
                {
                    if(cont <= fin)
                    {
                        text1 = "" + cont;
                    }
                    else {
                        text1 = "-";
                    }
                    clPapeleta = new PdfPCell(new Phrase(text1, _standardFont));
                    tablaDatosConsulta.AddCell(clPapeleta);
                    cont++;
                }
                BDOficio ofi = new BDOficio();
                String nroofi = "", titofi = "", desofi = "";
                String anio = data.Rows[0][0].ToString();
                nroofi = nrocnt + anio;
                titofi = strTitle + " DE "+nomefe;
                desofi = "EFECTIVO " +nomefe+ "DE LA COMISARIA " + nomcom + " RANGO DEL " + ini + " AL " +fin+ 
                    " CANTIDAD DE PAPELETAS ES " +cant+ " EN LA FECHA "+fechacadena ;
                if (ofi.IngresarOficio(nroofi, "E", titofi,  desofi, ini.ToString(), fin.ToString(),anio))
                {
                    reporte.Add(tablaDatosPrincipales);
                    reporte.Add(tablaDatosConsulta);
                    reporte.Close();
                    writer.Close();

                    //Confirmacion de creacion de reporte y apertura automatica del reporte en pdf
                    MessageBox.Show("Reporte creado con exito");
                    System.Diagnostics.Process.Start(ubicacionReporte);
                }
                
            }
        }
        public void TalonariosAsignados(DataTable dt)
        {
            BDSistema sis = new BDSistema();
            DataTable data = sis.sistemaDatos();
            String dir = data.Rows[0][1].ToString();
            if (dir != null && dt != null)
            {
                DataRow row1 = dt.Rows[0];
                String cod = row1["cip"].ToString();
                String nomefe = row1["nombre_efectivo"].ToString();
                String codc = row1["id_comisaria"].ToString();
                String nomcom = row1["nombre_comisaria"].ToString();
                String fec = row1["fecha_entrega"].ToString();
                String coa = DateTime.Now.ToString("MM/dd/yyyyHH:mm").Replace("/", "");
                coa = coa.Replace(":", "");
                String nrocnt = data.Rows[0][2].ToString();
                for (int i = 0; i < 4 - nrocnt.Length; i++)
                {
                    nrocnt = "0" + nrocnt;
                }

                int ini = Convert.ToInt32(row1["inicio"].ToString());
                int fin = Convert.ToInt32(row1["fin"].ToString());
                int cant = Convert.ToInt32(row1["cantidad"].ToString());
                //Ubicación del reporte
                string ubicacionReporte = @dir + "\\PapEntregado_" + cod + "_" + nrocnt + "_" + coa + ".pdf";
                //Creacion de reporte con iTextSharp
                Document reporte = new Document(PageSize.LETTER);
                PdfWriter writer = PdfWriter.GetInstance(reporte, new FileStream(ubicacionReporte, FileMode.Create));

                //Abriendo Archivo
                reporte.Open();

                Font _standardFont = new Font(Font.FontFamily.TIMES_ROMAN, 11, Font.NORMAL, BaseColor.BLACK);
                Font _boldstandardFont = new Font(Font.FontFamily.TIMES_ROMAN, 11, Font.BOLD, BaseColor.BLACK);

                //Añadiendo titulo 
                reporte.AddTitle("Reporte de Papeletas Entregadas" + cod + "\n");
                reporte.AddAuthor("Sistema de Gestión de papeletas Poltran v1.1");

                //Encabezado de reporte
                String strTitle = "ACTA DE RECEPCION DE PAPELETAS RE-ASIGNADAS";
                Font _TitleFont = new Font(Font.FontFamily.TIMES_ROMAN, 12, Font.BOLD, BaseColor.BLACK);
                reporte.Add(new iTextSharp.text.Paragraph("\n"));

                Paragraph title;
                title = new Paragraph(strTitle, _TitleFont);
                title.Alignment = Element.ALIGN_CENTER;
                reporte.Add(title);

                reporte.Add(new iTextSharp.text.Paragraph("\n"));
                Paragraph text;
                text = new Paragraph();
                text.Add(new Chunk("Yo ", _standardFont));
                text.Add(new Chunk(nomefe, _boldstandardFont));
                text.Add(new Chunk(" identificado con el CIP Nro. ", _standardFont));
                text.Add(new Chunk(cod, _boldstandardFont));
                text.Add(new Chunk(" trabajando en la Comisaria PNP ", _standardFont));
                text.Add(new Chunk(nomcom, _boldstandardFont));
                text.Add(new Chunk(" recepciono la cantidad de ", _standardFont));
                text.Add(new Chunk(cant + " ", _boldstandardFont));
                text.Add(new Chunk("papeletas reasignadas cuya numeracion correlativa es del ", _standardFont));
                text.Add(new Chunk(ini + " ", _boldstandardFont));
                text.Add(new Chunk("hasta ", _standardFont));
                text.Add(new Chunk(fin + " ", _boldstandardFont));
                text.Add(new Chunk("para mayor constancia firmo la presente Acta de Recepcion en señal de conformidad ", _standardFont));
                text.Alignment = Element.ALIGN_JUSTIFIED_ALL;
                reporte.Add(text);
                reporte.Add(new iTextSharp.text.Paragraph("\n"));
                Paragraph p1 = new Paragraph();
                String[] seg1 = fec.Split('-');
                String fechacadena = "Arequipa, " + seg1[2] + " de " + mes[(Convert.ToInt32(seg1[1])) - 1] + " del " + seg1[0];
                p1.Add(new Chunk(fechacadena, _boldstandardFont));
                reporte.Add(p1);

                reporte.Add(new iTextSharp.text.Paragraph("\n"));
                reporte.Add(new iTextSharp.text.Paragraph("\n"));
                int nrocol = 5;
                PdfPTable tablaDatosPrincipales = new PdfPTable(nrocol);
                tablaDatosPrincipales.WidthPercentage = 80;

                PdfPCell clPapeleta = new PdfPCell(new Phrase("Numero de Papeleta", _standardFont));
                clPapeleta.BorderWidth = 0;
                clPapeleta.BorderWidthBottom = 0.75f;
                tablaDatosPrincipales.AddCell(clPapeleta);

                PdfPTable tablaDatosConsulta = new PdfPTable(nrocol);
                tablaDatosConsulta.WidthPercentage = 80;

                int cont = ini, relleno = 0;
                String text1 = "";
                int temp = cant % 5;
                if (temp != 0)
                {
                    if (temp > 5)
                    {
                        relleno = 10 - temp;
                    }
                    else
                    {
                        relleno = 5 - temp;
                    }
                }
                int total = cant + relleno;
                for (int j = 0; j < total; j++)
                {
                    if (cont <= fin)
                    {
                        text1 = "" + cont;
                    }
                    else
                    {
                        text1 = "-";
                    }
                    clPapeleta = new PdfPCell(new Phrase(text1, _standardFont));
                    tablaDatosConsulta.AddCell(clPapeleta);
                    cont++;
                }
                BDOficio ofi = new BDOficio();
                String nroofi = "", titofi = "", desofi = "";
                String anio = data.Rows[0][0].ToString();
                nroofi = nrocnt + anio;
                titofi = strTitle + " DE " + nomefe;
                desofi = "EFECTIVO " + nomefe + "DE LA COMISARIA " + nomcom + " RANGO DEL " + ini + " AL " + fin +
                    " CANTIDAD DE PAPELETAS ES " + cant + " EN LA FECHA " + fechacadena;
                if (ofi.IngresarOficio(nroofi, "E", titofi, desofi, ini.ToString(), fin.ToString(), anio))
                {
                    reporte.Add(tablaDatosPrincipales);
                    reporte.Add(tablaDatosConsulta);
                    reporte.Close();
                    writer.Close();

                    //Confirmacion de creacion de reporte y apertura automatica del reporte en pdf
                    MessageBox.Show("Reporte creado con exito");
                    System.Diagnostics.Process.Start(ubicacionReporte);
                }

            }
        }
        public void imprimirPapeletasFaltantes(String codComi, String nom, int tip)
        {
            try
            {
                BDTal tal = new BDTal();
                DataTable ds = tal.GetByIdAll(codComi, tip);
                if (ds != null)
                {
                    BDSistema sis = new BDSistema();
                    String dir = sis.Directorio();
                    if (dir != null)
                    {
                        //Ubicación del reporte
                        string ubicacionReporte = @dir + "\\reportePapFaltantes_" + codComi + ".pdf";
                        //Creacion de reporte con iTextSharp
                        Document reporte = new Document(PageSize.LETTER);
                        PdfWriter writer = PdfWriter.GetInstance(reporte, new FileStream(ubicacionReporte, FileMode.Create));

                        //Abriendo Archivo
                        reporte.Open();

                        Font _standardFont = new Font(Font.FontFamily.TIMES_ROMAN, 8, Font.NORMAL, BaseColor.BLACK);

                        //Añadiendo titulo 
                        reporte.AddTitle("Reporte de papeletas faltantes " + codComi);
                        reporte.AddAuthor("Sistema de Gestión de papeletas Poltran v1.1");

                        //Encabezado de reporte
                        reporte.Add(new iTextSharp.text.Paragraph("Reporte de Papeletas Faltantes "));
                        reporte.Add(Chunk.NEWLINE);
                        int nrocol = 6;
                        PdfPTable tablaDatosPrincipales = new PdfPTable(nrocol);
                        tablaDatosPrincipales.WidthPercentage = 80;

                        //Configurando titulo de las columnas de datos por fila
                        PdfPCell clNroPapeleta = new PdfPCell(new Phrase("Nro. Papeleta", _standardFont));
                        clNroPapeleta.BorderWidth = 0;
                        clNroPapeleta.BorderWidthBottom = 0.50f;

                        PdfPCell clFalta = new PdfPCell(new Phrase("Falta", _standardFont));
                        clFalta.BorderWidth = 0;
                        clFalta.BorderWidthBottom = 0.50f;

                        PdfPCell clEstado = new PdfPCell(new Phrase("Estado", _standardFont));
                        clEstado.BorderWidth = 0;
                        clEstado.BorderWidthBottom = 0.50f;

                        PdfPCell clid;
                        PdfPCell clnom;
                        if (tip == 1)
                        {
                            clid = new PdfPCell(new Phrase("Codigo CIP", _standardFont));
                            clid.BorderWidth = 0;
                            clid.BorderWidthBottom = 0.50f;

                            clnom = new PdfPCell(new Phrase("Nombre de Efectivo", _standardFont));
                            clnom.BorderWidth = 0;
                            clnom.BorderWidthBottom = 0.75f;
                        }
                        else
                        {
                            clid = new PdfPCell(new Phrase("Codigo", _standardFont));
                            clid.BorderWidth = 0;
                            clid.BorderWidthBottom = 0.50f;

                            clnom = new PdfPCell(new Phrase("Nombre de Comisaria", _standardFont));
                            clnom.BorderWidth = 0;
                            clnom.BorderWidthBottom = 0.75f;
                        }

                        PdfPCell clfec = new PdfPCell(new Phrase("Fecha de Envio", _standardFont));
                        clfec.BorderWidth = 0;
                        clfec.BorderWidthBottom = 0.50f;

                        tablaDatosPrincipales.AddCell(clNroPapeleta);
                        tablaDatosPrincipales.AddCell(clFalta);
                        tablaDatosPrincipales.AddCell(clEstado);
                        tablaDatosPrincipales.AddCell(clid);
                        tablaDatosPrincipales.AddCell(clnom);
                        tablaDatosPrincipales.AddCell(clfec);

                        PdfPTable tablaDatosConsulta = new PdfPTable(nrocol);
                        tablaDatosConsulta.WidthPercentage = 80;
                        foreach (DataRow dr in ds.Rows)
                        {
                            //Recorriendo la lista y llenando el reporte
                            clNroPapeleta = new PdfPCell(new Phrase(dr["numero_papeleta"].ToString(), _standardFont));

                            String f = (dr["falta"].ToString() == "True") ? "1" : "0";
                            clFalta = new PdfPCell(new Phrase(f, _standardFont));

                            clEstado = new PdfPCell(new Phrase(dr["estado"].ToString(), _standardFont));

                            clid = new PdfPCell(new Phrase(dr["id"].ToString(), _standardFont));

                            clnom = new PdfPCell(new Phrase(dr["nombre"].ToString(), _standardFont));

                            clfec = new PdfPCell(new Phrase(dr["fecha_entrega"].ToString(), _standardFont));

                            tablaDatosConsulta.AddCell(clNroPapeleta);
                            tablaDatosConsulta.AddCell(clFalta);
                            tablaDatosConsulta.AddCell(clEstado);
                            tablaDatosConsulta.AddCell(clid);
                            tablaDatosConsulta.AddCell(clnom);
                            tablaDatosConsulta.AddCell(clfec);
                        }

                        reporte.Add(new iTextSharp.text.Paragraph("Lista de Papeletas Faltantes " + nom));
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
                {
                    String str = (tip == 1) ? " efectivo" : "a comisaria";
                    MessageBox.Show("Seleccione un" + str + "...");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
            

    }
}
