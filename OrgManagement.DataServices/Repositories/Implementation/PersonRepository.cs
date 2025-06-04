using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrgManagement.DataServices.Data;
using OrgManagement.Entities.Models;

namespace OrgManagement.DataServices.Repositories.Implementation;

public class PersonRepository : IPersonRepository
{
    private readonly AppDbContext _context;

    public PersonRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Person person)
    {
        await _context.Persons.AddAsync(person);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var person = await _context.Persons.FindAsync(id);
        if (person != null)
        {
            _context.Persons.Remove(person);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<Person> GetByIdAsync(Guid id)
    {
        return await _context.Persons.FindAsync(id);
    }

    public async Task<(IEnumerable<Person> People, int TotalCount)> GetPersonsAsync(Guid? organizationId, int page, int pageSize)
    {
        var query = _context.Persons.Include(p => p.Organization).AsQueryable();

        if (organizationId.HasValue)
            query = query.Where(p => p.OrganizationId == organizationId.Value);

        var totalCount = await query.CountAsync();

        var people = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (people, totalCount);
    }

    public async Task UpdateAsync(Person person)
    {
        _context.Persons.Update(person);
        await _context.SaveChangesAsync();
    }
}