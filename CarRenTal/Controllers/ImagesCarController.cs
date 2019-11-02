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

namespace CarRenTal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesCarController : ControllerBase
    {
        private readonly IHostingEnvironment _env;
        private readonly RentalCarContext _context;

        public ImagesCarController(RentalCarContext context, IHostingEnvironment env)
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
        public async Task<IActionResult> PutImages(int id, [FromForm(Name = "file")] IFormFile file, string loai)
        {
            var ima = _context.Images.Find(id);
            string src;
            var image = new Images();
            DateTime s = DateTime.Now;
            string date = s.ToString();
            var charsToRemove = new string[] { "@", ",", ".", ";", "'","-",":"," " };
            foreach (var c in charsToRemove)
            {
                date = date.Replace(c, string.Empty);
            }
            if (file != null)
            {
                string path_Root = _env.WebRootPath;

                string path_to_Images = path_Root + "\\Images\\"+ date + file.FileName;

                //< Copy File to Target >
                src = "\\Images\\" + date + file.FileName;

                using (var stream = new FileStream(path_to_Images, FileMode.Create))

                {
                    await file.CopyToAsync(stream);

                }

                if(id==0)
                {
                    string idcar = file.FileName;
                    var charsToRemove2 = new string[] {" ",".","p","n","g"};
                    foreach (var c in charsToRemove2)
                    {
                        idcar = idcar.Replace(c, string.Empty);
                    }
                    var xe = _context.Xe.Find(Convert.ToInt32(idcar));
                    if(xe== null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        xe.Hinh = src;
                        _context.Entry(xe).State = EntityState.Modified;

                        try
                        {
                            await _context.SaveChangesAsync();
                            return Ok(xe);
                        }
                        catch {
                        }
                    }
                }
                else
                {
                    image.XeId = ima.XeId;
                    ima.Src = src;
                    _context.Entry(ima).State = EntityState.Modified;
                    try
                    {
                        await _context.SaveChangesAsync();
                    }
                    catch
                    {

                    }
                }
                //</ Copy File to Target >
               

            }
            return Ok(image);
        }
        [HttpPost("{id}")]
        public async Task<IActionResult> Postimage(int id, [FromForm(Name = "file")] IFormFile file)
        {
            //tìm xe theo id xe nếu không có return notfault
            var xe = _context.Xe.Find(id);
            if (xe == null)
            {
                return NotFound();
            }
            Images img = new Images();

            DateTime s = DateTime.Now;
            string date = s.ToString();
            var charsToRemove = new string[] { "@", ",", ".", ";", "'", "-", ":", " " };
            foreach (var c in charsToRemove)
            {
                date = date.Replace(c, string.Empty);
            }
            if (file != null)
            {
                string path_Root = _env.WebRootPath;

                string path_to_Images = path_Root + "\\Images\\MainImg\\" + date + file.FileName;

                //< Copy File to Target >
                img.Src = "\\Images\\MainImg\\" + date + file.FileName;

                using (var stream = new FileStream(path_to_Images, FileMode.Create))

                {
                    await file.CopyToAsync(stream);

                }
                     img.XeId = id;
                    _context.Images.Add(img);
                    try
                    {
                        await _context.SaveChangesAsync();
                    }
                    catch
                    {
                    return NoContent();
                    }


            }
            return Ok(xe);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Images>> Delete(int id)
        {
            var cart = await _context.Images.FindAsync(id);
            if (cart == null)
            {
                return NotFound();
            }

            _context.Images.Remove(cart);
            await _context.SaveChangesAsync();

            return cart;
        }
    }
}