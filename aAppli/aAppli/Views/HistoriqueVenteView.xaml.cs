using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using aAppli.Enums;
using aAppli.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;


namespace aAppli.Views
{
    /// <summary>
    /// Interaction logic for HistoriqueVenteView.xaml
    /// </summary>
    public partial class HistoriqueVenteView : Window
    {
        decimal totaleGlobaleAchat = 0;
        decimal totaleGlobaleVente = 0;

        public HistoriqueVenteView()
        {
            InitializeComponent();
            this.Date.SelectedDate = DateTime.Now;
            this.Title += " " + cUser.Login + " / " + cUser.EstablishmentName;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Document document = new Document();


            string fileName = string.Format(@"Rapports\Rapport{0}.pdf", Date.SelectedDate.GetValueOrDefault().ToShortDateString().Replace('/', '-') + "-" + DateTime.Now.ToLongTimeString().Replace(":", "-"));
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(fileName, FileMode.Create));
            if (DateDe.SelectedDate != null && DateA.SelectedDate != null)
                writer.PageEvent = new pdfPage(Date.SelectedDate.GetValueOrDefault(), "Rapport Vente De : " + DateDe.SelectedDate.Value.ToShortDateString() + " jusqu'au : " + DateA.SelectedDate.Value.ToShortDateString());
            else if (Date.SelectedDate != null)
                writer.PageEvent = new pdfPage(Date.SelectedDate.GetValueOrDefault(), "Rapport Vente Date : " + Date.SelectedDate.Value.ToShortDateString());

            document.Open();

            if (cUser.EstablishmentName.Equals("Nouvelit", StringComparison.InvariantCultureIgnoreCase))
            {
                MyDBEntities db = DbManager.CreateDbManager();

                foreach (var item in db.Establishment)
                {
                    if (db.Vente.Any(v => v.EstablishmentId == item.Id))
                        Table(document, item.Id, item.Name);
                }

                iTextSharp.text.Paragraph p = new iTextSharp.text.Paragraph();
                p.Add(new Phrase("*********************************************************************"));
                p.Alignment = Element.ALIGN_CENTER;
                p.SpacingBefore = 3;
                document.Add(p);

                p = new iTextSharp.text.Paragraph();
                p.Add(new Phrase(string.Format("Totale Globale Vente = {0} D.T", totaleGlobaleVente.ToString("0.000"))));
                p.Alignment = Element.ALIGN_RIGHT;
                p.SpacingBefore = 23;
                document.Add(p);

                p = new iTextSharp.text.Paragraph();
                p.Add(new Phrase(string.Format("Totale Globale Achat = {0} D.T", totaleGlobaleAchat.ToString("0.000"))));
                p.Alignment = Element.ALIGN_RIGHT;
                p.SpacingBefore = 3;
                document.Add(p);

                p = new iTextSharp.text.Paragraph();
                p.Add(new Phrase(string.Format("Bénéfice Globale = {0} D.T", (totaleGlobaleVente - totaleGlobaleAchat).ToString("0.000"))));
                p.Alignment = Element.ALIGN_RIGHT;
                p.SpacingBefore = 3;
                document.Add(p);
            }
            else
            {
                PdfPTable table = new PdfPTable(8);
                table.WidthPercentage = 100f;
                float[] intTblWidth = new float[8] { .3f, .6f, .1f, .1f, .1f, .1f, .1f, .2f };
                table.SetWidths(intTblWidth);

                BaseColor grey = new BaseColor(System.Drawing.Color.Red);
                Font font = FontFactory.GetFont("Arial", 9, Font.NORMAL, grey);

                PdfPCell cell = new PdfPCell(new Phrase(""));

                cell.HorizontalAlignment = 1;
                cell.Colspan = 8;
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

                cell = new PdfPCell(new Phrase("Prix Achat U", FontFactory.GetFont("Arial", 9, Font.BOLD)));
                cell.HorizontalAlignment = 1;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Prix Achat T", FontFactory.GetFont("Arial", 9, Font.BOLD)));
                cell.HorizontalAlignment = 1;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Prix Vente U", FontFactory.GetFont("Arial", 9, Font.BOLD)));
                cell.HorizontalAlignment = 1;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Prix Vente T", FontFactory.GetFont("Arial", 9, Font.BOLD)));
                cell.HorizontalAlignment = 1;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Date/Time", FontFactory.GetFont("Arial", 9, Font.BOLD)));
                cell.HorizontalAlignment = 1;
                table.AddCell(cell);

                //DBEntities db = new DBEntities();
                List<VenteItemModel> Items = new List<VenteItemModel>();
                MyDBEntities db = DbManager.CreateDbManager();
                decimal totalAchat = 0;
                decimal totalVente = 0;

                foreach (var v in db.Vente.Where(a => a.EstablishmentId == cUser.EstablishmentId || cUser.EstablishmentName.Equals("Nouvelit", StringComparison.InvariantCultureIgnoreCase)).OrderBy(i => i.DateDeVente))
                {
                    VenteItemModel item = new VenteItemModel()
                    {
                        Id = v.ID,
                        ArticleId = v.ArticleId.GetValueOrDefault(),
                        Designation = v.Designation,
                        PrixAchat = v.PrixAchat.GetValueOrDefault(),
                        PrixVente = v.PrixVente.GetValueOrDefault(),
                        SN = v.SN,
                        DateDeVente = v.DateDeVente.GetValueOrDefault(),
                        Quantite = v.QT.GetValueOrDefault()
                    };
                    Items.Add(item);
                }

                List<VenteItemModel> lst = new List<VenteItemModel>();

                foreach (var item in Items)
                {
                    if (DateDe.SelectedDate != null && DateA.SelectedDate != null)
                    {
                        if (item.DateDeVente.Date >= DateDe.SelectedDate && item.DateDeVente.Date <= DateA.SelectedDate)
                        {
                            totalAchat += item.PrixAchat * item.Quantite;
                            totalVente += item.PrixVente * item.Quantite;

                            lst.Add(item);
                            cell = new PdfPCell(new Phrase(item.SN, FontFactory.GetFont("Arial", 7)));
                            table.AddCell(cell);

                            cell = new PdfPCell(new Phrase(item.Designation, FontFactory.GetFont("Arial", 7)));
                            table.AddCell(cell);
                            cell = new PdfPCell(new Phrase(item.Quantite.ToString(), FontFactory.GetFont("Arial", 7)));
                            table.AddCell(cell);
                            cell = new PdfPCell(new Phrase(item.PrixAchat.ToString("0.000"), FontFactory.GetFont("Arial", 7)));
                            table.AddCell(cell);
                            cell = new PdfPCell(new Phrase((item.PrixAchat * item.Quantite).ToString("0.000"), FontFactory.GetFont("Arial", 7)));
                            table.AddCell(cell);
                            cell = new PdfPCell(new Phrase(item.PrixVente.ToString("0.000"), FontFactory.GetFont("Arial", 7)));
                            table.AddCell(cell);
                            cell = new PdfPCell(new Phrase((item.PrixVente * item.Quantite).ToString("0.000"), FontFactory.GetFont("Arial", 7)));
                            table.AddCell(cell);

                            cell = new PdfPCell(new Phrase(item.DateDeVente.ToString(), FontFactory.GetFont("Arial", 7)));
                            table.AddCell(cell);
                        }
                    }
                    else if (Date.SelectedDate != null)
                    {
                        if (item.DateDeVente.Date.Equals(Date.SelectedDate))
                        {
                            totalAchat += item.PrixAchat * item.Quantite;
                            totalVente += item.PrixVente * item.Quantite;

                            lst.Add(item);
                            cell = new PdfPCell(new Phrase(item.SN, FontFactory.GetFont("Arial", 7)));
                            table.AddCell(cell);


                            cell = new PdfPCell(new Phrase(item.Designation, FontFactory.GetFont("Arial", 7)));
                            table.AddCell(cell);
                            cell = new PdfPCell(new Phrase(item.Quantite.ToString(), FontFactory.GetFont("Arial", 7)));
                            table.AddCell(cell);
                            cell = new PdfPCell(new Phrase(item.PrixAchat.ToString("0.000"), FontFactory.GetFont("Arial", 7)));
                            table.AddCell(cell);
                            cell = new PdfPCell(new Phrase((item.PrixAchat * item.Quantite).ToString("0.000"), FontFactory.GetFont("Arial", 7)));
                            table.AddCell(cell);
                            cell = new PdfPCell(new Phrase(item.PrixVente.ToString("0.000"), FontFactory.GetFont("Arial", 7)));
                            table.AddCell(cell);
                            cell = new PdfPCell(new Phrase((item.PrixVente * item.Quantite).ToString("0.000"), FontFactory.GetFont("Arial", 7)));
                            table.AddCell(cell);

                            cell = new PdfPCell(new Phrase(item.DateDeVente.ToString(), FontFactory.GetFont("Arial", 7)));
                            table.AddCell(cell);
                        }
                    }


                }

                if (lst.Count == 0)
                {
                    cell = new PdfPCell(new Phrase("Pas d'enregistrements trouvés.", FontFactory.GetFont("Arial", 7)));
                    cell.HorizontalAlignment = 1;
                    cell.Colspan = 5;
                    table.AddCell(cell);
                }

                document.Add(table);
                iTextSharp.text.Paragraph p = new iTextSharp.text.Paragraph();
                p.Add(new Phrase(string.Format("Total Vente = {0} D.T", totalVente.ToString("0.000"))));
                p.Alignment = Element.ALIGN_RIGHT;
                p.SpacingBefore = 23;
                document.Add(p);

                p = new iTextSharp.text.Paragraph();
                p.Add(new Phrase(string.Format("Total Achat = {0} D.T", totalAchat.ToString("0.000"))));
                p.Alignment = Element.ALIGN_RIGHT;
                p.SpacingBefore = 3;
                document.Add(p);

                p = new iTextSharp.text.Paragraph();
                p.Add(new Phrase(string.Format("Bénéfice = {0} D.T", (totalVente - totalAchat).ToString("0.000"))));
                p.Alignment = Element.ALIGN_RIGHT;
                p.SpacingBefore = 3;
                document.Add(p);
            }

