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
public class StaticInterceptorFuncT3Test
{
    [Test]
    public async Task TestStaticGetIntMethod_3()
    {
        var originalVal = StaticTestTargets.StaticGetIntMethod_3(1, 2);
        var interceptor = typeof(StaticTestTargets).StaticJitest<StaticTestTargets, int, int, int>("StaticGetIntMethod_3", out Func<int, int, int> originalMethod);

        using (interceptor.Intercept((t1, t2) => 9999))
        {
            var res = StaticTestTargets.StaticGetIntMethod_3(1, 2);
            await Assert.That(res).IsEqualTo(9999);

            var origRes = originalMethod.Invoke(1, 2);
            await Assert.That(origRes).IsEqualTo(originalVal);
        }

        var afterRes = StaticTestTargets.StaticGetIntMethod_3(1, 2);
        await Assert.That(afterRes).IsEqualTo(originalVal);
    }
}
