using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace DSAR.Models
{
    public class User : IdentityUser
    {

        public string FullName { get; set; }
        public int UserId { get; set; }

    }
    public class test
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

    }
}
