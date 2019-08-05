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
    public partial class SupplierDialog : Window
    {
        public SupplierDialog(ArticleModel article = null)
        {
            InitializeComponent();


            MyDBEntities db = DbManager.CreateDbManager();
            var suppliers = db.Fournisseur.OrderBy(f => f.Name).ToList();
            suppliers.Insert(0, new Fournisseur { Id = 0, Name = "-- Select --" });
            cbSuppliers.ItemsSource = suppliers;
            if (article == null)
            {
                cbSuppliers.SelectedIndex = 0;
            }
            else
            {
                var selectedSupplier = suppliers.Single(s => s.Id == article.SelectedSupplier.Id);
                cbSuppliers.SelectedItem = selectedSupplier;
            }
        }

        private void OKButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (cbSuppliers.SelectedIndex == 0)
                return;
            DialogResult = true;
        }
    }
}
