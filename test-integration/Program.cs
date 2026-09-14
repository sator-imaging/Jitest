// Licensed under the Apache-2.0 License
// https://github.com/sator-imaging/Jitest

using Jitest;
using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using TUnit.Core;

#pragma warning disable SMA8000 // Literal Should Be Passed as Named Argument
#pragma warning disable SMA7002 // Lambda Allocation
#pragma warning disable SMA0032 // Implicit Boxing Conversion
#pragma warning disable SMA0040 // Missing Using Statement
#pragma warning disable CA1822  // Mark members as static

namespace Jitest.IntegrationTest;

[NotInParallel]
public class IntegrationTests
{
    [Test]
    public async Task TestStaticMethodInterception()
    {
        using (var jitest = typeof(CalcBase)
            .Jitest<Func<int, int, int>>("SumCore", out _)
            .Intercept(static (int a, int b) => 42))
        {
            await Assert.That(CalcBase.Sum(1, 2)).IsEqualTo(42);
        }
        await Assert.That(CalcBase.Sum(1, 2)).IsEqualTo(3);
    }

    [Test]
    public async Task TestInstanceMethodInterceptionAndFiltering()
    {
        var targetCalc = new Calculator();
        var otherCalc = new CalcBase();

        using (var jitest = targetCalc
            .Jitest<Func<int, int, int>>(nameof(CalcBase.Multiply), out var originalMethod)
            .Intercept((Calculator instance, int a, int b) => instance == targetCalc ? 99 : originalMethod.Invoke(a, b)))
        {
            var targetCC = new CalculatorController(targetCalc);
            var otherCC = new CalculatorController(otherCalc);

            await Assert.That(targetCC.Multiply(3, 4)).IsEqualTo(99);
            await Assert.That(otherCC.Multiply(3, 4)).IsEqualTo(12);
        }
        await Assert.That(targetCalc.Multiply(3, 4)).IsEqualTo(12);
    }

    [Test]
    public async Task TestInstanceMethodInterceptionCombinations()
    {
        var baseInstance = new CalcBase();
        var derivedInstance = new Calculator();

        // 1) Target: CalcBase, Interceptor Delegate Declaring Parameter: CalcBase
        using (var j1 = baseInstance
            .Jitest<Func<int, int, int>>(nameof(CalcBase.Multiply), out _)
            .Intercept((CalcBase instance, int a, int b) => 100))
        {
            await Assert.That(baseInstance.Multiply(1, 1)).IsEqualTo(100);
        }

        // 2) Target: CalcBase, Interceptor Delegate Declaring Parameter: Calculator
        using (var j2 = baseInstance
            .Jitest<Func<int, int, int>>(nameof(CalcBase.Multiply), out _)
            .Intercept((Calculator instance, int a, int b) => 200))
        {
            await Assert.That(baseInstance.Multiply(1, 1)).IsEqualTo(200);
        }

        // 3) Target: Calculator, Interceptor Delegate Declaring Parameter: CalcBase
        using (var j3 = derivedInstance
            .Jitest<Func<int, int, int>>(nameof(CalcBase.Multiply), out _)
            .Intercept((CalcBase instance, int a, int b) => 300))
        {
            await Assert.That(derivedInstance.Multiply(1, 1)).IsEqualTo(300);
        }

        // 4) Target: Calculator, Interceptor Delegate Declaring Parameter: Calculator
        using (var j4 = derivedInstance
            .Jitest<Func<int, int, int>>(nameof(CalcBase.Multiply), out _)
            .Intercept((Calculator instance, int a, int b) => 400))
        {
            await Assert.That(derivedInstance.Multiply(1, 1)).IsEqualTo(400);
        }
    }

    [Test]
    public async Task TestStopwatchMethodInterception()
    {
        var expectedSpan = new TimeSpan(9, 8, 7, 6, 5, 4);
        using (var jitest = typeof(Stopwatch)
            .Jitest<Func<long, TimeSpan>>(nameof(Stopwatch.GetElapsedTime), out _)
            .Intercept((long _) => expectedSpan))
        {
            await Assert.That(Stopwatch.GetElapsedTime(123456789)).IsEqualTo(expectedSpan);
        }
    }

    [Test]
    public async Task TestHttpClientMethodInterception()
    {
        using var client = new HttpClient();
        using (var jitest = client
            .Jitest<Func<string, Task<HttpResponseMessage>>>(nameof(HttpClient.GetAsync), out _)
            .Intercept(static (HttpClient _, string _) => Task.FromResult(new HttpResponseMessage(HttpStatusCode.NotExtended) { Version = new(310, 42) })))
        {
            var response = await client.GetAsync("https://yahoo.co.jp/");
            await Assert.That(response.StatusCode).IsEqualTo(HttpStatusCode.NotExtended);
            await Assert.That(response.Version).IsEqualTo(new Version(310, 42));
        }
    }

    [Test]
    public async Task TestJitestContextScope()
    {
        var state = new ConcurrentBag<object>();

        async Task Worker()
        {
            using (JitestContext.BeginTestMethod())
            {
                await Task.Delay(1000);
                state.Add(new object());
            }
        }

        var t1 = Task.Run(Worker);
        var t2 = Task.Run(Worker);

        await Task.WhenAny(t1, t2);
        await Task.Delay(500);

        await Assert.That(state.Count).IsEqualTo(1);

        await Task.WhenAll(t1, t2);

        await Assert.That(state.Count).IsEqualTo(2);
    }

    [Test]
    public async Task TestExceptionHandlingInInteractions()
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
        await Assert.That(exceptionThrown).IsTrue();
        await Assert.That(CalcBase.Sum(1, 2)).IsEqualTo(3);
    }
}

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
