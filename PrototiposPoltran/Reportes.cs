using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
namespace PrototiposPoltran
{
    public class Reportes
    {
        public void TalonariosEntregados(String cod, int tip)
        {
            BDSistema sis = new BDSistema();
            String dir = sis.Directorio();
            if (dir != null)
            {
                Document doc1 = new Document(PageSize.LETTER);
                PdfWriter writer = PdfWriter.GetInstance(doc1,
                            new FileStream(@dir+"\\TalonarioEntregados.pdf", FileMode.Create));
                doc1.AddTitle("Prueba");
                doc1.AddCreator("POLTRAN");
                doc1.Open();
                iTextSharp.text.Font _standardFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                doc1.Add(new Paragraph("REPORTE DE TALONARIOS ENTREGADOS"));

                PdfPTable tblPrueba = new PdfPTable(3);
                tblPrueba.WidthPercentage = 100;
                PdfPCell clIni = new PdfPCell(new Phrase("Inicio", _standardFont));
                clIni.BorderWidth = 0;
                clIni.BorderWidthBottom = 0.75f;

                PdfPCell clFin = new PdfPCell(new Phrase("fin", _standardFont));
                clFin.BorderWidth = 0;
                clFin.BorderWidthBottom = 0.75f;

                PdfPCell clCant = new PdfPCell(new Phrase("Cantidad", _standardFont));
                clCant.BorderWidth = 0;
                clCant.BorderWidthBottom = 0.75f;

                PdfPCell clDlv = new PdfPCell(new Phrase("Devueltas", _standardFont));
                clDlv.BorderWidth = 0;
                clDlv.BorderWidthBottom = 0.75f;

                PdfPCell clFalta = new PdfPCell(new Phrase("Falta", _standardFont));
                clFalta.BorderWidth = 0;
                clFalta.BorderWidthBottom = 0.75f;

                doc1.Add(tblPrueba);
                doc1.Close();
                writer.Close();
            }
        }

    }
}
