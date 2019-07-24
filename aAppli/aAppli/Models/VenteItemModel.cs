using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.ViewModel;

namespace aAppli.Models
{
   public class VenteItemModel:NotificationObject
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

        private long  _ArticleId;
        public long ArticleId
        {
            get
            {
                return _ArticleId;
            }
            set
            {
                if (_ArticleId == value)
                {
                    return;
                }

                _ArticleId = value;
                RaisePropertyChanged(() => ArticleId);
            }
        }

        private string _Designation;
        public string Designation
        {
            get
            {
                return _Designation;
            }
            set
            {
                if (_Designation == value)
                {
                    return;
                }

                _Designation = value;
                RaisePropertyChanged(() => Designation);
            }
        }

        private decimal _PrixAchat;
        public decimal PrixAchat
        {
            get
            {
                return _PrixAchat;
            }
            set
            {
                if (_PrixAchat == value)
                {
                    return;
                }

                _PrixAchat = value;
                RaisePropertyChanged(() => PrixAchat);
            }
        }


        private decimal _PrixVente;
        public decimal PrixVente
        {
            get
            {
                return _PrixVente;
            }
            set
            {
                if (_PrixVente == value)
                {
                    return;
                }

                _PrixVente = value;
                RaisePropertyChanged(() => PrixVente);
            }
        }

        private string _SN;
        public string SN
        {
            get
            {
                return _SN;
            }
            set
            {
                if (_SN == value)
                {
                    return;
                }

                _SN = value;
                RaisePropertyChanged(() => SN);
            }
        }


        private DateTime _DateDeVente;
        public DateTime DateDeVente
        {
            get
            {
                return _DateDeVente;
            }
            set
            {
                if (_DateDeVente == value)
                {
                    return;
                }

                _DateDeVente = value;
                RaisePropertyChanged(() => DateDeVente);
            }
        }


        private int _Quantite;
        public int Quantite
        {
            get
            {
                return _Quantite;
            }
            set
            {
                if (_Quantite == value)
                {
                    return;
                }

                _Quantite = value;
                RaisePropertyChanged(() => Quantite);
            }
        }
        public VenteItemModel()
        {

        }
        //public VenteItemModel(Vente vente)
        //{
        //    Id = vente.Id;
        //    ArticleId = vente.ArticleId;
        //    Designation = vente.Designation;
        //    PrixAchat = vente.PrixAchat;
        //    PrixVente = vente.PrixVente;
        //    SN = vente.SN;
        //    DateDeVente = vente.DateDeVente;
        //}
    }


   public class CreditItemModel : NotificationObject
   {
        private string _NomClient;
        public string NomClient
        {
            get
            {
                return _NomClient;
            }
            set
            {
                if (_NomClient == value)
                {
                    return;
                }

                _NomClient = value;
                RaisePropertyChanged(() => NomClient);
            }
        }

        private string _Coordonnees;
        public string Coordonnees
        {
            get
            {
                return _Coordonnees;
            }
            set
            {
                if (_Coordonnees == value)
                {
                    return;
                }

                _Coordonnees = value;
                RaisePropertyChanged(() => Coordonnees);
            }
        }

        private string _ModePaiement;
        public string ModePaiement
        {
            get
            {
                return _ModePaiement;
            }
            set
            {
                if (_ModePaiement == value)
                {
                    return;
                }

                _ModePaiement = value;
                RaisePropertyChanged(() => ModePaiement);
            }
        }

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

       private long _ArticleId;
       public long ArticleId
       {
           get
           {
               return _ArticleId;
           }
           set
           {
               if (_ArticleId == value)
               {
                   return;
               }

               _ArticleId = value;
               RaisePropertyChanged(() => ArticleId);
           }
       }

       private string _Designation;
       public string Designation
       {
           get
           {
               return _Designation;
           }
           set
           {
               if (_Designation == value)
               {
                   return;
               }

               _Designation = value;
               RaisePropertyChanged(() => Designation);
           }
       }

       private decimal _PrixAchat;
       public decimal PrixAchat
       {
           get
           {
               return _PrixAchat;
           }
           set
           {
               if (_PrixAchat == value)
               {
                   return;
               }

               _PrixAchat = value;
               RaisePropertyChanged(() => PrixAchat);
           }
       }


       private decimal _PrixVente;
       public decimal PrixVente
       {
           get
           {
               return _PrixVente;
           }
           set
           {
               if (_PrixVente == value)
               {
                   return;
               }

               _PrixVente = value;
               RaisePropertyChanged(() => PrixVente);
           }
       }

       private string _SN;
       public string SN
       {
           get
           {
               return _SN;
           }
           set
           {
               if (_SN == value)
               {
                   return;
               }

               _SN = value;
               RaisePropertyChanged(() => SN);
           }
       }


       private DateTime _DateDeVente;
       public DateTime DateDeVente
       {
           get
           {
               return _DateDeVente;
           }
           set
           {
               if (_DateDeVente == value)
               {
                   return;
               }

               _DateDeVente = value;
               RaisePropertyChanged(() => DateDeVente);
           }
       }


       private int _Quantite;
       public int Quantite
       {
           get
           {
               return _Quantite;
           }
           set
           {
               if (_Quantite == value)
               {
                   return;
               }

               _Quantite = value;
               RaisePropertyChanged(() => Quantite);
           }
       }

       public long           ParentId { get; set; }

       public CreditItemModel()
       {

       }
       //public VenteItemModel(Vente vente)
       //{
       //    Id = vente.Id;
       //    ArticleId = vente.ArticleId;
       //    Designation = vente.Designation;
       //    PrixAchat = vente.PrixAchat;
       //    PrixVente = vente.PrixVente;
       //    SN = vente.SN;
       //    DateDeVente = vente.DateDeVente;
       //}
   }
}
