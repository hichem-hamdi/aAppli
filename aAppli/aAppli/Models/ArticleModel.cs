using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.ViewModel;
using Microsoft.Windows.Controls;
using System.Data.OleDb;
using System.IO;
using System.Collections.ObjectModel;

namespace aAppli.Models
{
    public class ArticleModel : NotificationObject
    {
        private Famille _SelectedFamily;
        public Famille SelectedFamily
        {
            get
            {
                return _SelectedFamily;
            }
            set
            {
                if (_SelectedFamily == value)
                {
                    return;
                }

                _SelectedFamily = value;
                FormatDesignation();
                RaisePropertyChanged(() => SelectedFamily);
            }
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

        private Categorie _SelectedCategory;
        public Categorie SelectedCategory
        {
            get
            {
                return _SelectedCategory;
            }
            set
            {
                if (_SelectedCategory == value)
                {
                    return;
                }

                _SelectedCategory = value;
                FormatDesignation();
                RaisePropertyChanged(() => SelectedCategory);
            }
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

        private SOUS_CATEGORIE _SelectedSubCategory;
        public SOUS_CATEGORIE SelectedSubCategory
        {
            get
            {
                return _SelectedSubCategory;
            }
            set
            {
                if (_SelectedSubCategory == value)
                {
                    return;
                }

                _SelectedSubCategory = value;
                FormatDesignation();
                RaisePropertyChanged(() => SelectedSubCategory);
            }
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

        private Brand _SelectedBrand;
        public Brand SelectedBrand
        {
            get
            {
                return _SelectedBrand;
            }
            set
            {
                if (_SelectedBrand == value)
                {
                    return;
                }

                _SelectedBrand = value;
                FormatDesignation();
                RaisePropertyChanged(() => SelectedBrand);
            }
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

        private Model _SelectedModel;
        public Model SelectedModel
        {
            get
            {
                return _SelectedModel;
            }
            set
            {
                if (_SelectedModel == value)
                {
                    return;
                }

                _SelectedModel = value;
                FormatDesignation();
                RaisePropertyChanged(() => SelectedModel);
            }
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

        private Size _SelectedSize;
        public Size SelectedSize
        {
            get
            {
                return _SelectedSize;
            }
            set
            {
                if (_SelectedSize == value)
                {
                    return;
                }

                _SelectedSize = value;
                FormatDesignation();
                RaisePropertyChanged(() => SelectedSize);
            }
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

        private Fournisseur _SelectedSupplier;
        public Fournisseur SelectedSupplier
        {
            get
            {
                return _SelectedSupplier;
            }
            set
            {
                if (_SelectedSupplier == value)
                {
                    return;
                }

                _SelectedSupplier = value;
                FormatDesignation();
                RaisePropertyChanged(() => SelectedSupplier);
            }
        }

        private ObservableCollection<Fournisseur> _Suppliers;
        public ObservableCollection<Fournisseur> Suppliers
        {
            get
            {
                return _Suppliers;
            }
            set
            {
                if (_Suppliers == value)
                {
                    return;
                }

                _Suppliers = value;
                RaisePropertyChanged(() => Suppliers);
            }
        }

        private DateTime? _PurchaseDate;
        public DateTime? PurchaseDate
        {
            get
            {
                return _PurchaseDate;
            }
            set
            {
                if (_PurchaseDate == value)
                {
                    return;
                }

                _PurchaseDate = value;
                FormatDesignation();
                RaisePropertyChanged(() => PurchaseDate);
            }
        }
        private string _Description;
        public string Description
        {
            get
            {
                return _Description;
            }
            set
            {
                if (_Description == value)
                {
                    return;
                }

                _Description = value;
                FormatDesignation();
                RaisePropertyChanged(() => Description);
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

                _Designation = value.ToUpperInvariant();
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

        private string _SNAVendre;
        public string SNAVendre
        {
            get
            {
                return _SNAVendre;
            }
            set
            {
                if (_SNAVendre == value)
                {
                    return;
                }

                _SNAVendre = value;

                RaisePropertyChanged(() => SNAVendre);
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


                if (SN.StartsWith(";"))
                {
                    string[] tab = SN.Split(';');
                    List<string> tabe2 = new List<string>(); ;
                    foreach (var item in tab)
                    {
                        if (item == string.Empty || tabe2.Contains(item))
                        {
                            continue;
                        }

                        tabe2.Add(item);
                        if (tab.Count(s => s == item) > 1)
                        {
                            MessageBox.Show("Le SN : " + item + " Exsite deja.", "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                            SN = SN.Remove(SN.IndexOf(item) - 1, item.Length + 1);
                        }
                    }
                }

                QT = SN.Count(c => c.Equals(';'));
                RaisePropertyChanged(() => SN);
            }
        }

        private int _QT;
        public int QT
        {
            get
            {
                return _QT;
            }
            set
            {
                if (_QT == value)
                {
                    return;
                }

                _QT = value;
                RaisePropertyChanged(() => QT);
            }
        }

        private int _QTAVendre;
        public int QTAVendre
        {
            get
            {
                return _QTAVendre;
            }
            set
            {
                if (_QTAVendre == value)
                {
                    return;
                }

                _QTAVendre = value;
                RaisePropertyChanged(() => QTAVendre);
            }
        }

        private bool _IsGenerique;
        public bool IsGenerique
        {
            get
            {
                return _IsGenerique;
            }

            set
            {
                if (_IsGenerique == value)
                {
                    return;
                }

                _IsGenerique = value;

                RaisePropertyChanged(() => IsGenerique);
            }
        }

        private int _PicesQuantity;
        public int PicesQuantity
        {
            get
            {
                return _PicesQuantity;
            }
            set
            {
                if (_PicesQuantity == value)
                {
                    return;
                }

                _PicesQuantity = value;
                FormatDesignation();
                RaisePropertyChanged(() => PicesQuantity);
            }
        }

        public long ParentId { get; set; }

        //public ArticleModel(Article art)
        //{
        //    //Id = art.Id;
        //    Designation = art.Designation;
        //    PrixAchat = art.PrixAchat;
        //    PrixVente = art.PrixVente;
        //    SN = art.SN;
        //    QT = art.QT;
        //}

        public ArticleModel()
        {
        }

        public bool IsLoadedFromDB { get; set; }

        public string DesignationText
        {
            get
            {
                var designationStringBuilder = new StringBuilder();

                if (SelectedSubCategory != null)
                {
                    designationStringBuilder.Append(SelectedSubCategory.Name);
                    designationStringBuilder.Append(" ");
                }

                if (SelectedBrand != null)
                {
                    designationStringBuilder.Append(SelectedBrand.Name);
                    designationStringBuilder.Append(" ");
                }

                if (SelectedModel != null)
                {
                    designationStringBuilder.Append(SelectedModel.Name);
                    designationStringBuilder.Append(" ");
                }

                if (SelectedSize != null)
                {
                    designationStringBuilder.Append(SelectedSize.Name);
                    designationStringBuilder.Append(" ");
                }

                designationStringBuilder.Append(Description);
                designationStringBuilder.Append(" ");

                if (SelectedSupplier != null)
                {
                    designationStringBuilder.Append(SelectedSupplier.Name);
                    designationStringBuilder.Append(" ");
                }

                if (PurchaseDate.HasValue)
                {
                    designationStringBuilder.Append("Le ");
                    designationStringBuilder.Append(PurchaseDate.Value.ToShortDateString());
                    designationStringBuilder.Append(" ");
                }
                designationStringBuilder.Append(PicesQuantity);
                designationStringBuilder.Append(" PCS");

                return designationStringBuilder.ToString().ToUpper();
            }
        }


        private void FormatDesignation()
        {
            if (IsLoadedFromDB)
                return;

            var designationStringBuilder = new StringBuilder();
            if (SelectedFamily != null)
            {
                designationStringBuilder.Append(SelectedFamily.Name);
                designationStringBuilder.Append(" ");
            }

            if (SelectedCategory != null)
            {
                designationStringBuilder.Append(SelectedCategory.Name);
                designationStringBuilder.Append(" ");
            }

            if (SelectedSubCategory != null)
            {
                designationStringBuilder.Append(SelectedSubCategory.Name);
                designationStringBuilder.Append(" ");
            }

            if (SelectedBrand != null)
            {
                designationStringBuilder.Append(SelectedBrand.Name);
                designationStringBuilder.Append(" ");
            }

            if (SelectedModel != null)
            {
                designationStringBuilder.Append(SelectedModel.Name);
                designationStringBuilder.Append(" ");
            }

            if (SelectedSize != null)
            {
                designationStringBuilder.Append(SelectedSize.Name);
                designationStringBuilder.Append(" ");
            }

            designationStringBuilder.Append(Description);
            designationStringBuilder.Append(" ");

            if (SelectedSupplier != null)
            {
                designationStringBuilder.Append(SelectedSupplier.Name);
                designationStringBuilder.Append(" ");
            }

            if (PurchaseDate.HasValue)
            {
                designationStringBuilder.Append("Le ");
                designationStringBuilder.Append(PurchaseDate.Value.ToShortDateString());
                designationStringBuilder.Append(" ");
            }
            designationStringBuilder.Append(PicesQuantity);
            designationStringBuilder.Append(" PCS");

            Designation = designationStringBuilder.ToString().ToUpper();
        }

    }
}