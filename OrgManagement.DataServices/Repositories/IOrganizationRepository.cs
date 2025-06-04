using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OrgManagement.Entities.Models;

namespace OrgManagement.DataServices.Repositories;

public interface IOrganizationRepository
{
    Task<Organization> GetByIdAsync(Guid id);
    Task<IEnumerable<Organization>> GetAllAsync();
    
    Task<IEnumerable<Organization>> GetOrganizationTreeAsync();
    
    Task<IEnumerable<Organization>> GetTopLevelOrganizationsAsync();

    Task AddAsync(Organization organization);
    Task UpdateAsync(Organization organization);
    Task DeleteAsync(Guid id);
}
