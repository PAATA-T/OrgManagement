namespace OrgManagement.Entities.Models;

public class Organization
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    
    public Guid? ParentOrganizationId { get; set; }
    
    public Organization ParentOrganization { get; set; }
    
    public ICollection<Organization> SubOrganizations { get; set; } = new List<Organization>();
    
    public ICollection<Person> Employees { get; set; } = new List<Person>();
}