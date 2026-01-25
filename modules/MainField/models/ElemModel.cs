// modules/MainField/models/ElemModel.cs
using System.ComponentModel.DataAnnotations;

namespace CatatoniaServer.Modules.MainField.Models;
public class ElemModel
{
    [Key]
    public int elem_id { get; set; }
    public required string elem_name { get; set; }
    public required bool elem_plantable { get; set; }
    public required bool elem_harvestable { get; set; }
    public required bool elem_weed { get; set; }
    public required int elem_lifetime { get; set; }
    public required int elem_cost { get; set; }
    public ICollection<FieldElemModel>? field_elems { get; set; }
}
