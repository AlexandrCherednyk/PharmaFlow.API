namespace PharmaFlow.AdministrationService.ViewModels.Pharmacies;

public record PharmacyViewModel
{
    public required Guid ID { get; init; }

    public required string Name { get; init; }

    public required PharmacyStateViewModel State { get; init; }
}
