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
public class InstanceInterceptorFuncT7Test
{
    [Test]
    public async Task TestGetInt_7()
    {
        var target = new InstanceTestTargets();
        var other = new InstanceTestTargets();
        var targetOriginal = target.GetInt_7(1, 2, 3, 4, 5, 6);
        var otherOriginal = other.GetInt_7(1, 2, 3, 4, 5, 6);
        var interceptor = target.InstanceJitest<InstanceTestTargets, int, int, int, int, int, int, int>("GetInt_7", out Func<int, int, int, int, int, int, int> originalMethod);

        // 1. Intercept specific instance
        using (interceptor.Intercept(target, (t1, t2, t3, t4, t5, t6) => 8888))
        {
            var targetRes = target.GetInt_7(1, 2, 3, 4, 5, 6);
            await Assert.That(targetRes).IsEqualTo(8888);

            var otherRes = other.GetInt_7(1, 2, 3, 4, 5, 6);
            await Assert.That(otherRes).IsEqualTo(otherOriginal);
        }

        // 2. Intercept unsafe (all instances)
        using (interceptor.InterceptUnsafe((t1, t2, t3, t4, t5, t6) => 7777))
        {
            var targetRes = target.GetInt_7(1, 2, 3, 4, 5, 6);
            await Assert.That(targetRes).IsEqualTo(7777);

            var otherRes = other.GetInt_7(1, 2, 3, 4, 5, 6);
            await Assert.That(otherRes).IsEqualTo(7777);
        }

        // 3. Original method delegate
        var origRes = originalMethod.Invoke(1, 2, 3, 4, 5, 6);
        await Assert.That(origRes).IsEqualTo(targetOriginal);

        // 4. Reverted after disposal
        var afterRes = target.GetInt_7(1, 2, 3, 4, 5, 6);
        await Assert.That(afterRes).IsEqualTo(targetOriginal);
    }
}
