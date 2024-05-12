using Application.Interfaces;
using Contracts;
using Contracts.Person;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Persons.Queries.GetAllPersons;

internal sealed class GetAllPersonsHandler(IAppDbContext dbContext) 
    : IRequestHandler<GetAllPersonsQuery, List<PersonResponse>>
{
    public async Task<List<PersonResponse>> Handle(GetAllPersonsQuery request, CancellationToken cancellationToken)
    {
        return await dbContext
            .Persons
            .AsNoTracking()
            .Select(p => new PersonResponse 
            {
                Id = p.Id,
                Name = p.Name,
                Surname = p.Surname,
                Email = p.Email,
                City = p.City,
                PostalCode = p.PostalCode,
                DateOfBirth = p.DateOfBirth 
            })
            .ToListAsync(cancellationToken);
    }
}