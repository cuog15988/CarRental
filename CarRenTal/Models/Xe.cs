using System;
using System.Collections.Generic;

namespace CarRenTal.Models
{
    public partial class Xe
    {
        public Xe()
        {
            DonHang = new HashSet<DonHang>();
        }

        public int Id { get; set; }
        public int? MaTenXe { get; set; }
        public DateTime? NgayNhap { get; set; }
        public int? MaNguoiDang { get; set; }
        public string Tinh { get; set; }
        public string Huyen { get; set; }
        public string TenLoai { get; set; }
        public string TenNguoiDang { get; set; }
        public string Doi { get; set; }
        public string Tenxe { get; set; }
        public string TenHang { get; set; }
        public string Hinh { get; set; }
        public string Mota { get; set; }
        public int? Gia { get; set; }

        public virtual Users MaNguoiDangNavigation { get; set; }
        public virtual TenXe MaTenXeNavigation { get; set; }
        public virtual ICollection<DonHang> DonHang { get; set; }
    }
}
