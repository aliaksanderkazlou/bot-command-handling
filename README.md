# Bot command handling
.Net Library to speed up your bot development

You will no longer have to care about the infrastructure. All you should care about - business logic.

# Usage

Specify the request and response models:
```
public class Request
{
  public long ChatId { get; set; }
  ...
  public string Text { get; set; }
}

public class Response
{
  public string Text { get; set; }
}
```

Register in the DI:
```
public void ConfigureServices(IServiceCollection services)
{
  ...
  services.AddCommandHandlingDependencies<Request, Response>();
  ...
}
```

Create the command handler:
```
[CommandHandler("/start")]
public class StartCommandHandler : ICommandHandler<Request, Response>
{
  ...

  public async Task<Response> HandleAsync(Request request)
  {
    ...
  }
}
```

If you will need context logic in your bot, please use the `IContextCommandHandler`:
```
public static class UserStatuses
{
  public const string WaitingForUserAction = "WaitingForUserAction";
}

[CommandHandler("/action", new[] { UserStatuses.WaitingForUserAction })]
public class ActionCommandHandler : IContextCommandHandler<Request, Response>
{
  ...

  public async Task<Response> HandleAsync(Request request)
  {
    ...
  }

  public async Task<Response> HandleContextAsync(Request request)
  {
    ...
  }
}
```

To use the command handling, please, inject `ICommandHandlingFactory`:
```
public class CommandHandler : IMainCommandHandler
{
  private readonly ICommandHandlerFactory<Request, Response> _factory;

  ...

  public async Task<Response> HandleAsync(Request request)
  {
    var action = status is null || request.Text.StartsWith('/')
      ? HandleMainCommandAsync(request)
      : HandleUserPendingActionAsync(request, status);

    try
    {
      return await action;
    }
    catch (CommandHandlingException exception)
    {
      switch (exception.Type)
      {
        case CommandHandlingExceptionType.HandlerNotFound:
          return ...
        default:
          throw new NotSupportedException($"Exception type {exception.Type} is not supported");
      }
    }
  }

  private async Task<Response> HandleMainCommandAsync(Request request)
  {
    return await _factory.GetByCommand(request.Text.Split(" ")[0]).HandleAsync(request);
  }
  
  private async Task<Response> HandleUserPendingActionAsync(Request request, string status)
  {
    return await _factory.GetByContextStatus(status).HandleContextAsync(request);
  }
}
```


