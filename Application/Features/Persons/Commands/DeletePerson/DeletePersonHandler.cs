using Application.Common.Exceptions;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Persons.Commands.DeletePerson;

internal class DeletePersonHandler(IAppDbContext dbContext) : IRequestHandler<DeletePersonCommand, Unit>
{
    public async Task<Unit> Handle(DeletePersonCommand request, CancellationToken cancellationToken)
    {
        var person = await dbContext
            .Persons
            .Where(p => p.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken) 
                     ?? throw new NotFoundException(nameof(Person), request.Id);
        
        dbContext
            .Persons
            .Remove(person);

        await dbContext.SaveChangesAsync(cancellationToken);

        return await Unit.Task;
    }
}