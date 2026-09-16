// Licensed under the Apache-2.0 License
// https://github.com/sator-imaging/Jitest

using System;
using System.Reflection;

namespace Jitest;

/// <summary>Static method interceptor for <see cref="Func&lt;T1, T2, T3, T4, T5, T6, T7, T8, T9, T10&gt;"/>.</summary>
public sealed class StaticInterceptorFuncT10<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>
{
    readonly MethodInfo target;
    readonly Delegate? originalMethod;

    internal StaticInterceptorFuncT10(MethodInfo target, Delegate? originalMethod)
    {
        this.target = target ?? throw new ArgumentNullException(nameof(target));
        this.originalMethod = originalMethod;
    }

    /// <summary>Intercepts static method for <see cref="Func&lt;T1, T2, T3, T4, T5, T6, T7, T8, T9, T10&gt;"/>.</summary>
    public DetourScope Intercept(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> replacement)
    {
        if (replacement == null) throw new ArgumentNullException(nameof(replacement));
        return DetourScope.Create(target, replacement, instance: null);
    }
}

/// <summary>Extension methods for static interceptors.</summary>
public static partial class StaticInterceptorExtensions
{
    /// <summary>Extension method for static method interceptor.</summary>
    public static StaticInterceptorFuncT10<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> StaticJitest<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this Type type, string methodName, out Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>? originalMethod)
    {
        var (method, dele) = Extensions.ResolveMethod<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>>(type, methodName);
        originalMethod = dele;
        return new StaticInterceptorFuncT10<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(method, dele);
    }
}