# SmartInject

SmartInject is a package to simplify dependency injection in .NET . For example, do you know how to deal with circular depencies? You may want to consider a redesign of your application. However, there is a quicker way:

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