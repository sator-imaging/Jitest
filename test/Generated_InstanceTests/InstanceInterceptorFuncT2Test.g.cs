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
public class InstanceInterceptorFuncT2Test
{
    [Test]
    public async Task TestGetInt_2()
    {
        var target = new InstanceTestTargets();
        var other = new InstanceTestTargets();
        var targetOriginal = target.GetInt_2(1);
        var otherOriginal = other.GetInt_2(1);
        var interceptor = target.InstanceJitest<InstanceTestTargets, int, int>("GetInt_2", out Func<int, int> originalMethod);

        // 1. Intercept specific instance
        using (interceptor.Intercept(target, (t1) => 8888))
        {
            var targetRes = target.GetInt_2(1);
            await Assert.That(targetRes).IsEqualTo(8888);

            var otherRes = other.GetInt_2(1);
            await Assert.That(otherRes).IsEqualTo(otherOriginal);
        }

        // 2. Intercept unsafe (all instances)
        using (interceptor.InterceptUnsafe((t1) => 7777))
        {
            var targetRes = target.GetInt_2(1);
            await Assert.That(targetRes).IsEqualTo(7777);

            var otherRes = other.GetInt_2(1);
            await Assert.That(otherRes).IsEqualTo(7777);
        }

        // 3. Original method delegate
        var origRes = originalMethod.Invoke(1);
        await Assert.That(origRes).IsEqualTo(targetOriginal);

        // 4. Reverted after disposal
        var afterRes = target.GetInt_2(1);
        await Assert.That(afterRes).IsEqualTo(targetOriginal);
    }
}
