namespace PharmaFlow.AdministrationService.Infrastructure.Mappings;

public static class PharmacyMemberExtensions
{
    internal static List<PharmacyMemberViewModel> ToPharmacyMemberViewModelList(this List<PharmacyMemberPersistence> memberList)
    {
        return memberList.ConvertAll(pm => pm.ToPharmacyMemberViewModel());
    }

    internal static PharmacyMemberViewModel ToPharmacyMemberViewModel(this PharmacyMemberPersistence member)
    {
        return new PharmacyMemberViewModel()
        {
            ID = member.ID,
            UserID = member.UserID,
            FirstName = member.FirstName,
            LastName = member.LastName,
            Email = member.Email,
            Phone = member.Phone,
        };
    }
}
