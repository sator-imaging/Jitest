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
public class InstanceInterceptorActionT2Test
{
    [Test]
    public async Task TestVoid_2()
    {
        var target = new InstanceTestTargets();
        var other = new InstanceTestTargets();
        var interceptor = target.InstanceJitest<InstanceTestTargets, int, int>("Void_2", out Action<int, int> originalMethod);

        // 1. Intercept specific instance
        bool targetIntercepted = false;
        using (interceptor.Intercept(target, (t1, t2) => { targetIntercepted = true; }))
        {
            target.Void_2(1, 2);
            await Assert.That(targetIntercepted).IsTrue();
            await Assert.That(target.Void_2Count).IsEqualTo(0);

            other.Void_2(1, 2);
            await Assert.That(other.Void_2Count).IsGreaterThan(0);
        }

        // 2. Intercept unsafe (all instances)
        bool unsafeIntercepted = false;
        using (interceptor.InterceptUnsafe((t1, t2) => { unsafeIntercepted = true; }))
        {
            target.Void_2(1, 2);
            await Assert.That(unsafeIntercepted).IsTrue();

            unsafeIntercepted = false;
            other.Void_2(1, 2);
            await Assert.That(unsafeIntercepted).IsTrue();
        }

        // 3. Original method delegate
        int targetCountBefore = target.Void_2Count;
        originalMethod.Invoke(1, 2);
        await Assert.That(target.Void_2Count).IsGreaterThan(targetCountBefore);

        // 4. Reverted after disposal
        int otherCountBefore = other.Void_2Count;
        other.Void_2(1, 2);
        await Assert.That(other.Void_2Count).IsGreaterThan(otherCountBefore);
    }
}
