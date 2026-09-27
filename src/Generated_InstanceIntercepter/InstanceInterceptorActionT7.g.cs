// Licensed under the Apache-2.0 License
// https://github.com/sator-imaging/Jitest

#nullable enable

using System;
using System.Collections.Generic;
using System.Reflection;

namespace Jitest;

/// <summary>Instance method interceptor for <see cref="Action&lt;T1, T2, T3, T4, T5, T6, T7&gt;"/>.</summary>
public sealed class InstanceInterceptorActionT7<TTarget, T1, T2, T3, T4, T5, T6, T7>
{
    readonly Interceptor interceptor;
    readonly Func<TTarget, Action<T1, T2, T3, T4, T5, T6, T7>, DetourScope> interceptFunc;

    internal InstanceInterceptorActionT7(Interceptor interceptor, Func<TTarget, Action<T1, T2, T3, T4, T5, T6, T7>, DetourScope> interceptFunc)
    {
        this.interceptor = interceptor ?? throw new ArgumentNullException(nameof(interceptor));
        this.interceptFunc = interceptFunc ?? throw new ArgumentNullException(nameof(interceptFunc));
    }

    /// <summary>Intercepts instance method for specific instance with <see cref="Action&lt;T1, T2, T3, T4, T5, T6, T7&gt;"/>.</summary>
    public DetourScope Intercept(TTarget instance, Action<T1, T2, T3, T4, T5, T6, T7> replacement)
    {
        if (replacement == null) throw new ArgumentNullException(nameof(replacement));
        return interceptFunc(instance, replacement);
    }

    /// <summary>Intercepts instance method across all instances with <see cref="Action&lt;T1, T2, T3, T4, T5, T6, T7&gt;"/>.</summary>
    public DetourScope InterceptUnsafe(Action<T1, T2, T3, T4, T5, T6, T7> replacement)
    {
        if (replacement == null) throw new ArgumentNullException(nameof(replacement));
        var actual = (TTarget self, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7) => replacement.Invoke(t1, t2, t3, t4, t5, t6, t7);
        return interceptor.Intercept(actual);
    }
}

/// <summary>Extension methods for instance interceptors.</summary>
public static partial class InstanceInterceptorExtensions
{
    /// <summary>Extension method for instance method interceptor.</summary>
    public static InstanceInterceptorActionT7<TTarget, T1, T2, T3, T4, T5, T6, T7> InstanceJitest<TTarget, T1, T2, T3, T4, T5, T6, T7>(this TTarget instance, string methodName, out Action<T1, T2, T3, T4, T5, T6, T7> originalMethod)
    {
        if (instance == null) throw new ArgumentNullException(nameof(instance));
        var interceptor = instance.Jitest<Action<T1, T2, T3, T4, T5, T6, T7>>(methodName, out var originalMethodUser);
        var actualMethod = originalMethodUser.Method is System.Reflection.Emit.DynamicMethod dm ? (Action<TTarget, T1, T2, T3, T4, T5, T6, T7>)dm.CreateDelegate(typeof(Action<TTarget, T1, T2, T3, T4, T5, T6, T7>)) : (Action<TTarget, T1, T2, T3, T4, T5, T6, T7>)Delegate.CreateDelegate(typeof(Action<TTarget, T1, T2, T3, T4, T5, T6, T7>), originalMethodUser.Method);
        originalMethod = (t1, t2, t3, t4, t5, t6, t7) => actualMethod.Invoke(instance, t1, t2, t3, t4, t5, t6, t7);
        return new InstanceInterceptorActionT7<TTarget, T1, T2, T3, T4, T5, T6, T7>(interceptor, (targetInst, replacement) =>
        {
            var actual = (TTarget self, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7) =>
            {
                if (typeof(TTarget).IsValueType ? EqualityComparer<TTarget>.Default.Equals(self, targetInst) : object.ReferenceEquals(self, targetInst))
                    replacement.Invoke(t1, t2, t3, t4, t5, t6, t7);
                else
                    actualMethod.Invoke(self, t1, t2, t3, t4, t5, t6, t7);
            };
            return interceptor.Intercept(actual);
        });
    }
}
