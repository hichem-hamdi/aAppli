using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;
using aAppli.Models;

namespace aAppli.ViewModels
{
    public class ManageSubCategoriesViewModel : NotificationObject
    {
        public ManageSubCategoriesViewModel()
        {
            _SubCategories = new ObservableCollection<SubCategoryModel>();
            LoadedCommand = new DelegateCommand(OnLoaded);
            SaveCommand = new DelegateCommand<SubCategoryModel>(OnSave);
            DeleteCommand = new DelegateCommand<SubCategoryModel>(OnDelete);
        }

        private ObservableCollection<SubCategoryModel> _SubCategories;
        public ObservableCollection<SubCategoryModel> SubCategories
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
            SubCategories = new ObservableCollection<SubCategoryModel>(db.SOUS_CATEGORIE.OrderBy(f => f.Name).ToList().Select(s =>
                new SubCategoryModel
                {
                    Id = s.Id,
                    Name = s.Name,
                    SelectedCategory = db.Categorie.FirstOrDefault(c => c.Id == s.CategoryId),
                    Categories = new ObservableCollection<Categorie>(db.Categorie.OrderBy(c => c.Name).ToList())
                }));
        }

        private void OnSave(SubCategoryModel subCategory)
        {
            subCategory.Name = subCategory.Name.ToUpperInvariant();
            MyDBEntities db = DbManager.CreateDbManager();

            if (subCategory.Id == 0 && db.SOUS_CATEGORIE.Any(f => f.Name.ToLower().Equals(subCategory.Name.ToLower())))
            {
                Microsoft.Windows.Controls.MessageBox.Show("Une sous-catégorie avec le même nom existe déjà!", "Stop", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Stop);
                return;
            }

            if (subCategory.Id == 0)
            {
                try
                {
                    db.SOUS_CATEGORIE.AddObject(new SOUS_CATEGORIE { Name = subCategory.Name, CategoryId = subCategory.SelectedCategory.Id });
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
                        est.CategoryId = subCategory.SelectedCategory.Id;
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

            SubCategories = new ObservableCollection<SubCategoryModel>();
            OnLoaded();
        }

        private void OnDelete(SubCategoryModel subCategory)
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

            SubCategories = new ObservableCollection<SubCategoryModel>();
            OnLoaded();
        }
    }
}
