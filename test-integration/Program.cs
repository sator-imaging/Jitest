// Licensed under the Apache-2.0 License
// https://github.com/sator-imaging/Jitest

using Jitest;
using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

#pragma warning disable SMA8000 // Literal Should Be Passed as Named Argument
#pragma warning disable SMA7002 // Lambda Allocation
#pragma warning disable SMA0032 // Implicit Boxing Conversion
#pragma warning disable SMA0040 // Missing Using Statement
#pragma warning disable CA1822  // Mark members as static

Console.WriteLine("=== Jitest Integration Test (NuGet Package) ===");
Console.WriteLine();
Console.WriteLine($"  Runtime: {Environment.Version}");
Console.WriteLine();

int failures = 0;

void AssertEqual<T>(T actual, T expected, string testName)
{
    if (Equals(actual, expected))
    {
        Console.WriteLine($"  [PASS] {testName}: {actual}");
    }
    else
    {
        Console.WriteLine($"  [FAIL] {testName}: got {actual}, expected {expected}");
        failures++;
    }
}

Console.WriteLine("--- Test 1: Static Method Interception ---");
{
    using (var jitest = typeof(CalcBase)
        .Jitest<Func<int, int, int>>("SumCore", out _)
        .Intercept(static (int a, int b) => 42))
    {
        AssertEqual(CalcBase.Sum(1, 2), 42, "During intercept");
    }
    AssertEqual(CalcBase.Sum(1, 2), 3, "After dispose");
}

Console.WriteLine();
Console.WriteLine("--- Test 2: Instance Method Interception & Target Filtering ---");
{
    var targetCalc = new Calculator();
    var otherCalc = new CalcBase();

    using (var jitest = targetCalc
        .Jitest<Func<int, int, int>>(nameof(CalcBase.Multiply), out var originalMethod)
        .Intercept((Calculator instance, int a, int b) => instance == targetCalc ? 99 : originalMethod.Invoke(a, b)))
    {
        var targetCC = new CalculatorController(targetCalc);
        var otherCC = new CalculatorController(otherCalc);

        AssertEqual(targetCC.Multiply(3, 4), 99, "Target instance");
        AssertEqual(otherCC.Multiply(3, 4), 12, "Other instance");
    }
    AssertEqual(targetCalc.Multiply(3, 4), 12, "After dispose");
}

#if NET7_0_OR_GREATER
Console.WriteLine();
Console.WriteLine("--- Test 3: Stopwatch Method Interception ---");
{
    var expectedSpan = new TimeSpan(9, 8, 7, 6, 5, 4);
    using (var jitest = typeof(Stopwatch)
        .Jitest<Func<long, TimeSpan>>(nameof(Stopwatch.GetElapsedTime), out _)
        .Intercept((long _) => expectedSpan))
    {
        AssertEqual(Stopwatch.GetElapsedTime(123456789), expectedSpan, "During Stopwatch intercept");
    }
}
#endif

Console.WriteLine();
Console.WriteLine("--- Test 4: HttpClient Method Interception ---");
{
    using var client = new HttpClient();
    using (var jitest = client
        .Jitest<Func<string, Task<HttpResponseMessage>>>(nameof(HttpClient.GetAsync), out _)
        .Intercept(static (HttpClient _, string _) => Task.FromResult(new HttpResponseMessage(HttpStatusCode.NotExtended) { Version = new(310, 42) })))
    {
        var response = client.GetAsync("https://yahoo.co.jp/").Result;
        AssertEqual(response.StatusCode, HttpStatusCode.NotExtended, "HttpClient StatusCode during intercept");
        AssertEqual(response.Version, new Version(310, 42), "HttpClient Version during intercept");
    }
}

Console.WriteLine();
Console.WriteLine("--- Test 5: JitestContext Test Method Scope ---");
{
    using (var scope1 = JitestContext.BeginTestMethod())
    {
        // Trying to acquire on another thread within timeout should wait/fail if locked
        var task = Task.Run(() =>
        {
            using var scope2 = JitestContext.BeginTestMethod();
            return true;
        });

        // Delay brief moment to ensure scope1 holds global semaphore
        bool completed = task.Wait(100);
        AssertEqual(completed, false, "Second thread should be blocked by JitestContext scope lock");
    }

    // After dispose of scope1, acquiring scope2 should succeed
    using (var scope2 = JitestContext.BeginTestMethod())
    {
        AssertEqual(true, true, "JitestContext acquired successfully after release");
    }
}

Console.WriteLine();
Console.WriteLine("--- Test 6: Exception Handling in Intercepted Method ---");
{
    bool exceptionThrown = false;
    using (var jitest = typeof(CalcBase)
        .Jitest<Func<int, int, int>>("SumCore", out _)
        .Intercept(new Func<int, int, int>((int a, int b) => throw new InvalidOperationException("Test exception"))))
    {
        try
        {
            CalcBase.Sum(1, 2);
        }
        catch (InvalidOperationException ex) when (ex.Message == "Test exception")
        {
            exceptionThrown = true;
        }
    }
    AssertEqual(exceptionThrown, true, "Exception thrown inside interceptor reached caller");
    AssertEqual(CalcBase.Sum(1, 2), 3, "Original method intact after exception");
}

Console.WriteLine();
if (failures > 0)
{
    Console.WriteLine($"[FAILED] {failures} test(s) failed.");
    return 1;
}

Console.WriteLine("=== All integration tests passed! ===");
return 0;

internal class CalcBase
{
    private static int SumCore(int a, int b) => a + b;
    public static int Sum(int a, int b) => SumCore(a, b);
    public int Multiply(int a, int b) => a * b;
}

internal class Calculator : CalcBase { }

internal class CalculatorController(CalcBase calculator)
{
    public int Multiply(int a, int b) => calculator.Multiply(a, b);
}
