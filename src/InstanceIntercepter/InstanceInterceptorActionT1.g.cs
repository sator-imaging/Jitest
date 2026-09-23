// Licensed under the Apache-2.0 License
// https://github.com/sator-imaging/Jitest

#nullable enable

using System;
using System.Collections.Generic;
using System.Reflection;

namespace Jitest;

/// <summary>Instance method interceptor for <see cref="Action&lt;T1&gt;"/>.</summary>
public sealed class InstanceInterceptorActionT1<TTarget, T1>
{
    readonly Interceptor interceptor;
    readonly Action<T1>? originalMethod;

    internal InstanceInterceptorActionT1(Interceptor interceptor, Action<T1> originalMethod)
    {
        this.interceptor = interceptor ?? throw new ArgumentNullException(nameof(interceptor));
        this.originalMethod = originalMethod;
    }

    /// <summary>Intercepts instance method for specific instance with <see cref="Action&lt;T1&gt;"/>.</summary>
    public DetourScope Intercept(TTarget instance, Action<T1> replacement)
    {
        if (replacement == null) throw new ArgumentNullException(nameof(replacement));
        var actual = (TTarget self, T1 t1) =>
        {
            if (typeof(TTarget).IsValueType ? EqualityComparer<TTarget>.Default.Equals(self, instance) : object.ReferenceEquals(self, instance)) replacement.Invoke(t1);
            else originalMethod.Invoke(t1);
        };
        return interceptor.Intercept(actual);
    }

    /// <summary>Intercepts instance method across all instances with <see cref="Action&lt;T1&gt;"/>.</summary>
    public DetourScope InterceptUnsafe(Action<T1> replacement)
    {
        if (replacement == null) throw new ArgumentNullException(nameof(replacement));
        var actual = (TTarget self, T1 t1) => replacement.Invoke(t1);
        return interceptor.Intercept(actual);
    }
}

/// <summary>Extension methods for instance interceptors.</summary>
public static partial class InstanceInterceptorExtensions
{
    /// <summary>Extension method for instance method interceptor.</summary>
    public static InstanceInterceptorActionT1<TTarget, T1> InstanceJitest<TTarget, T1>(this TTarget instance, string methodName, out Action<T1>? originalMethod)
    {
        if (instance == null) throw new ArgumentNullException(nameof(instance));
        var interceptor = instance.Jitest<Action<T1>>(methodName, out originalMethod);
        return new InstanceInterceptorActionT1<TTarget, T1>(interceptor, originalMethod);
    }
}
