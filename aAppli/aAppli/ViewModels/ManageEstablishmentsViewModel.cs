using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.ViewModel;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using System.Windows;

namespace aAppli.ViewModels
{
    public class ManageEstablishmentsViewModel : NotificationObject
    {
        public ManageEstablishmentsViewModel()
        {
            _Establishments = new ObservableCollection<Establishment>();
            LoadedCommand = new DelegateCommand(OnLoaded);
            SaveCommand = new DelegateCommand<Establishment>(OnSave);
            DeleteCommand = new DelegateCommand<Establishment>(OnDelete);
        }

        private ObservableCollection<Establishment> _Establishments;
        public ObservableCollection<Establishment> Establishments
        {
            get
            {
                return _Establishments;
            }
            set
            {
                if (_Establishments == value)
                {
                    return;
                }

                _Establishments = value;
                RaisePropertyChanged(() => Establishments);
            }
        }

        public ICommand LoadedCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        private void OnLoaded()
        {
            MyDBEntities db = DbManager.CreateDbManager();
            Establishments = new ObservableCollection<Establishment>(db.Establishment.ToList().OrderBy(e => e.Name));
        }

        private void OnSave(Establishment establishment)
        {
            MyDBEntities db = DbManager.CreateDbManager();
            if (establishment.Id == 0)
            {
                try
                {
                    db.Establishment.AddObject(establishment);
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

                    Establishment est = db.Establishment.FirstOrDefault(u => u.Id == establishment.Id);
                    if (est != null)
                    {
                        est.Name = establishment.Name;
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

            Establishments = new ObservableCollection<Establishment>();
            OnLoaded();
        }

        private void OnDelete(Establishment establishment)
        {
            MessageBoxResult result = Microsoft.Windows.Controls.MessageBox.Show("Vous confirmer le Delete de cet établissement?", "Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.No)
            {

                return;
            }

            //  DBEntities db = new DBEntities();

            if (establishment.Id == 0)
            {
                Establishments.Remove(establishment);
                return;
            }
            try
            {
                MyDBEntities db = DbManager.CreateDbManager();
                Establishment est = db.Establishment.FirstOrDefault(u => u.Id == establishment.Id);
                db.Establishment.DeleteObject(est);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Microsoft.Windows.Controls.MessageBox.Show(ex.Message, "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                return;
            }
            Microsoft.Windows.Controls.MessageBox.Show("Deleted", "Delete", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);

            Establishments = new ObservableCollection<Establishment>();
            OnLoaded();
        }
    }
}
