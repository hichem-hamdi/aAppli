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

namespace aAppli.Views
{
    /// <summary>
    /// Interaction logic for ManageEstablishments.xaml
    /// </summary>
    public partial class ManageModels : Window
    {
        public ManageModels()
        {
            InitializeComponent();
            this.Title += " " + cUser.Login + " / " + cUser.EstablishmentName;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MyDBEntities db = DbManager.CreateDbManager();
            var modelsModel = new ModelsModel { Brands = new System.Collections.ObjectModel.ObservableCollection<Brand>(db.Brand.ToList().OrderBy(b => b.Name)) };
            modelsModel.Brands.Insert(0, new Brand { Id = 0, Name = "-- Select --" });
            modelsModel.SelectedBrand = modelsModel.Brands.First();
            (DataContext as ManageModelsViewModel).Models.Add(modelsModel);
            myDataGrid.SelectedItem = (DataContext as ManageModelsViewModel).Models[(DataContext as ManageModelsViewModel).Models.Count - 1];
            myDataGrid.ScrollIntoView(myDataGrid.SelectedItem);
        }
    }
}
