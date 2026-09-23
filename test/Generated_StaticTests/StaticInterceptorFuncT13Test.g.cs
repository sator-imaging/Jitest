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
public class StaticInterceptorFuncT13Test
{
    [Test]
    public async Task TestStaticGetIntMethod_13()
    {
        var originalVal = StaticTestTargets.StaticGetIntMethod_13(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12);
        var interceptor = typeof(StaticTestTargets).StaticJitest<StaticTestTargets, int, int, int, int, int, int, int, int, int, int, int, int, int>("StaticGetIntMethod_13", out Func<int, int, int, int, int, int, int, int, int, int, int, int, int> originalMethod);

        using (interceptor.Intercept((t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12) => 9999))
        {
            var res = StaticTestTargets.StaticGetIntMethod_13(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12);
            await Assert.That(res).IsEqualTo(9999);

            var origRes = originalMethod.Invoke(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12);
            await Assert.That(origRes).IsEqualTo(originalVal);
        }

        var afterRes = StaticTestTargets.StaticGetIntMethod_13(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12);
        await Assert.That(afterRes).IsEqualTo(originalVal);
    }
}
