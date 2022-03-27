using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AnimeCalendar.Data;

public class ApplicationDbContext : IdentityDbContext<User, IdentityRole, string>
{
    public DbSet<Anime> Animes { get; set; } = null!;
    public DbSet<Collection> Collections { get; set; } = null!;
    public DbSet<InCollection> InCollections { get; set; } = null!;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<InCollection>()
            .HasOne(a => a.Anime)
            .WithMany(a => a.Collections)
            .IsRequired();

        builder.Entity<InCollection>()
            .HasOne(c => c.Collection)
            .WithMany(i => i.Elements)
            .IsRequired();

        builder.Entity<Collection>()
            .HasOne(u => u.User)
            .WithMany(u => u.Collections)
            .IsRequired();

        builder.Entity<IdentityRole>().HasData(new IdentityRole()
        {
            Name = "Administator",
            NormalizedName = ConstRoles.AdminRole
        }, new IdentityRole()
        {
            Name = "Anime Editor",
            NormalizedName = ConstRoles.AnimeEditor
        });
    }
}