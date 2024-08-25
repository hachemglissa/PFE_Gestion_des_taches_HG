using PFE_Gestion_des_taches_HG.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace PFE_Gestion_des_taches_HG.Data;


public class ApplicationDbContext : IdentityUserContext<ApplicationUser>
{
    
    public DbSet<Projet> Projets => Set<Projet>();

    public DbSet<Tache> Taches => Set<Tache>();


    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Tache>()
            .HasOne(t => t.Project)
            .WithMany(p => p.Taches)
            .HasForeignKey(t => t.ProjectId);
    }
}