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
    public partial class CategoryDialog : Window
    {
        public CategoryDialog(ArticleModel article = null)
        {
            InitializeComponent();


            MyDBEntities db = DbManager.CreateDbManager();
            var categories = db.Categorie.ToList();
            categories.Insert(0, new Categorie { Id = 0, Name = "-- Select --" });
            cbCategories.ItemsSource = categories;
            if (article == null)
            {
                cbCategories.SelectedIndex = 0;
            }
            else {
                var selectedCategory = categories.Single(c => c.Id == article.SelectedCategory.Id);
                cbCategories.SelectedItem = selectedCategory;
            }
        }

        private void OKButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (cbCategories.SelectedIndex == 0)
                return;
            DialogResult = true;
        }
    }
}
