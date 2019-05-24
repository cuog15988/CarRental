using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CarRenTal.Models;
using CarRenTal.DAO.Common;

namespace CarRenTal.Controllers
{
    public class CartsController : BaseUserController
    {
        private readonly RentalCarContext _context;

        public CartsController(RentalCarContext context)
        {
            _context = context;
        }

        // GET: Carts
        public async Task<IActionResult> Index(int? id)
        {
            var rentalCarContext = _context.Cart.Include(c => c.MaxeNavigation.MaNguoiDangNavigation).Include(x=>x.MaxeNavigation).Where(x => x.Ma == id);
            if (id != CommonConstants.UserID)
            {
                return NotFound();
            }

            return View(await rentalCarContext.ToListAsync());


        }

        // GET: Carts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = await _context.Cart
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cart == null)
            {
                return NotFound();
            }

            return View(cart);
        }



        // GET: Carts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var cart = await _context.Cart.FindAsync(id);
            _context.Cart.Remove(cart);
            await _context.SaveChangesAsync();
            string referer1 = "~/Carts?id=" + CommonConstants.UserID;
            return Redirect(referer1);
        }

        public IActionResult saveCart(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var xe = _context.Xe.SingleOrDefault(x => x.Id == id);
            Cart c = new Cart();
            if (xe == null)
            {
                return NotFound();
            }

            var cart = _context.Cart.SingleOrDefault(x => x.Maxe == id);
            if (cart == null)
            {

                if (ModelState.IsValid)
                {
                    c.Ma = CommonConstants.UserID;
                    c.Manguoidang = xe.MaNguoiDang;
                    c.Maxe = xe.Id;
                    c.Gia = xe.Gia;
                    c.Tenxe = xe.Tenxe;

                    _context.Add(c);
                    _context.SaveChangesAsync();
                    string referer1 = Request.Headers["Referer"].ToString();
                    return Redirect(referer1);
                }
            }
            return Redirect(Request.Headers["Referer"].ToString());
        }

    }
}
