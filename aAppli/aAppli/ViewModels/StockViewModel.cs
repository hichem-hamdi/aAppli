using System;
using System.Collections.ObjectModel;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using aAppli.Models;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;
using System.Text;
using aAppli.Views;

namespace aAppli.ViewModels
{
    public class StockViewModel : NotificationObject
    {
        private int _NbrArticleToBeDeleted;
        public int NbrArticleToBeDeleted
        {
            get
            {
                return _NbrArticleToBeDeleted;
            }
            set
            {
                if (_NbrArticleToBeDeleted == value)
                {
                    return;
                }

                _NbrArticleToBeDeleted = value;
                RaisePropertyChanged(() => NbrArticleToBeDeleted);
            }
        }

        private int _NbrArticle;

        public int NbrArticle
        {
            get
            {
                return _NbrArticle;
            }
            set
            {
                if (_NbrArticle == value)
                {
                    return;
                }

                _NbrArticle = value;
                RaisePropertyChanged(() => NbrArticle);
            }
        }

        private bool _IsBusy;
        public bool IsBusy
        {
            get
            {
                return _IsBusy;
            }
            set
            {
                if (_IsBusy == value)
                {
                    return;
                }

                _IsBusy = value;
                RaisePropertyChanged(() => IsBusy);
            }
        }

        private bool _IsMasquer;
        public bool IsMasquer
        {
            get
            {
                return _IsMasquer;
            }
            set
            {
                if (_IsMasquer == value)
                {
                    return;
                }

                _IsMasquer = value;
                RaisePropertyChanged(() => IsMasquer);

                if (IsMasquer)
                {
                    Articles = new ObservableCollection<ArticleModel>(InitArticles.Where(a => a.QT > 0).ToList());
                    NbrArticle = Articles.Count(a => a.QT > 0);
                }
                else
                {
                    Articles = new ObservableCollection<ArticleModel>(InitArticles.ToList());
                    NbrArticle = Articles.Count();
                }
            }
        }

        private string _SearchInOther;
        public string SearchInOther
        {
            get
            {
                return _SearchInOther;
            }
            set
            {
                if (_SearchInOther == value)
                {
                    return;
                }

                _SearchInOther = value;
                RaisePropertyChanged(() => SearchInOther);
            }
        }

        public ObservableCollection<ArticleModel> InitArticles;
        private ObservableCollection<ArticleModel> _Articles;

        public ObservableCollection<ArticleModel> Articles
        {
            get
            {
                return _Articles;
            }
            set
            {
                if (_Articles == value)
                {
                    return;
                }

                _Articles = value;
                RaisePropertyChanged(() => Articles);
            }
        }

        public StockViewModel()
        {
            _NbrArticleToBeDeleted = 1;

            Articles = new ObservableCollection<ArticleModel>();
            InitArticles = new ObservableCollection<ArticleModel>();
            StockViewLoadedCommand = new DelegateCommand(OnStockViewLoaded);
            SaveCommand = new DelegateCommand<ArticleModel>(OnSave);
            DeleteCommand = new DelegateCommand<ArticleModel>(OnDelete);
            EditCommand = new DelegateCommand<ArticleModel>(OnEdit);
            AddCommand = new DelegateCommand(OnAdd);
            EnterCommand = new DelegateCommand<ArticleModel>(OnEnter);
            EnterCommandSearchInOther = new DelegateCommand(OnEnterSearchInOther);
            DeleteManque = new DelegateCommand(OnDel);
        }

        public ICommand StockViewLoadedCommand { get; private set; }

        public ICommand SaveCommand { get; private set; }

        public ICommand DeleteManque { get; private set; }

        public ICommand DeleteCommand { get; private set; }
        public ICommand EditCommand { get; private set; }

        public ICommand AddCommand { get; private set; }

        public ICommand EnterCommand { get; private set; }

        public ICommand EnterCommandSearchInOther { get; private set; }

        private void OnDel()
        {
            MyDBEntities db = DbManager.CreateDbManager();
            foreach (var item in db.Article)
            {
                if (item.QT <= 0)
                    db.Article.DeleteObject(item);
            }

            db.SaveChanges();

            Articles = new ObservableCollection<ArticleModel>();
            InitArticles = new ObservableCollection<ArticleModel>();
            OnStockViewLoaded();
        }

