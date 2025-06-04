using System.ComponentModel.DataAnnotations;

namespace OrgManagement.Entities.Models;

public class OrganizationCreateDto
{
    [Required]
    [StringLength(100, MinimumLength = 2)]
    [RegularExpression(@"^[ა-ჰA-Za-z\s]+$", ErrorMessage = "დასახელება უნდა შეიცავდეს მხოლოდ ქართულ ან ლათინურ ასოებს")]
    public string Name { get; set; }

    [Required]
    [StringLength(200, MinimumLength = 2)]
    [RegularExpression(@"^[ა-ჰA-Za-z\s]+$", ErrorMessage = "მისამართი უნდა შეიცავდეს მხოლოდ ქართულ ან ლათინურ ასოებს")]
    public string Address { get; set; }

    public Guid? ParentOrganizationId { get; set; }
}