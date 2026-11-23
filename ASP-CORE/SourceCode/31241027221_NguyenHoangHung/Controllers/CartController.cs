using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using _31241027221_NguyenHoangHung.Data;
using _31241027221_NguyenHoangHung.Extension;
using _31241027221_NguyenHoangHung.Models;

namespace _31241027221_NguyenHoangHung.Controllers;

public sealed class CartController : Controller
{
    private readonly ApplicationDbContext _context;
    public CartController(ApplicationDbContext context) => _context = context;

    public IActionResult Index() => View(HttpContext.Session.GetCart());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Add(int id, CancellationToken cancellationToken)
    {
        var movie = await _context.Movies.AsNoTracking().FirstOrDefaultAsync(item => item.Id == id, cancellationToken);
        if (movie is null) return NotFound();

        var cart = HttpContext.Session.GetCart();
        var line = cart.FirstOrDefault(item => item.MovieId == id);
        if (line is null)
        {
            cart.Add(new CartItem { MovieId = movie.Id, Title = movie.Title, PicturePath = movie.PicturePath, UnitPrice = movie.Price, Quantity = 1 });
        }
        else
        {
            line.Quantity = Math.Min(50, line.Quantity + 1);
            line.Title = movie.Title;
            line.PicturePath = movie.PicturePath;
            line.UnitPrice = movie.Price;
        }
        HttpContext.Session.SaveCart(cart);
        TempData["Notice"] = $"Đã thêm “{movie.Title}” vào giỏ hàng.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Update(int id, int quantity)
    {
        var cart = HttpContext.Session.GetCart();
        var item = cart.FirstOrDefault(row => row.MovieId == id);
        if (item is not null)
        {
            if (quantity <= 0) cart.Remove(item);
            else item.Quantity = Math.Clamp(quantity, 1, 50);
            HttpContext.Session.SaveCart(cart);
        }
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Remove(int id)
    {
        var cart = HttpContext.Session.GetCart();
        cart.RemoveAll(item => item.MovieId == id);
        HttpContext.Session.SaveCart(cart);
        return RedirectToAction(nameof(Index));
    }
}
