
using BuildWeek2.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BuildWeek2.Data;

public class AppDbContext : IdentityDbContext<ApplicationUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    public DbSet<Animale> Animali { get; set; }
    public DbSet<RicoveroAnimale> RicoveriAnimali { get; set; }
    public DbSet<RicoveroAnimaleSmarrito> RicoveriAnimaliSmarriti { get; set; }
    public DbSet<Visita> Visite { get; set; }
    public DbSet<Prodotti> Prodotti { get; set; }
    public DbSet<Vendita> Vendite { get; set; }
    public DbSet<Fornitore> Fornitori { get; set;

    }
}