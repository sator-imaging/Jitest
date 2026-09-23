// Licensed under the Apache-2.0 License
// https://github.com/sator-imaging/Jitest

using MonoMod.Utils;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Reflection;

namespace Jitest;

/// <summary>
/// Jitest extensions.
/// </summary>
internal static class Extensions
{
    static readonly ConcurrentDictionary<EquatableMethodInfo, Delegate> OriginalMethodByMethodInfo = new();

    // TODO: Lambda interception for value types seems not supported?
    //       Try using MethodInfo to MethodInfo interception instead.
    /// <inheritdoc cref="Jitest{TDelegate}(object, string, out TDelegate)"/>
    [Obsolete("Value type method interception is not supported", error: true)]
    public static Interceptor Jitest<TDelegate>(this ValueType instance, string methodName, out TDelegate originalMethod)
        where TDelegate : Delegate
    {
        object obj = instance;
        return Jitest(obj, methodName, out originalMethod);
    }


    /// <inheritdoc cref="Jitest{TDelegate}(object, string, out TDelegate)"/>
    public static Interceptor Jitest<TDelegate>(this IDisposable instance, string methodName, out TDelegate originalMethod)
        where TDelegate : Delegate
    {
        // Don't dispose
        object obj = instance;
        return Jitest(obj, methodName, out originalMethod);
    }

    /// <inheritdoc cref="Jitest{TDelegate}(object, string, out TDelegate)"/>
    public static Interceptor Jitest<TDelegate>(this IAsyncDisposable instance, string methodName, out TDelegate originalMethod)
        where TDelegate : Delegate
    {
        // Don't dispose
        object obj = instance;
        return Jitest(obj, methodName, out originalMethod);
    }

    /// <summary>
    /// Instance method interceptor.
    /// </summary>
    /// <returns><see cref="Interceptor"/></returns>
    public static Interceptor Jitest<TDelegate>(this object instance, string methodName, out TDelegate originalMethod)
        where TDelegate : Delegate
    {
        var (method, dele) = ResolveMethod<TDelegate>(instance.GetType(), methodName, instance);
        originalMethod = dele;
        return new(method, instance);
    }

    /// <summary>
    /// Static method interceptor.
    /// </summary>
    /// <inheritdoc cref="Jitest{TDelegate}(object, string, out TDelegate)"/>
    public static Interceptor Jitest<TDelegate>(this Type type, string methodName, out TDelegate originalMethod)
        where TDelegate : Delegate
    {
        var (method, dele) = ResolveMethod<TDelegate>(type, methodName, instance: null);
        originalMethod = dele;
        return new(method, instance: null);
    }


    static (MethodInfo, TDelegate) ResolveMethod<TDelegate>(Type type, string methodName, object? instance = null)
        where TDelegate : Delegate
    {
        var parameters = typeof(TDelegate).Name.StartsWith("Func", StringComparison.Ordinal)  // .Name == Func`N
            ? typeof(TDelegate).GenericTypeArguments[..^1]
            : typeof(TDelegate).GenericTypeArguments;

        var methodParams = parameters;
        if (instance != null && parameters.Length > 0 && parameters[0].IsAssignableFrom(type))
        {
            methodParams = parameters[1..];
        }

        // Allow enum conversion
        const BindingFlags AllBindingFlags = (BindingFlags)(~0);

        var method = type.GetMethod(methodName, AllBindingFlags, binder: null, methodParams, modifiers: null);
        if (method == null)
        {
            var bt = type.BaseType;
            while (bt != null)
            {
                method = bt.GetMethod(methodName, AllBindingFlags, binder: null, methodParams, modifiers: null);
                if (method != null)
                {
                    break;
                }
                else
                {
                    bt = bt.BaseType;
                }
            }
        }

        if (method == null)
        {
            throw new JitestException(
                $"Method '{methodName}({string.Join(", ", methodParams.Select(static x => x.Name))})' is not found in the type hierarchy of '{type}'");
        }

        try
        {
            var cloneMethod = OriginalMethodByMethodInfo.GetOrAdd(
                method,
                static (methodKey) =>
                {
                    using var dmd = new DynamicMethodDefinition(methodKey.MethodInfo);
                    var copy = dmd.Generate();
                    try
                    {
                        return copy.CreateDelegate<TDelegate>();
                    }
                    catch (ArgumentException)
                    {
                        return null!;
                    }
                }) as TDelegate;

            if (cloneMethod == null && instance != null)
            {
                using var dmd = new DynamicMethodDefinition(method);
                var copy = dmd.Generate();
                try
                {
                    cloneMethod = copy.CreateDelegate<TDelegate>();
                }
                catch (ArgumentException)
                {
                    cloneMethod = copy.CreateDelegate<TDelegate>(instance);
                }
            }

            if (cloneMethod == null) throw new JitestException("Must not be reached");

            return (method, cloneMethod);
        }
        catch (ArgumentException ex)
        {
            throw new JitestException($"Method parameter type or return type doesn't match: {ex.Message}", inner: ex);
        }
    }
}
