using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarRenTal.Models;
using API2.Model;

namespace API2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartPerUserController : ControllerBase
    {
        private readonly RentalCarContext _context;

        public CartPerUserController(RentalCarContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Cart>>> GetCart(int id)
        {
            return await _context.Cart.Where(x => x.Ma == id).OrderByDescending(x=>x.Id).ToListAsync();
        }

    }
}