namespace PharmaFlow.AdministrationService.Grpc.DataContracts.Pharmacies;

[ProtoContract]
public record PharmacyPayloadGrpc
{
    [ProtoMember(1)]
    public required string Name { get; init; }
}
