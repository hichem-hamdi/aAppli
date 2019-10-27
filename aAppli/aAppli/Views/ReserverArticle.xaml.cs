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
using aAppli.Models;

namespace aAppli.Views
{
    /// <summary>
    /// Interaction logic for ReserverArticle.xaml
    /// </summary>
    public partial class ReserverArticle : Window
    {
        Article _ArticleToBook;

        public ReserverArticle()
        {
            InitializeComponent();

            SearchInOther.Focus();
            LoadMyBooking();
            LoadMyBookingToValidate();
            this.Title += " " + cUser.Login + " / " + cUser.EstablishmentName;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _ArticleToBook = ((FrameworkElement)sender).DataContext as Article;
            sn.Text = _ArticleToBook.SN;

            if (_ArticleToBook.IsGenerique)
            {
                qt.IsEnabled = true;
            }

            SelectSN.Show();
        }

        private void sn_TextChanged(object sender, TextChangedEventArgs e)
        {
            qt.Text = sn.Text.Split(';').Count(c => !string.IsNullOrWhiteSpace(c)).ToString();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Reservation rese = new Reservation
            {
                ArticleId = _ArticleToBook.ID,
                IsValidated = null,
                RequestDate = DateTime.Now,
                SN = sn.Text,
                UserId = cUser.Id,
                Qt = sn.Text.Split(';').Count(c => !string.IsNullOrWhiteSpace(c)),
                FromId = _ArticleToBook.EstablishmentId.GetValueOrDefault(),
                ToId = cUser.EstablishmentId,
                ValidatedBy = null,
                ValidationDate = null
            };

            if (_ArticleToBook.IsGenerique)
            {
                rese.Qt = int.Parse(qt.Text);
            }

            MyDBEntities db = DbManager.CreateDbManager();
            db.Reservation.AddObject(rese);
            db.SaveChanges();
            LoadMyBooking();
            SelectSN.Close();
            Microsoft.Windows.Controls.MessageBox.Show("Réservation enregistrée avec succès", "Réservtion", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
        }

        private void LoadMyBooking()
        {
            MyDBEntities db = DbManager.CreateDbManager();
            var bookings = db.Reservation.Where(r => r.IsValidated == null && r.UserId == cUser.Id).ToList();
            var myBookings = new List<MyBooking>();
            foreach (var item in bookings)
            {
                MyBooking b = new MyBooking
                {
                    Id = item.Id,
                    Designation = item.Article.Designation,
                    Magasin = item.Establishment1.Name,
                    QT = item.Qt.GetValueOrDefault(),
                    RequestDate = item.RequestDate,
                    SN = item.SN
                };

                myBookings.Add(b);
            }

            myBooking.ItemsSource = myBookings;
        }

        private void LoadMyBookingToValidate()
        {
            MyDBEntities db = DbManager.CreateDbManager();
            var bookings = db.Reservation.Where(r => r.IsValidated == null && r.FromId == cUser.EstablishmentId).ToList();
            var myBookings = new List<MyBooking>();
            foreach (var item in bookings)
            {
                MyBooking b = new MyBooking
                {
                    Id = item.Id,
                    Designation = item.Article?.Designation,
                    Magasin = item.Establishment.Name,
                    QT = item.Qt.GetValueOrDefault(),
                    RequestDate = item.RequestDate,
                    SN = item.SN
                };

                myBookings.Add(b);
            }

            myBookingToValidate.ItemsSource = myBookings;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var booking = ((FrameworkElement)sender).DataContext as MyBooking;
            MyDBEntities db = DbManager.CreateDbManager();
            var obj = db.Reservation.FirstOrDefault(r => r.Id == booking.Id);

            if (obj != null)
            {
                obj.IsValidated = false;
                obj.ValidationDate = DateTime.Now;
                obj.ValidatedBy = cUser.Id;
                db.SaveChanges();
            }

            LoadMyBookingToValidate();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            var booking = ((FrameworkElement)sender).DataContext as MyBooking;
            MyDBEntities db = DbManager.CreateDbManager();
            var obj = db.Reservation.FirstOrDefault(r => r.Id == booking.Id);
            obj.IsValidated = true;
            obj.ValidationDate = DateTime.Now;
            obj.ValidatedBy = cUser.Id;

            Article art;

            art = db.Article.FirstOrDefault(a => a.Designation == obj.Article.Designation && a.EstablishmentId == obj.ToId);

            if (art == null)
            {
                art = new Article
                {
                    Designation = obj.Article.Designation,
                    EstablishmentId = obj.ToId,
                    IsGenerique = obj.Article.IsGenerique,
                    PrixAchat = obj.Article.PrixAchat,
                    PrixVente = obj.Article.PrixVente,
                    QT = obj.Qt.GetValueOrDefault(),
                    SN = obj.SN
                };
                db.Article.AddObject(art);
            }
            else
            {
                art.QT += obj.Qt.GetValueOrDefault();
                if (!art.IsGenerique)
                {
                    art.SN +=obj.SN;
                }
            }

            Article article = db.Article.FirstOrDefault(a => a.ID == obj.ArticleId);
            article.QT -= obj.Qt.GetValueOrDefault();
            if (!article.IsGenerique)
            {
                var initialSn = article.SN.Split(';');
                var sn = obj.SN.Split(';');
                var finalSn = string.Empty;
                foreach (var item in initialSn)
                {
                    if (!sn.Contains(item))
                    {
                        finalSn += ";" + item;
                    }
                }
                article.SN = finalSn;
            }
            else if (article.IsGenerique && article.QT == 0)
            {
                article.SN = string.Empty;
            }
           

            db.SaveChanges();

            LoadMyBookingToValidate();
        }

        private void sn_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                if (sn.Text.IndexOf(';') == 0)
                {
                    return;
                }
                sn.Text = sn.Text.Insert(0, ";");
            }
        }
    }
}
