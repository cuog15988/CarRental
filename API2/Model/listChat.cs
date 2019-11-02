using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API2.Model
{
    public class listChat
    {
        public static int? myid { set; get; }
        public static int? fromid { set; get; }
        public static string myName { set; get; }
        public static string fromName { set; get; }
        public static string fromImg { set; get; }
        public static DateTime? date { set; get; }
    }
}
