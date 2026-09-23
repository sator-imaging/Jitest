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
public class StaticInterceptorFuncT5Test
{
    [Test]
    public async Task TestFunc5()
    {
        var originalVal = StaticTestTargets.Func5(1, 2, 3, 4);
        var interceptor = typeof(StaticTestTargets).StaticJitest<StaticTestTargets, int, int, int, int, int>("Func5", out Func<int, int, int, int, int> originalMethod);

        using (interceptor.Intercept((t1, t2, t3, t4) => 9999))
        {
            var res = StaticTestTargets.Func5(1, 2, 3, 4);
            await Assert.That(res).IsEqualTo(9999);

            var origRes = originalMethod.Invoke(1, 2, 3, 4);
            await Assert.That(origRes).IsEqualTo(originalVal);
        }

        var afterRes = StaticTestTargets.Func5(1, 2, 3, 4);
        await Assert.That(afterRes).IsEqualTo(originalVal);
    }
}
