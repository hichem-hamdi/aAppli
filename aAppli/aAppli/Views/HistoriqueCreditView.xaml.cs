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
using aAppli.ViewModels;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Diagnostics;
using aAppli.Models;
using System.Globalization;

namespace aAppli.Views
{
    /// <summary>
    /// Interaction logic for HistoriqueCreditView.xaml
    /// </summary>
    public partial class HistoriqueCreditView : Window
    {
        HistoriqueCreditViewModel vm;
        public HistoriqueCreditView()
        {
            InitializeComponent();
            vm = DataContext as HistoriqueCreditViewModel;
            this.Title += " " + cUser.Login + " / " + cUser.EstablishmentName;
        }

        private void txtNomClient_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtNomClient.Text != null && txtNomClient.Text!="")
                vm.Items = new System.Collections.ObjectModel.ObservableCollection<Models.CreditItemModel>(vm.InitItems.Where(i => i.NomClient != null && i.NomClient.Contains(txtNomClient.Text.ToUpper())));
            else
                vm.Items = new System.Collections.ObjectModel.ObservableCollection<Models.CreditItemModel>(vm.InitItems);
        }

        private void DateDe_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            vm.Items = new System.Collections.ObjectModel.ObservableCollection<Models.CreditItemModel>(vm.InitItems.Where(i => i.DateDeVente.Date>=this.DateDe.SelectedDate.GetValueOrDefault()));
            DateA.SelectedDate = DateDe.SelectedDate;
            vm.Items = new System.Collections.ObjectModel.ObservableCollection<Models.CreditItemModel>(vm.InitItems.Where(i => i.DateDeVente.Date == this.DateDe.SelectedDate.GetValueOrDefault()));
        }

        private void DateA_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            vm.Items = new System.Collections.ObjectModel.ObservableCollection<Models.CreditItemModel>(vm.InitItems.Where(i => i.DateDeVente.Date<=DateA.SelectedDate.GetValueOrDefault() &&  i.DateDeVente.Date>=this.DateDe.SelectedDate.GetValueOrDefault()));
        }

        private void txtSN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                (sender as TextBox).Text = ";" + txtSN.Text;


                string[] tab = txtSN.Text.Split(';');

                vm.Items = new System.Collections.ObjectModel.ObservableCollection<Models.CreditItemModel>(vm.InitItems.Where(i => i.SN.Split(';').Contains(tab[1])));
            }
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            DateA.SelectedDate = null;
            DateDe.SelectedDate = null;
            txtNomClient.Text = default(string);
            txtSN.Text = default(string);
            vm.Items = vm.InitItems;
        }

        private void btnVisualiser_Click(object sender, RoutedEventArgs e)
        {
            vm.Items = new System.Collections.ObjectModel.ObservableCollection<CreditItemModel>(vm.Items.Where(i => i.ParentId == 0).ToList());
            List<CreditItemModel> l = new List<CreditItemModel>();
            foreach (var item in vm.AccItems)
            {
                if (vm.Items.Any(i => i.Id == item.ParentId))
                    l.Add(item);
            }

            Document document = new Document();
            
            string fileName = string.Format(@"RapportsCredit\Rapport{0}.pdf", DateDe.SelectedDate.GetValueOrDefault().ToShortDateString().Replace('/', '-'));
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(fileName, FileMode.Create));
            if (DateDe.SelectedDate != null)
                writer.PageEvent = new pdfPage(DateDe.SelectedDate.GetValueOrDefault(), "Credit De : " + DateDe.SelectedDate.Value.ToShortDateString());
            else
                writer.PageEvent = new pdfPage(DateDe.SelectedDate.GetValueOrDefault(), "Credit De : " + DateTime.Now);
            document.Open();

            var nfi = (NumberFormatInfo)CultureInfo.InvariantCulture.NumberFormat.Clone();
            nfi.NumberGroupSeparator = " ";

            iTextSharp.text.Paragraph p = new iTextSharp.text.Paragraph();
            var totalAchat = vm.Items.Sum(t => t.PrixAchat);
            p.Add(new Phrase("Total achat: " + totalAchat.ToString("#,0.000") + " D.T"));
            p.Alignment = Element.ALIGN_LEFT;
            p.SpacingBefore = 10;
            document.Add(p);

            iTextSharp.text.Paragraph pVente = new iTextSharp.text.Paragraph();
            var totalVente = vm.Items.Sum(t => t.PrixVente);
            pVente.Add(new Phrase("Total vente: " + totalVente.ToString("#,0.000") + " D.T"));
            pVente.Alignment = Element.ALIGN_LEFT;
            pVente.SpacingBefore = 10;
            document.Add(pVente);

            iTextSharp.text.Paragraph pAchatAcc = new iTextSharp.text.Paragraph();
            var totalAchatAcc = l.Sum(t => t.PrixAchat);
            pAchatAcc.Add(new Phrase("Total achat accéssoires: " + totalAchatAcc.ToString("#,0.000") + " D.T"));
            pAchatAcc.Alignment = Element.ALIGN_LEFT;
            pAchatAcc.SpacingBefore = 10;
            document.Add(pAchatAcc);

            iTextSharp.text.Paragraph pBenefices = new iTextSharp.text.Paragraph();
            var benefices = totalVente - totalAchat - totalAchatAcc;
            pBenefices.Add(new Phrase("Bénéfices: " + benefices.ToString("#,0.000") + " D.T"));
            pBenefices.Alignment = Element.ALIGN_LEFT;
            pBenefices.SpacingBefore = 10;
            document.Add(pBenefices);

            PdfPTable table = new PdfPTable(9);
            table.WidthPercentage = 100f;
            float[] intTblWidth = new float[9] { .3f, .5f, .1f, .1f, .1f, .1f, .2f, .2f, .1f };
            table.SetWidths(intTblWidth);


            BaseColor grey = new BaseColor(System.Drawing.Color.Red);
            Font font = FontFactory.GetFont("Arial", 9, Font.NORMAL, grey);

            PdfPCell cell = new PdfPCell(new Phrase(""));

            cell.HorizontalAlignment = 1;
            cell.Colspan = 9;
            cell.Border = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("SN", FontFactory.GetFont("Arial", 9, Font.BOLD)));
            cell.HorizontalAlignment = 1;
            table.AddCell(cell);


            cell = new PdfPCell(new Phrase("Désignation", FontFactory.GetFont("Arial", 9, Font.BOLD)));
            cell.HorizontalAlignment = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Q.T", FontFactory.GetFont("Arial", 9, Font.BOLD)));
            cell.HorizontalAlignment = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Prix Achat", FontFactory.GetFont("Arial", 9, Font.BOLD)));
            cell.HorizontalAlignment = 1;
            table.AddCell(cell);
           

            cell = new PdfPCell(new Phrase("Prix Vente ", FontFactory.GetFont("Arial", 9, Font.BOLD)));
            cell.HorizontalAlignment = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Nom Client", FontFactory.GetFont("Arial", 9, Font.BOLD)));
            cell.HorizontalAlignment = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Coordonnées", FontFactory.GetFont("Arial", 9, Font.BOLD)));
            cell.HorizontalAlignment = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Mode paiement", FontFactory.GetFont("Arial", 9, Font.BOLD)));
            cell.HorizontalAlignment = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Date/Time", FontFactory.GetFont("Arial", 9, Font.BOLD)));
            cell.HorizontalAlignment = 1;
            table.AddCell(cell);
            
            foreach (var item in vm.Items)
            {
                cell = new PdfPCell(new Phrase(item.SN, FontFactory.GetFont("Arial", 7)));
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(item.Designation, FontFactory.GetFont("Arial", 7)));
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(item.Quantite.ToString(), FontFactory.GetFont("Arial", 7)));
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(item.PrixAchat.ToString("0.000"), FontFactory.GetFont("Arial", 7)));
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(item.PrixVente.ToString("0.000"), FontFactory.GetFont("Arial", 7)));
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(item.NomClient, FontFactory.GetFont("Arial", 7)));
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(item.Coordonnees, FontFactory.GetFont("Arial", 7)));
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(item.ModePaiement, FontFactory.GetFont("Arial", 7)));
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(item.DateDeVente.ToString(), FontFactory.GetFont("Arial", 7)));
                table.AddCell(cell);  
            }
            document.Add(table);

            PdfPTable table2 = new PdfPTable(5);
            table2.WidthPercentage = 100f;
            float[] intTblWidth2 = new float[5] { .3f, .5f, .1f, .1f, .2f };
            table2.SetWidths(intTblWidth2);

             grey = new BaseColor(System.Drawing.Color.Red);
             font = FontFactory.GetFont("Arial", 9, Font.NORMAL, grey);

             cell = new PdfPCell(new Phrase(""));

            cell.HorizontalAlignment = 1;
            cell.Colspan = 5;
            cell.Border = 0;
            table2.AddCell(cell);

            cell = new PdfPCell(new Phrase("Client", FontFactory.GetFont("Arial", 9, Font.BOLD)));
            cell.HorizontalAlignment = 1;
            table2.AddCell(cell);


            cell = new PdfPCell(new Phrase("Désignation Accéssoire", FontFactory.GetFont("Arial", 9, Font.BOLD)));
            cell.HorizontalAlignment = 1;
            table2.AddCell(cell);

            cell = new PdfPCell(new Phrase("Prix Achat", FontFactory.GetFont("Arial", 9, Font.BOLD)));
            cell.HorizontalAlignment = 1;
            table2.AddCell(cell);


            cell = new PdfPCell(new Phrase("Prix Vente ", FontFactory.GetFont("Arial", 9, Font.BOLD)));
            cell.HorizontalAlignment = 1;
            table2.AddCell(cell);

            cell = new PdfPCell(new Phrase("SN", FontFactory.GetFont("Arial", 9, Font.BOLD)));
            cell.HorizontalAlignment = 1;
            table2.AddCell(cell);          

            foreach (var item in l)
            {
                cell = new PdfPCell(new Phrase(item.NomClient, FontFactory.GetFont("Arial", 7)));
                table2.AddCell(cell);

                cell = new PdfPCell(new Phrase(item.Designation, FontFactory.GetFont("Arial", 7)));
                table2.AddCell(cell);

                cell = new PdfPCell(new Phrase(item.PrixAchat.ToString("0.000"), FontFactory.GetFont("Arial", 7)));
                table2.AddCell(cell);

                cell = new PdfPCell(new Phrase(item.PrixVente.ToString("0.000"), FontFactory.GetFont("Arial", 7)));
                table2.AddCell(cell);

                cell = new PdfPCell(new Phrase(item.SN, FontFactory.GetFont("Arial", 7)));
                table2.AddCell(cell);
            }


            document.Add(table2);

            document.Close();
            Process.Start(fileName);
        }

        private void btnAccessoires_Click(object sender, RoutedEventArgs e)
        {
            CreditItemModel Article = this.GridItems.SelectedItem as CreditItemModel;
            if (Article == null)
            {
                return;
            }
            CreditViewModel viewModel = new CreditViewModel();
            viewModel.NomClient = Article.NomClient;
            viewModel.Coordonnees = Article.Coordonnees;
            viewModel.ModePaiement = Article.ModePaiement;
            viewModel.IsAccessoire = true;
            viewModel.ParentId = Article.Id;
            CreditView credit = new CreditView(Article);

            credit.Show();
        }
    }

    public static class Extensions
    {
        public static bool IsSuperieur(this DateTime a, DateTime b)
        {
            if (a.Year >= b.Year)
                return true;
            
            if (a.Day >= b.Day && a.Month >= b.Month && a.Year >= b.Year)
                return true;
            return false;
        }

        public static bool IsInferieur(this DateTime a, DateTime b)
        {
            if (a.Day <= b.Day && a.Month <= b.Month && a.Year <= b.Year)
                return true;
            return false;
        }
    }

}
