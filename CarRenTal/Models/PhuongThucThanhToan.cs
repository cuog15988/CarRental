using System;
using System.Collections.Generic;

namespace CarRenTal.Models
{
    public partial class PhuongThucThanhToan
    {
        public PhuongThucThanhToan()
        {
            ChiTietThanhToan = new HashSet<ChiTietThanhToan>();
        }

        public int Id { get; set; }
        public string TenPt { get; set; }
        public string GhiChu { get; set; }
        public string Code { get; set; }

        public virtual ICollection<ChiTietThanhToan> ChiTietThanhToan { get; set; }
    }
}
