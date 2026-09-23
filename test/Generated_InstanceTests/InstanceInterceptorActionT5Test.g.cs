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
public class InstanceInterceptorActionT5Test
{
    [Test]
    public async Task TestVoidMethod_5()
    {
        var target = new InstanceTestTargets();
        var other = new InstanceTestTargets();
        var interceptor = target.InstanceJitest<InstanceTestTargets, int, int, int, int, int>("VoidMethod_5", out Action<int, int, int, int, int> originalMethod);

        // 1. Intercept specific instance
        bool targetIntercepted = false;
        using (interceptor.Intercept(target, (t1, t2, t3, t4, t5) => { targetIntercepted = true; }))
        {
            target.VoidMethod_5(1, 2, 3, 4, 5);
            await Assert.That(targetIntercepted).IsTrue();
            await Assert.That(target.VoidMethod_5Count).IsEqualTo(0);

            other.VoidMethod_5(1, 2, 3, 4, 5);
            await Assert.That(other.VoidMethod_5Count).IsGreaterThan(0);
        }

        // 2. Intercept unsafe (all instances)
        bool unsafeIntercepted = false;
        using (interceptor.InterceptUnsafe((t1, t2, t3, t4, t5) => { unsafeIntercepted = true; }))
        {
            target.VoidMethod_5(1, 2, 3, 4, 5);
            await Assert.That(unsafeIntercepted).IsTrue();

            unsafeIntercepted = false;
            other.VoidMethod_5(1, 2, 3, 4, 5);
            await Assert.That(unsafeIntercepted).IsTrue();
        }

        // 3. Original method delegate
        int targetCountBefore = target.VoidMethod_5Count;
        originalMethod.Invoke(1, 2, 3, 4, 5);
        await Assert.That(target.VoidMethod_5Count).IsGreaterThan(targetCountBefore);

        // 4. Reverted after disposal
        int otherCountBefore = other.VoidMethod_5Count;
        other.VoidMethod_5(1, 2, 3, 4, 5);
        await Assert.That(other.VoidMethod_5Count).IsGreaterThan(otherCountBefore);
    }
}
