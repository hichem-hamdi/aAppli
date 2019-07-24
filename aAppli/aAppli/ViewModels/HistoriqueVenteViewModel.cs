using Microsoft.Practices.Prism.ViewModel;
using System.Collections.ObjectModel;
using aAppli.Models;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using System.Data.OleDb;
using System.IO;

namespace aAppli.ViewModels
{
    public class HistoriqueVenteViewModel:NotificationObject
    {
        private ObservableCollection<VenteItemModel> _Items;
        public ObservableCollection<VenteItemModel> Items
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

        public HistoriqueVenteViewModel()
        {
            HistoriqueVenteViewLoadedCommand = new DelegateCommand(OnHistoriqueVenteViewLoaded);
            Items = new ObservableCollection<VenteItemModel>();
        }

        public ICommand HistoriqueVenteViewLoadedCommand { get;private set; }

        private void OnHistoriqueVenteViewLoaded()
        {
            MyDBEntities db = DbManager.CreateDbManager();
            foreach(var v in db.Vente)
            {
                VenteItemModel item = new VenteItemModel()
                {
                    Id =v.ID,
                    ArticleId = v.ArticleId.GetValueOrDefault(),
                    Designation = v.Designation,
                    PrixAchat = v.PrixAchat.GetValueOrDefault(),
                    PrixVente = v.PrixVente.GetValueOrDefault(),
                    SN = v.SN,
                    DateDeVente = v.DateDeVente.GetValueOrDefault(),
                    Quantite=v.QT.GetValueOrDefault()
                };
                Items.Add(item);
               
            }
        }
    }
}
