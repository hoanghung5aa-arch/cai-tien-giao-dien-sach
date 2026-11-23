namespace _31241027221_NguyenHoangHung.Models;

public class CartItem
{
    public int MovieId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string PicturePath { get; set; } = "/images/no-poster.svg";
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
    public decimal LineTotal => UnitPrice * Quantity;
}
