// Licensed under the Apache-2.0 License
// https://github.com/sator-imaging/Jitest

#nullable enable

using System;
using System.Reflection;

namespace Jitest;

/// <summary>Instance method interceptor for <see cref="Func&lt;T1, T2, T3, T4, T5, T6, T7&gt;"/>.</summary>
public sealed class InstanceInterceptorFuncT7<TTarget, T1, T2, T3, T4, T5, T6, T7>
{
    readonly Interceptor interceptor;
    readonly Func<T1, T2, T3, T4, T5, T6, T7>? originalMethod;

    internal InstanceInterceptorFuncT7(Interceptor interceptor, Func<T1, T2, T3, T4, T5, T6, T7>? originalMethod)
    {
        this.interceptor = interceptor ?? throw new ArgumentNullException(nameof(interceptor));
        this.originalMethod = originalMethod;
    }

    /// <summary>Intercepts instance method for specific instance with <see cref="Func&lt;T1, T2, T3, T4, T5, T6, T7&gt;"/>.</summary>
    public DetourScope Intercept(TTarget instance, Func<T1, T2, T3, T4, T5, T6, T7> replacement)
    {
        if (replacement == null) throw new ArgumentNullException(nameof(replacement));
        Func<TTarget, T1, T2, T3, T4, T5, T6, T7> actual = (TTarget self, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6) =>
            object.ReferenceEquals(self, instance)
                ? replacement.Invoke(t1, t2, t3, t4, t5, t6)
                : (originalMethod != null ? originalMethod.Invoke(t1, t2, t3, t4, t5, t6) : default!);
        return interceptor.Intercept(actual);
    }

    /// <summary>Intercepts instance method across all instances with <see cref="Func&lt;T1, T2, T3, T4, T5, T6, T7&gt;"/>.</summary>
    public DetourScope InterceptUnsafe(Func<T1, T2, T3, T4, T5, T6, T7> replacement)
    {
        if (replacement == null) throw new ArgumentNullException(nameof(replacement));
        Func<TTarget, T1, T2, T3, T4, T5, T6, T7> actual = (TTarget self, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6) => replacement.Invoke(t1, t2, t3, t4, t5, t6);
        return interceptor.Intercept(actual);
    }
}

/// <summary>Extension methods for instance interceptors.</summary>
public static partial class InstanceInterceptorExtensions
{
    /// <summary>Extension method for instance method interceptor.</summary>
    public static InstanceInterceptorFuncT7<TTarget, T1, T2, T3, T4, T5, T6, T7> InstanceJitest<TTarget, T1, T2, T3, T4, T5, T6, T7>(this TTarget instance, string methodName, out Func<T1, T2, T3, T4, T5, T6, T7>? originalMethod)
    {
        if (instance == null) throw new ArgumentNullException(nameof(instance));
        var interceptor = instance.Jitest<Func<T1, T2, T3, T4, T5, T6, T7>>(methodName, out originalMethod);
        return new InstanceInterceptorFuncT7<TTarget, T1, T2, T3, T4, T5, T6, T7>(interceptor, originalMethod);
    }
}
