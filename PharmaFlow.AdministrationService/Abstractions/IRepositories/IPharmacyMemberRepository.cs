namespace PharmaFlow.AdministrationService.Abstractions.IRepositories;

public interface IPharmacyMemberRepository
{
    Task<Guid> AddMemberToPharmacyAsync(Guid pharmacyID, CreatePharmacyMemberViewModel request, CancellationToken cancellationToken);

    Task UpdatePharmacyMemberAsync(Guid pharmacyID, Guid memberID, UpdatePharmacyMemberViewModel request, CancellationToken cancellationToken);

    Task<List<PharmacyMemberViewModel>> GetPharmacyMemberListAsync(Guid pharmacyID,  CancellationToken cancellationToken);

    Task<PharmacyMemberViewModel> GetPharmacyMemberByKeyAsync(Guid pharmacyID, Guid memberID, CancellationToken cancellationToken);

    Task RemovePharmacyMemberAsync(Guid pharmacyID, Guid memberID, CancellationToken cancellationToken);

    Task LinkUserIDToPharmacyMember(Guid userID, string email);
}
