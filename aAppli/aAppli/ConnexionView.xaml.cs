using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;
using System.Collections.Generic;

namespace aAppli
{
    /// <summary>
    /// Interaction logic for ConnexionView.xaml
    /// </summary>
    public partial class ConnexionView : Window
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true, CallingConvention = CallingConvention.Winapi)]
        public static extern short GetKeyState(int keyCode);

        public ConnexionView()
        {
            InitializeComponent();

            txtLogin.Focus();

        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            VerifyCredentials();
        }

        private void VerifyCredentials()
        {
            MyDBEntities db = DbManager.CreateDbManager();
            User user = db.Users.FirstOrDefault(u => u.Login.Equals(txtLogin.Text) && u.Pass.Equals(txtPass.Password));

            if (user == null)
            {
                MessageBox.Show("Mot de passe ou/et login invalide.");
                txtLogin.Text = null;
                txtPass.Password = null;
                txtLogin.Focus();
            }

            else
            {
                cUser.Id = user.Id;
                cUser.Stock = user.Stock.GetValueOrDefault();
                cUser.Vente = user.Vente.GetValueOrDefault();
                cUser.Vente_S = user.Vente_S.GetValueOrDefault();
                cUser.HistoriqueVente = user.HistoriqueVente.GetValueOrDefault();
                cUser.AVoir = user.AVoir.GetValueOrDefault();
                cUser.Inventaire = user.Inventaire.GetValueOrDefault();
                cUser.VenteCredit = user.VenteCredit.GetValueOrDefault();
                cUser.VenteCredit_S = user.VenteCredit_S.GetValueOrDefault();
                cUser.GererAcces = user.GererAcces.GetValueOrDefault();
                cUser.ViderBase = user.ViderBase.GetValueOrDefault();
                cUser.Login = user.Login;
                cUser.EstablishmentId = user.EstablishmentId.GetValueOrDefault();
                cUser.EditInsertion = user.EditInsertion.GetValueOrDefault();
                cUser.EstablishmentName = user.Establishment != null ? user.Establishment.Name : string.Empty;
                MainWindow main = new MainWindow();
                main.Show();
                this.Close();
                return;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            // cUser.IsAdmin = false;

            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }
    }

    public static class cUser
    {
        public static long Id { get; set; }
        public static bool Stock { get; set; }
        public static bool Vente { get; set; }
        public static bool Vente_S { get; set; }
        public static bool HistoriqueVente { get; set; }
        public static bool AVoir { get; set; }
        public static bool Inventaire { get; set; }
        public static bool VenteCredit { get; set; }
        public static bool VenteCredit_S { get; set; }
        public static bool GererAcces { get; set; }
        public static bool ViderBase { get; set; }
        public static bool EditInsertion     { get; set; }
        public static string Login { get; set; }
        public static long EstablishmentId { get; set; }
        public static string EstablishmentName { get; set; }
    }
}
