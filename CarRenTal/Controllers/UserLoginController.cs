using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CarRenTal.Models;
using Newtonsoft.Json;
using CarRenTal.DAO;

using CarRenTal.DAO.Common;

namespace CarRenTal.Controllers
{
    public class UserLoginController : Controller
    {
        private readonly RentalCarContext _context;

        public UserLoginController(RentalCarContext context)
        {
            _context = context;
        }
        public IActionResult Index(int? type)
        {
            if (type == null)
            {
                Link._link = Request.Headers["Referer"].ToString();
            }
            else
            {
                Link._link = "~/Home";
            }
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
                {
                    if (result.PassWord == passWord)
                        return 1;
                    else
                        return -2;
                }
            }
        }

        //trang hỏi đăng nhập
        
        public IActionResult questionLogin()
        {
            return View();
        }

        //login
        public IActionResult Login(UserLoginModel model, int? type )
        {
            if (ModelState.IsValid)
            {
                var result = LoginDAO(model.UserName, Encryptor.MD5Hash(model.Password));
                if (result == 1)
                {
                   
                        var user = _context.Users.SingleOrDefault(x => x.UserName == model.UserName);
                        var userSession = new UserLogin();
                        userSession.UserName = user.UserName;
                        userSession.UserID = user.Id;
                        CommonConstants.UserName = user.UserName;
                        CommonConstants.UserID = user.Id;
                    if (type == null)
                    {
                        return Redirect(Link._link);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                   
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

        //logout
        public ActionResult Logout()
        {
            CommonConstants.UserName = null;
            CommonConstants.UserID = 0;
            string referer1 = Request.Headers["Referer"].ToString();
            return Redirect(referer1);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserName,PassWord,HoTen,NgaySinh,NgayNhap,DiaChi,Email")] Users users)
        {
            if (ModelState.IsValid)
            {
                users.PassWord = Encryptor.MD5Hash(users.PassWord);
                users.NgayNhap = DateTime.Now;
                _context.Add(users);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(users);
        }
    }
}