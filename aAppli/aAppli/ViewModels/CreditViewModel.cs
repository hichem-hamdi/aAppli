using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.ViewModel;
using aAppli.Models;
using System.Collections.ObjectModel;
using Microsoft.Practices.Prism.Commands;
using System.Windows.Input;
using System.Data.OleDb;
using System.IO;
using Microsoft.Windows.Controls;
using aAppli.Views;
using System.Windows;

namespace aAppli.ViewModels
{
    public class CreditViewModel : NotificationObject
    {

        private ObservableCollection<ArticleModel> _ArticlesAVendre;

        public ObservableCollection<ArticleModel> ArticlesAVendre
        {
            get
            {
                return _ArticlesAVendre;
            }
            set
            {
                if (_ArticlesAVendre == value)
                {
                    return;
                }

                _ArticlesAVendre = value;
                RaisePropertyChanged(() => ArticlesAVendre);
            }
        }

        private Visibility _PrixAchatVisibility;
        public Visibility PrixAchatVisibility
        {
            get
            {
                return _PrixAchatVisibility;
            }
            set
            {
                _PrixAchatVisibility = value;
                RaisePropertyChanged(() => PrixAchatVisibility);
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

        private string _Designation;
        public string Designation
        {
            get
            {
                return _Designation;
            }
            set
            {
                if (_Designation == value)
                {
                    return;
                }

                _Designation = value;
                RaisePropertyChanged(() => Designation);
            }
        }

        private string _NomClient;
        public string NomClient
        {
            get
            {
                return _NomClient;
            }
            set
            {
                if (value == null)
                {
                    _NomClient = value;
                    RaisePropertyChanged(() => NomClient);
                    return;
                }
                if (_NomClient == value.ToUpper())
                {
                    return;
                }

                _NomClient = value.ToUpper();
                RaisePropertyChanged(() => NomClient);
            }
        }

        private string _Coordonnees;
        public string Coordonnees
        {
            get
            {
                return _Coordonnees;
            }
            set
            {
                if (value == null)
                {
                    _Coordonnees = value;
                    RaisePropertyChanged(() => Coordonnees);
                    return;
                }

                if (_Coordonnees == value.ToUpper())
                {
                    return;
                }

                _Coordonnees = value.ToUpper();
                RaisePropertyChanged(() => Coordonnees);
            }
        }

        private string _ModePaiement;
        public string ModePaiement
        {
            get
            {
                return _ModePaiement;
            }
            set
            {
                if (value == null)
                {
                    _ModePaiement = value;
                    RaisePropertyChanged(() => ModePaiement);
                    return;
                }

                if (_ModePaiement == value.ToUpper())
                {
                    return;
                }

                _ModePaiement = value.ToUpper();
                RaisePropertyChanged(() => ModePaiement);
            }
        }

        public bool IsAccessoire { get; set; }
        public long ParentId { get; set; }

        private string _QtRestante;
        public string QtRestante
        {
            get
            {
                return _QtRestante;
            }
            set
            {
                if (_QtRestante == value)
                {
                    return;
                }

                _QtRestante = value;
                RaisePropertyChanged(() => QtRestante);
            }
        }

        private int _qt;
        public int qt
        {
            get
            {
                return _qt;
            }
            set
            {
                if (_qt == value)
                {
                    return;
                }

                _qt = value;
                if (Article != null)
                {
                    int sum = ArticlesAVendre.Where(a => a.Id == Article.Id).Sum(ar => ar.QTAVendre);
                    //   QtRestante = string.Format("Il vous reste : {0} article(s).", Article.QT - (qt == 0 ? sum+1 : sum+qt));
                    if (Article.QT < qt)
                        Microsoft.Windows.Controls.MessageBox.Show("Pas de quantité disponible.", "Stop", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Stop);
                }
                RaisePropertyChanged(() => qt);
            }
        }

        private decimal _PrixVente;
        public decimal PrixVente
        {
            get
            {
                return _PrixVente;
            }
            set
            {
                if (_PrixVente == value)
                {
                    return;
                }

                _PrixVente = value;
                if (PrixVente != 0)
                    Article.PrixVente = PrixVente;
                RaisePropertyChanged(() => PrixVente);
            }
        }

        private decimal _PrixAchat;
        public decimal PrixAchat
        {
            get
            {
                return _PrixAchat;
            }
            set
            {
                if (_PrixAchat == value)
                {
                    return;
                }

                _PrixAchat = value;
                RaisePropertyChanged(() => PrixAchat);
            }
        }

        private string _SN;
        public string SN
        {
            get
            {
                return _SN;
            }
            set
            {
                if (_SN == value)
                {
                    return;
                }

                _SN = value;
                if (SN != null && SN.StartsWith(";"))
                {
                    string[] tab = SN.Split(';');
                    List<string> tabe2 = new List<string>(); ;
                    foreach (var item in tab)
                    {
                        if (item == string.Empty || tabe2.Contains(item))
                        {
                            continue;
                        }

                        tabe2.Add(item);
                        if (Article != null && !Article.IsGenerique && tab.Count(s => s == item) > 1
                           || ArticlesAVendre.Where(a => a.SNAVendre.Contains(item)).Count() > 0
                            )
                        {
                            Microsoft.Windows.Controls.MessageBox.Show("Le SN : " + item + " Exsite deja.", "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                            SN = SN.Remove(SN.IndexOf(item) - 1, item.Length + 1);
                        }

                        if (Article != null && Article.IsGenerique)
                        {
                            if (item != "" && item != Article.SN.Substring(1))
                            {
                                Microsoft.Windows.Controls.MessageBox.Show("Le SN : " + item + " est different.", "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                                SN = SN.Remove(SN.IndexOf(item) - 1, item.Length + 1);

                            }
                        }

                        if (Article != null && !Article.IsGenerique && !SnExist(item))
                        {
                            Microsoft.Windows.Controls.MessageBox.Show("Le SN : " + item + " n'existe pas.", "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                            SN = SN.Remove(SN.IndexOf(item) - 1, item.Length + 1);
                        }
                    }
                }
                RaisePropertyChanged(() => SN);
            }
        }

        private bool SnExist(string sn)
        {
            MyDBEntities db = DbManager.CreateDbManager();

            string Sn = "";
            foreach (var item in db.Article.OrderBy(a => a.Designation))
            {
                Sn += item.SN;
            }
            return Sn.Contains(sn);
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

        public CreditViewModel()
        {

            EnterCommand = new DelegateCommand(OnEnter);
            EffacerCommand = new DelegateCommand(OnEffacer);
            VendreCommand = new DelegateCommand(OnVendre);
            HistoriqueCommand = new DelegateCommand(OnHistorique);
            DeleteCommand = new DelegateCommand<ArticleModel>(OnDelete);

            ArticlesAVendre = new ObservableCollection<ArticleModel>();

            if (cUser.VenteCredit_S && !cUser.VenteCredit)
            {
                _PrixAchatVisibility = Visibility.Hidden;
            }
            else
                _PrixAchatVisibility = Visibility.Visible;
        }

        public ICommand EnterCommand { get; private set; }
        public ICommand EffacerCommand { get; private set; }
        public ICommand VendreCommand { get; set; }
        public ICommand HistoriqueCommand { get; set; }
        public ICommand DeleteCommand { get; private set; }

        private void OnHistorique()
        {
            HistoriqueCreditView hst = new HistoriqueCreditView();
            hst.Show();
        }
        private void OnDelete(ArticleModel art)
        {
            ArticlesAVendre.Remove(art);
        }

        private void OnVendre()
        {
            string fileName = string.Format(@"LogSN\SN{0}.txt", DateTime.Now.ToShortDateString().Replace('/', '-'));
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("         #Vente Credit#");
            foreach (var item in ArticlesAVendre)
            {
                if (item.QTAVendre > 0)
                {
                    if (ParentId > 0)
                    {
                        item.ParentId = ParentId;
                        VendreArticle(item);
                    }
                    else
                    {
                        item.ParentId = 0;
                        VendreArticle(item);
                    }
                    sb.Append(item.SNAVendre);
                    sb.Append("     ");
                    sb.Append(DateTime.Now);
                    sb.Append("     ");
                    sb.Append(item.QTAVendre);
                    sb.Append("     ");
                    sb.Append(cUser.Login);
                }
            }

            Log.WriteLog(fileName, sb.ToString());
            ArticlesAVendre = new ObservableCollection<ArticleModel>();
            OnEffacer();

            Microsoft.Windows.Controls.MessageBox.Show("Opération términée avec success", "Success", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
        }


        private void VendreArticle(ArticleModel article)
        {
            MyDBEntities db = DbManager.CreateDbManager();

            try
            {
                Credit credit = new Credit
                {
                    ArticleId = article.Id,
                    Designation = article.Designation,
                    PrixAchat = article.PrixAchat,
                    PrixVente = article.PrixVente,
                    SN = article.SNAVendre,
                    DateDeVente = DateTime.Now,
                    QT = article.QTAVendre,
                    NomClient = NomClient,
                    Coordonnes = Coordonnees,
                    ModePaiement = ModePaiement,
                    ParentId = article.ParentId,
                    EstablishmentId = cUser.EstablishmentId
                };
                db.Credit.AddObject(credit);
                db.SaveChanges();

            }
            catch (Exception ex)
            {
                Microsoft.Windows.Controls.MessageBox.Show(ex.Message, "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }

            article.SN = GetSn(article.Id);

            try
            {
                //Update QT
                string sn = article.SN;
                if (!article.IsGenerique)
                {
                    string[] tab = article.SNAVendre.Split(';');
                    foreach (var item in tab)
                    {
                        if (item == "")
                        {
                            continue;
                        }
                        sn = article.SN.Remove(article.SN.IndexOf(item) - 1, item.Length + 1);
                        article.SN = sn;

                    }

                }

                if (article.IsGenerique && article.QT - article.QTAVendre == 0)
                {
                    string[] tab = article.SNAVendre.Split(';');
                    foreach (var item in tab)
                    {
                        if (item == "")
                            continue;
                        //sn = article.SN.Remove(article.SN.IndexOf(SN, StringComparison.InvariantCultureIgnoreCase) - 1, item.Length + 1);
                        sn = "";
                        article.SN = "";
                    }
                }

                article.QT = GetQt(article.Id);

                Article art = db.Article.FirstOrDefault(a => a.ID == article.Id);
                art.QT = (article.QT == 0 ? 0 : article.QT - article.QTAVendre);
                art.SN = sn;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Microsoft.Windows.Controls.MessageBox.Show(ex.Message, "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
        }

        private string GetSn(long articleId)
        {
            MyDBEntities db = DbManager.CreateDbManager();
            Article art = db.Article.FirstOrDefault(a => a.ID == articleId);

            string sn = "";
            if (art != null)
            {
                sn = art.SN;
            }

            return sn;
        }

        private void OnEffacer()
        {
            Designation = default(string);
            PrixVente = default(decimal);
            PrixAchat = default(decimal);
            SN = default(string);
            qt = default(int);
            Article = null;
            QtRestante = default(string);
            NomClient = default(string);
            Coordonnees = default(string);
            ModePaiement = default(string);
        }

        private int GetQt(long articleId)
        {
            MyDBEntities db = DbManager.CreateDbManager();
            Article art = db.Article.FirstOrDefault(a => a.ID == articleId);

            int qt = 0;
            if (art != null)
            {
                qt = art.QT;
            }

            return qt;
        }

        private void OnEnter()
        {
            IsBusy = true;

            // DBEntities db = new DBEntities();

            // Article art = db.Articles.FirstOrDefault(a => a.SN.Equals(SN));
            ArticleModel art = null;
            if (string.IsNullOrWhiteSpace(SN))
            {
                IsBusy = false;
                return;
            }

            if (!SN.Contains(";"))
            {
                MyDBEntities db = DbManager.CreateDbManager();
                List<Article> artis = db.Article.Where(a => a.EstablishmentId == cUser.EstablishmentId || cUser.EstablishmentName.Equals("Nouvelit", StringComparison.InvariantCultureIgnoreCase)).ToList();
                Article arti = artis.FirstOrDefault(a => a.SN.Split(';').Contains(SN));
                if (arti == null)
                {
                    Microsoft.Windows.Controls.MessageBox.Show("Aucun article trouvé.", "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                    IsBusy = false;
                    return;
                }
                art = new ArticleModel
                {
                    Id = arti.ID,
                    Designation = arti.Designation,
                    PrixAchat = arti.PrixAchat,
                    PrixVente = arti.PrixVente,
                    SN = arti.SN,
                    QT = arti.QT,
                    IsGenerique = arti.IsGenerique
                };



            }

            SN = SN.Insert(0, ";");
            if (SN == "")
            {
                IsBusy = false;
                return;
            }
            if (art != null)
            {
                Article = art;
                Designation = art.Designation;
                PrixVente = art.PrixVente;
                PrixAchat = art.PrixAchat;
                int sumqt = ArticlesAVendre.Where(a => a.Id == Article.Id).Sum(ar => ar.QTAVendre);
                if (art.QT - (qt == 0 ? sumqt + 1 : sumqt + qt) < 0)
                {
                    QtRestante = "Quantité disponible insuffisante.";
                }
                else
                    QtRestante = string.Format("Il vous reste : {0} article(s).", art.QT - (qt == 0 ? sumqt + 1 : sumqt + qt));
                if (art.QT < qt)
                {
                    Microsoft.Windows.Controls.MessageBox.Show("Pas de quantité disponible.", "Stop", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Stop);
                    IsBusy = false;
                    return;
                }
            }
            if (Article == null)
            {
                IsBusy = false;
                return;
            }

            int sum = ArticlesAVendre.Where(a => a.Id == Article.Id).Sum(aa => aa.QTAVendre) + (qt == 0 ? 1 : qt);
            if (Article != null && Article.QT < sum)
            {
                Microsoft.Windows.Controls.MessageBox.Show("Pas de quantité disponible.", "Stop", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Stop);
                IsBusy = false;
                return;
            }

            Article.SNAVendre = SN;

            if (qt == 0)
            {
                Article.QTAVendre = 1;
                ArticlesAVendre.Add(Article);
                Designation = default(string);
                PrixVente = default(decimal);
                PrixAchat = default(decimal);
                SN = default(string);
                qt = default(int);
                Article = null;

                IsBusy = false;
                return;
            }


            string[] tab = SN.Split(';');
            if (qt == SN.Count(c => c.Equals(';')) || Article.IsGenerique)
            {
                if (Article.QT < qt)
                {
                    Microsoft.Windows.Controls.MessageBox.Show("Pas de quantité disponible.", "Stop", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Stop);
                    IsBusy = false;
                    return;
                }
                Article.QTAVendre = qt;
                ArticlesAVendre.Add(Article);
                Designation = default(string);
                PrixVente = default(decimal);
                PrixAchat = default(decimal);
                SN = default(string);
                qt = default(int);
                Article = null;

            }

            IsBusy = false;
        }
    }
}
