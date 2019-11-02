using System;
using System.Collections.Generic;

namespace CarRenTal.Models
{
    public partial class LoaiXe
    {
        public LoaiXe()
        {
            HangXe = new HashSet<HangXe>();
        }

        public int Id { get; set; }
        public string TenLoai { get; set; }

        public virtual ICollection<HangXe> HangXe { get; set; }
    }
}
