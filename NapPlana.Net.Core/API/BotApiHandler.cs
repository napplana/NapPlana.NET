using NapPlana.Core.Data.Action;

namespace NapPlana.Core.API;

/// <summary>
/// 从WS接收到的信息进入这里等待消费
/// 为了统一，Http也需要在缓冲区等待
/// </summary>
public static class BotApiHandler
{
    private static readonly ApiHandler Handler = new();

    /// <summary>
    /// 添加待接收响应
    /// </summary>
    /// <param name="echo">标识符</param>
    /// <param name="tcs">tcs</param>
    /// <returns>是否成功</returns>
    public static bool TryRegister(string echo, TaskCompletionSource<ActionResponse> tcs)
        => Handler.TryRegister(echo, tcs);

    /// <summary>
    /// 响应接收到之后移除
    /// </summary>
    /// <param name="echo">标识符</param>
    /// <param name="tcs">tcs</param>
    /// <returns>是否成功</returns>
    public static bool TryRemove(string echo, out TaskCompletionSource<ActionResponse>? tcs)
        => Handler.TryRemove(echo, out tcs);
    
    /// <summary>
    /// 接收响应
    /// </summary>
    /// <param name="raw">响应消息</param>
    public static void Dispatch(ActionResponse raw) => Handler.Dispatch(raw);
    
}