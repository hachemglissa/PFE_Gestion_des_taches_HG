using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PFE_Gestion_des_taches_HG.Data;
using PFE_Gestion_des_taches_HG.Models;

namespace PFE_Gestion_des_taches_HG.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/[controller]")]
    public class StatusController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public StatusController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [Authorize(Roles = "Admin,TeamLeader")]
        [HttpPost("new")]
        [EnableCors("AllowAll")] // Enable CORS for this controller
        public async Task<IActionResult> CreateStatus([FromBody] StatusDto statusDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Ensure ProjectId is passed in the DTO
            if (statusDto.ProjectId <= 0)
            {
                return BadRequest("Invalid ProjectId.");
            }

            var status = new Status
            {
                Title = statusDto.Title,
                ProjectId = statusDto.ProjectId // Set ProjectId from DTO
            };

            _dbContext.Statuses.Add(status);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetStatus), new { id = status.Id }, status);
        }

        [Authorize(Roles = "Admin,TeamLeader,User,Developer")]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetStatus(int id)
        {
            var status = await _dbContext.Statuses.FindAsync(id);

            if (status == null)
            {
                return NotFound();
            }

            var statusDto = new StatusDto
            {
                Id = status.Id,
                Title = status.Title,
                ProjectId = status.ProjectId // Include ProjectId in response if necessary
            };

            return Ok(statusDto);
        }

        [Authorize(Roles = "Admin,TeamLeader,User,Developer")]
        [HttpGet]
        public async Task<IActionResult> GetAllStatus()
        {
            var statuses = await _dbContext.Statuses.ToListAsync();

            var statusDtoList = statuses.Select(status => new StatusDto
            {
                Id = status.Id,
                Title = status.Title,
                ProjectId = status.ProjectId // Include ProjectId
            }).ToList();

            return Ok(statusDtoList);
        }

        [Authorize(Roles = "Admin,TeamLeader")]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] StatusDto statusDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var status = await _dbContext.Statuses.FindAsync(id);

            if (status == null)
            {
                return NotFound();
            }

            status.Title = statusDto.Title;
            status.ProjectId = statusDto.ProjectId; // Update ProjectId if necessary

            _dbContext.Statuses.Update(status);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        [Authorize(Roles = "Admin,TeamLeader")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteStatus(int id)
        {
            var status = await _dbContext.Statuses.FindAsync(id);

            if (status == null)
            {
                return NotFound();
            }

            _dbContext.Statuses.Remove(status);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        [Authorize(Roles = "Admin,TeamLeader,User,Developer")]
        [HttpGet("all/{projectId:int}")]
        public async Task<IActionResult> GetAllStatusWithTaches(int projectId)
        {
            // Filter statuses by ProjectId
            var statuses = await _dbContext.Statuses
                .Include(s => s.Taches)
                .Where(s => s.ProjectId == projectId) // Filter by ProjectId
                .ToListAsync();

            if (!statuses.Any())
            {
                return NotFound("No statuses found for this project.");
            }

            var result = new
            {
                lists = statuses.ToDictionary(status => $"list-{status.Id}", status => new
                {
                    id = $"list-{status.Id}",
                    title = status.Title,
                    cards = status.Taches.Select(t => $"card-{t.Id}").ToList()
                }),
                cards = statuses.SelectMany(s => s.Taches.Select(t => new
                {
                    id = $"card-{t.Id}",
                    list = $"list-{s.Id}",
                    title = t.Title
                })).ToDictionary(c => c.id, c => new { c.id, c.list, c.title }),
                columns = statuses.Select(status => $"list-{status.Id}").ToList()
            };

            return Ok(result);
        }
    }
}