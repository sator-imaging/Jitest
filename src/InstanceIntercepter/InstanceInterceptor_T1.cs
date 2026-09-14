// Licensed under the Apache-2.0 License
// https://github.com/sator-imaging/Jitest

using System;
using System.Reflection;

namespace Jitest;

/// <summary>Instance method interceptor.</summary>
public sealed class InstanceInterceptor<TTarget, T1>
{
    readonly MethodInfo target;
    readonly Delegate? originalMethod;

    internal InstanceInterceptor(MethodInfo target, Delegate? originalMethod)
    {
        this.target = target ?? throw new ArgumentNullException(nameof(target));
        this.originalMethod = originalMethod;
    }

    /// <summary>Intercepts instance method for specific instance with <see cref="Action&lt;T1&gt;"/>.</summary>
    public DetourScope Intercept(TTarget instance, Action<T1> replacement)
    {
        if (replacement == null) throw new ArgumentNullException(nameof(replacement));
        var orig = originalMethod != null ? (Action<T1>)(object)originalMethod : null;
        Action<TTarget, T1> actual = (TTarget self, T1 t1) =>
        {
            if (object.ReferenceEquals(self, instance)) replacement.Invoke(t1);
            else orig?.Invoke(t1);
        };
        return DetourScope.Create(target, actual, instance);
    }

    /// <summary>Intercepts instance method across all instances with <see cref="Action&lt;T1&gt;"/>.</summary>
    public DetourScope InterceptUnsafe(Action<T1> replacement)
    {
        if (replacement == null) throw new ArgumentNullException(nameof(replacement));
        Action<TTarget, T1> actual = (TTarget self, T1 t1) => replacement.Invoke(t1);
        return DetourScope.Create(target, actual, instance: null);
    }

    /// <summary>Intercepts instance method for specific instance with <see cref="Func&lt;T1&gt;"/>.</summary>
    public DetourScope Intercept(TTarget instance, Func<T1> replacement)
    {
        if (replacement == null) throw new ArgumentNullException(nameof(replacement));
        var orig = originalMethod != null ? (Func<T1>)(object)originalMethod : null;
        Func<TTarget, T1> actual = (TTarget self) =>
            object.ReferenceEquals(self, instance)
                ? replacement.Invoke()
                : (orig != null ? orig.Invoke() : default!);
        return DetourScope.Create(target, actual, instance);
    }

    /// <summary>Intercepts instance method across all instances with <see cref="Func&lt;T1&gt;"/>.</summary>
    public DetourScope InterceptUnsafe(Func<T1> replacement)
    {
        if (replacement == null) throw new ArgumentNullException(nameof(replacement));
        Func<TTarget, T1> actual = (TTarget self) => replacement.Invoke();
        return DetourScope.Create(target, actual, instance: null);
    }
}

/// <summary>Extension methods for instance interceptors.</summary>
public static class InstanceInterceptorExtensions_T1
{
    /// <summary>Extension method for instance method interceptor.</summary>
    public static InstanceInterceptor<TTarget, T1> InstanceJitest<TTarget, T1>(this TTarget instance, string methodName, out Action<T1>? originalMethod)
    {
        if (instance == null) throw new ArgumentNullException(nameof(instance));
        if (typeof(TTarget).IsValueType)
        {
            var (method, _) = ResolveMethodHelper.ResolveMethod<Delegate>(typeof(TTarget), methodName);
            originalMethod = null;
            return new InstanceInterceptor<TTarget, T1>(method, null);
        }
        else
        {
            var (method, dele) = ResolveMethodHelper.ResolveMethod<Action<T1>>(typeof(TTarget), methodName);
            originalMethod = dele;
            return new InstanceInterceptor<TTarget, T1>(method, dele);
        }
    }
    /// <summary>Extension method for instance method interceptor.</summary>
    public static InstanceInterceptor<TTarget, T1> InstanceJitest<TTarget, T1>(this TTarget instance, string methodName, out Func<T1>? originalMethod)
    {
        if (instance == null) throw new ArgumentNullException(nameof(instance));
        if (typeof(TTarget).IsValueType)
        {
            var (method, _) = ResolveMethodHelper.ResolveMethod<Delegate>(typeof(TTarget), methodName);
            originalMethod = null;
            return new InstanceInterceptor<TTarget, T1>(method, null);
        }
        else
        {
            var (method, dele) = ResolveMethodHelper.ResolveMethod<Func<T1>>(typeof(TTarget), methodName);
            originalMethod = dele;
            return new InstanceInterceptor<TTarget, T1>(method, dele);
        }
    }
}