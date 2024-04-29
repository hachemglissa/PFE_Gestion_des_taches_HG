using PFE_GestionDesTaches2024.Enums;
using Microsoft.AspNetCore.Identity;

namespace PFE_GestionDesTaches2024.Models;

public class ApplicationUser : IdentityUser
{
    public Role Role { get; set; }
}