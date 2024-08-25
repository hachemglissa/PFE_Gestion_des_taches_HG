using PFE_Gestion_des_taches_HG.Enums;
using Microsoft.AspNetCore.Identity;
namespace PFE_Gestion_des_taches_HG.Models;


public class ApplicationUser : IdentityUser
{
    public Role Role { get; set; }
}
