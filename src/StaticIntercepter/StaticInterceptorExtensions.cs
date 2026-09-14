// Licensed under the Apache-2.0 License
// https://github.com/sator-imaging/Jitest

using System;
using System.Reflection;

namespace Jitest;

/// <summary>Extension methods for static interceptors.</summary>
public static partial class StaticInterceptorExtensions
{
    /// <summary>Extension method for static method interceptor.</summary>
    public static StaticInterceptorActionT0<TTarget> StaticJitest<TTarget>(this Type type, string methodName, out Action? originalMethod)
    {
        var (method, dele) = Extensions.ResolveMethod<Action>(type, methodName);
        originalMethod = dele;
        return new StaticInterceptorActionT0<TTarget>(method, dele);
    }
    /// <summary>Extension method for static method interceptor.</summary>
    public static StaticInterceptorActionT1<TTarget, T1> StaticJitest<TTarget, T1>(this Type type, string methodName, out Action<T1>? originalMethod)
    {
        var (method, dele) = Extensions.ResolveMethod<Action<T1>>(type, methodName);
        originalMethod = dele;
        return new StaticInterceptorActionT1<TTarget, T1>(method, dele);
    }
    /// <summary>Extension method for static method interceptor.</summary>
    public static StaticInterceptorFuncT1<TTarget, T1> StaticJitest<TTarget, T1>(this Type type, string methodName, out Func<T1>? originalMethod)
    {
        var (method, dele) = Extensions.ResolveMethod<Func<T1>>(type, methodName);
        originalMethod = dele;
        return new StaticInterceptorFuncT1<TTarget, T1>(method, dele);
    }
    /// <summary>Extension method for static method interceptor.</summary>
    public static StaticInterceptorActionT2<TTarget, T1, T2> StaticJitest<TTarget, T1, T2>(this Type type, string methodName, out Action<T1, T2>? originalMethod)
    {
        var (method, dele) = Extensions.ResolveMethod<Action<T1, T2>>(type, methodName);
        originalMethod = dele;
        return new StaticInterceptorActionT2<TTarget, T1, T2>(method, dele);
    }
    /// <summary>Extension method for static method interceptor.</summary>
    public static StaticInterceptorFuncT2<TTarget, T1, T2> StaticJitest<TTarget, T1, T2>(this Type type, string methodName, out Func<T1, T2>? originalMethod)
    {
        var (method, dele) = Extensions.ResolveMethod<Func<T1, T2>>(type, methodName);
        originalMethod = dele;
        return new StaticInterceptorFuncT2<TTarget, T1, T2>(method, dele);
    }
    /// <summary>Extension method for static method interceptor.</summary>
    public static StaticInterceptorActionT3<TTarget, T1, T2, T3> StaticJitest<TTarget, T1, T2, T3>(this Type type, string methodName, out Action<T1, T2, T3>? originalMethod)
    {
        var (method, dele) = Extensions.ResolveMethod<Action<T1, T2, T3>>(type, methodName);
        originalMethod = dele;
        return new StaticInterceptorActionT3<TTarget, T1, T2, T3>(method, dele);
    }
    /// <summary>Extension method for static method interceptor.</summary>
    public static StaticInterceptorFuncT3<TTarget, T1, T2, T3> StaticJitest<TTarget, T1, T2, T3>(this Type type, string methodName, out Func<T1, T2, T3>? originalMethod)
    {
        var (method, dele) = Extensions.ResolveMethod<Func<T1, T2, T3>>(type, methodName);
        originalMethod = dele;
        return new StaticInterceptorFuncT3<TTarget, T1, T2, T3>(method, dele);
    }
    /// <summary>Extension method for static method interceptor.</summary>
    public static StaticInterceptorActionT4<TTarget, T1, T2, T3, T4> StaticJitest<TTarget, T1, T2, T3, T4>(this Type type, string methodName, out Action<T1, T2, T3, T4>? originalMethod)
    {
        var (method, dele) = Extensions.ResolveMethod<Action<T1, T2, T3, T4>>(type, methodName);
        originalMethod = dele;
        return new StaticInterceptorActionT4<TTarget, T1, T2, T3, T4>(method, dele);
    }
    /// <summary>Extension method for static method interceptor.</summary>
    public static StaticInterceptorFuncT4<TTarget, T1, T2, T3, T4> StaticJitest<TTarget, T1, T2, T3, T4>(this Type type, string methodName, out Func<T1, T2, T3, T4>? originalMethod)
    {
        var (method, dele) = Extensions.ResolveMethod<Func<T1, T2, T3, T4>>(type, methodName);
        originalMethod = dele;
        return new StaticInterceptorFuncT4<TTarget, T1, T2, T3, T4>(method, dele);
    }
    /// <summary>Extension method for static method interceptor.</summary>
    public static StaticInterceptorActionT5<TTarget, T1, T2, T3, T4, T5> StaticJitest<TTarget, T1, T2, T3, T4, T5>(this Type type, string methodName, out Action<T1, T2, T3, T4, T5>? originalMethod)
    {
        var (method, dele) = Extensions.ResolveMethod<Action<T1, T2, T3, T4, T5>>(type, methodName);
        originalMethod = dele;
        return new StaticInterceptorActionT5<TTarget, T1, T2, T3, T4, T5>(method, dele);
    }
    /// <summary>Extension method for static method interceptor.</summary>
    public static StaticInterceptorFuncT5<TTarget, T1, T2, T3, T4, T5> StaticJitest<TTarget, T1, T2, T3, T4, T5>(this Type type, string methodName, out Func<T1, T2, T3, T4, T5>? originalMethod)
    {
        var (method, dele) = Extensions.ResolveMethod<Func<T1, T2, T3, T4, T5>>(type, methodName);
        originalMethod = dele;
        return new StaticInterceptorFuncT5<TTarget, T1, T2, T3, T4, T5>(method, dele);
    }
    /// <summary>Extension method for static method interceptor.</summary>
    public static StaticInterceptorActionT6<TTarget, T1, T2, T3, T4, T5, T6> StaticJitest<TTarget, T1, T2, T3, T4, T5, T6>(this Type type, string methodName, out Action<T1, T2, T3, T4, T5, T6>? originalMethod)
    {
        var (method, dele) = Extensions.ResolveMethod<Action<T1, T2, T3, T4, T5, T6>>(type, methodName);
        originalMethod = dele;
        return new StaticInterceptorActionT6<TTarget, T1, T2, T3, T4, T5, T6>(method, dele);
    }
    /// <summary>Extension method for static method interceptor.</summary>
    public static StaticInterceptorFuncT6<TTarget, T1, T2, T3, T4, T5, T6> StaticJitest<TTarget, T1, T2, T3, T4, T5, T6>(this Type type, string methodName, out Func<T1, T2, T3, T4, T5, T6>? originalMethod)
    {
        var (method, dele) = Extensions.ResolveMethod<Func<T1, T2, T3, T4, T5, T6>>(type, methodName);
        originalMethod = dele;
        return new StaticInterceptorFuncT6<TTarget, T1, T2, T3, T4, T5, T6>(method, dele);
    }
    /// <summary>Extension method for static method interceptor.</summary>
    public static StaticInterceptorActionT7<TTarget, T1, T2, T3, T4, T5, T6, T7> StaticJitest<TTarget, T1, T2, T3, T4, T5, T6, T7>(this Type type, string methodName, out Action<T1, T2, T3, T4, T5, T6, T7>? originalMethod)
    {
        var (method, dele) = Extensions.ResolveMethod<Action<T1, T2, T3, T4, T5, T6, T7>>(type, methodName);
        originalMethod = dele;
        return new StaticInterceptorActionT7<TTarget, T1, T2, T3, T4, T5, T6, T7>(method, dele);
    }
    /// <summary>Extension method for static method interceptor.</summary>
    public static StaticInterceptorFuncT7<TTarget, T1, T2, T3, T4, T5, T6, T7> StaticJitest<TTarget, T1, T2, T3, T4, T5, T6, T7>(this Type type, string methodName, out Func<T1, T2, T3, T4, T5, T6, T7>? originalMethod)
    {
        var (method, dele) = Extensions.ResolveMethod<Func<T1, T2, T3, T4, T5, T6, T7>>(type, methodName);
        originalMethod = dele;
        return new StaticInterceptorFuncT7<TTarget, T1, T2, T3, T4, T5, T6, T7>(method, dele);
    }
    /// <summary>Extension method for static method interceptor.</summary>
    public static StaticInterceptorActionT8<TTarget, T1, T2, T3, T4, T5, T6, T7, T8> StaticJitest<TTarget, T1, T2, T3, T4, T5, T6, T7, T8>(this Type type, string methodName, out Action<T1, T2, T3, T4, T5, T6, T7, T8>? originalMethod)
    {
        var (method, dele) = Extensions.ResolveMethod<Action<T1, T2, T3, T4, T5, T6, T7, T8>>(type, methodName);
        originalMethod = dele;
        return new StaticInterceptorActionT8<TTarget, T1, T2, T3, T4, T5, T6, T7, T8>(method, dele);
    }
    /// <summary>Extension method for static method interceptor.</summary>
    public static StaticInterceptorFuncT8<TTarget, T1, T2, T3, T4, T5, T6, T7, T8> StaticJitest<TTarget, T1, T2, T3, T4, T5, T6, T7, T8>(this Type type, string methodName, out Func<T1, T2, T3, T4, T5, T6, T7, T8>? originalMethod)
    {
        var (method, dele) = Extensions.ResolveMethod<Func<T1, T2, T3, T4, T5, T6, T7, T8>>(type, methodName);
        originalMethod = dele;
        return new StaticInterceptorFuncT8<TTarget, T1, T2, T3, T4, T5, T6, T7, T8>(method, dele);
    }
    /// <summary>Extension method for static method interceptor.</summary>
    public static StaticInterceptorActionT9<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9> StaticJitest<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9>(this Type type, string methodName, out Action<T1, T2, T3, T4, T5, T6, T7, T8, T9>? originalMethod)
    {
        var (method, dele) = Extensions.ResolveMethod<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9>>(type, methodName);
        originalMethod = dele;
        return new StaticInterceptorActionT9<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9>(method, dele);
    }
    /// <summary>Extension method for static method interceptor.</summary>
    public static StaticInterceptorFuncT9<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9> StaticJitest<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9>(this Type type, string methodName, out Func<T1, T2, T3, T4, T5, T6, T7, T8, T9>? originalMethod)
    {
        var (method, dele) = Extensions.ResolveMethod<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9>>(type, methodName);
        originalMethod = dele;
        return new StaticInterceptorFuncT9<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9>(method, dele);
    }
    /// <summary>Extension method for static method interceptor.</summary>
    public static StaticInterceptorActionT10<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> StaticJitest<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this Type type, string methodName, out Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>? originalMethod)
    {
        var (method, dele) = Extensions.ResolveMethod<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>>(type, methodName);
        originalMethod = dele;
        return new StaticInterceptorActionT10<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(method, dele);
    }
    /// <summary>Extension method for static method interceptor.</summary>
    public static StaticInterceptorFuncT10<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> StaticJitest<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this Type type, string methodName, out Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>? originalMethod)
    {
        var (method, dele) = Extensions.ResolveMethod<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>>(type, methodName);
        originalMethod = dele;
        return new StaticInterceptorFuncT10<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(method, dele);
    }
    /// <summary>Extension method for static method interceptor.</summary>
    public static StaticInterceptorActionT11<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> StaticJitest<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(this Type type, string methodName, out Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>? originalMethod)
    {
        var (method, dele) = Extensions.ResolveMethod<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>>(type, methodName);
        originalMethod = dele;
        return new StaticInterceptorActionT11<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(method, dele);
    }
    /// <summary>Extension method for static method interceptor.</summary>
    public static StaticInterceptorFuncT11<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> StaticJitest<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(this Type type, string methodName, out Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>? originalMethod)
    {
        var (method, dele) = Extensions.ResolveMethod<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>>(type, methodName);
        originalMethod = dele;
        return new StaticInterceptorFuncT11<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(method, dele);
    }
    /// <summary>Extension method for static method interceptor.</summary>
    public static StaticInterceptorActionT12<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> StaticJitest<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(this Type type, string methodName, out Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>? originalMethod)
    {
        var (method, dele) = Extensions.ResolveMethod<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>>(type, methodName);
        originalMethod = dele;
        return new StaticInterceptorActionT12<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(method, dele);
    }
    /// <summary>Extension method for static method interceptor.</summary>
    public static StaticInterceptorFuncT12<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> StaticJitest<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(this Type type, string methodName, out Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>? originalMethod)
    {
        var (method, dele) = Extensions.ResolveMethod<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>>(type, methodName);
        originalMethod = dele;
        return new StaticInterceptorFuncT12<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(method, dele);
    }
    /// <summary>Extension method for static method interceptor.</summary>
    public static StaticInterceptorActionT13<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> StaticJitest<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(this Type type, string methodName, out Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>? originalMethod)
    {
        var (method, dele) = Extensions.ResolveMethod<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>>(type, methodName);
        originalMethod = dele;
        return new StaticInterceptorActionT13<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(method, dele);
    }
    /// <summary>Extension method for static method interceptor.</summary>
    public static StaticInterceptorFuncT13<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> StaticJitest<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(this Type type, string methodName, out Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>? originalMethod)
    {
        var (method, dele) = Extensions.ResolveMethod<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>>(type, methodName);
        originalMethod = dele;
        return new StaticInterceptorFuncT13<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(method, dele);
    }
    /// <summary>Extension method for static method interceptor.</summary>
    public static StaticInterceptorActionT14<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> StaticJitest<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(this Type type, string methodName, out Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>? originalMethod)
    {
        var (method, dele) = Extensions.ResolveMethod<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>>(type, methodName);
        originalMethod = dele;
        return new StaticInterceptorActionT14<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(method, dele);
    }
    /// <summary>Extension method for static method interceptor.</summary>
    public static StaticInterceptorFuncT14<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> StaticJitest<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(this Type type, string methodName, out Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>? originalMethod)
    {
        var (method, dele) = Extensions.ResolveMethod<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>>(type, methodName);
        originalMethod = dele;
        return new StaticInterceptorFuncT14<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(method, dele);
    }
    /// <summary>Extension method for static method interceptor.</summary>
    public static StaticInterceptorActionT15<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> StaticJitest<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(this Type type, string methodName, out Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>? originalMethod)
    {
        var (method, dele) = Extensions.ResolveMethod<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>>(type, methodName);
        originalMethod = dele;
        return new StaticInterceptorActionT15<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(method, dele);
    }
    /// <summary>Extension method for static method interceptor.</summary>
    public static StaticInterceptorFuncT15<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> StaticJitest<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(this Type type, string methodName, out Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>? originalMethod)
    {
        var (method, dele) = Extensions.ResolveMethod<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>>(type, methodName);
        originalMethod = dele;
        return new StaticInterceptorFuncT15<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(method, dele);
    }
    /// <summary>Extension method for static method interceptor.</summary>
    public static StaticInterceptorFuncT16<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> StaticJitest<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(this Type type, string methodName, out Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>? originalMethod)
    {
        var (method, dele) = Extensions.ResolveMethod<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>>(type, methodName);
        originalMethod = dele;
        return new StaticInterceptorFuncT16<TTarget, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(method, dele);
    }
}