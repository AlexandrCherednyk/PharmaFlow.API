namespace PharmaFlow.AdministrationService.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
public class PharmacyController : ControllerBase
{
    private readonly ILogger<PharmacyController> _logger;
    private readonly IPharmacyRepository _pharmacyRepository;

    public PharmacyController(
        ILogger<PharmacyController> logger,
        IPharmacyRepository pharmacyRepository)
    {
        _logger = logger;
        _pharmacyRepository = pharmacyRepository;
    }

    [HttpPost("/api/pharmacies")]
    [Authorize(Roles = "realm-admin, manage-realm")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult<Guid>> CreatePharmacy(
        [FromBody]
        CreatePharmacyViewModel request,
        CancellationToken cancellationToken)
    {
        try
        {
            Guid pharmacyKey = await _pharmacyRepository.AddPharmacyAsync(request, cancellationToken);

            return Ok(pharmacyKey);
        }
        catch (OperationCanceledException)
        {
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Pharmacy '{PharmacyName}' was not created.", request.Name);

            return Problem();
        }
    }

    [HttpPut("/api/pharmacies/{pharmacyID}")]
    [Authorize(Roles = "realm-admin, manage-realm, pharmacy-admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdatePharmacy(
        [FromRoute]
        Guid pharmacyID,
        UpdatePharmacyViewModel request,
        CancellationToken cancellationToken)
    {
        try
        {
            await _pharmacyRepository.UpdatePharmacyAsync(pharmacyID, request, cancellationToken);

            return Ok();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
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
            _logger.LogError(ex, "Pharmacy with: {PharmacyID} was not update.", pharmacyID);
            return Problem();
        }
    }

    [HttpGet("/api/pharmacies")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult<List<PharmacyViewModel>>> GetPharmacyList(CancellationToken cancellationToken)
    {
        try
        {
            List<PharmacyPersistence> pharmacyList = await _pharmacyRepository.GetPharmacyListAsync(cancellationToken);

            return pharmacyList.ToPharmacyViewModelList();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (OperationCanceledException)
        {
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to get pharmacy list...");
            return Problem();
        }
    }

    [HttpGet("/api/pharmacies/{pharmacyID}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult<PharmacyViewModel>> GetPharmacy(
        [FromRoute]
        Guid pharmacyID, 
        CancellationToken cancellationToken)
    {
        try
        {
            PharmacyPersistence pharmacy = await _pharmacyRepository.GetPharmacyAsync(pharmacyID, cancellationToken);

            return pharmacy.ToPharmacyViewModel();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (OperationCanceledException)
        {
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to get pharmacy with ID: {PharmacyID}", pharmacyID);
            return Problem();
        }
    }

    [HttpDelete("/api/pharmacies/{pharmacyID}")]
    [Authorize(Roles = "realm-admin, manage-realm, pharmacy-admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> RemovePharmacy(
        [FromRoute]
        Guid pharmacyID,
        CancellationToken cancellationToken)
    {
        try
        {
            await _pharmacyRepository.RemovePharmacyAsync(pharmacyID, cancellationToken);

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
            _logger.LogError(ex, "Failed to remove pharmacy with ID: {PharmacyID}", pharmacyID);
            return Problem();
        }
    }
}