namespace PharmaFlow.AdministrationService.Data.Persistences;

[Table("pharmacy")]
public record PharmacyPersistence
{
    public Guid ID { get; set; }

    public required string Name { get; set; }

    public PharmacyStatePersistence State { get; set; } = PharmacyStatePersistence.Active;

    public List<PharmacyMemberPersistence>? Members { get; set; }
}
