using DiaryAPI.DataAccess.Contexts;
using DiaryAPI.Entities.Entities;
using DiaryAPI.Entities.Repositories;

namespace DiaryAPI.DataAccess.Repository;

public class UserRepository : Repository<UserEntity>, IUserRepository
{
    public UserRepository(DiaryAPIDbContext dbContext) : base(dbContext)
    {
    }
}