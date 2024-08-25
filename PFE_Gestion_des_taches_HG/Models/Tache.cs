using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PFE_Gestion_des_taches_HG.Enums;

namespace PFE_Gestion_des_taches_HG.Models
{
    public class Tache
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "The title is required.")]
        [StringLength(100, ErrorMessage = "The title must not exceed 100 characters.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "The description is required.")]
        [StringLength(1000, ErrorMessage = "The description must not exceed 1000 characters.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "The status is required.")]
        public Status Status { get; set; }

        [Required(ErrorMessage = "The priority is required.")]
        public Priority Priority { get; set; }

        [Required(ErrorMessage = "The start date is required.")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "The due date is required.")]
        public DateTime DueDate { get; set; }

        [Required]
        public int ProjectId { get; set; }

        [ForeignKey("ProjectId")]
        public virtual Projet? Project { get; set; }
    }
}
