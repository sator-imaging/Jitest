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
public static class Extensions
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
        var (method, dele) = ResolveMethod<TDelegate>(instance.GetType(), methodName);
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
        var (method, dele) = ResolveMethod<TDelegate>(type, methodName);
        originalMethod = dele;
        return new(method, instance: null);
    }


    internal static (MethodInfo, TDelegate?) ResolveMethod<TDelegate>(Type type, string methodName)
        where TDelegate : class
    {
        Type[] parameters;
        if (typeof(TDelegate) == typeof(Delegate))
        {
            parameters = Array.Empty<Type>();
        }
        else if (typeof(TDelegate).Name.StartsWith("Func", StringComparison.Ordinal))
        {
            parameters = typeof(TDelegate).GenericTypeArguments[..^1];
        }
        else if (typeof(TDelegate).Name.StartsWith("Action", StringComparison.Ordinal))
        {
            parameters = typeof(TDelegate).GenericTypeArguments;
        }
        else
        {
            parameters = Array.Empty<Type>();
        }

        const BindingFlags AllBindingFlags = (BindingFlags)(~0);

        MethodInfo? method = null;
        if (parameters.Length > 0 || typeof(TDelegate) != typeof(Delegate))
        {
            method = type.GetMethod(methodName, AllBindingFlags, binder: null, parameters, modifiers: null);
        }

        if (method == null)
        {
            var methods = type.GetMethods(AllBindingFlags).Where(m => m.Name == methodName).ToArray();
            if (methods.Length == 1)
            {
                method = methods[0];
            }
            else if (methods.Length > 1 && parameters.Length > 0)
            {
                method = methods.FirstOrDefault(m => m.GetParameters().Length == parameters.Length);
            }
        }

        if (method == null)
        {
            var bt = type.BaseType;
            while (bt != null)
            {
                method = bt.GetMethod(methodName, AllBindingFlags, binder: null, parameters, modifiers: null);
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
                $"Method '{methodName}({string.Join(", ", parameters.Select(static x => x.Name))})' is not found in the type hierarchy of '{type}'");
        }

        if (typeof(TDelegate) == typeof(Delegate))
        {
            return (method, null);
        }

        try
        {
            var cloneMethod = OriginalMethodByMethodInfo.GetOrAdd(
                method,
                static (method) =>
                {
                    using var dmd = new DynamicMethodDefinition(method.MethodInfo);
                    var copy = dmd.Generate();

                    return copy.CreateDelegate(typeof(TDelegate));
                })
                as TDelegate;

            return (method, cloneMethod);
        }
        catch (ArgumentException)
        {
            return (method, null);
        }
    }
}
