using Microsoft.AspNetCore.Identity;

namespace Rush.Domain.Entities.Users
{
    public class User : IdentityUser
    {
        public bool IsDeleted { get; set; }
        public string? AvatarURL { get; set; }

    }
}