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

namespace API2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetCarController : ControllerBase
    {
        private readonly RentalCarContext _context;

        public GetCarController(RentalCarContext context)
        {
            _context = context;
        }
        //lấy xe theo mã xe
        [HttpGet("{id}")]
        public async Task<ActionResult<Xe>> getcar(int id)
        {
            var xe = await _context.Xe.FindAsync(id);

            if (xe == null)
            {
                return NotFound();
            }

            return xe;
        }
    }
}