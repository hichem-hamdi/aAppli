using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.ViewModel;
using System.Collections.ObjectModel;

namespace aAppli.Models
{
    public class CategoryModel : NotificationObject
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
                RaisePropertyChanged(() => SelectedFamily);
            }
        }

        public CategoryModel()
        {
            Families = new ObservableCollection<Famille>();
        }
    }
}
