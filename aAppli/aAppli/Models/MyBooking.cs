using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace aAppli.Models
{
    public class MyBooking
    {
        public long Id { get; set; }
        public string Designation { get; set; }
        public string SN { get; set; }
        public int QT { get; set; }
        public string Magasin { get; set; }
        public DateTime RequestDate { get; set; }
    }
}
