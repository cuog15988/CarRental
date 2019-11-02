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
    public class UsersController : ControllerBase
    {
        private readonly RentalCarContext _context;

        public UsersController(RentalCarContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Users>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Users>> GetUsers(int id)
        {
            var Users = await _context.Users.FindAsync(id);

            if (Users == null)
            {
                return NotFound();
            }

            return Users;
        }

        // GET: api/Users/5
        [Produces("application/json")]
        [Consumes("application/json")]
        [HttpPost]
        public async Task<ActionResult<Users>> GetUsers([FromBody] Users user)
        {
            var users = _context.Users.SingleOrDefault(x=>x.UserName == user.UserName);

            if (users == null)
            {
                return NotFound();
            }

            else
                if(users.PassWord == CarRenTal.DAO.Common.Encryptor.MD5Hash(user.PassWord))
            {
                return Ok(users);

            }
            else
            {
                return NotFound();
            }
        }

        // PUT: api/Users/5
        [Produces("application/json")]
        [Consumes("application/json")]
        [HttpPut]
        public async Task<IActionResult> update([FromBody] Users users)
        {
            try
            {
                var us = _context.Users.Find(users.Id);
                    us.HoTen = users.HoTen;
                us.Email = users.Email;
                us.Phone = users.Phone;
                _context.Update(us);

                await _context.SaveChangesAsync();

                return Ok(users);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsersExists(users.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }


        }

        // POST: api/Users
        //[HttpPost]
        //public async Task<ActionResult<Users>> PostUsers(Users users)
        //{
        //    _context.Users.Add(users);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetUsers", new { id = users.Id }, users);
        //}

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Users>> DeleteUsers(int id)
        {
            var users = await _context.Users.FindAsync(id);
            if (users == null)
            {
                return NotFound();
            }

            _context.Users.Remove(users);
            await _context.SaveChangesAsync();

            return users;
        }

        private bool UsersExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
