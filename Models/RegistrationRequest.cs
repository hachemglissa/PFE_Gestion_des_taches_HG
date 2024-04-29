using System.ComponentModel.DataAnnotations;
using PFE_GestionDesTaches2024.Enums;

namespace PFE_GestionDesTaches2024.Models;

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