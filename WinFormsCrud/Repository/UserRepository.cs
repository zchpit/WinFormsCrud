using WinFormsCrud.Dto;
using WinFormsCrud.IRepository;
using WinFormsCrud.Model;

namespace WinFormsCrud.Repository
{
    public class UserRepository : IUserRepository
    {
        private SimpleDbContext userContext;

        public UserRepository(SimpleDbContext context)
        {
            this.userContext = context;
        }

        /*
        List<UserDto> users = new List<UserDto>()
        {
            new UserDto(){ Id = 1, Email = "user@test.pl", Name = "test", Password = "6wDOrMcwJgr4EcGN49lJvA==", UserRole = RoleDto.User, IsActive = true},
            new UserDto(){ Id = 2, Email = "manager@test.pl", Name = "manager", Password = "0y2hZJngk432qjdyUi11mg==", UserRole = RoleDto.Manager, IsActive = true}
        };*/

        public SimpleUserDto GetSimpleUserDto(string username, string password)
        {
            var user = userContext.Users.FirstOrDefault(a => a.IsActive && a.Name == username && a.Password == password);
            if (user == null)
            {
                return null;
            }

            return new SimpleUserDto() { Id = user.Id, UserRole = user.UserRole };
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    userContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
