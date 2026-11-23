using System.ComponentModel.DataAnnotations;

namespace _31241027221_NguyenHoangHung.Models;

public class CheckoutViewModel
{
    [Required(ErrorMessage = "Vui lòng nhập họ tên.")]
    [StringLength(120)]
    [Display(Name = "Họ và tên")]
    public string FullName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng nhập địa chỉ giao hàng.")]
    [StringLength(250)]
    [Display(Name = "Địa chỉ")]
    public string Address { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng nhập email.")]
    [EmailAddress(ErrorMessage = "Email chưa đúng định dạng.")]
    [StringLength(254)]
    [Display(Name = "Email")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng nhập số điện thoại.")]
    [Phone(ErrorMessage = "Số điện thoại chưa đúng định dạng.")]
    [StringLength(20)]
    [Display(Name = "Số điện thoại")]
    public string Mobile { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng chọn ngày giao.")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    [_31241027221_NguyenHoangHung.Validation.FutureDate(ErrorMessage = "Ngày giao phải sau hôm nay.")]
    [Display(Name = "Ngày giao")]
    public DateTime ReleaseDate { get; set; }

    public IReadOnlyList<CartItem> Items { get; set; } = Array.Empty<CartItem>();
    public decimal Total => Items.Sum(item => item.LineTotal);
}
