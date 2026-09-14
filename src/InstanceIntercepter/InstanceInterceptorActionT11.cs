// Licensed under the Apache-2.0 License
// https://github.com/sator-imaging/Jitest

using System;
using System.Reflection;

namespace Jitest;

/// <summary>Instance method interceptor for <see cref="Action&lt;T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11&gt;"/>.</summary>
public sealed class InstanceInterceptorActionT11<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>
{
    readonly MethodInfo target;
    readonly Delegate? originalMethod;

    internal InstanceInterceptorActionT11(MethodInfo target, Delegate? originalMethod)
    {
        this.target = target ?? throw new ArgumentNullException(nameof(target));
        this.originalMethod = originalMethod;
    }

    /// <summary>Intercepts instance method for specific instance with <see cref="Action&lt;T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11&gt;"/>.</summary>
    public DetourScope Intercept(TTarget instance, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> replacement)
    {
        if (replacement == null) throw new ArgumentNullException(nameof(replacement));
        var orig = originalMethod != null ? (Action<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>)(object)originalMethod : null;
        Action<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> actual = (TTarget self, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11) =>
        {
            if (object.ReferenceEquals(self, instance)) replacement.Invoke(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11);
            else orig?.Invoke(self, t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11);
        };
        return DetourScope.Create(target, actual, instance);
    }

    /// <summary>Intercepts instance method across all instances with <see cref="Action&lt;T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11&gt;"/>.</summary>
    public DetourScope InterceptUnsafe(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> replacement)
    {
        if (replacement == null) throw new ArgumentNullException(nameof(replacement));
        Action<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> actual = (TTarget self, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11) => replacement.Invoke(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11);
        return DetourScope.Create(target, actual, instance: null);
    }
}