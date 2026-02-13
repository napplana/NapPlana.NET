using NapPlana.Core.Bot.BotInstance;
using NapPlana.Core.Data.API;

namespace NapPlana.DI.Service;

public class BotContext: INapBot
{
    public async Task<GroupMessageSendResponseData> SendGroupMessageAsync(GroupMessageSend groupMessage, int timeoutSeconds = 15)
    {
        
    }

    public async Task<PrivateMessageSendResponseData> SendPrivateMessageAsync(PrivateMessageSend privateMessage, int timeoutSeconds = 15)
    {
        
    }

    public async Task SendPokeAsync(PokeMessageSend pokeMessage)
    {
        
    }

    public async Task DeleteGroupMessageAsync(GroupMessageDelete deleteGroupMessage)
    {
        
    }

    public long SelfId { get; set; }
}