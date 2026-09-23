// Licensed under the Apache-2.0 License
// https://github.com/sator-imaging/Jitest

#nullable enable

using System;
using System.Collections.Generic;
using System.Reflection;

namespace Jitest;

/// <summary>Instance method interceptor for <see cref="Action&lt;T1, T2, T3, T4&gt;"/>.</summary>
public sealed class InstanceInterceptorActionT4<TTarget, T1, T2, T3, T4>
{
    readonly Interceptor interceptor;
    readonly Action<T1, T2, T3, T4> originalMethod;

    internal InstanceInterceptorActionT4(Interceptor interceptor, Action<T1, T2, T3, T4> originalMethod)
    {
        this.interceptor = interceptor ?? throw new ArgumentNullException(nameof(interceptor));
        this.originalMethod = originalMethod;
    }

    /// <summary>Intercepts instance method for specific instance with <see cref="Action&lt;T1, T2, T3, T4&gt;"/>.</summary>
    public DetourScope Intercept(TTarget instance, Action<T1, T2, T3, T4> replacement)
    {
        if (replacement == null) throw new ArgumentNullException(nameof(replacement));
        var actual = (TTarget self, T1 t1, T2 t2, T3 t3, T4 t4) =>
        {
            if (typeof(TTarget).IsValueType ? EqualityComparer<TTarget>.Default.Equals(self, instance) : object.ReferenceEquals(self, instance))
                replacement.Invoke(t1, t2, t3, t4);
            else
                originalMethod.Invoke(t1, t2, t3, t4);
        };
        return interceptor.Intercept(actual);
    }

    /// <summary>Intercepts instance method across all instances with <see cref="Action&lt;T1, T2, T3, T4&gt;"/>.</summary>
    public DetourScope InterceptUnsafe(Action<T1, T2, T3, T4> replacement)
    {
        if (replacement == null) throw new ArgumentNullException(nameof(replacement));
        var actual = (TTarget self, T1 t1, T2 t2, T3 t3, T4 t4) => replacement.Invoke(t1, t2, t3, t4);
        return interceptor.Intercept(actual);
    }
}

/// <summary>Extension methods for instance interceptors.</summary>
public static partial class InstanceInterceptorExtensions
{
    /// <summary>Extension method for instance method interceptor.</summary>
    public static InstanceInterceptorActionT4<TTarget, T1, T2, T3, T4> InstanceJitest<TTarget, T1, T2, T3, T4>(this TTarget instance, string methodName, out Action<T1, T2, T3, T4> originalMethod)
    {
        if (instance == null) throw new ArgumentNullException(nameof(instance));
        var interceptor = instance.Jitest<Action<T1, T2, T3, T4>>(methodName, out originalMethod);
        return new InstanceInterceptorActionT4<TTarget, T1, T2, T3, T4>(interceptor, originalMethod);
    }
}
