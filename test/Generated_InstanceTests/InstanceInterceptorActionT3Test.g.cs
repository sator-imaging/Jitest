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
public class InstanceInterceptorActionT3Test
{
    [Test]
    public async Task TestVoidMethod_3()
    {
        var target = new InstanceTestTargets();
        var other = new InstanceTestTargets();
        var interceptor = target.InstanceJitest<InstanceTestTargets, int, int, int>("VoidMethod_3", out Action<int, int, int> originalMethod);

        // 1. Intercept specific instance
        bool targetIntercepted = false;
        using (interceptor.Intercept(target, (t1, t2, t3) => { targetIntercepted = true; }))
        {
            target.VoidMethod_3(1, 2, 3);
            await Assert.That(targetIntercepted).IsTrue();
            await Assert.That(target.VoidMethod_3Count).IsEqualTo(0);

            other.VoidMethod_3(1, 2, 3);
            await Assert.That(other.VoidMethod_3Count).IsGreaterThan(0);
        }

        // 2. Intercept unsafe (all instances)
        bool unsafeIntercepted = false;
        using (interceptor.InterceptUnsafe((t1, t2, t3) => { unsafeIntercepted = true; }))
        {
            target.VoidMethod_3(1, 2, 3);
            await Assert.That(unsafeIntercepted).IsTrue();

            unsafeIntercepted = false;
            other.VoidMethod_3(1, 2, 3);
            await Assert.That(unsafeIntercepted).IsTrue();
        }

        // 3. Original method delegate
        int targetCountBefore = target.VoidMethod_3Count;
        originalMethod.Invoke(1, 2, 3);
        await Assert.That(target.VoidMethod_3Count).IsGreaterThan(targetCountBefore);

        // 4. Reverted after disposal
        int otherCountBefore = other.VoidMethod_3Count;
        other.VoidMethod_3(1, 2, 3);
        await Assert.That(other.VoidMethod_3Count).IsGreaterThan(otherCountBefore);
    }
}
