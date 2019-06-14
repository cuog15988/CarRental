using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CarRenTal.Models;

namespace CarRenTal.Areas.seller.Controllers
{
    [Area("seller")]
    public class CartsController : Controller
    {
        private readonly RentalCarContext _context;

        public CartsController(RentalCarContext context)
        {
            _context = context;
        }

        // GET: seller/Carts
        public async Task<IActionResult> Index()
        {
            var rentalCarContext = _context.Cart.Include(c => c.MaNavigation).Include(c => c.MaxeNavigation);
            return View(await rentalCarContext.ToListAsync());
        }

        // GET: seller/Carts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = await _context.Cart
                .Include(c => c.MaNavigation)
                .Include(c => c.MaxeNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cart == null)
            {
                return NotFound();
            }

            return View(cart);
        }

        // GET: seller/Carts/Create
        public IActionResult Create()
        {
            ViewData["Ma"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["Maxe"] = new SelectList(_context.Xe, "Id", "Id");
            return View();
        }

        // POST: seller/Carts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Ma,Maxe,Tenxe,Gia,Manguoidang")] Cart cart)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cart);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Ma"] = new SelectList(_context.Users, "Id", "Id", cart.Ma);
            ViewData["Maxe"] = new SelectList(_context.Xe, "Id", "Id", cart.Maxe);
            return View(cart);
        }

        // GET: seller/Carts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = await _context.Cart.FindAsync(id);
            if (cart == null)
            {
                return NotFound();
            }
            ViewData["Ma"] = new SelectList(_context.Users, "Id", "Id", cart.Ma);
            ViewData["Maxe"] = new SelectList(_context.Xe, "Id", "Id", cart.Maxe);
            return View(cart);
        }

        // POST: seller/Carts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Ma,Maxe,Tenxe,Gia,Manguoidang")] Cart cart)
        {
            if (id != cart.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cart);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CartExists(cart.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Ma"] = new SelectList(_context.Users, "Id", "Id", cart.Ma);
            ViewData["Maxe"] = new SelectList(_context.Xe, "Id", "Id", cart.Maxe);
            return View(cart);
        }

        // GET: seller/Carts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = await _context.Cart
                .Include(c => c.MaNavigation)
                .Include(c => c.MaxeNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cart == null)
            {
                return NotFound();
            }

            return View(cart);
        }

        // POST: seller/Carts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cart = await _context.Cart.FindAsync(id);
            _context.Cart.Remove(cart);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CartExists(int id)
        {
            return _context.Cart.Any(e => e.Id == id);
        }
    }
}
