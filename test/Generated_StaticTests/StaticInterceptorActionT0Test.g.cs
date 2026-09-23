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
public class StaticInterceptorActionT0Test
{
    [Test]
    public async Task TestAction0()
    {
        StaticTestTargets.Action0Count = 0;
        bool intercepted = false;
        var interceptor = typeof(StaticTestTargets).StaticJitest<StaticTestTargets>("Action0", out Action originalMethod);

        using (interceptor.Intercept(() => { intercepted = true; }))
        {
            StaticTestTargets.Action0();
            await Assert.That(intercepted).IsTrue();
            await Assert.That(StaticTestTargets.Action0Count).IsEqualTo(0);

            originalMethod.Invoke();
            await Assert.That(StaticTestTargets.Action0Count).IsGreaterThan(0);
        }

        StaticTestTargets.Action0Count = 0;
        StaticTestTargets.Action0();
        await Assert.That(StaticTestTargets.Action0Count).IsGreaterThan(0);
    }
}
