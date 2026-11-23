using System.ComponentModel.DataAnnotations;

namespace _31241027221_NguyenHoangHung.Models;

public class HoaDon
{
    public int Id { get; set; }

    [Display(Name = "Thời gian đặt")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [DataType(DataType.Currency)]
    [Display(Name = "Tổng tiền")]
    public decimal Total { get; set; }

    [StringLength(450)]
    public string UserId { get; set; } = string.Empty;

    public ICollection<ChiTietHoaDon> Items { get; set; } = new List<ChiTietHoaDon>();
    public ShippingDetail ShippingDetail { get; set; } = null!;
}
