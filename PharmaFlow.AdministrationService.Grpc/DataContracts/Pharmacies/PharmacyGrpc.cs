namespace PharmaFlow.AdministrationService.Grpc.DataContracts.Pharmacies;

[ProtoContract]
[ProtoInclude(101, typeof(PharmacyKeyGrpc))]
[ProtoInclude(102, typeof(PharmacyPayloadGrpc))]
public record PharmacyGrpc
{
    [ProtoMember(1)]
    public PharmacyKeyGrpc? Key { get; set; }

    [ProtoMember(2)]
    public PharmacyPayloadGrpc? Payload { get; set; } 
}
