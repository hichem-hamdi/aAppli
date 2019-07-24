using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;

namespace aAppli.ViewModels
{
    public class ManageSizesViewModel : NotificationObject
    {
        public ManageSizesViewModel()
        {
            _Sizes = new ObservableCollection<Size>();
            LoadedCommand = new DelegateCommand(OnLoaded);
            SaveCommand = new DelegateCommand<Size>(OnSave);
            DeleteCommand = new DelegateCommand<Size>(OnDelete);
        }

        private ObservableCollection<Size> _Sizes;
        public ObservableCollection<Size> Sizes
        {
            get
            {
                return _Sizes;
            }
            set
            {
                if (_Sizes == value)
                {
                    return;
                }

                _Sizes = value;
                RaisePropertyChanged(() => Sizes);
            }
        }

        public ICommand LoadedCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        private void OnLoaded()
        {
            MyDBEntities db = DbManager.CreateDbManager();
            Sizes = new ObservableCollection<Size>(db.Size.OrderBy(f => f.Name).ToList());
        }

        private void OnSave(Size size)
        {
            MyDBEntities db = DbManager.CreateDbManager();

            if (db.Brand.Any(f => f.Name.ToLower().Equals(size.Name.ToLower())))
            {
                Microsoft.Windows.Controls.MessageBox.Show("Une taille avec le même nom existe déjà!", "Stop", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Stop);
                return;
            }

            if (size.Id == 0)
            {
                try
                {
                    db.Size.AddObject(size);
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

                    Size est = db.Size.FirstOrDefault(u => u.Id == size.Id);
                    if (est != null)
                    {
                        est.Name = size.Name;
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

            Sizes = new ObservableCollection<Size>();
            OnLoaded();
        }

        private void OnDelete(Size size)
        {
            MessageBoxResult result = Microsoft.Windows.Controls.MessageBox.Show("Vous confirmer le Delete de cette taille?", "Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.No)
            {

                return;
            }

            //  DBEntities db = new DBEntities();

            if (size.Id == 0)
            {
                Sizes.Remove(size);
                return;
            }
            try
            {
                MyDBEntities db = DbManager.CreateDbManager();
                Size est = db.Size.FirstOrDefault(u => u.Id == size.Id);
                db.Size.DeleteObject(est);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Microsoft.Windows.Controls.MessageBox.Show(ex.Message, "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                return;
            }
            Microsoft.Windows.Controls.MessageBox.Show("Deleted", "Delete", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);

            Sizes = new ObservableCollection<Size>();
            OnLoaded();
        }
    }
}
