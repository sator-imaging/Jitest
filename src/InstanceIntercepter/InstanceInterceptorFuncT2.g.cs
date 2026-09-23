// Licensed under the Apache-2.0 License
// https://github.com/sator-imaging/Jitest

#nullable enable

using System;
using System.Reflection;

namespace Jitest;

/// <summary>Instance method interceptor for <see cref="Func&lt;T1, T2&gt;"/>.</summary>
public sealed class InstanceInterceptorFuncT2<TTarget, T1, T2>
{
    readonly Interceptor interceptor;
    readonly Func<T1, T2>? originalMethod;

    internal InstanceInterceptorFuncT2(Interceptor interceptor, Func<T1, T2>? originalMethod)
    {
        this.interceptor = interceptor ?? throw new ArgumentNullException(nameof(interceptor));
        this.originalMethod = originalMethod;
    }

    /// <summary>Intercepts instance method for specific instance with <see cref="Func&lt;T1, T2&gt;"/>.</summary>
    public DetourScope Intercept(TTarget instance, Func<T1, T2> replacement)
    {
        if (replacement == null) throw new ArgumentNullException(nameof(replacement));
        Func<TTarget, T1, T2> actual = (TTarget self, T1 t1) =>
            object.ReferenceEquals(self, instance)
                ? replacement.Invoke(t1)
                : (originalMethod != null ? originalMethod.Invoke(t1) : default!);
        return interceptor.Intercept(actual);
    }

    /// <summary>Intercepts instance method across all instances with <see cref="Func&lt;T1, T2&gt;"/>.</summary>
    public DetourScope InterceptUnsafe(Func<T1, T2> replacement)
    {
        if (replacement == null) throw new ArgumentNullException(nameof(replacement));
        Func<TTarget, T1, T2> actual = (TTarget self, T1 t1) => replacement.Invoke(t1);
        return interceptor.Intercept(actual);
    }
}

/// <summary>Extension methods for instance interceptors.</summary>
public static partial class InstanceInterceptorExtensions
{
    /// <summary>Extension method for instance method interceptor.</summary>
    public static InstanceInterceptorFuncT2<TTarget, T1, T2> InstanceJitest<TTarget, T1, T2>(this TTarget instance, string methodName, out Func<T1, T2>? originalMethod)
    {
        if (instance == null) throw new ArgumentNullException(nameof(instance));
        var interceptor = instance.Jitest<Func<T1, T2>>(methodName, out originalMethod);
        return new InstanceInterceptorFuncT2<TTarget, T1, T2>(interceptor, originalMethod);
    }
}
