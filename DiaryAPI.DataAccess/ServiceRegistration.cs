using DiaryAPI.DataAccess.Contexts;
using DiaryAPI.DataAccess.Repository;
using DiaryAPI.Entities.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DiaryAPI.DataAccess;

public static class ServiceRegistration
{
    public static void AddDataAccessServices(this IServiceCollection services)
    {
        services.AddDbContext<DiaryAPIDbContext>(options =>
            options.UseNpgsql("User ID=postgres;Password=123456;Host=localhost;Port=5432;Database=DiaryDB;"));

        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IMoodRepository, MoodRepository>();
    }
}