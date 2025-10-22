using Ecommerce.Areas.User.Models;
using Ecommerce.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Ecommerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();

        public IActionResult Index()
        {
            var products = context.Products.ToList();
            return View(products);
        }


        public IActionResult Remove(int id)
        {
            var products = context.Products.Find(id);
            var uploadDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Imgs",products.Image);
            System.IO.File.Delete(uploadDir);
            context.Products.Remove(products);
            context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Create()
        {
            ViewBag.Categories = context.Categories.ToList();
            return View(new Product());
        }

        public IActionResult Store(Product product, IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                var uploadDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Imgs");
                if (!Directory.Exists(uploadDir))
                {
                    Directory.CreateDirectory(uploadDir);
                }

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                var filePath = Path.Combine(uploadDir, fileName);

                // حفظ الملف
                using (var stream = System.IO.File.Create(filePath))
                {
                    file.CopyTo(stream);
                }

                // حفظ اسم الملف في الـ model
                product.Image = fileName;
           

            context.Products.Add(product);
            context.SaveChanges();

            return RedirectToAction("Index");
            }
            return View("Create", product);
        }

        public ActionResult Edit(int id)
        {
           
            var products = context.Products.Find(id);
            ViewBag.Categories = context.Categories.ToList();
            return View(products);
        }

        public IActionResult Update(Product request, IFormFile? file)
        {
            var product = context.Products.Find(request.Id);
            product.Name = request.Name;
            product.Description = request.Description;
            product.Price = request.Price;
            product.Quantity = request.Quantity;
            product.CategoryId = request.CategoryId;

            if(file != null && file.Length > 0)
            {
                var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Imgs", product.Image);
                System.IO.File.Delete(oldFilePath);


                var fileName = Guid.NewGuid().ToString();
                fileName += Path.GetExtension(file.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(),@"wwwroot\Imgs", fileName);

                using (var stream = System.IO.File.Create(filePath))
                {
                    file.CopyTo(stream);
                }
                product.Image = fileName;
            }

            context.SaveChanges();
            return RedirectToAction("Index");

        }





    }
}
