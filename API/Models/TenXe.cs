using System;
using System.Collections.Generic;

namespace CarRenTal.Models
{
    public partial class TenXe
    {
        public TenXe()
        {
            Xe = new HashSet<Xe>();
        }

        public int Id { get; set; }
        public string TenXe1 { get; set; }
        public int? Doi { get; set; }
        public int? MaHangXe { get; set; }

        public virtual HangXe MaHangXeNavigation { get; set; }
        public virtual ICollection<Xe> Xe { get; set; }
    }
}
