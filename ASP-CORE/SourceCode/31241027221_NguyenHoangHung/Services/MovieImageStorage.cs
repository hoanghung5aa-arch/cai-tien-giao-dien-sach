using Microsoft.AspNetCore.Http;

namespace _31241027221_NguyenHoangHung.Services;

public interface IMovieImageStorage
{
    Task<string> SaveAsync(IFormFile file, CancellationToken cancellationToken = default);
    void DeleteIfLocal(string path);
}

public sealed class MovieImageStorage : IMovieImageStorage
{
    private const long MaxUploadBytes = 5 * 1024 * 1024;
    private static readonly HashSet<string> Extensions = new(StringComparer.OrdinalIgnoreCase)
    {
        ".jpg", ".jpeg", ".png", ".gif", ".webp"
    };

    private readonly IWebHostEnvironment _environment;

    public MovieImageStorage(IWebHostEnvironment environment) => _environment = environment;

    public async Task<string> SaveAsync(IFormFile file, CancellationToken cancellationToken = default)
    {
        if (file is null || file.Length == 0)
        {
            throw new InvalidDataException("Vui lòng chọn một ảnh không rỗng.");
        }

        if (file.Length > MaxUploadBytes)
        {
            throw new InvalidDataException("Ảnh phải nhỏ hơn 5 MB.");
        }

        var extension = Path.GetExtension(Path.GetFileName(file.FileName));
        if (!Extensions.Contains(extension))
        {
            throw new InvalidDataException("Chỉ nhận ảnh JPG, JPEG, PNG, GIF hoặc WEBP.");
        }

        if (!await HasExpectedSignatureAsync(file, extension, cancellationToken))
        {
            throw new InvalidDataException("Nội dung ảnh không khớp với phần mở rộng tệp.");
        }

        var folder = Path.Combine(_environment.WebRootPath, "images", "uploads");
        Directory.CreateDirectory(folder);
        var name = $"{Guid.NewGuid():N}{extension.ToLowerInvariant()}";
        var destination = Path.Combine(folder, name);
        await using var stream = new FileStream(destination, FileMode.CreateNew, FileAccess.Write, FileShare.None);
        await file.CopyToAsync(stream, cancellationToken);
        return $"/images/uploads/{name}";
    }

    public void DeleteIfLocal(string path)
    {
        if (string.IsNullOrWhiteSpace(path) || !path.StartsWith("/images/uploads/", StringComparison.OrdinalIgnoreCase))
        {
            return;
        }

        var name = Path.GetFileName(path);
        if (string.IsNullOrWhiteSpace(name))
        {
            return;
        }

        var candidate = Path.Combine(_environment.WebRootPath, "images", "uploads", name);
        if (File.Exists(candidate))
        {
            File.Delete(candidate);
        }
    }

    private static async Task<bool> HasExpectedSignatureAsync(IFormFile file, string extension, CancellationToken cancellationToken)
    {
        var header = new byte[12];
        await using var stream = file.OpenReadStream();
        var count = await stream.ReadAsync(header.AsMemory(0, header.Length), cancellationToken);
        if (extension.Equals(".jpg", StringComparison.OrdinalIgnoreCase) || extension.Equals(".jpeg", StringComparison.OrdinalIgnoreCase))
        {
            return count >= 3 && header[0] == 0xFF && header[1] == 0xD8 && header[2] == 0xFF;
        }

        if (extension.Equals(".png", StringComparison.OrdinalIgnoreCase))
        {
            return count >= 8 && header.AsSpan(0, 8).SequenceEqual(new byte[] { 137, 80, 78, 71, 13, 10, 26, 10 });
        }

        if (extension.Equals(".gif", StringComparison.OrdinalIgnoreCase))
        {
            return count >= 6 && System.Text.Encoding.ASCII.GetString(header, 0, 6) is "GIF87a" or "GIF89a";
        }

        return extension.Equals(".webp", StringComparison.OrdinalIgnoreCase)
            && count >= 12
            && System.Text.Encoding.ASCII.GetString(header, 0, 4) == "RIFF"
            && System.Text.Encoding.ASCII.GetString(header, 8, 4) == "WEBP";
    }
}
