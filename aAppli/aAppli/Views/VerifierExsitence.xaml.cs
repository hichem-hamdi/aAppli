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

namespace aAppli.Views
{
    /// <summary>
    /// Interaction logic for VerifierExsitence.xaml
    /// </summary>
    public partial class VerifierExsitence : Window
    {
        public VerifierExsitence()
        {
            InitializeComponent();
            this.Title += " " + cUser.Login + " / " + cUser.EstablishmentName;
        }

        public VerifierExsitence(string search)
        {
            InitializeComponent();

            MyDBEntities db = DbManager.CreateDbManager();
            var articles = db.Article.ToList();
            myDataGrid.ItemsSource = articles.Where(article => (article.Designation != null && article.Designation.Contains(search.ToUpper()) || (article.SN != null && article.SN.Split(';').Contains(search)))).ToList();
        }
    }
}
