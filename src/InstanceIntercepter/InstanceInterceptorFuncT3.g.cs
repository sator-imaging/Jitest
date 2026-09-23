// Licensed under the Apache-2.0 License
// https://github.com/sator-imaging/Jitest

#nullable enable

using System;
using System.Reflection;

namespace Jitest;

/// <summary>Instance method interceptor for <see cref="Func&lt;T1, T2, T3&gt;"/>.</summary>
public sealed class InstanceInterceptorFuncT3<TTarget, T1, T2, T3>
{
    readonly Interceptor interceptor;
    readonly Func<T1, T2, T3>? originalMethod;

    internal InstanceInterceptorFuncT3(Interceptor interceptor, Func<T1, T2, T3>? originalMethod)
    {
        this.interceptor = interceptor ?? throw new ArgumentNullException(nameof(interceptor));
        this.originalMethod = originalMethod;
    }

    /// <summary>Intercepts instance method for specific instance with <see cref="Func&lt;T1, T2, T3&gt;"/>.</summary>
    public DetourScope Intercept(TTarget instance, Func<T1, T2, T3> replacement)
    {
        if (replacement == null) throw new ArgumentNullException(nameof(replacement));
        Func<TTarget, T1, T2, T3> actual = (TTarget self, T1 t1, T2 t2) =>
            object.ReferenceEquals(self, instance)
                ? replacement.Invoke(t1, t2)
                : (originalMethod != null ? originalMethod.Invoke(t1, t2) : default!);
        return interceptor.Intercept(actual);
    }

    /// <summary>Intercepts instance method across all instances with <see cref="Func&lt;T1, T2, T3&gt;"/>.</summary>
    public DetourScope InterceptUnsafe(Func<T1, T2, T3> replacement)
    {
        if (replacement == null) throw new ArgumentNullException(nameof(replacement));
        Func<TTarget, T1, T2, T3> actual = (TTarget self, T1 t1, T2 t2) => replacement.Invoke(t1, t2);
        return interceptor.Intercept(actual);
    }
}

/// <summary>Extension methods for instance interceptors.</summary>
public static partial class InstanceInterceptorExtensions
{
    /// <summary>Extension method for instance method interceptor.</summary>
    public static InstanceInterceptorFuncT3<TTarget, T1, T2, T3> InstanceJitest<TTarget, T1, T2, T3>(this TTarget instance, string methodName, out Func<T1, T2, T3>? originalMethod)
    {
        if (instance == null) throw new ArgumentNullException(nameof(instance));
        var interceptor = instance.Jitest<Func<T1, T2, T3>>(methodName, out originalMethod);
        return new InstanceInterceptorFuncT3<TTarget, T1, T2, T3>(interceptor, originalMethod);
    }
}
