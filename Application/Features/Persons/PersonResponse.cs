using Domain.Entities;

namespace Application.Features.Persons;

public sealed record PersonResponse
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Surname { get; private set; }
    public string Email { get; private set; }
    public string City { get; private set; }
    public string PostalCode { get; private set; }
    public DateOnly DateOfBirth { get; private set; }

    public PersonResponse ToPersonResponse(Person person)
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