using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarRenTal.Models;

namespace API2.Controllers.Seller
{
    [Route("api/[controller]")]
    [ApiController]
    //lấy badge cho all project
    public class getNotificalController : ControllerBase
    {
        private readonly RentalCarContext _context;

        public getNotificalController(RentalCarContext context)
        {
            _context = context;
        }

        // GET: api/getNotifical
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DonHang>>> GetDonHang()
        {
            return await _context.DonHang.ToListAsync();
        }

        // GET: api/getNotifical/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DonHang>> GetDonHang(int id)
        {
            var donHang = await _context.DonHang.Where(x=>x.MaUs == id && x.Readed== false).ToListAsync();

            if (donHang == null)
            {
                return NotFound();
            }

            return Ok(donHang);
        }

        // PUT: api/getNotifical/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDonHang(int id, DonHang donHang)
        {
            if (id != donHang.Id)
            {
                return BadRequest();
            }

            _context.Entry(donHang).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DonHangExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/getNotifical
        [HttpPost]
        public async Task<ActionResult<DonHang>> PostDonHang(DonHang donHang)
        {
            _context.DonHang.Add(donHang);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDonHang", new { id = donHang.Id }, donHang);
        }

        // DELETE: api/getNotifical/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<DonHang>> DeleteDonHang(int id)
        {
            var donHang = await _context.DonHang.FindAsync(id);
            if (donHang == null)
            {
                return NotFound();
            }

            _context.DonHang.Remove(donHang);
            await _context.SaveChangesAsync();

            return donHang;
        }

        private bool DonHangExists(int id)
        {
            return _context.DonHang.Any(e => e.Id == id);
        }
    }
}
