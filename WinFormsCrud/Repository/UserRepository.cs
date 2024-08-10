﻿using System.Data.Entity;
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


        public async ValueTask<SimpleUserDto> GetSimpleUserDto(string username, string password)
        {
            //var user2 = userContext.Users.FirstOrDefault(a => a.IsActive && a.Name == username && a.Password == password);

            var user = await userContext.Users.FirstOrDefaultAsync(a => a.IsActive && a.Name == username && a.Password == password);
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
