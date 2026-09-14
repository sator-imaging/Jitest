// Licensed under the Apache-2.0 License
// https://github.com/sator-imaging/Jitest

using System;
using System.Reflection;

namespace Jitest;

/// <summary>Instance method interceptor for <see cref="Func&lt;T1&gt;"/>.</summary>
public sealed class InstanceInterceptorFuncT1<TTarget, T1>
{
    readonly MethodInfo target;
    readonly Delegate? originalMethod;

    internal InstanceInterceptorFuncT1(MethodInfo target, Delegate? originalMethod)
    {
        this.target = target ?? throw new ArgumentNullException(nameof(target));
        this.originalMethod = originalMethod;
    }

    /// <summary>Intercepts instance method for specific instance with <see cref="Func&lt;T1&gt;"/>.</summary>
    public DetourScope Intercept(TTarget instance, Func<T1> replacement)
    {
        if (replacement == null) throw new ArgumentNullException(nameof(replacement));
        var orig = originalMethod != null ? (Func<TTarget, T1>)(object)originalMethod : null;
        Func<TTarget, T1> actual = (TTarget self) =>
            object.ReferenceEquals(self, instance)
                ? replacement.Invoke()
                : (orig != null ? orig.Invoke(self) : default!);
        return DetourScope.Create(target, actual, instance);
    }

    /// <summary>Intercepts instance method across all instances with <see cref="Func&lt;T1&gt;"/>.</summary>
    public DetourScope InterceptUnsafe(Func<T1> replacement)
    {
        if (replacement == null) throw new ArgumentNullException(nameof(replacement));
        Func<TTarget, T1> actual = (TTarget self) => replacement.Invoke();
        return DetourScope.Create(target, actual, instance: null);
    }
}