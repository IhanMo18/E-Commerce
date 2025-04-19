using System.Data.Entity;
using Dashboard.Models;
using Ecommerce.Domain.Interface.Service;
using Ecommerce.Domain.Models;
using Ecommerce.Services.ProductService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Dashboard.Areas.Admin.Controllers;

[Area("Admin")]
public class HomeController(IProductService productService,ICategoryService categoryService,UserManager<User> userManager,
    IWebHostEnvironment webHostEnvironment,IReviewService reviewService,IMessageService messageService):Controller
{
    public async Task<IActionResult> Index()
    {
        // var productVm = await productService.GetAllAsync();
        var userVm = new UsersVm()
        {
            Users = userManager.Users.ToList(),
            Products = (await productService.GetAllAsync())!,
            AllReviews = (await reviewService.GetAllAsync())!
        };
        

        return View(userVm);
    }
    
    public async Task<IActionResult>ProductsTables()
    {
        var products = await productService.GetAllAsync();
        List<ProductVm?> productVms = [];
        foreach (var product in products)
        {
           var productWithCategory = productService.GetProductsWithCategory(product.Id);
           var productWhitReviews = productService.GetProductsWithAllReviews(product.Id);
            ProductVm productVm = new ProductVm()
            {
                Product = productWithCategory,
                Category = productWithCategory.Category,
                AllReviews = productWhitReviews.reviews 
            };
            
            productVms.Add(productVm);
        }
        return View(productVms);
    }

    public async Task<IActionResult> SupportAdmin()
    {
        return View( await messageService.GetAllAsync());
    }
    
    
     [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> Create()
    {
        var listCategories = await categoryService.GetAllAsync();
        var productVm = new ProductVm()
        {
            Categories = listCategories.Select(category => new SelectListItem()
            {
                Text = category.Name,
                Value = category.Id.ToString()
            }),
            Product = new Product()
        };
        var sessionId = Request.Cookies["SessionId"];
        if (sessionId == null) return BadRequest("Error,session Id sin establecer");
        return View(productVm);
    }
    
   [HttpPost]
public async Task<IActionResult> CreateOrUpdate(ProductVm productsVm, IFormFile? file)
{
    if (!ModelState.IsValid)
    {
        var listCategories = await categoryService.GetAllAsync();
        TempData["Error"] = true;

        productsVm.Categories = listCategories.Select(category => new SelectListItem
        {
            Text = category.Name,
            Value = category.Id.ToString()
        });

        return View("Create", productsVm); // Usa la vista correspondiente
    }

    string wwwRootPath = webHostEnvironment.WebRootPath;

    if (file != null)
    {
        var extension = Path.GetExtension(file.FileName);
        var fileName = Guid.NewGuid().ToString() + extension;
        var productPath = Path.Combine(wwwRootPath, "img", "product");

        if (!Directory.Exists(productPath))
        {
            Directory.CreateDirectory(productPath);
        }

        var filePath = Path.Combine(productPath, fileName);

        // Eliminar imagen anterior (solo si es update)
        if (!string.IsNullOrEmpty(productsVm.Product.ImgUrl))
        {
            string oldImagePath = Path.Combine(wwwRootPath, productsVm.Product.ImgUrl.TrimStart('/'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }
        }

        // Guardar la nueva imagen
        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            file.CopyTo(fileStream);
        }

        productsVm.Product.ImgUrl = "/img/product/" + fileName;
    }

    productsVm.Product.ShowProduct = false;

    if (productsVm.Product.Id == 0)
    {
        // Crear nuevo producto
        productService.Update(productsVm.Product);
        TempData["ProductAddSucces"] = true;
    }
    else
    {
        // Actualizar producto existente
        productService.Update(productsVm.Product);
        TempData["ProductUpdateSucces"] = true;
    }

    return RedirectToAction("ProductsTables");
}



    public async Task<IActionResult> ShowProduct(int id)
    {
        var product = await productService.GetByIdAsync(id);
        if (product != null)
        {
            product.ShowProduct = !product.ShowProduct;
            productService.Update(product);
            return RedirectToAction("ProductsTables");
        }

        return NoContent();
    }


    public async Task<IActionResult> Edit(int id)
    {
        var listCategories = await categoryService.GetAllAsync();
        var product = productService.GetProductsWithCategory(id);
        product.reviews = productService.GetProductsWithAllReviews(id).reviews;
        var productVm = new ProductVm()
        {
            Categories = listCategories.Select(category => new SelectListItem()
            {
                Text = category!.Name,
                Value = category.Id.ToString()
            }),
            Product = product!,
            Category = product!.Category
        };
        return View(productVm);
    }
    
}