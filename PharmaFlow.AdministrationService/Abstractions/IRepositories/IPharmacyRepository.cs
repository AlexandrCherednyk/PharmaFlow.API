namespace PharmaFlow.AdministrationService.Abstractions.IRepositories;

public interface IPharmacyRepository
{
    Task<Guid> AddPharmacyAsync(CreatePharmacyViewModel request, CancellationToken cancellationToken);

    Task UpdatePharmacyAsync(Guid pharmacyID, UpdatePharmacyViewModel request, CancellationToken cancellationToken);

    Task<List<PharmacyPersistence>> GetPharmacyListAsync(CancellationToken cancellationToken);

    Task<PharmacyPersistence> GetPharmacyAsync(Guid pharmacyID, CancellationToken cancellationToken);

    Task RemovePharmacyAsync(Guid pharmacyID, CancellationToken cancellationToken);
}
