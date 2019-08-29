using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.ViewModel;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows;
using Microsoft.Practices.Prism.Commands;

namespace aAppli.ViewModels
{
    public class ManageModelsViewModel : NotificationObject
    {
        public ManageModelsViewModel()
        {
            _Models = new ObservableCollection<Model>();
            LoadedCommand = new DelegateCommand(OnLoaded);
            SaveCommand = new DelegateCommand<Model>(OnSave);
            DeleteCommand = new DelegateCommand<Model>(OnDelete);
        }

        private ObservableCollection<Model> _Models;
        public ObservableCollection<Model> Models
        {
            get
            {
                return _Models;
            }
            set
            {
                if (_Models == value)
                {
                    return;
                }

                _Models = value;
                RaisePropertyChanged(() => Models);
            }
        }

        public ICommand LoadedCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        private void OnLoaded()
        {
            MyDBEntities db = DbManager.CreateDbManager();
            Models = new ObservableCollection<Model>(db.Model.OrderBy(f => f.Name).ToList());
        }

        private void OnSave(Model model)
        {
            MyDBEntities db = DbManager.CreateDbManager();

            if (db.Brand.Any(f => f.Name.ToLower().Equals(model.Name.ToLower())))
            {
                Microsoft.Windows.Controls.MessageBox.Show("Un model avec le même nom existe déjà!", "Stop", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Stop);
                return;
            }

            if (model.Id == 0)
            {
                try
                {
                    db.Model.AddObject(model);
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

                    Model est = db.Model.FirstOrDefault(u => u.Id == model.Id);
                    if (est != null)
                    {
                        est.Name = model.Name;
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

            Models = new ObservableCollection<Model>();
            OnLoaded();
        }

        private void OnDelete(Model model)
        {
            MessageBoxResult result = Microsoft.Windows.Controls.MessageBox.Show("Vous confirmer le Delete de ce Model?", "Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.No)
            {

                return;
            }

            //  DBEntities db = new DBEntities();

            if (model.Id == 0)
            {
                Models.Remove(model);
                return;
            }
            try
            {
                MyDBEntities db = DbManager.CreateDbManager();
                Model est = db.Model.FirstOrDefault(u => u.Id == model.Id);
                db.Model.DeleteObject(est);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Microsoft.Windows.Controls.MessageBox.Show(ex.Message, "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                return;
            }
            Microsoft.Windows.Controls.MessageBox.Show("Deleted", "Delete", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);

            Models = new ObservableCollection<Model>();
            OnLoaded();
        }
    }
}
