using System.ComponentModel.DataAnnotations;

namespace _31241027221_NguyenHoangHung.Models;

public class Genre
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập tên thể loại.")]
    [StringLength(200, ErrorMessage = "Tên thể loại tối đa 200 ký tự.")]
    [Display(Name = "Tên thể loại")]
    public string Name { get; set; } = string.Empty;

    public ICollection<Movie> Movies { get; set; } = new List<Movie>();
}
