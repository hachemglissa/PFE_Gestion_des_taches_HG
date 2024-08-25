using System.ComponentModel.DataAnnotations;
using PFE_Gestion_des_taches_HG.Enums;

namespace PFE_Gestion_des_taches_HG.Models
{
    public class TacheDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The task title is required.")]
        [StringLength(100, ErrorMessage = "The task title must not exceed 100 characters.")]
        public string Title { get; set; } // Removed nullable type as required attribute ensures it's not null

        [Required(ErrorMessage = "The task description is required.")]
        [StringLength(1000, ErrorMessage = "The task description must not exceed 1000 characters.")]
        public string Description { get; set; } // Removed nullable type as required attribute ensures it's not null

        [Required(ErrorMessage = "The task status is required.")]
        public Status Status { get; set; }

        [Required(ErrorMessage = "The task priority is required.")]
        public Priority Priority { get; set; }

        [Required(ErrorMessage = "The start date is required.")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "The due date is required.")]
        public DateTime DueDate { get; set; }

        [Required(ErrorMessage = "The project ID is required.")]
        public int ProjectId { get; set; }

    }
}
