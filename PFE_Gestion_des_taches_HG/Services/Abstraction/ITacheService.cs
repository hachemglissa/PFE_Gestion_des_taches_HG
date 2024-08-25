using PFE_Gestion_des_taches_HG.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PFE_Gestion_des_taches_HG.Services.Abstraction
{
    public interface ITacheService
    {
        Task<Tache> GetTacheById(int id);
        Task<IEnumerable<Tache>> GetTachesByProjet(int projetId);
        Task<IEnumerable<Tache>> GetAllTaches();
        Task CreateTache(Tache tache);
        Task UpdateTache(Tache tache);
        Task DeleteTache(int id);
    }
}
