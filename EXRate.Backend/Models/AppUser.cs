using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace EXRate.Backend.Models
{
    public class AppUser : IdentityUser
    {
        [StringLength(100)]
        public string FirstName { get; set; }

        [StringLength(100)]
        public string LastName { get; set; }


    }
}
