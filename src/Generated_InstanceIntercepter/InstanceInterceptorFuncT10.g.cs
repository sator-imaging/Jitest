// Licensed under the Apache-2.0 License
// https://github.com/sator-imaging/Jitest

#nullable enable

using System;
using System.Collections.Generic;
using System.Reflection;

namespace Jitest;

/// <summary>Instance method interceptor for <see cref="Func&lt;T1, T2, T3, T4, T5, T6, T7, T8, T9, T10&gt;"/>.</summary>
public sealed class InstanceInterceptorFuncT10<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>
{
    readonly Interceptor interceptor;
    readonly Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>? originalMethod;

    internal InstanceInterceptorFuncT10(Interceptor interceptor, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> originalMethod)
    {
        this.interceptor = interceptor ?? throw new ArgumentNullException(nameof(interceptor));
        this.originalMethod = originalMethod;
    }

    /// <summary>Intercepts instance method for specific instance with <see cref="Func&lt;T1, T2, T3, T4, T5, T6, T7, T8, T9, T10&gt;"/>.</summary>
    public DetourScope Intercept(TTarget instance, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> replacement)
    {
        if (replacement == null) throw new ArgumentNullException(nameof(replacement));
        var actual = (TTarget self, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9) =>
            (typeof(TTarget).IsValueType ? EqualityComparer<TTarget>.Default.Equals(self, instance) : object.ReferenceEquals(self, instance))
                ? replacement.Invoke(t1, t2, t3, t4, t5, t6, t7, t8, t9)
                : originalMethod.Invoke(t1, t2, t3, t4, t5, t6, t7, t8, t9);
        return interceptor.Intercept(actual);
    }

    /// <summary>Intercepts instance method across all instances with <see cref="Func&lt;T1, T2, T3, T4, T5, T6, T7, T8, T9, T10&gt;"/>.</summary>
    public DetourScope InterceptUnsafe(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> replacement)
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
    public static InstanceInterceptorFuncT10<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> InstanceJitest<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this TTarget instance, string methodName, out Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>? originalMethod)
    {
        if (instance == null) throw new ArgumentNullException(nameof(instance));
        var interceptor = instance.Jitest<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>>(methodName, out originalMethod);
        return new InstanceInterceptorFuncT10<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(interceptor, originalMethod);
    }
}
