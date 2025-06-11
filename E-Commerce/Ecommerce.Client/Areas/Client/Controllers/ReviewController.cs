using Dashboard.Models;
using Ecommerce.Domain.Interface.Service;
using Ecommerce.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Dashboard.Areas.Client.Controllers;

[Area("Client")]
public class ReviewController : Controller
{
    private IReviewService _reviewService;

   public ReviewController(IReviewService reviewService)
    {
        _reviewService = reviewService;
    }

    [HttpPost]
    public IActionResult SendReview(ProductVm productVm)
    {
        var productId = productVm.Review.ProductId;
        var review = new Reviews()
        {
            Date = DateTime.UtcNow,
            ProductId = productVm.Review.ProductId,
            Message = productVm.Review.Message,
            Stars = productVm.Review.Stars,
            UserId = productVm.Review.UserId
        };
        _reviewService.Update(review);
        return RedirectToAction("Details", "Home",new{productId=productId});
    }
}