// modules/MainField/requests/FillFieldRequest.cs

namespace CatatoniaServer.Modules.MainField.Requests;
public class FillFieldRequest
{
    public int elem_id { get; set; }
    public string? elem_name { get; set; }
    public int x { get; set; }
    public int y { get; set; }
}
