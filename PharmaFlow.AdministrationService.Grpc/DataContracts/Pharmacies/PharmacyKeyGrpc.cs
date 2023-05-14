namespace PharmaFlow.AdministrationService.Grpc.DataContracts.Pharmacies;

[ProtoContract]
public record PharmacyKeyGrpc
{
    [ProtoMember(1)]
    public required Guid ID { get; init; }
}
