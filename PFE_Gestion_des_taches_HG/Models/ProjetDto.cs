using PFE_Gestion_des_taches_HG.Enums;
using System.ComponentModel.DataAnnotations;

namespace PFE_Gestion_des_taches_HG.Models;

public class ProjetDto
{
    public int Id { get; set; }

    [Required(ErrorMessage = "The name is required.")]
    [StringLength(maximumLength: 100, ErrorMessage = "The name must not exceed 100 characters.")]
    public string? Name { get; set; } 

    [Required(ErrorMessage = "The description is required.")]
    [StringLength(maximumLength: int.MaxValue, ErrorMessage = "The description must not exceed {1} characters.")]
    public string? Description { get; set; }

    [Required(ErrorMessage = "The start date is required.")]
    public DateTime StartDate { get; set; }

    [Required(ErrorMessage = "The end date is required.")]
    public DateTime EndDate { get; set; }

    [Required(ErrorMessage = "The status is required.")]
    public ProjetStatus Status { get; set; } 
}
