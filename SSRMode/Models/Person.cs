using Application.Features.Persons;

namespace SSRMode.Models;

public sealed record Person
{
    public Guid? Id { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? Email { get; set; }
    public string? City { get; set; }
    public string? PostalCode { get; set; }
    public DateOnly? DateOfBirth { get; set; }
    
    public Person ToPersonResponse(PersonResponse person)
    {
        Id = person.Id;
        Name = person.Name;
        Surname = person.Surname;
        Email = person.Email;
        City = person.City;
        PostalCode = person.PostalCode;
        DateOfBirth = person.DateOfBirth;
        
        return this;
    }
}