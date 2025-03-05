using Microsoft.AspNetCore.Identity;
namespace DataAcessLayer

{
    public class ApplicationUser : IdentityUser<Guid>
    {

        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string ProfilePicture { get; set; } = string.Empty;
    }
}

