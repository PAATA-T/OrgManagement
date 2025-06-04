using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrgManagement.DataServices.Data;
using OrgManagement.Entities.Models;

namespace OrgManagement.DataServices.Repositories.Implementation;

public class OrganizationRepository : IOrganizationRepository
{
    private readonly AppDbContext _context;

    public OrganizationRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Organization organization)
    {
        await _context.Organizations.AddAsync(organization);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var org = await _context.Organizations.FindAsync(id);
        if (org != null)
        {
            _context.Organizations.Remove(org);
            await _context.SaveChangesAsync();
        }
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
        // You might want to customize this for deep loading or use a DTO to project hierarchy properly.
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

    public async Task UpdateAsync(Organization organization)
    {
        _context.Organizations.Update(organization);
        await _context.SaveChangesAsync();
    }
}
