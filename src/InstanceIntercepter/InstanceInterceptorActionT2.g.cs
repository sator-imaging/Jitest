// Licensed under the Apache-2.0 License
// https://github.com/sator-imaging/Jitest

using System;
using System.Reflection;

namespace Jitest;

/// <summary>Instance method interceptor for <see cref="Action&lt;T1, T2&gt;"/>.</summary>
public sealed class InstanceInterceptorActionT2<TTarget, T1, T2>
{
    readonly MethodInfo target;
    readonly Delegate? originalMethod;

    internal InstanceInterceptorActionT2(MethodInfo target, Delegate? originalMethod)
    {
        this.target = target ?? throw new ArgumentNullException(nameof(target));
        this.originalMethod = originalMethod;
    }

    /// <summary>Intercepts instance method for specific instance with <see cref="Action&lt;T1, T2&gt;"/>.</summary>
    public DetourScope Intercept(TTarget instance, Action<T1, T2> replacement)
    {
        if (replacement == null) throw new ArgumentNullException(nameof(replacement));
        var orig = originalMethod != null ? (Action<TTarget, T1, T2>)(object)originalMethod : null;
        Action<TTarget, T1, T2> actual = (TTarget self, T1 t1, T2 t2) =>
        {
            if (object.ReferenceEquals(self, instance)) replacement.Invoke(t1, t2);
            else orig?.Invoke(self, t1, t2);
        };
        return DetourScope.Create(target, actual, instance);
    }

    /// <summary>Intercepts instance method across all instances with <see cref="Action&lt;T1, T2&gt;"/>.</summary>
    public DetourScope InterceptUnsafe(Action<T1, T2> replacement)
    {
        if (replacement == null) throw new ArgumentNullException(nameof(replacement));
        Action<TTarget, T1, T2> actual = (TTarget self, T1 t1, T2 t2) => replacement.Invoke(t1, t2);
        return DetourScope.Create(target, actual, instance: null);
    }
}

/// <summary>Extension methods for instance interceptors.</summary>
public static partial class InstanceInterceptorExtensions
{
    /// <summary>Extension method for instance method interceptor.</summary>
    public static InstanceInterceptorActionT2<TTarget, T1, T2> InstanceJitest<TTarget, T1, T2>(this TTarget instance, string methodName, out Action<T1, T2>? originalMethod)
    {
        if (instance == null) throw new ArgumentNullException(nameof(instance));
        if (typeof(TTarget).IsValueType)
        {
            var (method, _) = Extensions.ResolveMethod<Delegate>(typeof(TTarget), methodName);
            originalMethod = null;
            return new InstanceInterceptorActionT2<TTarget, T1, T2>(method, null);
        }
        else
        {
            var (method, dele) = Extensions.ResolveMethod<Action<TTarget, T1, T2>>(typeof(TTarget), methodName);
            originalMethod = dele != null ? (Action<T1, T2>)((object)dele) : null;
            return new InstanceInterceptorActionT2<TTarget, T1, T2>(method, dele);
        }
    }
}