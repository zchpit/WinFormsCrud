using CommonLibrary.Dto;
using Microsoft.VisualBasic.ApplicationServices;
using SimpleWebApi.Model;

namespace SimpleWebApi.IRepository
{
    public interface IUserRepository : IRepositoryBase<Model.User>
    {
        public ValueTask<SimpleUserDto> GetSimpleUserDto(string username, string password);
    }
}
