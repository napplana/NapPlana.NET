using NapPlana.Core.Bot;
using NapPlana.Core.Bot.BotInstance;
using NapPlana.Core.Data.API;

namespace NapPlana.Example.Examples;

/// <summary>
/// 戳回去
/// </summary>
public static class PokeBack
{
    /// <summary>
    /// 群聊里戳回去
    /// </summary>
    /// <param name="bot">机器人实例</param>
    /// <param name="groupId">群组id</param>
    /// <param name="userId">对象id</param>
    public static async Task ExecuteAsyncGroup(NapBot bot, string groupId, string userId)
    {
        //发送消息
        var message = MessageChainBuilder.Create()
            .AddMentionMessage(userId)
            .AddTextMessage(" 已戳你");
        
        //拉取图片到FileStream
        var imagePath = Path.Combine(AppContext.BaseDirectory, "nap_plana.png");
        if (File.Exists(imagePath))
        {
            using var fs = File.OpenRead(imagePath);
            message.AddImageMessage(fs);
        }
        else
        {
            Console.WriteLine($"未找到图片文件: {imagePath}，将仅发送文本消息。");
        }
        
        var groupMessage = new GroupMessageSend
        {
            GroupId = groupId,
            Message = message.Build()
        };
        await bot.SendGroupMessageAsync(groupMessage);
        //戳回去
        var pokeMessage = new PokeMessageSend
        {
            GroupId = groupId,
            UserId = userId
        };
        await bot.SendPokeAsync(pokeMessage);
    }
    
    /// <summary>
    /// 私聊内戳回去
    /// </summary>
    /// <param name="bot">机器人实例</param>
    /// <param name="userId">用户id</param>
    public static async Task ExecuteAsyncPrivate(NapBot bot, string userId)
    {
        var pokeMessage = new PokeMessageSend
        {
            UserId = userId
        };
        await bot.SendPokeAsync(pokeMessage);
    }
}