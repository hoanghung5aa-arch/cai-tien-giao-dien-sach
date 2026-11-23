using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using _31241027221_NguyenHoangHung.Models;

namespace _31241027221_NguyenHoangHung.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<Microsoft.AspNetCore.Identity.IdentityUser>(options)
{
    public DbSet<Movie> Movies => Set<Movie>();
    public DbSet<Genre> Genres => Set<Genre>();
    public DbSet<HoaDon> HoaDons => Set<HoaDon>();
    public DbSet<ChiTietHoaDon> ChiTietHoaDons => Set<ChiTietHoaDon>();
    public DbSet<ShippingDetail> ShippingDetails => Set<ShippingDetail>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<Genre>().HasIndex(genre => genre.Name).IsUnique();
        builder.Entity<Movie>().Property(movie => movie.Price).HasPrecision(18, 2);
        builder.Entity<Movie>().HasOne(movie => movie.GenreObj).WithMany(genre => genre.Movies)
            .HasForeignKey(movie => movie.GenreId).OnDelete(DeleteBehavior.Restrict);
        builder.Entity<HoaDon>().Property(order => order.Total).HasPrecision(18, 2);
        builder.Entity<HoaDon>().HasMany(order => order.Items).WithOne(item => item.HoaDon)
            .HasForeignKey(item => item.HoaDonId).OnDelete(DeleteBehavior.Cascade);
        builder.Entity<ChiTietHoaDon>().Property(item => item.UnitPrice).HasPrecision(18, 2);
        builder.Entity<ChiTietHoaDon>().HasOne(item => item.Movie).WithMany()
            .HasForeignKey(item => item.MovieId).OnDelete(DeleteBehavior.Restrict);
        builder.Entity<ShippingDetail>().HasOne(shipping => shipping.HoaDon).WithOne(order => order.ShippingDetail)
            .HasForeignKey<ShippingDetail>(shipping => shipping.HoaDonId).OnDelete(DeleteBehavior.Cascade);
        builder.Entity<Genre>().HasData(
            new Genre { Id = 1, Name = "Tâm lý" },
            new Genre { Id = 2, Name = "Khoa học viễn tưởng" },
            new Genre { Id = 3, Name = "Phiêu lưu" });
        builder.Entity<Movie>().HasData(
            new Movie { Id = 1, Title = "Mùa Gió Cuối", Summary = "Một họa sĩ trở về miền biển cũ để tìm lại những bức thư chưa từng gửi.", ReleaseDate = new DateTime(2027, 2, 14), GenreId = 1, Price = 129000m, Rated = 4.8, AgeRating = "T13", PicturePath = "/images/mua-gio.svg" },
            new Movie { Id = 2, Title = "Hành Tinh Thứ Chín", Summary = "Phi hành đoàn nhận được tín hiệu bí ẩn từ rìa ngoài của hệ Mặt Trời.", ReleaseDate = new DateTime(2027, 3, 21), GenreId = 2, Price = 159000m, Rated = 4.6, AgeRating = "T13", PicturePath = "/images/hanh-tinh.svg" },
            new Movie { Id = 3, Title = "Chuyến Tàu Đêm", Summary = "Hành khách trên chuyến tàu cuối ngày cùng khám phá một bí mật bị chôn giấu.", ReleaseDate = new DateTime(2027, 4, 18), GenreId = 3, Price = 139000m, Rated = 4.9, AgeRating = "T16", PicturePath = "/images/chuyen-tau.svg" },
            new Movie { Id = 4, Title = "Tín Hiệu Biển", Summary = "Một người gác hải đăng lần theo tín hiệu phát ra giữa những đêm bão.", ReleaseDate = new DateTime(2027, 5, 9), GenreId = 3, Price = 149000m, Rated = 4.7, AgeRating = "T13", PicturePath = "/images/tin-hieu-bien.svg" },
            new Movie { Id = 5, Title = "Hẻm 36", Summary = "Những người hàng xóm xa lạ tìm cách giữ lại con hẻm thân quen của mình.", ReleaseDate = new DateTime(2027, 6, 1), GenreId = 1, Price = 119000m, Rated = 4.5, AgeRating = "T13", PicturePath = "/images/hem-36.svg" });
    }
}
