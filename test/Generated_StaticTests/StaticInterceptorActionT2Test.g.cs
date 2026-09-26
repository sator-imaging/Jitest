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
public class StaticInterceptorActionT2Test
{
    [Test]
    public async Task TestStaticVoid_2()
    {
        StaticTestTargets.StaticVoid_2Count = 0;
        bool intercepted = false;
        var interceptor = typeof(StaticTestTargets).StaticJitest<StaticTestTargets, int, int>("StaticVoid_2", out Action<int, int> originalMethod);

        using (interceptor.Intercept((t1, t2) => { intercepted = true; }))
        {
            StaticTestTargets.StaticVoid_2(1, 2);
            await Assert.That(intercepted).IsTrue();
            await Assert.That(StaticTestTargets.StaticVoid_2Count).IsEqualTo(0);

            originalMethod.Invoke(1, 2);
            await Assert.That(StaticTestTargets.StaticVoid_2Count).IsGreaterThan(0);
        }

        StaticTestTargets.StaticVoid_2Count = 0;
        StaticTestTargets.StaticVoid_2(1, 2);
        await Assert.That(StaticTestTargets.StaticVoid_2Count).IsGreaterThan(0);
    }
}
