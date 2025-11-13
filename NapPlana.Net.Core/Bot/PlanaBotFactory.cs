using NapPlana.Core.Connections.WebSocket;
using NapPlana.Core.Data;

namespace NapPlana.Core.Bot;

/// <summary>
/// 机器人创建工厂
/// </summary>
public class PlanaBotFactory
{
    private string _ip = string.Empty;
    private int _port = 0;
    private string? _token;
    private BotConnectionType _connectionType = BotConnectionType.WebSocketClient;
    private long _selfId;

    private PlanaBotFactory() {}

    /// <summary>
    /// 创建机器人工厂实例
    /// </summary>
    /// <returns>工厂实例</returns>
    public static PlanaBotFactory Create() => new PlanaBotFactory();

    /// <summary>
    /// 设置访问IP
    /// </summary>
    /// <param name="ip">ip字段</param>
    /// <returns>工厂实例</returns>
    public PlanaBotFactory SetIp(string ip)
    {
        _ip = ip;
        return this;
    }
    
    /// <summary>
    /// 设置机器人QQ号
    /// </summary>
    /// <param name="selfId">QQ号</param>
    /// <returns>工厂实例</returns>
    public PlanaBotFactory SetSelfId(long selfId)
    {
        _selfId = selfId;
        return this;
    }

    /// <summary>
    /// 设置端口
    /// </summary>
    /// <param name="port">端口号</param>
    /// <returns>工厂实例</returns>
    public PlanaBotFactory SetPort(int port)
    {
        _port = port;
        return this;
    }

    /// <summary>
    /// 设置Token
    /// </summary>
    /// <param name="token">token，没有可不设置</param>
    /// <returns>工厂实例</returns>
    public PlanaBotFactory SetToken(string? token)
    {
        _token = token;
        return this;
    }
    /// <summary>
    /// 设置连接类型，目前仅支持WebSocketClient模式
    /// </summary>
    /// <param name="connectionType">连接方式</param>
    /// <returns>工厂实例</returns>
    public PlanaBotFactory SetConnectionType(BotConnectionType connectionType)
    {
        _connectionType = connectionType;
        return this;
    }

    /// <summary>
    /// 构造机器人实例
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentException">未正确设置必填项</exception>
    /// <exception cref="NotSupportedException">仅支持WebSocketClient模式</exception>
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
        return new NapBot(connection,_selfId);
    }
}