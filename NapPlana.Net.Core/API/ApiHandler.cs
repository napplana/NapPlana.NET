using System.Collections.Concurrent;
using NapPlana.Core.Data.Action;

namespace NapPlana.Core.API;

public class ApiHandler: IApiHandler
{
    private readonly ConcurrentDictionary<string, TaskCompletionSource<ActionResponse>> Awaiters = new();
    public bool TryRegister(string echo, TaskCompletionSource<ActionResponse> tcs)
    {
        return Awaiters.TryAdd(echo, tcs);
    }

    public bool TryRemove(string echo, out TaskCompletionSource<ActionResponse>? tcs)
    {
        return Awaiters.TryRemove(echo, out tcs);
    }

    public void Dispatch(ActionResponse raw)
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