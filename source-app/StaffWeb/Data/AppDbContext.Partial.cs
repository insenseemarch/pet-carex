using Microsoft.EntityFrameworkCore;
using StaffWeb.Models;

namespace StaffWeb.Data;

public partial class AppDbContext
{
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<LoginResult>().HasNoKey().ToView(null);
        modelBuilder.Entity<StaffLoginResult>().HasNoKey().ToView(null);
        modelBuilder.Entity<RevenueByBranch>().HasNoKey().ToView(null);
        modelBuilder.Entity<DoctorStat>().HasNoKey().ToView(null);
        modelBuilder.Entity<ProductStat>().HasNoKey().ToView(null);
        modelBuilder.Entity<VisitStat>().HasNoKey().ToView(null);
    }
}
