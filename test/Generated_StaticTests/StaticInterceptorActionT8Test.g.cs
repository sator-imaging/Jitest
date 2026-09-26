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
public class StaticInterceptorActionT8Test
{
    [Test]
    public async Task TestStaticVoid_8()
    {
        StaticTestTargets.StaticVoid_8_Count = 0;
        bool intercepted = false;
        var interceptor = typeof(StaticTestTargets).StaticJitest<StaticTestTargets, int, int, int, int, int, int, int, int>("StaticVoid_8", out Action<int, int, int, int, int, int, int, int> originalMethod);

        using (interceptor.Intercept((t1, t2, t3, t4, t5, t6, t7, t8) => { intercepted = true; }))
        {
            StaticTestTargets.StaticVoid_8(1, 2, 3, 4, 5, 6, 7, 8);
            await Assert.That(intercepted).IsTrue();
            await Assert.That(StaticTestTargets.StaticVoid_8_Count).IsEqualTo(0);

            originalMethod.Invoke(1, 2, 3, 4, 5, 6, 7, 8);
            await Assert.That(StaticTestTargets.StaticVoid_8_Count).IsGreaterThan(0);
        }

        StaticTestTargets.StaticVoid_8_Count = 0;
        StaticTestTargets.StaticVoid_8(1, 2, 3, 4, 5, 6, 7, 8);
        await Assert.That(StaticTestTargets.StaticVoid_8_Count).IsGreaterThan(0);
    }
}
