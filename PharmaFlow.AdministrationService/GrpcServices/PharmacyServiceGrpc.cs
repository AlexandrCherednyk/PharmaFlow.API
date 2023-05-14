namespace PharmaFlow.AdministrationService.Services;

internal class PharmacyServiceGrpc : IPharmacyServiceGrpc
{
    private readonly ILogger<PharmacyServiceGrpc> _logger;
    private readonly AdministrationServiceDbContext _db;

    public PharmacyServiceGrpc(
        ILogger<PharmacyServiceGrpc> logger,
        AdministrationServiceDbContext db)
    {
        _logger = logger;
        _db = db;
    }

    public async Task<PharmacyKeyGrpc> CreatePharmacyAsync(PharmacyPayloadGrpc requestGrpc, CallContext context = default)
    {
        if (String.IsNullOrEmpty(requestGrpc.Name))
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid arguments..."), "Invalid arguments...");
        }

        try
        {
            PharmacyPersistence pharmacyPersistence = new()
            {
                Name = requestGrpc.Name,
            };
            PharmacyPersistence pharmacy = pharmacyPersistence;

            _db.Pharmacies.Add(pharmacy);
            await _db.SaveChangesAsync();

            return new PharmacyKeyGrpc
            {
                ID = pharmacy.ID,
            };
        }
        catch (OperationCanceledException)
        {
            throw new RpcException(new Status(StatusCode.Cancelled, "Operation was cancelled."), "Operation was cancelled.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Creating pharmacy error.");
            throw new RpcException(new Status(StatusCode.Unknown, "Unknon error."), "Unknon error.");
        }
    }
}
