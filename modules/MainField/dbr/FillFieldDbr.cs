// modules/MainField/dbr/FillFieldDbr.cs
using System.Text.Json.Serialization;

namespace CatatoniaServer.Modules.MainField.Dbr;
public class FillFieldDbr
{
    [JsonPropertyName("FieldElemId")]
    public required int FieldElemId { get; set; }
    [JsonPropertyName("ElemId")]
    public required int ElemId { get; set; }
    [JsonPropertyName("Name")]
    public required string Name { get; set; }
    [JsonPropertyName("X")]
    public required int X { get; set; }
    [JsonPropertyName("Y")]
    public required int Y { get; set; }
    [JsonPropertyName("IsPlantable")]
    public required bool IsPlantable { get; set; }
    [JsonPropertyName("IsHarvestable")]
    public required bool IsHarvestable { get; set; }
    [JsonPropertyName("IsWeed")]
    public required bool IsWeed { get; set; }
    [JsonPropertyName("Lifetime")]
    public required int Lifetime { get; set; }
    [JsonPropertyName("UpdatedAt")]
    public required DateTime UpdatedAt { get; set; }
}
