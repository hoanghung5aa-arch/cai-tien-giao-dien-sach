namespace _31241027221_NguyenHoangHung.Models;

public class EmailSettings
{
    public string Host { get; set; } = "smtp.gmail.com";
    public int Port { get; set; } = 587;
    public string UserName { get; set; } = string.Empty;
    public string AppPassword { get; set; } = string.Empty;
    public string FromName { get; set; } = "Góc Phim Hưng";
}
