// Licensed under the Apache-2.0 License
// https://github.com/sator-imaging/Jitest

#nullable enable

using System;
using System.Collections.Generic;
using System.Reflection;

namespace Jitest;

/// <summary>Instance method interceptor for <see cref="Action&lt;T1, T2&gt;"/>.</summary>
public sealed class InstanceInterceptorActionT2<TTarget, T1, T2>
{
    readonly Interceptor interceptor;
    readonly Action<TTarget, T1, T2> originalMethodInternal;

    internal InstanceInterceptorActionT2(Interceptor interceptor, Action<TTarget, T1, T2> originalMethodInternal)
    {
        this.interceptor = interceptor ?? throw new ArgumentNullException(nameof(interceptor));
        this.originalMethodInternal = originalMethodInternal;
    }

    /// <summary>Intercepts instance method for specific instance with <see cref="Action&lt;T1, T2&gt;"/>.</summary>
    public DetourScope Intercept(TTarget instance, Action<T1, T2> replacement)
    {
        if (replacement == null) throw new ArgumentNullException(nameof(replacement));
        var actual = (TTarget self, T1 t1, T2 t2) =>
        {
            if (typeof(TTarget).IsValueType ? EqualityComparer<TTarget>.Default.Equals(self, instance) : object.ReferenceEquals(self, instance))
                replacement.Invoke(t1, t2);
            else
                originalMethodInternal.Invoke(self, t1, t2);
        };
        return interceptor.Intercept(actual);
    }

    /// <summary>Intercepts instance method across all instances with <see cref="Action&lt;T1, T2&gt;"/>.</summary>
    public DetourScope InterceptUnsafe(Action<T1, T2> replacement)
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
    public static InstanceInterceptorActionT2<TTarget, T1, T2> InstanceJitest<TTarget, T1, T2>(this TTarget instance, string methodName, out Action<T1, T2> originalMethod)
    {
        if (instance == null) throw new ArgumentNullException(nameof(instance));
        var interceptor = instance.Jitest<Action<TTarget, T1, T2>>(methodName, out var originalMethodInternal);
        originalMethod = (t1, t2) => originalMethodInternal.Invoke(instance, t1, t2);
        return new InstanceInterceptorActionT2<TTarget, T1, T2>(interceptor, originalMethodInternal);
    }
}
