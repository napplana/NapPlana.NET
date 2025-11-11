<p align="center"><a href="https://blog.stevesensei.work" target="_blank" rel="noopener noreferrer"><img width="350" height="350" src="https://i.imgur.com/p7pBCgg.png" alt=""></a></p>

<div align="center">

# NapPlana.NET

_Yet Another NapCat Framework SDK for .NET_

_logo artwork by: [芋泥雪糕喵](https://www.mihuashi.com/profiles/155732?role=painter)_

> 待機中、解決しなければならない作業が多数存在しています。



| Nuget                                                 | License | Workflow                                                                                                | Name Inspired By                                    | Github                                                                     |
|-------------------------------------------------------|---------|---------------------------------------------------------------------------------------------------------|-----------------------------------------------------|----------------------------------------------------------------------------|
| ![nuget](https://img.shields.io/nuget/v/NapPlana.NET) | ![](https://img.shields.io/github/license/stevesensei/NapPlana.NET)   | ![wf](https://img.shields.io/github/actions/workflow/status/stevesensei/NapPlana.NET/publish-nuget.yml) | ![](https://img.shields.io/badge/Blue-Archive-cyan) | ![last](https://img.shields.io/github/last-commit/stevesensei/NapPlana.NET) |

</div>

## 欢迎

这是一个.NET 平台下的[NapCat](https://github.com/NapNeko/NapCatQQ/)框架的SDK实现，旨在为开发者提供在.NET平台下开发QQ机器人的又一个解决方案。

虽然没法保证较高的效率，但至少能用，一些重复度较高的部分用copilot生成了一下

至于为什么不是NapCat.NET?

只能说这是本人 ~~(某神人档p)~~ 的奇妙趣味，以及我正在开发的机器人叫PlanaBot，这个框架原本是给这个机器人用的，但我找了一圈没在github上找到完成度比较高的实现，所以我把自己写的开源了。

## 快速开始

### 前置条件

- 请在napcat里创建一个`WebSocket服务器`，目前仅支持这种形式，配置好ip，token和端口号

- 确保配置中的消息格式为`array`

### 安装
 - 使用NuGet安装NapPlana.NET包：

```bash
Install-Package NapPlana.NET
```

- 该项目会自动安装`TouchSocket`和`TouchSocket.Http`作为依赖项，请确保其版本`>=4.0.0-rc.5`

### 创建一个机器人实例

```csharp
using NapPlana.Core.Bot;

//目前仅支持使用WebSocketClient连接方式
var bot = BotFactory
    .Create()
    .SetConnectionType(BotConnectionType.WebSocketClient) //设置连接到napcat服务器的方式
    .SetIp("your-ip") //设置ip
    .SetPort(6100) //设置端口
    .SetToken("your-token") //设置token
    .Build(); //创建机器人实例
```

该实例创建结束后，您需要手动调用`StartAsync()`来让机器人连接到NapCat服务器：

```csharp
await bot.StartAsync();
```

### 监听事件

项目内使用一个静态的`BotEventHandler`来处理所有的事件,您可以使用如下方式来监听消息事件：

```csharp
//这个是框架内置的日志处理事件，您可以通过监听它来获取日志输出，并搭配您自己的日志系统使用
BotEventHandler.OnLogReceived += (level, message) =>
{
    //示例：忽略Debug日志
    if (level == LogLevel.Debug)
    {
        return;
    }
    Console.WriteLine($"[{level}] {message}");
};

//监听机器人自身发送群消息事件
BotEventHandler.OnMessageSentGroup += (eventData) =>
{
    Console.WriteLine($"消息类型 {eventData.MessageType}, 消息ID: {eventData.MessageId}");
};
```

具体的可订阅事件请查看文档(WIP)

### 发送请求

发送请求可通过调用机器人实例的内部方法来处理，例如发送群消息：

```csharp
var res  = await bot.SendGroupMessageAsync(new GroupMessageSend()
{
    GroupId = "114514", //群号
    Message = message //List<MessageBase>消息，稍后会介绍如何构建
});
```

> 简述一下WebSocket环境下的处理方式
> 
> 发出请求时，Echo字段会随机生成一个Guid字符串，发送到NapCat服务器
> 
> 服务器处理完请求后，会将结果通过WebSocket返回，并携带相同的Echo字段，此时将其放入`ApiHandler`的类消息队列中（本质是个字典）
> 
> 调用`SendGroupMessageAsync`时会等待对应Echo的结果返回，从而实现异步等待
> 
> 如果在规定时间内没有收到结果，则会抛出超时异常
> 
> 以下是实现的部分代码 ~~(抛出这部分代码纯粹是因为我菜，想问问各位大佬有没有什么更高效的方案)~~
> ```csharp
>   public async Task<GroupMessageSendResponseData> SendGroupMessageAsync(GroupMessageSend groupMessage)
>   {
>        if (groupMessage is null) throw new ArgumentNullException(nameof(groupMessage));
>        
>        var echo = Guid.NewGuid().ToString();
>        await _connection.SendMessageAsync(ApiActionType.SendGroupMsg, groupMessage, echo);
>        //默认15秒超时
>        var timeout = TimeSpan.FromSeconds(15);
>        var start = DateTime.UtcNow;
>
>        while (DateTime.UtcNow - start < timeout)
>        {
>            //其实是个生产消费模型
>            if (ApiHandler.TryConsume(echo, out var response))
>            {
>                if (response.RetCode != 0)
>                {
>                    throw new InvalidOperationException($"send_group_msg failed: {response.RetCode} - {response.Message}");
>                }
>                var data = response.GetData<GroupMessageSendResponseData>();
>                if (data != null)
>                {
>                    return data;
>                }
>                throw new InvalidOperationException("Failed to parse send_group_msg response data.");
>            }
>            //避免过于频繁的轮询
>            await Task.Delay(50);
>        }
>
>        throw new TimeoutException("Timed out waiting for send_group_msg response.");
>    }
> ```

目前请求还没有写多少，仅支持发送消息和戳一戳，后续会慢慢补充

### 构建消息
消息的构建使用了一套基于继承的模型，所有消息类型都继承自`MessageBase`，您可以通过组合不同的消息类型来构建复杂的消息。
您可以使用`MessageChainBuilder`来简化消息的构建过程：

```csharp
var builder = MessageChainBuilder.Create()
    .AddMentionMessage("1145141919") //艾特用户
    .AddTextMessage("请输入文本"); //添加文本消息
var message = builder.Build();
```

同时，对于图片消息，您可以通过传入以下内容来实现
- 本地文件路径
- 网络图片URL
- Base64字符串
- FileStream

这里演示一下FileStream的用法：

```csharp
var imagePath = Path.Combine(AppContext.BaseDirectory, "image.png");
if (File.Exists(imagePath))
{
    using var fs = File.OpenRead(imagePath);
    builder.AddImageMessage(fs); //fs在使用后不会被关闭
}
```

### One more thing

在使用控制台程序时，别忘记卡住，否则程序会直接退出：

```csharp
try
{
    await Task.Delay(Timeout.Infinite, cts.Token);
}
catch (TaskCanceledException)
{
}
```

## 贡献
欢迎任何形式的贡献！无论是报告问题、提出功能请求，还是直接提交代码，我们都非常感谢您的参与

请确保在提交代码之前进行测试，并遵循项目的编码规范

本项目采用.NET 9.0进行开发，请确保您的开发环境已正确配置

## 许可证

本项目采用Apache-2.0许可证，详情请参阅[LICENSE](LICENSE)文件。

## 特别鸣谢

感谢以下项目和资源的支持：

- [NapCat](https://github.com/NapNeko/NapCatQQ)
  - 感谢猫猫，猫猫可爱捏.png

- [TouchSocket](https://touchsocket.net/)
  - 解决了网络通信部分的难题，让我能更专注于写~~石山~~代码

- [NapCatSharpLib](https://github.com/rurisilent/NapCatSharpLib)
  - 前辈项目，提供了一些灵感和参考

## 联系方式

- 邮箱：stevesensei@stu.qlu.edu.cn

其他的之后再补充