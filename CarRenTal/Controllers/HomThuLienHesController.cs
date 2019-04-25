using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CarRenTal.Models;

namespace CarRenTal.Controllers
{
    public class HomThuLienHesController : Controller
    {
        private readonly RentalCarContext _context;

        public HomThuLienHesController(RentalCarContext context)
        {
            _context = context;
        }

      

        // GET: HomThuLienHes/Details/5
       
        // GET: HomThuLienHes/Create
        public IActionResult Index()
        {
            return View();
        }

        // POST: HomThuLienHes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([Bind("Id,NgayGui,NguoiGui,NoiDung,Email")] HomThuLienHe homThuLienHe)
        {
            if (ModelState.IsValid)
            {
                homThuLienHe.NgayGui = DateTime.Now;
                _context.Add(homThuLienHe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(homThuLienHe);
        }

        private bool HomThuLienHeExists(int id)
        {
            return _context.HomThuLienHe.Any(e => e.Id == id);
        }
    }
}
