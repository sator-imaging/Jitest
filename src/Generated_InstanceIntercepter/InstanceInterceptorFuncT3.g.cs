// Licensed under the Apache-2.0 License
// https://github.com/sator-imaging/Jitest

#nullable enable

using System;
using System.Collections.Generic;
using System.Reflection;

namespace Jitest;

/// <summary>Instance method interceptor for <see cref="Func&lt;T1, T2, T3&gt;"/>.</summary>
public sealed class InstanceInterceptorFuncT3<TTarget, T1, T2, T3>
{
    readonly Interceptor interceptor;
    readonly Func<TTarget, T1, T2, T3> originalMethodInternal;

    internal InstanceInterceptorFuncT3(Interceptor interceptor, Func<TTarget, T1, T2, T3> originalMethodInternal)
    {
        this.interceptor = interceptor ?? throw new ArgumentNullException(nameof(interceptor));
        this.originalMethodInternal = originalMethodInternal;
    }

    /// <summary>Intercepts instance method for specific instance with <see cref="Func&lt;T1, T2, T3&gt;"/>.</summary>
    public DetourScope Intercept(TTarget instance, Func<T1, T2, T3> replacement)
    {
        if (replacement == null) throw new ArgumentNullException(nameof(replacement));
        var actual = (TTarget self, T1 t1, T2 t2) =>
            (typeof(TTarget).IsValueType ? EqualityComparer<TTarget>.Default.Equals(self, instance) : object.ReferenceEquals(self, instance))
                ? replacement.Invoke(t1, t2)
                : originalMethodInternal.Invoke(self, t1, t2);
        return interceptor.Intercept(actual);
    }

    /// <summary>Intercepts instance method across all instances with <see cref="Func&lt;T1, T2, T3&gt;"/>.</summary>
    public DetourScope InterceptUnsafe(Func<T1, T2, T3> replacement)
    {
        if (replacement == null) throw new ArgumentNullException(nameof(replacement));
        var actual = (TTarget self, T1 t1, T2 t2) => replacement.Invoke(t1, t2);
        return interceptor.Intercept(actual);
    }
}

/// <summary>Extension methods for instance interceptors.</summary>
public static partial class InstanceInterceptorExtensions
{
    /// <summary>Extension method for instance method interceptor.</summary>
    public static InstanceInterceptorFuncT3<TTarget, T1, T2, T3> InstanceJitest<TTarget, T1, T2, T3>(this TTarget instance, string methodName, out Func<T1, T2, T3> originalMethod)
    {
        if (instance == null) throw new ArgumentNullException(nameof(instance));
        var interceptor = instance.Jitest<Func<TTarget, T1, T2, T3>>(methodName, out var originalMethodInternal);
        originalMethod = (t1, t2) => originalMethodInternal.Invoke(instance, t1, t2);
        return new InstanceInterceptorFuncT3<TTarget, T1, T2, T3>(interceptor, originalMethodInternal);
    }
}
