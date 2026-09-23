// Licensed under the Apache-2.0 License
// https://github.com/sator-imaging/Jitest

#nullable enable

using System;
using System.Collections.Generic;
using System.Reflection;

namespace Jitest;

/// <summary>Instance method interceptor for <see cref="Func&lt;T1, T2, T3, T4, T5, T6&gt;"/>.</summary>
public sealed class InstanceInterceptorFuncT6<TTarget, T1, T2, T3, T4, T5, T6>
{
    readonly Interceptor interceptor;
    readonly Func<TTarget, T1, T2, T3, T4, T5, T6> originalMethodInternal;

    internal InstanceInterceptorFuncT6(Interceptor interceptor, Func<TTarget, T1, T2, T3, T4, T5, T6> originalMethodInternal)
    {
        this.interceptor = interceptor ?? throw new ArgumentNullException(nameof(interceptor));
        this.originalMethodInternal = originalMethodInternal;
    }

    /// <summary>Intercepts instance method for specific instance with <see cref="Func&lt;T1, T2, T3, T4, T5, T6&gt;"/>.</summary>
    public DetourScope Intercept(TTarget instance, Func<T1, T2, T3, T4, T5, T6> replacement)
    {
        if (replacement == null) throw new ArgumentNullException(nameof(replacement));
        var actual = (TTarget self, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5) =>
            (typeof(TTarget).IsValueType ? EqualityComparer<TTarget>.Default.Equals(self, instance) : object.ReferenceEquals(self, instance))
                ? replacement.Invoke(t1, t2, t3, t4, t5)
                : originalMethodInternal.Invoke(self, t1, t2, t3, t4, t5);
        return interceptor.Intercept(actual);
    }

    /// <summary>Intercepts instance method across all instances with <see cref="Func&lt;T1, T2, T3, T4, T5, T6&gt;"/>.</summary>
    public DetourScope InterceptUnsafe(Func<T1, T2, T3, T4, T5, T6> replacement)
    {
        if (replacement == null) throw new ArgumentNullException(nameof(replacement));
        var actual = (TTarget self, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5) => replacement.Invoke(t1, t2, t3, t4, t5);
        return interceptor.Intercept(actual);
    }
}

/// <summary>Extension methods for instance interceptors.</summary>
public static partial class InstanceInterceptorExtensions
{
    /// <summary>Extension method for instance method interceptor.</summary>
    public static InstanceInterceptorFuncT6<TTarget, T1, T2, T3, T4, T5, T6> InstanceJitest<TTarget, T1, T2, T3, T4, T5, T6>(this TTarget instance, string methodName, out Func<T1, T2, T3, T4, T5, T6> originalMethod)
    {
        if (instance == null) throw new ArgumentNullException(nameof(instance));
        var interceptor = instance.Jitest<Func<TTarget, T1, T2, T3, T4, T5, T6>>(methodName, out var originalMethodInternal);
        originalMethod = (t1, t2, t3, t4, t5) => originalMethodInternal.Invoke(instance, t1, t2, t3, t4, t5);
        return new InstanceInterceptorFuncT6<TTarget, T1, T2, T3, T4, T5, T6>(interceptor, originalMethodInternal);
    }
}
