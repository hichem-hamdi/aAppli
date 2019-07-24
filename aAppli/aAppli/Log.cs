using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace aAppli
{
    public static class Log
    {
        public static void WriteLog(string fileName, string data)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(fileName, true))
            {
                file.WriteLine(data);
            }
        }
    }
}