        private void OnEnter(ArticleModel article)
        {
            if (article == null)
            {
                return;
            }

            if (article.SN == null)
            {
                return;
            }

            if (article.SN.StartsWith(";"))
            {
                return;
            }

            article.SN = article.SN.Insert(0, ";");
        }

        private void OnEnterSearchInOther()
        {
            var verifierExsitence = new VerifierExsitence(SearchInOther);
            verifierExsitence.ShowDialog();
        }

        private void OnAdd()
        {
            // Articles.Add(new ArticleModel());
        }

        private void OnStockViewLoaded()
        {
            MyDBEntities db = DbManager.CreateDbManager();

            db.SaveChanges();
            IsBusy = true;

            foreach (var item in db.Article.Where(a => a.EstablishmentId == cUser.EstablishmentId || cUser.EstablishmentName.Equals("Nouvelit", StringComparison.InvariantCultureIgnoreCase)).OrderBy(a => a.Designation))
            {
                ArticleModel art = new ArticleModel
                {
                    IsLoadedFromDB = true,
                    Id = item.ID,
                    Designation = item.Designation,
                    PrixAchat = item.PrixAchat,
                    PrixVente = item.PrixVente,
                    SN = item.SN,
                    QT = item.QT,
                    IsGenerique = item.IsGenerique,
                    Families = new ObservableCollection<Famille>(db.Famille.ToList()),
                    SelectedFamily = item.Famille,
                    Categories = new ObservableCollection<Categorie>(db.Categorie.ToList()),
                    SelectedCategory = item.Categorie,
                    SubCategories = new ObservableCollection<SOUS_CATEGORIE>(db.SOUS_CATEGORIE.ToList()),
                    SelectedSubCategory = item.SOUS_CATEGORIE,
                    Brands = new ObservableCollection<Brand>(db.Brand.ToList()),
                    SelectedBrand = item.Brand,
                    Models = new ObservableCollection<Model>(db.Model.ToList()),
                    SelectedModel = item.Model,
                    Sizes = new ObservableCollection<Size>(db.Size.ToList()),
                    SelectedSize = item.Size,
                    Suppliers = new ObservableCollection<Fournisseur>(db.Fournisseur.ToList()),
                    SelectedSupplier = item.Fournisseur,
                    PurchaseDate = item.DateAchat,
                    PicesQuantity = item.PicesQuantity != null ? int.Parse(item.PicesQuantity.Value.ToString()) : 0,
                    Description = item.Description
                };
                Articles.Add(art);
                InitArticles.Add(art);
                art.IsLoadedFromDB = false;
            }
            NbrArticle = Articles.Count;
            IsBusy = false;
            IsMasquer = false;
        }

