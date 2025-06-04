using OrgManagement.Entities.Enums;

namespace OrgManagement.Entities.Models;

public class Person
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PersonalNumber { get; set; }
    public DateTime BirthDate { get; set; }
    public string? PhotoUrl { get; set; }
    public ForeignLanguage ForeignLanguage { get; set; }
    
    public Guid OrganizationId { get; set; }
    
    public Organization Organization { get; set; }
}