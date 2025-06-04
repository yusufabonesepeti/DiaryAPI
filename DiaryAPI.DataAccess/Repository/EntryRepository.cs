using DiaryAPI.DataAccess.Contexts;
using DiaryAPI.Entities.Entities;
using DiaryAPI.Entities.Repositories;

namespace DiaryAPI.DataAccess.Repository;

public class EntryRepository : Repository<EntryEntity>, IEntryRepository
{
    public EntryRepository(DiaryAPIDbContext dbContext) : base(dbContext)
    {
    }
}