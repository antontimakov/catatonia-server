// modules/MainField/requests/FillFieldRequest.cs

namespace CatatoniaServer.Modules.MainField.Requests;
public class FillFieldRequest
{
    public required string OldElemName { get; set; }
    public required string NewElemName { get; set; }
    public required int X { get; set; }
    public required int Y { get; set; }
}
