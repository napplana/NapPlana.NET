using System.Collections.Concurrent;
using NapPlana.Core.Data;
using NapPlana.Core.Data.Action;
using NapPlana.Core.Data.API;
using NapPlana.Core.Event.Handler;

namespace NapPlana.Core.API;

/// <summary>
/// 从WS接收到的信息进入这里等待消费
/// 为了统一，Http也需要在缓冲区等待
/// </summary>
public static class ApiHandler
{
    private static readonly ConcurrentDictionary<string, TaskCompletionSource<ActionResponse>> Awaiters = new();

    public static bool TryRegister(string echo, TaskCompletionSource<ActionResponse> tcs)
        => Awaiters.TryAdd(echo, tcs);

    public static bool TryRemove(string echo, out TaskCompletionSource<ActionResponse>? tcs)
        => Awaiters.TryRemove(echo, out tcs);
    
    public static void Dispatch(ActionResponse raw)
    {
        if (string.IsNullOrEmpty(raw.Echo)) 
            return;
        if (!Awaiters.TryRemove(raw.Echo, out var tcs)) 
            return;
        try
        {
            if (raw.RetCode != 0)
            {
                tcs.TrySetException(new InvalidOperationException($"API failed: {raw.RetCode} - {raw.Message}"));
                return;
            }
            tcs.TrySetResult(raw);
        }
        catch (Exception ex)
        {
            tcs.TrySetException(ex);
        }
    }
    
}