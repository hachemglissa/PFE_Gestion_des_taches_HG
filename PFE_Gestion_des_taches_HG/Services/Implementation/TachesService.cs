using Microsoft.EntityFrameworkCore;
using PFE_Gestion_des_taches_HG.Data;
using PFE_Gestion_des_taches_HG.Models;
using PFE_Gestion_des_taches_HG.Services.Abstraction;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PFE_Gestion_des_taches_HG.Services.Implementation
{
    public class TachesService : ITacheService
    {
        private readonly ApplicationDbContext _context;

        public TachesService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Tache> GetTacheById(int id)
        {
            return await _context.Taches.FindAsync(id);
        }

        public async Task<IEnumerable<Tache>> GetTachesByProjet(int projectId)
        {
            return await _context.Taches
                                 .Where(t => t.ProjectId == projectId)  // Assurez-vous d'utiliser 'ProjectId'
                                 .Include(t => t.Project)
                                 .ToListAsync();
        }


        public async Task<IEnumerable<Tache>> GetAllTaches()
        {
            return await _context.Taches.ToListAsync();
        }

        public async Task CreateTache(Tache tache)
        {
            await _context.Taches.AddAsync(tache);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTache(Tache tache)
        {
            _context.Taches.Update(tache);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTache(int id)
        {
            var tache = await GetTacheById(id);
            if (tache != null)
            {
                _context.Taches.Remove(tache);
                await _context.SaveChangesAsync();
            }
        }
    }
}