            document.Close();
            Process.Start(fileName);
        }

        private void Table(Document document, long establishmentId, string establishmentName)
        {
            iTextSharp.text.Paragraph p = new iTextSharp.text.Paragraph();
            p.Add(new Phrase("Rapport société : " + establishmentName));
            p.Alignment = Element.ALIGN_LEFT;
            p.SpacingBefore = 23;
            document.Add(p);

            PdfPTable table = new PdfPTable(8);
            table.WidthPercentage = 100f;
            float[] intTblWidth = new float[8] { .3f, .6f, .1f, .1f, .1f, .1f, .1f, .2f };
            table.SetWidths(intTblWidth);

            BaseColor grey = new BaseColor(System.Drawing.Color.Red);
            Font font = FontFactory.GetFont("Arial", 9, Font.NORMAL, grey);

            PdfPCell cell = new PdfPCell(new Phrase(""));

            cell.HorizontalAlignment = 1;
            cell.Colspan = 8;
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

            cell = new PdfPCell(new Phrase("Prix Achat U", FontFactory.GetFont("Arial", 9, Font.BOLD)));
            cell.HorizontalAlignment = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Prix Achat T", FontFactory.GetFont("Arial", 9, Font.BOLD)));
            cell.HorizontalAlignment = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Prix Vente U", FontFactory.GetFont("Arial", 9, Font.BOLD)));
            cell.HorizontalAlignment = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Prix Vente T", FontFactory.GetFont("Arial", 9, Font.BOLD)));
            cell.HorizontalAlignment = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Date/Time", FontFactory.GetFont("Arial", 9, Font.BOLD)));
            cell.HorizontalAlignment = 1;
            table.AddCell(cell);

            //DBEntities db = new DBEntities();
            List<VenteItemModel> Items = new List<VenteItemModel>();
            MyDBEntities db = DbManager.CreateDbManager();
            decimal totalAchat = 0;
            decimal totalVente = 0;

            foreach (var v in db.Vente.Where(a => a.EstablishmentId == establishmentId).OrderBy(i => i.DateDeVente))
            {
                VenteItemModel item = new VenteItemModel()
                {
                    Id = v.ID,
                    ArticleId = v.ArticleId.GetValueOrDefault(),
                    Designation = v.Designation,
                    PrixAchat = v.PrixAchat.GetValueOrDefault(),
                    PrixVente = v.PrixVente.GetValueOrDefault(),
                    SN = v.SN,
                    DateDeVente = v.DateDeVente.GetValueOrDefault(),
                    Quantite = v.QT.GetValueOrDefault()
                };
                Items.Add(item);
            }

            List<VenteItemModel> lst = new List<VenteItemModel>();

            foreach (var item in Items)
            {
                if (DateDe.SelectedDate != null && DateA.SelectedDate != null)
                {
                    if (item.DateDeVente.Date >= DateDe.SelectedDate && item.DateDeVente.Date <= DateA.SelectedDate)
                    {
                        totalAchat += item.PrixAchat * item.Quantite;
                        totalVente += item.PrixVente * item.Quantite;

                        lst.Add(item);
                        cell = new PdfPCell(new Phrase(item.SN, FontFactory.GetFont("Arial", 7)));
                        table.AddCell(cell);

                        cell = new PdfPCell(new Phrase(item.Designation, FontFactory.GetFont("Arial", 7)));
                        table.AddCell(cell);
                        cell = new PdfPCell(new Phrase(item.Quantite.ToString(), FontFactory.GetFont("Arial", 7)));
                        table.AddCell(cell);
                        cell = new PdfPCell(new Phrase(item.PrixAchat.ToString("0.000"), FontFactory.GetFont("Arial", 7)));
                        table.AddCell(cell);
                        cell = new PdfPCell(new Phrase((item.PrixAchat * item.Quantite).ToString("0.000"), FontFactory.GetFont("Arial", 7)));
                        table.AddCell(cell);
                        cell = new PdfPCell(new Phrase(item.PrixVente.ToString("0.000"), FontFactory.GetFont("Arial", 7)));
                        table.AddCell(cell);
                        cell = new PdfPCell(new Phrase((item.PrixVente * item.Quantite).ToString("0.000"), FontFactory.GetFont("Arial", 7)));
                        table.AddCell(cell);

                        cell = new PdfPCell(new Phrase(item.DateDeVente.ToString(), FontFactory.GetFont("Arial", 7)));
                        table.AddCell(cell);
                    }
                }
                else if (Date.SelectedDate != null)
                {
                    if (item.DateDeVente.Date.Equals(Date.SelectedDate))
                    {
                        totalAchat += item.PrixAchat * item.Quantite;
                        totalVente += item.PrixVente * item.Quantite;

                        lst.Add(item);
                        cell = new PdfPCell(new Phrase(item.SN, FontFactory.GetFont("Arial", 7)));
                        table.AddCell(cell);


                        cell = new PdfPCell(new Phrase(item.Designation, FontFactory.GetFont("Arial", 7)));
                        table.AddCell(cell);
                        cell = new PdfPCell(new Phrase(item.Quantite.ToString(), FontFactory.GetFont("Arial", 7)));
                        table.AddCell(cell);
                        cell = new PdfPCell(new Phrase(item.PrixAchat.ToString("0.000"), FontFactory.GetFont("Arial", 7)));
                        table.AddCell(cell);
                        cell = new PdfPCell(new Phrase((item.PrixAchat * item.Quantite).ToString("0.000"), FontFactory.GetFont("Arial", 7)));
                        table.AddCell(cell);
                        cell = new PdfPCell(new Phrase(item.PrixVente.ToString("0.000"), FontFactory.GetFont("Arial", 7)));
                        table.AddCell(cell);
                        cell = new PdfPCell(new Phrase((item.PrixVente * item.Quantite).ToString("0.000"), FontFactory.GetFont("Arial", 7)));
                        table.AddCell(cell);

                        cell = new PdfPCell(new Phrase(item.DateDeVente.ToString(), FontFactory.GetFont("Arial", 7)));
                        table.AddCell(cell);
                    }
                }


            }

            if (lst.Count == 0)
            {
                cell = new PdfPCell(new Phrase("Pas d'enregistrements trouvés.", FontFactory.GetFont("Arial", 7)));
                cell.HorizontalAlignment = 1;
                cell.Colspan = 5;
                table.AddCell(cell);
            }

            document.Add(table);
            p = new iTextSharp.text.Paragraph();
            p.Add(new Phrase(string.Format("Totale Vente = {0} D.T", totalVente.ToString("0.000"))));
            p.Alignment = Element.ALIGN_RIGHT;
            p.SpacingBefore = 23;
            document.Add(p);
            totaleGlobaleVente += totalVente;

            p = new iTextSharp.text.Paragraph();
            p.Add(new Phrase(string.Format("Totale Achat = {0} D.T", totalAchat.ToString("0.000"))));
            p.Alignment = Element.ALIGN_RIGHT;
            p.SpacingBefore = 3;
            document.Add(p);
            totaleGlobaleAchat += totalAchat;

            p = new iTextSharp.text.Paragraph();
            p.Add(new Phrase(string.Format("Bénéfice = {0} D.T", (totalVente - totalAchat).ToString("0.000"))));
            p.Alignment = Element.ALIGN_RIGHT;
            p.SpacingBefore = 3;
            document.Add(p);
        }

