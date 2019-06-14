using System;
using System.Collections.Generic;

namespace CarRenTal.Models
{
    public partial class DonHang
    {
        public int Id { get; set; }
        public int? MaXe { get; set; }
        public int? MaUs { get; set; }
        public int? MaLoaiThanhToan { get; set; }
        public DateTime? NgayLap { get; set; }
        public DateTime? TuNgay { get; set; }
        public DateTime? DenNgay { get; set; }
        public bool? TinhTrangThanhToan { get; set; }
        public bool? Status { get; set; }
        public int? Songay { get; set; }
        public int? TongTien { get; set; }
        public int? Giamgia { get; set; }
        public bool? Huy { get; set; }
        public bool? Xacnhan { get; set; }

        public virtual HinhThucThanhToan MaLoaiThanhToanNavigation { get; set; }
        public virtual Users MaUsNavigation { get; set; }
        public virtual Xe MaXeNavigation { get; set; }
    }
}
