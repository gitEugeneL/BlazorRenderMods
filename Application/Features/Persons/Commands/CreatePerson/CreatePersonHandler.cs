using Application.Common.Exceptions;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Persons.Commands.CreatePerson;

public class CreatePersonHandler(IAppDbContext dbContext) : IRequestHandler<CreatePersonCommand, PersonResponse>
{
    public async Task<PersonResponse> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
    {
        if (await dbContext.Persons.AnyAsync(p => p.Email == request.Email, cancellationToken))
            throw new AlreadyExistException(nameof(Person), request.Email);
        
        var person = new Person
        {
            Name = request.Name,
            Surname = request.Surname,
            Email = request.Email,
            City = request.City,
            PostalCode = request.PostalCode,
            DateOfBirth = request.DateOfBirth
        };

        await dbContext
            .Persons
            .AddAsync(person, cancellationToken);

        return new PersonResponse().ToPersonResponse(person);
    }
}