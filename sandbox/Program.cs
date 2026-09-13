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

Console.WriteLine("=== Jitest Smoke Test ===");
Console.WriteLine();
Console.WriteLine($"  Runtime: {Environment.Version}");
Console.WriteLine();

Console.WriteLine("--- Static Method (private) ---");
Console.WriteLine();
{
    using var jitest = typeof(CalcBase)
        .Jitest<Func<int, int, int>>("SumCore", out _)
        .Intercept(static (int a, int b) => 42);

    Console.WriteLine($"  1) During intercept:   Calc.Sum(1, 2) = {CalcBase.Sum(1, 2)}  (expected: 42)");
}
Console.WriteLine($"  2) After dispose:      Calc.Sum(1, 2) = {CalcBase.Sum(1, 2)}   (expected: 3)");

Console.WriteLine();
Console.WriteLine("--- Instance Method (DI: Calc <- CalcController) ---");
Console.WriteLine();
var targetCalc = new Calculator();
var otherCalc = new CalcBase();
{
    using var jitest = targetCalc
        .Jitest<Func<int, int, int>>(nameof(CalcBase.Multiply), out var originalMethod)
        .Intercept((Calculator instance, int a, int b) => instance == targetCalc ? 99 : originalMethod.Invoke(a, b));

    var targetCC = new CalculatorController(targetCalc);
    var otherCC = new CalculatorController(otherCalc);

    Console.WriteLine($"  3) During intercept, target instance:      targetCC.Multiply(3, 4)   = {targetCC.Multiply(3, 4)}  (expected: 99)");
    Console.WriteLine($"  4) During intercept, DIFFERENT instance:   otherCC.Multiply(3, 4)    = {otherCC.Multiply(3, 4)}  (expected: 12)");
}
Console.WriteLine($"  5) After dispose:                          targetCalc.Multiply(3, 4) = {targetCalc.Multiply(3, 4)}  (expected: 12)");

#if NET7_0_OR_GREATER

Console.WriteLine();
Console.WriteLine("--- Stopwatch Method ---");
Console.WriteLine();
{
    using var jitest = typeof(Stopwatch)
        .Jitest<Func<long, TimeSpan>>(nameof(Stopwatch.GetElapsedTime), out _)
        .Intercept(static (long _) => new TimeSpan(9, 8, 7, 6, 5, 4));
    Console.WriteLine($"  Stopwatch.GetElapsedTime(123456789) = {Stopwatch.GetElapsedTime(123456789)}  (expected: 9d 8h 7m 6s 5ms 4us)");
}
Console.WriteLine($"  Stopwatch.GetElapsedTime(123456789) =   {Stopwatch.GetElapsedTime(123456789)}  (reverted to original)");

#endif

Console.WriteLine();
Console.WriteLine("--- HttpClient Method ---");
Console.WriteLine();
using var client = new HttpClient();
{
    using var jitest = client
        .Jitest<Func<string, Task<HttpResponseMessage>>>(nameof(HttpClient.GetAsync), out _)
        .Intercept(static (HttpClient _, string _) => Task.FromResult(new HttpResponseMessage(HttpStatusCode.NotExtended) { Version = new(310, 42) }));

    Console.WriteLine($"  {client.GetAsync("https://yahoo.co.jp/").Result.ToString()[..64]}...  (expected: 510, 310.42)");
}
Console.WriteLine($"  {client.GetAsync("https://yahoo.co.jp/").Result.ToString()[..64]}...  (reverted to original)");

Console.WriteLine();
Console.WriteLine("  If every 'expected' value above matched, the whole pipeline is confirmed working.");

#if false
{
    using var jitest = 0xFFFF
        .Jitest<Func<string>>(nameof(object.ToString), out _)  // <-- Expected error on this method
        .Intercept(static (ref int _) => string.Empty);
}
#endif


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
