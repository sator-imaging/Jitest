// Licensed under the Apache-2.0 License
// https://github.com/sator-imaging/Jitest

#nullable enable

using System;
using System.Collections.Generic;
using System.Reflection;

namespace Jitest;

/// <summary>Static method interceptor for <see cref="Func&lt;T1, T2&gt;"/>.</summary>
public sealed class StaticInterceptorFuncT2<TTarget, T1, T2>
{
    readonly Interceptor interceptor;

    internal StaticInterceptorFuncT2(Interceptor interceptor)
    {
        this.interceptor = interceptor ?? throw new ArgumentNullException(nameof(interceptor));
    }

    /// <summary>Intercepts static method for <see cref="Func&lt;T1, T2&gt;"/>.</summary>
    public DetourScope Intercept(Func<T1, T2> replacement)
    {
        if (replacement == null) throw new ArgumentNullException(nameof(replacement));
        return interceptor.Intercept(replacement);
    }
}

/// <summary>Extension methods for static interceptors.</summary>
public static partial class StaticInterceptorExtensions
{
    /// <summary>Extension method for static method interceptor.</summary>
    public static StaticInterceptorFuncT2<TTarget, T1, T2> StaticJitest<TTarget, T1, T2>(this Type type, string methodName, out Func<T1, T2>? originalMethod)
    {
        if (type == null) throw new ArgumentNullException(nameof(type));
        var interceptor = type.Jitest<Func<T1, T2>>(methodName, out originalMethod);
        return new StaticInterceptorFuncT2<TTarget, T1, T2>(interceptor);
    }
}
