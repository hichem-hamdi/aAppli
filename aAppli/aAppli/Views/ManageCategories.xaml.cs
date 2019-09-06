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
    public partial class ManageCategories : Window
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true, CallingConvention = CallingConvention.Winapi)]
        public static extern short GetKeyState(int keyCode);

        private DispatcherTimer timer;

        public ManageCategories()
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
            var categoryModel = new CategoryModel { Families = new System.Collections.ObjectModel.ObservableCollection<Famille>(db.Famille.ToList().OrderBy(f => f.Name)) };
            categoryModel.Families.Insert(0, new Famille { Id = 0, Name = "-- Select --" });
            categoryModel.SelectedFamily = categoryModel.Families.First();
            (DataContext as ManageCategoriesViewModel).Categories.Add(categoryModel);
            myDataGrid.SelectedItem = (DataContext as ManageCategoriesViewModel).Categories[(DataContext as ManageCategoriesViewModel).Categories.Count - 1];
            myDataGrid.ScrollIntoView(myDataGrid.SelectedItem);
        }
    }
}
