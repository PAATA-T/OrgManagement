using Microsoft.EntityFrameworkCore;
using OrgManagement.DataServices.Data;
using OrgManagement.Entities.Models;

namespace OrgManagement.DataServices.Repositories.Implementation;

public class OrganizationRepository : IOrganizationRepository
{
    private readonly AppDbContext _context;
    private IOrganizationRepository _organizationRepositoryImplementation;

    public OrganizationRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Organization organization)
    {
        await _context.Organizations.AddAsync(organization);
        await _context.SaveChangesAsync();
    }

    public Task UpdateAsync(Organization organization)
    {
        return _organizationRepositoryImplementation.UpdateAsync(organization);
    }

    public Task DeleteAsync(Guid id)
    {
        return _organizationRepositoryImplementation.DeleteAsync(id);
    }

    public async Task<IEnumerable<Organization>> GetAllAsync()
    {
        return await _context.Organizations.ToListAsync();
    }

    public async Task<Organization> GetByIdAsync(Guid id)
    {
        return await _context.Organizations.FindAsync(id);
    }

    public async Task<IEnumerable<Organization>> GetOrganizationTreeAsync()
    {
        return await _context.Organizations
            .Include(o => o.SubOrganizations)
            .ToListAsync();
    }

    public async Task<IEnumerable<Organization>> GetTopLevelOrganizationsAsync()
    {
        return await _context.Organizations
            .Where(o => o.ParentOrganizationId == null)
            .ToListAsync();
    }
}
