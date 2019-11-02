using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarRenTal.Models;

namespace API2.Controllers.Seller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly RentalCarContext _context;

        public ImagesController(RentalCarContext context)
        {
            _context = context;
        }

        // GET: api/Images
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Images>>> GetImages(int id)
        {
            var im =  _context.Images.Where(x => x.XeId == id).ToList();
            var xe = _context.Xe.Find(id);

            List<Images> ima = new List<Images>();
            ima.Add(new Images { Src = xe.Hinh });
            foreach (var item in im)
            {
                ima.Add(new Images { Id = item.Id, Src = item.Src, XeId = item.XeId });
            }
            return ima;
        }

        // GET: api/Images/5
        // lấy hình theo sản phẩm
        //id= mã sản phẩm
       

        // PUT: api/Images/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutImages(int id, Images images)
        {
            if (id != images.Id)
            {
                return BadRequest();
            }

            _context.Entry(images).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ImagesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

       

        // DELETE: api/Images/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Images>> DeleteImages(int id)
        {
            var images = await _context.Images.FindAsync(id);
            if (images == null)
            {
                return NotFound();
            }

            _context.Images.Remove(images);
            await _context.SaveChangesAsync();

            return images;
        }

        private bool ImagesExists(int id)
        {
            return _context.Images.Any(e => e.Id == id);
        }
    }
}
