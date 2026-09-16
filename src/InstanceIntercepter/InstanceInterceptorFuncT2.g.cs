// Licensed under the Apache-2.0 License
// https://github.com/sator-imaging/Jitest

using System;
using System.Reflection;

namespace Jitest;

/// <summary>Instance method interceptor for <see cref="Func&lt;T1, T2&gt;"/>.</summary>
public sealed class InstanceInterceptorFuncT2<TTarget, T1, T2>
{
    readonly MethodInfo target;
    readonly Delegate? originalMethod;

    internal InstanceInterceptorFuncT2(MethodInfo target, Delegate? originalMethod)
    {
        this.target = target ?? throw new ArgumentNullException(nameof(target));
        this.originalMethod = originalMethod;
    }

    /// <summary>Intercepts instance method for specific instance with <see cref="Func&lt;T1, T2&gt;"/>.</summary>
    public DetourScope Intercept(TTarget instance, Func<T1, T2> replacement)
    {
        if (replacement == null) throw new ArgumentNullException(nameof(replacement));
        var orig = originalMethod != null ? (Func<TTarget, T1, T2>)(object)originalMethod : null;
        Func<TTarget, T1, T2> actual = (TTarget self, T1 t1) =>
            object.ReferenceEquals(self, instance)
                ? replacement.Invoke(t1)
                : (orig != null ? orig.Invoke(self, t1) : default!);
        return DetourScope.Create(target, actual, instance);
    }

    /// <summary>Intercepts instance method across all instances with <see cref="Func&lt;T1, T2&gt;"/>.</summary>
    public DetourScope InterceptUnsafe(Func<T1, T2> replacement)
    {
        if (replacement == null) throw new ArgumentNullException(nameof(replacement));
        Func<TTarget, T1, T2> actual = (TTarget self, T1 t1) => replacement.Invoke(t1);
        return DetourScope.Create(target, actual, instance: null);
    }
}

/// <summary>Extension methods for instance interceptors.</summary>
public static partial class InstanceInterceptorExtensions
{
    /// <summary>Extension method for instance method interceptor.</summary>
    public static InstanceInterceptorFuncT2<TTarget, T1, T2> InstanceJitest<TTarget, T1, T2>(this TTarget instance, string methodName, out Func<T1, T2>? originalMethod)
    {
        if (instance == null) throw new ArgumentNullException(nameof(instance));
        if (typeof(TTarget).IsValueType)
        {
            var (method, _) = Extensions.ResolveMethod<Delegate>(typeof(TTarget), methodName);
            originalMethod = null;
            return new InstanceInterceptorFuncT2<TTarget, T1, T2>(method, null);
        }
        else
        {
            var (method, dele) = Extensions.ResolveMethod<Func<TTarget, T1, T2>>(typeof(TTarget), methodName);
            originalMethod = dele != null ? (Func<T1, T2>)((object)dele) : null;
            return new InstanceInterceptorFuncT2<TTarget, T1, T2>(method, dele);
        }
    }
}