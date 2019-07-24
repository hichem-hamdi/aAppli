using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;

namespace aAppli.ViewModels
{
    public class ManageSubCategoriesViewModel : NotificationObject
    {
        public ManageSubCategoriesViewModel()
        {
            _SubCategories = new ObservableCollection<SOUS_CATEGORIE>();
            LoadedCommand = new DelegateCommand(OnLoaded);
            SaveCommand = new DelegateCommand<SOUS_CATEGORIE>(OnSave);
            DeleteCommand = new DelegateCommand<SOUS_CATEGORIE>(OnDelete);
        }

        private ObservableCollection<SOUS_CATEGORIE> _SubCategories;
        public ObservableCollection<SOUS_CATEGORIE> SubCategories
        {
            get
            {
                return _SubCategories;
            }
            set
            {
                if (_SubCategories == value)
                {
                    return;
                }

                _SubCategories = value;
                RaisePropertyChanged(() => SubCategories);
            }
        }

        public ICommand LoadedCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        private void OnLoaded()
        {
            MyDBEntities db = DbManager.CreateDbManager();
            SubCategories = new ObservableCollection<SOUS_CATEGORIE>(db.SOUS_CATEGORIE.OrderBy(f => f.Name).ToList());
        }

        private void OnSave(SOUS_CATEGORIE subCategory)
        {
            MyDBEntities db = DbManager.CreateDbManager();

            if (db.SOUS_CATEGORIE.Any(f => f.Name.ToLower().Equals(subCategory.Name.ToLower())))
            {
                Microsoft.Windows.Controls.MessageBox.Show("Une sous-catégorie avec le même nom existe déjà!", "Stop", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Stop);
                return;
            }

            if (subCategory.Id == 0)
            {
                try
                {
                    db.SOUS_CATEGORIE.AddObject(subCategory);
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

                    SOUS_CATEGORIE est = db.SOUS_CATEGORIE.FirstOrDefault(u => u.Id == subCategory.Id);
                    if (est != null)
                    {
                        est.Name = subCategory.Name;
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

            SubCategories = new ObservableCollection<SOUS_CATEGORIE>();
            OnLoaded();
        }

        private void OnDelete(SOUS_CATEGORIE subCategory)
        {
            MessageBoxResult result = Microsoft.Windows.Controls.MessageBox.Show("Vous confirmer le Delete de cette sous-catégorie?", "Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.No)
            {

                return;
            }

            //  DBEntities db = new DBEntities();

            if (subCategory.Id == 0)
            {
                SubCategories.Remove(subCategory);
                return;
            }
            try
            {
                MyDBEntities db = DbManager.CreateDbManager();
                SOUS_CATEGORIE est = db.SOUS_CATEGORIE.FirstOrDefault(u => u.Id == subCategory.Id);
                db.SOUS_CATEGORIE.DeleteObject(est);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Microsoft.Windows.Controls.MessageBox.Show(ex.Message, "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                return;
            }
            Microsoft.Windows.Controls.MessageBox.Show("Deleted", "Delete", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);

            SubCategories = new ObservableCollection<SOUS_CATEGORIE>();
            OnLoaded();
        }
    }
}
