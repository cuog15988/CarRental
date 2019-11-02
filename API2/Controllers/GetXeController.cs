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
    public class GetXeController : ControllerBase
    {

        private readonly RentalCarContext _context;

        public GetXeController(RentalCarContext context)
        {
            _context = context;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Xe>>> GetXe(int id)
        {
            return await _context.Xe.Where(x => x.MaHuyen == id).ToListAsync();
        }


        // POST: api/Images
        [HttpPost("{id}")]
        public async Task<ActionResult<Xe>> PostImages(int id)
        {
            var xe = _context.Xe.Find(id);
            if (xe == null)
            {
                return NotFound();
            }
            xe.Moban = !xe.Moban;
            _context.Entry(xe).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch
            {
                return NotFound();
            }
            return Ok(xe);
        }
    }
}