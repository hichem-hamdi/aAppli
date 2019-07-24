using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using aAppli.Models;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;
using Microsoft.Windows.Controls;
using System.Collections.ObjectModel;

namespace aAppli.ViewModels
{
    public class AvoirViewModel : NotificationObject
    {
        private ObservableCollection<VenteItemModel> _Items;
        public ObservableCollection<VenteItemModel> Items
        {
            get
            {
                return _Items;
            }
            set
            {
                if (_Items == value)
                {
                    return;
                }

                _Items = value;
                RaisePropertyChanged(() => Items);
            }
        }

        private VenteItemModel _selectedItem;
        public VenteItemModel SelectedItemd
        {
            get
            {
                return _selectedItem;
            }

            set
            {
                if (_selectedItem == value)
                    return;
                _selectedItem = value;

                if (value != null)
                    IsOkEnabled = true;
                else
                    IsOkEnabled = false;

                RaisePropertyChanged(() => SelectedItemd);
            }
        }

        private bool _isOkEnabled;
        public bool IsOkEnabled
        {
            get
            {
                return _isOkEnabled;
            }
            set
            {
                if (_isOkEnabled == value)
                    return;
                _isOkEnabled = value;
                RaisePropertyChanged(()=>IsOkEnabled);
            }
        }

        private ArticleModel _Article;
        public ArticleModel Article
        {
            get
            {
                return _Article;
            }
            set
            {
                if (_Article == value)
                {
                    return;
                }

                _Article = value;
                RaisePropertyChanged(() => Article);
            }
        }

        private string _Sn;
        public string Sn
        {
            get
            {
                return _Sn;
            }
            set
            {
                if (_Sn == value)
                {
                    return;
                }

                _Sn = value;
                Article.SN = value;
                RaisePropertyChanged(() => Sn);
            }
        }

        public ICommand OkCommand { get; private set; }

        public ICommand EnterCommand { get; private set; }

        public AvoirViewModel()
        {
            Items= new ObservableCollection<VenteItemModel>();
            Article = new ArticleModel();
            OkCommand = new DelegateCommand(OnOk);
            EnterCommand = new DelegateCommand(OnEnter);
        }

        private void OnEnter()
        {
            Items = new ObservableCollection<VenteItemModel>();

            if (Sn == string.Empty)
            {
                return;
            }

            GetArticleId(Sn);
        }

        private void OnOk()
        {
            if (SelectedItemd == null)
            {
                Microsoft.Windows.Controls.MessageBox.Show("Pas d'article trouvé.", "A voir", System.Windows.MessageBoxButton.OKCancel, System.Windows.MessageBoxImage.Error);
                return;
            }
            MyDBEntities db = DbManager.CreateDbManager();
            Vente v = db.Vente.FirstOrDefault(ve => ve.ID==SelectedItemd.Id);

            long articleId = 0;
            long venteId = 0;
            string sn = "";
            int qt = 0;
            if (v != null)
            {
                venteId = v.ID;
                articleId = v.ArticleId.GetValueOrDefault();
                sn = v.SN;
                qt = v.QT.GetValueOrDefault();
            }

            if (articleId == 0)
            {
                Microsoft.Windows.Controls.MessageBox.Show("Pas d'article trouvé.", "A voir", System.Windows.MessageBoxButton.OKCancel, System.Windows.MessageBoxImage.Error);
                return;
            }
            GetSN(articleId);

            MessageBoxResult result = Microsoft.Windows.Controls.MessageBox.Show("Vous voulez retournez l'Article au Stock?", "A voir", System.Windows.MessageBoxButton.OKCancel, System.Windows.MessageBoxImage.Question);

            if (result == MessageBoxResult.Cancel)
            {
                return;
            }

            if (!Article.IsGenerique)
            {
                if (sn.Count(c => c.Equals(';')) == 1)
                {
                    if (!DeleteArticleFromVente(venteId))
                    {
                        Microsoft.Windows.Controls.MessageBox.Show("Probleme supression article.", "A voir", System.Windows.MessageBoxButton.OKCancel, System.Windows.MessageBoxImage.Error);
                        return;
                    }
                }
                else
                {
                    sn = sn.Remove(sn.IndexOf(Sn), Sn.Length);//sn = sn.Remove(sn.IndexOf(Article.SN), Article.SN.Length);
                    UpdateArticleInVente(venteId, sn, qt - 1);
                }
            }
            if (Article.IsGenerique)
            {
                if (qt == 1)
                {
                    DeleteArticleFromVente(venteId);
                }
                else
                {
                    UpdateGeneriqueArticle(venteId, qt - 1);
                }
            }

            string fileName = string.Format(@"LogAVoir\AVoir{0}.txt", DateTime.Now.ToShortDateString().Replace('/', '-'));
            StringBuilder sb = new StringBuilder();
            sb.Append(Article.Designation);
            sb.Append("     ");
            sb.Append(Article.SN);


            sb.Append("     ");
            sb.Append(DateTime.Now);
            sb.Append("     ");
            sb.Append(cUser.Login);

            Log.WriteLog(fileName, sb.ToString());

            if (!UpdateArticleInStock(articleId))
            {
                Microsoft.Windows.Controls.MessageBox.Show("Probleme mise à jour quantité dans le stocke.", "A voir", System.Windows.MessageBoxButton.OKCancel, System.Windows.MessageBoxImage.Error);
                return;
            }

            Microsoft.Windows.Controls.MessageBox.Show("Opération terminé avec succé", "A voir", System.Windows.MessageBoxButton.OKCancel, System.Windows.MessageBoxImage.Information);
            Sn = string.Empty;
            Article.Designation = string.Empty;
            SelectedItemd = null;
            Items = new ObservableCollection<VenteItemModel>();
        }

