using System.ComponentModel.DataAnnotations;
using PFE_Gestion_des_taches_HG.Enums;
namespace PFE_Gestion_des_taches_HG.Models;
public class RegistrationRequest
{
    [Required]
    public string? Email { get; set; }

    [Required]
    public string? Username { get; set; }

    [Required]
    public string? Password { get; set; }

    public Role Role { get; set; } = Role.User;
}
