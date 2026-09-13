</div align="center">

# Jitest

JIT-time test method override = JIT-test

</div>

Jitest is an experimental C# library for replacing method behavior entirely from test code. It keeps test-only abstractions such as interfaces, `virtual` members, and time providers out of production design.

```cs
using var _ = typeof(Stopwatch)
    .Jitest<Func<long, TimeSpan>>(nameof(Stopwatch.GetElapsedTime), out _)
    .Intercept(static (long _) => new TimeSpan(9, 8, 7, 6, 5, 4));
```

## Motivation

Production code should not need to know how it will be tested. `Jitest` lets a test depend explicitly on implementation details and replace behavior only for the lifetime of that test scope.

This avoids changing production code solely to make a test possible, for example by:

- Adding an interface
- Making a member `virtual`
- Removing `sealed` from a class
- Passing a time or environment abstraction through the application

## Supported frameworks

- .NET 5 or later
- .NET Standard 2.1

# Basic Usage

## API overview

- `typeof(Target).Jitest<TDelegate>(...)` intercepts a static method.
- `instance.Jitest<TDelegate>(...)` intercepts an instance method.
- Methods are resolved by exact name and signature across the type hierarchy, regardless of visibility.
- `TDelegate` describes the explicit parameters and return type of the target method.
    - A replacement for an instance method adds a compatible declaring-type parameter at the beginning.
- The `out` argument of `Intercept` receives a clone of the original method that bypasses the hook.
- Use with `using` or dispose `DetourScope` returned by `Intercept` to remove the hook.

## Test Setup Requirements

### Global test initialization

Install all required interceptions once during the test framework's global initialization. Create a dedicated instance for each fixed behavior, such as `AlwaysNotFoundHttpClient` and `AlwaysTimeoutHttpClient`, and keep the interceptions unchanged until global cleanup.

Tests can then run concurrently because they only select a preconfigured instance and never replace method behavior while tests are running. Create every required instance and interception scope before parallel test execution starts, and dispose them only after all tests have finished.

### Serializing test methods

Alternatively, acquire a `JitestContext` scope once at the beginning of each participating test method. The scope holds a global semaphore until it is disposed, preventing those test methods from running concurrently even when interception begins later in the test or is not used at all.

```cs
[Fact]
public void ExampleTest()
{
    using var testMethodScope = JitestContext.BeginTestMethod();

    // Arrange, intercept if required, act, and assert.
}
```

Every test that can reach an intercepted method must follow this convention. Calling `BeginTestMethod` only from tests that use `Intercept` does not protect other tests from a process-wide hook.

## Intercepting Methods

`Jitest` deliberately moves the testing dependency into the test instead of changing production architecture for testability. Use it only where the runtime-wide impact of the selected method can be controlled.

- Static BCL methods have an unpredictable impact radius and are generally unsuitable targets. Prefer a method owned by the tested code whose callers are known, such as an internal or private `IsTimedOut` method.
- Instance methods on value types are not supported.
- Constructors, abstract methods, and open generic methods are not supported.
- Target and replacement parameter and return types must match (TODO: strongly-typed interceptor).

### Static methods

```cs
using var _ = typeof(Stopwatch)
    .Jitest<Func<long, TimeSpan>>(nameof(Stopwatch.GetElapsedTime), out _)
    .Intercept(static (long _) => new TimeSpan(9, 8, 7, 6, 5, 4));
```

Within `using` scope, `Stopwatch.GetElapsedTime(long)` always returns *9 days, 8 hours, 7 minutes, 6 seconds, 5 milliseconds, and 4 microseconds*.

### Instance methods

For an instance method, the replacement delegate must explicitly include the target instance as its first argument. This is the hidden argument that C# normally inserts at the call site.

For example, `HttpClient.GetAsync(string)` replacement will look like:

```cs
using var _ = client.Jitest<...>(...)
    // Requires a hidden first argument of HttpClient
    .Intercept((HttpClient instance, string url) => { ... };
```

### Restricting behavior to one instance

> [!WARNING]
> Passing an instance to `Jitest` selects the instance method to intercept; it does not target only that instance (TODO). The hook affects the same method on every instance. Use the replacement delegate's first argument to select the intended instance explicitly.

Use the cloned original method returned by `Jitest` and filter the target inside the replacement. Do not call the intercepted method directly: that would enter the hook again and cause stack overflow.

```cs
using var targetClient = new HttpClient();

using var _ = targetClient
    .Jitest<Func<string, Task<HttpResponseMessage>>>(nameof(HttpClient.GetAsync), out var originalGetAsync)
    .Intercept((HttpClient instance, string requestUri) =>
    {
        // The hook is process-wide, so explicitly forward calls for every other instance.
        return ReferenceEquals(instance, targetClient)
            ? Task.FromResult(new HttpResponseMessage(HttpStatusCode.NotExtended)
            {
                Version = new Version(310, 42),
            })
            : originalGetAsync.Invoke(requestUri);
    });

    // BAD EXAMPLE
    // This affects every call to GetAsync in the process, not only calls made through client.
    .Intercept(static (HttpClient _, string _) =>
        Task.FromResult(new HttpResponseMessage(HttpStatusCode.NotExtended)
        {
            Version = new Version(310, 42),
        }));
```
