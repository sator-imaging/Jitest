// Licensed under the Apache-2.0 License
// https://github.com/sator-imaging/Jitest

using System;
using System.Reflection;

namespace Jitest;

/// <summary>Instance method interceptor.</summary>
public sealed class InstanceInterceptor<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>
{
    readonly MethodInfo target;
    readonly Delegate? originalMethod;

    internal InstanceInterceptor(MethodInfo target, Delegate? originalMethod)
    {
        this.target = target ?? throw new ArgumentNullException(nameof(target));
        this.originalMethod = originalMethod;
    }

    /// <summary>Intercepts instance method for specific instance with <see cref="Func&lt;T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16&gt;"/>.</summary>
    public DetourScope Intercept(TTarget instance, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> replacement)
    {
        if (replacement == null) throw new ArgumentNullException(nameof(replacement));
        var orig = originalMethod != null ? (Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>)(object)originalMethod : null;
        Func<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> actual = (TTarget self, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11, T12 t12, T13 t13, T14 t14, T15 t15) =>
            object.ReferenceEquals(self, instance)
                ? replacement.Invoke(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14, t15)
                : (orig != null ? orig.Invoke(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14, t15) : default!);
        return DetourScope.Create(target, actual, instance);
    }

    /// <summary>Intercepts instance method across all instances with <see cref="Func&lt;T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16&gt;"/>.</summary>
    public DetourScope InterceptUnsafe(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> replacement)
    {
        if (replacement == null) throw new ArgumentNullException(nameof(replacement));
        Func<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> actual = (TTarget self, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11, T12 t12, T13 t13, T14 t14, T15 t15) => replacement.Invoke(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14, t15);
        return DetourScope.Create(target, actual, instance: null);
    }
}

/// <summary>Extension methods for instance interceptors.</summary>
public static class InstanceInterceptorExtensions_T16
{
    /// <summary>Extension method for instance method interceptor.</summary>
    public static InstanceInterceptor<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> InstanceJitest<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(this TTarget instance, string methodName, out Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>? originalMethod)
    {
        if (instance == null) throw new ArgumentNullException(nameof(instance));
        if (typeof(TTarget).IsValueType)
        {
            var (method, _) = ResolveMethodHelper.ResolveMethod<Delegate>(typeof(TTarget), methodName);
            originalMethod = null;
            return new InstanceInterceptor<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(method, null);
        }
        else
        {
            var (method, dele) = ResolveMethodHelper.ResolveMethod<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>>(typeof(TTarget), methodName);
            originalMethod = dele;
            return new InstanceInterceptor<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(method, dele);
        }
    }
}