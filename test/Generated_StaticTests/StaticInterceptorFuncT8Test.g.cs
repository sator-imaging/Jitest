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
public class StaticInterceptorFuncT8Test
{
    [Test]
    public async Task TestStaticGetInt_8()
    {
        var originalVal = StaticTestTargets.StaticGetInt_8(1, 2, 3, 4, 5, 6, 7);
        var interceptor = typeof(StaticTestTargets).StaticJitest<StaticTestTargets, int, int, int, int, int, int, int, int>("StaticGetInt_8", out Func<int, int, int, int, int, int, int, int> originalMethod);

        using (interceptor.Intercept((t1, t2, t3, t4, t5, t6, t7) => 9999))
        {
            var res = StaticTestTargets.StaticGetInt_8(1, 2, 3, 4, 5, 6, 7);
            await Assert.That(res).IsEqualTo(9999);

            var origRes = originalMethod.Invoke(1, 2, 3, 4, 5, 6, 7);
            await Assert.That(origRes).IsEqualTo(originalVal);
        }

        var afterRes = StaticTestTargets.StaticGetInt_8(1, 2, 3, 4, 5, 6, 7);
        await Assert.That(afterRes).IsEqualTo(originalVal);
    }
}
