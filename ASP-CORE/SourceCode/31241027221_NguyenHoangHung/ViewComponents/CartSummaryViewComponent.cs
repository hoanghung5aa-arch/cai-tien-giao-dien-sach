using Microsoft.AspNetCore.Mvc;
using _31241027221_NguyenHoangHung.Extension;

namespace _31241027221_NguyenHoangHung.ViewComponents;

public sealed class CartSummaryViewComponent : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        var cart = HttpContext.Session.GetCart();
        return View(new CartSummary(cart.Sum(item => item.Quantity), cart.Sum(item => item.LineTotal)));
    }

    public sealed record CartSummary(int Count, decimal Total);
}
