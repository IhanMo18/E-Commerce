using System.Diagnostics;
using Dashboard.Models;
using Ecommerce.Domain.Interface.Service;
using Ecommerce.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Dashboard.Areas.Client.Controllers;

[Area("Client")]
public class HomeController(
    ILogger<HomeController> logger,
    IProductService productService,
    ICategoryService categoryService,
    IWebHostEnvironment webHostEnvironment,
    ICartService cartService)
    : Controller
{
    private readonly ILogger<HomeController> _logger = logger;

    public async Task<IActionResult> Index()
    {
        List<Product> productWhitCategoryList = new List<Product>();
        foreach (var product in await productService.GetAllAsync() )
        {
           var productWhitCategory = productService.GetProductsWithCategory(product.Id);
           if (productWhitCategory!=null)productWhitCategoryList.Add(productWhitCategory);
        }
        
        return View(productWhitCategoryList);
    }
    

    
    
    public IActionResult Details(int productId)
    {
        
        if (ModelState.IsValid)
        {
            var productWhitAllReviews = productService.GetProductsWithAllReviews(productId);
            var productWhitCategory =   productService.GetProductsWithCategory(productId);

            if (productWhitCategory == null && productWhitAllReviews == null) return BadRequest();
           
            var productVm = new ProductVm()
            {
                Category = productWhitCategory.Category,
                AllReviews = productWhitAllReviews?.reviews,
                Product = productWhitCategory,
                Review = new Reviews()
            }; return View(productVm);
        }
        return View();
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