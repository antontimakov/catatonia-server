// modules/Common/Result/BaseResult.cs
using System.Text.Json.Serialization;

namespace CatatoniaServer.Modules.Common.Result;
public abstract class BaseResult
{
    [JsonPropertyName("Time")]
    public string Time { get; set; }
    [JsonPropertyName("Status")]
    public string Status { get; set; }

    public BaseResult()
    {
        Time = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.000Z");
        Status = "ok";
    }
}
