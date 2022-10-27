using System.Runtime.Serialization;

namespace AngularTestBackendServer.Core.Enums;

public enum Division
{
    [EnumMember(Value = "NFC West")]
    NfcWest = 1,
    [EnumMember(Value = "NFC East")]
    NfcEast = 2,
    [EnumMember(Value = "NFC South")]
    NfcSouth = 3,
    [EnumMember(Value = "NFC North")]
    NfcNorth = 4,
    [EnumMember(Value = "AFC West")]
    AfcWest = 5,
    [EnumMember(Value = "AFC East")]
    AfcEast = 6,
    [EnumMember(Value = "AFC South")]
    AfcSouth = 7,
    [EnumMember(Value = "AFC North")]
    AfcNorth = 8,
    
    // legacy
    [EnumMember(Value = "NFC Central")]
    NfcCentral = 9,
    [EnumMember(Value = "AFC Central")]
    AfcCentral = 10
}