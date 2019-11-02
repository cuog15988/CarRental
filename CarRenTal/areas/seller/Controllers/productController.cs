using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CarRenTal.Models;
using CarRenTal.Areas.seller.Models;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;


namespace CarRenTal.Areas.seller.Controllers
{
    [Area("seller")]
    public class productController : BaseSellerController
    {
        private readonly RentalCarContext _context;
        private readonly IHostingEnvironment _env;

        public productController(RentalCarContext context, IHostingEnvironment env)
        {
            _context = context;
            _env = env;
        }





        // GET: seller/product
        public async Task<IActionResult> Index()
        {

            var rentalCarContext = _context.Xe.Include(x => x.MaNguoiDangNavigation)
                .Include(x => x.MaHangXeNavigation)
                .Include(x=>x.MaHuyenNavigation.MaTinhNavigation)
                .Include(x=>x.MaHangXeNavigation.MaLoaiXeNavigation).Where(x=>x.MaNguoiDang==UserDao.UserId).OrderByDescending(x=>x.NgayNhap);
            return View(await rentalCarContext.ToListAsync());
        }

        // GET: seller/product/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var xe = await _context.Xe
                .Include(x => x.MaNguoiDangNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
             if (xe == null)
          {
                return NotFound();
            }

            var s = _context.DonHang.Where(x => x.MaXe == id).ToList();
            var sa = _context.DonHang.Where(x => x.MaXe == id && x.Status == true).ToList();
            ViewData["Luotthue"] = s.Count();
            ViewData["Thuethanhcong"] = sa.Count();
            ViewData["getDonhang"] = _context.DonHang.Where(x => x.MaXe == id).Include(x=>x.MaUsNavigation).ToList();
            double dem = 0;
            foreach (var item in s)
            {
                dem += (Convert.ToDateTime(item.DenNgay) - Convert.ToDateTime(item.TuNgay)).TotalDays;
            }
            ViewData["Dem"] = dem;

            return View(xe);
        }

