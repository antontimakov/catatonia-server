// Models/FieldElemModel.cs
using System.ComponentModel.DataAnnotations;

namespace CatatoniaServer.Modules.MainField.Models;
public class FieldElemModel
{
    [Key]
    public int Id { get; set; }
    public required int ElemId { get; set; }
    public required int FieldId { get; set; }
    public required int X { get; set; }
    public required int Y { get; set; }
    public required int LayerOrder { get; set; }
    public required DateTime UpdatedAt { get; set; }

    // Навигационные свойства
    public required FieldModel Field { get; set; }
    public required ElemModel Elem { get; set; }
}
