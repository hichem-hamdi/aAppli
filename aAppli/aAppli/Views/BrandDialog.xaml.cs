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
    public partial class BrandDialog : Window
    {
        public BrandDialog(ArticleModel article = null)
        {
            InitializeComponent();


            MyDBEntities db = DbManager.CreateDbManager();
            var brands = db.Brand.ToList();
            brands.Insert(0, new Brand { Id = 0, Name = "-- Select --" });
            cbBrands.ItemsSource = brands;
            if (article == null)
            {
                cbBrands.SelectedIndex = 0;
            }
            else
            {
                var selectedBrand = brands.Single(b => b.Id == article.SelectedBrand.Id);
                cbBrands.SelectedItem = selectedBrand;
            }
        }

        private void OKButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (cbBrands.SelectedIndex == 0)
                return;
            DialogResult = true;
        }
    }
}
