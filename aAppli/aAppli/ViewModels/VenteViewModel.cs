using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using aAppli.Models;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;

namespace aAppli.ViewModels
{
    public class VenteViewModel : NotificationObject
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

        private decimal _PrixVenteTotale;
        public decimal PrixVenteTotale
        {
            get
            {
                return _PrixVenteTotale;
            }

            set
            {
                if (_PrixVenteTotale == value)
                {
                    return;
                }

                _PrixVenteTotale = value;
                BeneficeTnd = PrixVenteTotale - PrixAchatTotale;
                RaisePropertyChanged(() => PrixVenteTotale);
            }
        }

        private decimal _PrixAchatTotale;
        public decimal PrixAchatTotale
        {
            get { return _PrixAchatTotale; }
            set
            {
                if (_PrixAchatTotale == value)
                {
                    return;
                }

                _PrixAchatTotale = value;
                BeneficeTnd = PrixVenteTotale - PrixAchatTotale;
                RaisePropertyChanged(() => PrixAchatTotale);
            }
        }

        private decimal _BeneficeTnd;
        public decimal BeneficeTnd
        {
            get { return _BeneficeTnd; }
            set
            {
                if (_BeneficeTnd == value)
                {
                    return;
                }

                _BeneficeTnd = value;
                RaisePropertyChanged(() => BeneficeTnd);
            }
        }

        private string _Benefice;
        public string Benefice
        {
            get { return _Benefice; }
            set
            {
                if (_Benefice == value)
                {
                    return;
                }

                _Benefice = value;
                RaisePropertyChanged(() => Benefice);
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

        string snn;
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

                if (value != null)
                    snn = value;
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
                            MessageBox.Show("Le SN : " + item + " Exsite deja.", "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                            SN = SN.Remove(SN.IndexOf(item) - 1, item.Length + 1);
                        }

                        if (Article != null && Article.IsGenerique)
                        {
                            if (item != "" && item != Article.SN.Substring(1))
                            {
                                MessageBox.Show("Le SN : " + item + " est different.", "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                                SN = SN.Remove(SN.IndexOf(item) - 1, item.Length + 1);

                            }
                        }

                        if (Article != null && !Article.IsGenerique && !SnExist(item))
                        {
                            MessageBox.Show("Le SN : " + item + " n'existe pas.", "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                            SN = SN.Remove(SN.IndexOf(item) - 1, item.Length + 1);
                        }
                    }
                }
                RaisePropertyChanged(() => SN);
            }
        }

        private bool SnExist(string sn)
        {
            string Sn = "";

            MyDBEntities db = DbManager.CreateDbManager();
            foreach (var item in db.Article.Where(a => a.EstablishmentId == cUser.EstablishmentId || cUser.EstablishmentName.Equals("Nouvelit", StringComparison.InvariantCultureIgnoreCase)).OrderBy(a => a.Designation))
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

        public VenteViewModel()
        {
            PrixVenteTotale = 0;
            PrixAchatTotale = 0;
            var profit = PrixAchatTotale == 0 ? 0 : (PrixVenteTotale - PrixAchatTotale) * 100m / PrixAchatTotale;
            Benefice = string.Format("{0} %", profit.ToString("00.00"));
            EnterCommand = new DelegateCommand(OnEnter);
            EffacerCommand = new DelegateCommand(OnEffacer);
            VendreCommand = new DelegateCommand(OnVendre);
            DeleteCommand = new DelegateCommand<ArticleModel>(OnDelete);

            ArticlesAVendre = new ObservableCollection<ArticleModel>();

            if (cUser.Vente_S && !cUser.Vente)
            {
                _PrixAchatVisibility = Visibility.Hidden;
            }
            else
                _PrixAchatVisibility = Visibility.Visible;
        }

        void Article_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            PrixVenteTotale = ArticlesAVendre.Sum(a => a.PrixVente * a.QTAVendre);
            PrixAchatTotale = ArticlesAVendre.Sum(a => a.PrixAchat * a.QTAVendre);

            var profit = PrixAchatTotale == 0 ? 0 : (PrixVenteTotale - PrixAchatTotale) * 100m / PrixAchatTotale;
            Benefice = string.Format("{0} %", profit.ToString("00.00"));
        }

        public ICommand EnterCommand { get; private set; }
        public ICommand EffacerCommand { get; private set; }
        public ICommand VendreCommand { get; set; }
        public ICommand DeleteCommand { get; private set; }

        private void OnDelete(ArticleModel art)
        {
            ArticlesAVendre.Remove(art);
            PrixVenteTotale -= art.PrixVente;
            PrixAchatTotale -= art.PrixAchat;
            var profit = PrixAchatTotale == 0 ? 0 : (PrixVenteTotale - PrixAchatTotale) * 100m / PrixAchatTotale;
            Benefice = string.Format("{0} %", profit.ToString("00.00"));
        }

