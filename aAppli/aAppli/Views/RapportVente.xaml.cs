using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using System.IO;

using System.Diagnostics;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace aAppli.Views
{
    /// <summary>
    /// Interaction logic for RapportVente.xaml
    /// </summary>
    public partial class RapportVente : Window
    {
        public RapportVente()
        {
            InitializeComponent();
            this.Title += " " + cUser.Login + " / " + cUser.EstablishmentName;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Date.SelectedDate = DateTime.Now.Date;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Document document = new Document();

            if (Date.SelectedDate != null)
            {
                string fileName = string.Format("Rapport{0}.pdf", Date.SelectedDate.GetValueOrDefault().ToShortDateString().Replace('/', '-'));
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(fileName, FileMode.Create));
               // writer.PageEvent = new pdfPage(Date.SelectedDate.GetValueOrDefault());


                document.Open();
                PdfPTable table = new PdfPTable(5);
                table.WidthPercentage = 100f;
                float[] intTblWidth = new float[5] { .3f, .6f, .2f, .2f, .2f };
                table.SetWidths(intTblWidth);


                BaseColor grey = new BaseColor(System.Drawing.Color.Red);
                Font font = FontFactory.GetFont("Arial", 9, Font.NORMAL, grey);

                PdfPCell cell = new PdfPCell(new Phrase(""));

                cell.HorizontalAlignment = 1;
                cell.Colspan = 5;
                cell.Border = 0;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("SN", FontFactory.GetFont("Arial", 9, Font.BOLD)));
                cell.HorizontalAlignment = 1;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Désignation", FontFactory.GetFont("Arial", 9, Font.BOLD)));
                cell.HorizontalAlignment = 1;

                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Prix Achat", FontFactory.GetFont("Arial", 9, Font.BOLD)));
                cell.HorizontalAlignment = 1;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Prix Vente", FontFactory.GetFont("Arial", 9, Font.BOLD)));
                cell.HorizontalAlignment = 1;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Date Vente", FontFactory.GetFont("Arial", 9, Font.BOLD)));
                cell.HorizontalAlignment = 1;
                table.AddCell(cell);

                //DBEntities db = new DBEntities();

                //foreach (var item in db.Ventes)
                //{
                //    if (item.DateDeVente.Date.Equals(Date.SelectedDate))
                //    {
                //        cell = new PdfPCell(new Phrase(item.SN, FontFactory.GetFont("Arial", 7)));
                //        table.AddCell(cell);

                //        cell = new PdfPCell(new Phrase(item.Designation, FontFactory.GetFont("Arial", 7)));
                //        table.AddCell(cell);
                //        cell = new PdfPCell(new Phrase(item.PrixAchat.ToString("0.00"), FontFactory.GetFont("Arial", 7)));
                //        table.AddCell(cell);
                //        cell = new PdfPCell(new Phrase(item.PrixVente.ToString("0.00"), FontFactory.GetFont("Arial", 7)));
                //        table.AddCell(cell);

                //        cell = new PdfPCell(new Phrase(item.DateDeVente.ToShortDateString(), FontFactory.GetFont("Arial", 7)));
                //        table.AddCell(cell);

                //    }
                //}

                document.Add(table);


                document.Close();

                Process.Start(fileName);
            }
        }
    }
}
