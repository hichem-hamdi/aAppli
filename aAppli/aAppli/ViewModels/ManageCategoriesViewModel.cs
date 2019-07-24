using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;

namespace aAppli.ViewModels
{
    public class ManageCategoriesViewModel : NotificationObject
    {
        public ManageCategoriesViewModel()
        {
            _Categories = new ObservableCollection<Categorie>();
            LoadedCommand = new DelegateCommand(OnLoaded);
            SaveCommand = new DelegateCommand<Categorie>(OnSave);
            DeleteCommand = new DelegateCommand<Categorie>(OnDelete);
        }

        private ObservableCollection<Categorie> _Categories;
        public ObservableCollection<Categorie> Categories
        {
            get
            {
                return _Categories;
            }
            set
            {
                if (_Categories == value)
                {
                    return;
                }

                _Categories = value;
                RaisePropertyChanged(() => Categories);
            }
        }

        public ICommand LoadedCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        private void OnLoaded()
        {
            MyDBEntities db = DbManager.CreateDbManager();
            Categories = new ObservableCollection<Categorie>(db.Categorie.OrderBy(f => f.Name).ToList());
        }

        private void OnSave(Categorie category)
        {
            MyDBEntities db = DbManager.CreateDbManager();

            if (db.Categorie.Any(f => f.Name.ToLower().Equals(category.Name.ToLower())))
            {
                Microsoft.Windows.Controls.MessageBox.Show("Une catégorie avec le même nom existe déjà!", "Stop", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Stop);
                return;
            }

            if (category.Id == 0)
            {
                try
                {
                    db.Categorie.AddObject(category);
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

                    Categorie est = db.Categorie.FirstOrDefault(u => u.Id == category.Id);
                    if (est != null)
                    {
                        est.Name = category.Name;
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

            Categories = new ObservableCollection<Categorie>();
            OnLoaded();
        }

        private void OnDelete(Categorie category)
        {
            MessageBoxResult result = Microsoft.Windows.Controls.MessageBox.Show("Vous confirmer le Delete de cette catégorie?", "Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.No)
            {

                return;
            }

            //  DBEntities db = new DBEntities();

            if (category.Id == 0)
            {
                Categories.Remove(category);
                return;
            }
            try
            {
                MyDBEntities db = DbManager.CreateDbManager();
                Categorie est = db.Categorie.FirstOrDefault(u => u.Id == category.Id);
                db.Categorie.DeleteObject(est);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Microsoft.Windows.Controls.MessageBox.Show(ex.Message, "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                return;
            }
            Microsoft.Windows.Controls.MessageBox.Show("Deleted", "Delete", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);

            Categories = new ObservableCollection<Categorie>();
            OnLoaded();
        }
    }
}
