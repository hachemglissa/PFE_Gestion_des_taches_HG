using PFE_Gestion_des_taches_HG.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PFE_Gestion_des_taches_HG.Models;

public class Projet
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required(ErrorMessage = "The name is required.")]
    [StringLength(100, ErrorMessage = "The name must not exceed 100 characters.")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "The description is required.")]
    [StringLength(int.MaxValue, ErrorMessage = "The description must not exceed {1} characters.")]
    public string? Description { get; set; }

    [Required(ErrorMessage = "The start date is required.")]
    public DateTime StartDate { get; set; }

    [Required(ErrorMessage = "The end date is required.")]
    public DateTime EndDate { get; set; }

    [Required(ErrorMessage = "The status is required.")]
    public ProjetStatus Status { get; set; } 

    public virtual ICollection<Tache>? Taches { get; set; }
}
