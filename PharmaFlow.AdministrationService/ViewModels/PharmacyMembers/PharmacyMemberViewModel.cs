namespace PharmaFlow.AdministrationService.ViewModels.PharmacyMembers;

public record PharmacyMemberViewModel
{
    public Guid ID { get; init; }

    public Guid? UserID { get; init; }

    public required string FirstName { get; init; }

    public required string LastName { get; init; }

    public required string Email { get; init; }

    public required string Phone { get; init; }
}
