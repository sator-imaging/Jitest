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
public class InstanceInterceptorActionT1Test
{
    [Test]
    public async Task TestVoidMethod_1()
    {
        var target = new InstanceTestTargets();
        var other = new InstanceTestTargets();
        var interceptor = target.InstanceJitest<InstanceTestTargets, int>("VoidMethod_1", out Action<int> originalMethod);

        // 1. Intercept specific instance
        bool targetIntercepted = false;
        using (interceptor.Intercept(target, (t1) => { targetIntercepted = true; }))
        {
            target.VoidMethod_1(1);
            await Assert.That(targetIntercepted).IsTrue();
            await Assert.That(target.VoidMethod_1Count).IsEqualTo(0);

            other.VoidMethod_1(1);
            await Assert.That(other.VoidMethod_1Count).IsGreaterThan(0);
        }

        // 2. Intercept unsafe (all instances)
        bool unsafeIntercepted = false;
        using (interceptor.InterceptUnsafe((t1) => { unsafeIntercepted = true; }))
        {
            target.VoidMethod_1(1);
            await Assert.That(unsafeIntercepted).IsTrue();

            unsafeIntercepted = false;
            other.VoidMethod_1(1);
            await Assert.That(unsafeIntercepted).IsTrue();
        }

        // 3. Original method delegate
        int targetCountBefore = target.VoidMethod_1Count;
        originalMethod.Invoke(1);
        await Assert.That(target.VoidMethod_1Count).IsGreaterThan(targetCountBefore);

        // 4. Reverted after disposal
        int otherCountBefore = other.VoidMethod_1Count;
        other.VoidMethod_1(1);
        await Assert.That(other.VoidMethod_1Count).IsGreaterThan(otherCountBefore);
    }
}
