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
public class InstanceInterceptorActionT0Test
{
    [Test]
    public async Task TestVoid_0()
    {
        var target = new InstanceTestTargets();
        var other = new InstanceTestTargets();
        var interceptor = target.InstanceJitest<InstanceTestTargets>("Void_0", out Action originalMethod);

        // 1. Intercept specific instance
        bool targetIntercepted = false;
        using (interceptor.Intercept(target, () => { targetIntercepted = true; }))
        {
            target.Void_0();
            await Assert.That(targetIntercepted).IsTrue();
            await Assert.That(target.Void_0Count).IsEqualTo(0);

            other.Void_0();
            await Assert.That(other.Void_0Count).IsGreaterThan(0);
        }

        // 2. Intercept unsafe (all instances)
        bool unsafeIntercepted = false;
        using (interceptor.InterceptUnsafe(() => { unsafeIntercepted = true; }))
        {
            target.Void_0();
            await Assert.That(unsafeIntercepted).IsTrue();

            unsafeIntercepted = false;
            other.Void_0();
            await Assert.That(unsafeIntercepted).IsTrue();
        }

        // 3. Original method delegate
        int targetCountBefore = target.Void_0Count;
        originalMethod.Invoke();
        await Assert.That(target.Void_0Count).IsGreaterThan(targetCountBefore);

        // 4. Reverted after disposal
        int otherCountBefore = other.Void_0Count;
        other.Void_0();
        await Assert.That(other.Void_0Count).IsGreaterThan(otherCountBefore);
    }
}
