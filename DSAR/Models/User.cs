using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace DSAR.Models
{
    public class User : IdentityUser
    {
        
        public int UserId { get; set; }
        public string FullName { get; set; }
        
    }
}
