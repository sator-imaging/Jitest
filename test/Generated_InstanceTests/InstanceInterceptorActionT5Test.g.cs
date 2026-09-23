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
    public async Task TestAction5()
    {
        var target = new InstanceTestTargets();
        var other = new InstanceTestTargets();
        var interceptor = target.InstanceJitest<InstanceTestTargets, int, int, int, int, int>("Action5", out Action<int, int, int, int, int> originalMethod);

        // 1. Intercept specific instance
        bool targetIntercepted = false;
        using (interceptor.Intercept(target, (t1, t2, t3, t4, t5) => { targetIntercepted = true; }))
        {
            target.Action5(1, 2, 3, 4, 5);
            await Assert.That(targetIntercepted).IsTrue();
            await Assert.That(target.Action5Count).IsEqualTo(0);

            other.Action5(1, 2, 3, 4, 5);
            await Assert.That(other.Action5Count).IsGreaterThan(0);
        }

        // 2. Intercept unsafe (all instances)
        bool unsafeIntercepted = false;
        using (interceptor.InterceptUnsafe((t1, t2, t3, t4, t5) => { unsafeIntercepted = true; }))
        {
            target.Action5(1, 2, 3, 4, 5);
            await Assert.That(unsafeIntercepted).IsTrue();

            unsafeIntercepted = false;
            other.Action5(1, 2, 3, 4, 5);
            await Assert.That(unsafeIntercepted).IsTrue();
        }

        // 3. Original method delegate
        int targetCountBefore = target.Action5Count;
        originalMethod.Invoke(1, 2, 3, 4, 5);
        await Assert.That(target.Action5Count).IsGreaterThan(targetCountBefore);

        // 4. Reverted after disposal
        int otherCountBefore = other.Action5Count;
        other.Action5(1, 2, 3, 4, 5);
        await Assert.That(other.Action5Count).IsGreaterThan(otherCountBefore);
    }
}
