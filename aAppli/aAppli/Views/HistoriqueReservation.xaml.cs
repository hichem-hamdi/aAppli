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
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Diagnostics;

namespace aAppli.Views
{
    /// <summary>
    /// Interaction logic for HistoriqueReservation.xaml
    /// </summary>
    public partial class HistoriqueReservation : Window
    {
        public HistoriqueReservation()
        {
            InitializeComponent();

            LoadBooking();
            this.Title += " " + cUser.Login + " / " + cUser.EstablishmentName;

            MyDBEntities db = DbManager.CreateDbManager();
            var est = db.Establishment.ToList();
            est.Insert(0, new Establishment { Id = 0, Name = "-- Select --" });
            cbMagasinCeder.ItemsSource = est;

            cbMagasinCeder.SelectedIndex = 0;
            cbMagasinDemandeur.ItemsSource = est;
            cbMagasinDemandeur.SelectedIndex = 0;

            var users = db.Users.ToList();
            users.Insert(0, new User { Id = 0, Login = "-- Select --" });

            cbValiderPar.ItemsSource = users;
            cbValiderPar.SelectedIndex = 0;
            cbDemanderPar.ItemsSource = users;
            cbDemanderPar.SelectedIndex = 0;
        }

        private void LoadBooking()
        {
            MyDBEntities db = DbManager.CreateDbManager();
            myBooking.ItemsSource = db.Reservation.ToList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var bookings = Filtrer();

            myBooking.ItemsSource = bookings;
        }

        private List<Reservation> Filtrer()
        {
            MyDBEntities db = DbManager.CreateDbManager();
            var bookings = db.Reservation.ToList();
            if ((long)cbMagasinCeder.SelectedValue > 0)
                bookings = db.Reservation.Where(r => r.Establishment1.Id == (long)cbMagasinCeder.SelectedValue).ToList();

            if ((long)cbMagasinDemandeur.SelectedValue > 0)
                bookings = bookings.Where(r => r.Establishment.Id == (long)cbMagasinDemandeur.SelectedValue).ToList();

            if ((long)cbValiderPar.SelectedValue > 0)
                bookings = bookings.Where(r => r.Users.Id == (long)cbValiderPar.SelectedValue).ToList();

            if ((long)cbDemanderPar.SelectedValue > 0)
                bookings = bookings.Where(r => r.Users1.Id == (long)cbDemanderPar.SelectedValue).ToList();

            if (dateDemande.SelectedDate != null)
                bookings = bookings.Where(r => r.RequestDate.Date == dateDemande.SelectedDate.Value.Date).ToList();

            if (dateValidation.SelectedDate != null)
                bookings = bookings.Where(r => r.ValidationDate.HasValue && r.ValidationDate.Value.Date == dateValidation.SelectedDate.Value.Date).ToList();
            return bookings;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Document document = new Document();


            string fileName = string.Format(@"Rapports\RapportReservation{0}.pdf", DateTime.Now.ToShortDateString().Replace('/', '-') + "-" + DateTime.Now.ToLongTimeString().Replace(":", "-"));
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(fileName, FileMode.Create));

            writer.PageEvent = new pdfPage(DateTime.Now, "Rapport Historique Réservation");

            document.Open();


            PdfPTable table = new PdfPTable(8);
            table.WidthPercentage = 100f;
            float[] intTblWidth = new float[8] { .4f, .5f, .1f, .1f, .1f, .1f, .1f, .2f };
            table.SetWidths(intTblWidth);




            BaseColor grey = new BaseColor(System.Drawing.Color.Red);
            Font font = FontFactory.GetFont("Arial", 9, Font.NORMAL, grey);

            PdfPCell cell = new PdfPCell(new Phrase(""));

            cell.HorizontalAlignment = 1;
            cell.Colspan = 8;
            cell.Border = 0;
            table.AddCell(cell);



            cell = new PdfPCell(new Phrase("Désignation", FontFactory.GetFont("Arial", 9, Font.BOLD)));
            cell.HorizontalAlignment = 1;
            table.AddCell(cell);


            cell = new PdfPCell(new Phrase("SN", FontFactory.GetFont("Arial", 9, Font.BOLD)));
            cell.HorizontalAlignment = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Q.T", FontFactory.GetFont("Arial", 9, Font.BOLD)));
            cell.HorizontalAlignment = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Réservé Par", FontFactory.GetFont("Arial", 9, Font.BOLD)));
            cell.HorizontalAlignment = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Date réservation", FontFactory.GetFont("Arial", 9, Font.BOLD)));
            cell.HorizontalAlignment = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Validé?", FontFactory.GetFont("Arial", 9, Font.BOLD)));
            cell.HorizontalAlignment = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Validé par", FontFactory.GetFont("Arial", 9, Font.BOLD)));
            cell.HorizontalAlignment = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Date Validation", FontFactory.GetFont("Arial", 9, Font.BOLD)));
            cell.HorizontalAlignment = 1;
            table.AddCell(cell);

            MyDBEntities db = DbManager.CreateDbManager();

            var Items = Filtrer();

            foreach (var item in Items)
            {
                cell = new PdfPCell(new Phrase(item.Article.Designation, FontFactory.GetFont("Arial", 7)));
                table.AddCell(cell);
                cell = new PdfPCell(new Phrase(item.SN, FontFactory.GetFont("Arial", 7)));
                table.AddCell(cell);
                cell = new PdfPCell(new Phrase(item.Qt.ToString(), FontFactory.GetFont("Arial", 7)));
                table.AddCell(cell);
                cell = new PdfPCell(new Phrase(item.Users1.Login, FontFactory.GetFont("Arial", 7)));
                table.AddCell(cell);
                cell = new PdfPCell(new Phrase(item.RequestDate.ToShortDateString(), FontFactory.GetFont("Arial", 7)));
                table.AddCell(cell);
                if (item.IsValidated == null)
                {
                    cell = new PdfPCell(new Phrase(string.Empty, FontFactory.GetFont("Arial", 7)));
                    table.AddCell(cell);
                }
                else if (item.IsValidated == true)
                {
                    cell = new PdfPCell(new Phrase("Oui", FontFactory.GetFont("Arial", 7)));
                    table.AddCell(cell);
                }
                else
                {
                    cell = new PdfPCell(new Phrase("Non", FontFactory.GetFont("Arial", 7)));
                    table.AddCell(cell);
                }
                cell = new PdfPCell(new Phrase(item.Users.Login, FontFactory.GetFont("Arial", 7)));
                table.AddCell(cell);

                if (item.ValidationDate.HasValue)
                {
                    cell = new PdfPCell(new Phrase(item.ValidationDate.Value.ToShortDateString(), FontFactory.GetFont("Arial", 7)));
                    table.AddCell(cell);
                }
                else
                {
                    cell = new PdfPCell(new Phrase(string.Empty, FontFactory.GetFont("Arial", 7)));
                    table.AddCell(cell);
                }
            }

            if (Items.Count == 0)
            {
                cell = new PdfPCell(new Phrase("Pas d'enregistrements trouvés.", FontFactory.GetFont("Arial", 7)));
                cell.HorizontalAlignment = 1;
                cell.Colspan = 5;
                table.AddCell(cell);
            }

            document.Add(table);
            document.Close();

            Process.Start(fileName);
        }
    }
}
