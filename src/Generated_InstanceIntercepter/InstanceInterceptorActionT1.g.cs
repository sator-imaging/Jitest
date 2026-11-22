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
    readonly Func<TTarget, Action<T1>, DetourScope> interceptFunc;

    internal InstanceInterceptorActionT1(Interceptor interceptor, Func<TTarget, Action<T1>, DetourScope> interceptFunc)
    {
        this.interceptor = interceptor ?? throw new ArgumentNullException(nameof(interceptor));
        this.interceptFunc = interceptFunc ?? throw new ArgumentNullException(nameof(interceptFunc));
    }

    /// <summary>Intercepts instance method for specific instance with <see cref="Action&lt;T1&gt;"/>.</summary>
    public DetourScope Intercept(TTarget instance, Action<T1> replacement)
    {
        if (replacement == null) throw new ArgumentNullException(nameof(replacement));
        return interceptFunc(instance, replacement);
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
    public static InstanceInterceptorActionT1<TTarget, T1> InstanceJitest<TTarget, T1>(this TTarget instance, string methodName, out Action<T1> originalMethod)
    {
        if (instance == null) throw new ArgumentNullException(nameof(instance));
        var interceptor = instance.Jitest<Action<T1>>(methodName, out var originalMethodUser);
        var actualMethod = originalMethodUser.Method is System.Reflection.Emit.DynamicMethod dm ? (Action<TTarget, T1>)dm.CreateDelegate(typeof(Action<TTarget, T1>)) : (Action<TTarget, T1>)Delegate.CreateDelegate(typeof(Action<TTarget, T1>), originalMethodUser.Method);
        originalMethod = (t1) => actualMethod.Invoke(instance, t1);
        return new InstanceInterceptorActionT1<TTarget, T1>(interceptor, (targetInst, replacement) =>
        {
            var actual = (TTarget self, T1 t1) =>
            {
                if (typeof(TTarget).IsValueType ? EqualityComparer<TTarget>.Default.Equals(self, targetInst) : object.ReferenceEquals(self, targetInst))
                    replacement.Invoke(t1);
                else
                    actualMethod.Invoke(self, t1);
            };
            return interceptor.Intercept(actual);
        });
    }
}
