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
    public class vehicleController : Controller
    {
        private readonly RentalCarContext _context;

        public vehicleController(RentalCarContext context)
        {
            _context = context;
        }


        public IActionResult onoffVehcle(int? id, int? khoa)
        {
            if (ModelState.IsValid)
            {
                var s = _context.Xe.Find(id);

                if (id == null)
                {
                    return NotFound();
                }
                if (khoa != null && khoa == 1)
                {
                    s.Status = null;
                    
                }
                
                else
                if(s.Status==null && khoa==0)
                {
                    s.Status = true;

                }
                if (s.Status == true)
                {
                    s.Status = false;
                   
                }
                else
                if(s.Status==false)
                {
                    s.Status = true;
                    
                }
                _context.Update(s);
                _context.SaveChanges();
                string referer1 = Request.Headers["Referer"].ToString();

                return Redirect(referer1);
            }

            return View(NotFound());
        }


        // GET: admin/vehicle
        public async Task<IActionResult> Index(int? showdo)
        {
            if (showdo==0 || showdo==null)
            {
                ViewData["Content"] = "Tất cả phương tiện";
                ViewData["Loai"] = 0;
                var rentalCarContext = _context.Xe.Include(x => x.MaHangXeNavigation).Include(x => x.MaHuyenNavigation).Include(x => x.MaNguoiDangNavigation).Include(x => x.MaTenXeNavigation);
                return View(await rentalCarContext.ToListAsync());
            }
            else if(showdo==1)
            {
                ViewData["Content"] = "Phương tiện chưa duyệt";
                ViewData["Loai"] = 1;
                var rentalCarContext = _context.Xe.Where(x=>x.Status==false).Include(x => x.MaHangXeNavigation).Include(x => x.MaHuyenNavigation).Include(x => x.MaNguoiDangNavigation).Include(x => x.MaTenXeNavigation);
                return View(await rentalCarContext.ToListAsync());
            }
            else if (showdo == 2)
            {
                ViewData["Content"] = "Phương tiện đã duyệt";
                ViewData["Loai"] = 2;
                var rentalCarContext = _context.Xe.Where(x => x.Status == true).Include(x => x.MaHangXeNavigation).Include(x => x.MaHuyenNavigation).Include(x => x.MaNguoiDangNavigation).Include(x => x.MaTenXeNavigation);
                return View(await rentalCarContext.ToListAsync());
            }
            else if (showdo == 3)
            {
                ViewData["Content"] = "Phương tiện bị khoá";
                ViewData["Loai"] = 3;
                var rentalCarContext = _context.Xe.Where(x => x.Status == null).Include(x => x.MaHangXeNavigation).Include(x => x.MaHuyenNavigation).Include(x => x.MaNguoiDangNavigation).Include(x => x.MaTenXeNavigation);
                return View(await rentalCarContext.ToListAsync());
            }
         
            return View();
        }

        // GET: admin/vehicle/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var xe = await _context.Xe
                .Include(x => x.MaHangXeNavigation)
                .Include(x => x.MaHuyenNavigation)
                .Include(x => x.MaNguoiDangNavigation)
                .Include(x => x.MaTenXeNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (xe == null)
            {
                return NotFound();
            }

            return View(xe);
        }

        // GET: admin/vehicle/Create
        public IActionResult Create()
        {
            ViewData["MaHangXe"] = new SelectList(_context.HangXe, "Id", "Id");
            ViewData["MaHuyen"] = new SelectList(_context.Huyen, "Id", "Id");
            ViewData["MaNguoiDang"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["MaTenXe"] = new SelectList(_context.TenXe, "Id", "Id");
            return View();
        }

        // POST: admin/vehicle/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NgayNhap,MaNguoiDang,Tinh,Huyen,TenLoai,TenNguoiDang,Doi,Tenxe,TenHang,Hinh,Mota,Gia,LoaiXe,Status,Moban,MaHuyen,MaHangXe,MaTenXe,Diachi")] Xe xe)
        {
            if (ModelState.IsValid)
            {
                _context.Add(xe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaHangXe"] = new SelectList(_context.HangXe, "Id", "Id", xe.MaHangXe);
            ViewData["MaHuyen"] = new SelectList(_context.Huyen, "Id", "Id", xe.MaHuyen);
            ViewData["MaNguoiDang"] = new SelectList(_context.Users, "Id", "Id", xe.MaNguoiDang);
            ViewData["MaTenXe"] = new SelectList(_context.TenXe, "Id", "Id", xe.MaTenXe);
            return View(xe);
        }

        // GET: admin/vehicle/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var xe = await _context.Xe.FindAsync(id);
            if (xe == null)
            {
                return NotFound();
            }
            ViewData["MaHangXe"] = new SelectList(_context.HangXe, "Id", "Id", xe.MaHangXe);
            ViewData["MaHuyen"] = new SelectList(_context.Huyen, "Id", "Id", xe.MaHuyen);
            ViewData["MaNguoiDang"] = new SelectList(_context.Users, "Id", "Id", xe.MaNguoiDang);
            ViewData["MaTenXe"] = new SelectList(_context.TenXe, "Id", "Id", xe.MaTenXe);
            return View(xe);
        }

        // POST: admin/vehicle/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NgayNhap,MaNguoiDang,Tinh,Huyen,TenLoai,TenNguoiDang,Doi,Tenxe,TenHang,Hinh,Mota,Gia,LoaiXe,Status,Moban,MaHuyen,MaHangXe,MaTenXe,Diachi")] Xe xe)
        {
            if (id != xe.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(xe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!XeExists(xe.Id))
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
            ViewData["MaHangXe"] = new SelectList(_context.HangXe, "Id", "Id", xe.MaHangXe);
            ViewData["MaHuyen"] = new SelectList(_context.Huyen, "Id", "Id", xe.MaHuyen);
            ViewData["MaNguoiDang"] = new SelectList(_context.Users, "Id", "Id", xe.MaNguoiDang);
            ViewData["MaTenXe"] = new SelectList(_context.TenXe, "Id", "Id", xe.MaTenXe);
            return View(xe);
        }

        // GET: admin/vehicle/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var xe = await _context.Xe
                .Include(x => x.MaHangXeNavigation)
                .Include(x => x.MaHuyenNavigation)
                .Include(x => x.MaNguoiDangNavigation)
                .Include(x => x.MaTenXeNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (xe == null)
            {
                return NotFound();
            }

            return View(xe);
        }

        // POST: admin/vehicle/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var xe = await _context.Xe.FindAsync(id);
            _context.Xe.Remove(xe);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool XeExists(int id)
        {
            return _context.Xe.Any(e => e.Id == id);
        }
    }
}
