using Ecommerce.Areas.User.Models;
using Ecommerce.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Ecommerce.Areas.User.Controllers
{
    [Area("User")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ApplicationDbContext context = new ApplicationDbContext();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }



        public IActionResult Index()
        {
            ViewBag.Categories = context.Categories.ToList();
            ViewBag.Products = context.Products.ToList();
            ViewBag.Carousels = context.Carousels.ToList();
            ViewBag.ProductsTopRate = context.Products.Where(p => p.Rate >= 4m).Take(5).ToList();
            ViewBag.ProductsLessQuantity = context.Products.Where(p => p.Quantity <= 10).Take(10).ToList();

            ViewBag.LatestProducts = context.Products.OrderByDescending(p => p.Id).Take(5).ToList();

            var lastSelection = context.LastSelectedCategories.FirstOrDefault();
            if (lastSelection != null)
            {
                ViewBag.productsFilter = context.Products
                                                .Where(p => p.CategoryId == lastSelection.CategoryId)
                                                .Take(5)
                                                .ToList();
                ViewBag.CategoryName = context.Categories
                                              .FirstOrDefault(c => c.Id == lastSelection.CategoryId)?.Name;
            }
            else
            {
                ViewBag.productsFilter = new List<Product>();
                ViewBag.CategoryName = "";
            }

            return View("Index");
        }




        public IActionResult _Layout()
        {
            var categories = context.Categories.Take(4).ToList();
            ViewBag.Categories = categories;
            return View("~/Views/Shared/_Layout.cshtml", categories);
        }



        public IActionResult Products(int id)
        {
            ViewBag.Categories = context.Categories.ToList();

            ViewBag.Products = context.Products
                                      .Where(p => p.CategoryId == id)
                                      .ToList();

            ViewBag.CategoryName = context.Categories
                                          .Where(c => c.Id == id)
                                          .Select(c => c.Name)
                                          .FirstOrDefault();

            return View("/Areas/User/Views/Products/Products.cshtml");
        }

        public IActionResult Details(int id)
        {
            ViewBag.Categories = context.Categories.ToList();

            var product = context.Products
                                 .FirstOrDefault(p => p.Id == id);

            ViewBag.RelatedProducts = context.Products
                            .Where(p => p.CategoryId == product.CategoryId && p.Id != id)
                            .Take(4)
                            .ToList();

            return View("/Areas/User/Views/Products/Details.cshtml", product);
        }

        public IActionResult AllProducts(string search)
        {
            ViewBag.Categories = context.Categories.ToList();

            var productsQuery = context.Products.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                search = search.ToLower();
                productsQuery = productsQuery.Where(p =>
                    p.Name.ToLower().Contains(search) ||
                    p.Category.Name.ToLower().Contains(search)  
                );
            }

            var products = productsQuery.ToList();
            return View("/Areas/User/Views/Products/AllProducts.cshtml", products);
        }



        public IActionResult SearchSuggest(string term)
        {
            term = term.ToLower();

            // البحث في المنتجات
            var products = context.Products
                                  .Where(p => p.Name.ToLower().StartsWith(term))
                                  .Select(p => new {
                                      id = p.Id,
                                      name = p.Name,
                                      price = p.Price,
                                      image = p.Image,
                                      categoryId = p.CategoryId
                                  })
                                  .Take(5)
                                  .ToList();

            // البحث في الكاتيجوريز
            var categories = context.Categories
                                    .Where(c => c.Name.ToLower().StartsWith(term))
                                    .Select(c => new {
                                        id = c.Id,
                                        name = c.Name,
                                        latestProducts = context.Products
                                                               .Where(p => p.CategoryId == c.Id)
                                                               .OrderByDescending(p => p.CreatedDate)
                                                               .Take(2)
                                                               .Select(p => new { p.Id, p.Name, p.Price, p.Image })
                                                               .ToList()
                                    })
                                    .ToList();

            return Json(new { products, categories });
        }




        public IActionResult WeAre()
        {
            ViewBag.Categories = context.Categories.ToList();

            return View("/Areas/User/Views/WeAre/Index.cshtml");

        }






        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
