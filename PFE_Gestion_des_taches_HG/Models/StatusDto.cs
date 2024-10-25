using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PFE_Gestion_des_taches_HG.Models
{
    public class StatusDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int ProjectId { get; set; }

    }
}
