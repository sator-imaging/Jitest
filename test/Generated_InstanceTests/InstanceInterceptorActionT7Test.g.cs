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
public class InstanceInterceptorActionT7Test
{
    [Test]
    public async Task TestAction7()
    {
        var target = new InstanceTestTargets();
        var other = new InstanceTestTargets();
        var interceptor = target.InstanceJitest<InstanceTestTargets, int, int, int, int, int, int, int>("Action7", out Action<int, int, int, int, int, int, int> originalMethod);

        // 1. Intercept specific instance
        bool targetIntercepted = false;
        using (interceptor.Intercept(target, (t1, t2, t3, t4, t5, t6, t7) => { targetIntercepted = true; }))
        {
            target.Action7(1, 2, 3, 4, 5, 6, 7);
            await Assert.That(targetIntercepted).IsTrue();
            await Assert.That(target.Action7Count).IsEqualTo(0);

            other.Action7(1, 2, 3, 4, 5, 6, 7);
            await Assert.That(other.Action7Count).IsGreaterThan(0);
        }

        // 2. Intercept unsafe (all instances)
        bool unsafeIntercepted = false;
        using (interceptor.InterceptUnsafe((t1, t2, t3, t4, t5, t6, t7) => { unsafeIntercepted = true; }))
        {
            target.Action7(1, 2, 3, 4, 5, 6, 7);
            await Assert.That(unsafeIntercepted).IsTrue();

            unsafeIntercepted = false;
            other.Action7(1, 2, 3, 4, 5, 6, 7);
            await Assert.That(unsafeIntercepted).IsTrue();
        }

        // 3. Original method delegate
        int targetCountBefore = target.Action7Count;
        originalMethod.Invoke(1, 2, 3, 4, 5, 6, 7);
        await Assert.That(target.Action7Count).IsGreaterThan(targetCountBefore);

        // 4. Reverted after disposal
        int otherCountBefore = other.Action7Count;
        other.Action7(1, 2, 3, 4, 5, 6, 7);
        await Assert.That(other.Action7Count).IsGreaterThan(otherCountBefore);
    }
}
