using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CarRenTal.Models;

namespace CarRenTal.Areas.admin.Controllers
{
    [Area("admin")]
    public class UsersController : Controller
    {
        private readonly RentalCarContext _context;

        public UsersController(RentalCarContext context)
        {
            _context = context;
        }

        // GET: admin/Users
        public async Task<IActionResult> Index(int? showdo)
        {
            if(showdo==null || showdo==0)
            {
                ViewData["Content"] = "Tất cả tài khoản";
                ViewData["Loai"] = 0;
                return View(await _context.Users.ToListAsync());

            }
            else
                if(showdo==1)
            {
                ViewData["Content"] = "Danh sách tài khoản bị khoá";
                ViewData["Loai"] = 1;

                return View(await _context.Users.Where(x=>x.Status==false).ToListAsync());

            }
            else
                if(showdo==2)
            {
                ViewData["Loai"] = 2;

                ViewData["Content"] = "Danh sách tài khoản bán hàng";
                return View(await _context.Users.Where(x=>x.Xacthuc==true).ToListAsync());

            }
            return View(await _context.Users.ToListAsync());

        }

        public IActionResult khoataikhoan(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            try
            {
                if (ModelState.IsValid)
                {
                    var s = _context.Users.Find(id);
                    if (s.Status == true)
                    {
                        s.Status = false;
                    }
                    else
                    {
                        s.Status = true;
                    }
                    _context.Update(s);
                    _context.SaveChanges();
                    return Redirect("/admin/Users/Details/" + id);
                }

            }
            catch (Exception ex)
            {

            }
            return View();
        }


        // GET: admin/Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (users == null)
            {
                return NotFound();
            }

            return View(users);
        }

        // GET: admin/Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: admin/Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserName,PassWord,HoTen,NgaySinh,NgayNhap,DiaChi,Gioitinh,Status,GroudId,Email,Phone,Xacthuc")] Users users)
        {
            if (ModelState.IsValid)
            {
                _context.Add(users);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(users);
        }

        // GET: admin/Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await _context.Users.FindAsync(id);
            if (users == null)
            {
                return NotFound();
            }
            return View(users);
        }

        // POST: admin/Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,Users users)
        {
            ViewData["status"] = null;

            if (id != users.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
                try
                {
                    var s = _context.Users.Find(id);
                    s.UserName = users.UserName;
                    s.NgaySinh = users.NgaySinh;
                    s.Phone = users.Phone;
                    s.DiaChi = users.DiaChi;
                    s.Email = users.Email;
                    s.HoTen = users.HoTen;
                    _context.Update(s);
                    await _context.SaveChangesAsync();
                    ViewData["status"] = "Đã chỉnh sửa thành công user " + s.UserName;
                    return RedirectToAction(nameof(Index));

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsersExists(users.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            return View(users);
        }

        // GET: admin/Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (users == null)
            {
                return NotFound();
            }

            return View(users);
        }

        // POST: admin/Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var users = await _context.Users.FindAsync(id);
            _context.Users.Remove(users);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsersExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
