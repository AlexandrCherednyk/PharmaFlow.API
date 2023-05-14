namespace PharmaFlow.AdministrationService.ViewModels.Pharmacies;

public record CreatePharmacyViewModel
{
    [Required]
    public string Name { get; init; } = null!;
}
