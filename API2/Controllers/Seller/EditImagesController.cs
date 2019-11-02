using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarRenTal.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace API2.Controllers.Seller
{
    [Route("api/[controller]")]
    [ApiController]
    public class EditImagesController : ControllerBase
    {
        private readonly IHostingEnvironment _env;
        private readonly RentalCarContext _context;

        public EditImagesController(RentalCarContext context, IHostingEnvironment env)
        {
            _context = context;
            _env = env;
        }
     
        // GET: api/EditImages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Images>>> GetImages()
        {
            return await _context.Images.ToListAsync();
        }

        // GET: api/EditImages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Images>> GetImages(int id)
        {
            var images = await _context.Images.FindAsync(id);

            if (images == null)
            {
                return NotFound();
            }

            return images;
        }

        // PUT: api/EditImages/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutImages(int id, [FromForm(Name = "file")] IFormFile file)
        {
            var ima = _context.Images.Find(id);
            var image = new Images();
            if (file != null)
            {
                string path_Root = _env.WebRootPath;

                string path_to_Images = path_Root + "\\Images\\" + file.FileName;

                //</ get Path >
                if (System.IO.File.Exists(path_to_Images))
                {
                    for (int i = 1; i <= 5; i++)
                    {
                        path_to_Images = path_Root + "\\Images\\x" + i + "\\" + file.FileName;
                        image.Src = "\\Images\\" + file.FileName;
                        if (!System.IO.File.Exists(path_to_Images))
                        {
                            break;
                        }

                        if (i == 5)
                        {
                            return Content("something is wrong!!");
                        }
                    }
                }
                else
                {
                    //< Copy File to Target >
                    image.Src = "\\Images\\" + file.FileName;
                }

                using (var stream = new FileStream(path_to_Images, FileMode.Create))

                {
                    await file.CopyToAsync(stream);

                }

                //</ Copy File to Target >
                image.XeId = ima.XeId;
                _context.Update(image);

            }
            

            return Ok();
        }

        // POST: api/EditImages
        [HttpPost]
        public async Task<ActionResult<Images>> PostImages(Images images)
        {
            _context.Images.Add(images);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetImages", new { id = images.Id }, images);
        }

        // DELETE: api/EditImages/5
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
