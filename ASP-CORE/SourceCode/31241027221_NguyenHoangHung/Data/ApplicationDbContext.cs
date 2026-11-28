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
            new Genre { Id = 1, Name = "Hoạt hình" },
            new Genre { Id = 2, Name = "Khoa học viễn tưởng" },
            new Genre { Id = 3, Name = "Phiêu lưu" });
        builder.Entity<Movie>().HasData(
            new Movie { Id = 1, Title = "Avengers: Doomsday", Summary = "Các anh hùng từ nhiều vũ trụ đối mặt với một mối đe dọa có thể thay đổi tất cả.", ReleaseDate = new DateTime(2026, 12, 18), GenreId = 2, Price = 159000m, Rated = 4.8, AgeRating = "", PicturePath = "/images/posters/avengers-doomsday.jpg" },
            new Movie { Id = 2, Title = "Dune: Part Three", Summary = "Paul Atreides đối diện hậu quả của quyền lực và một âm mưu đang siết chặt đế chế.", ReleaseDate = new DateTime(2026, 12, 18), GenreId = 2, Price = 159000m, Rated = 4.6, AgeRating = "", PicturePath = "/images/posters/dune-part-three.jpg" },
            new Movie { Id = 3, Title = "Jumanji: Open World", Summary = "Thế giới của trò chơi tràn vào đời thực, mở ra một cuộc phiêu lưu mới cho nhóm bạn.", ReleaseDate = new DateTime(2026, 12, 25), GenreId = 3, Price = 139000m, Rated = 4.9, AgeRating = "", PicturePath = "/images/posters/jumanji-open-world.jpg" },
            new Movie { Id = 4, Title = "Gatto", Summary = "Chú mèo Nero tìm kiếm tình bạn và lối đi riêng giữa những con kênh của Venice.", ReleaseDate = new DateTime(2027, 3, 5), GenreId = 1, Price = 129000m, Rated = 4.7, AgeRating = "", PicturePath = "/images/posters/gatto.jpg" },
            new Movie { Id = 5, Title = "Ice Age: Boiling Point", Summary = "Manny, Sid và Diego bước vào cuộc phiêu lưu mới giữa khủng long và dung nham của Lost World.", ReleaseDate = new DateTime(2027, 2, 5), GenreId = 1, Price = 119000m, Rated = 4.5, AgeRating = "", PicturePath = "/images/posters/ice-age-boiling-point.jpg" });
    }
}
