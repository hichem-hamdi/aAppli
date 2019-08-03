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
    public partial class SizeDialog : Window
    {
        public SizeDialog(ArticleModel article = null)
        {
            InitializeComponent();


            MyDBEntities db = DbManager.CreateDbManager();
            var sizes = db.Size.ToList();
            sizes.Insert(0, new Size { Id = 0, Name = "-- Select --" });
            cbSizes.ItemsSource = sizes;
            if (article == null)
            {
                cbSizes.SelectedIndex = 0;
            }
            else
            {
                var selectedSize = sizes.Single(s => s.Id == article.SelectedSize.Id);
                cbSizes.SelectedItem = selectedSize;
            }
        }

        private void OKButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (cbSizes.SelectedIndex == 0)
                return;
            DialogResult = true;
        }
    }
}
