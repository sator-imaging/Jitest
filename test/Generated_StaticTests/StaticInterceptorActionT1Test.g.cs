// Licensed under the Apache-2.0 License
// https://github.com/sator-imaging/Jitest

#nullable enable

using Jitest;
using System;
using System.Threading.Tasks;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using TUnit.Core;

namespace Jitest.Test;

[NotInParallel]
public class StaticInterceptorActionT1Test
{
    [Test]
    public async Task TestStaticVoidMethod_1()
    {
        StaticTestTargets.StaticVoidMethod_1Count = 0;
        bool intercepted = false;
        var interceptor = typeof(StaticTestTargets).StaticJitest<StaticTestTargets, int>("StaticVoidMethod_1", out Action<int> originalMethod);

        using (interceptor.Intercept((t1) => { intercepted = true; }))
        {
            StaticTestTargets.StaticVoidMethod_1(1);
            await Assert.That(intercepted).IsTrue();
            await Assert.That(StaticTestTargets.StaticVoidMethod_1Count).IsEqualTo(0);

            originalMethod.Invoke(1);
            await Assert.That(StaticTestTargets.StaticVoidMethod_1Count).IsGreaterThan(0);
        }

        StaticTestTargets.StaticVoidMethod_1Count = 0;
        StaticTestTargets.StaticVoidMethod_1(1);
        await Assert.That(StaticTestTargets.StaticVoidMethod_1Count).IsGreaterThan(0);
    }
}
