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
    public partial class FamilleDialog : Window
    {
        public FamilleDialog(ArticleModel article = null)
        {
            InitializeComponent();

            MyDBEntities db = DbManager.CreateDbManager();
            var families = db.Famille.OrderBy(f => f.Name).ToList();
            families.Insert(0, new Famille { Id = 0, Name = "-- Select --" });
            cbFamilies.ItemsSource = families;
            if (article == null)
            {
                cbFamilies.SelectedIndex = 0;
            }
            else
            {
                var selectedFamily = families.Single(f => f.Id == article.SelectedFamily.Id);
                cbFamilies.SelectedItem = selectedFamily;
            }
        }

        private void OKButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (cbFamilies.SelectedIndex == 0)
                return;
            DialogResult = true;
        }
    }
}
