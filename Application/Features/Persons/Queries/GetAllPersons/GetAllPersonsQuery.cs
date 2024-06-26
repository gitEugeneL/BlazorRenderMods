using MediatR;

namespace Application.Features.Persons.Queries.GetAllPersons;

public sealed record GetAllPersonsQuery : IRequest<List<PersonResponse>>;