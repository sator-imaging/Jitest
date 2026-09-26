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
public class StaticInterceptorActionT5Test
{
    [Test]
    public async Task TestStaticVoid_5()
    {
        StaticTestTargets.StaticVoid_5Count = 0;
        bool intercepted = false;
        var interceptor = typeof(StaticTestTargets).StaticJitest<StaticTestTargets, int, int, int, int, int>("StaticVoid_5", out Action<int, int, int, int, int> originalMethod);

        using (interceptor.Intercept((t1, t2, t3, t4, t5) => { intercepted = true; }))
        {
            StaticTestTargets.StaticVoid_5(1, 2, 3, 4, 5);
            await Assert.That(intercepted).IsTrue();
            await Assert.That(StaticTestTargets.StaticVoid_5Count).IsEqualTo(0);

            originalMethod.Invoke(1, 2, 3, 4, 5);
            await Assert.That(StaticTestTargets.StaticVoid_5Count).IsGreaterThan(0);
        }

        StaticTestTargets.StaticVoid_5Count = 0;
        StaticTestTargets.StaticVoid_5(1, 2, 3, 4, 5);
        await Assert.That(StaticTestTargets.StaticVoid_5Count).IsGreaterThan(0);
    }
}
