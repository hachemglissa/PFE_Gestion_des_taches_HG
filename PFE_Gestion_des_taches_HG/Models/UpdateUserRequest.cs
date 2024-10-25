using PFE_Gestion_des_taches_HG.Enums;

namespace PFE_Gestion_des_taches_HG.Models
{
        public class UpdateUserRequest
        {
            public string? UserName { get; set; }
            public string? Email { get; set; }
            public Role Role { get; set; }
        }
}
