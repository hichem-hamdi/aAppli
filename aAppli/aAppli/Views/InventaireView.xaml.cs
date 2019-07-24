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
using System.Data.OleDb;
using System.IO;
using aAppli.Models;

namespace aAppli.Views
{
    /// <summary>
    /// Interaction logic for InventaireView.xaml
    /// </summary>
    public partial class InventaireView : Window
    {
        public InventaireView()
        {
            InitializeComponent();
            this.Title += " " + cUser.Login + " / " + cUser.EstablishmentName;
        }

        private void TxtSN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                (sender as TextBox).Text = ";" + TxtSN.Text;
                TxtQt.Text = TxtSN.Text.Count(c => c.Equals(';')).ToString();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            List<VenteItemModel> Items = new List<VenteItemModel>();
            MyDBEntities db = DbManager.CreateDbManager();
            foreach (var v in db.Article.Where(a => a.EstablishmentId == cUser.EstablishmentId || cUser.EstablishmentName.Equals("Nouvelit", StringComparison.InvariantCultureIgnoreCase)).OrderBy(a => a.Designation))
            {
                VenteItemModel item = new VenteItemModel()
                {
                    Id = v.ID,
                    Designation = v.Designation,
                    SN = v.SN,
                    Quantite = v.QT
                };
                Items.Add(item);
            }

            string[] t = TxtSN.Text.Split(';');

            VenteItemModel article = Items.SingleOrDefault(i => i.SN.Split(';').Contains(t[1]));

            if (article != null)
            {
                string sn = "";// article.SN.Remove(article.SN.IndexOf(TxtSN.Text));
                foreach (var item in article.SN.Split(';'))
                {
                    if (t.Contains(item))
                    {
                        continue;
                    }

                    sn += ";" + item;
                }

                Txtarticle.Text = string.Format("Vous avez {0} articles perdus dont les SN sont : {1}", article.Quantite - TxtSN.Text.Count(c => c.Equals(';')), sn);
            }
            else
            {
                MessageBox.Show("Aucun article trouvé.");
            }
        }
    }
}
