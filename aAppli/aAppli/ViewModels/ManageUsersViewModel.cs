// -----------------------------------------------------------------------
// <copyright file="ManageUsersViewModel.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace aAppli.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.Practices.Prism.ViewModel;
    using aAppli.Models;
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using Microsoft.Practices.Prism.Commands;
    using System.Data.OleDb;
    using System.IO;
    using System.Windows;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ManageUsersViewModel : NotificationObject
    {

        public ManageUsersViewModel()
        {
            _Users = new ObservableCollection<UserModel>();
            LoadedCommand = new DelegateCommand(OnLoaded);
            SaveCommand = new DelegateCommand<UserModel>(OnSave);
            DeleteCommand = new DelegateCommand<UserModel>(OnDelete);
            MyDBEntities db = DbManager.CreateDbManager();
            Establishments = new ObservableCollection<Establishment> { new Establishment { Id = 0, Name = "-- Select --" } };
            foreach (var item in db.Establishment)
            {

                Establishments.Add(item);
            }
        }

        #region Commands

        public ICommand LoadedCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        #endregion

        #region Properties

        private ObservableCollection<UserModel> _Users;
        public ObservableCollection<UserModel> Users
        {
            get
            {
                return _Users;
            }
            set
            {
                if (_Users == value)
                {
                    return;
                }

                _Users = value;
                RaisePropertyChanged(() => Users);
            }
        }

        #endregion

        #region Private Methods

        private void OnLoaded()
        {
            MyDBEntities db = DbManager.CreateDbManager();
            foreach (var item in db.Users)
            {
                UserModel usr = new UserModel
                {
                    Id = item.Id,
                    Login = item.Login,
                    Password = item.Pass,
                    Stock = item.Stock.GetValueOrDefault(),
                    Vente = item.Vente.GetValueOrDefault(),
                    Vente_S = item.Vente_S.GetValueOrDefault(),
                    HV = item.HistoriqueVente.GetValueOrDefault(),
                    AVoir = item.AVoir.GetValueOrDefault(),
                    Inventaire = item.Inventaire.GetValueOrDefault(),
                    VC = item.VenteCredit.GetValueOrDefault(),
                    VC_S = item.VenteCredit_S.GetValueOrDefault(),
                    GerrerAcces = item.GererAcces.GetValueOrDefault(),
                    ViderBase = item.ViderBase.GetValueOrDefault(),
                    EditInsertion = item.EditInsertion.GetValueOrDefault(),
                    EstablishmentId = item.Establishment != null ? item.Establishment.Id : 0
                };
                Users.Add(usr);
            }
        }

        public static ObservableCollection<Establishment> Establishments;

        private void OnSave(UserModel user)
        {
            if (user.EstablishmentId == 0)
            {
                Microsoft.Windows.Controls.MessageBox.Show("Vous devez sélectionner une société.", "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                return;
            }

            MyDBEntities db = DbManager.CreateDbManager();

            if (user.Id == 0)
            {
                try
                {
                    User usr = new User
                    {
                        Login = user.Login,
                        Pass = user.Password,
                        Stock = user.Stock,
                        Vente = user.Vente,
                        Vente_S = user.Vente_S,
                        HistoriqueVente = user.HV,
                        AVoir = user.AVoir,
                        Inventaire = user.Inventaire,
                        VenteCredit = user.VC,
                        VenteCredit_S = user.VC_S,
                        GererAcces = user.GerrerAcces,
                        ViderBase = user.ViderBase,
                        EstablishmentId = user.EstablishmentId,
                        EditInsertion = user.EditInsertion
                    };
                    db.Users.AddObject(usr);
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

                    User usr = db.Users.FirstOrDefault(u => u.Id == user.Id);
                    if (usr != null)
                    {
                        usr.Pass = user.Password;
                        usr.Login = user.Login;
                        usr.Stock = user.Stock;
                        usr.Vente = user.Vente;
                        usr.Vente_S = user.Vente_S;
                        usr.HistoriqueVente = user.HV;
                        usr.AVoir = user.AVoir;
                        usr.Inventaire = user.Inventaire;
                        usr.VenteCredit = user.VC;
                        usr.VenteCredit_S = user.VC_S;
                        usr.GererAcces = user.GerrerAcces;
                        usr.ViderBase = user.ViderBase;
                        usr.EditInsertion = user.EditInsertion;
                        usr.EstablishmentId = user.EstablishmentId;
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

            Users = new ObservableCollection<UserModel>();
            OnLoaded();
        }

        private void OnDelete(UserModel user)
        {


            MessageBoxResult result = Microsoft.Windows.Controls.MessageBox.Show("Vous confirmer le Delete de cet utilisateur?", "Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.No)
            {

                return;
            }

            //  DBEntities db = new DBEntities();

            if (user.Id == 0)
            {
                Users.Remove(user);
                return;
            }
            try
            {
                MyDBEntities db = DbManager.CreateDbManager();
                User usr = db.Users.FirstOrDefault(u => u.Id == user.Id);
                db.Users.DeleteObject(usr);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Microsoft.Windows.Controls.MessageBox.Show(ex.Message, "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                return;
            }
            Microsoft.Windows.Controls.MessageBox.Show("Deleted", "Delete", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);

            Users = new ObservableCollection<UserModel>();
            OnLoaded();
        }
        #endregion
    }
}
