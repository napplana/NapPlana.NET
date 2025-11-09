using NapPlana.Core.Connections.WebSocket;
using NapPlana.Core.Data;

namespace NapPlana.Core.Bot;

public class BotFactory
{
    private string _ip = string.Empty;
    private int _port = 0;
    private string? _token;
    private BotConnectionType _connectionType = BotConnectionType.WebSocketClient;

    private BotFactory() {}

    // Entry point
    public static BotFactory Create() => new BotFactory();

    public BotFactory SetIp(string ip)
    {
        _ip = ip;
        return this;
    }

    public BotFactory SetPort(int port)
    {
        _port = port;
        return this;
    }

    public BotFactory SetToken(string? token)
    {
        _token = token;
        return this;
    }
    public BotFactory SetConnectionType(BotConnectionType connectionType)
    {
        _connectionType = connectionType;
        return this;
    }

    public NapBot Build()
    {
        if (string.IsNullOrWhiteSpace(_ip))
            throw new ArgumentException("请设置IP");
        if (_port <= 0)
            throw new ArgumentException("请设置正确的端口号");

        var connection = _connectionType switch
        {
            BotConnectionType.WebSocketClient => new WebsocketClientConnection(_ip, _port, _token),
            _ => throw new NotSupportedException("不支持的连接类型"),
        };
        return new NapBot(connection);
    }
}