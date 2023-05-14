namespace PharmaFlow.AdministrationService.Data.Repositories;

internal class PharmacyMemberRepository : IPharmacyMemberRepository
{
    private readonly AdministrationServiceDbContext _db;

    public PharmacyMemberRepository(AdministrationServiceDbContext db)
    {
        _db = db;
    }

    public async Task<Guid> AddMemberToPharmacyAsync(Guid pharmacyID, CreatePharmacyMemberViewModel request, CancellationToken cancellationToken)
    {
        if(await _db.PharmacyMembers.AnyAsync(pm => pm.Email == request.Email))
        {
            throw new InvalidOperationException();
        }

        PharmacyMemberPersistence pharmacyMember = new()
        {
            PharmacyID = pharmacyID,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            Phone = request.Phone,
        };

        _db.PharmacyMembers.Add(pharmacyMember);
        await _db.SaveChangesAsync(cancellationToken);

        return pharmacyMember.ID;
    }

    public async Task UpdatePharmacyMemberAsync(Guid pharmacyID, Guid memberID, UpdatePharmacyMemberViewModel request, CancellationToken cancellationToken)
    {
        PharmacyMemberPersistence member = await _db.PharmacyMembers
            .Include(m => m.Pharmacy)
            .FirstAsync(m =>
                m.ID == memberID
                && m.PharmacyID == pharmacyID
                && m.Pharmacy!.State == PharmacyStatePersistence.Active,
                cancellationToken);

        member.FirstName = request.FirstName;
        member.LastName = request.LastName;
        member.Phone = request.Phone;

        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<PharmacyMemberViewModel>> GetPharmacyMemberListAsync(Guid pharmacyID, CancellationToken cancellationToken)
    {
        PharmacyPersistence pharmacy = await _db.Pharmacies
            .Include(p => p.Members)
            .FirstAsync(p => p.ID == pharmacyID, cancellationToken);

        return pharmacy.Members!.ToPharmacyMemberViewModelList(); ;
    }

    public async Task<PharmacyMemberViewModel> GetPharmacyMemberByKeyAsync(Guid pharmacyID, Guid memberID, CancellationToken cancellationToken)
    {
        PharmacyMemberPersistence member = await _db.PharmacyMembers
            .Include(m => m.Pharmacy)
            .FirstAsync(m =>
                m.ID == memberID
                && m.PharmacyID == pharmacyID
                && m.Pharmacy!.State == PharmacyStatePersistence.Active,
                cancellationToken);

        return member.ToPharmacyMemberViewModel();
    }

    public async Task RemovePharmacyMemberAsync(Guid pharmacyID, Guid memberID, CancellationToken cancellationToken)
    {
        PharmacyMemberPersistence member = await _db.PharmacyMembers
            .Include(m => m.Pharmacy)
            .FirstAsync(m =>
                m.ID == memberID
                && m.PharmacyID == pharmacyID
                && m.Pharmacy!.State == PharmacyStatePersistence.Active,
                cancellationToken);

        member.State = PharmacyMemberStatePersistence.Removed;
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task LinkUserIDToPharmacyMember(Guid userID, string email)
    {
        PharmacyMemberPersistence member = await _db.PharmacyMembers
            .Include(m => m.Pharmacy)
            .FirstAsync(m => m.Email == email);

        if (member.UserID is null)
        {
            member.UserID = userID;

            await _db.SaveChangesAsync();
        }
    }
}
