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
public class InstanceInterceptorActionT12Test
{
    [Test]
    public async Task TestVoid_12()
    {
        var target = new InstanceTestTargets();
        var other = new InstanceTestTargets();
        var interceptor = target.InstanceJitest<InstanceTestTargets, int, int, int, int, int, int, int, int, int, int, int, int>("Void_12", out Action<int, int, int, int, int, int, int, int, int, int, int, int> originalMethod);

        // 1. Intercept specific instance
        bool targetIntercepted = false;
        using (interceptor.Intercept(target, (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12) => { targetIntercepted = true; }))
        {
            target.Void_12(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12);
            await Assert.That(targetIntercepted).IsTrue();
            await Assert.That(target.Void_12Count).IsEqualTo(0);

            other.Void_12(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12);
            await Assert.That(other.Void_12Count).IsGreaterThan(0);
        }

        // 2. Intercept unsafe (all instances)
        bool unsafeIntercepted = false;
        using (interceptor.InterceptUnsafe((t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12) => { unsafeIntercepted = true; }))
        {
            target.Void_12(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12);
            await Assert.That(unsafeIntercepted).IsTrue();

            unsafeIntercepted = false;
            other.Void_12(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12);
            await Assert.That(unsafeIntercepted).IsTrue();
        }

        // 3. Original method delegate
        int targetCountBefore = target.Void_12Count;
        originalMethod.Invoke(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12);
        await Assert.That(target.Void_12Count).IsGreaterThan(targetCountBefore);

        // 4. Reverted after disposal
        int otherCountBefore = other.Void_12Count;
        other.Void_12(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12);
        await Assert.That(other.Void_12Count).IsGreaterThan(otherCountBefore);
    }
}
