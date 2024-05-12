using Contracts;
using Contracts.Person;
using FluentValidation;
using MediatR;

namespace Application.Features.Persons.Commands.CreatePerson;

public sealed record CreatePersonCommand(
    string Name,
    string Surname,
    string Email,
    string City,
    string PostalCode,
    DateOnly DateOfBirth
) : IRequest<PersonResponse>;
    
public sealed class CreatePersonValidator : AbstractValidator<CreatePersonCommand>
{
    public CreatePersonValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(50)
            .MinimumLength(3);

        RuleFor(p => p.Surname)
            .NotEmpty()
            .MaximumLength(50)
            .MinimumLength(3);

        RuleFor(p => p.Email)
            .NotEmpty()
            .EmailAddress();
        
        RuleFor(p => p.City)
            .NotEmpty()
            .MaximumLength(50)
            .MinimumLength(3);
        
        RuleFor(p => p.PostalCode)
            .NotEmpty()
            .MaximumLength(10);

        RuleFor(p => p.DateOfBirth)
            .NotEmpty();
    }
}
   