using Application.Common.Exceptions;
using Application.Features.Persons.Commands.CreatePerson;
using Application.Features.Persons.Commands.UpdatePerson;
using Application.Features.Persons.Queries.GetAllPersons;
using Application.Features.Persons.Queries.GetPersonById;
using Carter;
using Carter.ModelBinding;
using Contracts.Person;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace WebAssemblyMode.Api.Endpoints;

public class PersonEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/person")
            .WithTags(nameof(Person));

        group.MapGet("", GetAll)
            .Produces<List<PersonResponse>>();

        group.MapGet("{id:guid}", GetOne)
            .Produces<PersonResponse>()
            .Produces(StatusCodes.Status404NotFound);

        group.MapPost("", Create)
            .Produces<PersonResponse>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status409Conflict)
            .Produces(StatusCodes.Status400BadRequest);

        group.MapPut("", Update)
            .Produces<PersonResponse>()
            .Produces(StatusCodes.Status404NotFound);
    }

    private static async Task<IResult> GetAll(ISender sender)
    {
        var result = await sender.Send(new GetAllPersonsQuery());
        return TypedResults.Ok(result);
    }

    private static async Task<Results<Ok<PersonResponse>, NotFound<string>>> GetOne(Guid id, ISender sender)
    {
        try
        {
            var result = await sender.Send(new GetPersonByIdQuery(id));
            return TypedResults.Ok(result);
        }
        catch (NotFoundException e)
        {
            return TypedResults.NotFound(e.Message);
        }
    }

    private static async Task<Results<Created<PersonResponse>, ValidationProblem, Conflict<string>>> Create(
        [FromBody] PersonModel model, 
        IValidator<CreatePersonCommand> validator,
        ISender sender)
    {
        var command = new CreatePersonCommand(
            Name: model.Name,
            Surname: model.Surname, 
            Email: model.Email,
            City: model.City,
            PostalCode: model.PostalCode,
            DateOfBirth: model.DateOfBirth
        );

        var validationResult = await validator.ValidateAsync(command);
        if (!validationResult.IsValid)
            return TypedResults.ValidationProblem(validationResult.GetValidationProblems());
        
        try
        {
            var result = await sender.Send(command);
            return TypedResults.Created(result.Id.ToString(), result);
        }
        catch (AlreadyExistException e)
        {
            return TypedResults.Conflict(e.Message);
        }
    }

    private static async Task<Results<Ok<PersonResponse>, NotFound<string>, ValidationProblem>> Update(
        [FromBody] PersonModel model,
        IValidator<UpdatePersonCommand> validator,
        ISender sender)
    {
        var command = new UpdatePersonCommand(
            Id: model.Id,
            Name: model.Name,
            Surname: model.Surname,
            City: model.City,
            PostalCode: model.PostalCode,
            DateOfBirth: model.DateOfBirth
        );
        
        var validationResult = await validator.ValidateAsync(command);
        if (!validationResult.IsValid)
            return TypedResults.ValidationProblem(validationResult.GetValidationProblems());

        try
        {
            var result = await sender.Send(command);
            return TypedResults.Ok(result);
        }
        catch (NotFoundException e)
        {
            return TypedResults.NotFound(e.Message);
        }
    }
}