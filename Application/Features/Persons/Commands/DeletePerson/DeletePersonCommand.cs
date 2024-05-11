using MediatR;

namespace Application.Features.Persons.Commands.DeletePerson;

public sealed record DeletePersonCommand(Guid Id) : IRequest<Unit>;
