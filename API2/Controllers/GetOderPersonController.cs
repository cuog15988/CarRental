using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarRenTal.Models;

namespace API2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetOderPersonController : ControllerBase
    {
        private readonly RentalCarContext _context;

        public GetOderPersonController(RentalCarContext context)
        {
            _context = context;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<DonHang>>> GetDonHang(int id)
        {
            var xe = await _context.DonHang.Where(x => x.MaUs == id).OrderByDescending(x=>x.NgayLap).ToListAsync();

            if (xe == null)
            {
                return NotFound();
            }

            return xe;
        }
    }
}