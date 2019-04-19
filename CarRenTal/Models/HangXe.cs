using System;
using System.Collections.Generic;

namespace CarRenTal.Models
{
    public partial class HangXe
    {
        public HangXe()
        {
            TenXe = new HashSet<TenXe>();
        }

        public int Id { get; set; }
        public string TenHang { get; set; }
        public int? MaLoaiXe { get; set; }

        public virtual LoaiXe MaLoaiXeNavigation { get; set; }
        public virtual ICollection<TenXe> TenXe { get; set; }
    }
}
