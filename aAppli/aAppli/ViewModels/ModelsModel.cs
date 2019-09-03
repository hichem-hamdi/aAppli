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
                RaisePropertyChanged(() => SelectedBrand);
            }
        }

        public ModelsModel()
        {
            Brands = new ObservableCollection<Brand>();
        }
    }
}
