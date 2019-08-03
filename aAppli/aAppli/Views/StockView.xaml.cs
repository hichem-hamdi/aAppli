using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using aAppli.Models;
using aAppli.ViewModels;
using Microsoft.Windows.Controls;
using System.Collections.ObjectModel;

namespace aAppli.Views
{
    /// <summary>
    /// Interaction logic for StockView.xaml
    /// </summary>
    public partial class StockView : Window
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true, CallingConvention = CallingConvention.Winapi)]
        public static extern short GetKeyState(int keyCode);

        private DispatcherTimer timer;

        public StockView()
        {
            InitializeComponent();
            this.Title += " " + cUser.Login + " / " + cUser.EstablishmentName;
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(0.5);
            timer.Tick += timer1_Tick;
            search.Focus();
            timer.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            bool CapsLock = (((ushort)GetKeyState(0x14)) & 0xffff) != 0;
            if (CapsLock)
            {
                busy.IsBusy = true;
                busy.BusyContent = "Touche Majuscule";
                return;
            }

            busy.IsBusy = false;
        }

        private void WatermarkTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            StockViewModel viewModel = (DataContext as StockViewModel);

            if ((sender as WatermarkTextBox).Text == string.Empty)
            {
                viewModel.Articles = viewModel.InitArticles;
                return;
            }

            viewModel.Articles = new System.Collections.ObjectModel.ObservableCollection<Models.ArticleModel>(viewModel.InitArticles.Where(article => (article.Designation != null && article.Designation.Contains((sender as WatermarkTextBox).Text.ToUpper()) || (article.SN != null && article.SN.Split(';').Contains((sender as WatermarkTextBox).Text)))));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            search.Text = string.Empty;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.IsEnabled = false;

            MyDBEntities db = DbManager.CreateDbManager();
            ArticleModel art = new ArticleModel
            {
                Families = new ObservableCollection<Famille>(db.Famille.ToList()),
                Categories = new ObservableCollection<Categorie>(db.Categorie.ToList()),
                SubCategories = new ObservableCollection<SOUS_CATEGORIE>(db.SOUS_CATEGORIE.ToList()),
                Brands = new ObservableCollection<Brand>(db.Brand.ToList()),
                Sizes = new ObservableCollection<Size>(db.Size.ToList()),
                Suppliers = new ObservableCollection<Fournisseur>(db.Fournisseur.ToList())
            };

            var familyDialog = new FamilleDialog();
            if (familyDialog.ShowDialog() == true)
            {
                art.SelectedFamily = familyDialog.cbFamilies.SelectedItem as Famille;
            }
            else
            {
                return;
            }
            var categoryDialog = new CategoryDialog();
            if (categoryDialog.ShowDialog() == true)
            {
                art.SelectedCategory = categoryDialog.cbCategories.SelectedItem as Categorie;
            }
            else
            {
                return;
            }
            var subCategoryDialog = new SubCategoryDialog();
            if (subCategoryDialog.ShowDialog() == true)
            {
                art.SelectedSubCategory = subCategoryDialog.cbSubCategories.SelectedItem as SOUS_CATEGORIE;
            }
            else
            {
                return;
            }
            var brandsDialog = new BrandDialog();
            if (brandsDialog.ShowDialog() == true)
            {
                art.SelectedBrand = brandsDialog.cbBrands.SelectedItem as Brand;
            }
            else
            {
                return;
            }
            var sizesDialog = new SizeDialog();
            if (sizesDialog.ShowDialog() == true)
            {
                art.SelectedSize = sizesDialog.cbSizes.SelectedItem as Size;
            }
            else
            {
                return;
            }
            var suppliersDialog = new SupplierDialog();
            if (suppliersDialog.ShowDialog() == true)
            {
                art.SelectedSupplier = suppliersDialog.cbSuppliers.SelectedItem as Fournisseur;
            }
            else
            {
                return;
            }
            var purchaseDateDialog = new PurchaseDateDialog();
            if (purchaseDateDialog.ShowDialog() == true)
            {
                art.PurchaseDate = purchaseDateDialog.purchaseDate.SelectedDate.Value.Date;
            }
            else
            {
                return;
            }
            var quantityDialog = new QuantityDialog();
            if (quantityDialog.ShowDialog() == true)
            {
                art.PicesQuantity = int.Parse(quantityDialog.quantityNumber.Text);
            }
            else
            {
                return;
            }
            var descriptionDialog = new DescriptionDialog();
            if (descriptionDialog.ShowDialog() == true)
            {
                art.Description = descriptionDialog.descriptionText.Text;
            }
            else
            {
                return;
            }
            this.IsEnabled = true;

            (DataContext as StockViewModel).Articles.Add(art);
            myDataGrid.SelectedItem = (DataContext as StockViewModel).Articles[(DataContext as StockViewModel).Articles.Count - 1];
            myDataGrid.ScrollIntoView(myDataGrid.SelectedItem);
        }

        public DataGridCell GetCell(int row, int column)
        {
            DataGridRow rowContainer = GetRow(row);

            if (rowContainer != null)
            {
                DataGridCellsPresenter presenter = GetVisualChild<DataGridCellsPresenter>(rowContainer);

                // try to get the cell but it may possibly be virtualized
                DataGridCell cell = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(column);
                if (cell == null)
                {
                    // now try to bring into view and retreive the cell
                    myDataGrid.ScrollIntoView(rowContainer, myDataGrid.Columns[column]);
                    cell = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(column);
                }
                return cell;
            }
            return null;
        }

        private static T GetVisualChild<T>(Visual parent) where T : Visual
        {
            T child = default(T);
            int numVisuals = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < numVisuals; i++)
            {
                Visual v = (Visual)VisualTreeHelper.GetChild(parent, i);
                child = v as T;
                if (child == null)
                {
                    child = GetVisualChild<T>(v);
                }
                if (child != null)
                {
                    break;
                }
            }
            return child;
        }

        public DataGridRow GetRow(int index)
        {
            DataGridRow row = (DataGridRow)myDataGrid.ItemContainerGenerator.ContainerFromIndex(index);
            if (row == null)
            {
                // may be virtualized, bring into view and try again
                myDataGrid.ScrollIntoView(myDataGrid.Items[index]);
                row = (DataGridRow)myDataGrid.ItemContainerGenerator.ContainerFromIndex(index);
            }
            return row;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            StockViewModel viewModel = (DataContext as StockViewModel);

            MessageBoxResult result = Microsoft.Windows.Controls.MessageBox.Show("Voulez vous supprimer l'Article SN : " + search.Text + " Quantité : " + viewModel.NbrArticleToBeDeleted.ToString(), "Confirmation Supprimer", System.Windows.MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Exclamation);

            if (result.Equals(MessageBoxResult.No))
            {
                return;
            }

            ArticleModel article = viewModel.InitArticles.SingleOrDefault(art => art.SN.Contains(search.Text));

            if (article == null)
            {
                Microsoft.Windows.Controls.MessageBox.Show("Aucun Article trouvé avec le SN : " + search.Text, "Supprimer", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Stop);
                return;
            }
            if (article.QT < viewModel.NbrArticleToBeDeleted)
            {
                Microsoft.Windows.Controls.MessageBox.Show("Quantité disponible insuffisante.", "Supprimer", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Stop);
                return;
            }

            article.QT = article.QT - viewModel.NbrArticleToBeDeleted;

            if (article.IsGenerique && article.QT <= 0)
                article.SN = article.SN.Replace(";" + search.Text, "");
            else if (!article.IsGenerique)
                article.SN = article.SN.Replace(";" + search.Text, "");



            MyDBEntities db = DbManager.CreateDbManager();
            Article artt = db.Article.FirstOrDefault(a => a.ID == article.Id);
            if (artt != null)
            {
                artt.SN = article.SN;
                artt.QT = article.QT;
            }
            db.SaveChanges();


            Microsoft.Windows.Controls.MessageBox.Show("Article Supprimer", "Update", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);

            viewModel.NbrArticleToBeDeleted = 1;


            search.Text = string.Empty;
        }

        private void btnVerifier_Click(object sender, RoutedEventArgs e)
        {
            MyDBEntities db = DbManager.CreateDbManager();

            StringBuilder sb = new StringBuilder();
            foreach (var a in db.Article.Where(a => a.EstablishmentId == cUser.EstablishmentId || cUser.EstablishmentName.Equals("Nouvelit", StringComparison.InvariantCultureIgnoreCase)).OrderBy(a => a.Designation))
            {
                ArticleModel art = new ArticleModel
                {
                    Id = a.ID,
                    Designation = a.Designation,
                    PrixAchat = a.PrixAchat,
                    PrixVente = a.PrixVente,
                    SN = a.SN,
                    QT = a.QT,
                    IsGenerique = a.IsGenerique
                };

                if (art.QT != art.SN.Count(c => c == ';') && !art.IsGenerique)
                {
                    sb.Append(art.Designation);
                    sb.Append(" QT= ");
                    sb.Append(art.QT);
                    sb.Append(" N° SN : ");
                    sb.AppendLine(art.SN.Count(c => c == ';').ToString());
                }
            }

            if (sb.Length > 0)
                EcrireFichier(sb);
        }

        private void EcrireFichier(StringBuilder sb)
        {
            string fileName = string.Format(@"RapportsCredit\Stock{0}.txt", DateTime.Now.ToShortDateString().Replace('/', '-'));
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(fileName))
            {
                file.WriteLine(sb.ToString());
            }

            Process.Start(fileName);
        }

        private void search_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string fileName = string.Format(@"LogSN\SN{0}.txt", DateTime.Now.ToShortDateString().Replace('/', '-'));
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("#Stock recherche#");
                sb.Append(search.Text);
                sb.Append("     ");
                sb.Append(DateTime.Now);
                sb.Append("     ");
                sb.Append(cUser.Login);
                Log.WriteLog(fileName, sb.ToString());
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {

        }
    }
}