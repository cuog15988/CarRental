using System;
using System.Collections.Generic;

namespace CarRenTal.Models
{
    public partial class Images
    {
        public int Id { get; set; }
        public int? XeId { get; set; }
        public string Src { get; set; }

        public virtual Xe Xe { get; set; }
    }
}
