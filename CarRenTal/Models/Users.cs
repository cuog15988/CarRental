using System;
using System.Collections.Generic;

namespace CarRenTal.Models
{
    public partial class Users
    {
        public Users()
        {
            Cart = new HashSet<Cart>();
            ChiTietThanhToan = new HashSet<ChiTietThanhToan>();
            DonHang = new HashSet<DonHang>();
            HopThu = new HashSet<HopThu>();
            Xe = new HashSet<Xe>();
        }

        public int Id { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public string HoTen { get; set; }
        public DateTime? NgaySinh { get; set; }
        public DateTime? NgayNhap { get; set; }
        public string DiaChi { get; set; }
        public bool? Gioitinh { get; set; }
        public bool? Status { get; set; }
        public int? GroudId { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool? Xacthuc { get; set; }

        public virtual ICollection<Cart> Cart { get; set; }
        public virtual ICollection<ChiTietThanhToan> ChiTietThanhToan { get; set; }
        public virtual ICollection<DonHang> DonHang { get; set; }
        public virtual ICollection<HopThu> HopThu { get; set; }
        public virtual ICollection<Xe> Xe { get; set; }
    }
}
