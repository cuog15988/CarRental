using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRenTal.Models
{
    public class ChatList
    {
        public int Id { get; set; }
        public int? MyUser { get; set; }
        public int? FromUser { get; set; }
        public DateTime? Date { get; set; }
        public int? MsFrom { get; set; }
        public string LastMs { get; set; }
        public string Name { get; set; }
        public bool? Status { get; set; }
    }
}
