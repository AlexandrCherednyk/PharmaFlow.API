namespace PharmaFlow.AdministrationService.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
[ProducesResponseType(StatusCodes.Status403Forbidden)]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
public class PharmacyMemberController : ControllerBase
{
    private readonly ILogger<PharmacyMemberController> _logger;
    private readonly IPharmacyMemberRepository _pharmacyMemberRepository;

    public PharmacyMemberController(
        ILogger<PharmacyMemberController> logger,
        IPharmacyMemberRepository pharmacyMemberRepository)
    {
        _logger = logger;
        _pharmacyMemberRepository = pharmacyMemberRepository;
    }

    [HttpPost("/api/pharmacies/{pharmacyID}")]
    [Authorize(Roles = "realm-admin, manage-realm, pharmacy-admin, pharmacy-manager")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<Guid>> CreatePharmacyMember(
        [FromRoute]
        Guid pharmacyID,
        [FromBody]
        CreatePharmacyMemberViewModel request,
        CancellationToken cancellationToken)
    {
        try
        {
            Guid pharmacyKey = await _pharmacyMemberRepository.AddMemberToPharmacyAsync(pharmacyID, request, cancellationToken);

            return Ok(pharmacyKey);
        }
        catch (OperationCanceledException)
        {
            return NoContent();
        }
        catch (InvalidOperationException)
        {
            return Conflict();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "The user with email {Email} has not been added to pharmacy with ID {}.", request.Email, pharmacyID);

            return Problem();
        }
    }

    [HttpPut("/api/pharmacies/{pharmacyID}/members/{memberID}")]
    [Authorize(Roles = "realm-admin, manage-realm, pharmacy-admin, pharmacy-manager")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdatePharmacyMember(
        [FromRoute]
        Guid pharmacyID,
        [FromRoute]
        Guid memberID,
        [FromBody]
        UpdatePharmacyMemberViewModel request,
        CancellationToken cancellationToken)
    {
        try
        {
            await _pharmacyMemberRepository.UpdatePharmacyMemberAsync(pharmacyID, memberID, request, cancellationToken);

            return Ok();
        }
        catch (OperationCanceledException)
        {
            return NoContent();
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Pharmacy:{PharmacyID} member:{MemberID} was not updated.", pharmacyID, memberID);

            return Problem();
        }
    }

    [HttpGet("/api/pharmacies/{pharmacyID}/members")]
    [Authorize(Roles = "realm-admin, manage-realm, pharmacy-admin, pharmacy-manager, pharmacy-contributor")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<List<PharmacyMemberViewModel>>> GetPharmacyMemberList(
        [FromRoute]
        Guid pharmacyID,
        CancellationToken cancellationToken)
    {
        try
        {
            List<PharmacyMemberViewModel> members = await _pharmacyMemberRepository.GetPharmacyMemberListAsync(pharmacyID, cancellationToken);

            return Ok(members);
        }
        catch (OperationCanceledException)
        {
            return NoContent();
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Pharmacy:{PharmacyID} members were not received.", pharmacyID);

            return Problem();
        }
    }

    [HttpGet("/api/pharmacies/{pharmacyID}/members/{memberID}")]
    [Authorize(Roles = "realm-admin, manage-realm, pharmacy-admin, pharmacy-manager, pharmacy-contributor")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PharmacyMemberViewModel>> GetPharmacyMemberByKey(
        [FromRoute]
        Guid pharmacyID,
        [FromRoute]
        Guid memberID,
        CancellationToken cancellationToken)
    {
        try
        {
            PharmacyMemberViewModel member = await _pharmacyMemberRepository.GetPharmacyMemberByKeyAsync(pharmacyID, memberID, cancellationToken);

            return Ok(member);
        }
        catch (OperationCanceledException)
        {
            return NoContent();
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Pharmacy:{PharmacyID} member:{MemberID} was not received.", pharmacyID, memberID);

            return Problem();
        }
    }

    [HttpDelete("/api/pharmacies/{pharmacyID}/members/{memberID}")]
    [Authorize(Roles = "realm-admin, manage-realm, pharmacy-admin, pharmacy-manager")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> RemovePharmacyMember(
        [FromRoute]
        Guid pharmacyID,
        [FromRoute]
        Guid memberID,
        CancellationToken cancellationToken)
    {
        try
        {
            await _pharmacyMemberRepository.RemovePharmacyMemberAsync(pharmacyID, memberID, cancellationToken);

            return Ok();
        }
        catch (OperationCanceledException)
        {
            return NoContent();
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Pharmacy:{PharmacyID} member:{MemberID} was not removed.", pharmacyID, memberID);

            return Problem();
        }
    }
}