        private void OnSave(ArticleModel article)
        {
            if (article.SelectedBrand == null || article.SelectedCategory == null || article.SelectedFamily == null || article.SelectedSize == null
            || article.SelectedSubCategory == null || article.SelectedSupplier == null || article.PurchaseDate == null || article.SelectedModel == null)
            {
                Microsoft.Windows.Controls.MessageBox.Show("La saisie n'est pas complete!", "Error", MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }

            IsBusy = true;
            string[] tab = null;
            if (article.SN != null)
            {
                tab = article.SN.Split(';');
            }
            if (tab != null && article.Id == 0)
            {
                bool snExist = false;
                foreach (var item in tab)
                {
                    if (item != string.Empty && SnExist(item))
                    {
                        Microsoft.Windows.Controls.MessageBox.Show(string.Format("Le SN : {0} existe dans la BD.", item), "Error", MessageBoxButton.OK, MessageBoxImage.Stop);
                        snExist = true;
                    }
                }

                if (snExist)
                {
                    IsBusy = false;
                    return;
                }
            }

            if (DesignationExist(article.Designation) && article.Id == 0)
            {
                Microsoft.Windows.Controls.MessageBox.Show(string.Format("La designation : {0} existe dans la BD.", article.Designation), "Error", MessageBoxButton.OK, MessageBoxImage.Stop);
                IsBusy = false;
                return;
            }
            MyDBEntities db = DbManager.CreateDbManager();
            //DBEntities db = new DBEntities();            
            if (article.Id == 0)
            {
                try
                {
                    Article art = new Article
                    {
                        Designation = article.Designation,
                        IsGenerique = article.IsGenerique,
                        PrixAchat = article.PrixAchat,
                        PrixVente = article.PrixVente,
                        QT = article.QT,
                        SN = article.SN,
                        EstablishmentId = cUser.EstablishmentId,
                        FamilleId = article.SelectedFamily.Id,
                        CategorieId = article.SelectedCategory.Id,
                        SousCategorieId = article.SelectedSubCategory.Id,
                        BrandId = article.SelectedBrand.Id,
                        ModelId = article.SelectedModel.Id,
                        SizeId = article.SelectedSize.Id,
                        FournisseurId = article.SelectedSupplier.Id,
                        Description = article.Description,
                        PicesQuantity = article.PicesQuantity,
                        DateAchat = article.PurchaseDate
                    };

                    db.Article.AddObject(art);
                    db.SaveChanges();


                }
                catch (Exception ex)
                {
                    Microsoft.Windows.Controls.MessageBox.Show(ex.Message, "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                    IsBusy = false;
                    return;
                }

                Microsoft.Windows.Controls.MessageBox.Show("Saved", "Save", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);

            }
            else
            {
                try
                {
                    Article art = db.Article.FirstOrDefault(a => a.ID == article.Id);
                    if (art != null)
                    {
                        art.Designation = article.Designation;
                        art.IsGenerique = article.IsGenerique;
                        art.PrixAchat = article.PrixAchat;
                        art.PrixVente = article.PrixVente;
                        art.QT = article.QT;
                        art.SN = article.SN;
                        art.FamilleId = article.SelectedFamily.Id;
                        art.CategorieId = article.SelectedCategory.Id;
                        art.SousCategorieId = article.SelectedSubCategory.Id;
                        art.BrandId = article.SelectedBrand.Id;
                        art.ModelId = article.SelectedModel.Id;
                        art.SizeId = article.SelectedSize.Id;
                        art.FournisseurId = article.SelectedSupplier.Id;
                        art.DateAchat = article.PurchaseDate;
                        art.PicesQuantity = article.PicesQuantity;
                        art.Description = article.Description;
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    Microsoft.Windows.Controls.MessageBox.Show(ex.Message, "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                    IsBusy = false;
                    return;
                }
                Microsoft.Windows.Controls.MessageBox.Show("Saved", "Save", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
            }
            string fileName = string.Format(@"LogSN\SN{0}.txt", DateTime.Now.ToShortDateString().Replace('/', '-'));
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("         #Stock Ajout#");
            sb.Append(article.SN);
            sb.Append("     ");
            sb.Append(DateTime.Now);
            sb.Append("     ");
            sb.Append(article.QT);
            sb.Append("     ");
            sb.Append(cUser.Login);
            Log.WriteLog(fileName, sb.ToString());
            Articles = new ObservableCollection<ArticleModel>();
            InitArticles = new ObservableCollection<ArticleModel>();
            OnStockViewLoaded();
            IsBusy = false;
        }

        private bool SnExist(string sn)
        {
            string Sn = "";
            MyDBEntities db = DbManager.CreateDbManager();
            foreach (var item in db.Article.Where(a => a.EstablishmentId == cUser.EstablishmentId || cUser.EstablishmentName.Equals("Nouvelit", StringComparison.InvariantCultureIgnoreCase)).OrderBy(a => a.Designation))
            {
                Sn += item.SN;
            }

            return Sn.Contains(";" + sn + ";");
        }

        private bool DesignationExist(string Designation)
        {
            string designation = "";
            bool exist = false;
            MyDBEntities db = DbManager.CreateDbManager();
            foreach (var item in db.Article.Where(a => a.EstablishmentId == cUser.EstablishmentId || cUser.EstablishmentName.Equals("Nouvelit", StringComparison.InvariantCultureIgnoreCase)).OrderBy(a => a.Designation))
            {
                designation = item.Designation;
                exist = string.Equals(designation, Designation, StringComparison.InvariantCultureIgnoreCase);
                if (exist)
                {
                    break;
                }
            }
            return exist;
        }

        private void OnDelete(ArticleModel article)
        {
            IsBusy = true;

            MessageBoxResult result = Microsoft.Windows.Controls.MessageBox.Show("Vous confirmer le Delete de cet article?", "Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.No)
            {
                IsBusy = false;
                return;
            }

            //  DBEntities db = new DBEntities();

            if (article.Id == 0)
            {
                Articles.Remove(article);
                IsBusy = false;
                return;
            }
            try
            {
                MyDBEntities db = DbManager.CreateDbManager();
                Article art = db.Article.FirstOrDefault(a => a.ID == article.Id);
                db.Article.DeleteObject(art);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Microsoft.Windows.Controls.MessageBox.Show(ex.Message, "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                IsBusy = false;
                return;
            }


            Microsoft.Windows.Controls.MessageBox.Show("Deleted", "Delete", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);


            Articles = new ObservableCollection<ArticleModel>();
            InitArticles = new ObservableCollection<ArticleModel>();
            OnStockViewLoaded();
            IsBusy = false;
        }

        private void OnEdit(ArticleModel article)
        {
            IsBusy = true;
            MyDBEntities db = DbManager.CreateDbManager();

            var familyDialog = new FamilleDialog(article);
            if (familyDialog.ShowDialog() == true)
            {
                article.SelectedFamily = familyDialog.cbFamilies.SelectedItem as Famille;
            }
            else
            {
                return;
            }
            var categoryDialog = new CategoryDialog(article);
            if (categoryDialog.ShowDialog() == true)
            {
                article.SelectedCategory = categoryDialog.cbCategories.SelectedItem as Categorie;
            }
            else
            {
                return;
            }
            var subCategoryDialog = new SubCategoryDialog(article);
            if (subCategoryDialog.ShowDialog() == true)
            {
                article.SelectedSubCategory = subCategoryDialog.cbSubCategories.SelectedItem as SOUS_CATEGORIE;
            }
            else
            {
                return;
            }
            var brandsDialog = new BrandDialog(article);
            if (brandsDialog.ShowDialog() == true)
            {
                article.SelectedBrand = brandsDialog.cbBrands.SelectedItem as Brand;
            }
            else
            {
                return;
            }
            var modelsDialog = new ModelDialog(article);
            if (modelsDialog.ShowDialog() == true)
            {
                article.SelectedModel = modelsDialog.cbModels.SelectedItem as Model;
            }
            else
            {
                return;
            }
            var sizesDialog = new SizeDialog(article);
            if (sizesDialog.ShowDialog() == true)
            {
                article.SelectedSize = sizesDialog.cbSizes.SelectedItem as Size;
            }
            else
            {
                return;
            }

            var descriptionDialog = new DescriptionDialog(article);
            if (descriptionDialog.ShowDialog() == true)
            {
                article.Description = descriptionDialog.descriptionText.Text;
            }
            else
            {
                return;
            }
            var suppliersDialog = new SupplierDialog(article);
            if (suppliersDialog.ShowDialog() == true)
            {
                article.SelectedSupplier = suppliersDialog.cbSuppliers.SelectedItem as Fournisseur;
            }
            else
            {
                return;
            }
            var purchaseDateDialog = new PurchaseDateDialog(article);
            if (purchaseDateDialog.ShowDialog() == true)
            {
                article.PurchaseDate = purchaseDateDialog.purchaseDate.SelectedDate.Value.Date;
            }
            else
            {
                return;
            }
            var quantityDialog = new QuantityDialog(article);
            if (quantityDialog.ShowDialog() == true)
            {
                article.PicesQuantity = int.Parse(quantityDialog.quantityNumber.Text);
            }
            else
            {
                return;
            }

            Article art = db.Article.FirstOrDefault(a => a.ID == article.Id);
            if (art != null)
            {
                art.Designation = article.Designation;
                art.FamilleId = article.SelectedFamily.Id;
                art.CategorieId = article.SelectedCategory.Id;
                art.SousCategorieId = article.SelectedSubCategory.Id;
                art.BrandId = article.SelectedBrand.Id;
                art.ModelId = article.SelectedModel.Id;
                art.SizeId = article.SelectedSize.Id;
                art.FournisseurId = article.SelectedSupplier.Id;
                art.DateAchat = article.PurchaseDate;
                art.PicesQuantity = article.PicesQuantity;
                art.Description = article.Description;
                db.SaveChanges();
            }

            IsBusy = false;
        }
    }
}