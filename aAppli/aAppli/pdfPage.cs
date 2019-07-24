using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace aAppli
{
    public class pdfPage : IPdfPageEvent
    {
        DateTime date;
        string Title;
        public pdfPage(DateTime d,string title)
        {
            date = d;
            Title = title;
        }

        public void OnChapter(PdfWriter writer, Document document, float paragraphPosition, Paragraph title)
        {
            throw new NotImplementedException();
        }

        public void OnChapterEnd(PdfWriter writer, Document document, float paragraphPosition)
        {
            throw new NotImplementedException();
        }

        public void OnCloseDocument(PdfWriter writer, Document document)
        {

        }

        public void OnGenericTag(PdfWriter writer, Document document, Rectangle rect, string text)
        {
            throw new NotImplementedException();
        }

        public void OnOpenDocument(PdfWriter writer, Document document)
        {

        }

        public void OnParagraph(PdfWriter writer, Document document, float paragraphPosition)
        {
            
        }

        public void OnParagraphEnd(PdfWriter writer, Document document, float paragraphPosition)
        {
          
        }

        public void OnSection(PdfWriter writer, Document document, float paragraphPosition, int depth, Paragraph title)
        {
            throw new NotImplementedException();
        }

        public void OnSectionEnd(PdfWriter writer, Document document, float paragraphPosition)
        {
            throw new NotImplementedException();
        }

        public void OnStartPage(PdfWriter writer, Document document)
        {

        }

        PdfContentByte pdfContent;

        public void OnEndPage(PdfWriter writer, Document document)
        {
            //We are going to add two strings in header. Create separate Phrase object with font setting and string to be included
            Phrase p1Header = new Phrase(Title, FontFactory.GetFont("verdana", 7));//new Phrase("Rapport Vente Date : "+date.ToShortDateString(), FontFactory.GetFont("verdana", 8));
            Phrase p2Header = new Phrase("", FontFactory.GetFont("verdana", 7));
            //create iTextSharp.text Image object using local image path
            // iTextSharp.text.Image imgPDF = iTextSharp.text.Image.GetInstance(HttpRuntime.AppDomainAppPath + "\\images\\bluelemoncode.jpg");

            //Create PdfTable object
            PdfPTable pdfTab = new PdfPTable(3);
            //We will have to create separate cells to include image logo and 2 separate strings
            PdfPCell pdfCell1 = new PdfPCell(new Phrase(""));
            PdfPCell pdfCell2 = new PdfPCell(p1Header);
            PdfPCell pdfCell3 = new PdfPCell(p2Header);
            //set the alignment of all three cells and set border to 0
            //pdfCell1.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfCell2.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfCell3.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfCell1.Border = 0;
            pdfCell2.Border = 0;
            pdfCell3.Border = 0;
            //add all three cells into PdfTable
            pdfTab.AddCell(pdfCell1);
            pdfTab.AddCell(pdfCell2);
            pdfTab.AddCell(pdfCell3);
            pdfTab.TotalWidth = document.PageSize.Width - 20;
            //call WriteSelectedRows of PdfTable. This writes rows from PdfWriter in PdfTable
            //first param is start row. -1 indicates there is no end row and all the rows to be included to write
            //Third and fourth param is x and y position to start writing
            pdfTab.WriteSelectedRows(0, -1, 10, document.PageSize.Height - 15, writer.DirectContent);
            //set pdfContent value
            pdfContent = writer.DirectContent;
            //Move the pointer and draw line to separate header section from rest of page
            pdfContent.MoveTo(30, document.PageSize.Height - 35);
            pdfContent.LineTo(document.PageSize.Width - 40, document.PageSize.Height - 35);
            pdfContent.Stroke();

            
            BaseColor grey = new BaseColor(128, 128, 128);
            Font font = FontFactory.GetFont("Arial", 9, Font.NORMAL, grey);
            //tbl footer
            PdfPTable footerTbl = new PdfPTable(1);
            footerTbl.TotalWidth = document.PageSize.Width;



            //page number
            Chunk myFooter = new Chunk("Page " + (document.PageNumber), FontFactory.GetFont(FontFactory.HELVETICA_OBLIQUE, 8, grey));
            PdfPCell footer = new PdfPCell(new Phrase(myFooter));
            footer.Border = Rectangle.NO_BORDER;
            footer.HorizontalAlignment = Element.ALIGN_CENTER;
            footerTbl.AddCell(footer);

            //this is for the position of the footer ... im my case is "+80"
            footerTbl.WriteSelectedRows(0, -1, 0, (document.BottomMargin), writer.DirectContent);
        }
    }
}
