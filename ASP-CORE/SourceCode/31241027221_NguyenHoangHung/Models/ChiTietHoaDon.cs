using System.ComponentModel.DataAnnotations;

namespace _31241027221_NguyenHoangHung.Models;

public class ChiTietHoaDon
{
    public int Id { get; set; }

    public int HoaDonId { get; set; }
    public HoaDon HoaDon { get; set; } = null!;

    public int MovieId { get; set; }
    public Movie Movie { get; set; } = null!;

    [Range(1, 50)]
    [Display(Name = "Số lượng")]
    public int Quantity { get; set; }

    [DataType(DataType.Currency)]
    [Display(Name = "Đơn giá")]
    public decimal UnitPrice { get; set; }
}
