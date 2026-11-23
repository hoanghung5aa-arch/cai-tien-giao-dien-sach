using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using _31241027221_NguyenHoangHung.Data;
using _31241027221_NguyenHoangHung.Models;

namespace _31241027221_NguyenHoangHung.Controllers;

public sealed class GenresController : Controller
{
    private readonly ApplicationDbContext _context;
    public GenresController(ApplicationDbContext context) => _context = context;

    public async Task<IActionResult> Index(CancellationToken cancellationToken)
        => View(await _context.Genres.Include(genre => genre.Movies).AsNoTracking().OrderBy(genre => genre.Name).ToListAsync(cancellationToken));

    public async Task<IActionResult> Details(int? id, CancellationToken cancellationToken)
    {
        if (id is null) return NotFound();
        var genre = await _context.Genres.Include(item => item.Movies).AsNoTracking().FirstOrDefaultAsync(item => item.Id == id, cancellationToken);
        return genre is null ? NotFound() : View(genre);
    }

    public IActionResult Create() => View(new Genre());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Genre input, CancellationToken cancellationToken)
    {
        var name = input.Name?.Trim() ?? string.Empty;
        if (await _context.Genres.AnyAsync(genre => genre.Name == name, cancellationToken))
            ModelState.AddModelError(nameof(input.Name), "Tên thể loại này đã tồn tại.");
        if (!ModelState.IsValid) return View(input);
        _context.Genres.Add(new Genre { Name = name });
        try
        {
            await _context.SaveChangesAsync(cancellationToken);
            TempData["Notice"] = $"Đã thêm thể loại “{name}”.";
            return RedirectToAction(nameof(Index));
        }
        catch (DbUpdateException)
        {
            ModelState.AddModelError(nameof(input.Name), "Tên thể loại đã được sử dụng.");
            return View(input);
        }
    }

    public async Task<IActionResult> Edit(int? id, CancellationToken cancellationToken)
    {
        if (id is null) return NotFound();
        var genre = await _context.Genres.FindAsync([id.Value], cancellationToken);
        return genre is null ? NotFound() : View(genre);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Genre input, CancellationToken cancellationToken)
    {
        if (id != input.Id) return NotFound();
        var name = input.Name?.Trim() ?? string.Empty;
        if (await _context.Genres.AnyAsync(genre => genre.Id != id && genre.Name == name, cancellationToken))
            ModelState.AddModelError(nameof(input.Name), "Tên thể loại này đã tồn tại.");
        if (!ModelState.IsValid) return View(input);
        var genreToUpdate = await _context.Genres.FindAsync([id], cancellationToken);
        if (genreToUpdate is null) return NotFound();
        genreToUpdate.Name = name;
        try
        {
            await _context.SaveChangesAsync(cancellationToken);
            TempData["Notice"] = "Đã cập nhật thể loại.";
            return RedirectToAction(nameof(Index));
        }
        catch (DbUpdateException)
        {
            ModelState.AddModelError(nameof(input.Name), "Tên thể loại đã được sử dụng.");
            return View(input);
        }
    }

    public async Task<IActionResult> Delete(int? id, CancellationToken cancellationToken)
    {
        if (id is null) return NotFound();
        var genre = await _context.Genres.Include(item => item.Movies).AsNoTracking().FirstOrDefaultAsync(item => item.Id == id, cancellationToken);
        return genre is null ? NotFound() : View(genre);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id, CancellationToken cancellationToken)
    {
        var genre = await _context.Genres.Include(item => item.Movies).FirstOrDefaultAsync(item => item.Id == id, cancellationToken);
        if (genre is null) return RedirectToAction(nameof(Index));
        if (genre.Movies.Count > 0)
        {
            TempData["Notice"] = "Thể loại đang có phim, hãy chuyển hoặc xóa các phim trước.";
            return RedirectToAction(nameof(Index));
        }
        _context.Genres.Remove(genre);
        await _context.SaveChangesAsync(cancellationToken);
        TempData["Notice"] = "Đã xóa thể loại.";
        return RedirectToAction(nameof(Index));
    }
}
