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

namespace aAppli.Views
{
    /// <summary>
    /// Interaction logic for EditInsertionView.xaml
    /// </summary>
    public partial class EditInsertionView : Window
    {
        public EditInsertionView()
        {
            InitializeComponent();
        }



        private void btnManageFamilies_Click(object sender, RoutedEventArgs e)
        {
            var manageFamilies = new ManageFamilies();
            manageFamilies.Show();
        }

        private void btnManageGategories_Click(object sender, RoutedEventArgs e)
        {
            var manageCategories = new ManageCategories();
            manageCategories.Show();
        }

        private void btnManageSubGategories_Click(object sender, RoutedEventArgs e)
        {
            var manageSubCategories = new ManageSubCategories();
            manageSubCategories.Show();
        }

        private void btnManageBrands_Click(object sender, RoutedEventArgs e)
        {
            var manageBrands = new ManageBrands();
            manageBrands.Show();
        }

        private void btnManageSizes_Click(object sender, RoutedEventArgs e)
        {
            var manageSizes = new ManageSizes();
            manageSizes.Show();
        }
        private void btnManageSuppliers_Click(object sender, RoutedEventArgs e)
        {
            var manageSuppliers = new ManageSuppliers();
            manageSuppliers.Show();
        }
    }
}
