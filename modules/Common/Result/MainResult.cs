// modules/Common/Result/MainResult.cs

namespace CatatoniaServer.Modules.Common.Result;
public class MainResult<T> : BaseResult
{
    public T? Received { get; set; }
}
