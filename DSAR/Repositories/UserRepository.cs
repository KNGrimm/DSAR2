using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using DSAR.Data;
using DSAR.Models;

namespace DSAR.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Create(IdentityUser user)
        {
            throw new NotImplementedException();
        }

        public void Delete(IdentityUser UserId)
        {
            throw new NotImplementedException();
        }

        public void Delete(string userId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IdentityUser> GetAll()
        {
            return _context.Users.ToList();
        }

        public IdentityUser GetById(string userId)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Update(User user)
        {
            throw new NotImplementedException();
        }
    }
}
