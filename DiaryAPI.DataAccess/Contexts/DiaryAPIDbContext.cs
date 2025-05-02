using DiaryAPI.Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace DiaryAPI.DataAccess.Contexts;

public class DiaryAPIDbContext : DbContext
{
    public DiaryAPIDbContext() { }
    
    public DiaryAPIDbContext(DbContextOptions options) : base(options) { }

    public DbSet<UserEntity> Users { get; set; }
}