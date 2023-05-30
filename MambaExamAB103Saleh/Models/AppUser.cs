using Microsoft.AspNetCore.Identity;

namespace MambaExamAB103Saleh.Models
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
