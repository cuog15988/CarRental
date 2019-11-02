using System;
using System.Collections.Generic;

namespace CarRenTal.Models {
    public partial class HomThuLienHe
    {
        public int Id { get; set; }
        public DateTime? NgayGui { get; set; }
        public string NguoiGui { get; set; }
        public string NoiDung { get; set; }
        public string Email { get; set; }
    }
}
