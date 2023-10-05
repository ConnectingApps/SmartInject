# SmartInject

## Introduction

SmartInject is a package to simplify dependency injection in .NET . For example, do you know how to deal with circular depencies? You may want to consider a redesign of your application. However, there is a quicker way. Moreover, do you know how to change your health check to ensure you'll get a detailed json formatted health status instead of just `Healthy` or `Unhealthy`? With SmartInject, just a single line of code needs to be changed.

- [SmartInject](#smartinject)
  - [Introduction](#introduction)
  - [Lazy Injection](#lazy-injection)
  - [Health Check Result in Json](#health-check-result-in-json)

## Lazy Injection

Assume we have two classes. Instances of these classes need to communicate with each other without knowing each other. You may have experienced this Exception:

> **System.InvalidOperationException:** A circular dependency was detected for the service of type ...

To solve this problem, the code will be like this:

```csharp
public class Something : ISomething
{
    private readonly Lazy<ISomethingElse> _somethingElse;
    public Something(Lazy<ISomethingElse> somethingElse)
    {
        _somethingElse = somethingElse;
    }
}
```

and

```csharp
public class SomethingElse : ISomethingElse
{
    private readonly Lazy<ISomething> _something;
    public SomethingElse(Lazy<ISomething> something)
    {
        _something = something;
    }
}
```

All you need to do now is to add the collowing line of code to your `Program.cs`

```csharp
builder.Services.AddLazySingleton<ISomething, Something>();
```

Alternatively, you can use `AddLazyTransient` or `AddLazyScoped` instead.
Do not forget use the following namespace line: 

```csharp
using ConnectingApps.SmartInject;
```

## Health Check Result in Json

When adding a health check to your .NET application, you typically get a result like `Healthy` after doing a request.
The response is not very detailed, which can be a problem when there is something wrong.

However, instead of getting `Healthy` as a response, you can get a detailed json response instead:

```json
{
    "status": "Healthy",
    "results": {
        "ExampleHealthCheck": {
            "status": "Healthy",
            "description": "Example health check is healthy",
            "data": {
            "exampleDataKey": "exampleDataValue"
            }
        }
    }
}
```

Achieving this is very simple. Instead of adding a health check like this:

```csharp
app.MapHealthChecks("/healthz");
```

you'll add a health check like this:

```csharp
app.MapHealthChecks("/healthz", new HealthCheckOptions
{
    ResponseWriter = HealthCheckResponseWriters.WriteJsonResponse
});
```

with this namespace added:

```csharp
using ConnectingApps.SmartInject;
```