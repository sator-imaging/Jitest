// Licensed under the Apache-2.0 License
// https://github.com/sator-imaging/Jitest

using System;
using System.Reflection;

namespace Jitest;

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
    /// <summary>Extension method for instance method interceptor.</summary>
    public static InstanceInterceptorActionT1<TTarget, T1> InstanceJitest<TTarget, T1>(this TTarget instance, string methodName, out Action<T1>? originalMethod)
    {
        if (instance == null) throw new ArgumentNullException(nameof(instance));
        if (typeof(TTarget).IsValueType)
        {
            var (method, _) = Extensions.ResolveMethod<Delegate>(typeof(TTarget), methodName);
            originalMethod = null;
            return new InstanceInterceptorActionT1<TTarget, T1>(method, null);
        }
        else
        {
            var (method, dele) = Extensions.ResolveMethod<Action<TTarget, T1>>(typeof(TTarget), methodName);
            originalMethod = dele != null ? (Action<T1>)((object)dele) : null;
            return new InstanceInterceptorActionT1<TTarget, T1>(method, dele);
        }
    }
    /// <summary>Extension method for instance method interceptor.</summary>
    public static InstanceInterceptorFuncT1<TTarget, T1> InstanceJitest<TTarget, T1>(this TTarget instance, string methodName, out Func<T1>? originalMethod)
    {
        if (instance == null) throw new ArgumentNullException(nameof(instance));
        if (typeof(TTarget).IsValueType)
        {
            var (method, _) = Extensions.ResolveMethod<Delegate>(typeof(TTarget), methodName);
            originalMethod = null;
            return new InstanceInterceptorFuncT1<TTarget, T1>(method, null);
        }
        else
        {
            var (method, dele) = Extensions.ResolveMethod<Func<TTarget, T1>>(typeof(TTarget), methodName);
            originalMethod = dele != null ? (Func<T1>)((object)dele) : null;
            return new InstanceInterceptorFuncT1<TTarget, T1>(method, dele);
        }
    }
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
    /// <summary>Extension method for instance method interceptor.</summary>
    public static InstanceInterceptorActionT3<TTarget, T1, T2, T3> InstanceJitest<TTarget, T1, T2, T3>(this TTarget instance, string methodName, out Action<T1, T2, T3>? originalMethod)
    {
        if (instance == null) throw new ArgumentNullException(nameof(instance));
        if (typeof(TTarget).IsValueType)
        {
            var (method, _) = Extensions.ResolveMethod<Delegate>(typeof(TTarget), methodName);
            originalMethod = null;
            return new InstanceInterceptorActionT3<TTarget, T1, T2, T3>(method, null);
        }
        else
        {
            var (method, dele) = Extensions.ResolveMethod<Action<TTarget, T1, T2, T3>>(typeof(TTarget), methodName);
            originalMethod = dele != null ? (Action<T1, T2, T3>)((object)dele) : null;
            return new InstanceInterceptorActionT3<TTarget, T1, T2, T3>(method, dele);
        }
    }
    /// <summary>Extension method for instance method interceptor.</summary>
    public static InstanceInterceptorFuncT3<TTarget, T1, T2, T3> InstanceJitest<TTarget, T1, T2, T3>(this TTarget instance, string methodName, out Func<T1, T2, T3>? originalMethod)
    {
        if (instance == null) throw new ArgumentNullException(nameof(instance));
        if (typeof(TTarget).IsValueType)
        {
            var (method, _) = Extensions.ResolveMethod<Delegate>(typeof(TTarget), methodName);
            originalMethod = null;
            return new InstanceInterceptorFuncT3<TTarget, T1, T2, T3>(method, null);
        }
        else
        {
            var (method, dele) = Extensions.ResolveMethod<Func<TTarget, T1, T2, T3>>(typeof(TTarget), methodName);
            originalMethod = dele != null ? (Func<T1, T2, T3>)((object)dele) : null;
            return new InstanceInterceptorFuncT3<TTarget, T1, T2, T3>(method, dele);
        }
    }
    /// <summary>Extension method for instance method interceptor.</summary>
    public static InstanceInterceptorActionT4<TTarget, T1, T2, T3, T4> InstanceJitest<TTarget, T1, T2, T3, T4>(this TTarget instance, string methodName, out Action<T1, T2, T3, T4>? originalMethod)
    {
        if (instance == null) throw new ArgumentNullException(nameof(instance));
        if (typeof(TTarget).IsValueType)
        {
            var (method, _) = Extensions.ResolveMethod<Delegate>(typeof(TTarget), methodName);
            originalMethod = null;
            return new InstanceInterceptorActionT4<TTarget, T1, T2, T3, T4>(method, null);
        }
        else
        {
            var (method, dele) = Extensions.ResolveMethod<Action<TTarget, T1, T2, T3, T4>>(typeof(TTarget), methodName);
            originalMethod = dele != null ? (Action<T1, T2, T3, T4>)((object)dele) : null;
            return new InstanceInterceptorActionT4<TTarget, T1, T2, T3, T4>(method, dele);
        }
    }
    /// <summary>Extension method for instance method interceptor.</summary>
    public static InstanceInterceptorFuncT4<TTarget, T1, T2, T3, T4> InstanceJitest<TTarget, T1, T2, T3, T4>(this TTarget instance, string methodName, out Func<T1, T2, T3, T4>? originalMethod)
    {
        if (instance == null) throw new ArgumentNullException(nameof(instance));
        if (typeof(TTarget).IsValueType)
        {
            var (method, _) = Extensions.ResolveMethod<Delegate>(typeof(TTarget), methodName);
            originalMethod = null;
            return new InstanceInterceptorFuncT4<TTarget, T1, T2, T3, T4>(method, null);
        }
        else
        {
            var (method, dele) = Extensions.ResolveMethod<Func<TTarget, T1, T2, T3, T4>>(typeof(TTarget), methodName);
            originalMethod = dele != null ? (Func<T1, T2, T3, T4>)((object)dele) : null;
            return new InstanceInterceptorFuncT4<TTarget, T1, T2, T3, T4>(method, dele);
        }
    }
    /// <summary>Extension method for instance method interceptor.</summary>
    public static InstanceInterceptorActionT5<TTarget, T1, T2, T3, T4, T5> InstanceJitest<TTarget, T1, T2, T3, T4, T5>(this TTarget instance, string methodName, out Action<T1, T2, T3, T4, T5>? originalMethod)
    {
        if (instance == null) throw new ArgumentNullException(nameof(instance));
        if (typeof(TTarget).IsValueType)
        {
            var (method, _) = Extensions.ResolveMethod<Delegate>(typeof(TTarget), methodName);
            originalMethod = null;
            return new InstanceInterceptorActionT5<TTarget, T1, T2, T3, T4, T5>(method, null);
        }
        else
        {
            var (method, dele) = Extensions.ResolveMethod<Action<TTarget, T1, T2, T3, T4, T5>>(typeof(TTarget), methodName);
            originalMethod = dele != null ? (Action<T1, T2, T3, T4, T5>)((object)dele) : null;
            return new InstanceInterceptorActionT5<TTarget, T1, T2, T3, T4, T5>(method, dele);
        }
    }
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
    /// <summary>Extension method for instance method interceptor.</summary>
    public static InstanceInterceptorActionT6<TTarget, T1, T2, T3, T4, T5, T6> InstanceJitest<TTarget, T1, T2, T3, T4, T5, T6>(this TTarget instance, string methodName, out Action<T1, T2, T3, T4, T5, T6>? originalMethod)
    {
        if (instance == null) throw new ArgumentNullException(nameof(instance));
        if (typeof(TTarget).IsValueType)
        {
            var (method, _) = Extensions.ResolveMethod<Delegate>(typeof(TTarget), methodName);
            originalMethod = null;
            return new InstanceInterceptorActionT6<TTarget, T1, T2, T3, T4, T5, T6>(method, null);
        }
        else
        {
            var (method, dele) = Extensions.ResolveMethod<Action<TTarget, T1, T2, T3, T4, T5, T6>>(typeof(TTarget), methodName);
            originalMethod = dele != null ? (Action<T1, T2, T3, T4, T5, T6>)((object)dele) : null;
            return new InstanceInterceptorActionT6<TTarget, T1, T2, T3, T4, T5, T6>(method, dele);
        }
    }
    /// <summary>Extension method for instance method interceptor.</summary>
    public static InstanceInterceptorFuncT6<TTarget, T1, T2, T3, T4, T5, T6> InstanceJitest<TTarget, T1, T2, T3, T4, T5, T6>(this TTarget instance, string methodName, out Func<T1, T2, T3, T4, T5, T6>? originalMethod)
    {
        if (instance == null) throw new ArgumentNullException(nameof(instance));
        if (typeof(TTarget).IsValueType)
        {
            var (method, _) = Extensions.ResolveMethod<Delegate>(typeof(TTarget), methodName);
            originalMethod = null;
            return new InstanceInterceptorFuncT6<TTarget, T1, T2, T3, T4, T5, T6>(method, null);
        }
        else
        {
            var (method, dele) = Extensions.ResolveMethod<Func<TTarget, T1, T2, T3, T4, T5, T6>>(typeof(TTarget), methodName);
            originalMethod = dele != null ? (Func<T1, T2, T3, T4, T5, T6>)((object)dele) : null;
            return new InstanceInterceptorFuncT6<TTarget, T1, T2, T3, T4, T5, T6>(method, dele);
        }
    }
    /// <summary>Extension method for instance method interceptor.</summary>
    public static InstanceInterceptorActionT7<TTarget, T1, T2, T3, T4, T5, T6, T7> InstanceJitest<TTarget, T1, T2, T3, T4, T5, T6, T7>(this TTarget instance, string methodName, out Action<T1, T2, T3, T4, T5, T6, T7>? originalMethod)
    {
        if (instance == null) throw new ArgumentNullException(nameof(instance));
        if (typeof(TTarget).IsValueType)
        {
            var (method, _) = Extensions.ResolveMethod<Delegate>(typeof(TTarget), methodName);
            originalMethod = null;
            return new InstanceInterceptorActionT7<TTarget, T1, T2, T3, T4, T5, T6, T7>(method, null);
        }
        else
        {
            var (method, dele) = Extensions.ResolveMethod<Action<TTarget, T1, T2, T3, T4, T5, T6, T7>>(typeof(TTarget), methodName);
            originalMethod = dele != null ? (Action<T1, T2, T3, T4, T5, T6, T7>)((object)dele) : null;
            return new InstanceInterceptorActionT7<TTarget, T1, T2, T3, T4, T5, T6, T7>(method, dele);
        }
    }
    /// <summary>Extension method for instance method interceptor.</summary>
    public static InstanceInterceptorFuncT7<TTarget, T1, T2, T3, T4, T5, T6, T7> InstanceJitest<TTarget, T1, T2, T3, T4, T5, T6, T7>(this TTarget instance, string methodName, out Func<T1, T2, T3, T4, T5, T6, T7>? originalMethod)
    {
        if (instance == null) throw new ArgumentNullException(nameof(instance));
        if (typeof(TTarget).IsValueType)
        {
            var (method, _) = Extensions.ResolveMethod<Delegate>(typeof(TTarget), methodName);
            originalMethod = null;
            return new InstanceInterceptorFuncT7<TTarget, T1, T2, T3, T4, T5, T6, T7>(method, null);
        }
        else
        {
            var (method, dele) = Extensions.ResolveMethod<Func<TTarget, T1, T2, T3, T4, T5, T6, T7>>(typeof(TTarget), methodName);
            originalMethod = dele != null ? (Func<T1, T2, T3, T4, T5, T6, T7>)((object)dele) : null;
            return new InstanceInterceptorFuncT7<TTarget, T1, T2, T3, T4, T5, T6, T7>(method, dele);
        }
    }
    /// <summary>Extension method for instance method interceptor.</summary>
    public static InstanceInterceptorActionT8<TTarget, T1, T2, T3, T4, T5, T6, T7, T8> InstanceJitest<TTarget, T1, T2, T3, T4, T5, T6, T7, T8>(this TTarget instance, string methodName, out Action<T1, T2, T3, T4, T5, T6, T7, T8>? originalMethod)
    {
        if (instance == null) throw new ArgumentNullException(nameof(instance));
        if (typeof(TTarget).IsValueType)
        {
            var (method, _) = Extensions.ResolveMethod<Delegate>(typeof(TTarget), methodName);
            originalMethod = null;
            return new InstanceInterceptorActionT8<TTarget, T1, T2, T3, T4, T5, T6, T7, T8>(method, null);
        }
        else
        {
            var (method, dele) = Extensions.ResolveMethod<Action<TTarget, T1, T2, T3, T4, T5, T6, T7, T8>>(typeof(TTarget), methodName);
            originalMethod = dele != null ? (Action<T1, T2, T3, T4, T5, T6, T7, T8>)((object)dele) : null;
            return new InstanceInterceptorActionT8<TTarget, T1, T2, T3, T4, T5, T6, T7, T8>(method, dele);
        }
    }
    /// <summary>Extension method for instance method interceptor.</summary>
    public static InstanceInterceptorFuncT8<TTarget, T1, T2, T3, T4, T5, T6, T7, T8> InstanceJitest<TTarget, T1, T2, T3, T4, T5, T6, T7, T8>(this TTarget instance, string methodName, out Func<T1, T2, T3, T4, T5, T6, T7, T8>? originalMethod)
    {
        if (instance == null) throw new ArgumentNullException(nameof(instance));
        if (typeof(TTarget).IsValueType)
        {
            var (method, _) = Extensions.ResolveMethod<Delegate>(typeof(TTarget), methodName);
            originalMethod = null;
            return new InstanceInterceptorFuncT8<TTarget, T1, T2, T3, T4, T5, T6, T7, T8>(method, null);
        }
        else
        {
            var (method, dele) = Extensions.ResolveMethod<Func<TTarget, T1, T2, T3, T4, T5, T6, T7, T8>>(typeof(TTarget), methodName);
            originalMethod = dele != null ? (Func<T1, T2, T3, T4, T5, T6, T7, T8>)((object)dele) : null;
            return new InstanceInterceptorFuncT8<TTarget, T1, T2, T3, T4, T5, T6, T7, T8>(method, dele);
        }
    }
    /// <summary>Extension method for instance method interceptor.</summary>
    public static InstanceInterceptorActionT9<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9> InstanceJitest<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9>(this TTarget instance, string methodName, out Action<T1, T2, T3, T4, T5, T6, T7, T8, T9>? originalMethod)
    {
        if (instance == null) throw new ArgumentNullException(nameof(instance));
        if (typeof(TTarget).IsValueType)
        {
            var (method, _) = Extensions.ResolveMethod<Delegate>(typeof(TTarget), methodName);
            originalMethod = null;
            return new InstanceInterceptorActionT9<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9>(method, null);
        }
        else
        {
            var (method, dele) = Extensions.ResolveMethod<Action<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9>>(typeof(TTarget), methodName);
            originalMethod = dele != null ? (Action<T1, T2, T3, T4, T5, T6, T7, T8, T9>)((object)dele) : null;
            return new InstanceInterceptorActionT9<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9>(method, dele);
        }
    }
    /// <summary>Extension method for instance method interceptor.</summary>
    public static InstanceInterceptorFuncT9<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9> InstanceJitest<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9>(this TTarget instance, string methodName, out Func<T1, T2, T3, T4, T5, T6, T7, T8, T9>? originalMethod)
    {
        if (instance == null) throw new ArgumentNullException(nameof(instance));
        if (typeof(TTarget).IsValueType)
        {
            var (method, _) = Extensions.ResolveMethod<Delegate>(typeof(TTarget), methodName);
            originalMethod = null;
            return new InstanceInterceptorFuncT9<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9>(method, null);
        }
        else
        {
            var (method, dele) = Extensions.ResolveMethod<Func<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9>>(typeof(TTarget), methodName);
            originalMethod = dele != null ? (Func<T1, T2, T3, T4, T5, T6, T7, T8, T9>)((object)dele) : null;
            return new InstanceInterceptorFuncT9<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9>(method, dele);
        }
    }
    /// <summary>Extension method for instance method interceptor.</summary>
    public static InstanceInterceptorActionT10<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> InstanceJitest<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this TTarget instance, string methodName, out Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>? originalMethod)
    {
        if (instance == null) throw new ArgumentNullException(nameof(instance));
        if (typeof(TTarget).IsValueType)
        {
            var (method, _) = Extensions.ResolveMethod<Delegate>(typeof(TTarget), methodName);
            originalMethod = null;
            return new InstanceInterceptorActionT10<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(method, null);
        }
        else
        {
            var (method, dele) = Extensions.ResolveMethod<Action<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>>(typeof(TTarget), methodName);
            originalMethod = dele != null ? (Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>)((object)dele) : null;
            return new InstanceInterceptorActionT10<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(method, dele);
        }
    }
    /// <summary>Extension method for instance method interceptor.</summary>
    public static InstanceInterceptorFuncT10<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> InstanceJitest<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this TTarget instance, string methodName, out Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>? originalMethod)
    {
        if (instance == null) throw new ArgumentNullException(nameof(instance));
        if (typeof(TTarget).IsValueType)
        {
            var (method, _) = Extensions.ResolveMethod<Delegate>(typeof(TTarget), methodName);
            originalMethod = null;
            return new InstanceInterceptorFuncT10<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(method, null);
        }
        else
        {
            var (method, dele) = Extensions.ResolveMethod<Func<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>>(typeof(TTarget), methodName);
            originalMethod = dele != null ? (Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>)((object)dele) : null;
            return new InstanceInterceptorFuncT10<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(method, dele);
        }
    }
    /// <summary>Extension method for instance method interceptor.</summary>
    public static InstanceInterceptorActionT11<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> InstanceJitest<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(this TTarget instance, string methodName, out Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>? originalMethod)
    {
        if (instance == null) throw new ArgumentNullException(nameof(instance));
        if (typeof(TTarget).IsValueType)
        {
            var (method, _) = Extensions.ResolveMethod<Delegate>(typeof(TTarget), methodName);
            originalMethod = null;
            return new InstanceInterceptorActionT11<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(method, null);
        }
        else
        {
            var (method, dele) = Extensions.ResolveMethod<Action<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>>(typeof(TTarget), methodName);
            originalMethod = dele != null ? (Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>)((object)dele) : null;
            return new InstanceInterceptorActionT11<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(method, dele);
        }
    }
    /// <summary>Extension method for instance method interceptor.</summary>
    public static InstanceInterceptorFuncT11<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> InstanceJitest<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(this TTarget instance, string methodName, out Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>? originalMethod)
    {
        if (instance == null) throw new ArgumentNullException(nameof(instance));
        if (typeof(TTarget).IsValueType)
        {
            var (method, _) = Extensions.ResolveMethod<Delegate>(typeof(TTarget), methodName);
            originalMethod = null;
            return new InstanceInterceptorFuncT11<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(method, null);
        }
        else
        {
            var (method, dele) = Extensions.ResolveMethod<Func<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>>(typeof(TTarget), methodName);
            originalMethod = dele != null ? (Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>)((object)dele) : null;
            return new InstanceInterceptorFuncT11<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(method, dele);
        }
    }
    /// <summary>Extension method for instance method interceptor.</summary>
    public static InstanceInterceptorActionT12<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> InstanceJitest<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(this TTarget instance, string methodName, out Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>? originalMethod)
    {
        if (instance == null) throw new ArgumentNullException(nameof(instance));
        if (typeof(TTarget).IsValueType)
        {
            var (method, _) = Extensions.ResolveMethod<Delegate>(typeof(TTarget), methodName);
            originalMethod = null;
            return new InstanceInterceptorActionT12<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(method, null);
        }
        else
        {
            var (method, dele) = Extensions.ResolveMethod<Action<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>>(typeof(TTarget), methodName);
            originalMethod = dele != null ? (Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>)((object)dele) : null;
            return new InstanceInterceptorActionT12<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(method, dele);
        }
    }
    /// <summary>Extension method for instance method interceptor.</summary>
    public static InstanceInterceptorFuncT12<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> InstanceJitest<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(this TTarget instance, string methodName, out Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>? originalMethod)
    {
        if (instance == null) throw new ArgumentNullException(nameof(instance));
        if (typeof(TTarget).IsValueType)
        {
            var (method, _) = Extensions.ResolveMethod<Delegate>(typeof(TTarget), methodName);
            originalMethod = null;
            return new InstanceInterceptorFuncT12<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(method, null);
        }
        else
        {
            var (method, dele) = Extensions.ResolveMethod<Func<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>>(typeof(TTarget), methodName);
            originalMethod = dele != null ? (Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>)((object)dele) : null;
            return new InstanceInterceptorFuncT12<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(method, dele);
        }
    }
    /// <summary>Extension method for instance method interceptor.</summary>
    public static InstanceInterceptorActionT13<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> InstanceJitest<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(this TTarget instance, string methodName, out Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>? originalMethod)
    {
        if (instance == null) throw new ArgumentNullException(nameof(instance));
        if (typeof(TTarget).IsValueType)
        {
            var (method, _) = Extensions.ResolveMethod<Delegate>(typeof(TTarget), methodName);
            originalMethod = null;
            return new InstanceInterceptorActionT13<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(method, null);
        }
        else
        {
            var (method, dele) = Extensions.ResolveMethod<Action<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>>(typeof(TTarget), methodName);
            originalMethod = dele != null ? (Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>)((object)dele) : null;
            return new InstanceInterceptorActionT13<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(method, dele);
        }
    }
    /// <summary>Extension method for instance method interceptor.</summary>
    public static InstanceInterceptorFuncT13<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> InstanceJitest<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(this TTarget instance, string methodName, out Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>? originalMethod)
    {
        if (instance == null) throw new ArgumentNullException(nameof(instance));
        if (typeof(TTarget).IsValueType)
        {
            var (method, _) = Extensions.ResolveMethod<Delegate>(typeof(TTarget), methodName);
            originalMethod = null;
            return new InstanceInterceptorFuncT13<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(method, null);
        }
        else
        {
            var (method, dele) = Extensions.ResolveMethod<Func<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>>(typeof(TTarget), methodName);
            originalMethod = dele != null ? (Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>)((object)dele) : null;
            return new InstanceInterceptorFuncT13<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(method, dele);
        }
    }
    /// <summary>Extension method for instance method interceptor.</summary>
    public static InstanceInterceptorActionT14<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> InstanceJitest<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(this TTarget instance, string methodName, out Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>? originalMethod)
    {
        if (instance == null) throw new ArgumentNullException(nameof(instance));
        if (typeof(TTarget).IsValueType)
        {
            var (method, _) = Extensions.ResolveMethod<Delegate>(typeof(TTarget), methodName);
            originalMethod = null;
            return new InstanceInterceptorActionT14<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(method, null);
        }
        else
        {
            var (method, dele) = Extensions.ResolveMethod<Action<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>>(typeof(TTarget), methodName);
            originalMethod = dele != null ? (Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>)((object)dele) : null;
            return new InstanceInterceptorActionT14<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(method, dele);
        }
    }
    /// <summary>Extension method for instance method interceptor.</summary>
    public static InstanceInterceptorFuncT14<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> InstanceJitest<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(this TTarget instance, string methodName, out Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>? originalMethod)
    {
        if (instance == null) throw new ArgumentNullException(nameof(instance));
        if (typeof(TTarget).IsValueType)
        {
            var (method, _) = Extensions.ResolveMethod<Delegate>(typeof(TTarget), methodName);
            originalMethod = null;
            return new InstanceInterceptorFuncT14<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(method, null);
        }
        else
        {
            var (method, dele) = Extensions.ResolveMethod<Func<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>>(typeof(TTarget), methodName);
            originalMethod = dele != null ? (Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>)((object)dele) : null;
            return new InstanceInterceptorFuncT14<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(method, dele);
        }
    }
    /// <summary>Extension method for instance method interceptor.</summary>
    public static InstanceInterceptorActionT15<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> InstanceJitest<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(this TTarget instance, string methodName, out Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>? originalMethod)
    {
        if (instance == null) throw new ArgumentNullException(nameof(instance));
        if (typeof(TTarget).IsValueType)
        {
            var (method, _) = Extensions.ResolveMethod<Delegate>(typeof(TTarget), methodName);
            originalMethod = null;
            return new InstanceInterceptorActionT15<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(method, null);
        }
        else
        {
            var (method, dele) = Extensions.ResolveMethod<Action<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>>(typeof(TTarget), methodName);
            originalMethod = dele != null ? (Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>)((object)dele) : null;
            return new InstanceInterceptorActionT15<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(method, dele);
        }
    }
    /// <summary>Extension method for instance method interceptor.</summary>
    public static InstanceInterceptorFuncT15<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> InstanceJitest<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(this TTarget instance, string methodName, out Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>? originalMethod)
    {
        if (instance == null) throw new ArgumentNullException(nameof(instance));
        if (typeof(TTarget).IsValueType)
        {
            var (method, _) = Extensions.ResolveMethod<Delegate>(typeof(TTarget), methodName);
            originalMethod = null;
            return new InstanceInterceptorFuncT15<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(method, null);
        }
        else
        {
            var (method, dele) = Extensions.ResolveMethod<Func<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>>(typeof(TTarget), methodName);
            originalMethod = dele != null ? (Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>)((object)dele) : null;
            return new InstanceInterceptorFuncT15<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(method, dele);
        }
    }
    /// <summary>Extension method for instance method interceptor.</summary>
    public static InstanceInterceptorFuncT16<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> InstanceJitest<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(this TTarget instance, string methodName, out Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>? originalMethod)
    {
        if (instance == null) throw new ArgumentNullException(nameof(instance));
        if (typeof(TTarget).IsValueType)
        {
            var (method, _) = Extensions.ResolveMethod<Delegate>(typeof(TTarget), methodName);
            originalMethod = null;
            return new InstanceInterceptorFuncT16<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(method, null);
        }
        else
        {
            var (method, dele) = Extensions.ResolveMethod<Func<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>>(typeof(TTarget), methodName);
            originalMethod = dele != null ? (Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>)((object)dele) : null;
            return new InstanceInterceptorFuncT16<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(method, dele);
        }
    }
}