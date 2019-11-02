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
    public class huyensController : ControllerBase
    {
        private readonly RentalCarContext _context;

        public huyensController(RentalCarContext context)
        {
            _context = context;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Huyen>>> GetTinh(int id)
        {
            return await _context.Huyen.Where(x => x.MaTinh==id).ToListAsync();
        }
    }
    }
