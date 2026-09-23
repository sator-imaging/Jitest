// Licensed under the Apache-2.0 License
// https://github.com/sator-imaging/Jitest

#nullable enable

using System;
using System.Collections.Generic;
using System.Reflection;

namespace Jitest;

/// <summary>Instance method interceptor for <see cref="Func&lt;T1&gt;"/>.</summary>
public sealed class InstanceInterceptorFuncT1<TTarget, T1>
{
    readonly Interceptor interceptor;
    readonly Func<T1>? originalMethod;

    internal InstanceInterceptorFuncT1(Interceptor interceptor, Func<T1> originalMethod)
    {
        this.interceptor = interceptor ?? throw new ArgumentNullException(nameof(interceptor));
        this.originalMethod = originalMethod;
    }

    /// <summary>Intercepts instance method for specific instance with <see cref="Func&lt;T1&gt;"/>.</summary>
    public DetourScope Intercept(TTarget instance, Func<T1> replacement)
    {
        if (replacement == null) throw new ArgumentNullException(nameof(replacement));
        var actual = (TTarget self) =>
            (typeof(TTarget).IsValueType ? EqualityComparer<TTarget>.Default.Equals(self, instance) : object.ReferenceEquals(self, instance))
                ? replacement.Invoke()
                : originalMethod.Invoke();
        return interceptor.Intercept(actual);
    }

    /// <summary>Intercepts instance method across all instances with <see cref="Func&lt;T1&gt;"/>.</summary>
    public DetourScope InterceptUnsafe(Func<T1> replacement)
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
    public static InstanceInterceptorFuncT1<TTarget, T1> InstanceJitest<TTarget, T1>(this TTarget instance, string methodName, out Func<T1>? originalMethod)
    {
        if (instance == null) throw new ArgumentNullException(nameof(instance));
        var interceptor = instance.Jitest<Func<T1>>(methodName, out originalMethod);
        return new InstanceInterceptorFuncT1<TTarget, T1>(interceptor, originalMethod);
    }
}
