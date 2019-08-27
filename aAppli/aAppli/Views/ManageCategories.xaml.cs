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
    public partial class ManageCategories : Window
    {
        public ManageCategories()
        {
            InitializeComponent();
            this.Title += " " + cUser.Login + " / " + cUser.EstablishmentName;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MyDBEntities db = DbManager.CreateDbManager();
            var categoryModel = new CategoryModel { Families = new System.Collections.ObjectModel.ObservableCollection<Famille>(db.Famille.ToList()) };
            categoryModel.Families.Insert(0, new Famille { Id = 0, Name = "-- Select --" });
            categoryModel.SelectedFamily = categoryModel.Families.First();
            (DataContext as ManageCategoriesViewModel).Categories.Add(categoryModel);
            myDataGrid.SelectedItem = (DataContext as ManageCategoriesViewModel).Categories[(DataContext as ManageCategoriesViewModel).Categories.Count - 1];
            myDataGrid.ScrollIntoView(myDataGrid.SelectedItem);
        }
    }
}
