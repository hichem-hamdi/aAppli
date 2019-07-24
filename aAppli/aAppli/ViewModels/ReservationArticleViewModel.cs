using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.ViewModel;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using System.Collections.ObjectModel;

namespace aAppli.ViewModels
{
    public class ReservationArticleViewModel : NotificationObject
    {
        public ReservationArticleViewModel()
        {
            EnterCommandSearch = new DelegateCommand(OnEnterCommandSearch);
        }

        private string _Search;
        public string Search
        {
            get
            {
                return _Search;
            }
            set
            {
                if (_Search == value)
                {
                    return;
                }

                _Search = value;
                RaisePropertyChanged(() => Search);
            }
        }

        private ObservableCollection<Article> _Articles;
        public ObservableCollection<Article> Articles
        {
            get
            {
                return _Articles;
            }

            set
            {
                if (_Articles == value)
                {
                    return;
                }

                _Articles = value;
                RaisePropertyChanged(() => Articles);
            }
        }

        public ICommand EnterCommandSearch { get; private set; }

        private void OnEnterCommandSearch()
        {
            MyDBEntities db = DbManager.CreateDbManager();
            var articles = db.Article.Where(a => a.QT > 0).ToList();
            Articles = new ObservableCollection<Article>(articles.Where(article => article.EstablishmentId != cUser.EstablishmentId && (article.Designation != null && article.Designation.Contains(Search.ToUpper()) || (article.SN.Split(';').Contains(Search)))).ToList());
        }
    }
}
