using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.ViewModel;
using System.Collections.ObjectModel;

namespace aAppli.ViewModels
{
    public class ModelsModel : NotificationObject
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


        private string _Name;

        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                if (_Name == value)
                {
                    return;
                }

                _Name = value;
                RaisePropertyChanged(() => Name);
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
                RaisePropertyChanged(() => SelectedSubCategory);
            }
        }

        public ModelsModel()
        {
            SubCategories = new ObservableCollection<SOUS_CATEGORIE>();
        }
    }
}
