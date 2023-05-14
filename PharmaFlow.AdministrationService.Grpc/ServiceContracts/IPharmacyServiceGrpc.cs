namespace PharmaFlow.AdministrationService.Grpc.ServiceContracts;

[Service]
public interface IPharmacyServiceGrpc
{
    [Operation]
    Task<PharmacyKeyGrpc> CreatePharmacyAsync(PharmacyPayloadGrpc requestGrpc, CallContext context = default);
}
