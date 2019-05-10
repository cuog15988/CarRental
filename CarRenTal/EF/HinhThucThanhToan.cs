using System;
using System.Collections.Generic;

namespace CarRenTal.EF
{
    public partial class HinhThucThanhToan
    {
        public HinhThucThanhToan()
        {
            DonHang = new HashSet<DonHang>();
        }

        public int Id { get; set; }
        public string TenLoai { get; set; }

        public virtual ICollection<DonHang> DonHang { get; set; }
    }
}
