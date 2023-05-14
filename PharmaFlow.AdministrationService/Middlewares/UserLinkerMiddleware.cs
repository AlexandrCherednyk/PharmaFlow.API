using System.Security.Claims;

namespace PharmaFlow.AdministrationService.Middlewares;

public class UserLinkerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<UserLinkerMiddleware> _logger;


    public UserLinkerMiddleware(
        RequestDelegate next,
        ILogger<UserLinkerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, IPharmacyMemberRepository pharmacyMemberRepository)
    {
        try
        {
            string userID = context.User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            string email = context.User.FindFirstValue(ClaimTypes.Email)!;

            await pharmacyMemberRepository.LinkUserIDToPharmacyMember(Guid.Parse(userID), email);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "User ID was not linked to pharmacy member.");
        }

        await _next.Invoke(context);
    }
}
