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
    public partial class SubCategoryDialog : Window
    {
        public SubCategoryDialog(ArticleModel article = null)
        {
            InitializeComponent();


            MyDBEntities db = DbManager.CreateDbManager();
            var subCategories = db.SOUS_CATEGORIE.OrderBy(s => s.Name).ToList();
            subCategories.Insert(0, new SOUS_CATEGORIE { Id = 0, Name = "-- Select --" });
            cbSubCategories.ItemsSource = subCategories;
            if (article == null)
            {
                cbSubCategories.SelectedIndex = 0;
            }
            else
            {
                var selectedSubCategory = subCategories.Single(s => s.Id == article.SelectedSubCategory.Id);
                cbSubCategories.SelectedItem = selectedSubCategory;
            }
        }

        private void OKButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (cbSubCategories.SelectedIndex == 0)
                return;
            DialogResult = true;
        }
    }
}
