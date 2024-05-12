using Application.Common.Exceptions;
using Application.Interfaces;
using Contracts;
using Contracts.Person;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Persons.Queries.GetPersonById;

internal sealed class GetPersonByIdHandler(IAppDbContext dbContext) : IRequestHandler<GetPersonByIdQuery, PersonResponse>
{
    public async Task<PersonResponse> Handle(GetPersonByIdQuery request, CancellationToken cancellationToken)
    {
        var person = await dbContext
            .Persons
            .AsNoTracking()
            .Where(p => p.Id == request.Id)
            .Select(p => new PersonResponse
            {
                Id = p.Id,
                Name = p.Name,
                Surname = p.Surname,
                City = p.City,
                PostalCode = p.PostalCode,
                DateOfBirth = p.DateOfBirth
            })
            .SingleOrDefaultAsync(cancellationToken);

        if (person is null)
            throw new NotFoundException(nameof(Person), request.Id);
        
        return person;
    }
}