        // GET: seller/product/Create
        public IActionResult Create()
        {
            ViewData["MaNguoiDang"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["MaTenXe"] = new SelectList(_context.TenXe, "Id", "TenXe");
            ViewData["MaHangXe"] = new SelectList(_context.HangXe, "Id", "TenHang");
            ViewData["MaTinh"] = _context.Tinh.ToList();
            ViewData["Loaixe"] = _context.LoaiXe.ToList();

            return View();
        }

        // POST: seller/product/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Xe xe, [FromForm(Name = "file")] IFormFile file)
        {
            //mã người đăng = UserID
            //ngày đăng = datetime
            //tên loaị = mahang + tên loại
            //tên người đưang = userid+tên người đăng
            //status =false
            //mở bán = true
            //tên hãng= mahang+tên hãng
            //loại xe = tên loại
            //đời xe, năm sản xuất

            Xe sa = new Xe();
            if (ModelState.IsValid)
            {
                if (sa == null)
                {
                    return Content("not found");
                }
                else
                {
                    int a = Convert.ToInt32(xe.MaHangXe);
                    sa = xe;
                    sa.NgayNhap = DateTime.Now;
                    sa.MaNguoiDang = Models.UserDao.UserId;
                    var s = _context.HangXe.Find(xe.MaHangXe);
                    sa.TenNguoiDang = _context.Users.Find(UserDao.UserId).HoTen;
                    sa.TenLoai = _context.HangXe.Include(x => x.MaLoaiXeNavigation).FirstOrDefault(x => x.Id == xe.MaHangXe).MaLoaiXeNavigation.TenLoai;

                    sa.TenHang = s.TenHang;

                    sa.Huyen = _context.Huyen.Find(xe.MaHuyen).TenHuyen;
                    var t = _context.Huyen.Include(x => x.MaTinhNavigation).FirstOrDefault(x => x.Id == xe.MaHuyen);
                    sa.Tinh = t.MaTinhNavigation.TenTinh;

                    sa.LoaiXe = s.MaLoaiXeNavigation.TenLoai;
                    sa.LoaiXe = sa.TenLoai;
                    sa.Status = false;
                    sa.Moban = true;
                    if (file != null)
                    {
                        string path_Root = _env.WebRootPath;

                        string path_to_Images = path_Root + "\\Images\\" + file.FileName;

                        //</ get Path >
                        if (System.IO.File.Exists(path_to_Images))
                        {
                            for (int i = 1; i <= 5; i++)
                            {
                                path_to_Images = path_Root + "\\Images\\x" + i + "\\" + file.FileName;
                                sa.Hinh = "\\Images\\" + file.FileName;
                                if (!System.IO.File.Exists(path_to_Images))
                                {
                                    break;
                                }

                                if (i == 5)
                                {
                                    return Content("something is wrong!!");
                                }
                            }
                        }
                        else
                        {
                            //< Copy File to Target >
                            sa.Hinh = "\\Images\\" + file.FileName;
                        }


                        using (var stream = new FileStream(path_to_Images, FileMode.Create))

                        {

                            await file.CopyToAsync(stream);

                        }

                        //</ Copy File to Target >


                    }


                    _context.Add(sa);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            ViewData["MaNguoiDang"] = new SelectList(_context.Users, "Id", "Id", xe.MaNguoiDang);
            ViewData["MaTenXe"] = new SelectList(_context.TenXe, "Id", "Id", xe.MaHangXe);
            return View(xe);
        }
        public JsonResult getHuyenbyID(int id)
        {
            var citylist = new SelectList(_context.Huyen.Where(c => c.MaTinh == id), "Id", "TenHuyen");
            return Json(citylist);

        }
        [HttpGet]
        // GET: seller/product/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var xe =  await _context.Xe.FindAsync(id);
                
            if (xe == null)
            {
                return NotFound();
            }
            var s = _context.Xe.Include(x => x.MaHuyenNavigation)
                .Include(x => x.MaHuyenNavigation.MaTinhNavigation)
                .Include(x => x.MaHangXeNavigation.MaLoaiXeNavigation)
                .Include(x => x.MaHangXeNavigation)
                .SingleOrDefault(x => x.Id == id);

            ViewData["xo"] = s;

            ViewData["MaLoaiXe"] = new SelectList(_context.LoaiXe, "Id", "TenLoai");
            ViewBag.Tinh = _context.Tinh.ToList();
            ViewBag.Loaixe = _context.LoaiXe.ToList();

            ViewData["MaNguoiDang"] = new SelectList(_context.Users, "Id", "Id", xe.MaNguoiDang);
            ViewData["MaTenXe"] = new SelectList(_context.TenXe, "Id", "Id", xe.MaHangXe);
            return View(xe);
        }

        // POST: seller/product/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Xe xe, [FromForm(Name = "file")] IFormFile file)
        {

            if (id != xe.Id)
            {
                return NotFound();
            }
            
            if (ModelState.IsValid)
            {
                try
                {
                    var sa = _context.Xe.Find(id);
                    if (xe.MaHuyen != sa.MaHuyen && xe.MaHuyen != null)
                    {
                        sa.MaHuyen = xe.MaHuyen;
                        sa.Huyen = _context.Huyen.Find(xe.MaHuyen).TenHuyen;
                        var t = _context.Huyen.Include(x => x.MaTinhNavigation).FirstOrDefault(x => x.Id == xe.MaHuyen);
                        sa.Tinh = t.MaTinhNavigation.TenTinh;
                        sa.Diachi = xe.Diachi;

                    }


                    if (xe.MaHangXe != null && xe.MaHangXe !=sa.MaHangXe)
                    {
                        sa.MaHangXe = xe.MaHangXe;
                        sa.TenLoai = _context.HangXe.Include(x => x.MaLoaiXeNavigation).FirstOrDefault(x => x.Id == xe.MaHangXe).MaLoaiXeNavigation.TenLoai;
                        sa.Tenxe = xe.Tenxe;
                        sa.LoaiXe= _context.HangXe.Include(x => x.MaLoaiXeNavigation).FirstOrDefault(x => x.Id == xe.MaHangXe).MaLoaiXeNavigation.TenLoai;
                    }
                    var filename = file;
                    if (file !=null)
                    {
                        string path_Root = _env.WebRootPath;

                        string path_to_Images = path_Root + "\\Images\\" + file.FileName;

                        //</ get Path >
                        if (System.IO.File.Exists(path_to_Images))
                        {
                            for (int i = 1; i <= 5; i++)
                            {
                                path_to_Images = path_Root + "\\Images\\x" + i + "\\" + file.FileName;
                                sa.Hinh = "\\Images\\" + file.FileName;
                                if (!System.IO.File.Exists(path_to_Images))
                                {
                                    break;
                                }

                                if(i==5)
                                {
                                    return Content("something is wrong!!");
                                }
                            }
                        }
                        else
                        {
                            //< Copy File to Target >
                            sa.Hinh = "\\Images\\" + file.FileName;
                        }


                        using (var stream = new FileStream(path_to_Images, FileMode.Create))

                        {

                            await file.CopyToAsync(stream);

                        }

                        //</ Copy File to Target >


                    }
                    sa.Mota = xe.Mota;
                    sa.Gia = xe.Gia;
                    _context.Update(sa);

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
                return Redirect(Request.Headers["Referer"].ToString());
            }
            ViewData["MaNguoiDang"] = new SelectList(_context.Users, "Id", "Id", xe.MaNguoiDang);
            ViewData["MaTenXe"] = new SelectList(_context.TenXe, "Id", "Id", xe.MaHangXeNavigation);
            return Redirect(Request.Headers["Referer"].ToString());
        }


        public IActionResult changeOnOff(int? id)
        {
            if(id==null)
            {
                return NotFound();
            }
            try
            {
                if(ModelState.IsValid)
                {
                    var s = _context.Xe.Find(id);
                    if(s.Moban == true)
                    {
                        s.Moban = false;
                    }
                    else
                    {
                        s.Moban = true;
                    }
                    _context.Update(s);

                     _context.SaveChanges();
                }
            }
            catch
            {

            }
            return Redirect(Request.Headers["Referer"].ToString());
        }
        public IActionResult Cancel()
        {
            return Redirect(Request.Headers["Referer"].ToString());
        }
        // GET: seller/product/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var xe = await _context.Xe
                .Include(x => x.MaNguoiDangNavigation)
                .Include(x => x.MaHuyenNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (xe == null)
            {
                return NotFound();
            }

            return View(xe);
        }

        // POST: seller/product/Delete/5
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

        public async Task<IActionResult> ChuaDuyet()
        {

            var rentalCarContext = _context.Xe.Include(x => x.MaNguoiDangNavigation)
                .Include(x => x.MaHangXeNavigation)
                .Include(x => x.MaHuyenNavigation.MaTinhNavigation)
                .Include(x => x.MaHangXeNavigation.MaLoaiXeNavigation).Where(x => x.MaNguoiDang == UserDao.UserId && x.Status==false).OrderByDescending(x => x.NgayNhap);
            return View(await rentalCarContext.ToListAsync());
        }

        public async Task<IActionResult> ChuaKichHoat()
        {

            var rentalCarContext = _context.Xe.Include(x => x.MaNguoiDangNavigation)
                .Include(x => x.MaHangXeNavigation)
                .Include(x => x.MaHuyenNavigation.MaTinhNavigation)
                .Include(x => x.MaHangXeNavigation.MaLoaiXeNavigation).Where(x => x.MaNguoiDang == UserDao.UserId && x.Status == true && x.Moban==false).OrderByDescending(x => x.NgayNhap);
            return View(await rentalCarContext.ToListAsync());
        }

    }
}
