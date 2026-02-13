using Microsoft.Extensions.Hosting;
using NapPlana.Core.Connections;

namespace NapPlana.DI.Service;

public class ConnectionService:IHostedService
{
    private readonly ConnectionBase _connection;
    
    public ConnectionService(ConnectionBase connection)
    {
        _connection = connection;
    }
    
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await _connection.InitializeAsync();
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await _connection.ShutdownAsync();
    }
}