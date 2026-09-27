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
    readonly Func<TTarget, Action<T1, T2, T3, T4>, DetourScope> interceptFunc;

    internal InstanceInterceptorActionT4(Interceptor interceptor, Func<TTarget, Action<T1, T2, T3, T4>, DetourScope> interceptFunc)
    {
        this.interceptor = interceptor ?? throw new ArgumentNullException(nameof(interceptor));
        this.interceptFunc = interceptFunc ?? throw new ArgumentNullException(nameof(interceptFunc));
    }

    /// <summary>Intercepts instance method for specific instance with <see cref="Action&lt;T1, T2, T3, T4&gt;"/>.</summary>
    public DetourScope Intercept(TTarget instance, Action<T1, T2, T3, T4> replacement)
    {
        if (replacement == null) throw new ArgumentNullException(nameof(replacement));
        return interceptFunc(instance, replacement);
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
        var interceptor = instance.Jitest<Action<T1, T2, T3, T4>>(methodName, out var originalMethodUser);
        var actualMethod = originalMethodUser.Method is System.Reflection.Emit.DynamicMethod dm ? (Action<TTarget, T1, T2, T3, T4>)dm.CreateDelegate(typeof(Action<TTarget, T1, T2, T3, T4>)) : (Action<TTarget, T1, T2, T3, T4>)Delegate.CreateDelegate(typeof(Action<TTarget, T1, T2, T3, T4>), originalMethodUser.Method);
        originalMethod = (t1, t2, t3, t4) => actualMethod.Invoke(instance, t1, t2, t3, t4);
        return new InstanceInterceptorActionT4<TTarget, T1, T2, T3, T4>(interceptor, (targetInst, replacement) =>
        {
            var actual = (TTarget self, T1 t1, T2 t2, T3 t3, T4 t4) =>
            {
                if (typeof(TTarget).IsValueType ? EqualityComparer<TTarget>.Default.Equals(self, targetInst) : object.ReferenceEquals(self, targetInst))
                    replacement.Invoke(t1, t2, t3, t4);
                else
                    actualMethod.Invoke(self, t1, t2, t3, t4);
            };
            return interceptor.Intercept(actual);
        });
    }
}
