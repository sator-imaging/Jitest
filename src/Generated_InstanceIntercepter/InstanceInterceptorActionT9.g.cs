// Licensed under the Apache-2.0 License
// https://github.com/sator-imaging/Jitest

#nullable enable

using System;
using System.Collections.Generic;
using System.Reflection;

namespace Jitest;

/// <summary>Instance method interceptor for <see cref="Action&lt;T1, T2, T3, T4, T5, T6, T7, T8, T9&gt;"/>.</summary>
public sealed class InstanceInterceptorActionT9<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9>
{
    readonly Interceptor interceptor;
    readonly Action<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9> originalMethodInternal;

    internal InstanceInterceptorActionT9(Interceptor interceptor, Action<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9> originalMethodInternal)
    {
        this.interceptor = interceptor ?? throw new ArgumentNullException(nameof(interceptor));
        this.originalMethodInternal = originalMethodInternal;
    }

    /// <summary>Intercepts instance method for specific instance with <see cref="Action&lt;T1, T2, T3, T4, T5, T6, T7, T8, T9&gt;"/>.</summary>
    public DetourScope Intercept(TTarget instance, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> replacement)
    {
        if (replacement == null) throw new ArgumentNullException(nameof(replacement));
        var actual = (TTarget self, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9) =>
        {
            if (typeof(TTarget).IsValueType ? EqualityComparer<TTarget>.Default.Equals(self, instance) : object.ReferenceEquals(self, instance))
                replacement.Invoke(t1, t2, t3, t4, t5, t6, t7, t8, t9);
            else
                originalMethodInternal.Invoke(self, t1, t2, t3, t4, t5, t6, t7, t8, t9);
        };
        return interceptor.Intercept(actual);
    }

    /// <summary>Intercepts instance method across all instances with <see cref="Action&lt;T1, T2, T3, T4, T5, T6, T7, T8, T9&gt;"/>.</summary>
    public DetourScope InterceptUnsafe(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> replacement)
    {
        if (replacement == null) throw new ArgumentNullException(nameof(replacement));
        var actual = (TTarget self, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9) => replacement.Invoke(t1, t2, t3, t4, t5, t6, t7, t8, t9);
        return interceptor.Intercept(actual);
    }
}

/// <summary>Extension methods for instance interceptors.</summary>
public static partial class InstanceInterceptorExtensions
{
    /// <summary>Extension method for instance method interceptor.</summary>
    public static InstanceInterceptorActionT9<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9> InstanceJitest<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9>(this TTarget instance, string methodName, out Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> originalMethod)
    {
        if (instance == null) throw new ArgumentNullException(nameof(instance));
        var interceptor = instance.Jitest<Action<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9>>(methodName, out var originalMethodInternal);
        originalMethod = (t1, t2, t3, t4, t5, t6, t7, t8, t9) => originalMethodInternal.Invoke(instance, t1, t2, t3, t4, t5, t6, t7, t8, t9);
        return new InstanceInterceptorActionT9<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9>(interceptor, originalMethodInternal);
    }
}
