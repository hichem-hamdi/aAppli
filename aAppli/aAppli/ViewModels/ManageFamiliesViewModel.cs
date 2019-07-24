using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;

namespace aAppli.ViewModels
{
    public class ManageFamiliesViewModel : NotificationObject
    {
        public ManageFamiliesViewModel()
        {
            _Families = new ObservableCollection<Famille>();
            LoadedCommand = new DelegateCommand(OnLoaded);
            SaveCommand = new DelegateCommand<Famille>(OnSave);
            DeleteCommand = new DelegateCommand<Famille>(OnDelete);
        }

        private ObservableCollection<Famille> _Families;
        public ObservableCollection<Famille> Families
        {
            get
            {
                return _Families;
            }
            set
            {
                if (_Families == value)
                {
                    return;
                }

                _Families = value;
                RaisePropertyChanged(() => Families);
            }
        }

        public ICommand LoadedCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        private void OnLoaded()
        {
            MyDBEntities db = DbManager.CreateDbManager();
            Families = new ObservableCollection<Famille>(db.Famille.OrderBy(f => f.Name).ToList());
        }

        private void OnSave(Famille famille)
        {
            MyDBEntities db = DbManager.CreateDbManager();

            if (db.Famille.Any(f => f.Name.ToLower().Equals(famille.Name.ToLower())))
            {
                Microsoft.Windows.Controls.MessageBox.Show("Une famille avec le même nom existe déjà!", "Stop", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Stop);
                return;
            }

            if (famille.Id == 0)
            {
                try
                {
                    db.Famille.AddObject(famille);
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

                    Famille est = db.Famille.FirstOrDefault(u => u.Id == famille.Id);
                    if (est != null)
                    {
                        est.Name = famille.Name;
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

            Families = new ObservableCollection<Famille>();
            OnLoaded();
        }

        private void OnDelete(Famille famille)
        {
            MessageBoxResult result = Microsoft.Windows.Controls.MessageBox.Show("Vous confirmer le Delete de cette famille?", "Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.No)
            {

                return;
            }

            //  DBEntities db = new DBEntities();

            if (famille.Id == 0)
            {
                Families.Remove(famille);
                return;
            }
            try
            {
                MyDBEntities db = DbManager.CreateDbManager();
                Famille est = db.Famille.FirstOrDefault(u => u.Id == famille.Id);
                db.Famille.DeleteObject(est);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Microsoft.Windows.Controls.MessageBox.Show(ex.Message, "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                return;
            }
            Microsoft.Windows.Controls.MessageBox.Show("Deleted", "Delete", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);

            Families = new ObservableCollection<Famille>();
            OnLoaded();
        }
    }
}
