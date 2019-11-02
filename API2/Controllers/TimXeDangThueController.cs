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
    public class TimXeDangThueController : ControllerBase
    {
        private readonly RentalCarContext _context;

        public TimXeDangThueController(RentalCarContext context)
        {
            _context = context;
        }

        // GET: api/Xes
        //lấy xe theo user seller
        [HttpGet("{id}")]

        public async Task<ActionResult> donhang(int id, int Loai, String type)
        {
            if (type == "dangthue")
            {
                List<Xe> xe = new List<Xe>();
                DateTime date = DateTime.Now;
                var s= _context.DonHang.Include(x => x.MaXeNavigation).Where(x => x.MaXeNavigation.MaNguoiDang == id && x.MaXeNavigation.MaHangXeNavigation.MaLoaiXe == Loai && x.Huy == false && date >= x.TuNgay && date <= x.DenNgay).ToList();
                foreach (var item in s)
                {
                    xe.Add(new Xe { Id = item.MaXeNavigation.Id, Hinh= item.MaXeNavigation.Hinh,Tenxe= item.MaXeNavigation.Tenxe });
                }
                return Ok(xe);
            }
            else
                return NotFound();
        }
    }
}

           