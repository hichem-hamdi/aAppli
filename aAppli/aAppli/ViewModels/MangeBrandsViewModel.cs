using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;

namespace aAppli.ViewModels
{
    public class ManageBrandsViewModel : NotificationObject
    {
        public ManageBrandsViewModel()
        {
            _Brands = new ObservableCollection<Brand>();
            LoadedCommand = new DelegateCommand(OnLoaded);
            SaveCommand = new DelegateCommand<Brand>(OnSave);
            DeleteCommand = new DelegateCommand<Brand>(OnDelete);
        }

        private ObservableCollection<Brand> _Brands;
        public ObservableCollection<Brand> Brands
        {
            get
            {
                return _Brands;
            }
            set
            {
                if (_Brands == value)
                {
                    return;
                }

                _Brands = value;
                RaisePropertyChanged(() => Brands);
            }
        }

        public ICommand LoadedCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        private void OnLoaded()
        {
            MyDBEntities db = DbManager.CreateDbManager();
            Brands = new ObservableCollection<Brand>(db.Brand.OrderBy(f => f.Name).ToList());
        }

        private void OnSave(Brand brand)
        {
            MyDBEntities db = DbManager.CreateDbManager();

            if (db.Brand.Any(f => f.Name.ToLower().Equals(brand.Name.ToLower())))
            {
                Microsoft.Windows.Controls.MessageBox.Show("Une marque avec le même nom existe déjà!", "Stop", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Stop);
                return;
            }

            if (brand.Id == 0)
            {
                try
                {
                    db.Brand.AddObject(brand);
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

                    Brand est = db.Brand.FirstOrDefault(u => u.Id == brand.Id);
                    if (est != null)
                    {
                        est.Name = brand.Name;
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

            Brands = new ObservableCollection<Brand>();
            OnLoaded();
        }

        private void OnDelete(Brand brand)
        {
            MessageBoxResult result = Microsoft.Windows.Controls.MessageBox.Show("Vous confirmer le Delete de cette marque?", "Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.No)
            {

                return;
            }

            //  DBEntities db = new DBEntities();

            if (brand.Id == 0)
            {
                Brands.Remove(brand);
                return;
            }
            try
            {
                MyDBEntities db = DbManager.CreateDbManager();
                Brand est = db.Brand.FirstOrDefault(u => u.Id == brand.Id);
                db.Brand.DeleteObject(est);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Microsoft.Windows.Controls.MessageBox.Show(ex.Message, "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                return;
            }
            Microsoft.Windows.Controls.MessageBox.Show("Deleted", "Delete", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);

            Brands = new ObservableCollection<Brand>();
            OnLoaded();
        }
    }
}
