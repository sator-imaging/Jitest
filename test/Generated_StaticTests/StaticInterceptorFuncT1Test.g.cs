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
public class StaticInterceptorFuncT1Test
{
    [Test]
    public async Task TestFunc1()
    {
        var originalVal = StaticTestTargets.Func1();
        var interceptor = typeof(StaticTestTargets).StaticJitest<StaticTestTargets, int>("Func1", out Func<int> originalMethod);

        using (interceptor.Intercept(() => 9999))
        {
            var res = StaticTestTargets.Func1();
            await Assert.That(res).IsEqualTo(9999);

            var origRes = originalMethod.Invoke();
            await Assert.That(origRes).IsEqualTo(originalVal);
        }

        var afterRes = StaticTestTargets.Func1();
        await Assert.That(afterRes).IsEqualTo(originalVal);
    }
}