        private void Button_Click2(object sender, RoutedEventArgs e)
        {
            Document document = new Document();


            string fileName = string.Format(@"RapportsDetaille\Rapport{0}.pdf", Date.SelectedDate.GetValueOrDefault().ToShortDateString().Replace('/', '-') + "-" + DateTime.Now.ToLongTimeString().Replace(":", "-"));
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(fileName, FileMode.Create));
            if (DateDe.SelectedDate != null && DateA.SelectedDate != null)
                writer.PageEvent = new pdfPage(Date.SelectedDate.GetValueOrDefault(), "Rapport Détaillé De : " + DateDe.SelectedDate.Value.ToShortDateString() + " jusqu'au : " + DateA.SelectedDate.Value.ToShortDateString());
            else if (Date.SelectedDate != null)
                writer.PageEvent = new pdfPage(Date.SelectedDate.GetValueOrDefault(), "Rapport Détaillé Date : " + Date.SelectedDate.Value.ToShortDateString());


            document.Open();
            PdfPTable table = new PdfPTable(8);
            table.WidthPercentage = 100f;
            float[] intTblWidth = new float[8] { .3f, .6f, .1f, .1f, .1f, .1f, .1f, .2f };
            table.SetWidths(intTblWidth);

            BaseColor grey = new BaseColor(System.Drawing.Color.Red);
            Font font = FontFactory.GetFont("Arial", 9, Font.NORMAL, grey);

            PdfPCell cell = new PdfPCell(new Phrase(""));

            cell.HorizontalAlignment = 1;
            cell.Colspan = 8;
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

            cell = new PdfPCell(new Phrase("Prix Achat U", FontFactory.GetFont("Arial", 9, Font.BOLD)));
            cell.HorizontalAlignment = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Prix Achat T", FontFactory.GetFont("Arial", 9, Font.BOLD)));
            cell.HorizontalAlignment = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Prix Vente U", FontFactory.GetFont("Arial", 9, Font.BOLD)));
            cell.HorizontalAlignment = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Prix Vente T", FontFactory.GetFont("Arial", 9, Font.BOLD)));
            cell.HorizontalAlignment = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Date/Time", FontFactory.GetFont("Arial", 9, Font.BOLD)));
            cell.HorizontalAlignment = 1;
            table.AddCell(cell);

            List<VenteItemModel> Items = new List<VenteItemModel>();
            decimal totalAchat = 0;
            decimal totalVente = 0;

            MyDBEntities db = DbManager.CreateDbManager();

            foreach (var v in db.Vente.Where(a => a.EstablishmentId == cUser.EstablishmentId || cUser.EstablishmentName.Equals("Nouvelit", StringComparison.InvariantCultureIgnoreCase)).OrderBy(i => i.Designation))
            {
                VenteItemModel item = new VenteItemModel()
                {
                    Id = v.ID,
                    ArticleId = v.ArticleId.GetValueOrDefault(),
                    Designation = v.Designation,
                    PrixAchat = v.PrixAchat.GetValueOrDefault(),
                    PrixVente = v.PrixVente.GetValueOrDefault(),
                    SN = v.SN,
                    DateDeVente = v.DateDeVente.GetValueOrDefault(),
                    Quantite = v.QT.GetValueOrDefault()
                };
                Items.Add(item);
            }

            List<VenteItemModel> lst = new List<VenteItemModel>();

            foreach (var item in Items)
            {
                if (DateDe.SelectedDate != null && DateA.SelectedDate != null)
                {
                    if (item.DateDeVente.Date >= DateDe.SelectedDate && item.DateDeVente.Date <= DateA.SelectedDate)
                    {
                        totalAchat += item.PrixAchat * item.Quantite;
                        totalVente += item.PrixVente * item.Quantite;

                        lst.Add(item);
                        cell = new PdfPCell(new Phrase(item.SN, FontFactory.GetFont("Arial", 7)));
                        table.AddCell(cell);

                        cell = new PdfPCell(new Phrase(item.Designation, FontFactory.GetFont("Arial", 7)));
                        table.AddCell(cell);
                        cell = new PdfPCell(new Phrase(item.Quantite.ToString(), FontFactory.GetFont("Arial", 7)));
                        table.AddCell(cell);
                        cell = new PdfPCell(new Phrase(item.PrixAchat.ToString("0.000"), FontFactory.GetFont("Arial", 7)));
                        table.AddCell(cell);
                        cell = new PdfPCell(new Phrase((item.PrixAchat * item.Quantite).ToString("0.000"), FontFactory.GetFont("Arial", 7)));
                        table.AddCell(cell);
                        cell = new PdfPCell(new Phrase(item.PrixVente.ToString("0.000"), FontFactory.GetFont("Arial", 7)));
                        table.AddCell(cell);
                        cell = new PdfPCell(new Phrase((item.PrixVente * item.Quantite).ToString("0.000"), FontFactory.GetFont("Arial", 7)));
                        table.AddCell(cell);

                        cell = new PdfPCell(new Phrase(item.DateDeVente.ToString(), FontFactory.GetFont("Arial", 7)));
                        table.AddCell(cell);
                    }
                }

                else if (Date.SelectedDate != null)
                {
                    if (item.DateDeVente.Date.Equals(Date.SelectedDate))
                    {
                        totalAchat += item.PrixAchat * item.Quantite;
                        totalVente += item.PrixVente * item.Quantite;

                        lst.Add(item);
                        cell = new PdfPCell(new Phrase(item.SN, FontFactory.GetFont("Arial", 7)));
                        table.AddCell(cell);

                        cell = new PdfPCell(new Phrase(item.Designation, FontFactory.GetFont("Arial", 7)));
                        table.AddCell(cell);
                        cell = new PdfPCell(new Phrase(item.Quantite.ToString(), FontFactory.GetFont("Arial", 7)));
                        table.AddCell(cell);
                        cell = new PdfPCell(new Phrase(item.PrixAchat.ToString("0.000"), FontFactory.GetFont("Arial", 7)));
                        table.AddCell(cell);
                        cell = new PdfPCell(new Phrase((item.PrixAchat * item.Quantite).ToString("0.000"), FontFactory.GetFont("Arial", 7)));
                        table.AddCell(cell);
                        cell = new PdfPCell(new Phrase(item.PrixVente.ToString("0.000"), FontFactory.GetFont("Arial", 7)));
                        table.AddCell(cell);
                        cell = new PdfPCell(new Phrase((item.PrixVente * item.Quantite).ToString("0.000"), FontFactory.GetFont("Arial", 7)));
                        table.AddCell(cell);
                        cell = new PdfPCell(new Phrase(item.DateDeVente.ToString(), FontFactory.GetFont("Arial", 7)));
                        table.AddCell(cell);
                    }
                }

            }

            if (lst.Count == 0)
            {
                cell = new PdfPCell(new Phrase("Pas d'enregistrements trouvés.", FontFactory.GetFont("Arial", 7)));
                cell.HorizontalAlignment = 1;
                cell.Colspan = 5;
                table.AddCell(cell);
            }

