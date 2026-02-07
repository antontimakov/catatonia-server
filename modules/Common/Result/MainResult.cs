// modules/Common/Result/MainResult.cs
using System.Text.Json.Serialization;

namespace CatatoniaServer.Modules.Common.Result;
public class MainResult<T> : BaseResult
{
    [JsonPropertyName("Received")]
    public T? Received { get; set; }
}
