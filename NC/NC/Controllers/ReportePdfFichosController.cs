using NC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace NC.Controllers
{
    //Clase para implentar el pdf
    public class ReportePdfFichosController : Controller
    {        
        //Conexión de la base de datos mediante entity Framework
        NCEntities1 db = new NCEntities1();

        // GET: ReportePdfFichos
        public ActionResult Index()
        {
            return View();
        }
        //Método para la cración del pdf con la utilización de la libreria iTextSharp
        public ActionResult pdf()
        {
            //FileStream fs = new FileStream("c://pdf/reporte.pdf",FileMode.Create);
            //Instancia del objeto de flujos de datos de almacenamiento
            MemoryStream ms = new MemoryStream();

            //Utilizamos la palabra reservada Docuemnt y se crea el objeto document para especificar las caracteristicas del archivo pdf
            Document document = new Document(iTextSharp.text.PageSize.LETTER, 30f, 20f, 50, 40);
            //indicamos que la informacion del archivo se almacenará la info
            PdfWriter pw = PdfWriter.GetInstance(document, ms);
            //variable y ruta de acceso para la ruta de acceso fisica del archivo, en este caso la ubicacion de la imagen
            string pathImage = Server.MapPath("/Content/Images/logo.png");
            pw.PageEvent = new Headerfooter(pathImage);

            // Abrimos el archivo
            document.Open();

            //Personalización de la fuente del arcgivo a través de clases de la liberia

            BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.EMBEDDED);
            Font fontText = new Font(bf, 11, 0, BaseColor.DARK_GRAY);
            //Creación de la tabla en el archivo y su tamaño
            PdfPTable table = new PdfPTable(4);
            table.WidthPercentage = 100f;

            //lista de los usuarios de la tabla usuarios
            List<Tbl_Fichos> fichos = db.Tbl_Fichos.ToList();


            foreach (var item in fichos)
            {

                //creacion de las celdas
                PdfPCell _cell = new PdfPCell();
                //llamado de los datos de la tabla usuarios en el archivo y su posición en la tabla
               
                _cell = new PdfPCell(new Paragraph(item.IdFicho.ToString(), fontText));
                _cell.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(_cell);

                _cell = new PdfPCell(new Paragraph(item.FechaFicho.ToString(), fontText));
                _cell.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(_cell);

                _cell = new PdfPCell(new Paragraph(item.HoraFicho, fontText));
                _cell.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(_cell);

                _cell = new PdfPCell(new Paragraph(item.Tbl_Usuarios.NombreUsuario, fontText));
                _cell.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(_cell);
            }

            //se agrega al documento lo que contiene la tabla
            document.Add(table);
            //cierre del documento
            document.Close();
            //paso de la informacion a bytes para la memorystream

            byte[] bytesStream = ms.ToArray();

            ms = new MemoryStream();
            ms.Write(bytesStream, 0, bytesStream.Length);
            ms.Position = 0;
            // retorno de la instacia y ubicación
            return new FileStreamResult(ms, "application/pdf");

            // return null;
        }
    }
    //clase para personalización del header y footer del archivo
    class Headerfooter : PdfPageEventHelper
    {
        string PathImage = null;
        public Headerfooter(string logoPath)
        {
            PathImage = logoPath;
        }

        //informacion del contenido del footer y header
        public override void OnEndPage(PdfWriter writer, Document document)
        {
            //base.OnEndPage(writer, document);
            PdfPTable tbHeader = new PdfPTable(3);
            tbHeader.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
            tbHeader.DefaultCell.Border = 0;

            tbHeader.AddCell(new Paragraph());

            PdfPCell _cell = new PdfPCell(new Paragraph("Fichos Reservados"));
            _cell.HorizontalAlignment = Element.ALIGN_CENTER;
            _cell.Border = 0;

            tbHeader.AddCell(_cell);
            tbHeader.AddCell(new Paragraph());

            tbHeader.WriteSelectedRows(0, -1, document.LeftMargin, writer.PageSize.GetTop(document.TopMargin) + 40, writer.DirectContent);


            PdfPTable tbFooter = new PdfPTable(3);
            tbFooter.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
            tbFooter.DefaultCell.Border = 0;
            tbFooter.AddCell(new Paragraph());

            _cell = new PdfPCell(new Paragraph("NearCollege, sin duda una buena opción."));
            _cell.HorizontalAlignment = Element.ALIGN_CENTER;
            _cell.Border = 0;

            tbFooter.AddCell(_cell);

            _cell = new PdfPCell(new Paragraph("pagina" + writer.PageNumber));
            _cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            _cell.Border = 0;

            tbFooter.AddCell(_cell);

            tbFooter.WriteSelectedRows(0, -1, document.LeftMargin, writer.PageSize.GetBottom(document.BottomMargin) - 5, writer.DirectContent);

            //caracteristicas de la imagen
            //Begin Image

            Image logo = Image.GetInstance(PathImage);
            logo.SetAbsolutePosition(document.LeftMargin, writer.PageSize.GetTop(document.TopMargin) + 2);

            logo.ScaleAbsolute(50f, 50f);
            document.Add(logo);

            //End Image


        }
    }
}
    

