using Dashboard.Models;
using Ecommerce.Domain.Interface.Service;
using Ecommerce.Domain.Models;
using Ecommerce.Services.ProductService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Dashboard.Areas.Admin.Controllers;

[Area("Admin")]
public class HomeController(IProductService productService,ICategoryService categoryService,
    IWebHostEnvironment webHostEnvironment):Controller
{
    public async Task<IActionResult> Index()
    {
        IEnumerable<Product?> productVm = await productService.GetAllAsync();

        return View(productVm);
    }
    
    public async Task<IActionResult>ProductsTables()
    {
        var products = await productService.GetAllAsync();
        List<ProductVm?> productVms = [];
        
        
        
        foreach (var product in products)
        {
            Product productWithCategory = productService.GetProductsWithCategory(product.Id);
            ProductVm productVm = new ProductVm()
            {
                Product = productWithCategory,
                Category = productWithCategory.Category
            };
            
            productVms.Add(productVm);
        }
        
       
            

        return View(productVms);
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
    public async Task<IActionResult> Create(ProductVm productsVm, IFormFile? file)
    {
        if (!ModelState.IsValid)
        {
            var listCategories = await categoryService.GetAllAsync();
            TempData["Error"] = true;

            IEnumerable<SelectListItem> categories = listCategories.Select(category =>
                new SelectListItem()
                {
                    Text = category.Name,
                    Value = category.Id.ToString()
                });
            productsVm.Categories = categories;
            return View(productsVm);
        }

        string wwwRootPath = webHostEnvironment.WebRootPath;
        
        
        // Si no hay archivo,Guardar el producto
        if (file != null)
        {
            var extension = Path.GetExtension(file.FileName);
            var fileName = Guid.NewGuid().ToString() + extension;
            var productPath = Path.Combine(wwwRootPath,"img", "product");
            if (!Directory.Exists(productPath))
            {
                Directory.CreateDirectory(productPath);
            }
            var filePath = Path.Combine(productPath, fileName);

            // Elimina la imagen anterior si existe
            if (!string.IsNullOrEmpty(productsVm.Product.ImgUrl))
            {
                string pathOldImg = Path.Combine(wwwRootPath, productsVm.Product.ImgUrl.TrimStart('/'));
                if (System.IO.File.Exists(pathOldImg))
                {
                    System.IO.File.Delete(pathOldImg);
                }
            }

            // Guarda la nueva imagen
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(fileStream);
            }
        
            // Asigna la nueva URL de la imagen antes de guardar en BD
            productsVm.Product.ImgUrl = "/img/product/" + fileName;
        }

        productsVm.Product.ShowProduct = false;
        
        productService.Update(productsVm.Product);

        TempData["ProductAddSucces"] = true;
        
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
    
}