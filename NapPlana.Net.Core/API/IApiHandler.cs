using NapPlana.Core.Data.Action;

namespace NapPlana.Core.API;

public interface IApiHandler
{
    bool TryRegister(string echo, TaskCompletionSource<ActionResponse> tcs);
    bool TryRemove(string echo, out TaskCompletionSource<ActionResponse>? tcs);
    void Dispatch(ActionResponse raw);
}