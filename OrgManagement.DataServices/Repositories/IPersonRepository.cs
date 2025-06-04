using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OrgManagement.Entities.Models;

namespace OrgManagement.DataServices.Repositories;

public interface IPersonRepository
{
    Task<Person> GetByIdAsync(Guid id);
    Task<(IEnumerable<Person> People, int TotalCount)> GetPersonsAsync(Guid? organizationId, int page, int pageSize);
    
    Task AddAsync(Person person);
    Task UpdateAsync(Person person);
    Task DeleteAsync(Guid id);
}