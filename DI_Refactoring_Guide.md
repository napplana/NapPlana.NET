# NapPlana.NET 依赖注入(DI)改造指南

## 📋 目录
- [项目分析](#项目分析)
- [是否适合添加DI](#是否适合添加di)
- [改造方案](#改造方案)
- [具体实施步骤](#具体实施步骤)
- [代码示例](#代码示例)
- [注意事项](#注意事项)

---

## 📊 项目分析

### 当前项目结构
NapPlana.NET 是一个基于 .NET 9.0 的 NapCat 框架 SDK，主要包含以下组件：

#### 核心组件
- **Bot层**: `NapBot`, `PlanaBotFactory` - 机器人主体和工厂模式创建
- **连接层**: `ConnectionBase`, `WebsocketClientConnection` - WebSocket连接管理
- **事件处理层**: `BotEventHandler`, `RootEventParser` - 静态事件处理器
- **API层**: `ApiHandler` - 静态API响应处理器
- **插件层**: `WebSocketMessageReceiverPlugin`, `WebSocketAuthPlugin` - TouchSocket插件

#### 当前架构特点
1. ✅ **工厂模式**: 使用 `PlanaBotFactory` 创建Bot实例
2. ⚠️ **静态类滥用**: `BotEventHandler` 和 `ApiHandler` 使用静态类和静态事件
3. ⚠️ **硬编码依赖**: 插件中直接引用静态类(`BotEventHandler`, `ApiHandler`)
4. ⚠️ **配置管理**: Example中使用 `IConfiguration`，但Core层未集成
5. ✅ **异步支持**: 良好的异步编程支持
6. ⚠️ **生命周期管理**: 手动管理连接生命周期

---

## ✅ 是否适合添加DI

### **结论: 非常适合！**

### 适合的理由

#### 1. **架构复杂度已达到临界点**
- 多层架构(Bot → Connection → Plugin → Handler)
- 组件间存在复杂依赖关系
- 静态类导致测试困难和扩展性差

#### 2. **现有痛点明显**
```csharp
// ❌ 当前问题示例
public class WebSocketMessageReceiverPlugin: PluginBase
{
    private readonly RootEventParser _parser = new(); // 硬编码创建
    
    public Task OnWebSocketReceived(IWebSocket webSocket, WSDataFrameEventArgs e)
    {
        BotEventHandler.LogReceived(...);  // 静态依赖
        ApiHandler.Dispatch(...);           // 静态依赖
    }
}
```

**问题**:
- 无法替换 `RootEventParser` 实现
- 无法单元测试(依赖静态类)
- 无法在多Bot场景下隔离状态
- 配置修改需要重新编译

#### 3. **目标用户场景需要**
- **SDK场景**: 用户需要灵活配置和扩展
- **多实例需求**: 可能需要运行多个Bot实例
- **测试友好**: SDK应该易于测试

#### 4. **.NET生态支持**
- ✅ 已使用 .NET 9.0
- ✅ Example项目已使用 `Microsoft.Extensions.Configuration`
- ✅ 可无缝集成 `Microsoft.Extensions.DependencyInjection`
- ✅ 支持 `IHostedService` 模式

---

## 🎯 改造方案

### 设计目标
1. **向后兼容**: 保留现有简单工厂API，同时提供DI扩展
2. **灵活性**: 支持自定义实现替换
3. **可测试性**: 所有组件可注入Mock
4. **生命周期管理**: 利用IHostedService自动管理
5. **配置驱动**: 支持Options模式配置

### 推荐架构

```
┌─────────────────────────────────────────┐
│         用户应用层 (Example)              │
│  - Program.cs with Host Builder         │
│  - appsettings.json                     │
└────────────────┬────────────────────────┘
                 │
                 │ 注册服务
                 ▼
┌─────────────────────────────────────────┐
│      DependencyInjection 扩展层          │
│  - ServiceCollectionExtensions          │
│  - NapBotOptions (Options模式)          │
│  - NapBotHostedService                  │
└────────────────┬────────────────────────┘
                 │
                 │ 创建和管理
                 ▼
┌─────────────────────────────────────────┐
│          Core 服务层                     │
│  - INapBot / NapBot                     │
│  - IEventHandler / EventHandler         │
│  - IApiHandler / ApiHandler             │
│  - IEventParser / RootEventParser       │
└────────────────┬────────────────────────┘
                 │
                 │ 使用
                 ▼
┌─────────────────────────────────────────┐
│        Connection & Plugin 层           │
│  - ConnectionBase (注入依赖)            │
│  - Plugins (注入服务)                   │
└─────────────────────────────────────────┘
```

---

## 🔧 具体实施步骤

### Phase 1: 接口抽象 (不破坏现有代码)

#### 1.1 创建核心接口

```csharp
// NapPlana.Net.Core/Bot/INapBot.cs
public interface INapBot
{
    long SelfId { get; }
    Task StartAsync(CancellationToken cancellationToken = default);
    Task StopAsync(CancellationToken cancellationToken = default);
    // ... 其他公共API
}
```

```csharp
// NapPlana.Net.Core/Event/Handler/IEventHandler.cs
public interface IEventHandler
{
    event Action<LogLevel, string>? OnLogReceived;
    event Action? OnBotConnected;
    event Action<HeartBeatEvent>? OnBotHeartbeat;
    // ... 其他事件
    
    void LogReceived(LogLevel logLevel, string message);
    void BotConnected();
    // ... 其他方法
}
```

```csharp
// NapPlana.Net.Core/API/IApiHandler.cs
public interface IApiHandler
{
    bool TryRegister(string echo, TaskCompletionSource<ActionResponse> tcs);
    bool TryRemove(string echo, out TaskCompletionSource<ActionResponse>? tcs);
    void Dispatch(ActionResponse raw);
}
```

```csharp
// NapPlana.Net.Core/Event/Parser/IEventParser.cs
public interface IEventParser
{
    void ParseEvent(string jsonText);
}
```

#### 1.2 改造现有类实现接口

```csharp
// 保留静态类作为默认单例实现
public class EventHandler : IEventHandler
{
    // 实例成员
    public event Action<LogLevel, string>? OnLogReceived;
    // ...
    
    public void LogReceived(LogLevel logLevel, string message)
    {
        OnLogReceived?.Invoke(logLevel, message);
    }
}

// 保留静态门面供现有代码使用
public static class BotEventHandler
{
    private static IEventHandler _instance = new EventHandler();
    
    public static event Action<LogLevel, string>? OnLogReceived
    {
        add => _instance.OnLogReceived += value;
        remove => _instance.OnLogReceived -= value;
    }
    
    public static void LogReceived(LogLevel logLevel, string message)
        => _instance.LogReceived(logLevel, message);
        
    // 供DI使用：设置实例
    internal static void SetInstance(IEventHandler instance)
        => _instance = instance;
}
```

### Phase 2: Options模式配置

#### 2.1 创建配置类

```csharp
// NapPlana.Net.Core/DependencyInjection/NapBotOptions.cs
namespace NapPlana.Core.DependencyInjection;

public class NapBotOptions
{
    public const string SectionName = "NapBot";
    
    /// <summary>
    /// 机器人QQ号
    /// </summary>
    public long SelfId { get; set; }
    
    /// <summary>
    /// 连接类型
    /// </summary>
    public BotConnectionType ConnectionType { get; set; } = BotConnectionType.WebSocketClient;
    
    /// <summary>
    /// NapCat服务器IP
    /// </summary>
    public string Ip { get; set; } = "127.0.0.1";
    
    /// <summary>
    /// NapCat服务器端口
    /// </summary>
    public int Port { get; set; }
    
    /// <summary>
    /// 访问令牌(可选)
    /// </summary>
    public string? Token { get; set; }
    
    /// <summary>
    /// 是否自动启动
    /// </summary>
    public bool AutoStart { get; set; } = true;
    
    /// <summary>
    /// API超时时间(秒)
    /// </summary>
    public int ApiTimeout { get; set; } = 15;
}
```

#### 2.2 更新appsettings.json示例

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "NapPlana": "Debug"
    }
  },
  "NapBot": {
    "SelfId": 123456789,
    "ConnectionType": "WebSocketClient",
    "Ip": "127.0.0.1",
    "Port": 3001,
    "Token": "your_token_here",
    "AutoStart": true,
    "ApiTimeout": 15
  }
}
```

### Phase 3: DI扩展方法

#### 3.1 创建服务注册扩展

```csharp
// NapPlana.Net.Core/DependencyInjection/ServiceCollectionExtensions.cs
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace NapPlana.Core.DependencyInjection;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// 添加NapBot服务
    /// </summary>
    public static IServiceCollection AddNapBot(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // 绑定配置
        services.Configure<NapBotOptions>(
            configuration.GetSection(NapBotOptions.SectionName));
        
        // 注册核心服务
        services.AddSingleton<IEventHandler, EventHandler>();
        services.AddSingleton<IApiHandler, ApiHandler>();
        services.AddSingleton<IEventParser, RootEventParser>();
        
        // 注册连接工厂
        services.AddSingleton<IConnectionFactory, ConnectionFactory>();
        
        // 注册Bot (Scoped用于支持多实例，Singleton用于单实例)
        services.AddSingleton<INapBot, NapBot>();
        
        return services;
    }
    
    /// <summary>
    /// 添加NapBot服务并注册为HostedService
    /// </summary>
    public static IServiceCollection AddNapBotHostedService(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddNapBot(configuration);
        services.AddHostedService<NapBotHostedService>();
        
        return services;
    }
    
    /// <summary>
    /// 添加NapBot服务 (委托配置)
    /// </summary>
    public static IServiceCollection AddNapBot(
        this IServiceCollection services,
        Action<NapBotOptions> configureOptions)
    {
        services.Configure(configureOptions);
        
        services.AddSingleton<IEventHandler, EventHandler>();
        services.AddSingleton<IApiHandler, ApiHandler>();
        services.AddSingleton<IEventParser, RootEventParser>();
        services.AddSingleton<IConnectionFactory, ConnectionFactory>();
        services.AddSingleton<INapBot, NapBot>();
        
        return services;
    }
}
```

### Phase 4: HostedService实现

#### 4.1 创建HostedService

```csharp
// NapPlana.Net.Core/DependencyInjection/NapBotHostedService.cs
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace NapPlana.Core.DependencyInjection;

/// <summary>
/// NapBot后台服务，负责Bot的生命周期管理
/// </summary>
public class NapBotHostedService : IHostedService
{
    private readonly INapBot _bot;
    private readonly ILogger<NapBotHostedService> _logger;
    private readonly NapBotOptions _options;

    public NapBotHostedService(
        INapBot bot,
        IOptions<NapBotOptions> options,
        ILogger<NapBotHostedService> logger)
    {
        _bot = bot;
        _options = options.Value;
        _logger = logger;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("NapBot服务正在启动...");
        
        if (_options.AutoStart)
        {
            try
            {
                await _bot.StartAsync(cancellationToken);
                _logger.LogInformation("NapBot已成功启动，QQ: {SelfId}", _options.SelfId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "NapBot启动失败");
                throw;
            }
        }
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("NapBot服务正在停止...");
        
        try
        {
            await _bot.StopAsync(cancellationToken);
            _logger.LogInformation("NapBot已安全停止");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "NapBot停止时出错");
        }
    }
}
```

### Phase 5: 改造Plugin层

#### 5.1 插件支持依赖注入

```csharp
// NapPlana.Net.Core/Connections/Plugins/WebSocketMessageReceiverPlugin.cs
public class WebSocketMessageReceiverPlugin : PluginBase, IWebSocketReceivedPlugin
{
    private readonly IEventParser _parser;
    private readonly IEventHandler _eventHandler;
    private readonly IApiHandler _apiHandler;

    // 支持DI构造
    public WebSocketMessageReceiverPlugin(
        IEventParser parser,
        IEventHandler eventHandler,
        IApiHandler apiHandler)
    {
        _parser = parser;
        _eventHandler = eventHandler;
        _apiHandler = apiHandler;
    }
    
    // 保留无参构造函数以向后兼容
    public WebSocketMessageReceiverPlugin()
        : this(
            new RootEventParser(),
            BotEventHandler._instance,  // 使用静态实例
            ApiHandler._instance)
    {
    }

    public Task OnWebSocketReceived(IWebSocket webSocket, WSDataFrameEventArgs e)
    {
        var text = e.DataFrame.ToText();
        _eventHandler.LogReceived(LogLevel.Debug, $"接收到消息: {text}");
        
        if (!string.IsNullOrWhiteSpace(text))
        {
            try
            {
                using var doc = JsonDocument.Parse(text);
                if (doc.RootElement.TryGetProperty("retcode", out _))
                {
                    var actionResponse = JsonSerializer.Deserialize<ActionResponse>(text);
                    if (actionResponse != null)
                    {
                        if (actionResponse.RetCode != 0)
                        {
                            _eventHandler.LogReceived(LogLevel.Error,
                                $"动作失败: {actionResponse.RetCode} - {actionResponse.Message}");
                        }
                        _apiHandler.Dispatch(actionResponse);
                        return EasyTask.CompletedTask;
                    }
                }
            }
            catch (Exception ex)
            {
                _eventHandler.LogReceived(LogLevel.Debug,
                    $"解析动作响应时出现异常(忽略): {ex.Message}");
            }
        }
        
        _parser.ParseEvent(text ?? "");
        return EasyTask.CompletedTask;
    }
}
```

### Phase 6: 改造Bot和Connection

#### 6.1 NapBot使用注入的服务

```csharp
// NapPlana.Net.Core/Bot/NapBot.cs
public class NapBot : INapBot
{
    private readonly IConnectionBase _connection;
    private readonly IEventHandler _eventHandler;
    private readonly IApiHandler _apiHandler;
    private readonly NapBotOptions _options;

    public long SelfId { get; }

    // DI构造函数
    public NapBot(
        IConnectionFactory connectionFactory,
        IEventHandler eventHandler,
        IApiHandler apiHandler,
        IOptions<NapBotOptions> options)
    {
        _options = options.Value;
        _eventHandler = eventHandler;
        _apiHandler = apiHandler;
        
        SelfId = _options.SelfId;
        _connection = connectionFactory.CreateConnection(_options);
    }
    
    // 保留旧构造函数以向后兼容
    public NapBot(ConnectionBase connection, long selfId)
    {
        _connection = connection;
        _eventHandler = BotEventHandler._instance;
        _apiHandler = ApiHandler._instance;
        SelfId = selfId;
        _options = new NapBotOptions { SelfId = selfId };
    }

    public Task StartAsync(CancellationToken cancellationToken = default)
        => _connection.InitializeAsync();

    public Task StopAsync(CancellationToken cancellationToken = default)
        => _connection.ShutdownAsync();

    // ... 其他方法使用注入的服务
}
```

#### 6.2 ConnectionFactory

```csharp
// NapPlana.Net.Core/Connections/IConnectionFactory.cs
public interface IConnectionFactory
{
    IConnectionBase CreateConnection(NapBotOptions options);
}

// NapPlana.Net.Core/Connections/ConnectionFactory.cs
public class ConnectionFactory : IConnectionFactory
{
    private readonly IServiceProvider _serviceProvider;

    public ConnectionFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IConnectionBase CreateConnection(NapBotOptions options)
    {
        return options.ConnectionType switch
        {
            BotConnectionType.WebSocketClient => 
                ActivatorUtilities.CreateInstance<WebsocketClientConnection>(
                    _serviceProvider, options.Ip, options.Port, options.Token),
            _ => throw new NotSupportedException($"不支持的连接类型: {options.ConnectionType}")
        };
    }
}
```

### Phase 7: 更新Example项目

#### 7.1 使用Host Builder模式

```csharp
// NapPlana.Net.Example/Program.cs
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NapPlana.Core.DependencyInjection;
using NapPlana.Core.Event.Handler;
using NapPlana.Core.Bot;
using NapPlana.Example.Examples;

var builder = Host.CreateApplicationBuilder(args);

// 配置日志
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// 添加NapBot服务 (带HostedService)
builder.Services.AddNapBotHostedService(builder.Configuration);

// 注册示例服务
builder.Services.AddSingleton<IExample, PokeBack>();
builder.Services.AddSingleton<IExample, NeteaseVoice>();

var host = builder.Build();

// 设置事件处理器
var eventHandler = host.Services.GetRequiredService<IEventHandler>();
var logger = host.Services.GetRequiredService<ILogger<Program>>();

eventHandler.OnLogReceived += (level, message) =>
{
    var logLevel = level switch
    {
        NapPlana.Core.Data.LogLevel.Debug => Microsoft.Extensions.Logging.LogLevel.Debug,
        NapPlana.Core.Data.LogLevel.Info => Microsoft.Extensions.Logging.LogLevel.Information,
        NapPlana.Core.Data.LogLevel.Warning => Microsoft.Extensions.Logging.LogLevel.Warning,
        NapPlana.Core.Data.LogLevel.Error => Microsoft.Extensions.Logging.LogLevel.Error,
        _ => Microsoft.Extensions.Logging.LogLevel.Information
    };
    logger.Log(logLevel, message);
};

// 配置事件处理
var bot = host.Services.GetRequiredService<INapBot>();
eventHandler.OnGroupPokeNoticeReceived += async (notice) =>
{
    if (notice.UserId == bot.SelfId) return;
    
    var pokeBack = host.Services.GetRequiredService<PokeBack>();
    await pokeBack.ExecuteAsyncGroup(bot, notice.GroupId.ToString(), notice.UserId.ToString());
};

// 初始化其他示例
foreach (var example in host.Services.GetServices<IExample>())
{
    await example.InitializeAsync(bot);
}

// 运行Host (自动处理生命周期)
await host.RunAsync();
```

#### 7.2 更新appsettings.json

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.Hosting.Lifetime": "Information",
      "NapPlana": "Debug"
    }
  },
  "NapBot": {
    "SelfId": 123456789,
    "ConnectionType": "WebSocketClient",
    "Ip": "127.0.0.1",
    "Port": 3001,
    "Token": "",
    "AutoStart": true,
    "ApiTimeout": 15
  }
}
```

---

## 💻 代码示例

### 使用方式对比

#### 旧方式 (保持兼容)
```csharp
var bot = PlanaBotFactory
    .Create()
    .SetSelfId(123456789)
    .SetConnectionType(BotConnectionType.WebSocketClient)
    .SetIp("127.0.0.1")
    .SetPort(3001)
    .Build();

await bot.StartAsync();
```

#### 新方式1: 简单DI
```csharp
var services = new ServiceCollection();
services.AddNapBot(options =>
{
    options.SelfId = 123456789;
    options.Ip = "127.0.0.1";
    options.Port = 3001;
});

var provider = services.BuildServiceProvider();
var bot = provider.GetRequiredService<INapBot>();
await bot.StartAsync();
```

#### 新方式2: 配置文件 + HostedService
```csharp
var host = Host.CreateApplicationBuilder(args)
    .Services
    .AddNapBotHostedService(builder.Configuration)
    .Build();

await host.RunAsync(); // 自动启动和停止
```

#### 新方式3: ASP.NET Core集成
```csharp
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddNapBot(builder.Configuration);
builder.Services.AddControllers();

var app = builder.Build();

var bot = app.Services.GetRequiredService<INapBot>();
await bot.StartAsync();

app.MapControllers();
await app.RunAsync();
```

### 测试友好示例

```csharp
// 单元测试 - Mock依赖
[Fact]
public async Task WebSocketPlugin_Should_Parse_Events()
{
    // Arrange
    var mockParser = new Mock<IEventParser>();
    var mockEventHandler = new Mock<IEventHandler>();
    var mockApiHandler = new Mock<IApiHandler>();
    
    var plugin = new WebSocketMessageReceiverPlugin(
        mockParser.Object,
        mockEventHandler.Object,
        mockApiHandler.Object);
    
    // Act
    var eventArgs = CreateMockEventArgs("{\"post_type\":\"message\"}");
    await plugin.OnWebSocketReceived(null, eventArgs);
    
    // Assert
    mockParser.Verify(p => p.ParseEvent(It.IsAny<string>()), Times.Once);
}
```

---

## ⚠️ 注意事项

### 1. 向后兼容性
- ✅ 保留所有现有API
- ✅ 静态类继续工作
- ✅ 工厂模式继续可用
- ⚠️ 新功能优先使用DI方式

### 2. 迁移策略

#### 渐进式迁移
```
Phase 1: 创建接口和DI扩展 (不影响现有代码)
  ↓
Phase 2: 文档和示例更新
  ↓
Phase 3: 标记静态类为 [Obsolete] (给出迁移提示)
  ↓
Phase 4: 下一个大版本移除静态类
```

#### 版本规划
- **v0.1.x**: 添加DI支持，保持完全兼容
- **v0.2.x**: 推荐DI方式，静态类标记为过时
- **v1.0.0**: 仅保留DI方式 (破坏性更改)

### 3. 多实例支持

```csharp
// 支持运行多个Bot实例
builder.Services.AddNapBot("bot1", config.GetSection("Bot1"));
builder.Services.AddNapBot("bot2", config.GetSection("Bot2"));

// 使用命名实例
var bot1 = provider.GetRequiredService<INapBot>("bot1");
var bot2 = provider.GetRequiredService<INapBot>("bot2");
```

### 4. 性能考虑
- DI容器创建开销极小
- 单例生命周期避免重复创建
- 异步方法不会因DI增加延迟

### 5. 第三方集成
- ✅ 可集成 Serilog、NLog等日志库
- ✅ 可集成 Polly 进行重试和熔断
- ✅ 可集成 HealthChecks 监控
- ✅ 可集成 OpenTelemetry 追踪

---

## 📦 NuGet包依赖

需要添加的包:
```xml
<PackageReference Include="Microsoft.Extensions.DependencyInjection" Version="9.0.0" />
<PackageReference Include="Microsoft.Extensions.Hosting" Version="9.0.0" />
<PackageReference Include="Microsoft.Extensions.Options" Version="9.0.0" />
<PackageReference Include="Microsoft.Extensions.Options.ConfigurationExtensions" Version="9.0.0" />
<PackageReference Include="Microsoft.Extensions.Logging.Abstractions" Version="9.0.0" />
```

Example项目:
```xml
<PackageReference Include="Microsoft.Extensions.Hosting" Version="9.0.0" />
<PackageReference Include="Microsoft.Extensions.Logging.Console" Version="9.0.0" />
```

---

## 🎓 最佳实践

### 1. 服务生命周期选择
```csharp
// Singleton: 全局唯一，线程安全
services.AddSingleton<IEventHandler, EventHandler>();
services.AddSingleton<IApiHandler, ApiHandler>();

// Scoped: 每个作用域一个实例 (HTTP请求、手动作用域)
services.AddScoped<IMessageHandler, MessageHandler>();

// Transient: 每次请求创建新实例
services.AddTransient<IEventParser, RootEventParser>();
```

### 2. 配置验证
```csharp
services.AddOptions<NapBotOptions>()
    .Bind(configuration.GetSection(NapBotOptions.SectionName))
    .ValidateDataAnnotations()
    .Validate(options =>
    {
        return options.SelfId > 0;
    }, "SelfId必须大于0");
```

### 3. 日志集成
```csharp
public class EventHandler : IEventHandler
{
    private readonly ILogger<EventHandler> _logger;
    
    public EventHandler(ILogger<EventHandler> logger)
    {
        _logger = logger;
    }
    
    public void LogReceived(LogLevel level, string message)
    {
        _logger.Log(ConvertLogLevel(level), message);
        OnLogReceived?.Invoke(level, message);
    }
}
```

---

## 📚 参考资料

### Microsoft官方文档
- [依赖注入](https://learn.microsoft.com/zh-cn/dotnet/core/extensions/dependency-injection)
- [后台服务](https://learn.microsoft.com/zh-cn/dotnet/core/extensions/hosted-services)
- [Options模式](https://learn.microsoft.com/zh-cn/dotnet/core/extensions/options)
- [配置](https://learn.microsoft.com/zh-cn/dotnet/core/extensions/configuration)

### 社区最佳实践
- [Clean Architecture](https://github.com/jasontaylordev/CleanArchitecture)
- [.NET Microservices](https://learn.microsoft.com/zh-cn/dotnet/architecture/microservices/)

---

## 🗺️ 实施路线图

### Sprint 1 (1-2天)
- [ ] 创建核心接口 (IEventHandler, IApiHandler, etc.)
- [ ] 实现实例版本的Handler类
- [ ] 创建Options类

### Sprint 2 (1-2天)
- [ ] 实现ServiceCollectionExtensions
- [ ] 实现NapBotHostedService
- [ ] 创建ConnectionFactory

### Sprint 3 (2-3天)
- [ ] 改造Plugin层支持DI
- [ ] 改造Bot和Connection层
- [ ] 保持向后兼容的静态门面

### Sprint 4 (1天)
- [ ] 更新Example项目使用Host Builder
- [ ] 编写单元测试
- [ ] 更新文档

### Sprint 5 (1天)
- [ ] 性能测试
- [ ] 集成测试
- [ ] 发布新版本

**总计: 6-9天开发周期**

---

## ✅ 总结

### 核心优势
1. ✨ **灵活性**: 所有组件可替换
2. 🧪 **可测试性**: 完全支持Mock和单元测试
3. 🔧 **可维护性**: 清晰的依赖关系
4. 📦 **可扩展性**: 易于添加新功能
5. 🎯 **标准化**: 遵循.NET生态最佳实践
6. ⚡ **性能**: 无明显性能损失
7. 🔄 **兼容性**: 不破坏现有代码

### 推荐指数: ⭐⭐⭐⭐⭐

**强烈推荐进行DI改造！** 这将使NapPlana.NET成为一个更专业、更易用的SDK。

---

*文档版本: 1.0*  
*创建日期: 2026-02-12*  
*作者: GitHub Copilot*

