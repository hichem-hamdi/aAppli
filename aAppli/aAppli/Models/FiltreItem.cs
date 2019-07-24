using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using aAppli.Enums;

namespace aAppli.Models
{
    public class FiltreItem
    {
        public int Qt { get; set; }
        public string Designation { get; set; }
        public DateTime DateVente { get; set; }
        public decimal PrixAchat { get; set; }
        public decimal PrixVente { get; set; }
        public ItemFamille Famille { get; set; }
        public string SN { get; set; }
    }
}
