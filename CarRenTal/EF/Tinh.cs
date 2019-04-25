﻿using System;
using System.Collections.Generic;

namespace CarRenTal.EF
{
    public partial class Tinh
    {
        public Tinh()
        {
            Huyen = new HashSet<Huyen>();
        }

        public string Ma { get; set; }
        public string TenTinh { get; set; }

        public virtual ICollection<Huyen> Huyen { get; set; }
    }
}