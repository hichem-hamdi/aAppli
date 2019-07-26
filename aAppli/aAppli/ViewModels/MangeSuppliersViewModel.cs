using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;

namespace aAppli.ViewModels
{
    public class ManageSuppliersViewModel : NotificationObject
    {
        public ManageSuppliersViewModel()
        {
            _Suppliers = new ObservableCollection<Fournisseur>();
            LoadedCommand = new DelegateCommand(OnLoaded);
            SaveCommand = new DelegateCommand<Fournisseur>(OnSave);
            DeleteCommand = new DelegateCommand<Fournisseur>(OnDelete);
        }

        private ObservableCollection<Fournisseur> _Suppliers;
        public ObservableCollection<Fournisseur> Suppliers
        {
            get
            {
                return _Suppliers;
            }
            set
            {
                if (_Suppliers == value)
                {
                    return;
                }

                _Suppliers = value;
                RaisePropertyChanged(() => Suppliers);
            }
        }

        public ICommand LoadedCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        private void OnLoaded()
        {
            MyDBEntities db = DbManager.CreateDbManager();
            Suppliers = new ObservableCollection<Fournisseur>(db.Fournisseur.OrderBy(f => f.Name).ToList());
        }

        private void OnSave(Fournisseur supplier)
        {
            MyDBEntities db = DbManager.CreateDbManager();

            if (db.Fournisseur.Any(f => f.Name.ToLower().Equals(supplier.Name.ToLower())))
            {
                Microsoft.Windows.Controls.MessageBox.Show("Un fournisseur avec le même nom existe déjà!", "Stop", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Stop);
                return;
            }

            if (supplier.Id == 0)
            {
                try
                {
                    db.Fournisseur.AddObject(supplier);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    Microsoft.Windows.Controls.MessageBox.Show(ex.Message, "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                    return;
                }
                Microsoft.Windows.Controls.MessageBox.Show("Saved", "Save", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
            }
            else
            {
                try
                {

                    Fournisseur est = db.Fournisseur.FirstOrDefault(u => u.Id == supplier.Id);
                    if (est != null)
                    {
                        est.Name = supplier.Name;
                    }

                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    Microsoft.Windows.Controls.MessageBox.Show(ex.Message, "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                    return;
                }


                Microsoft.Windows.Controls.MessageBox.Show("Updated", "Update", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);

            }

            Suppliers = new ObservableCollection<Fournisseur>();
            OnLoaded();
        }

        private void OnDelete(Fournisseur supplier)
        {
            MessageBoxResult result = Microsoft.Windows.Controls.MessageBox.Show("Vous confirmer le Delete de ce fournisseur?", "Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.No)
            {

                return;
            }

            //  DBEntities db = new DBEntities();

            if (supplier.Id == 0)
            {
                Suppliers.Remove(supplier);
                return;
            }
            try
            {
                MyDBEntities db = DbManager.CreateDbManager();
                Fournisseur est = db.Fournisseur.FirstOrDefault(u => u.Id == supplier.Id);
                db.Fournisseur.DeleteObject(est);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Microsoft.Windows.Controls.MessageBox.Show(ex.Message, "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                return;
            }
            Microsoft.Windows.Controls.MessageBox.Show("Deleted", "Delete", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);

            Suppliers = new ObservableCollection<Fournisseur>();
            OnLoaded();
        }
    }
}
