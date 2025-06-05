using MediatR;
using OrgManagement.API.Commands;
using OrgManagement.DataServices.Repositories;

namespace OrgManagement.API.Handlers;

public class UploadPersonPhotoCommandHandler : IRequestHandler<UploadPersonPhotoCommand, string>
{
    private readonly IPersonRepository _personRepository;
    private readonly IHostEnvironment _environment;

    public UploadPersonPhotoCommandHandler(IPersonRepository personRepository, IHostEnvironment environment)
    {
        _personRepository = personRepository;
        _environment = environment;
    }

    public async Task<string> Handle(UploadPersonPhotoCommand request, CancellationToken cancellationToken)
    {
        var person = await _personRepository.GetByIdAsync(request.PersonId);
        if (person == null)
        {
            throw new Exception("Person not found");
        }

        var uploadsFolder = Path.Combine(_environment.ContentRootPath, "Uploads");
        if (!Directory.Exists(uploadsFolder))
        {
            Directory.CreateDirectory(uploadsFolder);
        }

        var uniqueFileName = $"{Guid.NewGuid()}{Path.GetExtension(request.PhotoFileName)}";
        var filePath = Path.Combine(uploadsFolder, uniqueFileName);

        await File.WriteAllBytesAsync(filePath, request.PhotoContent, cancellationToken);

        person.PhotoUrl = Path.Combine("Uploads", uniqueFileName);

        await _personRepository.UpdateAsync(person);

        return person.PhotoUrl;
    }
}