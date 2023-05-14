namespace PharmaFlow.AdministrationService.Data.Repositories;

internal class PharmacyRepository : IPharmacyRepository
{
    private readonly AdministrationServiceDbContext _db;

    public PharmacyRepository(AdministrationServiceDbContext db)
    {
        _db = db;
    }

    public async Task<Guid> AddPharmacyAsync(CreatePharmacyViewModel request, CancellationToken cancellationToken)
    {
        PharmacyPersistence pharmacy = new()
        {
            Name = request.Name,
        };

        _db.Pharmacies.Add(pharmacy);
        await _db.SaveChangesAsync(cancellationToken);

        return pharmacy.ID;
    }

    public async Task UpdatePharmacyAsync(Guid pharmacyID, UpdatePharmacyViewModel request, CancellationToken cancellationToken)
    {
        PharmacyPersistence pharmacy = await _db.Pharmacies.FirstAsync(p => p.ID == pharmacyID, cancellationToken);

        pharmacy.Name = request.Name;

        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<PharmacyPersistence>> GetPharmacyListAsync(CancellationToken cancellationToken)
    {
        List<PharmacyPersistence> pharmacyList = await _db.Pharmacies.ToListAsync(cancellationToken);

        return pharmacyList;
    }

    public async Task<PharmacyPersistence> GetPharmacyAsync(Guid pharmacyID, CancellationToken cancellationToken)
    {
        PharmacyPersistence pharmacy = await _db.Pharmacies.FirstAsync(p => p.ID == pharmacyID, cancellationToken);

        return pharmacy;
    }

    public async Task RemovePharmacyAsync(Guid pharmacyID, CancellationToken cancellationToken)
    {
        PharmacyPersistence pharmacy = await _db.Pharmacies.FirstAsync(p => p.ID == pharmacyID, cancellationToken);

        pharmacy.State = PharmacyStatePersistence.Removed;

        await _db.SaveChangesAsync(cancellationToken);
    }
}
