using System.Configuration;
using System.IO;
using System.Windows.Forms;

namespace aAppli
{
    public static class DbManager
    {
        public static MyDBEntities CreateDbManager()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["MyDBEntities"].ConnectionString;
            string s = string.Format(connectionString, Path.GetDirectoryName(Application.ExecutablePath));
            return new MyDBEntities(s);
        }
    }
}
