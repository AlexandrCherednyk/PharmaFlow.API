namespace PharmaFlow.AdministrationService.Data.Persistences;

[Table("pharmacy_member")]
public class PharmacyMemberPersistence
{
    public Guid ID { get; set; }

    public Guid PharmacyID { get; set; }

    public PharmacyPersistence? Pharmacy { get; set; }

    public Guid? UserID { get; set; }

    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    [EmailAddress]
    public required string Email { get; set; }

    [Phone]
    public required string Phone { get; set; }

    public PharmacyMemberStatePersistence State { get; set; } = PharmacyMemberStatePersistence.Active;
}
