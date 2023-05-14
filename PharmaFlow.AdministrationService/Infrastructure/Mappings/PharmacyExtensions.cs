namespace PharmaFlow.AdministrationService.Infrastructure.Mappings;

public static class PharmacyExtensions
{
    internal static PharmacyStatePersistence ToPharmacyStatePersistence(this PharmacyStateViewModel state)
    {
        return state switch
        {
            PharmacyStateViewModel.Active => PharmacyStatePersistence.Active,
            PharmacyStateViewModel.Removed => PharmacyStatePersistence.Removed,
            _ => throw new ArgumentException($"Invalid {nameof(state)}: {state}", nameof(state)),
        };
    }

    internal static PharmacyStateViewModel ToPharmacyStateViewModel(this PharmacyStatePersistence state)
    {
        return state switch
        {
            PharmacyStatePersistence.Active => PharmacyStateViewModel.Active,
            PharmacyStatePersistence.Removed => PharmacyStateViewModel.Removed,
            _ => throw new ArgumentException($"Invalid {nameof(state)}: {state}", nameof(state)),
        };
    }

    internal static List<PharmacyViewModel> ToPharmacyViewModelList(this List<PharmacyPersistence> pharmacyList)
    {
        return pharmacyList.ConvertAll(p => p.ToPharmacyViewModel());
    }

    internal static PharmacyViewModel ToPharmacyViewModel(this PharmacyPersistence pharmacy)
    {
        return new PharmacyViewModel()
        {
            ID = pharmacy.ID,
            Name = pharmacy.Name,
            State = pharmacy.State.ToPharmacyStateViewModel(),
        };
    }
}
