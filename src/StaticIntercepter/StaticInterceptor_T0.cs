// Licensed under the Apache-2.0 License
// https://github.com/sator-imaging/Jitest

using System;
using System.Reflection;

namespace Jitest;

/// <summary>Static method interceptor.</summary>
public sealed class StaticInterceptor<TTarget>
{
    readonly MethodInfo target;
    readonly Delegate? originalMethod;

    internal StaticInterceptor(MethodInfo target, Delegate? originalMethod)
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
public static class StaticInterceptorExtensions_T0
{
    /// <summary>Extension method for static method interceptor.</summary>
    public static StaticInterceptor<TTarget> StaticJitest<TTarget>(this Type type, string methodName, out Action? originalMethod)
    {
        var (method, dele) = ResolveMethodHelper.ResolveMethod<Action>(type, methodName);
        originalMethod = dele;
        return new StaticInterceptor<TTarget>(method, dele);
    }
}