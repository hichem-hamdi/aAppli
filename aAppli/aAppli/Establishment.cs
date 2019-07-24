using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace aAppli
{
    public partial class Establishment
    {
        private Visibility _ActionVisiblity;
        public Visibility ActionVisiblity
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Name))
                    return Visibility.Hidden;

                MyDBEntities db = DbManager.CreateDbManager();
                if (db.Article.Any(a => a.EstablishmentId == Id) || Name.Equals("Nouvelit", StringComparison.InvariantCultureIgnoreCase))
                    return Visibility.Hidden;
                else
                    return Visibility.Visible;

            }
        }
    }
}
