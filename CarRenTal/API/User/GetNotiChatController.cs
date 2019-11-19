using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CarRenTal.DAO;
using CarRenTal.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRenTal.API.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetNotiChatController : ControllerBase
    {
        private readonly RentalCarContext _context;

        public GetNotiChatController(RentalCarContext context)
        {
            _context = context;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ChatList>> GetDonHang(int id)
        {
            var donHang = await _context.ChatList.Where(x=>x.MyUser== id || x.FromUser ==id).ToListAsync();
           
            if (donHang == null)
            {
                return NotFound();
            }
            var s = donHang.Where(x => x.Status == true && x.MsFrom != id).ToList();
            if(s==null)
            {
                return NotFound();
            }
            return Ok(s);
        }
    }
}