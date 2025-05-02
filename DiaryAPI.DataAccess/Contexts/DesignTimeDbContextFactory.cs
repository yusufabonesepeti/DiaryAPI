using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DiaryAPI.DataAccess.Contexts;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<DiaryAPIDbContext>
{
    public DiaryAPIDbContext CreateDbContext(string[] args)
    {
        DbContextOptionsBuilder<DiaryAPIDbContext> dbContextBuilder = new();
        dbContextBuilder.UseNpgsql("User ID=postgres;Password=123456;Host=localhost;Port=5432;Database=DiaryDB;");
        return new(dbContextBuilder.Options);
    }
}