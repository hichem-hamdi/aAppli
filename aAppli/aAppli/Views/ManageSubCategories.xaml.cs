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
using System.Runtime.InteropServices;
using System.Windows.Threading;

namespace aAppli.Views
{
    /// <summary>
    /// Interaction logic for ManageEstablishments.xaml
    /// </summary>
    public partial class ManageSubCategories : Window
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true, CallingConvention = CallingConvention.Winapi)]
        public static extern short GetKeyState(int keyCode);
        private DispatcherTimer timer;

        public ManageSubCategories()
        {
            InitializeComponent();
            this.Title += " " + cUser.Login + " / " + cUser.EstablishmentName;

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(0.5);
            timer.Tick += timer1_Tick;
            timer.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            bool CapsLock = (((ushort)GetKeyState(0x14)) & 0xffff) != 0;
            if (CapsLock)
            {
                busy.IsBusy = true;
                busy.BusyContent = "Touche Majuscule";
                return;
            }

            busy.IsBusy = false;
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
