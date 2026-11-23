using System.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using _31241027221_NguyenHoangHung.Data;
using _31241027221_NguyenHoangHung.Extension;
using _31241027221_NguyenHoangHung.Models;
using _31241027221_NguyenHoangHung.Services;

namespace _31241027221_NguyenHoangHung.Controllers;

public sealed class CheckoutController : Controller
{
    private const string LastOrderSessionKey = "LastSubmittedOrderId";

    private readonly ApplicationDbContext _context;
    private readonly UserManager<IdentityUser> _users;
    private readonly IOrderEmailService _emailService;

    public CheckoutController(ApplicationDbContext context, UserManager<IdentityUser> users, IOrderEmailService emailService)
    {
        _context = context;
        _users = users;
        _emailService = emailService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var cart = HttpContext.Session.GetCart();
        if (cart.Count == 0) return RedirectToAction("Index", "Cart");
        return View(new CheckoutViewModel { Items = cart, ReleaseDate = DateTime.Today.AddDays(1) });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Index(CheckoutViewModel input, CancellationToken cancellationToken)
    {
        var cart = HttpContext.Session.GetCart();
        if (cart.Count == 0) return RedirectToAction("Index", "Cart");
        input.Items = cart;
        if (!ModelState.IsValid) return View(input);

        // Reload product prices from SQL Server before committing, so a stale session cannot set the order total.
        var ids = cart.Select(item => item.MovieId).Distinct().ToArray();
        var movies = await _context.Movies.AsNoTracking().Where(movie => ids.Contains(movie.Id)).ToDictionaryAsync(movie => movie.Id, cancellationToken);
        if (movies.Count != ids.Length)
        {
            ModelState.AddModelError(string.Empty, "Một sản phẩm trong giỏ không còn tồn tại. Vui lòng kiểm tra lại giỏ hàng.");
            return View(input);
        }

        foreach (var item in cart)
        {
            var movie = movies[item.MovieId];
            item.Title = movie.Title;
            item.PicturePath = movie.PicturePath;
            item.UnitPrice = movie.Price;
            item.Quantity = Math.Clamp(item.Quantity, 1, 50);
        }

        var order = new HoaDon
        {
            CreatedAt = DateTime.UtcNow,
            UserId = _users.GetUserId(User) ?? string.Empty,
            Total = cart.Sum(item => item.UnitPrice * item.Quantity),
            Items = cart.Select(item => new ChiTietHoaDon
            {
                MovieId = item.MovieId,
                Quantity = item.Quantity,
                UnitPrice = item.UnitPrice
            }).ToList(),
            ShippingDetail = new ShippingDetail
            {
                FullName = input.FullName.Trim(),
                Address = input.Address.Trim(),
                Email = input.Email.Trim(),
                Mobile = input.Mobile.Trim(),
                ReleaseDate = input.ReleaseDate.Date
            }
        };

        await using (var transaction = await _context.Database.BeginTransactionAsync(IsolationLevel.ReadCommitted, cancellationToken))
        {
            _context.HoaDons.Add(order);
            await _context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }

        HttpContext.Session.SetString(LastOrderSessionKey, order.Id.ToString(System.Globalization.CultureInfo.InvariantCulture));
        var emailSent = await _emailService.TrySendConfirmationAsync(order, order.ShippingDetail, movies, cancellationToken);
        HttpContext.Session.ClearCart();
        TempData["EmailSent"] = emailSent;
        return RedirectToAction(nameof(ThankYou), new { id = order.Id });
    }

    [HttpGet]
    public async Task<IActionResult> ThankYou(int id, CancellationToken cancellationToken)
    {
        if (!int.TryParse(
                HttpContext.Session.GetString(LastOrderSessionKey),
                System.Globalization.NumberStyles.None,
                System.Globalization.CultureInfo.InvariantCulture,
                out var permittedOrderId)
            || permittedOrderId != id)
        {
            return NotFound();
        }

        var order = await _context.HoaDons.AsNoTracking()
            .Include(item => item.Items).ThenInclude(item => item.Movie)
            .Include(item => item.ShippingDetail)
            .FirstOrDefaultAsync(item => item.Id == id, cancellationToken);
        return order is null ? NotFound() : View(order);
    }
}
