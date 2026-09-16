// Licensed under the Apache-2.0 License
// https://github.com/sator-imaging/Jitest

using System;
using System.Reflection;

namespace Jitest;

/// <summary>Instance method interceptor for <see cref="Func&lt;T1, T2, T3, T4, T5&gt;"/>.</summary>
public sealed class InstanceInterceptorFuncT5<TTarget, T1, T2, T3, T4, T5>
{
    readonly MethodInfo target;
    readonly Delegate? originalMethod;

    internal InstanceInterceptorFuncT5(MethodInfo target, Delegate? originalMethod)
    {
        this.target = target ?? throw new ArgumentNullException(nameof(target));
        this.originalMethod = originalMethod;
    }

    /// <summary>Intercepts instance method for specific instance with <see cref="Func&lt;T1, T2, T3, T4, T5&gt;"/>.</summary>
    public DetourScope Intercept(TTarget instance, Func<T1, T2, T3, T4, T5> replacement)
    {
        if (replacement == null) throw new ArgumentNullException(nameof(replacement));
        var orig = originalMethod != null ? (Func<TTarget, T1, T2, T3, T4, T5>)(object)originalMethod : null;
        Func<TTarget, T1, T2, T3, T4, T5> actual = (TTarget self, T1 t1, T2 t2, T3 t3, T4 t4) =>
            object.ReferenceEquals(self, instance)
                ? replacement.Invoke(t1, t2, t3, t4)
                : (orig != null ? orig.Invoke(self, t1, t2, t3, t4) : default!);
        return DetourScope.Create(target, actual, instance);
    }

    /// <summary>Intercepts instance method across all instances with <see cref="Func&lt;T1, T2, T3, T4, T5&gt;"/>.</summary>
    public DetourScope InterceptUnsafe(Func<T1, T2, T3, T4, T5> replacement)
    {
        if (replacement == null) throw new ArgumentNullException(nameof(replacement));
        Func<TTarget, T1, T2, T3, T4, T5> actual = (TTarget self, T1 t1, T2 t2, T3 t3, T4 t4) => replacement.Invoke(t1, t2, t3, t4);
        return DetourScope.Create(target, actual, instance: null);
    }
}

/// <summary>Extension methods for instance interceptors.</summary>
public static partial class InstanceInterceptorExtensions
{
    /// <summary>Extension method for instance method interceptor.</summary>
    public static InstanceInterceptorFuncT5<TTarget, T1, T2, T3, T4, T5> InstanceJitest<TTarget, T1, T2, T3, T4, T5>(this TTarget instance, string methodName, out Func<T1, T2, T3, T4, T5>? originalMethod)
    {
        if (instance == null) throw new ArgumentNullException(nameof(instance));
        if (typeof(TTarget).IsValueType)
        {
            var (method, _) = Extensions.ResolveMethod<Delegate>(typeof(TTarget), methodName);
            originalMethod = null;
            return new InstanceInterceptorFuncT5<TTarget, T1, T2, T3, T4, T5>(method, null);
        }
        else
        {
            var (method, dele) = Extensions.ResolveMethod<Func<TTarget, T1, T2, T3, T4, T5>>(typeof(TTarget), methodName);
            originalMethod = dele != null ? (Func<T1, T2, T3, T4, T5>)((object)dele) : null;
            return new InstanceInterceptorFuncT5<TTarget, T1, T2, T3, T4, T5>(method, dele);
        }
    }
}