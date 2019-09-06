using System.Windows;
using aAppli.ViewModels;
using System.Windows.Threading;
using System.Runtime.InteropServices;
using System;

namespace aAppli.Views
{
    /// <summary>
    /// Interaction logic for ManageEstablishments.xaml
    /// </summary>
    public partial class ManageBrands : Window
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true, CallingConvention = CallingConvention.Winapi)]
        public static extern short GetKeyState(int keyCode);

        private DispatcherTimer timer;

       public ManageBrands()
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
            (DataContext as ManageBrandsViewModel).Brands.Add(new Brand());
            myDataGrid.SelectedItem = (DataContext as ManageBrandsViewModel).Brands[(DataContext as ManageBrandsViewModel).Brands.Count - 1];
            myDataGrid.ScrollIntoView(myDataGrid.SelectedItem);
        }
    }
}
