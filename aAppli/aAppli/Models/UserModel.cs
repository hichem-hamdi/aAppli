// -----------------------------------------------------------------------
// <copyright file="UserModel.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace aAppli.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.Practices.Prism.ViewModel;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class UserModel:NotificationObject
    {
        private long _Id;
        public long Id
        {
            get
            {
                return _Id;
            }
            set
            {
                if (_Id == value)
                {
                    return;
                }

                _Id = value;
                RaisePropertyChanged(() => Id);
            }
        }

        private string _Login;
        public string Login
        {
            get
            {
                return _Login;
            }
            set
            {
                if (_Login == value)
                {
                    return;
                }

                _Login = value;
                RaisePropertyChanged(() => Login);
            }
        }

        private string _Password;
        public string Password
        {
            get
            {
                return _Password;
            }
            set
            {
                if (_Password == value)
                {
                    return;
                }

                _Password = value;
                RaisePropertyChanged(() => Password);
            }
        }

        private bool _IsAdmin;
        public bool IsAdmin
        {
            get
            {
                return _IsAdmin;
            }
            set
            {
                if (_IsAdmin == value)
                {
                    return;
                }

                _IsAdmin = value;
                RaisePropertyChanged(() => IsAdmin);
            }
        }

        private bool _IsCashier;
        public bool IsCashier
        {
            get
            {
                return _IsCashier;
            }
            set
            {
                if (_IsCashier == value)
                {
                    return;
                }

                _IsCashier = value;
                RaisePropertyChanged(() => IsCashier);
            }
        }

        private bool _IsGestionnaire;
        public bool IsGestionnaire
        {
            get
            {
                return _IsGestionnaire;
            }
            set
            {
                if (_IsGestionnaire == value)
                {
                    return;
                }

                _IsGestionnaire = value;
                RaisePropertyChanged(() => IsGestionnaire);
            }
        }

        private bool _IsGestionnaireStock;
        public bool IsGestionnaireStock
        {
            get
            {
                return _IsGestionnaireStock;
            }
            set
            {
                if (_IsGestionnaireStock == value)
                {
                    return;
                }

                _IsGestionnaireStock = value;
                RaisePropertyChanged(() => IsGestionnaireStock);
            }
        }

        private bool _Stock;
        public bool Stock
        {
            get
            {
                return _Stock;
            }
            set
            {
                if (_Stock == value)
                {
                    return;
                }

                _Stock = value;
                RaisePropertyChanged(() => Stock);
            }
        }

        private bool _Vente;
        public bool Vente
        {
            get
            {
                return _Vente;
            }
            set
            {
                if (_Vente == value)
                {
                    return;
                }

                _Vente = value;
                RaisePropertyChanged(() => Vente);
            }
        }

        private bool _Vente_S;
        public bool Vente_S
        {
            get
            {
                return _Vente_S;
            }
            set
            {
                if (_Vente_S == value)
                {
                    return;
                }

                _Vente_S = value;
                RaisePropertyChanged(() => Vente_S);
            }
        }

        private bool _HV;
        public bool HV
        {
            get
            {
                return _HV;
            }
            set
            {
                if (_HV == value)
                {
                    return;
                }

                _HV = value;
                RaisePropertyChanged(() => HV);
            }
        }

        private bool _AVoir;
        public bool AVoir
        {
            get
            {
                return _AVoir;
            }
            set
            {
                if (_AVoir == value)
                {
                    return;
                }

                _AVoir = value;
                RaisePropertyChanged(() => AVoir);
            }
        }

        private bool _Inventaire;
        public bool Inventaire
        {
            get
            {
                return _Inventaire;
            }
            set
            {
                if (_Inventaire == value)
                {
                    return;
                }

                _Inventaire = value;
                RaisePropertyChanged(() => Inventaire);
            }
        }

        private bool _VC;
        public bool VC
        {
            get
            {
                return _VC;
            }
            set
            {
                if (_VC == value)
                {
                    return;
                }

                _VC = value;
                RaisePropertyChanged(() =>VC);
            }
        }

        private bool _VC_S;
        public bool VC_S
        {
            get
            {
                return _VC_S;
            }
            set
            {
                if (_VC_S == value)
                {
                    return;
                }

                _VC_S = value;
                RaisePropertyChanged(() => VC_S);
            }
        }

        private bool _GerrerAcces;
        public bool GerrerAcces
        {
            get
            {
                return _GerrerAcces;
            }
            set
            {
                if (_GerrerAcces == value)
                {
                    return;
                }

                _GerrerAcces = value;
                RaisePropertyChanged(() => GerrerAcces);
            }
        }

        private bool _ViderBase;
        public bool ViderBase
        {
            get
            {
                return _ViderBase;
            }
            set
            {
                if (_ViderBase == value)
                {
                    return;
                }

                _ViderBase = value;
                RaisePropertyChanged(() => ViderBase);
            }
        }

        private long _EstablishmentId;
        public long EstablishmentId
        {
            get
            {
                return _EstablishmentId;
            }
            set
            {
                if (_EstablishmentId == value)
                {
                    return;
                }

                _EstablishmentId = value;
                RaisePropertyChanged(() => EstablishmentId);
            }
        }
    }
}
