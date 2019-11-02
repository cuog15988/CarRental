using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarRenTal.Models;

namespace API2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class XesController : ControllerBase
    {
        private readonly RentalCarContext _context;

        public XesController(RentalCarContext context)
        {
            _context = context;
        }

        // GET: api/Xes
        //lấy xe theo user seller
        [HttpGet("{id}")]

        public async Task<ActionResult<IEnumerable<Xe>>> GetcarPerUser(int id)
        {
            if(id==null)
            {
                return NotFound();
            }
            return await _context.Xe.Where(x=>x.MaNguoiDang==id).ToListAsync();
        }

        // GET: api/Xes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Xe>> GetXe(int id)
        {
            var xe = await _context.Xe.FindAsync(id);

            if (xe == null)
            {
                return NotFound();
            }

            return xe;
        }

        // PUT: api/Xes/5
        [Produces("application/json")]
        [Consumes("application/json")]
        [HttpPut]
        public async Task<IActionResult> PutXe(Xe xe)
        {
            var x = _context.Xe.Find(xe.Id);
            Xe ren = new Xe();
            if(xe.MaHuyen!= null )
            {
                x.MaHuyen = xe.MaHuyen;
                var huyen = _context.Huyen.Include(a => a.MaTinhNavigation).Where(a => a.Id == xe.MaHuyen).SingleOrDefault();

                if (huyen == null)
                {
                    return NotFound();
                }
                x.Huyen = huyen.TenHuyen;
                x.Tinh = huyen.MaTinhNavigation.TenTinh;
            }
            
            else
            if(xe.MaHangXe != null)
            {
                var Hangxe = _context.HangXe.Find(xe.MaHangXe);
                if(Hangxe == null)
                {
                    return NotFound();
                }

                if( !string.IsNullOrEmpty(xe.Tenxe))
                {
                    x.Tenxe = xe.Tenxe;
                }

                x.MaHangXe = xe.MaHangXe;
                x.TenHang = Hangxe.TenHang;
            }
            else
            {
                x.BienSo = xe.BienSo;
                x.SoKM = xe.SoKM;
                x.Gia = xe.Gia;
            }




            _context.Entry(x).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                ren = _context.Xe.Find(xe.Id);
                ren.MaHuyenNavigation = null;
                ren.MaHangXeNavigation = null;
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

            return Ok(ren);
        }

        // POST: api/Xes
        [HttpPost]
        public async Task<ActionResult<Xe>> PostXe(Xe xe)
        {
            xe.NgayNhap = DateTime.Now;
            var h = _context.Huyen.Include(x => x.MaTinhNavigation).Where(x => x.Id == xe.MaHuyen).Single();
            var l = _context.HangXe.Include(x => x.MaLoaiXeNavigation).Where(x => x.Id == xe.MaHangXe).Single();
            var nguoidang = _context.Users.Find(xe.MaNguoiDang);
            if(h==null)
            {
                return NotFound();
            }
            xe.Tinh = h.MaTinhNavigation.TenTinh;
            xe.Huyen = h.TenHuyen;
            xe.TenLoai = _context.HangXe.Find(xe.MaHangXe).MaLoaiXeNavigation.TenLoai;
            xe.TenHang = l.TenHang;
            xe.LoaiXe = l.MaLoaiXeNavigation.TenLoai;
            xe.TenLoai = l.MaLoaiXeNavigation.TenLoai;
            xe.TenNguoiDang = nguoidang.HoTen;
            _context.Xe.Add(xe);
            await _context.SaveChangesAsync();
            xe.MaHangXeNavigation = null;
            xe.MaNguoiDangNavigation = null;
            xe.MaHuyenNavigation = null;
            return xe;
        }

        // DELETE: api/Xes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Xe>> DeleteXe(int id)
        {
            var xe = await _context.Xe.FindAsync(id);
            if (xe == null)
            {
                return NotFound();
            }

            _context.Xe.Remove(xe);
            await _context.SaveChangesAsync();

            return xe;
        }

        private bool XeExists(int id)
        {
            return _context.Xe.Any(e => e.Id == id);
        }
    }
}
