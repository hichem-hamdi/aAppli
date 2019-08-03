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
    /// Interaction logic for FamilleDialog.xaml
    /// </summary>
    public partial class PurchaseDateDialog : Window
    {
        public PurchaseDateDialog(ArticleModel article = null)
        {
            InitializeComponent();

            if (article == null)
                return;

            purchaseDate.SelectedDate = article.PurchaseDate;
        }

        private void OKButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (purchaseDate.SelectedDate == null)
                return;
            DialogResult = true;
        }
    }
}
