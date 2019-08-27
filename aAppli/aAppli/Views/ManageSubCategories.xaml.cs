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
using aAppli.ViewModels;
using aAppli.Models;

namespace aAppli.Views
{
    /// <summary>
    /// Interaction logic for ManageEstablishments.xaml
    /// </summary>
    public partial class ManageSubCategories : Window
    {
        public ManageSubCategories()
        {
            InitializeComponent();
            this.Title += " " + cUser.Login + " / " + cUser.EstablishmentName;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MyDBEntities db = DbManager.CreateDbManager();
            var subCategoryModel = new SubCategoryModel { Categories = new System.Collections.ObjectModel.ObservableCollection<Categorie>(db.Categorie.ToList()) };
            subCategoryModel.Categories.Insert(0, new Categorie { Id = 0, Name = "-- Select --" });
            subCategoryModel.SelectedCategory = subCategoryModel.Categories.First();

            (DataContext as ManageSubCategoriesViewModel).SubCategories.Add(subCategoryModel);
            myDataGrid.SelectedItem = (DataContext as ManageSubCategoriesViewModel).SubCategories[(DataContext as ManageSubCategoriesViewModel).SubCategories.Count - 1];
            myDataGrid.ScrollIntoView(myDataGrid.SelectedItem);
        }
    }
}
