namespace PharmaFlow.AdministrationService.ViewModels.PharmacyMembers;

public record CreatePharmacyMemberViewModel
{
    [Required]
    [MaxLength(150)]
    public string FirstName { get; set; } = null!;

    [Required]
    [MaxLength(150)]
    public string LastName { get; set; } = null!;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;

    [Required]
    [Phone]
    public string Phone { get; set; } = null!;
}
