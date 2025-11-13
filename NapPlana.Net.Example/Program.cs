using NapPlana.Core.Bot;
using NapPlana.Core.Data;
using NapPlana.Core.Event.Handler;
using NapPlana.Example.Examples;
using Microsoft.Extensions.Configuration;

// Load configuration
// Ensure you have an appsettings.json file in the output directory with the necessary settings
// Rename Example appsettings_example.json to appsettings.json and fill in your details
var config = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

var napCatSection = config.GetSection("NapCatConfig");
var botSection = config.GetSection("BotConfig");

var ip = napCatSection.GetValue<string>("IP");
var port = napCatSection.GetValue<int>("Port");
var token = napCatSection.GetValue<string>("Token");
var selfId = botSection.GetValue<long>("SelfId");

var bot = PlanaBotFactory
    .Create()
    .SetSelfId(selfId)// Your bot QQ ID
    .SetConnectionType(BotConnectionType.WebSocketClient)// Set connection type,only this mode is supported now
    .SetIp(ip ?? "127.0.0.1") // Your napcat server IP
    .SetPort(port)// Your napcat server port
    .SetToken(token)// Your napcat token
    .Build();
// Log handler
BotEventHandler.OnLogReceived += (level, message) =>
{
    // Print log to console,you can integrate with your own logging system
    Console.WriteLine($"[{level}] {message}");
};

// Start the bot
await bot.StartAsync();

// Example usage: Poke back when poked
BotEventHandler.OnGroupPokeNoticeReceived += async (notice) =>
{
    //exclude self poke
    if (notice.UserId == bot.SelfId)
    {
        return;
    }

    await PokeBack.ExecuteAsyncGroup(bot, notice.GroupId.ToString(), notice.UserId.ToString());
};

// Initialize other examples
NeteaseVoice.InitializeAsync(bot);

// Graceful shutdown on Ctrl+C
// Prevent the process from terminating immediately
var cts = new CancellationTokenSource();
Console.CancelKeyPress += async (s, e) =>
{
    e.Cancel = true;
    await bot.StopAsync();
    cts.Cancel();
};

try
{
    await Task.Delay(Timeout.Infinite, cts.Token);
}
catch (TaskCanceledException)
{

}
