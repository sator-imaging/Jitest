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
    public async Task TestStaticVoidMethod_2()
    {
        StaticTestTargets.StaticVoidMethod_2Count = 0;
        bool intercepted = false;
        var interceptor = typeof(StaticTestTargets).StaticJitest<StaticTestTargets, int, int>("StaticVoidMethod_2", out Action<int, int> originalMethod);

        using (interceptor.Intercept((t1, t2) => { intercepted = true; }))
        {
            StaticTestTargets.StaticVoidMethod_2(1, 2);
            await Assert.That(intercepted).IsTrue();
            await Assert.That(StaticTestTargets.StaticVoidMethod_2Count).IsEqualTo(0);

            originalMethod.Invoke(1, 2);
            await Assert.That(StaticTestTargets.StaticVoidMethod_2Count).IsGreaterThan(0);
        }

        StaticTestTargets.StaticVoidMethod_2Count = 0;
        StaticTestTargets.StaticVoidMethod_2(1, 2);
        await Assert.That(StaticTestTargets.StaticVoidMethod_2Count).IsGreaterThan(0);
    }
}
