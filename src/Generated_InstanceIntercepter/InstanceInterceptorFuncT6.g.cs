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
    readonly Func<TTarget, Func<T1, T2, T3, T4, T5, T6>, DetourScope> interceptFunc;

    internal InstanceInterceptorFuncT6(Interceptor interceptor, Func<TTarget, Func<T1, T2, T3, T4, T5, T6>, DetourScope> interceptFunc)
    {
        this.interceptor = interceptor ?? throw new ArgumentNullException(nameof(interceptor));
        this.interceptFunc = interceptFunc ?? throw new ArgumentNullException(nameof(interceptFunc));
    }

    /// <summary>Intercepts instance method for specific instance with <see cref="Func&lt;T1, T2, T3, T4, T5, T6&gt;"/>.</summary>
    public DetourScope Intercept(TTarget instance, Func<T1, T2, T3, T4, T5, T6> replacement)
    {
        if (replacement == null) throw new ArgumentNullException(nameof(replacement));
        return interceptFunc(instance, replacement);
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
        var interceptor = instance.Jitest<Func<T1, T2, T3, T4, T5, T6>>(methodName, out var originalMethodUser);
        var actualMethod = originalMethodUser.Method is System.Reflection.Emit.DynamicMethod dm ? (Func<TTarget, T1, T2, T3, T4, T5, T6>)dm.CreateDelegate(typeof(Func<TTarget, T1, T2, T3, T4, T5, T6>)) : (Func<TTarget, T1, T2, T3, T4, T5, T6>)Delegate.CreateDelegate(typeof(Func<TTarget, T1, T2, T3, T4, T5, T6>), originalMethodUser.Method);
        originalMethod = (t1, t2, t3, t4, t5) => actualMethod.Invoke(instance, t1, t2, t3, t4, t5);
        return new InstanceInterceptorFuncT6<TTarget, T1, T2, T3, T4, T5, T6>(interceptor, (targetInst, replacement) =>
        {
            var actual = (TTarget self, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5) =>
                (typeof(TTarget).IsValueType ? EqualityComparer<TTarget>.Default.Equals(self, targetInst) : object.ReferenceEquals(self, targetInst))
                    ? replacement.Invoke(t1, t2, t3, t4, t5)
                    : actualMethod.Invoke(self, t1, t2, t3, t4, t5);
            return interceptor.Intercept(actual);
        });
    }
}
