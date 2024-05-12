using System.ComponentModel.DataAnnotations;

namespace Contracts.Person;

public sealed record PersonModel
{
    public Guid Id { get; set; }

    [Required]
    [MaxLength(50)]
    [MinLength(3)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    [MinLength(3)]
    public string Surname { get; set; } = string.Empty;

    [Required] 
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    [MinLength(3)]
    public string City { get; set; } = string.Empty;

    [Required]
    [MaxLength(10)]
    [MinLength(3)]
    public string PostalCode { get; set; } = string.Empty;

    [Required]
    public DateOnly DateOfBirth { get; set; }
}