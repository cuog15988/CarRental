﻿using System;
using System.Collections.Generic;

namespace CarRenTal.EF
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

        public virtual Users MaNguoiDangNavigation { get; set; }
        public virtual TenXe MaTenXeNavigation { get; set; }
        public virtual ICollection<DonHang> DonHang { get; set; }
    }
}
