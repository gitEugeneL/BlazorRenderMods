using Domain.Common;

namespace Domain.Entities;

public sealed class Person : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string PostalCode { get; set; } = string.Empty;
    public DateOnly DateOfBirth { get; set; }
}