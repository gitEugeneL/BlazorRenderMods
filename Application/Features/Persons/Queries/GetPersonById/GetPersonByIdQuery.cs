using MediatR;

namespace Application.Features.Persons.Queries.GetPersonById;

public sealed record GetPersonByIdQuery(Guid Id) : IRequest<PersonResponse>;
