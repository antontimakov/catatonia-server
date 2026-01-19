// modules/MainField/dbr/FillFieldDbr.cs

namespace CatatoniaServer.Modules.MainField.Dbr;
public class FillFieldDbr
{
    public required int field_elem_id { get; set; }
    public required int elem_id { get; set; }
    public required string elem_name { get; set; }
    public required int x { get; set; }
    public required int y { get; set; }
    public required bool elem_plantable { get; set; }
    public required bool elem_harvestable { get; set; }
    public required bool elem_weed { get; set; }
    public required int elem_lifetime { get; set; }
    public required DateTime updated { get; set; }
}
