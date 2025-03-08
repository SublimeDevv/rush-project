using Microsoft.AspNetCore.Identity;

namespace Rush.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public bool IsDeleted { get; set; }
        public string? AvatarURL { get; set; }

        public Guid? ProjectId { get; set; }

    }
}
