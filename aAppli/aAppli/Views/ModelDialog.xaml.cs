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
    public partial class ModelDialog : Window
    {
        public ModelDialog(ArticleModel article = null)
        {
            InitializeComponent();


            MyDBEntities db = DbManager.CreateDbManager();
            var models = db.Models.Where(m => m.SubCategoryId == article.SelectedSubCategory.Id).OrderBy(b => b.Name).ToList();
            models.Insert(0, new Model { Id = 0, Name = "-- Select --" });
            cbModels.ItemsSource = models;
            if (article == null || article.SelectedModel == null)
            {
                cbModels.SelectedIndex = 0;
            }
            else
            {
                var selectedModel = models.Single(b => b.Id == article.SelectedModel.Id);
                cbModels.SelectedItem = selectedModel;
            }
        }

        private void OKButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (cbModels.SelectedIndex == 0)
                return;
            DialogResult = true;
        }
    }
}
