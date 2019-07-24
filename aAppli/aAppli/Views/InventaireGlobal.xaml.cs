using System.Windows;
using System.Windows.Forms;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Text;

namespace aAppli.Views
{
    /// <summary>
    /// Interaction logic for InventaireGlobal.xaml
    /// </summary>
    public partial class InventaireGlobal : Window
    {
        public InventaireGlobal()
        {
            InitializeComponent();
            this.Title += " " + cUser.Login + " / " + cUser.EstablishmentName;
        }

        string selectedFile = string.Empty;
        List<ArticleManquant> manquants;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog selectFileDialog = new OpenFileDialog();
            selectFileDialog.Filter = "txt | *.txt";

            if (selectFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                manquants = new List<ArticleManquant>();
                txtFileName.Text = selectedFile = selectFileDialog.FileName;
                btnVerify.IsEnabled = true;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var sns = new StringBuilder();
            var articles = new List<Article>();
            var articlesNonLister = new List<Article>();
            manquants = new List<ArticleManquant>();
            gbArticleManquant3.Visibility = gbArticleManquant2.Visibility = gbArticleManquant.Visibility = System.Windows.Visibility.Visible;
            gbSNNonReconnu.Visibility = System.Windows.Visibility.Visible;
            string ligne = string.Empty;
            Dictionary<long, List<string>> dict = new Dictionary<long, List<string>>();
            Dictionary<long, int> dictGenerique = new Dictionary<long, int>();
            var serials = new HashSet<string>();
            var serialsRepeated = new HashSet<ArticleManquant>();
            MyDBEntities db = DbManager.CreateDbManager();

            var isCompo = false;
            var nbrNonReconnus = 0;

            using (StreamReader sr = new StreamReader(selectedFile))
            {
                while (sr.Peek() >= 0)
                {
                    isCompo = false;
                    ligne = sr.ReadLine();
                    var serial = string.Empty;
                    var nbr = 0;
                    if (string.IsNullOrWhiteSpace(ligne))
                    {
                        continue;
                    }
                    else if (ligne.Contains(':'))
                    {
                        isCompo = true;
                        serial = ligne.Split(':')[0];
                        nbr = int.Parse(ligne.Split(':')[1]);
                    }
                    else
                    {
                        serial = ligne;
                    }

                    //var article = (from art in db.Article
                    //               where art.SN.Split(';').Contains(serial)
                    //               select art).FirstOrDefault();
                    var article = db.Article.Where(a => a.EstablishmentId == cUser.EstablishmentId || cUser.EstablishmentName.Equals("Nouvelit", StringComparison.InvariantCultureIgnoreCase)).ToList().FirstOrDefault(a => a.SN.Split(';').Contains(serial));
                    articles.Add(article);

                    if (!serials.Add(serial) && article != null && !article.IsGenerique)
                    {
                        var SNss = new ArticleManquant
                        {
                            SN = serial,
                            Article = article.Designation,
                            QT = article.QT
                        };

                        serialsRepeated.Add(SNss);
                    }

                    if (article != null)
                    {
                        if (article.IsGenerique)
                        {
                            //if (article.QT != nbr)
                            //{
                            //    var manquant = new ArticleManquant
                            //    {
                            //        Article = article.Designation,
                            //        SN = article.SN.Trim(';'),
                            //        QT = article.QT - nbr
                            //    };
                            //    manquants.Add(manquant);
                            //}
                            nbr = nbr == 0 ? 1 : nbr;
                            AddItemGenerique(dictGenerique, article, nbr);
                            //continue;
                        }



                        if (dict.ContainsKey(article.ID))
                        {
                            var hashedSet = dict[article.ID];
                            hashedSet.Add(ligne);
                        }
                        else
                        {
                            var hashedSet = new List<string>();
                            hashedSet.Add(ligne);
                            dict.Add(article.ID, hashedSet);
                        }
                    }
                    else
                    {
                        nbrNonReconnus++;
                        sns.AppendLine(ligne);
                    }

                }
            }

            foreach (var item in dict)
            {
                var article = db.Article.FirstOrDefault(a => a.ID == item.Key);
                if (article != null)
                {
                    string sn = article.SN;
                    int nbr = 0;
                    if (article.IsGenerique && dictGenerique.ContainsKey(article.ID))
                    {
                        nbr = dictGenerique[article.ID];
                    }
                    else
                    {
                        nbr = item.Value.Count;
                    }
                    if (article.QT != nbr)
                    {
                        if (!article.IsGenerique)
                            item.Value.ForEach(s =>
                            {
                                if (sn.IndexOf(s) > 0)
                                    sn = sn.Remove(sn.IndexOf(s), s.Length);
                            });
                        //if (article.IsGenerique)
                        //    sn = article.SN;
                        var manquant = new ArticleManquant
                        {
                            Article = article.Designation,
                            SN = sn.Trim(';'),
                            QT = article.QT - nbr
                        };
                        manquants.Add(manquant);
                    }
                }

            }

            articlesNonLister = db.Article.Where(a => a.QT > 0).ToList().Except(articles).ToList();
            var articlesDesignation = new List<ArticleDesignation>();
            articlesNonLister.OrderBy(a => a.Designation).ToList().ForEach(a =>
            {
                var article = new ArticleDesignation
                {
                    Designation = a.Designation
                };

                articlesDesignation.Add(article);
            });

            gdArticles2.ItemsSource = articlesDesignation.OrderBy(a => a.Designation).ToList();
            lblNbrNonLister.Content = string.Format("Articles non listés dans le fichier, Nbr = {0}", articlesDesignation.Count);

            gdArticles.ItemsSource = manquants.OrderBy(a => a.Article).ToList();
            lblNbrManquant.Content = string.Format("Articles manquants dans le stock magasin, Nbr = {0}", manquants.Count);

            gdArticles3.ItemsSource = serialsRepeated.ToList().OrderBy(a => a.Article).ToList();
            // gdSNNonReconnu.ItemsSource = sns;
            txtSN.Text = sns.ToString();
            lblNonReconnus.Content = string.Format("SNs non reconnus par le logiciel, Nbr = {0}", nbrNonReconnus);
        }

        private void AddItemGenerique(Dictionary<long, int> dict, Article article, int count)
        {
            if (dict.ContainsKey(article.ID))
            {
                var nbr = dict[article.ID] + count;
                dict[article.ID] = nbr;
                return;
            }

            dict[article.ID] = count;
        }
    }

    public class ArticleManquant
    {
        public string Article { get; set; }
        public string SN { get; set; }
        public int QT { get; set; }
    }

    public class SNNonReconnu
    {
        public string SN { get; set; }
    }

    public class ArticleDesignation
    {
        public string Designation { get; set; }
    }

    public class SNs
    {
        public string SN { get; set; }
    }
}
