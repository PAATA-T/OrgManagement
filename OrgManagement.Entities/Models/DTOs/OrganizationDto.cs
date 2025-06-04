namespace OrgManagement.Entities.Models;

public class OrganizationDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }

    public List<OrganizationDto> SubOrganizations { get; set; } = new List<OrganizationDto>();
}