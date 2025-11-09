using NapPlana.Core.Data;

namespace NapPlana.Core.Connections;

public interface IConnectionBase
{
    public BotConnectionType ConnectionType { get; protected set; }
}