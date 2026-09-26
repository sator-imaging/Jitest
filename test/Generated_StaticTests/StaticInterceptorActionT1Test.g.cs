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
    public async Task TestStaticVoid_1()
    {
        StaticTestTargets.StaticVoid_1Count = 0;
        bool intercepted = false;
        var interceptor = typeof(StaticTestTargets).StaticJitest<StaticTestTargets, int>("StaticVoid_1", out Action<int> originalMethod);

        using (interceptor.Intercept((t1) => { intercepted = true; }))
        {
            StaticTestTargets.StaticVoid_1(1);
            await Assert.That(intercepted).IsTrue();
            await Assert.That(StaticTestTargets.StaticVoid_1Count).IsEqualTo(0);

            originalMethod.Invoke(1);
            await Assert.That(StaticTestTargets.StaticVoid_1Count).IsGreaterThan(0);
        }

        StaticTestTargets.StaticVoid_1Count = 0;
        StaticTestTargets.StaticVoid_1(1);
        await Assert.That(StaticTestTargets.StaticVoid_1Count).IsGreaterThan(0);
    }
}
