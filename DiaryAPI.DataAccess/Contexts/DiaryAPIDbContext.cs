using DiaryAPI.Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace DiaryAPI.DataAccess.Contexts;

public class DiaryAPIDbContext : DbContext
{
    public DiaryAPIDbContext() { }
    
    public DiaryAPIDbContext(DbContextOptions options) : base(options) { }

    public DbSet<UserEntity> Users { get; set; }
    public DbSet<MoodEntity> Moods { get; set; }
    public DbSet<EntryEntity> Entries { get; set; }
}