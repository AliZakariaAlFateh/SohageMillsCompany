using Microsoft.AspNetCore.Identity;
using Sohag__Mills_Company.Models.DataContext;

namespace Sohag__Mills_Company.Services
{
    public class UserRepository : IUserRepository<IdentityUser>
    {

        private readonly ECcontext db;
        public UserRepository(ECcontext _db)
        {
            db = _db;
        }

        public void Add(IdentityUser entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public int Save()
        {
            throw new NotImplementedException();
        }

        public void Update(IdentityUser entity)
        {
            throw new NotImplementedException();
        }
    }
}
