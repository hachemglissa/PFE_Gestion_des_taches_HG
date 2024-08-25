using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using PFE_Gestion_des_taches_HG.Models;
using PFE_Gestion_des_taches_HG.Data;
using Microsoft.AspNetCore.Cors;

namespace PFE_Gestion_des_taches_HG.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/[controller]")]
    public class ProjetsController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public ProjetsController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [Authorize(Roles = "Admin,TeamLeader")]
        [HttpPost("new")]
        [EnableCors("AllowAll")] // Enable CORS for this controller
        public async Task<ActionResult<Projet>> CreateProjet([FromBody] ProjetDto projetDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Ensure StartDate and EndDate are not null
            if (projetDto.StartDate == null || projetDto.EndDate == null)
            {
                return BadRequest("StartDate and EndDate are required.");
            }

            var projet = new Projet
            {
                Name = projetDto.Name,
                Description = projetDto.Description,
                StartDate = projetDto.StartDate,
                EndDate = projetDto.EndDate,
                Status = projetDto.Status
            };

            _dbContext.Projets.Add(projet);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProjet), new { id = projet.Id }, projet);
        }

        [Authorize(Roles = "Admin,TeamLeader,Client,User,Developer")]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProjetDto>> GetProjet(int id)
        {
            var projet = await _dbContext.Projets.FindAsync(id);

            if (projet == null)
            {
                return NotFound();
            }

            var projetDto = new ProjetDto
            {
                Id = projet.Id,
                Name = projet.Name,
                Description = projet.Description,
                StartDate = projet.StartDate,
                EndDate = projet.EndDate,
                Status = projet.Status
            };

            return Ok(projetDto);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjetDto>>> GetAllProjets()
        {
            var projets = await _dbContext.Projets.ToListAsync();

            var projetsDto = projets.Select(projet => new ProjetDto
            {
                Id = projet.Id,
                Name = projet.Name,
                Description = projet.Description,
                StartDate = projet.StartDate,
                EndDate = projet.EndDate,
                Status = projet.Status
            });

            return Ok(projetsDto);
        }

        [Authorize(Roles = "Admin,TeamLeader")]
        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateProjet(int id, [FromBody] ProjetDto projetDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var projet = await _dbContext.Projets.FindAsync(id);

            if (projet == null)
            {
                return NotFound();
            }

            projet.Name = projetDto.Name;
            projet.Description = projetDto.Description;
            projet.StartDate = projetDto.StartDate;
            projet.EndDate = projetDto.EndDate;
            projet.Status = projetDto.Status; // Directly assign the enum

            _dbContext.Projets.Update(projet);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        [Authorize(Roles = "Admin,TeamLeader")]
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteProjet(int id)
        {
            var projet = await _dbContext.Projets.FindAsync(id);

            if (projet == null)
            {
                return NotFound();
            }

            _dbContext.Projets.Remove(projet);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
