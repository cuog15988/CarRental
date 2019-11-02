using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRenTal.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API2.Controllers.Seller
{
    [Route("api/[controller]")]
    [ApiController]
    public class SellerOderController : ControllerBase
    {
        private readonly RentalCarContext _context;

        public SellerOderController(RentalCarContext context)
        {
            _context = context;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Xe>> Readedrequest(int id)
        {
            var xe = await _context.DonHang.FindAsync(id);
            if (xe == null)
            {
                return NotFound();
            }
            xe.Readed = true;
            _context.DonHang.Update(xe);
            await _context.SaveChangesAsync();

            return Ok(xe);
        }
       
    }
}