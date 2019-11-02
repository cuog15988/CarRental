using System;
using System.Collections.Generic;

namespace CarRenTal.Models
{
    public partial class Cart
    {
        public int Id { get; set; }
        public int Ma { get; set; }
        public int Maxe { get; set; }
        public string Tenxe { get; set; }
        public int? Gia { get; set; }
        public int? Manguoidang { get; set; }

        public string DiaChi { get; set; }
        public string Images { get; set; }

        public virtual Users MaNavigation { get; set; }
        public virtual Xe MaxeNavigation { get; set; }
    }
}
