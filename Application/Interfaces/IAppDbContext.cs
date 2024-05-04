using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces;

public interface IAppDbContext
{
    DbSet<Person> Persons { get; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}