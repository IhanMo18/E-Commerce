
using Dashboard.Models;
using Ecommerce.Domain.Interface.Service;
using Ecommerce.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Dashboard.Areas.Client.Controllers;

[Area("Client")]
public class CategoryController(ILogger<CategoryController> logger
    ,ICategoryService categoryService) : Controller
{
    
    public IActionResult CreateCategory()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult CreateCategory(CategoryVM categoryVm)
    {
        Category category = new Category()
        {
            Name = categoryVm.Name
        };
        if (ModelState.IsValid)
        {
            categoryService.Update(category);
            return RedirectToAction("Index","Home");
        }
        
        return View();
    }
}