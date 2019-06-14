using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarRenTal.Areas.seller.Models
{
    public class UserDao
    {

        [Required(ErrorMessage = "Mời nhập user name")]
        public string UserName { set; get; }

        [Required(ErrorMessage = "Mời nhập password")]
        public string Password { set; get; }

        public bool RememberMe { set; get; }
        public static int UserId { get; set; }
        public static String name { get; set; }
        public static int GroupId { get; set; }



    }
}
