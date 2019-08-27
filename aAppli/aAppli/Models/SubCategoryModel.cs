using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.ViewModel;
using System.Collections.ObjectModel;

namespace aAppli.Models
{
    public class SubCategoryModel : NotificationObject
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

        private ObservableCollection<Categorie> _Categries;
        public ObservableCollection<Categorie> Categories
        {
            get
            {
                return _Categries;
            }
            set
            {
                if (_Categries == value)
                {
                    return;
                }

                _Categries = value;
                RaisePropertyChanged(() => Categories);
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
                RaisePropertyChanged(() => SelectedCategory);
            }
        }

        public SubCategoryModel()
        {
            Categories = new ObservableCollection<Categorie>();
        }
    }
}
