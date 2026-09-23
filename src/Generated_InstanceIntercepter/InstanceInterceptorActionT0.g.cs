// Licensed under the Apache-2.0 License
// https://github.com/sator-imaging/Jitest

#nullable enable

using System;
using System.Collections.Generic;
using System.Reflection;

namespace Jitest;

/// <summary>Instance method interceptor for <see cref="Action"/>.</summary>
public sealed class InstanceInterceptorActionT0<TTarget>
{
    readonly Interceptor interceptor;
    readonly Action? originalMethod;

    internal InstanceInterceptorActionT0(Interceptor interceptor, Action originalMethod)
    {
        this.interceptor = interceptor ?? throw new ArgumentNullException(nameof(interceptor));
        this.originalMethod = originalMethod;
    }

    /// <summary>Intercepts instance method for specific instance with <see cref="Action"/>.</summary>
    public DetourScope Intercept(TTarget instance, Action replacement)
    {
        if (replacement == null) throw new ArgumentNullException(nameof(replacement));
        var actual = (TTarget self) =>
        {
            if (typeof(TTarget).IsValueType ? EqualityComparer<TTarget>.Default.Equals(self, instance) : object.ReferenceEquals(self, instance))
                replacement.Invoke();
            else
                originalMethod.Invoke();
        };
        return interceptor.Intercept(actual);
    }

    /// <summary>Intercepts instance method across all instances with <see cref="Action"/>.</summary>
    public DetourScope InterceptUnsafe(Action replacement)
    {
        if (replacement == null) throw new ArgumentNullException(nameof(replacement));
        var actual = (TTarget self) => replacement.Invoke();
        return interceptor.Intercept(actual);
    }
}

/// <summary>Extension methods for instance interceptors.</summary>
public static partial class InstanceInterceptorExtensions
{
    /// <summary>Extension method for instance method interceptor.</summary>
    public static InstanceInterceptorActionT0<TTarget> InstanceJitest<TTarget>(this TTarget instance, string methodName, out Action? originalMethod)
    {
        if (instance == null) throw new ArgumentNullException(nameof(instance));
        var interceptor = instance.Jitest<Action>(methodName, out originalMethod);
        return new InstanceInterceptorActionT0<TTarget>(interceptor, originalMethod);
    }
}
