// Licensed under the Apache-2.0 License
// https://github.com/sator-imaging/Jitest

#nullable enable

using System;
using System.Collections.Generic;
using System.Reflection;

namespace Jitest;

/// <summary>Static method interceptor for <see cref="Action"/>.</summary>
public sealed class StaticInterceptorActionT0<TTarget>
{
    readonly Interceptor interceptor;

    internal StaticInterceptorActionT0(Interceptor interceptor)
    {
        this.interceptor = interceptor ?? throw new ArgumentNullException(nameof(interceptor));
    }

    /// <summary>Intercepts static method for <see cref="Action"/>.</summary>
    public DetourScope Intercept(Action replacement)
    {
        if (replacement == null) throw new ArgumentNullException(nameof(replacement));
        return interceptor.Intercept(replacement);
    }
}

/// <summary>Extension methods for static interceptors.</summary>
public static partial class StaticInterceptorExtensions
{
    /// <summary>Extension method for static method interceptor.</summary>
    public static StaticInterceptorActionT0<TTarget> StaticJitest<TTarget>(this Type type, string methodName, out Action originalMethod)
    {
        if (type == null) throw new ArgumentNullException(nameof(type));
        var interceptor = type.Jitest<Action>(methodName, out originalMethod);
        return new StaticInterceptorActionT0<TTarget>(interceptor);
    }
}
