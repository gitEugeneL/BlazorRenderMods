using Domain.Entities;

namespace Application.Interfaces;

public interface IPersonRepository
{
    Task<List<Person?>> GetAllAsync();
    
    Task<Person?> GetOneByIdAsync(Guid id);
    
    Task CreateAsync(Person? person);
    
    Task UpdateAsync(Person? person);
}