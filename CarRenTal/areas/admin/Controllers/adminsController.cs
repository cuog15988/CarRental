using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CarRenTal.Models;
using CarRenTal.DAO.Common;

namespace CarRenTal.Areas.admin.Controllers
{
    [Area("admin")]
    public class adminsController : Controller
    {
        private readonly RentalCarContext _context;

        public adminsController(RentalCarContext context)
        {
            _context = context;
        }

        public IActionResult khoataikhoan(int? id)
        {
            if(id== null)
            {
                return NotFound();
            }

            if(ModelState.IsValid)
            {
                var admin = _context.Admin.Find(id);
                if (admin.Status == true )
                {
                    admin.Status = false;
                }
                else
                {
                    admin.Status = true;
                }

                _context.Update(admin);
                _context.SaveChanges();
                string referer1 = Request.Headers["Referer"].ToString();

                return Redirect(referer1);

            }

            return View(NotFound());
        }
        // GET: admin/admins
        public async Task<IActionResult> Index(int? showdo)
        {
            if (showdo == 0 || showdo == null)
            {
                ViewData["Content"] = "Tất cả tài khoản";
                ViewData["Loai"] = 0;
                return View(await _context.Admin.ToListAsync());
            }
            else
            if(showdo==1)
            {
                ViewData["Content"] = "Tài khoản đang bị khoá";
                ViewData["Loai"] = 1;
                return View(await _context.Admin.Where(x=>x.Status==false).ToListAsync());
            }

            else
            if (showdo == 2)
            {
                ViewData["Content"] = "Tài khoản đang hoạt động";
                ViewData["Loai"] = 1;
                return View(await _context.Admin.Where(x => x.Status == true).ToListAsync());
            }
            return View(await _context.Admin.ToListAsync());
        }

        // GET: admin/admins/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var admin = await _context.Admin
                .FirstOrDefaultAsync(m => m.Id == id);
            if (admin == null)
            {
                return NotFound();
            }

            return View(admin);
        }

        // GET: admin/admins/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: admin/admins/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserName,PassWord,HoTen,NgaySinh,NgayNhap,DiaChi,Status,GroudId")] Admin admin)
        {
            if (ModelState.IsValid)
            {
                Admin ad = new Admin();
                ad = admin;
                ad.NgayNhap = DateTime.Now;
                ad.PassWord = Encryptor.MD5Hash(admin.PassWord);
                ad.Status = true;
                ad.GroudId = 2;
                if(_context.Admin.Where(x=>x.UserName==ad.UserName).SingleOrDefault() !=default)
                {
                    ViewData["TrungUser"] = "UserName đã tồn tại";
                    return View();
                }
                _context.Add(ad);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(admin);
        }

        // GET: admin/admins/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var admin = await _context.Admin.FindAsync(id);
            if (admin == null)
            {
                return NotFound();
            }
            return View(admin);
        }

        // POST: admin/admins/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserName,PassWord,HoTen,NgaySinh,NgayNhap,DiaChi,Status,GroudId")] Admin admin)
        {
            if (id != admin.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(admin);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdminExists(admin.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(admin);
        }

        // GET: admin/admins/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var admin = await _context.Admin
                .FirstOrDefaultAsync(m => m.Id == id);
            if (admin == null)
            {
                return NotFound();
            }

            return View(admin);
        }

        // POST: admin/admins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var admin = await _context.Admin.FindAsync(id);
            _context.Admin.Remove(admin);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdminExists(int id)
        {
            return _context.Admin.Any(e => e.Id == id);
        }
    }
}