        private void UpdateGeneriqueArticle(long venteId, int? qt)
        {
            MyDBEntities db = DbManager.CreateDbManager();
            Vente vente = db.Vente.FirstOrDefault(v => v.ID == venteId);
            vente.QT = qt;
            db.SaveChanges();
        }

        private void UpdateArticleInVente(long venteId, string sn,int? qt)
        {
            string req = string.Format("Update  Vente set SN='{0}',QT={1} where ID={2}", sn,qt, venteId);
            MyDBEntities db = DbManager.CreateDbManager();
            Vente vente = db.Vente.FirstOrDefault(v => v.ID == venteId);
            vente.SN = sn;
            vente.QT = qt;
            db.SaveChanges();
        }

        private bool DeleteArticleFromVente(long venteId)
        {
            MyDBEntities db = DbManager.CreateDbManager();
            Vente vente = db.Vente.FirstOrDefault(v => v.ID == venteId);
            db.Vente.DeleteObject(vente);
            db.SaveChanges();
            return true;
        }

        private bool UpdateArticleInStock(long? articleId)
        {
            string NewSn = Article.SN;

            if (!NewSn.StartsWith(";"))
            {
                NewSn = ";" + NewSn;
            }
            MyDBEntities db = DbManager.CreateDbManager();
            Article article = db.Article.FirstOrDefault(a => a.ID == articleId);
            article.SN = NewSn;
            article.QT = Article.QT + 1;
            db.SaveChanges();
            return true;
        }

        private void GetSN(long? articleId)
        {
            MyDBEntities db = DbManager.CreateDbManager();
            Article article = db.Article.FirstOrDefault(a => a.ID == articleId);

            if(article!=null)
            {
                if (Article.SN != article.SN && Article.SN != ";" + article.SN && article.SN != ";" + Article.SN)
                    Article.SN = string.Format(";{0}{1}", Article.SN, article.SN);
                Article.QT = article.QT;
                Article.Designation = article.Designation;
                Article.IsGenerique = article.IsGenerique;
            }
        }

        private void GetArticleId(string sn)
        {
            MyDBEntities db = DbManager.CreateDbManager();
            Vente vente = db.Vente.FirstOrDefault(v => v.SN.Contains(sn));
            long? articleId = 0;
            long? venteId = 0;
            if(vente!=null)
            {
                venteId = vente.ID;
                articleId =vente.ArticleId;
            }
            GetArticleBySn(articleId);
           var items=db.Vente.Where(v => v.SN.Contains(sn)).ToList();

           foreach (var v in items)
           {
               VenteItemModel item= new VenteItemModel()
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
        }

        private void GetArticleBySn(long? articleId)
        {
            MyDBEntities db = DbManager.CreateDbManager();
            Article article = db.Article.FirstOrDefault(a => a.ID == articleId);
            if(article!=null)
            {
                Article.Designation = article.Designation;
            }
        }
    }
}