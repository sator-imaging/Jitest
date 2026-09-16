// Licensed under the Apache-2.0 License
// https://github.com/sator-imaging/Jitest

using System;
using System.Reflection;

namespace Jitest;

/// <summary>Instance method interceptor for <see cref="Action"/>.</summary>
public sealed class InstanceInterceptorActionT0<TTarget>
{
    readonly MethodInfo target;
    readonly Delegate? originalMethod;

    internal InstanceInterceptorActionT0(MethodInfo target, Delegate? originalMethod)
    {
        this.target = target ?? throw new ArgumentNullException(nameof(target));
        this.originalMethod = originalMethod;
    }

    /// <summary>Intercepts instance method for specific instance with <see cref="Action"/>.</summary>
    public DetourScope Intercept(TTarget instance, Action replacement)
    {
        if (replacement == null) throw new ArgumentNullException(nameof(replacement));
        var orig = originalMethod != null ? (Action<TTarget>)(object)originalMethod : null;
        Action<TTarget> actual = (TTarget self) =>
        {
            if (object.ReferenceEquals(self, instance)) replacement.Invoke();
            else orig?.Invoke(self);
        };
        return DetourScope.Create(target, actual, instance);
    }

    /// <summary>Intercepts instance method across all instances with <see cref="Action"/>.</summary>
    public DetourScope InterceptUnsafe(Action replacement)
    {
        if (replacement == null) throw new ArgumentNullException(nameof(replacement));
        Action<TTarget> actual = (TTarget self) => replacement.Invoke();
        return DetourScope.Create(target, actual, instance: null);
    }
}

/// <summary>Extension methods for instance interceptors.</summary>
public static partial class InstanceInterceptorExtensions
{
    /// <summary>Extension method for instance method interceptor.</summary>
    public static InstanceInterceptorActionT0<TTarget> InstanceJitest<TTarget>(this TTarget instance, string methodName, out Action? originalMethod)
    {
        if (instance == null) throw new ArgumentNullException(nameof(instance));
        if (typeof(TTarget).IsValueType)
        {
            var (method, _) = Extensions.ResolveMethod<Delegate>(typeof(TTarget), methodName);
            originalMethod = null;
            return new InstanceInterceptorActionT0<TTarget>(method, null);
        }
        else
        {
            var (method, dele) = Extensions.ResolveMethod<Action<TTarget>>(typeof(TTarget), methodName);
            originalMethod = dele != null ? (Action)((object)dele) : null;
            return new InstanceInterceptorActionT0<TTarget>(method, dele);
        }
    }
}