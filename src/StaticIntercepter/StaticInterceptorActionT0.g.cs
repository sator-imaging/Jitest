// Licensed under the Apache-2.0 License
// https://github.com/sator-imaging/Jitest

using System;
using System.Reflection;

namespace Jitest;

/// <summary>Static method interceptor for <see cref="Action"/>.</summary>
public sealed class StaticInterceptorActionT0<TTarget>
{
    readonly MethodInfo target;
    readonly Delegate? originalMethod;

    internal StaticInterceptorActionT0(MethodInfo target, Delegate? originalMethod)
    {
        this.target = target ?? throw new ArgumentNullException(nameof(target));
        this.originalMethod = originalMethod;
    }

    /// <summary>Intercepts static method for <see cref="Action"/>.</summary>
    public DetourScope Intercept(Action replacement)
    {
        if (replacement == null) throw new ArgumentNullException(nameof(replacement));
        return DetourScope.Create(target, replacement, instance: null);
    }
}

/// <summary>Extension methods for static interceptors.</summary>
public static partial class StaticInterceptorExtensions
{
    /// <summary>Extension method for static method interceptor.</summary>
    public static StaticInterceptorActionT0<TTarget> StaticJitest<TTarget>(this Type type, string methodName, out Action? originalMethod)
    {
        var (method, dele) = Extensions.ResolveMethod<Action>(type, methodName);
        originalMethod = dele;
        return new StaticInterceptorActionT0<TTarget>(method, dele);
    }
}