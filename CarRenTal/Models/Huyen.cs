using System;
using System.Collections.Generic;

namespace CarRenTal.Models
{
    public partial class Huyen
    {
        public Huyen()
        {
            Xe = new HashSet<Xe>();
        }

        public int Id { get; set; }
        public string TenHuyen { get; set; }
        public int? MaTinh { get; set; }

        public virtual Tinh MaTinhNavigation { get; set; }
        public virtual ICollection<Xe> Xe { get; set; }
    }
}
