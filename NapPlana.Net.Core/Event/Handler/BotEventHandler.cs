using NapPlana.Core.Data;

namespace NapPlana.Core.Event.Handler;

public static class BotEventHandler
{
    /// <summary>
    /// 日志通知事件
    /// </summary>
    public static event Action<LogLevel,string>? OnLogReceived;

    public static void LogReceived(LogLevel logLevel, string message)
    {
        OnLogReceived?.Invoke(logLevel, message);
    }
    
    /// <summary>
    /// 机器人正式上线
    /// </summary>
    public static event Action? OnBotConnected;
    public static void BotConnected()
    {
        OnBotConnected?.Invoke();
    }
}