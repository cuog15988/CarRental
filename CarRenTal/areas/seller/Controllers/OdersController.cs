using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CarRenTal.Models;
using CarRenTal.Areas.seller.Models;

namespace CarRenTal.Areas.seller.Controllers
{
    [Area("seller")]
    public class OdersController : BaseSellerController
    {
        private readonly RentalCarContext _context;

        public OdersController(RentalCarContext context)
        {
            _context = context;
        }

        // GET: seller/Oders
        public async Task<IActionResult> Index(int? showdo)
        {
            if(showdo==null)
            {
                ViewData["content"] = "Tất cả yêu cầu ";
                return View(await _context.DonHang.Include(d => d.MaLoaiThanhToanNavigation).Include(d => d.MaUsNavigation).Include(d => d.MaXeNavigation)
                                .Where(x => x.MaXeNavigation.MaNguoiDang == UserDao.UserId).ToListAsync());
            }
            else
                if(showdo==1)
            {
                ViewData["content"] = "yêu cầu bị huỷ";
                return View(await _context.DonHang.Include(d => d.MaLoaiThanhToanNavigation).Include(d => d.MaUsNavigation).Include(d => d.MaXeNavigation)
                                    .Where(x => x.MaXeNavigation.MaNguoiDang == UserDao.UserId && x.Huy==true).ToListAsync());
            }
            else
                if (showdo == 2)
            {
                ViewData["content"] = "yêu cầu chưa xấc nhận";
                return View(await _context.DonHang.Include(d => d.MaLoaiThanhToanNavigation).Include(d => d.MaUsNavigation).Include(d => d.MaXeNavigation)
                                    .Where(x => x.MaXeNavigation.MaNguoiDang == UserDao.UserId && x.Huy == false && x.TuNgay>DateTime.Now && x.Status==false).ToListAsync());
            }
            else
                if (showdo == 3)
            {
                ViewData["content"] = "yêu cầu hết hạn";
                return View(await _context.DonHang.Include(d => d.MaLoaiThanhToanNavigation).Include(d => d.MaUsNavigation).Include(d => d.MaXeNavigation)
                                    .Where(x => x.MaXeNavigation.MaNguoiDang == UserDao.UserId && x.Huy == false && x.Status==false && x.TuNgay <= DateTime.Now).ToListAsync());
            }
            if (showdo == 4)
            {
                ViewData["content"] = "yêu cầu đã hoàn thành";
                return View(await _context.DonHang.Include(d => d.MaLoaiThanhToanNavigation).Include(d => d.MaUsNavigation).Include(d => d.MaXeNavigation)
                                    .Where(x => x.MaXeNavigation.MaNguoiDang == UserDao.UserId && x.Huy == false && x.Status == true && x.DenNgay < DateTime.Now).ToListAsync());
            }

            return View(await _context.DonHang.Include(d => d.MaLoaiThanhToanNavigation).Include(d => d.MaUsNavigation).Include(d => d.MaXeNavigation)
                                    .Where(x => x.MaXeNavigation.MaNguoiDang == UserDao.UserId).ToListAsync());

        }

       


        // GET: seller/Oders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var f = _context.DonHang.Where(x => x.MaXeNavigation.MaNguoiDang == UserDao.UserId).ToList();
            if (f.Count <1)
            {
                return NotFound();
            }


            var donHang = await _context.DonHang
                .Include(d => d.MaLoaiThanhToanNavigation)
                .Include(d => d.MaUsNavigation)
                .Include(d => d.MaXeNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (donHang == null)
            {
                return NotFound();
            }

            return View(donHang);
        }

        public IActionResult getStatus(int? id)
        {
            if(id== null)
            {
                return NotFound();
            }

            if(ModelState.IsValid)
            {
                var s = _context.DonHang.Find(id);
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
                return Redirect("/seller/oders/Details?id=" + id);
            }
            return View();
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donHang = await _context.DonHang.FindAsync(id);
            if (donHang == null)
            {
                return NotFound();
            }
            ViewData["MaLoaiThanhToan"] = new SelectList(_context.HinhThucThanhToan, "Id", "Id", donHang.MaLoaiThanhToan);
            ViewData["MaUs"] = new SelectList(_context.Users, "Id", "Id", donHang.MaUs);
            ViewData["MaXe"] = new SelectList(_context.Xe, "Id", "Id", donHang.MaXe);
            return View(donHang);
        }

        // POST: seller/Oders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MaXe,MaUs,MaLoaiThanhToan,NgayLap,TuNgay,DenNgay,TinhTrangThanhToan,Status,Songay,TongTien,Giamgia")] DonHang donHang)
        {
            if (id != donHang.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(donHang);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DonHangExists(donHang.Id))
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
            ViewData["MaLoaiThanhToan"] = new SelectList(_context.HinhThucThanhToan, "Id", "Id", donHang.MaLoaiThanhToan);
            ViewData["MaUs"] = new SelectList(_context.Users, "Id", "Id", donHang.MaUs);
            ViewData["MaXe"] = new SelectList(_context.Xe, "Id", "Id", donHang.MaXe);
            return View(donHang);
        }

        // GET: seller/Oders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donHang = await _context.DonHang
                .Include(d => d.MaLoaiThanhToanNavigation)
                .Include(d => d.MaUsNavigation)
                .Include(d => d.MaXeNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (donHang == null)
            {
                return NotFound();
            }

            return View(donHang);
        }

        // POST: seller/Oders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var donHang = await _context.DonHang.FindAsync(id);
            _context.DonHang.Remove(donHang);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DonHangExists(int id)
        {
            return _context.DonHang.Any(e => e.Id == id);
        }
    }
}
