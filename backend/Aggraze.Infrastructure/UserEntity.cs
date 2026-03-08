using Microsoft.AspNetCore.Identity;

namespace Aggraze.Infrastructure;

public class UserEntity : IdentityUser
{
    public string Name { get; set; } = string.Empty;

    public string DisplayName { get; set; } = string.Empty;
}