            document.Add(table);
            iTextSharp.text.Paragraph p = new iTextSharp.text.Paragraph();
            p.Add(new Phrase(string.Format("Total Vente = {0} D.T", totalVente.ToString("0.000"))));
            p.Alignment = Element.ALIGN_RIGHT;
            p.SpacingBefore = 23;
            document.Add(p);

            p = new iTextSharp.text.Paragraph();
            p.Add(new Phrase(string.Format("Total Achat = {0} D.T", totalAchat.ToString("0.000"))));
            p.Alignment = Element.ALIGN_RIGHT;
            p.SpacingBefore = 3;
            document.Add(p);

            p = new iTextSharp.text.Paragraph();
            p.Add(new Phrase(string.Format("Bénéfice = {0} D.T", (totalVente - totalAchat).ToString("0.000"))));
            p.Alignment = Element.ALIGN_RIGHT;
            p.SpacingBefore = 3;
            document.Add(p);
            document.Close();

            Process.Start(fileName);

        }

        private void Button_Click3(object sender, RoutedEventArgs e)
        {
            Document document = new Document();


            string fileName = string.Format(@"RapportsQT\Rapport{0}.pdf", Date.SelectedDate.GetValueOrDefault().ToShortDateString().Replace('/', '-') + "-" + DateTime.Now.ToLongTimeString().Replace(":", "-"));
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(fileName, FileMode.Create));
            if (DateDe.SelectedDate != null && DateA.SelectedDate != null)
                writer.PageEvent = new pdfPage(Date.SelectedDate.GetValueOrDefault(), "Rapport Quantitée De : " + DateDe.SelectedDate.Value.ToShortDateString() + " jusqu'au : " + DateA.SelectedDate.Value.ToShortDateString());
            else if (Date.SelectedDate != null)
                writer.PageEvent = new pdfPage(Date.SelectedDate.GetValueOrDefault(), "Rapport Quantitée Date : " + Date.SelectedDate.Value.ToShortDateString());


            document.Open();
            PdfPTable table = new PdfPTable(2);
            table.WidthPercentage = 100f;
            float[] intTblWidth = new float[2] { .7f, .2f };
            table.SetWidths(intTblWidth);

            BaseColor grey = new BaseColor(System.Drawing.Color.Red);
            Font font = FontFactory.GetFont("Arial", 9, Font.NORMAL, grey);

            PdfPCell cell = new PdfPCell(new Phrase(""));

            cell.HorizontalAlignment = 1;
            cell.Colspan = 2;
            cell.Border = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Désignation", FontFactory.GetFont("Arial", 9, Font.BOLD)));
            cell.HorizontalAlignment = 1;

            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Nbr", FontFactory.GetFont("Arial", 9, Font.BOLD)));
            cell.HorizontalAlignment = 1;

            table.AddCell(cell);

            //DBEntities db = new DBEntities();
            List<VenteItemModel> Items = new List<VenteItemModel>();
            MyDBEntities db = DbManager.CreateDbManager();
            foreach (var v in db.Vente.Where(a => a.EstablishmentId == cUser.EstablishmentId || cUser.EstablishmentName.Equals("Nouvelit", StringComparison.InvariantCultureIgnoreCase)).OrderBy(i => i.Designation))
            {
                VenteItemModel item = new VenteItemModel()
                {
                    Id = v.ID,
                    ArticleId = v.ArticleId.GetValueOrDefault(),
                    Designation = v.Designation,
                    PrixAchat = v.PrixAchat.GetValueOrDefault(),
                    PrixVente = v.PrixVente.GetValueOrDefault(),
                    SN = v.SN,
                    DateDeVente = v.DateDeVente.GetValueOrDefault(),
                    Quantite = v.QT.GetValueOrDefault()
                };
                Items.Add(item);
            }
            var list = Items.Where(it => it.DateDeVente.Date.Equals(Date.SelectedDate)).GroupBy(i => i.Designation).ToList();
            if (DateDe.SelectedDate != null && DateA.SelectedDate != null)
            {
                list = Items.Where(it => it.DateDeVente.Date >= DateDe.SelectedDate && it.DateDeVente.Date <= DateA.SelectedDate).GroupBy(i => i.Designation).ToList();
            }

            foreach (var item in list)
            {
                int count = 0;
                if (Date.SelectedDate != null)
                {
                    count = Items.Where(i => i.Designation == item.Key && i.DateDeVente.Date.Equals(Date.SelectedDate)).Select(ii => ii.Quantite).Sum();
                }
                if (DateDe.SelectedDate != null && DateA.SelectedDate != null)
                {
                    count = Items.Where(i => i.Designation == item.Key && i.DateDeVente.Date >= DateDe.SelectedDate && i.DateDeVente.Date <= DateA.SelectedDate).Select(ii => ii.Quantite).Sum();
                }
                cell = new PdfPCell(new Phrase(item.Key, FontFactory.GetFont("Arial", 7)));
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(count.ToString(), FontFactory.GetFont("Arial", 7)));
                table.AddCell(cell);
            }

            if (list.Count == 0)
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Date.SelectedDate = DateTime.Now.Date;
        }

        private void Button_Click4(object sender, RoutedEventArgs e)
        {
            Document document = new Document();


            string fileName = string.Format(@"Rapports\RapportStock.pdf", Date.SelectedDate.GetValueOrDefault().ToShortDateString().Replace('/', '-') + "-" + DateTime.Now.ToLongTimeString().Replace(":", "-"));
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(fileName, FileMode.Create));
            if (DateDe.SelectedDate != null && DateA.SelectedDate != null)
                writer.PageEvent = new pdfPage(Date.SelectedDate.GetValueOrDefault(), "Rapport Stock De : " + DateDe.SelectedDate.Value.ToShortDateString() + " jusqu'au : " + DateA.SelectedDate.Value.ToShortDateString());
            else if (Date.SelectedDate != null)
                writer.PageEvent = new pdfPage(Date.SelectedDate.GetValueOrDefault(), "Rapport Stock Date : " + Date.SelectedDate.Value.ToShortDateString());


            document.Open(); PdfPTable table;
            if (this.WithSn.IsChecked.GetValueOrDefault())
            {
                table = new PdfPTable(5);
                table.WidthPercentage = 100f;
                float[] intTblWidth = new float[5] { .3f, .6f, .2f, .2f, .2f };
                table.SetWidths(intTblWidth);
            }
            else
            {
                table = new PdfPTable(4);
                table.WidthPercentage = 100f;
                float[] intTblWidth = new float[4] { .6f, .2f, .2f, .2f };
                table.SetWidths(intTblWidth);
            }

            BaseColor grey = new BaseColor(System.Drawing.Color.Red);
            Font font = FontFactory.GetFont("Arial", 9, Font.NORMAL, grey);

            PdfPCell cell = new PdfPCell(new Phrase(""));

            cell.HorizontalAlignment = 1;
            cell.Colspan = 5;
            cell.Border = 0;
            table.AddCell(cell);

            if (this.WithSn.IsChecked.GetValueOrDefault())
            {
                cell = new PdfPCell(new Phrase("SN", FontFactory.GetFont("Arial", 9, Font.BOLD)));
                cell.HorizontalAlignment = 1;
                table.AddCell(cell);
            }

            cell = new PdfPCell(new Phrase("Désignation", FontFactory.GetFont("Arial", 9, Font.BOLD)));
            cell.HorizontalAlignment = 1;

            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Prix Achat", FontFactory.GetFont("Arial", 9, Font.BOLD)));
            cell.HorizontalAlignment = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Prix Vente", FontFactory.GetFont("Arial", 9, Font.BOLD)));
            cell.HorizontalAlignment = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Q.T", FontFactory.GetFont("Arial", 9, Font.BOLD)));
            cell.HorizontalAlignment = 1;
            table.AddCell(cell);

            //DBEntities db = new DBEntities();
            List<VenteItemModel> Items = new List<VenteItemModel>();
            MyDBEntities db = DbManager.CreateDbManager();
            foreach (var v in db.Article.Where(a => a.EstablishmentId == cUser.EstablishmentId || cUser.EstablishmentName.Equals("Nouvelit", StringComparison.InvariantCultureIgnoreCase)).OrderBy(i => i.Designation))
            {
                VenteItemModel item = new VenteItemModel()
                {
                    Id = v.ID,
                    Designation = v.Designation,
                    PrixAchat = v.PrixAchat,
                    PrixVente = v.PrixVente,
                    SN = v.SN,
                    Quantite = v.QT
                };
                Items.Add(item);
            }

            if (this.RapportType.SelectedIndex == 1)
            {
                Items = Items.Where(item => item.Quantite > 0).ToList();
            }
            else if (this.RapportType.SelectedIndex == 2)
            {
                Items = Items.Where(item => item.Quantite == 0).ToList();
            }


            List<VenteItemModel> lst = new List<VenteItemModel>();

            foreach (var item in Items)
            {
                //totalAchat += item.PrixAchat;
                //totalVente += item.PrixVente;

                lst.Add(item);
                if (this.WithSn.IsChecked.GetValueOrDefault())
                {
                    cell = new PdfPCell(new Phrase(item.SN, FontFactory.GetFont("Arial", 7)));
                    table.AddCell(cell);
                }

                cell = new PdfPCell(new Phrase(item.Designation, FontFactory.GetFont("Arial", 7)));
                table.AddCell(cell);
                cell = new PdfPCell(new Phrase(item.PrixAchat.ToString("0.000"), FontFactory.GetFont("Arial", 7)));
                table.AddCell(cell);
                cell = new PdfPCell(new Phrase(item.PrixVente.ToString("0.000"), FontFactory.GetFont("Arial", 7)));
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(item.Quantite.ToString(), FontFactory.GetFont("Arial", 7)));
                table.AddCell(cell);

            }

            if (lst.Count == 0)
            {
                cell = new PdfPCell(new Phrase("Pas d'enregistrements trouvés.", FontFactory.GetFont("Arial", 7)));
                cell.HorizontalAlignment = 1;
                cell.Colspan = 5;
                table.AddCell(cell);
            }

            document.Add(table);
            //iTextSharp.text.Paragraph p = new iTextSharp.text.Paragraph();
            //p.Add(new Phrase(string.Format("Total Vente = {0} D.T", totalVente.ToString("0.000"))));
            //p.Alignment = Element.ALIGN_RIGHT;
            //p.SpacingBefore = 23;
            //document.Add(p);

            //p = new iTextSharp.text.Paragraph();
            //p.Add(new Phrase(string.Format("Total Achat = {0} D.T", totalAchat.ToString("0.000"))));
            //p.Alignment = Element.ALIGN_RIGHT;
            //p.SpacingBefore = 3;
            //document.Add(p);

            //p = new iTextSharp.text.Paragraph();
            //p.Add(new Phrase(string.Format("Bénéfice = {0} D.T", (totalVente - totalAchat).ToString("0.000"))));
            //p.Alignment = Element.ALIGN_RIGHT;
            //p.SpacingBefore = 3;
            //document.Add(p);
            document.Close();

            Process.Start(fileName);

        }

        private void BtnFiltreClick(object sender, RoutedEventArgs e)
        {
            MyDBEntities db = DbManager.CreateDbManager();

            List<FiltreItem> lst = new List<FiltreItem>();
            foreach (var v in db.Vente)
            {
                FiltreItem item = new FiltreItem()
                {
                    Designation = v.Designation,
                    PrixAchat = v.PrixAchat.GetValueOrDefault(),
                    PrixVente = v.PrixVente.GetValueOrDefault(),
                    DateVente = v.DateDeVente.GetValueOrDefault(),
                    Qt = v.QT.GetValueOrDefault()
                };

                lst.Add(item);
            }

            lst = FiltreItems(lst);

            Document document = new Document();

            string fileName = string.Format(@"RapportsFinancier\Rapport{0}.pdf", Date.SelectedDate.GetValueOrDefault().ToShortDateString().Replace('/', '-') + "-" + DateTime.Now.ToLongTimeString().Replace(":", "-"));
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(fileName, FileMode.Create));
            if (DateDe.SelectedDate != null && DateA.SelectedDate != null)
                writer.PageEvent = new pdfPage(Date.SelectedDate.GetValueOrDefault(), "Rapport Financier De : " + DateDe.SelectedDate.Value.ToShortDateString() + " jusqu'au : " + DateA.SelectedDate.Value.ToShortDateString());
            else if (Date.SelectedDate != null)
                writer.PageEvent = new pdfPage(Date.SelectedDate.GetValueOrDefault(), "Rapport Financier Date : " + Date.SelectedDate.Value.ToShortDateString());

            document.Open();
            document.Add(AddParagraph(13));
            document.Add(CreateBeneficeTable(lst));
            document.Add(AddParagraph(80));
            document.Add(CreateMargeTable(lst));
            document.Add(AddParagraph(80));
            document.Add(CreateTotalAchatTable(lst));
            document.Add(AddParagraph(3));
            document.Add(CreateTotalVenteTable(lst));
            document.Add(AddParagraph(3));
            document.Add(CreateTotalBeneficeTable(lst));

            document.Close();
            Process.Start(fileName);
        }

        private List<FiltreItem> FiltreItems(List<FiltreItem> lst)
        {
            List<FiltreItem> lstFiltred = new List<FiltreItem>();

            foreach (var item in lst)
            {
                if (item.Designation.StartsWith("Maintenance", StringComparison.InvariantCultureIgnoreCase))
                    item.Famille = ItemFamille.Maintenance;
                else if (item.Designation.StartsWith("Notebook", StringComparison.InvariantCultureIgnoreCase))
                    item.Famille = ItemFamille.Notebook;
                else if (item.Designation.StartsWith("Pc", StringComparison.InvariantCultureIgnoreCase))
                    item.Famille = ItemFamille.Pc;
                else if (item.Designation.StartsWith("Internet", StringComparison.InvariantCultureIgnoreCase))
                    item.Famille = ItemFamille.Internet;
                else
                    item.Famille = ItemFamille.Accessoire;

                if (DateDe.SelectedDate != null && DateA.SelectedDate != null)
                {
                    if (item.DateVente.Date >= DateDe.SelectedDate && item.DateVente.Date <= DateA.SelectedDate)
                    {
                        lstFiltred.Add(item);
                    }
                }
                else if (Date.SelectedDate != null)
                {
                    if (item.DateVente.Date.Equals(Date.SelectedDate))
                    {
                        lstFiltred.Add(item);
                    }
                }
            }

            return lstFiltred;
        }

        private PdfPTable CreateBeneficeTable(List<FiltreItem> lst)
        {
            PdfPTable table = new PdfPTable(5);
            table.WidthPercentage = 100f;
            float[] intTblWidth = new float[5] { .2f, .2f, .2f, .2f, .2f };
            table.SetWidths(intTblWidth);

            BaseColor grey = new BaseColor(System.Drawing.Color.Red);
            Font font = FontFactory.GetFont("Arial", 9, Font.NORMAL, grey);

            PdfPCell cell = new PdfPCell(new Phrase(""));

            cell.HorizontalAlignment = 1;
            cell.Colspan = 5;
            cell.Border = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Benefice Accessoire", FontFactory.GetFont("Arial", 9, Font.BOLD)));
            cell.HorizontalAlignment = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Benefice Maintenance", FontFactory.GetFont("Arial", 9, Font.BOLD)));
            cell.HorizontalAlignment = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Benefice Notebook", FontFactory.GetFont("Arial", 9, Font.BOLD)));
            cell.HorizontalAlignment = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Benefice PC", FontFactory.GetFont("Arial", 9, Font.BOLD)));
            cell.HorizontalAlignment = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Benefice Internet", FontFactory.GetFont("Arial", 9, Font.BOLD)));
            cell.HorizontalAlignment = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(CalculateBenefice(lst, ItemFamille.Accessoire).ToString("0.000"), FontFactory.GetFont("Arial", 7)));
            table.AddCell(cell);
            cell = new PdfPCell(new Phrase(CalculateBenefice(lst, ItemFamille.Maintenance).ToString("0.000"), FontFactory.GetFont("Arial", 7)));
            table.AddCell(cell);
            cell = new PdfPCell(new Phrase(CalculateBenefice(lst, ItemFamille.Notebook).ToString("0.000"), FontFactory.GetFont("Arial", 7)));
            table.AddCell(cell);
            cell = new PdfPCell(new Phrase(CalculateBenefice(lst, ItemFamille.Pc).ToString("0.000"), FontFactory.GetFont("Arial", 7)));
            table.AddCell(cell);
            cell = new PdfPCell(new Phrase(CalculateBenefice(lst, ItemFamille.Internet).ToString("0.000"), FontFactory.GetFont("Arial", 7)));
            table.AddCell(cell);
            return table;
        }

        private Decimal CalculateBenefice(List<FiltreItem> lst, ItemFamille famille)
        {
            decimal totAchat = 0;
            decimal totVente = 0;

            totAchat = lst.Where(i => i.Famille == famille).Sum(i => i.PrixAchat * i.Qt);
            totVente = lst.Where(i => i.Famille == famille).Sum(i => i.PrixVente * i.Qt);

            return totVente - totAchat;
        }

        private iTextSharp.text.Paragraph AddParagraph(int spacing)
        {
            var p = new iTextSharp.text.Paragraph();
            p.Add(new Phrase(""));
            p.Alignment = Element.ALIGN_RIGHT;
            p.SpacingBefore = spacing;

            return p;
        }

        private PdfPTable CreateMargeTable(List<FiltreItem> lst)
        {
            PdfPTable table = new PdfPTable(6);
            table.WidthPercentage = 100f;
            float[] intTblWidth = new float[6] { .2f, .2f, .2f, .2f, .2f, .2f };
            table.SetWidths(intTblWidth);

            BaseColor grey = new BaseColor(System.Drawing.Color.Red);
            Font font = FontFactory.GetFont("Arial", 9, Font.NORMAL, grey);

            PdfPCell cell = new PdfPCell(new Phrase(""));

            cell.HorizontalAlignment = 1;
            cell.Colspan = 6;
            cell.Border = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Famille", FontFactory.GetFont("Arial", 9, Font.BOLD)));
            cell.HorizontalAlignment = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Nombre Article", FontFactory.GetFont("Arial", 9, Font.BOLD)));
            cell.HorizontalAlignment = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Total prix Achat", FontFactory.GetFont("Arial", 9, Font.BOLD)));
            cell.HorizontalAlignment = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Total prix Vente", FontFactory.GetFont("Arial", 9, Font.BOLD)));
            cell.HorizontalAlignment = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Benefice", FontFactory.GetFont("Arial", 9, Font.BOLD)));
            cell.HorizontalAlignment = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Marge %", FontFactory.GetFont("Arial", 9, Font.BOLD)));
            cell.HorizontalAlignment = 1;
            table.AddCell(cell);
            //Accessoire
            cell = new PdfPCell(new Phrase("Accessoire", FontFactory.GetFont("Arial", 7, Font.BOLD)));
            table.AddCell(cell);
            cell = new PdfPCell(new Phrase(lst.Where(i => i.Famille == ItemFamille.Accessoire).Sum(i => i.Qt).ToString(), FontFactory.GetFont("Arial", 7)));
            table.AddCell(cell);
            cell = new PdfPCell(new Phrase(lst.Where(i => i.Famille == ItemFamille.Accessoire).Sum(i => i.PrixAchat * i.Qt).ToString("0.000"), FontFactory.GetFont("Arial", 7)));
            table.AddCell(cell);
            cell = new PdfPCell(new Phrase(lst.Where(i => i.Famille == ItemFamille.Accessoire).Sum(i => i.PrixVente * i.Qt).ToString("0.000"), FontFactory.GetFont("Arial", 7)));
            table.AddCell(cell);
            cell = new PdfPCell(new Phrase(CalculateBenefice(lst, ItemFamille.Accessoire).ToString("0.000"), FontFactory.GetFont("Arial", 7)));
            table.AddCell(cell);
            cell = new PdfPCell(new Phrase(
                (CalculateMarge(lst, ItemFamille.Accessoire)).ToString("0.000"), FontFactory.GetFont("Arial", 7)));
            table.AddCell(cell);

            //Maintenance
            cell = new PdfPCell(new Phrase("Maintenance", FontFactory.GetFont("Arial", 7, Font.BOLD)));
            table.AddCell(cell);
            cell = new PdfPCell(new Phrase(lst.Where(i => i.Famille == ItemFamille.Maintenance).Sum(i => i.Qt).ToString(), FontFactory.GetFont("Arial", 7)));
            table.AddCell(cell);
            cell = new PdfPCell(new Phrase(lst.Where(i => i.Famille == ItemFamille.Maintenance).Sum(i => i.PrixAchat * i.Qt).ToString("0.000"), FontFactory.GetFont("Arial", 7)));
            table.AddCell(cell);
            cell = new PdfPCell(new Phrase(lst.Where(i => i.Famille == ItemFamille.Maintenance).Sum(i => i.PrixVente * i.Qt).ToString("0.000"), FontFactory.GetFont("Arial", 7)));
            table.AddCell(cell);
            cell = new PdfPCell(new Phrase(CalculateBenefice(lst, ItemFamille.Maintenance).ToString("0.000"), FontFactory.GetFont("Arial", 7)));
            table.AddCell(cell);
            cell = new PdfPCell(
                new Phrase(
                    (CalculateMarge(lst, ItemFamille.Maintenance)).ToString("0.000"), FontFactory.GetFont("Arial", 7)));
            table.AddCell(cell);

            //Notebook
            cell = new PdfPCell(new Phrase("Notebook", FontFactory.GetFont("Arial", 7, Font.BOLD)));
            table.AddCell(cell);
            cell = new PdfPCell(new Phrase(lst.Where(i => i.Famille == ItemFamille.Notebook).Sum(i => i.Qt).ToString(), FontFactory.GetFont("Arial", 7)));
            table.AddCell(cell);
            cell = new PdfPCell(new Phrase(lst.Where(i => i.Famille == ItemFamille.Notebook).Sum(i => i.PrixAchat * i.Qt).ToString("0.000"), FontFactory.GetFont("Arial", 7)));
            table.AddCell(cell);
            cell = new PdfPCell(new Phrase(lst.Where(i => i.Famille == ItemFamille.Notebook).Sum(i => i.PrixVente * i.Qt).ToString("0.000"), FontFactory.GetFont("Arial", 7)));
            table.AddCell(cell);
            cell = new PdfPCell(new Phrase(CalculateBenefice(lst, ItemFamille.Notebook).ToString("0.000"), FontFactory.GetFont("Arial", 7)));
            table.AddCell(cell);
            cell = new PdfPCell(new Phrase((CalculateMarge(lst, ItemFamille.Notebook)).ToString("0.000"), FontFactory.GetFont("Arial", 7)));
            table.AddCell(cell);

            //PC
            cell = new PdfPCell(new Phrase("PC", FontFactory.GetFont("Arial", 7, Font.BOLD)));
            table.AddCell(cell);
            cell = new PdfPCell(new Phrase(lst.Where(i => i.Famille == ItemFamille.Pc).Sum(i => i.Qt).ToString(), FontFactory.GetFont("Arial", 7)));
            table.AddCell(cell);
            cell = new PdfPCell(new Phrase(lst.Where(i => i.Famille == ItemFamille.Pc).Sum(i => i.PrixAchat * i.Qt).ToString("0.000"), FontFactory.GetFont("Arial", 7)));
            table.AddCell(cell);
            cell = new PdfPCell(new Phrase(lst.Where(i => i.Famille == ItemFamille.Pc).Sum(i => i.PrixVente * i.Qt).ToString("0.000"), FontFactory.GetFont("Arial", 7)));
            table.AddCell(cell);
            cell = new PdfPCell(new Phrase(CalculateBenefice(lst, ItemFamille.Pc).ToString("0.000"), FontFactory.GetFont("Arial", 7)));
            table.AddCell(cell);
            cell = new PdfPCell(new Phrase((CalculateMarge(lst, ItemFamille.Pc)).ToString("0.000"), FontFactory.GetFont("Arial", 7)));
            table.AddCell(cell);

            //Internet
            cell = new PdfPCell(new Phrase("Internet", FontFactory.GetFont("Arial", 7, Font.BOLD)));
            table.AddCell(cell);
            cell = new PdfPCell(new Phrase(lst.Where(i => i.Famille == ItemFamille.Internet).Sum(i => i.Qt).ToString(), FontFactory.GetFont("Arial", 7)));
            table.AddCell(cell);
            cell = new PdfPCell(new Phrase(lst.Where(i => i.Famille == ItemFamille.Internet).Sum(i => i.PrixAchat * i.Qt).ToString("0.000"), FontFactory.GetFont("Arial", 7)));
            table.AddCell(cell);
            cell = new PdfPCell(new Phrase(lst.Where(i => i.Famille == ItemFamille.Internet).Sum(i => i.PrixVente * i.Qt).ToString("0.000"), FontFactory.GetFont("Arial", 7)));
            table.AddCell(cell);
            cell = new PdfPCell(new Phrase(CalculateBenefice(lst, ItemFamille.Internet).ToString("0.000"), FontFactory.GetFont("Arial", 7)));
            table.AddCell(cell);
            cell = new PdfPCell(new Phrase((CalculateMarge(lst, ItemFamille.Internet)).ToString("0.000"), FontFactory.GetFont("Arial", 7)));
            table.AddCell(cell);

            return table;
        }

        private PdfPTable CreateTotalAchatTable(List<FiltreItem> lst)
        {
            PdfPTable table = new PdfPTable(2);
            table.WidthPercentage = 100f;
            float[] intTblWidth = new float[2] { .2f, .2f };
            table.SetWidths(intTblWidth);

            BaseColor grey = new BaseColor(System.Drawing.Color.Red);
            Font font = FontFactory.GetFont("Arial", 9, Font.NORMAL, grey);

            PdfPCell cell = new PdfPCell(new Phrase(""));

            cell.HorizontalAlignment = 1;
            cell.Colspan = 2;
            cell.Border = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Total Achat", FontFactory.GetFont("Arial", 7)));
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(lst.Sum(i => i.PrixAchat * i.Qt).ToString("0.000"), FontFactory.GetFont("Arial", 7)));
            table.AddCell(cell);


            return table;
        }

        private PdfPTable CreateTotalVenteTable(List<FiltreItem> lst)
        {
            PdfPTable table = new PdfPTable(2);
            table.WidthPercentage = 100f;
            float[] intTblWidth = new float[2] { .2f, .2f };
            table.SetWidths(intTblWidth);

            BaseColor grey = new BaseColor(System.Drawing.Color.Red);
            Font font = FontFactory.GetFont("Arial", 9, Font.NORMAL, grey);

            PdfPCell cell = new PdfPCell(new Phrase(""));

            cell.HorizontalAlignment = 1;
            cell.Colspan = 2;
            cell.Border = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Total Vente", FontFactory.GetFont("Arial", 7)));
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(lst.Sum(i => i.PrixVente * i.Qt).ToString("0.000"), FontFactory.GetFont("Arial", 7)));
            table.AddCell(cell);


            return table;
        }

        private PdfPTable CreateTotalBeneficeTable(List<FiltreItem> lst)
        {
            PdfPTable table = new PdfPTable(2);
            table.WidthPercentage = 100f;
            float[] intTblWidth = new float[2] { .2f, .2f };
            table.SetWidths(intTblWidth);

            BaseColor grey = new BaseColor(System.Drawing.Color.Red);
            Font font = FontFactory.GetFont("Arial", 9, Font.NORMAL, grey);

            PdfPCell cell = new PdfPCell(new Phrase(""));

            cell.HorizontalAlignment = 1;
            cell.Colspan = 2;
            cell.Border = 0;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Total Benefice", FontFactory.GetFont("Arial", 7)));
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(CalculateTotalBenefice(lst).ToString("0.000"), FontFactory.GetFont("Arial", 7)));
            table.AddCell(cell);


            return table;
        }

        private decimal CalculateTotalBenefice(List<FiltreItem> lst)
        {
            return CalculateBenefice(lst, ItemFamille.Accessoire) + CalculateBenefice(lst, ItemFamille.Internet) + CalculateBenefice(lst, ItemFamille.Maintenance)
                + CalculateBenefice(lst, ItemFamille.Notebook) + CalculateBenefice(lst, ItemFamille.Pc);
        }

        private decimal CalculateMarge(List<FiltreItem> lst, ItemFamille famille)
        {
            decimal marge = 0;

            decimal benefice = CalculateBenefice(lst, famille);

            decimal totAchat = lst.Where(i => i.Famille == famille).Sum(i => i.PrixAchat * i.Qt);
            if (totAchat != 0)
                marge = benefice * 100 / totAchat;
            else if (benefice != 0)
                marge = 100;
            else
                marge = 0;
            return marge;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DateDe.SelectedDate = null;
            DateA.SelectedDate = null;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Date.SelectedDate = DateTime.Now;
        }

        private void BtnRapportFiltreClick(object sender, RoutedEventArgs e)
        {
            MyDBEntities db = DbManager.CreateDbManager();

            List<FiltreItem> lst = new List<FiltreItem>();


            foreach (var v in db.Vente)
            {
                FiltreItem item = new FiltreItem()
                {
                    Designation = v.Designation,
                    PrixAchat = v.PrixAchat.GetValueOrDefault(),
                    PrixVente = v.PrixVente.GetValueOrDefault(),
                    SN = v.SN,
                    DateVente = v.DateDeVente.GetValueOrDefault(),
                    Qt = v.QT.GetValueOrDefault()
                };

                lst.Add(item);
            }

            lst = FiltreItems(lst);

            Document document = new Document();

            string fileName = string.Format(@"RapportsFiltre\Rapport{0}.pdf", Date.SelectedDate.GetValueOrDefault().ToShortDateString().Replace('/', '-') + "-" + DateTime.Now.ToLongTimeString().Replace(":", "-"));
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(fileName, FileMode.Create));
            if (DateDe.SelectedDate != null && DateA.SelectedDate != null)
                writer.PageEvent = new pdfPage(Date.SelectedDate.GetValueOrDefault(), "Rapport Filtre De : " + DateDe.SelectedDate.Value.ToShortDateString() + " jusqu'au : " + DateA.SelectedDate.Value.ToShortDateString());
            else if (Date.SelectedDate != null)
                writer.PageEvent = new pdfPage(Date.SelectedDate.GetValueOrDefault(), "Rapport Filtre Date : " + Date.SelectedDate.Value.ToShortDateString());

            document.Open();
            document.Add(AddParagraph(13));
            document.Add(CreateFiltredTable(lst));
            document.Add(AddParagraph(80));

            decimal totalVente = lst.Sum(i => i.PrixVente * i.Qt);
            decimal totalAchat = lst.Sum(i => i.PrixAchat * i.Qt);

            iTextSharp.text.Paragraph p = new iTextSharp.text.Paragraph();
            p.Add(new Phrase(string.Format("Total Vente = {0} D.T", totalVente.ToString("0.000"))));
            p.Alignment = Element.ALIGN_RIGHT;
            p.SpacingBefore = 23;
            document.Add(p);

            p = new iTextSharp.text.Paragraph();
            p.Add(new Phrase(string.Format("Total Achat = {0} D.T", totalAchat.ToString("0.000"))));
            p.Alignment = Element.ALIGN_RIGHT;
            p.SpacingBefore = 3;
            document.Add(p);

            p = new iTextSharp.text.Paragraph();
            p.Add(new Phrase(string.Format("Bénéfice = {0} D.T", (totalVente - totalAchat).ToString("0.000"))));
            p.Alignment = Element.ALIGN_RIGHT;
            p.SpacingBefore = 3;
            document.Add(p);

            document.Close();
            Process.Start(fileName);
        }

        private PdfPTable CreateFiltredTable(List<FiltreItem> lst)
        {
            PdfPTable table = new PdfPTable(6);
            table.WidthPercentage = 100f;
            float[] intTblWidth = new float[6] { .3f, .6f, .2f, .2f, .2f, .2f };
            table.SetWidths(intTblWidth);

            BaseColor grey = new BaseColor(System.Drawing.Color.Red);
            Font font = FontFactory.GetFont("Arial", 9, Font.NORMAL, grey);

            PdfPCell cell = new PdfPCell(new Phrase(""));

            cell.HorizontalAlignment = 1;
            cell.Colspan = 6;
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

            cell = new PdfPCell(new Phrase("Prix Vente", FontFactory.GetFont("Arial", 9, Font.BOLD)));
            cell.HorizontalAlignment = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Date/Time", FontFactory.GetFont("Arial", 9, Font.BOLD)));
            cell.HorizontalAlignment = 1;
            table.AddCell(cell);
            lst = lst.OrderBy(i => i.Famille).ThenBy(i => i.DateVente).ToList();
            FiltreItem prevItem = new FiltreItem();
            foreach (var item in lst)
            {
                if (item.Famille != prevItem.Famille)
                {
                    cell = new PdfPCell(new Phrase(item.Famille.ToString(), FontFactory.GetFont("Arial", 7, Font.BOLD)));
                    cell.Colspan = 6;
                    cell.HorizontalAlignment = 1;
                    table.AddCell(cell);
                }
                cell = new PdfPCell(new Phrase(item.SN, FontFactory.GetFont("Arial", 7)));
                table.AddCell(cell);
                cell = new PdfPCell(new Phrase(item.Designation, FontFactory.GetFont("Arial", 7)));
                table.AddCell(cell);
                cell = new PdfPCell(new Phrase(item.Qt.ToString(), FontFactory.GetFont("Arial", 7)));
                table.AddCell(cell);
                cell = new PdfPCell(new Phrase(item.PrixAchat.ToString("0.000"), FontFactory.GetFont("Arial", 7)));
                table.AddCell(cell);
                cell = new PdfPCell(new Phrase(item.PrixVente.ToString("0.000"), FontFactory.GetFont("Arial", 7)));
                table.AddCell(cell);
                cell = new PdfPCell(new Phrase(item.DateVente.ToString(), FontFactory.GetFont("Arial", 7)));
                table.AddCell(cell);

                prevItem = item;
            }

            return table;
        }

        private void Button_RapportExcel(object sender, RoutedEventArgs e)
        {
            string filName = string.Format("Rapports Excel/Rapport{0}--{1}.xlsx", DateTime.Now.ToShortDateString().Replace('/', '-'), DateTime.Now.ToLongTimeString().Replace(':', '-'));
            System.IO.File.Copy("Rapport.xlsx", filName, true);

            MyDBEntities db = DbManager.CreateDbManager();
            List<FiltreItem> lst = new List<FiltreItem>();
            foreach (var k in db.Vente.Where(a => a.EstablishmentId == cUser.EstablishmentId || cUser.EstablishmentName.Equals("Nouvelit", StringComparison.InvariantCultureIgnoreCase)).OrderBy(p => p.DateDeVente))
            {
                FiltreItem item = new FiltreItem()
                {
                    Designation = k.Designation,
                    PrixAchat = k.PrixAchat.GetValueOrDefault(),
                    PrixVente = k.PrixVente.GetValueOrDefault(),
                    SN = k.SN,
                    DateVente = k.DateDeVente.GetValueOrDefault(),
                    Qt = k.QT.GetValueOrDefault()
                };
                lst.Add(item);
            }

            if (DateDe.SelectedDate != null && DateA.SelectedDate != null)
            {
                lst = lst.Where(item => item.DateVente.Date >= DateDe.SelectedDate && item.DateVente.Date <= DateA.SelectedDate).ToList();
            }
            else if (Date.SelectedDate != null)
            {
                lst = lst.Where(item => item.DateVente.Date.Equals(Date.SelectedDate)).ToList();
            }

            lst = FiltreItems(lst);
            lst = lst.OrderBy(l => l.DateVente).ToList();
            string file = Directory.GetCurrentDirectory() + "\\" + filName;

            // excel_init(@"C:\Users\Administrateur\Downloads\AppCredit\AppCredit\AppCredit\bin\Debug\rapport.xlsx");

            excel_init(file);
            //string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filName + ";Extended Properties=Excel 12.0;";
            //using (OleDbConnection connection = new OleDbConnection(connectionString))
            //{
            //    connection.Open();

            //    // Le nom des colonnes se trouve en première ligne de la feuille.
            //    // Ici les colonnes concernées s'intitulent COL 1 et COL 2 :
            //    string cmdText = "INSERT INTO [rapport du mois$] (A8) VALUES ('Valeur dans la cellule A1')";
            //    using (OleDbCommand command = new OleDbCommand(cmdText, connection))
            //    {
            //        command.ExecuteNonQuery();
            //    }
            //}
            int i = 8;
            this.Cursor = Cursors.Wait;
            DateTime? date = null;

            foreach (var item in lst)
            {
                if (date != item.DateVente.Date)
                {
                    date = item.DateVente.Date;
                    excel_setValue("F" + i, String.Format("{0:m}", item.DateVente));
                }

                if (item.Famille == ItemFamille.Accessoire)
                    excel_setValue("A" + i, "ACC");
                else if (item.Famille == ItemFamille.Internet)
                    excel_setValue("A" + i, "INT");
                else if (item.Famille == ItemFamille.Maintenance)
                    excel_setValue("A" + i, "MAINT");
                else if (item.Famille == ItemFamille.Notebook)
                    excel_setValue("A" + i, "N");
                else if (item.Famille == ItemFamille.Pc)
                    excel_setValue("A" + i, "PC");
                // excel_setValue(item.CellNum);

                excel_setValue("B" + i, item.Qt + " * " + item.Designation);
                excel_setValue("C" + i, item.PrixAchat * item.Qt);
                excel_setValue("D" + i, item.PrixVente * item.Qt);

                i++;

            }


            excel_save();

            this.Cursor = Cursors.Arrow;
            var v = Process.GetProcessesByName("Excel");
            foreach (var item in v)
            {
                item.Kill();
            }
            Process.Start(file);


        }

        #region
        private static Microsoft.Office.Interop.Excel.Application appExcel;
        private static Microsoft.Office.Interop.Excel.Workbook newWorkbook = null;
        private static Microsoft.Office.Interop.Excel._Worksheet objsheet = null;

        //Method to initialize opening Excel
        static void excel_init(String path)
        {
            appExcel = new Microsoft.Office.Interop.Excel.Application();

            if (System.IO.File.Exists(path))
            {
                // then go and load this into excel

                newWorkbook = appExcel.Workbooks.Open(path, false, false);
                objsheet = (Microsoft.Office.Interop.Excel._Worksheet)appExcel.ActiveWorkbook.ActiveSheet;
            }
            else
            {
                MessageBox.Show("Unable to open file!");
                System.Runtime.InteropServices.Marshal.ReleaseComObject(appExcel);
                appExcel = null;

            }

        }

        //Method to get value; cellname is A1,A2, or B1,B2 etc...in excel.
        static string excel_getValue(string cellname)
        {
            string value = string.Empty;
            try
            {
                value = objsheet.get_Range(cellname).get_Value().ToString();
                // objsheet.get_Range("N3").set_Value(XlRangeValueDataType.xlRangeValueDefault, "Oui");
            }
            catch
            {
                value = "";
            }

            return value;
        }

        static void excel_setValue(string cellname, object cellValue)
        {
            string value = string.Empty;
            try
            {
                // value = objsheet.get_Range(cellname).get_Value().ToString();
                objsheet.get_Range(cellname).set_Value(Microsoft.Office.Interop.Excel.XlRangeValueDataType.xlRangeValueDefault, cellValue);
            }
            catch
            {
                value = "";
            }

            //   return value;
        }

        //Method to close excel connection
        static void excel_close()
        {
            if (appExcel != null)
            {
                try
                {
                    newWorkbook.Close();
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(appExcel);
                    appExcel = null;
                    objsheet = null;
                }
                catch (Exception ex)
                {
                    appExcel = null;
                    MessageBox.Show("Unable to release the Object " + ex.ToString());
                }
                finally
                {
                    GC.Collect();
                }
            }
        }

        static void excel_save()
        {
            if (appExcel != null)
            {
                try
                {
                    newWorkbook.Save();

                    System.Runtime.InteropServices.Marshal.ReleaseComObject(appExcel);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(newWorkbook);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(objsheet);
                    MessageBox.Show("Opération términé avec succéss");
                    appExcel = null;
                    objsheet = null;
                }
                catch (Exception ex)
                {
                    appExcel = null;
                    MessageBox.Show("Unable to release the Object " + ex.ToString());
                }
                finally
                {
                    GC.Collect();
                }
            }
        }

        #endregion
    }
}