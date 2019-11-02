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
    public class SellerCarController : ControllerBase
    {
        private readonly RentalCarContext _context;

        public SellerCarController(RentalCarContext context)
        {
            _context = context;
        }

        // GET: api/Xes
        //lấy xe theo user seller
        [HttpGet("{id}")]

        public async Task<ActionResult<IEnumerable<Xe>>> GetcarPerUser(int id, int Loai, String type)
        {


            if (String.IsNullOrEmpty(type))
            {
                return await _context.Xe.Where(x => x.MaNguoiDang == id && x.MaHangXeNavigation.MaLoaiXeNavigation.Id == Loai).OrderByDescending(x=>x.NgayNhap).ToListAsync();
            }
            else
            if (type == "active")
            {
                return await _context.Xe.Where(x => x.MaNguoiDang == id && x.MaHangXeNavigation.MaLoaiXeNavigation.Id == Loai && x.Moban == true).OrderByDescending(x => x.NgayNhap).ToListAsync();
            }
            else
            if (type == "Notactive")
            {
                return await _context.Xe.Where(x => x.MaNguoiDang == id && x.MaHangXeNavigation.MaLoaiXeNavigation.Id == Loai && x.Moban == false).OrderByDescending(x => x.NgayNhap).ToListAsync();
            }
            else
            if (type == "chuaduyet")
            {
                return await _context.Xe.Where(x => x.MaNguoiDang == id && x.MaHangXeNavigation.MaLoaiXeNavigation.Id == Loai && x.Status == false).OrderByDescending(x => x.NgayNhap).ToListAsync();
            }
            else
                return NotFound();
            
        }

       
    }
}