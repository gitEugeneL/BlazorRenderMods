namespace Contracts.Person;

public sealed record PersonResponse
{
    public Guid Id { get; init; }
 
    public string Name { get; init; } = string.Empty;
    
    public string Surname { get; init; } = string.Empty;
    
    public string Email { get; init; } = string.Empty;
    
    public string City { get; init; } = string.Empty;
    
    public string PostalCode { get; init; } = string.Empty;
    
    public DateOnly DateOfBirth { get; init; }
}