using Microsoft.AspNetCore.Identity;

namespace Core.Entities;

public class AppUser : IdentityUser
{
    public string? Firstame { get; set; }
    public string? Lastname { get; set; }
    public Address? Address { get; set; }
}

