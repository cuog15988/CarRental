using System;
using System.Collections.Generic;

namespace CarRenTal.Models
{
    public partial class ChiTietThanhToan
    {
        public int MaPt { get; set; }
        public int MaUs { get; set; }
        public DateTime? NgayNhap { get; set; }
        public string Code { get; set; }

        public virtual Users MaPtNavigation { get; set; }
        public virtual PhuongThucThanhToan MaUsNavigation { get; set; }
    }
}
