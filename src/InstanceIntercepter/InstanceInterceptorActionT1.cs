// Licensed under the Apache-2.0 License
// https://github.com/sator-imaging/Jitest

using System;
using System.Reflection;

namespace Jitest;

/// <summary>Instance method interceptor for <see cref="Action&lt;T1&gt;"/>.</summary>
public sealed class InstanceInterceptorActionT1<TTarget, T1>
{
    readonly MethodInfo target;
    readonly Delegate? originalMethod;

    internal InstanceInterceptorActionT1(MethodInfo target, Delegate? originalMethod)
    {
        this.target = target ?? throw new ArgumentNullException(nameof(target));
        this.originalMethod = originalMethod;
    }

    /// <summary>Intercepts instance method for specific instance with <see cref="Action&lt;T1&gt;"/>.</summary>
    public DetourScope Intercept(TTarget instance, Action<T1> replacement)
    {
        if (replacement == null) throw new ArgumentNullException(nameof(replacement));
        var orig = originalMethod != null ? (Action<TTarget, T1>)(object)originalMethod : null;
        Action<TTarget, T1> actual = (TTarget self, T1 t1) =>
        {
            if (object.ReferenceEquals(self, instance)) replacement.Invoke(t1);
            else orig?.Invoke(self, t1);
        };
        return DetourScope.Create(target, actual, instance);
    }

    /// <summary>Intercepts instance method across all instances with <see cref="Action&lt;T1&gt;"/>.</summary>
    public DetourScope InterceptUnsafe(Action<T1> replacement)
    {
        if (replacement == null) throw new ArgumentNullException(nameof(replacement));
        Action<TTarget, T1> actual = (TTarget self, T1 t1) => replacement.Invoke(t1);
        return DetourScope.Create(target, actual, instance: null);
    }
}