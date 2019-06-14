using CarRenTal.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CarRenTal.wwwroot.DAO;

namespace CarRenTal.ViewComponents
{
    [ViewComponent(Name = "student")]

    public class student:ViewComponent
    {
        private readonly RentalCarContext _context;

        public student(RentalCarContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id, int maxe)
        {
            var s1 = _context.Xe.SingleOrDefault(x => x.Id == maxe);
            ViewData["Tongtien"] = (Seacrch.daydiff * s1.Gia + 2500000);
            ViewData["Xe"] = _context.Xe.SingleOrDefault(x => x.Id == maxe);
            var s = await _context.Users.FirstOrDefaultAsync(x=>x.Id==id);
            return View(s);
            
        }
    }
}
