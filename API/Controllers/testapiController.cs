using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarRenTal.Models;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class testapiController : ControllerBase
    {
        private readonly RentalCarContext _context;

        public testapiController(RentalCarContext context)
        {
            _context = context;
        }

        // GET: api/testapi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tinh>>> GetTinh()
        {
            return await _context.Tinh.ToListAsync();
        }

        // GET: api/testapi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tinh>> GetTinh(int id)
        {
            var tinh = await _context.Tinh.FindAsync(id);

            if (tinh == null)
            {
                return NotFound();
            }

            return tinh;
        }

        // PUT: api/testapi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTinh(int id, Tinh tinh)
        {
            if (id != tinh.Ma)
            {
                return BadRequest();
            }

            _context.Entry(tinh).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TinhExists(id))
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

        // POST: api/testapi
        [HttpPost]
        public async Task<ActionResult<Tinh>> PostTinh(Tinh tinh)
        {
            _context.Tinh.Add(tinh);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TinhExists(tinh.Ma))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTinh", new { id = tinh.Ma }, tinh);
        }

        // DELETE: api/testapi/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Tinh>> DeleteTinh(int id)
        {
            var tinh = await _context.Tinh.FindAsync(id);
            if (tinh == null)
            {
                return NotFound();
            }

            _context.Tinh.Remove(tinh);
            await _context.SaveChangesAsync();

            return tinh;
        }

        private bool TinhExists(int id)
        {
            return _context.Tinh.Any(e => e.Ma == id);
        }
    }
}
