// Licensed under the Apache-2.0 License
// https://github.com/sator-imaging/Jitest

using System;
using System.Reflection;

namespace Jitest;

/// <summary>Static method interceptor.</summary>
public sealed class StaticInterceptor<TTarget, T1, T2, T3, T4, T5, T6, T7, T8>
{
    readonly MethodInfo target;
    readonly Delegate? originalMethod;

    internal StaticInterceptor(MethodInfo target, Delegate? originalMethod)
    {
        this.target = target ?? throw new ArgumentNullException(nameof(target));
        this.originalMethod = originalMethod;
    }

    /// <summary>Intercepts static method for <see cref="Action&lt;T1, T2, T3, T4, T5, T6, T7, T8&gt;"/>.</summary>
    public DetourScope Intercept(Action<T1, T2, T3, T4, T5, T6, T7, T8> replacement)
    {
        if (replacement == null) throw new ArgumentNullException(nameof(replacement));
        return DetourScope.Create(target, replacement, instance: null);
    }

    /// <summary>Intercepts static method for <see cref="Func&lt;T1, T2, T3, T4, T5, T6, T7, T8&gt;"/>.</summary>
    public DetourScope Intercept(Func<T1, T2, T3, T4, T5, T6, T7, T8> replacement)
    {
        if (replacement == null) throw new ArgumentNullException(nameof(replacement));
        return DetourScope.Create(target, replacement, instance: null);
    }
}

/// <summary>Extension methods for static interceptors.</summary>
public static class StaticInterceptorExtensions_T8
{
    /// <summary>Extension method for static method interceptor.</summary>
    public static StaticInterceptor<TTarget, T1, T2, T3, T4, T5, T6, T7, T8> StaticJitest<TTarget, T1, T2, T3, T4, T5, T6, T7, T8>(this Type type, string methodName, out Action<T1, T2, T3, T4, T5, T6, T7, T8>? originalMethod)
    {
        var (method, dele) = ResolveMethodHelper.ResolveMethod<Action<T1, T2, T3, T4, T5, T6, T7, T8>>(type, methodName);
        originalMethod = dele;
        return new StaticInterceptor<TTarget, T1, T2, T3, T4, T5, T6, T7, T8>(method, dele);
    }
    /// <summary>Extension method for static method interceptor.</summary>
    public static StaticInterceptor<TTarget, T1, T2, T3, T4, T5, T6, T7, T8> StaticJitest<TTarget, T1, T2, T3, T4, T5, T6, T7, T8>(this Type type, string methodName, out Func<T1, T2, T3, T4, T5, T6, T7, T8>? originalMethod)
    {
        var (method, dele) = ResolveMethodHelper.ResolveMethod<Func<T1, T2, T3, T4, T5, T6, T7, T8>>(type, methodName);
        originalMethod = dele;
        return new StaticInterceptor<TTarget, T1, T2, T3, T4, T5, T6, T7, T8>(method, dele);
    }
}