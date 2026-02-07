// modules/MainField/models/ElemModel.cs
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace CatatoniaServer.Modules.MainField.Models;
public class ElemModel
{
    [Key]
    [JsonPropertyName("Id")]
    public int Id { get; set; }
    [JsonPropertyName("Name")]
    public required string Name { get; set; }
    [JsonPropertyName("IsPlantable")]
    public required bool IsPlantable { get; set; }
    [JsonPropertyName("IsHarvestable")]
    public required bool IsHarvestable { get; set; }
    [JsonPropertyName("IsWeed")]
    public required bool IsWeed { get; set; }
    [JsonPropertyName("Lifetime")]
    public required int Lifetime { get; set; }
    [JsonPropertyName("Cost")]
    public required int Cost { get; set; }
    [JsonPropertyName("FieldElems")]
    public ICollection<FieldElemModel>? FieldElems { get; set; }
}
