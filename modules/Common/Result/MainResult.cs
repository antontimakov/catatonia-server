// modules/Common/Result/MainResult.cs

namespace CatatoniaServer.Modules.Common.Result;
public class MainResult<T> : BaseResult
{
    public List<T> received { get; set; }
    public MainResult() => received = [];
}
