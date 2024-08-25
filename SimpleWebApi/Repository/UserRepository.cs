using SimpleWebApi.IRepository;
using SimpleWebApi.Model;

namespace SimpleWebApi.Repository
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(SimpleDbContext repositoryContext)
                    : base(repositoryContext)
        {
        }
    }
}
