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
public class StaticInterceptorFuncT4Test
{
    [Test]
    public async Task TestFunc4()
    {
        var originalVal = StaticTestTargets.Func4(1, 2, 3);
        var interceptor = typeof(StaticTestTargets).StaticJitest<StaticTestTargets, int, int, int, int>("Func4", out Func<int, int, int, int> originalMethod);

        using (interceptor.Intercept((t1, t2, t3) => 9999))
        {
            var res = StaticTestTargets.Func4(1, 2, 3);
            await Assert.That(res).IsEqualTo(9999);

            var origRes = originalMethod.Invoke(1, 2, 3);
            await Assert.That(origRes).IsEqualTo(originalVal);
        }

        var afterRes = StaticTestTargets.Func4(1, 2, 3);
        await Assert.That(afterRes).IsEqualTo(originalVal);
    }
}
