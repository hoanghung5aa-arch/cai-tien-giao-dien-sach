using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using _31241027221_NguyenHoangHung.Validation;

namespace _31241027221_NguyenHoangHung.Models;

public class Movie
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập tên phim.")]
    [StringLength(200, MinimumLength = 3, ErrorMessage = "Tên phim phải có từ 3 đến 200 ký tự.")]
    [Display(Name = "Tên phim")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng nhập phần giới thiệu.")]
    [StringLength(1200, MinimumLength = 3, ErrorMessage = "Phần giới thiệu phải có ít nhất 3 ký tự và tối đa 1.200 ký tự.")]
    [Display(Name = "Giới thiệu")]
    public string Summary { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng chọn ngày phát hành.")]
    [FutureDate]
    [DataType(DataType.Date)]
    [Display(Name = "Ngày phát hành")]
    public DateTime ReleaseDate { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Vui lòng chọn thể loại.")]
    [GenreExists]
    [Display(Name = "Thể loại")]
    public int GenreId { get; set; }

    public Genre GenreObj { get; set; } = null!;

    [Range(typeof(decimal), "5000", "9999999999999999.99", ParseLimitsInInvariantCulture = true, ErrorMessage = "Giá phải từ 5.000 VNĐ trở lên.")]
    [DataType(DataType.Currency)]
    [Display(Name = "Giá bán (VNĐ)")]
    public decimal Price { get; set; }

    [Range(typeof(double), "1", "5", ParseLimitsInInvariantCulture = true, ErrorMessage = "Xếp hạng phải từ 1 đến 5.")]
    [Display(Name = "Xếp hạng")]
    public double Rated { get; set; }

    [StringLength(10)]
    [Display(Name = "Phân loại độ tuổi cũ")]
    public string AgeRating { get; set; } = string.Empty;

    [StringLength(260)]
    [Display(Name = "Đường dẫn ảnh")]
    public string PicturePath { get; set; } = "/images/no-poster.svg";

    [NotMapped]
    [Display(Name = "Ảnh poster")]
    public IFormFile PictureUpload { get; set; } = null!;
}
