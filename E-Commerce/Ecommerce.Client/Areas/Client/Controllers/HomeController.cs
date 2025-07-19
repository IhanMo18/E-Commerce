using System.Diagnostics;
using Dashboard.Models;
using Ecommerce.Application.CQRS.Mediator;
using Ecommerce.Application.CQRS.Queries;
using Ecommerce.Domain.Interface.Service;
using Ecommerce.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Dashboard.Areas.Client.Controllers;

[Area("Client")]
public class HomeController(
    ILogger<HomeController> logger,
    IMediator mediator,
    ICategoryService categoryService,
    IWebHostEnvironment webHostEnvironment,
    ICartService cartService)
    : Controller
{
    private readonly ILogger<HomeController> _logger = logger;
    private readonly IMediator _mediator = mediator;
    public async Task<IActionResult> Index()
    {
        List<Product> productWhitCategoryList = new List<Product>();
        var products = await _mediator.Send(new GetAllProductsQuery());
        foreach (var product in products)
        {
           var productWhitCategory = await _mediator.Send(new GetProductWithCategoryQuery(product.Id));
           if (productWhitCategory != null) productWhitCategoryList.Add(productWhitCategory);
        }

        return View(productWhitCategoryList);
    }
    

    
    
    public async Task<IActionResult> Details(int productId)
    {
        if (ModelState.IsValid)
        {
            var productWhitAllReviews = await _mediator.Send(new GetProductWithAllReviewsQuery(productId));
            var productWhitCategory = await _mediator.Send(new GetProductWithCategoryQuery(productId));

            if (productWhitCategory == null && productWhitAllReviews == null) return BadRequest();

            var productVm = new ProductVm()
            {
                Category = productWhitCategory?.Category!,
                AllReviews = productWhitAllReviews?.reviews,
                Product = productWhitCategory!,
                Review = new Reviews()
            };
            return View(productVm);
        }
        return View();
    }
    
    public IActionResult Privacy()
    {
        return View();
    }
    
    public IActionResult SupportContact()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}