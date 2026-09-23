// Licensed under the Apache-2.0 License
// https://github.com/sator-imaging/Jitest

#nullable enable

using System;
using System.Collections.Generic;
using System.Reflection;

namespace Jitest;

/// <summary>Static method interceptor for <see cref="Func&lt;T1, T2, T3, T4&gt;"/>.</summary>
public sealed class StaticInterceptorFuncT4<TTarget, T1, T2, T3, T4>
{
    readonly Interceptor interceptor;

    internal StaticInterceptorFuncT4(Interceptor interceptor)
    {
        this.interceptor = interceptor ?? throw new ArgumentNullException(nameof(interceptor));
    }

    /// <summary>Intercepts static method for <see cref="Func&lt;T1, T2, T3, T4&gt;"/>.</summary>
    public DetourScope Intercept(Func<T1, T2, T3, T4> replacement)
    {
        if (replacement == null) throw new ArgumentNullException(nameof(replacement));
        return interceptor.Intercept(replacement);
    }
}

/// <summary>Extension methods for static interceptors.</summary>
public static partial class StaticInterceptorExtensions
{
    /// <summary>Extension method for static method interceptor.</summary>
    public static StaticInterceptorFuncT4<TTarget, T1, T2, T3, T4> StaticJitest<TTarget, T1, T2, T3, T4>(this Type type, string methodName, out Func<T1, T2, T3, T4>? originalMethod)
    {
        if (type == null) throw new ArgumentNullException(nameof(type));
        var interceptor = type.Jitest<Func<T1, T2, T3, T4>>(methodName, out originalMethod);
        return new StaticInterceptorFuncT4<TTarget, T1, T2, T3, T4>(interceptor);
    }
}
