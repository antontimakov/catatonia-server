// Models/FieldElemModel.cs
using System.ComponentModel.DataAnnotations;

namespace CatatoniaServer.Modules.MainField.Models;
public class FieldElemModel
{
    [Key]
    public int field_elem_id { get; set; }
    public required int elem_id { get; set; }
    public required int field_id { get; set; }
    public required int x { get; set; }
    public required int y { get; set; }
    public required int field_order { get; set; }
    public required DateTime updated { get; set; }

    // Навигационные свойства
    public required FieldModel field { get; set; }
    public required ElemModel elem { get; set; }
}
