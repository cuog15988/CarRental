using System;
using System.Collections.Generic;

namespace CarRenTal.Models
{
    public partial class HomThuLienHe
    {
        public int Id { get; set; }
        public DateTime? NgayGui { get; set; }
        public int? NguoiGui { get; set; }
        public string TieuDe { get; set; }
        public string NoiDung { get; set; }
    }
}
