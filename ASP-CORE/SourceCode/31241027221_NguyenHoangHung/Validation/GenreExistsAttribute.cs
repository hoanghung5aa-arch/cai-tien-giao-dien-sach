using System.ComponentModel.DataAnnotations;
using _31241027221_NguyenHoangHung.Data;

namespace _31241027221_NguyenHoangHung.Validation;

public sealed class GenreExistsAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is not int genreId || genreId <= 0)
        {
            return new ValidationResult("Vui lòng chọn thể loại.");
        }

        var db = validationContext.GetService(typeof(ApplicationDbContext)) as ApplicationDbContext;
        if (db is null || db.Genres.Any(genre => genre.Id == genreId))
        {
            return ValidationResult.Success;
        }

        return new ValidationResult("Thể loại đã chọn không tồn tại.");
    }
}
