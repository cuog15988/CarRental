using System;
using System.Collections.Generic;

namespace CarRenTal.EF
{
    public partial class Huyen
    {
        public int Id { get; set; }
        public string TenHuyen { get; set; }
        public int? MaTinh { get; set; }

        public virtual Tinh MaTinhNavigation { get; set; }
    }
}
