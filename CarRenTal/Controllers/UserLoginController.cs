using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CarRenTal.Models;

namespace CarRenTal.Controllers
{
    public class UserLoginController : Controller
    {
        private readonly RentalCarContext _context;

        public UserLoginController(RentalCarContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
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
        public async Task<IActionResult> Create([Bind("Id,UserName,PassWord,HoTen,NgaySinh,NgayNhap,DiaChi,Gioitinh,Status,GroudId,Email")] Users users)
        {
            if (ModelState.IsValid)
            {
                _context.Add(users);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(users);
        }
    }
}