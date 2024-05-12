using Application.Common.Exceptions;
using Application.Interfaces;
using Contracts;
using Contracts.Person;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Persons.Commands.UpdatePerson;

internal sealed class UpdatePersonHandler(IAppDbContext dbContext) : IRequestHandler<UpdatePersonCommand, PersonResponse>
{
    public async Task<PersonResponse> Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
    {
        var person = await dbContext
            .Persons
            .SingleOrDefaultAsync(p => p.Id == request.Id, cancellationToken) 
                     ?? throw new NotFoundException(nameof(Person), request.Id);

        person.Name = request.Name ?? person.Name;
        person.Surname = request.Surname ?? person.Surname;
        person.City = request.City ?? person.City;
        person.PostalCode = request.PostalCode ?? person.PostalCode;
        person.DateOfBirth = request.DateOfBirth ?? person.DateOfBirth;
        
        dbContext
            .Persons
            .Update(person);
        await dbContext
            .SaveChangesAsync(cancellationToken);

        return new PersonResponse
        {
            Id = person.Id,
            Name = person.Name,
            Surname = person.Surname,
            City = person.City,
            PostalCode = person.PostalCode,
            DateOfBirth = person.DateOfBirth
        };
    }
}