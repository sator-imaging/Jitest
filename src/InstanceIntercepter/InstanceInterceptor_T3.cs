// Licensed under the Apache-2.0 License
// https://github.com/sator-imaging/Jitest

using System;
using System.Reflection;

namespace Jitest;

/// <summary>Instance method interceptor.</summary>
public sealed class InstanceInterceptor<TTarget, T1, T2, T3>
{
    readonly MethodInfo target;
    readonly Delegate? originalMethod;

    internal InstanceInterceptor(MethodInfo target, Delegate? originalMethod)
    {
        this.target = target ?? throw new ArgumentNullException(nameof(target));
        this.originalMethod = originalMethod;
    }

    /// <summary>Intercepts instance method for specific instance with <see cref="Action&lt;T1, T2, T3&gt;"/>.</summary>
    public DetourScope Intercept(TTarget instance, Action<T1, T2, T3> replacement)
    {
        if (replacement == null) throw new ArgumentNullException(nameof(replacement));
        var orig = originalMethod != null ? (Action<T1, T2, T3>)(object)originalMethod : null;
        Action<TTarget, T1, T2, T3> actual = (TTarget self, T1 t1, T2 t2, T3 t3) =>
        {
            if (object.ReferenceEquals(self, instance)) replacement.Invoke(t1, t2, t3);
            else orig?.Invoke(t1, t2, t3);
        };
        return DetourScope.Create(target, actual, instance);
    }

    /// <summary>Intercepts instance method across all instances with <see cref="Action&lt;T1, T2, T3&gt;"/>.</summary>
    public DetourScope InterceptUnsafe(Action<T1, T2, T3> replacement)
    {
        if (replacement == null) throw new ArgumentNullException(nameof(replacement));
        Action<TTarget, T1, T2, T3> actual = (TTarget self, T1 t1, T2 t2, T3 t3) => replacement.Invoke(t1, t2, t3);
        return DetourScope.Create(target, actual, instance: null);
    }

    /// <summary>Intercepts instance method for specific instance with <see cref="Func&lt;T1, T2, T3&gt;"/>.</summary>
    public DetourScope Intercept(TTarget instance, Func<T1, T2, T3> replacement)
    {
        if (replacement == null) throw new ArgumentNullException(nameof(replacement));
        var orig = originalMethod != null ? (Func<T1, T2, T3>)(object)originalMethod : null;
        Func<TTarget, T1, T2, T3> actual = (TTarget self, T1 t1, T2 t2) =>
            object.ReferenceEquals(self, instance)
                ? replacement.Invoke(t1, t2)
                : (orig != null ? orig.Invoke(t1, t2) : default!);
        return DetourScope.Create(target, actual, instance);
    }

    /// <summary>Intercepts instance method across all instances with <see cref="Func&lt;T1, T2, T3&gt;"/>.</summary>
    public DetourScope InterceptUnsafe(Func<T1, T2, T3> replacement)
    {
        if (replacement == null) throw new ArgumentNullException(nameof(replacement));
        Func<TTarget, T1, T2, T3> actual = (TTarget self, T1 t1, T2 t2) => replacement.Invoke(t1, t2);
        return DetourScope.Create(target, actual, instance: null);
    }
}

/// <summary>Extension methods for instance interceptors.</summary>
public static class InstanceInterceptorExtensions_T3
{
    /// <summary>Extension method for instance method interceptor.</summary>
    public static InstanceInterceptor<TTarget, T1, T2, T3> InstanceJitest<TTarget, T1, T2, T3>(this TTarget instance, string methodName, out Action<T1, T2, T3>? originalMethod)
    {
        if (instance == null) throw new ArgumentNullException(nameof(instance));
        if (typeof(TTarget).IsValueType)
        {
            var (method, _) = ResolveMethodHelper.ResolveMethod<Delegate>(typeof(TTarget), methodName);
            originalMethod = null;
            return new InstanceInterceptor<TTarget, T1, T2, T3>(method, null);
        }
        else
        {
            var (method, dele) = ResolveMethodHelper.ResolveMethod<Action<T1, T2, T3>>(typeof(TTarget), methodName);
            originalMethod = dele;
            return new InstanceInterceptor<TTarget, T1, T2, T3>(method, dele);
        }
    }
    /// <summary>Extension method for instance method interceptor.</summary>
    public static InstanceInterceptor<TTarget, T1, T2, T3> InstanceJitest<TTarget, T1, T2, T3>(this TTarget instance, string methodName, out Func<T1, T2, T3>? originalMethod)
    {
        if (instance == null) throw new ArgumentNullException(nameof(instance));
        if (typeof(TTarget).IsValueType)
        {
            var (method, _) = ResolveMethodHelper.ResolveMethod<Delegate>(typeof(TTarget), methodName);
            originalMethod = null;
            return new InstanceInterceptor<TTarget, T1, T2, T3>(method, null);
        }
        else
        {
            var (method, dele) = ResolveMethodHelper.ResolveMethod<Func<T1, T2, T3>>(typeof(TTarget), methodName);
            originalMethod = dele;
            return new InstanceInterceptor<TTarget, T1, T2, T3>(method, dele);
        }
    }
}