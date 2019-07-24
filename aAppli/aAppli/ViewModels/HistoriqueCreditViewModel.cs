using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using aAppli.Models;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;

namespace aAppli.ViewModels
{
    public class HistoriqueCreditViewModel : NotificationObject
    {
        private ObservableCollection<CreditItemModel> _Items;
        public ObservableCollection<CreditItemModel> Items
        {
            get
            {
                return _Items;
            }
            set
            {
                if (_Items == value)
                {
                    return;
                }

                _Items = value;
                RaisePropertyChanged(() => Items);
            }
        }

        public ObservableCollection<CreditItemModel> InitItems;
        public ObservableCollection<CreditItemModel> AccItems;

        public HistoriqueCreditViewModel()
        {
            HistoriqueVenteViewLoadedCommand = new DelegateCommand(OnHistoriqueVenteViewLoaded);
            DeleteCommand = new DelegateCommand<CreditItemModel>(OnDelete);
            SaveCommand = new DelegateCommand<CreditItemModel>(OnSave);

            Items = new ObservableCollection<CreditItemModel>();
            InitItems = new ObservableCollection<CreditItemModel>();
            AccItems = new ObservableCollection<CreditItemModel>();

        }

        public ICommand HistoriqueVenteViewLoadedCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public ICommand SaveCommand { get; private set; }

        private void OnDelete(CreditItemModel item)
        {
            MessageBoxResult result = Microsoft.Windows.Controls.MessageBox.Show("Vous confirmer le Delete de cet article?", "Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.No)
            {
                return;
            }
            try
            {
                MyDBEntities db = DbManager.CreateDbManager();
                Credit c = db.Credit.First(cre => cre.ID == item.Id);
                db.Credit.DeleteObject(c);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Microsoft.Windows.Controls.MessageBox.Show(ex.Message, "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                return;
            }

            Microsoft.Windows.Controls.MessageBox.Show("Deleted", "Delete", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);


            Items = new ObservableCollection<CreditItemModel>();
            InitItems = new ObservableCollection<CreditItemModel>();
            AccItems = new ObservableCollection<CreditItemModel>();

            OnHistoriqueVenteViewLoaded();

        }

        private void OnSave(CreditItemModel item)
        {

            try
            {
                MyDBEntities db = DbManager.CreateDbManager();
                Credit credit = db.Credit.FirstOrDefault(c => c.ID == item.Id);
                credit.Designation = item.Designation;
                credit.PrixAchat = item.PrixAchat;
                credit.PrixVente = item.PrixVente;
                credit.SN = item.SN;
                credit.QT = item.Quantite;
                credit.NomClient = item.NomClient;
                credit.Coordonnes = item.Coordonnees;
                credit.ModePaiement = item.ModePaiement;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Microsoft.Windows.Controls.MessageBox.Show(ex.Message, "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                return;
            }
            Microsoft.Windows.Controls.MessageBox.Show("Updated", "Update", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);


            Items = new ObservableCollection<CreditItemModel>();
            InitItems = new ObservableCollection<CreditItemModel>();
            AccItems = new ObservableCollection<CreditItemModel>();

            OnHistoriqueVenteViewLoaded();
        }

        private void OnHistoriqueVenteViewLoaded()
        {
            MyDBEntities db = DbManager.CreateDbManager();
            foreach (var i in db.Credit.Where(c => c.EstablishmentId == cUser.EstablishmentId || cUser.EstablishmentName.Equals("Nouvelit", StringComparison.InvariantCultureIgnoreCase)))
            {
                CreditItemModel item = new CreditItemModel()
                {
                    Id = i.ID,
                    ArticleId = i.ArticleId.GetValueOrDefault(),
                    Designation = i.Designation,
                    PrixAchat = i.PrixAchat.GetValueOrDefault(),
                    PrixVente = i.PrixVente.GetValueOrDefault(),
                    SN = i.SN,
                    DateDeVente = i.DateDeVente.GetValueOrDefault(),
                    Quantite = i.QT.GetValueOrDefault(),
                    NomClient = i.NomClient,
                    Coordonnees = i.Coordonnes,
                    ModePaiement = i.ModePaiement,
                    ParentId = i.ParentId.GetValueOrDefault()
                };

                if (item.ParentId == 0)
                {
                    Items.Add(item);
                    InitItems.Add(item);
                }
                else
                    AccItems.Add(item);
            }
        }
    }
}
