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
public class StaticInterceptorActionT15Test
{
    [Test]
    public async Task TestAction15()
    {
        StaticTestTargets.Action15Count = 0;
        bool intercepted = false;
        var interceptor = typeof(StaticTestTargets).StaticJitest<StaticTestTargets, int, int, int, int, int, int, int, int, int, int, int, int, int, int, int>("Action15", out Action<int, int, int, int, int, int, int, int, int, int, int, int, int, int, int> originalMethod);

        using (interceptor.Intercept((t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14, t15) => { intercepted = true; }))
        {
            StaticTestTargets.Action15(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15);
            await Assert.That(intercepted).IsTrue();
            await Assert.That(StaticTestTargets.Action15Count).IsEqualTo(0);

            originalMethod.Invoke(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15);
            await Assert.That(StaticTestTargets.Action15Count).IsGreaterThan(0);
        }

        StaticTestTargets.Action15Count = 0;
        StaticTestTargets.Action15(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15);
        await Assert.That(StaticTestTargets.Action15Count).IsGreaterThan(0);
    }
}
