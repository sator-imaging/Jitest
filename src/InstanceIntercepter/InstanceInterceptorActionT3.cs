// Licensed under the Apache-2.0 License
// https://github.com/sator-imaging/Jitest

using System;
using System.Reflection;

namespace Jitest;

/// <summary>Instance method interceptor for <see cref="Action&lt;T1, T2, T3&gt;"/>.</summary>
public sealed class InstanceInterceptorActionT3<TTarget, T1, T2, T3>
{
    readonly MethodInfo target;
    readonly Delegate? originalMethod;

    internal InstanceInterceptorActionT3(MethodInfo target, Delegate? originalMethod)
    {
        this.target = target ?? throw new ArgumentNullException(nameof(target));
        this.originalMethod = originalMethod;
    }

    /// <summary>Intercepts instance method for specific instance with <see cref="Action&lt;T1, T2, T3&gt;"/>.</summary>
    public DetourScope Intercept(TTarget instance, Action<T1, T2, T3> replacement)
    {
        if (replacement == null) throw new ArgumentNullException(nameof(replacement));
        var orig = originalMethod != null ? (Action<TTarget, T1, T2, T3>)(object)originalMethod : null;
        Action<TTarget, T1, T2, T3> actual = (TTarget self, T1 t1, T2 t2, T3 t3) =>
        {
            if (object.ReferenceEquals(self, instance)) replacement.Invoke(t1, t2, t3);
            else orig?.Invoke(self, t1, t2, t3);
        };
        return DetourScope.Create(target, actual, instance);
    }

    /// <summary>Intercepts instance method across all instances with <see cref="Action&lt;T1, T2, T3&gt;"/>.</summary>
    public DetourScope InterceptUnsafe(Action<T1, T2, T3> replacement)
    {
        if (replacement == null) throw new ArgumentNullException(nameof(replacement));
        Action<TTarget, T1, T2, T3> actual = (TTarget self, T1 t1, T2 t2, T3 t3) => replacement.Invoke(t1, t2, t3);
        return DetourScope.Create(target, actual, instance: null);
    }
}