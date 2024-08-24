using CommonLibrary.Dto;
using Microsoft.EntityFrameworkCore;
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

        public async ValueTask<SimpleUserDto> GetSimpleUserDto(string username, string password)
        {
            var user = await repositoryContext.Users.FirstOrDefaultAsync(a => a.IsActive && a.Name == username && a.Password == password);
            if (user == null)
            {
                return null;
            }

            return new SimpleUserDto() { Id = user.Id, UserRole = user.UserRole };
        }
    }
}
