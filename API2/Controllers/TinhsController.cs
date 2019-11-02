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
    public class TinhsController : ControllerBase
    {
        private readonly RentalCarContext _context;

        public TinhsController(RentalCarContext context)
        {
            _context = context;
        }

        // GET: api/Tinhs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tinh>>> GetTinh()
        {
            return await _context.Tinh.Where(x=> x.Ma == 0 || x.TenTinh=="Hà Nội" || x.TenTinh=="Đà Lạt" || x.TenTinh =="Hải Phòng" || x.TenTinh == "Hồ Chí Minh" ).ToListAsync();
        }

        // GET: api/Tinhs/5
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

        [Produces("application/json")]
        [Consumes("application/json")]
        [HttpPut]
        public async Task<IActionResult> update([FromBody] Tinh tinh)
        {
            try
            {
                _context.Entry(tinh).State = EntityState.Modified;

                await _context.SaveChangesAsync();

                return Ok(tinh);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TinhExists(tinh.Ma))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }


        }

    
        private bool TinhExists(int id)
        {
            return _context.Tinh.Any(e => e.Ma == id);
        }
    }
}
