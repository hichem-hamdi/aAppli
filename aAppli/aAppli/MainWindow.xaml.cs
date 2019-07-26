using System.Windows;
using aAppli.ViewModels;
using aAppli.Views;

namespace aAppli
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            btnStock.IsEnabled = cUser.Stock;
            btnVider.IsEnabled = cUser.ViderBase;
            btnManage.IsEnabled = cUser.GererAcces;

            btnHistorique.IsEnabled = cUser.HistoriqueVente;
            btnAVoir.IsEnabled = cUser.AVoir;
            btnInventaire.IsEnabled = cUser.Inventaire;
            btnInventaireGlobal.IsEnabled = cUser.Inventaire;
            btnVente.IsEnabled = cUser.Vente || cUser.Vente_S;
            btnCredit.IsEnabled = cUser.VenteCredit || cUser.VenteCredit_S;

            btnManageEstablishments.IsEnabled = cUser.EstablishmentName.Equals("Nouvelit", System.StringComparison.InvariantCultureIgnoreCase);

            btnHistoriqueReservation.IsEnabled = cUser.EstablishmentName.Equals("Nouvelit", System.StringComparison.InvariantCultureIgnoreCase);
            btnReserverArticle.IsEnabled = !cUser.EstablishmentName.Equals("Nouvelit", System.StringComparison.InvariantCultureIgnoreCase);

            this.Title += " " + cUser.Login + " / " + cUser.EstablishmentName;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            StockView stock = new StockView();
            stock.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            VenteView vente = new VenteView();
            vente.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            HistoriqueVenteView historique = new HistoriqueVenteView();
            historique.Show();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            AvoirView avoir = new AvoirView();
            avoir.Show();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            Pnl.Visibility = System.Windows.Visibility.Visible;
        }

        private void ViderVente()
        {
            MyDBEntities db = DbManager.CreateDbManager();
            foreach (var item in db.Vente)
            {
                db.Vente.DeleteObject(item);
            }

            foreach (var item in db.Credit)
            {
                db.Credit.DeleteObject(item);
            }

            db.SaveChanges();
        }

        private void ViderStock()
        {
            MyDBEntities db = DbManager.CreateDbManager();
            foreach (var item in db.Article)
            {
                db.Article.DeleteObject(item);
            }

            db.SaveChanges();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            if (ChkVente.IsChecked.GetValueOrDefault())
            {
                MessageBoxResult result = MessageBox.Show("Vouz voulez vider la base de donnée Vente?", "Vider la base", MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    ViderVente();
                    MessageBox.Show("Opération terminée");
                }
            }

            if (ChkStock.IsChecked.GetValueOrDefault())
            {
                MessageBoxResult result = MessageBox.Show("Vouz voulez vider la base de donnée Stock?", "Vider la base", MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.Cancel)
                {
                    return;
                }
                ViderStock();
                MessageBox.Show("Opération terminée");
            }

            if (ChkSupprimerArticleEnManque.IsChecked.GetValueOrDefault())
            {
                MessageBoxResult result = MessageBox.Show("Vouz voulez supprimer les articles manquants?", "Vider la base", MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.Cancel)
                {
                    return;
                }
                SupprimerArticlesManquants();
                MessageBox.Show("Opération terminée");
            }

            ChkVente.IsChecked = false;
            ChkStock.IsChecked = false;

            this.Pnl.Visibility = System.Windows.Visibility.Hidden;
        }

        private void SupprimerArticlesManquants()
        {
            MyDBEntities db = DbManager.CreateDbManager();
            foreach (var item in db.Article)
            {
                if (item.QT <= 0)
                    db.Article.DeleteObject(item);
            }

            db.SaveChanges();
        }

        private void Inventaire_Click(object sender, RoutedEventArgs e)
        {
            InventaireView inv = new InventaireView();
            inv.Show();
        }

        private void Credit_Click(object sender, RoutedEventArgs e)
        {
            CreditViewModel viewModel = new CreditViewModel();
            CreditView credit = new CreditView(null);
            credit.Show();
        }

        private void btnManage_click(object sender, RoutedEventArgs e)
        {
            ManageUsers manageUsers = new ManageUsers();
            manageUsers.Show();
        }

        private void btnInventaireGlobal_Click(object sender, RoutedEventArgs e)
        {
            var globalInventaire = new InventaireGlobal();
            globalInventaire.Show();
        }

        private void btnManageEstablishments_Click(object sender, RoutedEventArgs e)
        {
            var manageEstablishments = new ManageEstablishments();
            manageEstablishments.Show();
        }

        private void btnReserverArticle_Click(object sender, RoutedEventArgs e)
        {
            var reservation = new ReserverArticle();
            reservation.Show();
        }

        private void btnHistoriqueReservation_Click(object sender, RoutedEventArgs e)
        {
            var historiqueReservation = new HistoriqueReservation();
            historiqueReservation.Show();
        }
        private void btnEditInsertion_Click(object sender, RoutedEventArgs e)
        {
            var editInsertion = new EditInsertionView();
            editInsertion.Show();
        }
    }
}