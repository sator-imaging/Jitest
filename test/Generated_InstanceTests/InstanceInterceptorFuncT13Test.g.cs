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
public class InstanceInterceptorFuncT13Test
{
    [Test]
    public async Task TestFunc13()
    {
        var target = new InstanceTestTargets();
        var other = new InstanceTestTargets();
        var targetOriginal = target.Func13(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12);
        var otherOriginal = other.Func13(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12);
        var interceptor = target.InstanceJitest<InstanceTestTargets, int, int, int, int, int, int, int, int, int, int, int, int, int>("Func13", out Func<int, int, int, int, int, int, int, int, int, int, int, int, int> originalMethod);

        // 1. Intercept specific instance
        using (interceptor.Intercept(target, (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12) => 8888))
        {
            var targetRes = target.Func13(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12);
            await Assert.That(targetRes).IsEqualTo(8888);

            var otherRes = other.Func13(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12);
            await Assert.That(otherRes).IsEqualTo(otherOriginal);
        }

        // 2. Intercept unsafe (all instances)
        using (interceptor.InterceptUnsafe((t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12) => 7777))
        {
            var targetRes = target.Func13(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12);
            await Assert.That(targetRes).IsEqualTo(7777);

            var otherRes = other.Func13(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12);
            await Assert.That(otherRes).IsEqualTo(7777);
        }

        // 3. Original method delegate
        var origRes = originalMethod.Invoke(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12);
        await Assert.That(origRes).IsEqualTo(targetOriginal);

        // 4. Reverted after disposal
        var afterRes = target.Func13(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12);
        await Assert.That(afterRes).IsEqualTo(targetOriginal);
    }
}
