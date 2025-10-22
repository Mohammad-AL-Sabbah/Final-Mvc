using Ecommerce.Areas.User.Models;
using Ecommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriesController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();

        public IActionResult Index()
        {
            var cats = context.Categories.ToList();
            return View(cats);
        }

        public IActionResult CategoryProducts(int id)
        {
            var category = context.Categories
                .Include(c => c.Products)
                .FirstOrDefault(c => c.Id == id);

            if (category is null)
                return NotFound("Category not found.");

            ViewBag.CategoryName = category.Name;

            return View("/Areas/Admin/Views/Categories/CategoryProducts.cshtml", category.Products);
        }

        public IActionResult RemoveCats(int id)
        {
            var cat = context.Categories.Find(id);
        
                context.Categories.Remove(cat);
                context.SaveChanges();
            return RedirectToAction("Index");
        }


        public IActionResult Create()
        {
            ViewBag.Categories = context.Carousels.ToList(); //sss
            return View(new Category());
        }


        public ActionResult Edit(int id)
        {
            var category = context.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }



        [HttpPost]
        public IActionResult Store(Category request, IFormFile Image)
        {
            // تحقق من عدد الفئات قبل أي عملية
            var categoryCount = context.Categories.Count();
            if (categoryCount >= 3)
            {
                // إعادة عرض الفورم مع رسالة خطأ
                ModelState.AddModelError("", "لا يمكن إضافة أكثر من 3 فئات.");
                return View("Create", request); // "Create" اسم صفحة الفورم
            }

            // رفع الصورة إذا تم اختيارها
            if (Image != null && Image.Length > 0)
            {
                var uploadDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Imgs");
                if (!Directory.Exists(uploadDir))
                    Directory.CreateDirectory(uploadDir);

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(Image.FileName);
                var filePath = Path.Combine(uploadDir, fileName);

                using (var stream = System.IO.File.Create(filePath))
                {
                    Image.CopyTo(stream);
                }

                request.Image = fileName;
            }

            context.Categories.Add(request);
            context.SaveChanges();

            return RedirectToAction("Index");
        }



        [HttpPost]
        public IActionResult SaveLastSelectedCategory(int id)
        {
            var oldSelection = context.LastSelectedCategories.FirstOrDefault();
            if (oldSelection != null)
            {
                context.LastSelectedCategories.Remove(oldSelection);
            }

            context.LastSelectedCategories.Add(new LastSelectedCategory { CategoryId = id });
            context.SaveChanges();

            var categories = context.Categories.ToList();
            return View("Index", categories); 
        }










        [HttpPost]
public IActionResult Update(int Id, string Name, IFormFile? Image)
{
    var cat = context.Categories.Find(Id);
    if (cat == null)
        return NotFound();

    // تحديث الاسم
    cat.Name = Name;

    // تحديث الصورة إذا تم رفع واحدة جديدة
    if (Image != null && Image.Length > 0)
    {
        var uploadDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Imgs");
        if (!Directory.Exists(uploadDir))
        {
            Directory.CreateDirectory(uploadDir);
        }

        // حذف الصورة القديمة إذا وجدت
        if (!string.IsNullOrEmpty(cat.Image))
        {
            var oldFile = Path.Combine(uploadDir, cat.Image);
            if (System.IO.File.Exists(oldFile))
            {
                System.IO.File.Delete(oldFile);
            }
        }

        // حفظ الصورة الجديدة
        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(Image.FileName);
        var filePath = Path.Combine(uploadDir, fileName);

        using (var stream = System.IO.File.Create(filePath))
        {
            Image.CopyTo(stream);
        }

        cat.Image = fileName;
    }

    context.SaveChanges();

    return RedirectToAction("index");
}



    }
}
