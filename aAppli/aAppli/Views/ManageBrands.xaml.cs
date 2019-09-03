using System.Windows;
using aAppli.ViewModels;

namespace aAppli.Views
{
    /// <summary>
    /// Interaction logic for ManageEstablishments.xaml
    /// </summary>
    public partial class ManageBrands : Window
    {
       public ManageBrands()
        {
            InitializeComponent();
            this.Title += " " + cUser.Login + " / " + cUser.EstablishmentName;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            (DataContext as ManageBrandsViewModel).Brands.Add(new Brand());
            myDataGrid.SelectedItem = (DataContext as ManageBrandsViewModel).Brands[(DataContext as ManageBrandsViewModel).Brands.Count - 1];
            myDataGrid.ScrollIntoView(myDataGrid.SelectedItem);
        }
    }
}
