using System.ComponentModel.DataAnnotations;
using OrgManagement.Entities.Enums;

namespace OrgManagement.Entities.Models;

public class PersonCreateDto
{
    [Required]
    [StringLength(20, MinimumLength = 2)]
    [RegularExpression(@"^[ა-ჰA-Za-z]+$", ErrorMessage = "სახელი უნდა შეიცავდეს მხოლოდ ქართულ ან ლათინურ ასოებს")]
    public string FirstName { get; set; }

    [Required]
    [StringLength(20, MinimumLength = 2)]
    [RegularExpression(@"^[ა-ჰA-Za-z]+$", ErrorMessage = "გვარი უნდა შეიცავდეს მხოლოდ ქართულ ან ლათინურ ასოებს")]
    public string LastName { get; set; }

    [Required]
    [RegularExpression(@"^\d{11}$", ErrorMessage = "პირადი ნომერი უნდა შეიცავდეს 11 ციფრს")]
    public string PersonalNumber { get; set; }

    [Required]
    [CustomValidation(typeof(PersonCreateDto), nameof(ValidateAge))]
    public DateTime BirthDate { get; set; }
    
    public string? PhotoUrl { get; set; }

    [Required]
    public ForeignLanguage ForeignLanguage { get; set; }

    [Required]
    public Guid OrganizationId { get; set; }

    public static ValidationResult ValidateAge(DateTime birthDate, ValidationContext context)
    {
        var age = DateTime.Today.Year - birthDate.Year;
        if (birthDate > DateTime.Today.AddYears(-age)) age--;
        return age >= 18 ? ValidationResult.Success : new ValidationResult("პიროვნების ასაკი უნდა იყოს 18 წლის მაინც.");
    }
    
}