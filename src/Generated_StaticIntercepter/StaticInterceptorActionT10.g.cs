// Licensed under the Apache-2.0 License
// https://github.com/sator-imaging/Jitest

#nullable enable

using System;
using System.Collections.Generic;
using System.Reflection;

namespace Jitest;

/// <summary>Static method interceptor for <see cref="Action&lt;T1, T2, T3, T4, T5, T6, T7, T8, T9, T10&gt;"/>.</summary>
public sealed class StaticInterceptorActionT10<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>
{
    readonly Interceptor interceptor;

    internal StaticInterceptorActionT10(Interceptor interceptor)
    {
        this.interceptor = interceptor ?? throw new ArgumentNullException(nameof(interceptor));
    }

    /// <summary>Intercepts static method for <see cref="Action&lt;T1, T2, T3, T4, T5, T6, T7, T8, T9, T10&gt;"/>.</summary>
    public DetourScope Intercept(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> replacement)
    {
        if (replacement == null) throw new ArgumentNullException(nameof(replacement));
        return interceptor.Intercept(replacement);
    }
}

/// <summary>Extension methods for static interceptors.</summary>
public static partial class StaticInterceptorExtensions
{
    /// <summary>Extension method for static method interceptor.</summary>
    public static StaticInterceptorActionT10<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> StaticJitest<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this Type type, string methodName, out Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>? originalMethod)
    {
        if (type == null) throw new ArgumentNullException(nameof(type));
        var interceptor = type.Jitest<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>>(methodName, out originalMethod);
        return new StaticInterceptorActionT10<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(interceptor);
    }
}
