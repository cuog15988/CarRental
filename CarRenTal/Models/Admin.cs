using System;
using System.Collections.Generic;

namespace CarRenTal.Models
{
    public partial class Admin
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public string HoTen { get; set; }
        public DateTime? NgaySinh { get; set; }
        public DateTime? NgayNhap { get; set; }
        public string DiaChi { get; set; }
        public bool? Status { get; set; }
        public int? GroudId { get; set; }
    }
}
