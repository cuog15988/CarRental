using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRenTal.Areas.seller.Models;
using CarRenTal.DAO.Common;
using CarRenTal.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarRenTal.Areas.seller.Controllers
{
    [Area("seller")]
    public class SellerLoginController : Controller
    {
        private readonly RentalCarContext _context;

        public SellerLoginController(RentalCarContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }


        public int LoginDAO(string userName, string passWord)
        {
            var result = _context.Users.SingleOrDefault(x => x.UserName == userName);
            if (result == null)
            {
                return 0;
            }
            else
            {
                if (result.Status == false)
                {
                    return -1;
                }
                else
                if(result.Xacthuc==false)
                {
                    return -3;
                }
                else
                {
                    if (result.PassWord == passWord)
                        return 1;
                    else
                        return -2;
                }
            }
        }
        public IActionResult Login(UserDao model, int? type)
        {
            if (ModelState.IsValid)
            {
                var result = LoginDAO(model.UserName, Encryptor.MD5Hash(model.Password));
                if (result == 1)
                {

                    var user = _context.Users.SingleOrDefault(x => x.UserName == model.UserName);
                   
                    UserDao.UserId = user.Id;
                    UserDao.name = user.UserName;
                    
                        return RedirectToAction("Index", "product");

                }
                else if (result == 0)
                {
                    ModelState.AddModelError("", "Tài khoản không tồn tại.");
                }
                else if (result == -1)
                {
                    ModelState.AddModelError("", "Tài khoản đang bị khoá.");
                }
                else if (result == -2)
                {
                    ModelState.AddModelError("", "Mật khẩu không đúng.");
                }
                else if (result == -3)
                {
                    ModelState.AddModelError("", "Tài khoản của bạn không có quyền đăng nhập.");
                }
               
                else
                {
                    ModelState.AddModelError("", "đăng nhập không đúng.");
                }
            }
            return View("Index");
        }
        public ActionResult Logout()
        {
            UserDao.name = null;
            UserDao.UserId = 0;
            string referer1 = Request.Headers["Referer"].ToString();
            return Redirect(referer1);
        }
    }
}