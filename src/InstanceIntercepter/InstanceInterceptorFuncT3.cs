// Licensed under the Apache-2.0 License
// https://github.com/sator-imaging/Jitest

using System;
using System.Reflection;

namespace Jitest;

/// <summary>Instance method interceptor for <see cref="Func&lt;T1, T2, T3&gt;"/>.</summary>
public sealed class InstanceInterceptorFuncT3<TTarget, T1, T2, T3>
{
    readonly MethodInfo target;
    readonly Delegate? originalMethod;

    internal InstanceInterceptorFuncT3(MethodInfo target, Delegate? originalMethod)
    {
        this.target = target ?? throw new ArgumentNullException(nameof(target));
        this.originalMethod = originalMethod;
    }

    /// <summary>Intercepts instance method for specific instance with <see cref="Func&lt;T1, T2, T3&gt;"/>.</summary>
    public DetourScope Intercept(TTarget instance, Func<T1, T2, T3> replacement)
    {
        if (replacement == null) throw new ArgumentNullException(nameof(replacement));
        var orig = originalMethod != null ? (Func<TTarget, T1, T2, T3>)(object)originalMethod : null;
        Func<TTarget, T1, T2, T3> actual = (TTarget self, T1 t1, T2 t2) =>
            object.ReferenceEquals(self, instance)
                ? replacement.Invoke(t1, t2)
                : (orig != null ? orig.Invoke(self, t1, t2) : default!);
        return DetourScope.Create(target, actual, instance);
    }

    /// <summary>Intercepts instance method across all instances with <see cref="Func&lt;T1, T2, T3&gt;"/>.</summary>
    public DetourScope InterceptUnsafe(Func<T1, T2, T3> replacement)
    {
        if (replacement == null) throw new ArgumentNullException(nameof(replacement));
        Func<TTarget, T1, T2, T3> actual = (TTarget self, T1 t1, T2 t2) => replacement.Invoke(t1, t2);
        return DetourScope.Create(target, actual, instance: null);
    }
}