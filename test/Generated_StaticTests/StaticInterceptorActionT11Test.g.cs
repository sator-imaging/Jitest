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
public class StaticInterceptorActionT11Test
{
    [Test]
    public async Task TestStaticVoidMethod_11()
    {
        StaticTestTargets.StaticVoidMethod_11Count = 0;
        bool intercepted = false;
        var interceptor = typeof(StaticTestTargets).StaticJitest<StaticTestTargets, int, int, int, int, int, int, int, int, int, int, int>("StaticVoidMethod_11", out Action<int, int, int, int, int, int, int, int, int, int, int> originalMethod);

        using (interceptor.Intercept((t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11) => { intercepted = true; }))
        {
            StaticTestTargets.StaticVoidMethod_11(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11);
            await Assert.That(intercepted).IsTrue();
            await Assert.That(StaticTestTargets.StaticVoidMethod_11Count).IsEqualTo(0);

            originalMethod.Invoke(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11);
            await Assert.That(StaticTestTargets.StaticVoidMethod_11Count).IsGreaterThan(0);
        }

        StaticTestTargets.StaticVoidMethod_11Count = 0;
        StaticTestTargets.StaticVoidMethod_11(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11);
        await Assert.That(StaticTestTargets.StaticVoidMethod_11Count).IsGreaterThan(0);
    }
}
