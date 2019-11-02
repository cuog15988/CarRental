using System;
using System.Collections.Generic;

namespace CarRenTal.Models
{
    public partial class Tinh
    {
        public Tinh()
        {
            Huyen = new HashSet<Huyen>();
        }

        public int Ma { get; set; }
        public string TenTinh { get; set; }

        public virtual ICollection<Huyen> Huyen { get; set; }
    }
}
