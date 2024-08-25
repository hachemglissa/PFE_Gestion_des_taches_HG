using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using PFE_Gestion_des_taches_HG.Models;
using PFE_Gestion_des_taches_HG.Services.Abstraction;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PFE_Gestion_des_taches_HG.Enums;

namespace PFE_Gestion_des_taches_HG.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/[controller]")]
    public class TachesController : ControllerBase
    {
        private readonly ITacheService _tacheService;
        private readonly ILogger<TachesController> _logger;

        public TachesController(ITacheService tacheService, ILogger<TachesController> logger)
        {
            _tacheService = tacheService;
            _logger = logger;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("new")]
        [EnableCors("AllowAll")]
        public async Task<ActionResult<TacheDto>> CreateTache([FromBody] TacheDto tacheDto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for the CreateTache request.");
                return BadRequest(ModelState);
            }

            try
            {
                var tache = new Tache
                {
                    Title = tacheDto.Title,
                    Description = tacheDto.Description,
                    ProjectId = tacheDto.ProjectId,
                    Status = tacheDto.Status,
                    Priority = tacheDto.Priority,
                    StartDate = tacheDto.StartDate,
                    DueDate = tacheDto.DueDate
                };

                await _tacheService.CreateTache(tache);

                var createdTacheDto = new TacheDto
                {
                    Id = tache.Id,
                    Title = tache.Title,
                    Description = tache.Description,
                    ProjectId = tache.ProjectId,
                    Status = tache.Status,
                    Priority = tache.Priority,
                    StartDate = tache.StartDate,
                    DueDate = tache.DueDate
                };

                _logger.LogInformation($"Task created successfully with ID {tache.Id}.");
                return CreatedAtAction(nameof(GetTache), new { id = tache.Id }, createdTacheDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a new task.");
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<TacheDto>> GetTache(int id)
        {
            try
            {
                var tache = await _tacheService.GetTacheById(id);

                if (tache == null)
                {
                    _logger.LogWarning($"Task with ID {id} not found.");
                    return NotFound();
                }

                var tacheDto = new TacheDto
                {
                    Id = tache.Id,
                    Title = tache.Title,
                    Description = tache.Description,
                    ProjectId = tache.ProjectId,
                    Status = tache.Status,
                    Priority = tache.Priority,
                    StartDate = tache.StartDate,
                    DueDate = tache.DueDate
                };

                return Ok(tacheDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while retrieving the task with ID {id}.");
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpGet("byproject/{idProjet:int}")]
        public async Task<ActionResult<IEnumerable<TacheDto>>> GetAllTachesByProjet(int idProjet)
        {
            try
            {
                // Assurez-vous que l'appel à GetTachesByProjet utilise 'idProjet' correctement
                var taches = await _tacheService.GetTachesByProjet(idProjet);

                if (taches == null || !taches.Any())
                {
                    _logger.LogWarning($"No tasks found for project with ID {idProjet}.");
                    return NotFound("No tasks found for the project.");
                }

                // Mapping des entités Tache en TacheDto
                var tachesDto = taches.Select(tache => new TacheDto
                {
                    Id = tache.Id,
                    Title = tache.Title,
                    Description = tache.Description,
                    ProjectId = tache.ProjectId,  // Assurez-vous d'utiliser 'ProjectId'
                    Status = tache.Status,
                    Priority = tache.Priority,
                    StartDate = tache.StartDate,
                    DueDate = tache.DueDate
                }).ToList();

                return Ok(tachesDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while retrieving tasks for project with ID {idProjet}.");
                return StatusCode(500, "Internal server error.");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateTache(int id, [FromBody] TacheDto tacheDto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for the UpdateTache request.");
                return BadRequest(ModelState);
            }

            try
            {
                var tache = await _tacheService.GetTacheById(id);

                if (tache == null)
                {
                    _logger.LogWarning($"Task with ID {id} not found.");
                    return NotFound();
                }

                tache.Title = tacheDto.Title;
                tache.Description = tacheDto.Description;
                tache.ProjectId = tacheDto.ProjectId;
                tache.Status = tacheDto.Status;
                tache.Priority = tacheDto.Priority;
                tache.StartDate = tacheDto.StartDate;
                tache.DueDate = tacheDto.DueDate;

                await _tacheService.UpdateTache(tache);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while updating the task with ID {id}.");
                return StatusCode(500, "Internal server error.");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteTache(int id)
        {
            try
            {
                var tache = await _tacheService.GetTacheById(id);

                if (tache == null)
                {
                    _logger.LogWarning($"Task with ID {id} not found.");
                    return NotFound();
                }

                await _tacheService.DeleteTache(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while deleting the task with ID {id}.");
                return StatusCode(500, "Internal server error.");
            }
        }
    }
}