        private void OnVendre()
        {
            foreach (var item in ArticlesAVendre)
            {
                if (item.QTAVendre > 0)
                    if (!VendreArticle(item))
                    {
                        Microsoft.Windows.Controls.MessageBox.Show("Opération non effectuée.", "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                        return;
                    }
            }
            Microsoft.Windows.Controls.MessageBox.Show("Opération términée avec success", "Success", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);

            string fileName = string.Format(@"LogSN\SN{0}.txt", DateTime.Now.ToShortDateString().Replace('/', '-'));
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("         #Vente#");
            foreach (var item in ArticlesAVendre)
            {
                sb.Append(item.SNAVendre);
                sb.Append("     ");
                sb.Append(DateTime.Now);
                sb.Append("     ");
                sb.Append(item.QTAVendre);
                sb.Append("     ");
                sb.Append(cUser.Login);
            }

            Log.WriteLog(fileName, sb.ToString());

            ArticlesAVendre = new ObservableCollection<ArticleModel>();
            OnEffacer();
        }


        private bool VendreArticle(ArticleModel article)
        {
            article.SN = GetSN(article.Id);


            //if (i > 0)
            //{
            //    Microsoft.Windows.Controls.MessageBox.Show("Opération términée avec success", "Success", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
            //}

            MyDBEntities db = DbManager.CreateDbManager();
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
                        sn = "";//sn = article.SN.Remove(article.SN.IndexOf(snn, StringComparison.InvariantCultureIgnoreCase) - 1, item.Length + 1);
                        article.SN = sn;
                    }
                }

                article.QT = GetQt(article.Id);
                if (!article.IsGenerique)
                {
                    if (article.SN.Count(c => c == ';') != article.QT - article.QTAVendre)
                    {
                        MessageBox.Show("Error de sauvgarde." + Environment.NewLine + "Nombre des SN et quantité non égaus.");
                        return false;
                    }
                }

                Article art = db.Article.FirstOrDefault(a => a.ID == article.Id);
                art.QT = (article.QT == 0 ? 0 : article.QT - article.QTAVendre);
                art.SN = sn;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Microsoft.Windows.Controls.MessageBox.Show(ex.Message, "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                return false;
            }

            try
            {

                Vente v = new Vente
                {
                    ArticleId = article.Id,
                    Designation = article.Designation,
                    PrixAchat = article.PrixAchat,
                    PrixVente = article.PrixVente,
                    SN = article.SNAVendre,
                    DateDeVente = DateTime.Now,
                    QT = article.QTAVendre,
                    EstablishmentId = cUser.EstablishmentId
                };

                db.Vente.AddObject(v);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Microsoft.Windows.Controls.MessageBox.Show(ex.Message, "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                return false;
            }

            return true;
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
        }

        private int GetQt(long articleId)
        {
            int qt = 0;

            MyDBEntities db = DbManager.CreateDbManager();
            Article art = db.Article.FirstOrDefault(a => a.ID == articleId);
            if (art != null)
            {
                qt = art.QT;
            }

            return qt;
        }

        private string GetSN(long articleId)
        {
            string sn = default(string);

            MyDBEntities db = DbManager.CreateDbManager();
            Article art = db.Article.FirstOrDefault(a => a.ID == articleId);
            if (art != null)
            {
                sn = art.SN;
            }

            return sn;
        }

        private void OnEnter()
        {
            IsBusy = true;

            string fileName = string.Format(@"LogSN\SN{0}.txt", DateTime.Now.ToShortDateString().Replace('/', '-'));
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("     #Vente recherche#");
            sb.Append(SN);
            sb.Append("     ");
            sb.Append(DateTime.Now);
            sb.Append("     ");
            sb.Append(cUser.Login);
            Log.WriteLog(fileName, sb.ToString());
            ArticleModel art = null;


            if (!SN.Contains(";"))
            {
                MyDBEntities db = DbManager.CreateDbManager();
                List<Article> art2s = db.Article.Where(a => a.EstablishmentId == cUser.EstablishmentId || cUser.EstablishmentName.Equals("Nouvelit", StringComparison.InvariantCultureIgnoreCase)).ToList();
                Article art2 = art2s.FirstOrDefault(a => a.SN != null && a.SN.Split(';').Contains(SN));
                if (art2 == null)
                {
                    Microsoft.Windows.Controls.MessageBox.Show("Aucun article trouvé.", "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                    IsBusy = false;
                    return;
                }
                art = new ArticleModel
                {
                    Id = art2.ID,
                    Designation = art2.Designation,
                    PrixAchat = art2.PrixAchat,
                    PrixVente = art2.PrixVente,
                    SN = art2.SN,
                    QT = art2.QT,
                    IsGenerique = art2.IsGenerique
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
                Article.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(Article_PropertyChanged);
                PrixVenteTotale += Article.PrixVente;
                PrixAchatTotale += Article.PrixAchat;
                var profit = PrixAchatTotale == 0 ? 0 : (PrixVenteTotale - PrixAchatTotale) * 100m / PrixAchatTotale;
                Benefice = string.Format("{0} %", profit.ToString("00.00"));
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
                Article.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(Article_PropertyChanged);
                PrixVenteTotale += Article.PrixVente * qt;
                PrixAchatTotale += Article.PrixAchat * qt;
                var profit = PrixAchatTotale == 0 ? 0 : (PrixVenteTotale - PrixAchatTotale) * 100m / PrixAchatTotale;
                Benefice = string.Format("{0} %", profit.ToString("00.00"));
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
