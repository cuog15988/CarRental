using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarRenTal.Models;
using System.Net.Http;
using System.Net;
using API2.Model;

namespace API2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OderstatusController : ControllerBase
    {
        private readonly RentalCarContext _context;

        public OderstatusController(RentalCarContext context)
        {
            _context = context;
        }
        //id là mã đơn hàng
        [HttpGet("{id}")]
        public async Task<IActionResult> update(int id)
        {
            var status = new status();
            var donhang = _context.DonHang.Find(id);
            if(donhang.Huy== true)
            {
                status.TinhTrang = "bạn đã huỷ yêu cầu";
            }
            else
            {
                if(donhang.Status==true)
                {
                    if (DateTime.Now > donhang.DenNgay)
                    {
                        status.TinhTrang = "Đã hoàn thành";
                    }
                    else
                        if(DateTime.Now >= donhang.TuNgay && DateTime.Now <= donhang.DenNgay)
                    {
                        status.TinhTrang = "Đang tiến hành";
                    }
                    else
                        if(DateTime.Now< donhang.TuNgay)
                    {
                        status.TinhTrang = "Chưa tiến hành";
                    }
                }
                else
                {
                    if (DateTime.Now >= donhang.TuNgay)
                    {
                        status.TinhTrang = "Yêu cầu quá hạn";
                    }
                    else
                        if(DateTime.Now < donhang.TuNgay)
                    {
                        status.TinhTrang = "Yêu cầu đang xử lý";
                    }
                }
            }

            if (donhang == null)
            {
                return NotFound();
            }

            return Ok(status);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<DonHang>> GetDonHang(int id)
        {
            var donHang = await _context.DonHang.Where(x => x.MaUs == id && x.Readed == true).ToListAsync();

            if (donHang.Count == 0)
            {
                return NotFound();
            }
            foreach (var item in donHang)
            {
                _context.DonHang.Remove(item);
                await _context.SaveChangesAsync();
            }
            return Ok(donHang);
        }

        [HttpPost("{id}")]
        public async Task<ActionResult<DonHang>> UpdateOder(int id)
        {
            var donHang = await _context.DonHang.FindAsync(id);


            if (donHang == null)
            {
                return NotFound();
            }
            donHang.Status = !donHang.Status;
            _context.DonHang.Update(donHang);
            await _context.SaveChangesAsync();
            return Ok(donHang);
        }
    }
}
