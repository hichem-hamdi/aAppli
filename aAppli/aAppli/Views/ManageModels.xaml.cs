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
using System.Runtime.InteropServices;
using System.Windows.Threading;

namespace aAppli.Views
{
    /// <summary>
    /// Interaction logic for ManageEstablishments.xaml
    /// </summary>
    public partial class ManageModels : Window
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true, CallingConvention = CallingConvention.Winapi)]
        public static extern short GetKeyState(int keyCode);
        private DispatcherTimer timer;

        public ManageModels()
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
            var modelsModel = new ModelsModel { SubCategories = new System.Collections.ObjectModel.ObservableCollection<SOUS_CATEGORIE>(db.SOUS_CATEGORIE.ToList().OrderBy(b => b.Name)) };
            modelsModel.SubCategories.Insert(0, new SOUS_CATEGORIE { Id = 0, Name = "-- Select --" });
            modelsModel.SelectedSubCategory = modelsModel.SubCategories.First();
            (DataContext as ManageModelsViewModel).Models.Add(modelsModel);
            myDataGrid.SelectedItem = (DataContext as ManageModelsViewModel).Models[(DataContext as ManageModelsViewModel).Models.Count - 1];
            myDataGrid.ScrollIntoView(myDataGrid.SelectedItem);
        }
    }
}
