using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PFE_GestionDesTaches2024.Models;

public class Projet
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required(ErrorMessage = "The name is required.")]
    [StringLength(maximumLength: 100, ErrorMessage = "The name must not exceed 100 characters.")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "The prescription is required.")]
    [StringLength(maximumLength: int.MaxValue, ErrorMessage = "The prescription must not exceed {1} characters.")]
    public string? Prescription { get; set; }

    [Required(ErrorMessage = "The start date is required.")]
    public DateTime? StartDate { get; set; }

    [Required(ErrorMessage = "The end date is required.")]
    public DateTime? EndDate { get; set; }
}
