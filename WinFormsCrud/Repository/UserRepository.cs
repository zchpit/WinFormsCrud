using Microsoft.VisualBasic.ApplicationServices;
using WinFormsCrud.Dto;
using WinFormsCrud.IRepository;

namespace WinFormsCrud.Repository
{
    public class UserRepository : IUserRepository
    {
        public UserRepository() { }

        List<UserDto> users = new List<UserDto>()
        {
            new UserDto(){ Id = 1, Email = "user@test.pl", Name = "test", Password = "6wDOrMcwJgr4EcGN49lJvA==", UserRole = RoleDto.User, IsActive = true},
            new UserDto(){ Id = 2, Email = "manager@test.pl", Name = "manager", Password = "0y2hZJngk432qjdyUi11mg==", UserRole = RoleDto.Manager, IsActive = true}
        };


        public SimpleUserDto GetSimpleUserDto(string username, string password)
        {
            var user = users.FirstOrDefault(a => a.IsActive && a.Name == username && a.Password == password);
            if (user == null)
            {
                return null;
            }

            return new SimpleUserDto() { Id = user.Id, UserRole = user.UserRole };
        }
    }
}
