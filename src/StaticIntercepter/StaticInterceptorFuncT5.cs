// Licensed under the Apache-2.0 License
// https://github.com/sator-imaging/Jitest

using System;
using System.Reflection;

namespace Jitest;

/// <summary>Static method interceptor for <see cref="Func&lt;T1, T2, T3, T4, T5&gt;"/>.</summary>
public sealed class StaticInterceptorFuncT5<TTarget, T1, T2, T3, T4, T5>
{
    readonly MethodInfo target;
    readonly Delegate? originalMethod;

    internal StaticInterceptorFuncT5(MethodInfo target, Delegate? originalMethod)
    {
        this.target = target ?? throw new ArgumentNullException(nameof(target));
        this.originalMethod = originalMethod;
    }

    /// <summary>Intercepts static method for <see cref="Func&lt;T1, T2, T3, T4, T5&gt;"/>.</summary>
    public DetourScope Intercept(Func<T1, T2, T3, T4, T5> replacement)
    {
        if (replacement == null) throw new ArgumentNullException(nameof(replacement));
        return DetourScope.Create(target, replacement, instance: null);
    }
}