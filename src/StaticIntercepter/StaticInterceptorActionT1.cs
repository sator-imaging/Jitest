// Licensed under the Apache-2.0 License
// https://github.com/sator-imaging/Jitest

using System;
using System.Reflection;

namespace Jitest;

/// <summary>Static method interceptor for <see cref="Action&lt;T1&gt;"/>.</summary>
public sealed class StaticInterceptorActionT1<TTarget, T1>
{
    readonly MethodInfo target;
    readonly Delegate? originalMethod;

    internal StaticInterceptorActionT1(MethodInfo target, Delegate? originalMethod)
    {
        this.target = target ?? throw new ArgumentNullException(nameof(target));
        this.originalMethod = originalMethod;
    }

    /// <summary>Intercepts static method for <see cref="Action&lt;T1&gt;"/>.</summary>
    public DetourScope Intercept(Action<T1> replacement)
    {
        if (replacement == null) throw new ArgumentNullException(nameof(replacement));
        return DetourScope.Create(target, replacement, instance: null);
    }
}