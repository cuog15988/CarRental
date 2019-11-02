using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CarRenTal.Models;
using CarRenTal.Controllers;
using CarRenTal.DAO.Common;

namespace CarRenTal.Areas.User.Controllers
{
    [Area("User")]
    public class ProfileController : BaseUserController
    {
        private readonly RentalCarContext _context;

        public ProfileController(RentalCarContext context)
        {
            _context = context;
        }

     

        // GET: User/Profile/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            if(id!= CommonConstants.UserID)
            {
                return NotFound();
            }


            var users = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (users == null)
            {
                return NotFound();
            }

            return View(users);
        }



        //GET: User/Profile/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await _context.Users.FindAsync(id);
            if (users == null)
            {
                return NotFound();
            }
            return View(users);
        }

        // POST: User/Profile/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserName,PassWord,HoTen,NgaySinh,NgayNhap,DiaChi,Gioitinh,Status,Email,Phone")] Users users)
        {
            var u = await _context.Users.FindAsync(id);
            u.NgaySinh = users.NgaySinh;
            u.Email = users.Email;
            u.Phone = users.Phone;
            if (id != users.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(u);
                    await _context.SaveChangesAsync();
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
                string url = "/User/Profile/Details?id=" + id;
                return Redirect(url);
            }
            return View(users);
        }

        private bool UsersExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
