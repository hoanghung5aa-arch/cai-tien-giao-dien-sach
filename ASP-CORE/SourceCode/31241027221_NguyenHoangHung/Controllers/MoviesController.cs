#nullable enable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using _31241027221_NguyenHoangHung.Data;
using _31241027221_NguyenHoangHung.Extension;
using _31241027221_NguyenHoangHung.Models;
using _31241027221_NguyenHoangHung.Services;

namespace _31241027221_NguyenHoangHung.Controllers;

public sealed class MoviesController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly IMovieImageStorage _imageStorage;

    public MoviesController(ApplicationDbContext context, IMovieImageStorage imageStorage)
    {
        _context = context;
        _imageStorage = imageStorage;
    }

    public async Task<IActionResult> Index(string? search, int? genreId, CancellationToken cancellationToken)
    {
        var movies = _context.Movies.Include(movie => movie.GenreObj).AsNoTracking();
        if (!string.IsNullOrWhiteSpace(search))
        {
            var term = search.Trim();
            movies = movies.Where(movie => movie.Title.Contains(term));
        }
        if (genreId is > 0)
        {
            movies = movies.Where(movie => movie.GenreId == genreId.Value);
        }

        ViewBag.Search = search;
        ViewBag.Genres = new SelectList(await _context.Genres.AsNoTracking().OrderBy(g => g.Name).ToListAsync(cancellationToken), "Id", "Name", genreId);
        return View(await movies.OrderBy(movie => movie.Title).ToListAsync(cancellationToken));
    }

    public async Task<IActionResult> Details(int? id, CancellationToken cancellationToken)
    {
        if (id is null) return NotFound();
        var movie = await _context.Movies.Include(item => item.GenreObj)
            .AsNoTracking().FirstOrDefaultAsync(item => item.Id == id, cancellationToken);
        return movie is null ? NotFound() : View(movie);
    }

    // Lab route used by the assignment: POST /Movies/AddToCart/{id} keeps the address visible
    // while rendering the updated cart. The form tag helper adds an anti-forgery token.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddToCart(int id, CancellationToken cancellationToken)
    {
        var movie = await _context.Movies.AsNoTracking().FirstOrDefaultAsync(item => item.Id == id, cancellationToken);
        if (movie is null) return NotFound();
        var cart = HttpContext.Session.GetCart();
        var line = cart.FirstOrDefault(item => item.MovieId == movie.Id);
        if (line is null)
            cart.Add(new CartItem { MovieId = movie.Id, Title = movie.Title, PicturePath = movie.PicturePath, UnitPrice = movie.Price, Quantity = 1 });
        else
        {
            line.Quantity = Math.Min(50, line.Quantity + 1);
            line.Title = movie.Title;
            line.PicturePath = movie.PicturePath;
            line.UnitPrice = movie.Price;
        }
        HttpContext.Session.SaveCart(cart);
        TempData["Notice"] = $"Đã thêm “{movie.Title}” vào giỏ hàng.";
        return View("~/Views/Cart/Index.cshtml", cart);
    }

    public async Task<IActionResult> Create(CancellationToken cancellationToken)
    {
        await LoadGenresAsync(null, cancellationToken);
        return View(new Movie { ReleaseDate = DateTime.Today.AddDays(1), Rated = 4.5 });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Movie movie, CancellationToken cancellationToken)
    {
        NormalizeTextAndValidateMinimumLengths(movie);
        if (!ModelState.IsValid)
        {
            await LoadGenresAsync(movie.GenreId, cancellationToken);
            return View(movie);
        }

        var newPicture = "/images/no-poster.svg";
        try
        {
            if (movie.PictureUpload is { Length: > 0 })
                newPicture = await _imageStorage.SaveAsync(movie.PictureUpload, cancellationToken);
            movie.PicturePath = newPicture;
            movie.GenreObj = null!;
            _context.Movies.Add(movie);
            await _context.SaveChangesAsync(cancellationToken);
            TempData["Notice"] = $"Đã thêm phim “{movie.Title}” vào danh mục.";
            return RedirectToAction(nameof(Index));
        }
        catch (InvalidDataException exception)
        {
            ModelState.AddModelError(nameof(movie.PictureUpload), exception.Message);
        }
        catch
        {
            if (newPicture != "/images/no-poster.svg") _imageStorage.DeleteIfLocal(newPicture);
            throw;
        }

        await LoadGenresAsync(movie.GenreId, cancellationToken);
        return View(movie);
    }

    public async Task<IActionResult> Edit(int? id, CancellationToken cancellationToken)
    {
        if (id is null) return NotFound();
        var movie = await _context.Movies.FindAsync([id.Value], cancellationToken);
        if (movie is null) return NotFound();
        await LoadGenresAsync(movie.GenreId, cancellationToken);
        return View(movie);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Movie input, CancellationToken cancellationToken)
    {
        if (id != input.Id) return NotFound();
        NormalizeTextAndValidateMinimumLengths(input);
        var movie = await _context.Movies.FindAsync([id], cancellationToken);
        if (movie is null) return NotFound();

        if (!ModelState.IsValid)
        {
            input.PicturePath = movie.PicturePath;
            await LoadGenresAsync(input.GenreId, cancellationToken);
            return View(input);
        }

        var oldPicture = movie.PicturePath;
        var newPicture = string.Empty;
        try
        {
            if (input.PictureUpload is { Length: > 0 })
                newPicture = await _imageStorage.SaveAsync(input.PictureUpload, cancellationToken);

            movie.Title = input.Title.Trim();
            movie.Summary = input.Summary.Trim();
            movie.ReleaseDate = input.ReleaseDate;
            movie.GenreId = input.GenreId;
            movie.Price = input.Price;
            movie.Rated = input.Rated;
            if (!string.IsNullOrEmpty(newPicture)) movie.PicturePath = newPicture;

            await _context.SaveChangesAsync(cancellationToken);
            if (!string.IsNullOrEmpty(newPicture)) _imageStorage.DeleteIfLocal(oldPicture);
            TempData["Notice"] = $"Đã cập nhật “{movie.Title}”.";
            return RedirectToAction(nameof(Index));
        }
        catch (InvalidDataException exception)
        {
            if (!string.IsNullOrEmpty(newPicture)) _imageStorage.DeleteIfLocal(newPicture);
            ModelState.AddModelError(nameof(input.PictureUpload), exception.Message);
        }

        input.PicturePath = oldPicture;
        await LoadGenresAsync(input.GenreId, cancellationToken);
        return View(input);
    }

    public async Task<IActionResult> Delete(int? id, CancellationToken cancellationToken)
    {
        if (id is null) return NotFound();
        var movie = await _context.Movies.Include(item => item.GenreObj)
            .AsNoTracking().FirstOrDefaultAsync(item => item.Id == id, cancellationToken);
        return movie is null ? NotFound() : View(movie);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id, CancellationToken cancellationToken)
    {
        var movie = await _context.Movies.FindAsync([id], cancellationToken);
        if (movie is not null)
        {
            var image = movie.PicturePath;
            _context.Movies.Remove(movie);
            try
            {
                await _context.SaveChangesAsync(cancellationToken);
                _imageStorage.DeleteIfLocal(image);
                TempData["Notice"] = "Đã xóa phim khỏi danh mục.";
            }
            catch (DbUpdateException)
            {
                TempData["Notice"] = "Không thể xóa phim đã có trong đơn hàng.";
            }
        }
        return RedirectToAction(nameof(Index));
    }

    private async Task LoadGenresAsync(int? selectedId, CancellationToken cancellationToken)
    {
        ViewData["GenreId"] = new SelectList(
            await _context.Genres.AsNoTracking().OrderBy(genre => genre.Name).ToListAsync(cancellationToken),
            "Id", "Name", selectedId);
    }

    private void NormalizeTextAndValidateMinimumLengths(Movie movie)
    {
        movie.Title = movie.Title?.Trim() ?? string.Empty;
        movie.Summary = movie.Summary?.Trim() ?? string.Empty;

        if (movie.Title.Length < 3 && (ModelState[nameof(movie.Title)]?.Errors.Count ?? 0) == 0)
            ModelState.AddModelError(nameof(movie.Title), "Tên phim phải có từ 3 đến 200 ký tự.");
        if (movie.Summary.Length < 3 && (ModelState[nameof(movie.Summary)]?.Errors.Count ?? 0) == 0)
            ModelState.AddModelError(nameof(movie.Summary), "Phần giới thiệu phải có ít nhất 3 ký tự và tối đa 1.200 ký tự.");
    }
}
