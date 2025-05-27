using DSAR.Models;
using Microsoft.AspNetCore.Identity;

namespace DSAR.Repositories
{
    public interface IUserRepository
    {
       IEnumerable<IdentityUser> GetAll();
        IdentityUser GetById(string userId);
        void Create(IdentityUser user);
        void Update(User user);
        void Delete(IdentityUser UserId);
        void Save();
        void Delete(string userId);
    }
}
