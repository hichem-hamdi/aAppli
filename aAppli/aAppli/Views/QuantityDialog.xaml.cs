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
using System.Text.RegularExpressions;
using aAppli.Models;

namespace aAppli.Views
{
    /// <summary>
    /// Interaction logic for FamilleDialog.xaml
    /// </summary>
    public partial class QuantityDialog : Window
    {
        public QuantityDialog(ArticleModel article = null)
        {
            InitializeComponent();

            if (article == null)
                return;

            quantityNumber.Text = article.PicesQuantity.ToString();
        }

        private void OKButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(quantityNumber.Text) || !IsTextAllowed(quantityNumber.Text))
                return;
            DialogResult = true;
        }

        private static readonly Regex _regex = new Regex("[^0-9]+"); //regex that matches disallowed text
        private static bool IsTextAllowed(string text)
        {
            return !_regex.IsMatch(text);
        }
    }
}
