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
public class StaticInterceptorActionT7Test
{
    [Test]
    public async Task TestAction7()
    {
        StaticTestTargets.Action7Count = 0;
        bool intercepted = false;
        var interceptor = typeof(StaticTestTargets).StaticJitest<StaticTestTargets, int, int, int, int, int, int, int>("Action7", out Action<int, int, int, int, int, int, int> originalMethod);

        using (interceptor.Intercept((t1, t2, t3, t4, t5, t6, t7) => { intercepted = true; }))
        {
            StaticTestTargets.Action7(1, 2, 3, 4, 5, 6, 7);
            await Assert.That(intercepted).IsTrue();
            await Assert.That(StaticTestTargets.Action7Count).IsEqualTo(0);

            originalMethod.Invoke(1, 2, 3, 4, 5, 6, 7);
            await Assert.That(StaticTestTargets.Action7Count).IsGreaterThan(0);
        }

        StaticTestTargets.Action7Count = 0;
        StaticTestTargets.Action7(1, 2, 3, 4, 5, 6, 7);
        await Assert.That(StaticTestTargets.Action7Count).IsGreaterThan(0);
    }
}
