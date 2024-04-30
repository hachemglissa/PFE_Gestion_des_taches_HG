using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using PFE_GestionDesTaches2024.Models;
using PFE_GestionDesTaches2024.Data;

namespace PFE_GestionDesTaches2024.Controllers;

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

    [Authorize(Roles = "Admin")]
    [HttpPost("new")]
    public async Task<ActionResult<Projet>> CreateProjet(ProjetDto projetDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var projet = new Projet
        {
            Name = projetDto.Name,
            Prescription = projetDto.Prescription,
            StartDate = projetDto.StartDate,
            EndDate = projetDto.EndDate
        };

        _dbContext.Projets.Add(projet);
        await _dbContext.SaveChangesAsync();

        return CreatedAtAction(nameof(GetProjet), new { id = projet.Id }, projet);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProjetDto>> GetProjet(int id)
    {
        var projet = await _dbContext.Projets.FindAsync(id);

        if (projet is null)
        {
            return NotFound();
        }

        var projetDto = new ProjetDto
        {
            Id = projet.Id,
            Name = projet.Name,
            Prescription = projet.Prescription,
            StartDate = projet.StartDate,
            EndDate = projet.EndDate
        };

        return projetDto;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProjetDto>>> GetAllProjets()
    {
        var projets = await _dbContext.Projets.ToListAsync();

        var projetsDto = projets.Select(projet => new ProjetDto
        {
            Id = projet.Id,
            Name = projet.Name,
            Prescription = projet.Prescription,
            StartDate = projet.StartDate,
            EndDate = projet.EndDate
        });

        return Ok(projetsDto);
    }


}
