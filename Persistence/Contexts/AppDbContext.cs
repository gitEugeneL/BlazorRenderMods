using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Contexts;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options), IAppDbContext
{
    public DbSet<Person> Persons { get; init; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var persons = modelBuilder.Entity<Person>();

        persons.HasIndex(p => p.Email)
            .IsUnique();

        persons.Property(p => p.Email)
            .IsRequired();

        persons.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(50);

        persons.Property(p => p.Surname)
            .IsRequired()
            .HasMaxLength(50);

        persons.Property(p => p.City)
            .HasMaxLength(50);
    
        persons.Property(p => p.PostalCode)
            .HasMaxLength(10);
        
        persons.HasData(
            new Person
            {
                Id = Guid.NewGuid(),
                Name = "Eugene",
                Surname = "Smith",
                Email = "e.smith@test.com",
                City = "Barcelona",
                PostalCode = "02495F",
                DateOfBirth = new DateOnly(2020, 12, 31)
            }
        );
    }
}