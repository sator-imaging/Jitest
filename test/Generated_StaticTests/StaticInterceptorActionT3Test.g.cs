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
public class StaticInterceptorActionT3Test
{
    [Test]
    public async Task TestAction3()
    {
        StaticTestTargets.Action3Count = 0;
        bool intercepted = false;
        var interceptor = typeof(StaticTestTargets).StaticJitest<StaticTestTargets, int, int, int>("Action3", out Action<int, int, int> originalMethod);

        using (interceptor.Intercept((t1, t2, t3) => { intercepted = true; }))
        {
            StaticTestTargets.Action3(1, 2, 3);
            await Assert.That(intercepted).IsTrue();
            await Assert.That(StaticTestTargets.Action3Count).IsEqualTo(0);

            originalMethod.Invoke(1, 2, 3);
            await Assert.That(StaticTestTargets.Action3Count).IsGreaterThan(0);
        }

        StaticTestTargets.Action3Count = 0;
        StaticTestTargets.Action3(1, 2, 3);
        await Assert.That(StaticTestTargets.Action3Count).IsGreaterThan(0);
    }
}
