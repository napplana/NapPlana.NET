using NapPlana.Core.Connections;

namespace NapPlana.Core.Bot;

public class NapBot
{
    private ConnectionBase _connection;
    
    public NapBot()
    {
        // Default to a dummy connection; should be set properly later
        _connection = new ConnectionBase();
    }

    // Added: constructor that accepts a connection
    public NapBot(ConnectionBase connection)
    {
        _connection = connection;
    }

    // Added: fluent setter for the connection
    public NapBot SetConnection(ConnectionBase connection)
    {
        _connection = connection;
        return this;
    }

    // Added: lifecycle helpers
    public Task StartAsync() => _connection.InitializeAsync();
    public Task StopAsync() => _connection.ShutdownAsync();

    // Added: message helpers
    public Task SendMessageAsync(string message) => _connection.SendMessageAsync(message);
}