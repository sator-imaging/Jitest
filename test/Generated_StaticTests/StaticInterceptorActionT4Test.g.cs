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
public class StaticInterceptorActionT4Test
{
    [Test]
    public async Task TestStaticVoidMethod_4()
    {
        StaticTestTargets.StaticVoidMethod_4Count = 0;
        bool intercepted = false;
        var interceptor = typeof(StaticTestTargets).StaticJitest<StaticTestTargets, int, int, int, int>("StaticVoidMethod_4", out Action<int, int, int, int> originalMethod);

        using (interceptor.Intercept((t1, t2, t3, t4) => { intercepted = true; }))
        {
            StaticTestTargets.StaticVoidMethod_4(1, 2, 3, 4);
            await Assert.That(intercepted).IsTrue();
            await Assert.That(StaticTestTargets.StaticVoidMethod_4Count).IsEqualTo(0);

            originalMethod.Invoke(1, 2, 3, 4);
            await Assert.That(StaticTestTargets.StaticVoidMethod_4Count).IsGreaterThan(0);
        }

        StaticTestTargets.StaticVoidMethod_4Count = 0;
        StaticTestTargets.StaticVoidMethod_4(1, 2, 3, 4);
        await Assert.That(StaticTestTargets.StaticVoidMethod_4Count).IsGreaterThan(0);
    }
}
