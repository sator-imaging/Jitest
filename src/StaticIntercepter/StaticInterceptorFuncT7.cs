// Licensed under the Apache-2.0 License
// https://github.com/sator-imaging/Jitest

using System;
using System.Reflection;

namespace Jitest;

/// <summary>Static method interceptor for <see cref="Func&lt;T1, T2, T3, T4, T5, T6, T7&gt;"/>.</summary>
public sealed class StaticInterceptorFuncT7<TTarget, T1, T2, T3, T4, T5, T6, T7>
{
    readonly MethodInfo target;
    readonly Delegate? originalMethod;

    internal StaticInterceptorFuncT7(MethodInfo target, Delegate? originalMethod)
    {
        this.target = target ?? throw new ArgumentNullException(nameof(target));
        this.originalMethod = originalMethod;
    }

    /// <summary>Intercepts static method for <see cref="Func&lt;T1, T2, T3, T4, T5, T6, T7&gt;"/>.</summary>
    public DetourScope Intercept(Func<T1, T2, T3, T4, T5, T6, T7> replacement)
    {
        if (replacement == null) throw new ArgumentNullException(nameof(replacement));
        return DetourScope.Create(target, replacement, instance: null);
    }
}

/// <summary>Extension methods for static interceptors.</summary>
public static partial class StaticInterceptorExtensions
{
    /// <summary>Extension method for static method interceptor.</summary>
    public static StaticInterceptorFuncT7<TTarget, T1, T2, T3, T4, T5, T6, T7> StaticJitest<TTarget, T1, T2, T3, T4, T5, T6, T7>(this Type type, string methodName, out Func<T1, T2, T3, T4, T5, T6, T7>? originalMethod)
    {
        var (method, dele) = Extensions.ResolveMethod<Func<T1, T2, T3, T4, T5, T6, T7>>(type, methodName);
        originalMethod = dele;
        return new StaticInterceptorFuncT7<TTarget, T1, T2, T3, T4, T5, T6, T7>(method, dele);
    }
}