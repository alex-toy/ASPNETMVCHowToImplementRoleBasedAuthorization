using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SalesCRMApp.Entities;

namespace SalesCRMApp.Repo;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<SalesLead> SalesLeads { get; set; }
    public DbSet<ApplicationUser> ApplicationUsers { get; set; }
}
