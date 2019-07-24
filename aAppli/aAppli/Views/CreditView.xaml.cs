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
using System.Runtime.InteropServices;
using System.Windows.Threading;
using aAppli.ViewModels;
using aAppli.Models;

namespace aAppli.Views
{
    /// <summary>
    /// Interaction logic for CreditView.xaml
    /// </summary>
    public partial class CreditView : Window
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true, CallingConvention = CallingConvention.Winapi)]
        public static extern short GetKeyState(int keyCode);

        private DispatcherTimer timer;

        public CreditView(CreditItemModel article)
        {
            InitializeComponent();
            if (article != null)
            {
                (DataContext as CreditViewModel).NomClient = article.NomClient;
                (DataContext as CreditViewModel).ModePaiement = article.ModePaiement;
                (DataContext as CreditViewModel).Coordonnees = article.Coordonnees;
                (DataContext as CreditViewModel).ParentId = article.Id;
                (DataContext as CreditViewModel).IsAccessoire = true;
            }

            if (cUser.VenteCredit_S && !cUser.VenteCredit)
            {
                txtPrixAchat.Visibility = System.Windows.Visibility.Hidden;
                lblPrixAchat.Visibility = System.Windows.Visibility.Hidden;
            }

            this.Title += " " + cUser.Login + " / " + cUser.EstablishmentName;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            sn.Focus();


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
    }
}
