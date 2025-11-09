// See https://aka.ms/new-console-template for more information

using System.Threading;
using NapPlana.Core.Bot;
using NapPlana.Core.Data;
using NapPlana.Core.Event.Handler;

var bot = BotFactory
    .Create()
    .SetConnectionType(BotConnectionType.WebSocketClient)
    .SetIp("172.17.21.238")
    .SetPort(6100)
    .SetToken("plana-bot")
    .Build();
BotEventHandler.OnLogReceived += (level, message) =>
{
    Console.WriteLine($"[{level}] {message}");
};


var cts = new CancellationTokenSource();
Console.CancelKeyPress += async (s, e) =>
{
    e.Cancel = true;
    await bot.StopAsync();
    cts.Cancel();
};

await bot.StartAsync();

try
{
    await Task.Delay(Timeout.Infinite, cts.Token);
}
catch (TaskCanceledException)
{
}
