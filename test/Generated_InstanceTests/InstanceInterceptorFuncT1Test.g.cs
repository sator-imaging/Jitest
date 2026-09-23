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
public class InstanceInterceptorFuncT1Test
{
    [Test]
    public async Task TestFunc1()
    {
        var target = new InstanceTestTargets();
        var other = new InstanceTestTargets();
        var targetOriginal = target.Func1();
        var otherOriginal = other.Func1();
        var interceptor = target.InstanceJitest<InstanceTestTargets, int>("Func1", out Func<int> originalMethod);

        // 1. Intercept specific instance
        using (interceptor.Intercept(target, () => 8888))
        {
            var targetRes = target.Func1();
            await Assert.That(targetRes).IsEqualTo(8888);

            var otherRes = other.Func1();
            await Assert.That(otherRes).IsEqualTo(otherOriginal);
        }

        // 2. Intercept unsafe (all instances)
        using (interceptor.InterceptUnsafe(() => 7777))
        {
            var targetRes = target.Func1();
            await Assert.That(targetRes).IsEqualTo(7777);

            var otherRes = other.Func1();
            await Assert.That(otherRes).IsEqualTo(7777);
        }

        // 3. Original method delegate
        var origRes = originalMethod.Invoke();
        await Assert.That(origRes).IsEqualTo(targetOriginal);

        // 4. Reverted after disposal
        var afterRes = target.Func1();
        await Assert.That(afterRes).IsEqualTo(targetOriginal);
    }
}
