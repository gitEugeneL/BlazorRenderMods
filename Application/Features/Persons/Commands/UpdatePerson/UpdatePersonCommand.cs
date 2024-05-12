using Contracts.Person;
using FluentValidation;
using MediatR;

namespace Application.Features.Persons.Commands.UpdatePerson;

public sealed record UpdatePersonCommand(
    Guid Id,
    string? Name,
    string? Surname,
    string? City,
    string? PostalCode,
    DateOnly? DateOfBirth
) : IRequest<PersonResponse>;

public sealed class UpdatePersonValidator : AbstractValidator<UpdatePersonCommand>
{
    public UpdatePersonValidator()
    {
        RuleFor(p => p.Name)
            .MaximumLength(50)
            .MinimumLength(3);

        RuleFor(p => p.Surname)
            .MaximumLength(50)
            .MinimumLength(3);
        
        RuleFor(p => p.City)
            .MaximumLength(50)
            .MinimumLength(3);
        
        RuleFor(p => p.PostalCode)
            .MaximumLength(10);

        RuleFor(p => p.DateOfBirth)
            .NotEmpty();
    }
}