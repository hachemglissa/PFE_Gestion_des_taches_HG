using PFE_GestionDesTaches2024.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace PFE_GestionDesTaches2024.Data;


public class ApplicationDbContext : IdentityUserContext<ApplicationUser>
{
    public DbSet<Page> Pages => Set<Page>();

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}