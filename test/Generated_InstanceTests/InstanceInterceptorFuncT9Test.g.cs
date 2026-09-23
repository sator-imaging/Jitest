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
public class InstanceInterceptorFuncT9Test
{
    [Test]
    public async Task TestFunc9()
    {
        var target = new InstanceTestTargets();
        var other = new InstanceTestTargets();
        var targetOriginal = target.Func9(1, 2, 3, 4, 5, 6, 7, 8);
        var otherOriginal = other.Func9(1, 2, 3, 4, 5, 6, 7, 8);
        var interceptor = target.InstanceJitest<InstanceTestTargets, int, int, int, int, int, int, int, int, int>("Func9", out Func<int, int, int, int, int, int, int, int, int> originalMethod);

        // 1. Intercept specific instance
        using (interceptor.Intercept(target, (t1, t2, t3, t4, t5, t6, t7, t8) => 8888))
        {
            var targetRes = target.Func9(1, 2, 3, 4, 5, 6, 7, 8);
            await Assert.That(targetRes).IsEqualTo(8888);

            var otherRes = other.Func9(1, 2, 3, 4, 5, 6, 7, 8);
            await Assert.That(otherRes).IsEqualTo(otherOriginal);
        }

        // 2. Intercept unsafe (all instances)
        using (interceptor.InterceptUnsafe((t1, t2, t3, t4, t5, t6, t7, t8) => 7777))
        {
            var targetRes = target.Func9(1, 2, 3, 4, 5, 6, 7, 8);
            await Assert.That(targetRes).IsEqualTo(7777);

            var otherRes = other.Func9(1, 2, 3, 4, 5, 6, 7, 8);
            await Assert.That(otherRes).IsEqualTo(7777);
        }

        // 3. Original method delegate
        var origRes = originalMethod.Invoke(1, 2, 3, 4, 5, 6, 7, 8);
        await Assert.That(origRes).IsEqualTo(targetOriginal);

        // 4. Reverted after disposal
        var afterRes = target.Func9(1, 2, 3, 4, 5, 6, 7, 8);
        await Assert.That(afterRes).IsEqualTo(targetOriginal);
    }
}
