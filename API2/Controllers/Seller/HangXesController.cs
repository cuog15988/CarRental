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
    public class HangXesController : ControllerBase
    {
        private readonly RentalCarContext _context;

        public HangXesController(RentalCarContext context)
        {
            _context = context;
        }

        // GET: api/HangXes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HangXe>>> GetHangXe()
        {
            return await _context.HangXe.ToListAsync();
        }

        // GET: api/HangXes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<HangXe>>> GetHangXe(int id)
        {
            var hangXe = await _context.HangXe.Where(x=>x.MaLoaiXe==id).ToListAsync();

            if (hangXe == null)
            {
                return NotFound();
            }

            return hangXe;
        }

        // PUT: api/HangXes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHangXe(int id, HangXe hangXe)
        {
            if (id != hangXe.Id)
            {
                return BadRequest();
            }

            _context.Entry(hangXe).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HangXeExists(id))
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

        // POST: api/HangXes
        [HttpPost]
        public async Task<ActionResult<HangXe>> PostHangXe(HangXe hangXe)
        {
            _context.HangXe.Add(hangXe);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHangXe", new { id = hangXe.Id }, hangXe);
        }

        // DELETE: api/HangXes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<HangXe>> DeleteHangXe(int id)
        {
            var hangXe = await _context.HangXe.FindAsync(id);
            if (hangXe == null)
            {
                return NotFound();
            }

            _context.HangXe.Remove(hangXe);
            await _context.SaveChangesAsync();

            return hangXe;
        }

        private bool HangXeExists(int id)
        {
            return _context.HangXe.Any(e => e.Id == id);
        }
    }
}
