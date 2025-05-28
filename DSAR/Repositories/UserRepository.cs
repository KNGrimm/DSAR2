using DSAR.Data;
using DSAR.Models;
using DSAR.Repositories;
using Microsoft.AspNetCore.Identity;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;
    private readonly UserManager<User> _userManager; // Use User instead of IdentityUser

    public UserRepository(AppDbContext context, UserManager<User> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public void Create(User user)
    {
        _context.Users.Add(user);
        return;
    }

    public void Delete(string Id)
    {
        var userInDb = _context.Users.Find(Id);

        if (userInDb != null) 
        { 
            _context.Users.Remove(userInDb);
        }
        throw new NotImplementedException();
    }

    public IEnumerable<User> GetAll()
    {
        var allusers = _context.Users;
        return allusers;
    }

    public User GetById(string Id)
    {
        var userInDb = _context.Users.Find(Id);
        return userInDb;
    }

    public void Save()
    {
        _context.SaveChanges();
    }

    public void Update(User user)
    {
        var userInDb = _context.Users.Find(user.Id);
        if (userInDb != null)
        {
            user.FullName = user.FullName;
        }
    }
}