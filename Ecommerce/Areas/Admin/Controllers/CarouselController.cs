using Ecommerce.Areas.User.Models;
using Ecommerce.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class CarouselController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult CreateCarousels()
        {
            ViewBag.Categories = context.Carousels.ToList();
            return View("Index");
        }

        public IActionResult StoreCarousel(Carousel carousel, IFormFile Image)
        {
            if (Image != null && Image.Length > 0)
            {
                var uploadDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Imgs");
                if (!Directory.Exists(uploadDir))
                {
                    Directory.CreateDirectory(uploadDir);
                }

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(Image.FileName);
                var filePath = Path.Combine(uploadDir, fileName);

                using (var stream = System.IO.File.Create(filePath))
                {
                    Image.CopyTo(stream);
                }

                carousel.Image = fileName; 


                context.Carousels.Add(carousel);
                context.SaveChanges();

                return RedirectToAction("Index");
            }
            return View("Index", carousel);
        
    }
        public IActionResult Show()
        {
            var carousels = context.Carousels.ToList();
            return View("Show",carousels);
        }
        public IActionResult RemoveCarousel(int id)
        {
            var carousel = context.Carousels.Find(id);
            if (carousel != null)
            {
                context.Carousels.Remove(carousel);
                context.SaveChanges();
            }
            return RedirectToAction("Show");

        }

        public IActionResult Edit(int id)
        {
            var carousel = context.Carousels.Find(id);
            if (carousel == null)
            {
                return NotFound();
            }
            return View("Update", carousel);
        }


        public IActionResult Update(Carousel carousel, IFormFile Image)
        {
            var existingCarousel = context.Carousels.Find(carousel.Id);
            if (existingCarousel != null)
            {
                existingCarousel.Title = carousel.Title;
                existingCarousel.Description = carousel.Description;
                existingCarousel.SubTitle = carousel.SubTitle;

                if (Image != null && Image.Length > 0)
                {
                    var uploadDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Imgs");
                    if (!Directory.Exists(uploadDir))
                    {
                        Directory.CreateDirectory(uploadDir);
                    }
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(Image.FileName);
                    var filePath = Path.Combine(uploadDir, fileName);
                    using (var stream = System.IO.File.Create(filePath))
                    {
                        Image.CopyTo(stream);
                    }
                    existingCarousel.Image = fileName; 
                }
                context.SaveChanges();
            }
            return RedirectToAction("Show");



        }
}
}



