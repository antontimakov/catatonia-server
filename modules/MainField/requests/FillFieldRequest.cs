// modules/MainField/requests/FillFieldRequest.cs

namespace CatatoniaServer.Modules.MainField.Requests;
public class FillFieldRequest
{
    public required string old_elem_name { get; set; }
    public required string new_elem_name { get; set; }
    public required int x { get; set; }
    public required int y { get; set; }
}
