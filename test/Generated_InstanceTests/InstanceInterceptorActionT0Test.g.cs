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
    public async Task TestAction0()
    {
        var target = new InstanceTestTargets();
        var other = new InstanceTestTargets();
        var interceptor = target.InstanceJitest<InstanceTestTargets>("Action0", out Action originalMethod);

        // 1. Intercept specific instance
        bool targetIntercepted = false;
        using (interceptor.Intercept(target, () => { targetIntercepted = true; }))
        {
            target.Action0();
            await Assert.That(targetIntercepted).IsTrue();
            await Assert.That(target.Action0Count).IsEqualTo(0);

            other.Action0();
            await Assert.That(other.Action0Count).IsGreaterThan(0);
        }

        // 2. Intercept unsafe (all instances)
        bool unsafeIntercepted = false;
        using (interceptor.InterceptUnsafe(() => { unsafeIntercepted = true; }))
        {
            target.Action0();
            await Assert.That(unsafeIntercepted).IsTrue();

            unsafeIntercepted = false;
            other.Action0();
            await Assert.That(unsafeIntercepted).IsTrue();
        }

        // 3. Original method delegate
        int targetCountBefore = target.Action0Count;
        originalMethod.Invoke();
        await Assert.That(target.Action0Count).IsGreaterThan(targetCountBefore);

        // 4. Reverted after disposal
        int otherCountBefore = other.Action0Count;
        other.Action0();
        await Assert.That(other.Action0Count).IsGreaterThan(otherCountBefore);
    }
}
