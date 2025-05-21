using DiaryAPI.DataAccess.Contexts;
using DiaryAPI.Entities.Entities;
using DiaryAPI.Entities.Repositories;

namespace DiaryAPI.DataAccess.Repository;

public class MoodRepository : Repository<MoodEntity>, IMoodRepository
{
    public MoodRepository(DiaryAPIDbContext dbContext) : base(dbContext)
    {
    }
}