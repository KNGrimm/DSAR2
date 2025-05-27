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

    public IEnumerable<User> GetAll()
    {
        return _userManager.Users.ToList(); // This now returns User objects
    }

    public User GetById(string id)
    {
        return _userManager.Users.FirstOrDefault(u => u.Id == id);
    }

    public void Create(User user)
    {
        var result = _userManager.CreateAsync(user).Result;
        if (!result.Succeeded)
        {
            throw new Exception("User creation failed");
        }
    }

    public void Update(User user)
    {
        var result = _userManager.UpdateAsync(user).Result;
        if (!result.Succeeded)
        {
            throw new Exception("User update failed");
        }
    }

    public void Delete(string id)
    {
        var user = GetById(id);
        if (user != null)
        {
            var result = _userManager.DeleteAsync(user).Result;
            if (!result.Succeeded)
            {
                throw new Exception("User deletion failed");
            }
        }
    }

    public void Save()
    {
        _context.SaveChanges();
    }
}