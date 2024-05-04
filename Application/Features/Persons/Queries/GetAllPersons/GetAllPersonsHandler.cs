using Application.Interfaces;
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
            .Select(p => new PersonResponse()
                .ToPersonResponse(p))
            .ToListAsync(cancellationToken);
    }
}