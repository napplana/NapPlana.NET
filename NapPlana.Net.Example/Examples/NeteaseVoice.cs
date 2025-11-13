using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using NapPlana.Core.Bot;
using NapPlana.Core.Data.API;
using NapPlana.Core.Event.Handler;

namespace NapPlana.Example.Examples;

public record DisplaySongItem(long SongId, string Title, string Author,string Album);
public static class NeteaseVoice
{
    private static NapBot _bot;
    //groupId,userId -> messageId
    private static Dictionary<Tuple<long,long>,long> _searcherMessageCache = new();
    //groupId,userId -> retrieving messageId
    private static Dictionary<Tuple<long,long>,long> _searchingMessageRetrieve = new();
    //groupId,userId -> search results
    private static Dictionary<Tuple<long,long>, List<DisplaySongItem>> _searchResultsCache = new();
    
    public static void InitializeAsync(NapBot napbot)
    {
        _bot = napbot;
        BotEventHandler.OnGroupMessageReceived += async (e) =>
        {
            if (e.UserId == _bot.SelfId) return;
            //解析序号
            var key = Tuple.Create(e.GroupId, e.UserId);
            if (_searchResultsCache.TryGetValue(Tuple.Create(e.GroupId,e.UserId), out var songItems))
            {
                if (!int.TryParse(e.RawMessage, out var ind) || ind < 1 || ind > songItems.Count)
                {
                    var message = MessageChainBuilder.Create()
                        .AddReplyMessage(e.MessageId.ToString())
                        .AddTextMessage("请输入正确的序号来播放对应歌曲。").Build();
                    await _bot.SendGroupMessageAsync(new GroupMessageSend()
                    {
                        GroupId = e.GroupId.ToString(),
                        Message = message
                    });
                    _searchResultsCache.Remove(key);
                    return;
                }
                _searchResultsCache.Remove(key);
                var song = songItems[ind - 1];
                var voiceMessage = MessageChainBuilder.Create()
                    .AddReplyMessage(e.MessageId.ToString())
                    .AddTextMessage("正在获取歌曲，请稍候...").Build();
                var retrievingMessage = await _bot.SendGroupMessageAsync(new GroupMessageSend()
                {
                    GroupId = e.GroupId.ToString(),
                    Message = voiceMessage
                });
                _searchingMessageRetrieve[key] = retrievingMessage.MessageId;
                //解析，读取歌曲
                var voiceUrl = $"https://api.injahow.cn/meting/?server=netease&type=url&id={song.SongId}";
                var voiceMsg = MessageChainBuilder.Create()
                    .AddVoiceMessage(voiceUrl);
                await _bot.SendGroupMessageAsync(new GroupMessageSend()
                {
                    GroupId = e.GroupId.ToString(),
                    Message = voiceMsg.Build()
                },60);
                //删除正在获取消息
                if (_searchingMessageRetrieve.TryGetValue(key, out var retrievingMsgId))
                {
                    await _bot.DeleteGroupMessageAsync(new GroupMessageDelete()
                    {
                        MessageId = retrievingMsgId
                    });
                    _searchingMessageRetrieve.Remove(key);
                }
            }
            
            //通过消息链匹配前缀
            if (e.RawMessage.StartsWith("search "))
            {
                var keyword = e.RawMessage.Substring(7).Trim();
                var results = SearchSongs(keyword, 5);
                var sendMessage = MessageChainBuilder.Create().AddTextMessage(results.Count == 0 ? "未找到相关歌曲。" : "搜索结果如下：\n");
                if (results.Count == 0)
                {
                    await _bot.SendGroupMessageAsync(new GroupMessageSend()
                    {
                        GroupId = e.GroupId.ToString(),
                        Message = sendMessage.Build()
                    });
                }
                else
                {
                    for (var index = 0; index < results.Count; index++)
                    {
                        var song = results[index];
                        sendMessage.AddTextMessage($"  {index+1} - {song.SongId} - {song.Title} / {song.Author} ({song.Album})\n");
                    }
                    sendMessage.AddTextMessage("发送序号来播放对应歌曲。\n");
                    //cache
                    _searchResultsCache.TryAdd(key, results);
                    
                    await _bot.SendGroupMessageAsync(new GroupMessageSend()
                    {
                        GroupId = e.GroupId.ToString(),
                        Message = sendMessage.Build()
                    });
                }
            }
        };
    }
    /// <summary>
    /// 搜索网易云音乐歌曲，无需在意具体的过程，只需要在意结果即可
    /// </summary>
    /// <param name="keyword">关键词</param>
    /// <param name="limit">限制</param>
    /// <returns></returns>
    public static List<DisplaySongItem> SearchSongs(string keyword, int limit)
    {
        if (string.IsNullOrWhiteSpace(keyword)) return new List<DisplaySongItem>();
        if (limit <= 0) limit = 10;

        var encoded = Uri.EscapeDataString(keyword);
        var url = $"http://music.163.com/api/search/get/web?csrf_token=hlpretag=&hlposttag=&s={encoded}&type=1&offset=0&total=true&limit={limit}";

        try
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (compatible; NeteaseVoiceBot/1.0)");
            client.DefaultRequestHeaders.Referrer = new Uri("https://music.163.com/");
            client.DefaultRequestHeaders.Accept.ParseAdd("application/json, text/plain, */*");

            var resp = client.GetStringAsync(url).GetAwaiter().GetResult();
            if (string.IsNullOrWhiteSpace(resp)) return new List<DisplaySongItem>();

            using var doc = JsonDocument.Parse(resp);
            var root = doc.RootElement;
            if (!root.TryGetProperty("result", out var resultElem)) return new List<DisplaySongItem>();
            if (!resultElem.TryGetProperty("songs", out var songsElem)) return new List<DisplaySongItem>();
            if (songsElem.ValueKind != JsonValueKind.Array) return new List<DisplaySongItem>();

            var list = new List<DisplaySongItem>();
            foreach (var song in songsElem.EnumerateArray())
            {
                try
                {
                    long id = 0;
                    if (song.TryGetProperty("id", out var idProp) && idProp.ValueKind == JsonValueKind.Number)
                    {
                        id = idProp.GetInt64();
                    }

                    string title = song.TryGetProperty("name", out var nameProp) && nameProp.ValueKind == JsonValueKind.String
                        ? nameProp.GetString() ?? string.Empty
                        : string.Empty;

                    string author = string.Empty;
                    if (song.TryGetProperty("artists", out var artistsElem) && artistsElem.ValueKind == JsonValueKind.Array)
                    {
                        var names = artistsElem.EnumerateArray()
                            .Select(a => (a.ValueKind == JsonValueKind.Object && a.TryGetProperty("name", out var n) && n.ValueKind == JsonValueKind.String) ? n.GetString() ?? string.Empty : string.Empty)
                            .Where(s => !string.IsNullOrEmpty(s));
                        author = string.Join("/", names);
                    }

                    string album = string.Empty;
                    if (song.TryGetProperty("album", out var albumElem) && albumElem.ValueKind == JsonValueKind.Object && albumElem.TryGetProperty("name", out var albumNameProp) && albumNameProp.ValueKind == JsonValueKind.String)
                    {
                        album = albumNameProp.GetString() ?? string.Empty;
                    }

                    if (id != 0)
                    {
                        list.Add(new DisplaySongItem(id, title, author, album));
                    }
                }
                catch
                {
                    // skip malformed song entries
                }
            }

            return list;
        }
        catch
        {
            return new List<DisplaySongItem>();
        }
    }
}