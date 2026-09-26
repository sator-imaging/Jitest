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
    public async Task TestStaticVoid_0()
    {
        StaticTestTargets.StaticVoid_0Count = 0;
        bool intercepted = false;
        var interceptor = typeof(StaticTestTargets).StaticJitest<StaticTestTargets>("StaticVoid_0", out Action originalMethod);

        using (interceptor.Intercept(() => { intercepted = true; }))
        {
            StaticTestTargets.StaticVoid_0();
            await Assert.That(intercepted).IsTrue();
            await Assert.That(StaticTestTargets.StaticVoid_0Count).IsEqualTo(0);

            originalMethod.Invoke();
            await Assert.That(StaticTestTargets.StaticVoid_0Count).IsGreaterThan(0);
        }

        StaticTestTargets.StaticVoid_0Count = 0;
        StaticTestTargets.StaticVoid_0();
        await Assert.That(StaticTestTargets.StaticVoid_0Count).IsGreaterThan(0);
    }
}
